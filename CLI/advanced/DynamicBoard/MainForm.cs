using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;


namespace DynamicBoard
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private System.Guid rootId = new System.Guid();

        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IFeatureDataSet dataset = null;

        IGeometryFactory geoFactory = null;
        IPoint positionPoint = null;

        IModelPoint fdepoint_biaopan = null;
        IRenderModelPoint render_biaopan = null;
        IModelPoint fdepoint_ZhiZhen = null;
        IRenderModelPoint render_ZhiZhen = null;
        ILabel label = null;

        double biaoPanX = 15377.60;
        double biaoPanY = 35641.73;
        double biaoPanZ = 5.00;
        public static int zhiZhenValue = 0; //设置初始指针值
        Random random1 = new Random();

        DXForm dxform;
        private int clickFlag = 0;

        // 线程转发
        public int MainThreadId = 0;
        

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);
            this.axRenderControl1.Camera.FlyTime = 1;

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;

            this.axRenderControl1.Camera.NearClipPlane = 0.1f;
            this.axRenderControl1.Camera.AutoClipPlane = false;

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

            // 加载FDB场景
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
                dataset = ds.OpenFeatureDataset(setnames[0]);
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

                    this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, null, null, rootId);

                    //if (!hasfly)
                    //{
                    //    IFieldInfoCollection fieldinfos = fc.GetFields();
                    //    IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                    //    IGeometryDef geometryDef = fieldinfo.GeometryDef;
                    //    IEnvelope env = geometryDef.Envelope;
                    //    if (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                    //        env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0)
                    //        continue;
                    //    IEulerAngle angle = new EulerAngle();
                    //    angle.Set(0, -20, 0);
                    //    this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    //}
                    //hasfly = true;
                }
            }

            geoFactory = new GeometryFactory();
            positionPoint = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            positionPoint.SpatialCRS = dataset.SpatialReference;

            // 加载仪表盘
            string modelNameBiaoPan = (strMediaPath + @"\osg\Dashboard\yibiaopan.osg");
            fdepoint_biaopan = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            fdepoint_biaopan.ModelName = modelNameBiaoPan;
            fdepoint_biaopan.SetCoords(biaoPanX, biaoPanY, biaoPanZ, 0, 0);
            fdepoint_biaopan.SpatialCRS = dataset.SpatialReference;
            render_biaopan = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(fdepoint_biaopan, null, rootId);
            // 加载指针
            string modelNameZhiZhen = (strMediaPath + @"\osg\Dashboard\zhizhen.osg");
            fdepoint_ZhiZhen = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            fdepoint_ZhiZhen.ModelName = modelNameZhiZhen;
            fdepoint_ZhiZhen.SetCoords(biaoPanX, biaoPanY, biaoPanZ + 0.01, 0, 0);
            fdepoint_ZhiZhen.SpatialCRS = dataset.SpatialReference;
            render_ZhiZhen = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(fdepoint_ZhiZhen, null, rootId);
            // 加载文字标签
            IVector3 positionLabel = new Vector3();
            positionLabel.Set(biaoPanX, biaoPanY, biaoPanZ + 0.1);
            positionPoint.Position = positionLabel;
            label = this.axRenderControl1.ObjectManager.CreateLabel(rootId);
            label.Text = "当前的压力值是：" + zhiZhenValue.ToString();
            label.Position = positionPoint;
            ITextSymbol textSymbol = new TextSymbol();
            TextAttribute textAttribute = new TextAttribute();
            textAttribute.TextColor = System.Drawing.Color.Black;
            textAttribute.TextSize = 30;
            textAttribute.Font = "宋体";
            textSymbol.TextAttribute = textAttribute;
            textSymbol.MaxVisualDistance = 3;
            label.TextSymbol = textSymbol;
            // 飞入表盘
            IEulerAngle angle_camera = new EulerAngle();
            angle_camera.Set(0, -45, 0);
            this.axRenderControl1.Camera.LookAt2(positionPoint, 2, angle_camera);

            // 注册鼠标单击事件
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DynamicBoard.html";
            }
        }


        private void btnStartRealtime_Click(object sender, System.EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            // 删除原有指针
            if (render_ZhiZhen != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(render_ZhiZhen.Guid);
                render_ZhiZhen = null;
            }
            // 加载指针
            string modelNameZhiZhen = (strMediaPath + @"\osg\Dashboard\zhizhen.osg");
            fdepoint_ZhiZhen = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            fdepoint_ZhiZhen.ModelName = modelNameZhiZhen;
            fdepoint_ZhiZhen.SetCoords(biaoPanX, biaoPanY, biaoPanZ + 0.01, 0, 0);
            fdepoint_ZhiZhen.SpatialCRS = dataset.SpatialReference;
            zhiZhenValue = random1.Next(0, 16);
            fdepoint_ZhiZhen.SelfRotate(0, 0, 1, -((zhiZhenValue % 16) * 3 * 3.14 / 32));
            render_ZhiZhen = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(fdepoint_ZhiZhen, null, rootId);
            label.Text = "当前的压力值是：" + zhiZhenValue.ToString();

            // 刷新弹出窗口
            if (clickFlag == 1)
            {
                if (dxform.IsDisposed)
                {
                    return;
                }
                else
                {
                    dxform.arcScaleComponent1.Value = zhiZhenValue;
                    dxform.Refresh();
                }
            }  
        }

        private void btnSelectBiaopan_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectRenderGeometry;
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            IPickResult pr = PickResult;
            IRenderModelPointPickResult renderpr = pr as IRenderModelPointPickResult;
            if (renderpr != null)
            {
                IRenderModelPoint mp = renderpr.ModelPoint;
                if (mp.ModelName.Contains("yibiaopan.osg") || mp.ModelName.Contains("zhizhen.osg"))
                {
                    dxform = new DXForm();
                    dxform.Show();
                    clickFlag = 1;
                }
            }
        }

        private void btnRover_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }
 
    }
}
