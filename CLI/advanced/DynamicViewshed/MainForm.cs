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

namespace DynamicViewshed
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private string wkt = string.Empty;
        private System.Guid rootId = new System.Guid();

        private IVector3 position = new Vector3();
        private IEulerAngle angle = new EulerAngle();
        private IPoint point = null;

        private IDynamicObject dynamicObject = null;
        private IPolyline line = null;
        private IRenderPolyline rline = null;
        private ISkinnedMesh skinMesh = null;
        private IViewshed tv = null;

        private void init()
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);
            this.axRenderControl1.Camera.FlyTime = 1;

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;

            // 设置天空盒
            
            if(System.IO.Directory.Exists(strMediaPath))
            {
                string tmpSkyboxPath = strMediaPath + @"\skybox";
                ISkyBox skybox = this.axRenderControl1.ObjectManager.GetSkyBox(0);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\1_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\1_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\1_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\1_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\1_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\1_UP.jpg");
            }
            else
            {
                MessageBox.Show("请不要随意更改SDK目录名");
                return;
            }

            string tmpTileLayerPath = (strMediaPath + @"\sdk.tdb");
            I3DTileLayer layer = this.axRenderControl1.ObjectManager.Create3DTileLayer(tmpTileLayerPath, "", rootId);
            //this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
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
            LoadSkinMeshAndViewshed();
               
            this.cbFlyMode.SelectedIndex = 0;

            // 获取设置参数
            cbAutoRepeat.Checked = dynamicObject.AutoRepeat;
            numTurnSpeed.Value = (decimal)dynamicObject.TurnSpeed;
            comboxMotionStyle.SelectedIndex = getIndexFromEnum(dynamicObject.MotionStyle);
            numAspectRatio.Value = (decimal)tv.AspectRatio;
            numFarClip.Value = (decimal)tv.FarClip;
            numFieldOfView.Value = (decimal)tv.FieldOfView;
           
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DynamicViewshed.html";
            }
        }

        private void LoadDynamicObjectAndLineFromFile()
        {
            ICameraTour tour = null;
            string tourPath = (strMediaPath + @"\xml\CameraTour_2.xml");
            if (File.Exists(tourPath))
            {
                StreamReader sr = new StreamReader(tourPath);
                string xmlstring = sr.ReadToEnd();
                sr.Close();
                tour = this.axRenderControl1.ObjectManager.CreateCameraTour(rootId);
                tour.FromXml(xmlstring);
            }
            if (tour == null)
                MessageBox.Show("xml文件读取失败");

            dynamicObject.CrsWKT = tour.CrsWKT;
            point.SpatialCRS = new CRSFactory().CreateFromWKT(tour.CrsWKT) as ISpatialCRS;
            line.SpatialCRS = new CRSFactory().CreateFromWKT(tour.CrsWKT) as ISpatialCRS;

            for (int i = 0; i < tour.WaypointsNumber; i++)
            {
                double duration;
                gviCameraTourMode mode;
                tour.GetWaypoint(i, out position, out angle, out duration, out mode);
                dynamicObject.AddWaypoint(position, 10);

                point.Position = position;
                if (line.PointCount == 0)
                    line.StartPoint = point;
                else
                    line.AddPointAfter(i - 1, point);
            }           

            ICurveSymbol cur = new CurveSymbol();
            cur.Color = System.Drawing.Color.Yellow;
            cur.Width = -2;
            rline = this.axRenderControl1.ObjectManager.CreateRenderPolyline(line, cur, rootId);
        }

        private void LoadSkinMeshAndViewshed()
        {
            string fileName = (strMediaPath + @"\x\Character\QiYeYuanGong.X");
            if (skinMesh == null)
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
                skinMesh = this.axRenderControl1.ObjectManager.CreateSkinnedMesh(mp, rootId);
                if (skinMesh == null)
                {
                    MessageBox.Show("骨骼动画创建失败！");
                    return;
                }
                skinMesh.Loop = true;
                skinMesh.Play();
                skinMesh.MaxVisibleDistance = 1000;
                skinMesh.ViewingDistance = 50;

                // 绑定到运动路径
                IMotionable m = skinMesh as IMotionable;
                position.Set(0, 0, 0);
                m.Bind2(dynamicObject, position, 0, 0, 0);
                this.axRenderControl1.Camera.FlyToObject(skinMesh.Guid, gviActionCode.gviActionFollowBehind);  
            }

            if (tv == null)
            {
                tv = this.axRenderControl1.ObjectManager.CreateViewshed(line.GetPoint(0), rootId);
                IMotionable tvm = tv as IMotionable;
                position.Set(0, 0, 0);
                tvm.Bind2(dynamicObject, position, 0, 0, 0);
            }
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
                    this.axRenderControl1.Camera.FlyToObject(this.skinMesh.Guid, gviActionCode.gviActionFollowBehind);
                    break;
                case "后上方跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMesh.Guid, gviActionCode.gviActionFollowBehindAndAbove);
                    break;
                case "正上方跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMesh.Guid, gviActionCode.gviActionFollowAbove);
                    break;
                case "正下方跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMesh.Guid, gviActionCode.gviActionFollowBelow);
                    break;
                case "左侧跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMesh.Guid, gviActionCode.gviActionFollowLeft);
                    break;
                case "右侧跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMesh.Guid, gviActionCode.gviActionFollowRight);
                    break;
                case "座舱跟随":
                    this.axRenderControl1.Camera.FlyToObject(this.skinMesh.Guid, gviActionCode.gviActionFollowCockpit);
                    break;
            }
        }

        private void cbDrawLine_CheckedChanged(object sender, EventArgs e)
        {
            rline.VisibleMask = cbDrawLine.Checked ? gviViewportMask.gviView0 : gviViewportMask.gviViewNone;
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

        private void numAspectRatio_ValueChanged(object sender, EventArgs e)
        {
            if (tv != null)
                tv.AspectRatio = double.Parse(numAspectRatio.Value.ToString());
        }

        private void numFarClip_ValueChanged(object sender, EventArgs e)
        {
            if (tv != null)
                tv.FarClip = double.Parse(numFarClip.Value.ToString());
        }

        private void numFieldOfView_ValueChanged(object sender, EventArgs e)
        {
            if (tv != null)
                tv.FieldOfView = double.Parse(numFieldOfView.Value.ToString());
        }

        private void cbShowProjectionLines_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cbShowProjector_CheckedChanged(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
