using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System.Xml;
using Gvitech.CityMaker.Controls;

namespace MotionPath
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private string wkt = string.Empty;

        private IVector3 position = new Vector3();
        private IPoint point = null;

        private IDynamicObject dynamicObject = null;
        private IPolyline line = null;
        private IRenderPolyline rline = null;

        private ISkinnedMesh skinMeshPlane = null;
        private ITableLabel dynamicTableLabel = null;

        private FrameCallback frameCallBack = null;

        private System.Guid rootId = new System.Guid();
        _IRenderControlEvents_RcFrameEventHandler OnFrame;

        private void init()
        {
            // 设置RenderControl控件为球
            
            string tmpTedPath = (strMediaPath + @"\terrain\SingaporeGlobeTerrain.ted");
            wkt = this.axRenderControl1.GetTerrainCrsWKT(tmpTedPath, "");
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", "OpenGL");
            this.axRenderControl1.Initialize2(wkt, ps);
            this.axRenderControl1.Camera.FlyTime = 1;

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;

            // 注册地形
            this.axRenderControl1.Terrain.RegisterTerrain(tmpTedPath, "");
            this.axRenderControl1.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);

            this.axRenderControl1.RcFrame += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcFrameEventHandler(axRenderControl1_RcFrame);
            this.OnFrame = new _IRenderControlEvents_RcFrameEventHandler(OnFrameUpdateUI);

            #region 加载FDB场景
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\SDKDEMO.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                fcMap = new Hashtable(fcnames.Length);
                foreach (string name in fcnames)
                {
                    IFeatureClass fc = dataset.OpenFeatureClass(name);
                    // 找到空间列字段
                    List<string> geoNames = new List<string>();
                    IFieldInfoCollection fieldinfos = fc.GetFields();
                    for (int i = 0; i < fieldinfos.Count; i++)
                    {
                        IFieldInfo fieldinfo = fieldinfos.Get(i);
                        if (null == fieldinfo)
                            continue;
                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        if (null == geometryDef)
                            continue;
                        geoNames.Add(fieldinfo.Name);
                    }
                    fcMap.Add(fc, geoNames);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            // CreateFeautureLayer
            //bool hasfly = false;
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    if (!geoName.Equals("Geometry"))
                        continue;

                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                    fc, geoName, null, null, rootId);

                    //if (!hasfly)
                    //{
                    //    IFieldInfoCollection fieldinfos = fc.GetFields();
                    //    IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                    //    IGeometryDef geometryDef = fieldinfo.GeometryDef;
                    //    IEnvelope env = geometryDef.Envelope;
                    //    if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                    //        env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                    //        continue;
                    //    angle.Set(0, -20, 0);
                    //    point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    //    point.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                    //    point.SetCoords(env.Center.X, env.Center.Y, env.Center.Z, 0, 0);
                    //    this.axRenderControl1.Camera.LookAt2(point, 500, angle);
                    //}
                    //hasfly = true;
                }
            }
            #endregion 
        }

        void axRenderControl1_RcFrame(int FrameIndex, double ReferencedTime)
        {
            this.BeginInvoke(OnFrame, new Object[] { FrameIndex, ReferencedTime });
        }

        public MainForm()
        {
            InitializeComponent();         
        }

        private void LoadDynamicObjectAndLineFromFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + @"\MotionPath.xml");
            wkt = xmlDoc.SelectSingleNode("root/WKT").InnerText;
            // 指定坐标系与xml里的相同
            dynamicObject.CrsWKT = xmlDoc.SelectSingleNode("root/WKT").InnerText;
            point.SpatialCRS = new CRSFactory().CreateFromWKT(dynamicObject.CrsWKT) as ISpatialCRS;
            line.SpatialCRS = new CRSFactory().CreateFromWKT(dynamicObject.CrsWKT) as ISpatialCRS;

            XmlNodeList nodes = xmlDoc.SelectNodes("root/Waypoint");
            int i = 0;
            foreach (XmlNode node in nodes)
            {
                double x = double.Parse(node.SelectSingleNode("X").InnerText);
                double y = double.Parse(node.SelectSingleNode("Y").InnerText);
                double z = double.Parse(node.SelectSingleNode("Z").InnerText);
                position.Set(x, y, z);
                point.Position = position;
                if (line.PointCount == 0)
                {
                    line.StartPoint = point;
                }
                else
                    line.AddPointAfter(i - 1, point);
                i++;
                double when = double.Parse(node.SelectSingleNode("When").InnerText);              
                dynamicObject.AddWaypoint2(point, when);
                this.axRenderControl1.ObjectManager.CreateRenderPoint(point, null, rootId);
            }

            ICurveSymbol cur = new CurveSymbol();
            cur.Color = System.Drawing.Color.Red;
            cur.Width = -2;
            rline = this.axRenderControl1.ObjectManager.CreateRenderPolyline(line, cur, rootId);
        }

        private void LoadPlane()
        {
            // 加载直升飞机
            string fileName = (strMediaPath + @"\x\Vehicles\wrj.X");
            if (skinMeshPlane == null)
            {
                // 构造ModelPoint
                IGeometryFactory gf = new GeometryFactory();
                IModelPoint mp = gf.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                mp.ModelName = fileName;
                mp.SpatialCRS = new CRSFactory().CreateFromWKT(wkt) as ISpatialCRS;
                // 设置位置
                IMatrix matrix = new Matrix();
                matrix.MakeIdentity();
                matrix.SetTranslate(line.GetPoint(0).Position);
                mp.FromMatrix(matrix);
                // 创建骨骼动画
                skinMeshPlane = this.axRenderControl1.ObjectManager.CreateSkinnedMesh(mp, rootId);
                if (skinMeshPlane == null)
                {
                    MessageBox.Show("骨骼动画创建失败！");
                    return;
                }
                skinMeshPlane.Loop = true;
                skinMeshPlane.Play();
                skinMeshPlane.MaxVisibleDistance = 1000;
                skinMeshPlane.ViewingDistance = 100;

                // 绑定到运动路径
                IMotionable m = skinMeshPlane as IMotionable;
                position.Set(0, 0, 0);
                m.Bind2(dynamicObject, position, 0, 0, 0);
            }
        }

        private void LoadTableLabel()
        {
            dynamicTableLabel = this.axRenderControl1.ObjectManager.CreateTableLabel(3, 2, rootId);

            dynamicTableLabel.TitleText = "直升机当前位置";
            dynamicTableLabel.SetRecord(0, 0, "X:");
            dynamicTableLabel.SetRecord(0, 1, line.GetPoint(0).X.ToString());
            dynamicTableLabel.SetRecord(1, 0, "Y:");
            dynamicTableLabel.SetRecord(1, 1, line.GetPoint(0).Y.ToString());
            dynamicTableLabel.SetRecord(2, 0, "Z:");
            dynamicTableLabel.SetRecord(2, 1, line.GetPoint(0).Z.ToString());

            dynamicTableLabel.Position = line.GetPoint(0);

            dynamicTableLabel.BorderColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            dynamicTableLabel.BorderWidth = 2;
            dynamicTableLabel.TableBackgroundColor = System.Drawing.Color.FromArgb(200, 255, 255, 165);
            dynamicTableLabel.TitleBackgroundColor = System.Drawing.Color.FromArgb(180, 122, 122, 122);

            // 第一列文本样式
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = System.Drawing.Color.FromArgb(120, 127, 64, 0);
            headerTextAttribute.OutlineColor = System.Drawing.Color.Red;
            headerTextAttribute.Font = "细黑";
            headerTextAttribute.Bold = true;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            dynamicTableLabel.SetColumnTextAttribute(0, headerTextAttribute);

            // 第二列文本样式
            TextAttribute contentTextAttribute = new TextAttribute();
            contentTextAttribute.TextColor = System.Drawing.Color.Black;
            contentTextAttribute.OutlineColor = System.Drawing.Color.FromArgb(125, 255, 127, 64);
            contentTextAttribute.Font = "黑体";
            contentTextAttribute.Bold = false;
            contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            dynamicTableLabel.SetColumnTextAttribute(1, contentTextAttribute);

            // 标题文本样式
            TextAttribute capitalTextAttribute = new TextAttribute();
            capitalTextAttribute.TextColor = System.Drawing.Color.White;
            capitalTextAttribute.OutlineColor = System.Drawing.Color.Gray;
            capitalTextAttribute.Font = "华文新魏";
            capitalTextAttribute.TextSize = 14;
            capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.Bold = true;
            dynamicTableLabel.TitleTextAttribute = capitalTextAttribute;

            // 绑定到运动路径
            IMotionable m = dynamicTableLabel as IMotionable;
            position.Set(0, 0, 2);
            m.Bind2(dynamicObject, position, 0, 0, 0);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            dynamicObject.Play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            dynamicObject.Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            dynamicObject.Stop();
            dynamicObject.Index = 0; //让播放节点恢复到从路径第0个点开始播放
        }

        private void btnFlyToObject_Click(object sender, EventArgs e)
        {
            switch (cbFlyMode.SelectedItem.ToString())
            {
                case "正后方跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFollowBehind);
                    break;
                case "后上方跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFollowBehindAndAbove);
                    break;
                case "正上方跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFollowAbove);
                    break;
                case "正下方跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFollowBelow);
                    break;
                case "左侧跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFollowLeft);
                    break;
                case "右侧跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFollowRight);
                    break;
                case "座舱跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFollowCockpit);
                    break;
            }
        }

        private void cbDrawLine_CheckedChanged(object sender, EventArgs e)
        {
            rline.VisibleMask = cbDrawLine.Checked ? gviViewportMask.gviView0 : gviViewportMask.gviViewNone;
        }

        private void OnFrameUpdateUI(int frameIndex, double refTime)
        {
            if (this.dynamicTableLabel != null && this.skinMeshPlane != null)
            {
                dynamicTableLabel.SetRecord(0, 1, this.skinMeshPlane.ModelPoint.Position.X.ToString());
                dynamicTableLabel.SetRecord(1, 1, this.skinMeshPlane.ModelPoint.Position.Y.ToString());
                dynamicTableLabel.SetRecord(2, 1, this.skinMeshPlane.ModelPoint.Position.Z.ToString());
            }
        }

        private gviDynamicMotionStyle getMotionStyleFromString(string value)
        {
            switch (value)
            {
                case "GroundVehicle":
                    return gviDynamicMotionStyle.gviDynamicMotionGroundVehicle;
                case "Airplane":
                    return gviDynamicMotionStyle.gviDynamicMotionAirplane;
                case "Helicopter":
                    return gviDynamicMotionStyle.gviDynamicMotionHelicopter;
                case "Hover":
                    return gviDynamicMotionStyle.gviDynamicMotionHover;
            }
            return gviDynamicMotionStyle.gviDynamicMotionHover;
        }

        private int getIndexFromEnum(gviDynamicMotionStyle value)
        {
            switch (value)
            {
                case gviDynamicMotionStyle.gviDynamicMotionAirplane:
                    return 1;
                case gviDynamicMotionStyle.gviDynamicMotionGroundVehicle:
                    return 0;
                case gviDynamicMotionStyle.gviDynamicMotionHelicopter:
                    return 2;
                case gviDynamicMotionStyle.gviDynamicMotionHover:
                    return 3;
            }
            return 3;
        }

        #region 参数设置
        private void cbAutoRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (dynamicObject != null)
                dynamicObject.AutoRepeat = cbAutoRepeat.Checked;
        }

        private void comboxMotionStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dynamicObject != null)
                dynamicObject.MotionStyle = getMotionStyleFromString(comboxMotionStyle.SelectedItem.ToString());
        }

        private void numTurnSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (dynamicObject != null)
                dynamicObject.TurnSpeed = double.Parse(numTurnSpeed.Value.ToString());
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.axRenderControl1.OnFrameInvoke = null; //解除回调绑定
            Application.Exit();  //关闭应用程序
        }

        private void axRenderControl1_Load(object sender, EventArgs e)
        {
            init();

            // 运动路径定位点
            point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            //point.SpatialCRS = new CRSFactory().CreateFromWKT(wkt) as ISpatialCRS;

            // 加载运动路径
            dynamicObject = this.axRenderControl1.ObjectManager.CreateDynamicObject(rootId);
            //dynamicObject.CrsWKT = wkt;

            // 画出运动路径
            line = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            //line.SpatialCRS = new CRSFactory().CreateFromWKT(wkt) as ISpatialCRS;

            LoadDynamicObjectAndLineFromFile();
            LoadPlane();
            LoadTableLabel();

            this.axRenderControl1.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFollowCockpit);
            this.cbFlyMode.SelectedIndex = 6;

            // 获取设置参数
            cbAutoRepeat.Checked = dynamicObject.AutoRepeat;
            numTurnSpeed.Value = (decimal)dynamicObject.TurnSpeed;
            comboxMotionStyle.SelectedIndex = getIndexFromEnum(dynamicObject.MotionStyle);


            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DynamicObject.html";
            }
        }
    }
}
