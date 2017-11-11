using System;
using System.Collections.Generic;
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
using Gvitech.CityMaker.Controls;

namespace GeometryConvert2
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private System.Guid rootId = new System.Guid();

        private IFeatureClass _featureClass = null;  // 要素类
        private List<int> fidList = new List<int>();  //存储拾取的Feature的FID
        private List<IRenderModelPoint> mpList = new List<IRenderModelPoint>();  //存储创建的RenderModelPoint用于切换page时清除
        private List<IRenderTriMesh> tmList = new List<IRenderTriMesh>();  //存储创建的RenderTriMesh用于切换page时清除

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

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;
            this.axRenderControl1.Camera.FlyTime = 1;

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

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "GeometryConvert2.html";
            }

            // 设置界面默认值
            this.comboxRoofType.SelectedIndex = 0;
            this.comboBoxFacadeTexture.SelectedIndex = 0;
            this.comboBoxRoofTexture.SelectedIndex = 0;

            // 可视化Polygon类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polygon.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, true, "Polygon");
            }

            //绑定事件
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        // 公共方法
        void FeatureLayerVisualize(IConnectionInfo ci, bool needfly, string sourceName)
        {
            try
            {
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
                    _featureClass = fc;
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
            bool hasfly = !needfly;  
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                    fc, geoName, null, null, rootId);

                    IFieldInfoCollection fieldinfos = fc.GetFields();
                    IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                    IGeometryDef geometryDef = fieldinfo.GeometryDef;
                    IEnvelope env = geometryDef.Envelope;
                    if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                        env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                        continue;

                    // 相机飞入
                    if (!hasfly)
                    {
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            try
            {
                if (PickResult != null)
                {
                    if (PickResult.Type == gviObjectType.gviObjectFeatureLayer)
                    {
                        IFeatureLayerPickResult flpr = PickResult as IFeatureLayerPickResult;
                        int fid = flpr.FeatureId;
                        this.axRenderControl1.FeatureManager.HighlightFeature(_featureClass, fid, System.Drawing.Color.Yellow);

                        //////////////////////////////////////////////////////////////////////////
                        //
                        //  GeometryConvert的代码添加在这里
                        // 
                        //////////////////////////////////////////////////////////////////////////
                        fidList.Clear();
                        fidList.Add(fid);
                        IRowBuffer rowGC = _featureClass.GetRow(fidList[0]);

                        int nPose = rowGC.FieldIndex("Geometry");
                        if (nPose == -1)
                        {
                            MessageBox.Show("不存在Geometry列");
                            return;
                        }

                        // 获取polygon
                        IPolygon polygonGC = null;
                        if (rowGC != null)
                        {
                            nPose = rowGC.FieldIndex("Geometry");
                            IGeometry geo = rowGC.GetValue(nPose) as IGeometry;
                            if (geo.GeometryType == gviGeometryType.gviGeometryPolygon)
                                polygonGC = geo as IPolygon;
                        }

                        this.Text = "拾取成功";

                        //第一个Tab页：ExtrudePolygonToModel
                        if (this.tabControl1.SelectedIndex == 0)
                        {
                            // 1.调接口构造模型
                            IGeometryConvertor gc = new GeometryConvertor();
                            gviRoofType rooftype = gviRoofType.gviRoofFlat;
                            switch (this.comboxRoofType.Text)
                            {
                                case "Flat":
                                    rooftype = gviRoofType.gviRoofFlat;
                                    break;
                                case "Gable":
                                    rooftype = gviRoofType.gviRoofGable;
                                    break;
                                case "Hip":
                                    rooftype = gviRoofType.gviRoofHip;
                                    break;
                            }
                            string imgPath = (strMediaPath + @"\dds");
                            string roof = this.comboBoxRoofTexture.Text;
                            string facade = this.comboBoxFacadeTexture.Text;
                            IModelPoint mp = null;
                            IModel model = null;
                            if (!gc.ExtrudePolygonToModel(polygonGC, int.Parse(this.numFloorNumber.Value.ToString()), double.Parse(this.numFloorHeight.Value.ToString()), double.Parse(this.numSlopeAngle.Value.ToString()),
                                rooftype, facade, roof, out mp, out model))
                            {
                                MessageBox.Show("拉体块出错！");
                                return;
                            }

                            //2、将模型及贴图写入osg文件
                            string modelName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".osg");//输出osg文件路径 
                            IResourceFactory resFactory = new ResourceFactory();
                            string[] imageNames = model.GetImageNames();
                            IPropertySet ps = new PropertySet();
                            for (int i = 0; i < imageNames.Length; i++)
                            {
                                string imgName = imageNames[i];
                                IImage img = resFactory.CreateImageFromFile(string.Format(@"{0}\{1}", imgPath + @"\facade", imgName));
                                if (img == null)
                                    img = resFactory.CreateImageFromFile(string.Format(@"{0}\{1}", imgPath + @"\roof", imgName));
                                ps.SetProperty(imgName, img);
                            }
                            model.WriteFile(modelName, ps);

                            //3、测试显示模型
                            mp.ModelName = modelName;
                            IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, null, rootId);
                            rmp.MouseSelectMask = gviViewportMask.gviViewNone;
                            rmp.MaxVisibleDistance = 100000;
                            this.axRenderControl1.Camera.LookAtEnvelope(mp.Envelope);//飞入
                            mpList.Add(rmp);
                        }
                        //第二个tab页：ExtrudePolygonToTriMesh
                        else
                        {
                            // 1.调接口构造模型
                            IGeometryConvertor gc = new GeometryConvertor();
                            ITriMesh tm = gc.ExtrudePolygonToTriMesh(polygonGC, double.Parse(this.numHeight.Value.ToString()), true);
                            if (tm == null)
                            {
                                MessageBox.Show("拉体块出错！");
                                return;
                            }

                            //2、显示三角面
                            //---- 渲染样式不是必须的 -----
                            ISurfaceSymbol surfaceSym = new SurfaceSymbol();
                            surfaceSym.Color = System.Drawing.Color.Red;
                            surfaceSym.EnableLight = true;
                            ICurveSymbol curveSym = new CurveSymbol();
                            curveSym.Color = System.Drawing.Color.Yellow;
                            curveSym.Width = 10;
                            surfaceSym.BoundarySymbol = curveSym;
                            //---- ------------------ -----
                            IRenderTriMesh rmp = this.axRenderControl1.ObjectManager.CreateRenderTriMesh(tm, surfaceSym, rootId);
                            rmp.MouseSelectMask = gviViewportMask.gviViewNone;
                            rmp.MaxVisibleDistance = 100000;
                            this.axRenderControl1.Camera.LookAtEnvelope(rmp.Envelope);//飞入
                            tmList.Add(rmp);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
                    MessageBox.Show("需要标准runtime授权");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (IRenderModelPoint rmp in mpList)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(rmp.Guid);
            }
            mpList.Clear();
            foreach (IRenderTriMesh rtm in tmList)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(rtm.Guid);
            }
            tmList.Clear();
        }

    }

}
