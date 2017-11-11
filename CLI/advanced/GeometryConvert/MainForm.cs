using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Resource;
using System.Collections;
using Gvitech.CityMaker.Controls;


namespace GeometryConvert
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private IFeatureClass _featureClass = null;  //“Building”要素类
        private IFeatureLayer _featureLayerModelPoint = null;  //“Building”图层
        private List<int> fidList = new List<int>();  //存储拾取的Feature的FID

        List<IRenderTriMesh> rTMeshList = new List<IRenderTriMesh>();
        List<IRenderPolygon> rPolygonList = new List<IRenderPolygon>();  //投影Polygon
        List<IRenderPolygon> rPolygonList2 = new List<IRenderPolygon>();  //切割Polygon
        List<IRenderMultiPoint> rPointList = new List<IRenderMultiPoint>();

        private System.Guid rootId = new System.Guid();

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

                //仅仅加载“建筑”要素类
                _featureClass = dataset.OpenFeatureClass("Building");

                //仅仅用名为"Geometry"的空间列创建FeautureLayer  
                _featureLayerModelPoint = this.axRenderControl1.ObjectManager.CreateFeatureLayer(_featureClass, "Geometry", null, null, rootId);

                IFieldInfoCollection fieldinfos = _featureClass.GetFields();
                IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf("Geometry"));
                IGeometryDef geometryDef = fieldinfo.GeometryDef;
                IEnvelope env = geometryDef.Envelope;
                IEulerAngle angle = new EulerAngle();
                angle.Set(0, -20, 0);
                this.axRenderControl1.Camera.LookAt(env.Center, 500, angle);
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
            #endregion

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "GeometryConvert.html";
            }
        }

        private void cbSelectFeature_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectFeature.Checked)
            {
                this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
                
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
                this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            }
            else
            {
                this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
                
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;

                this.axRenderControl1.FeatureManager.UnhighlightAll();
            }
        }

        #region RenderControl事件
        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            try
            {
                this.axRenderControl1.FeatureManager.UnhighlightAll();
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

                        // 获取ModelPoint
                        IModelPoint modelPointGC = null;
                        if (rowGC != null)
                        {
                            nPose = rowGC.FieldIndex("Geometry");
                            IGeometry geo = rowGC.GetValue(nPose) as IGeometry;
                            if (geo.GeometryType == gviGeometryType.gviGeometryModelPoint)
                                modelPointGC = geo as IModelPoint;
                        }

                        this.Text = "拾取成功";

                        // 获取Model
                        string modelName = modelPointGC.ModelName;
                        IModel modelGC = (_featureClass.FeatureDataSet as IResourceManager).GetModel(modelName);

                        // 1、获取MultiTriMesh                        
                        IGeometryConvertor gc = new GeometryConvertor();
                        IMultiTriMesh multiTM = gc.ModelPointToTriMesh(modelGC, modelPointGC, false);
                        this.Text = "ModelToTriMesh完成";

                        if (this.cbCreateRenderTriMesh.Checked)
                        {
                            // 创建RenderTriMesh在三维上显示
                            for (int i = 0; i < multiTM.GeometryCount; i++)
                            {
                                ITriMesh tm = multiTM.GetGeometry(i) as ITriMesh;
                                IRenderTriMesh rtm = this.axRenderControl1.ObjectManager.CreateRenderTriMesh(tm, null, rootId);

                                //随机颜色填充TriMesh
                                Random randObj = new Random(i);
                                int aColor = randObj.Next(0, 255);
                                int gColor = randObj.Next(0, 255);
                                int rColor = randObj.Next(0, 255);
                                ISurfaceSymbol ss = new SurfaceSymbol();
                                ss.Color = System.Drawing.Color.FromArgb(rColor, gColor, aColor);
                                rtm.Symbol = ss;
                                rTMeshList.Add(rtm);
                            }
                        }

                        if (this.cbCreateRenderPolygon.Checked)
                        {
                            // 2、获取投影MultiPolygon
                            IMultiPolygon multiPolygon = gc.ProjectTriMeshToPolygon(multiTM, 1.0);
                            this.Text = "MultiTriMeshToFootprint完成。面积:" + multiPolygon.GetArea();

                            // 创建RenderPolygon在三维上显示
                            for (int i = 0; i < multiPolygon.GeometryCount; i++)
                            {
                                IPolygon polygon = multiPolygon.GetGeometry(i) as IPolygon;
                                IRenderPolygon rpolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(polygon, null, rootId);
                                rPolygonList.Add(rpolygon);
                            }
                        }

                        if (this.cbCreateRenderPolygon2.Checked)
                        {
                            // 3、获取切割MultiPolygon
                            double heightSpec = 0.0, heightIntersect = 0.0;
                            Double.TryParse(IntersectPoint.Z.ToString(), out heightIntersect);
                            fidList.Clear();
                            fidList.Add(fid);
                            IEnvelope box = _featureClass.GetFeaturesEnvelope(fidList.ToArray(), "Geometry");
                            heightSpec = System.Math.Abs(heightIntersect - box.MinZ);
                            // 注意：CutTriMeshToPolygon方法的第三个参数为空间分辨率，应该选择合适值。
                            // 值过大会导致结果不精确，值过小会导致转换时间过长。使用时应设置大小合适的值来平衡精度和效率问题。
                            IMultiPolygon multiPolygon2 = gc.CutTriMeshToPolygon(multiTM, heightSpec, 0.5);
                            this.Text = "MultiTriMeshToFootprint2完成。面积:" + multiPolygon2.GetArea();

                            // 创建RenderPolygon在三维上显示
                            for (int i = 0; i < multiPolygon2.GeometryCount; i++)
                            {
                                IPolygon polygon2 = multiPolygon2.GetGeometry(i) as IPolygon;
                                IRenderPolygon rpolygon2 = this.axRenderControl1.ObjectManager.CreateRenderPolygon(polygon2, null, rootId);
                                rPolygonList2.Add(rpolygon2);
                            }
                        }

                        if (this.cbCreateRenderMulPoint.Checked)
                        {
                            // 4、获取MultiPoint
                            IMultiPoint multiPoint = gc.MultiTriMeshToMultiPoint(multiTM, 3.0);
                            this.Text = "MultiTriMeshToMultiPoint完成。MultiPoint个数为：" + multiPoint.GeometryCount;

                            //创建RenderPoint在三维上显示
                            IRenderMultiPoint rpoint = this.axRenderControl1.ObjectManager.CreateRenderMultiPoint(multiPoint, null, rootId);
                            rPointList.Add(rpoint);
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
        #endregion

        #region 隐藏、删除
        private void cbHideRenderTMesh_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < rTMeshList.Count; i++)
                rTMeshList[i].VisibleMask = cbHideRenderTMesh.Checked ? gviViewportMask.gviViewNone : gviViewportMask.gviViewAllNormalView;
        }

        private void cbHideRenderPolygon_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < rPolygonList.Count; i++)
                rPolygonList[i].VisibleMask = cbHideRenderPolygon.Checked ? gviViewportMask.gviViewNone : gviViewportMask.gviViewAllNormalView;
        }

        private void cbHideRenderPolygon2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < rPolygonList2.Count; i++)
                rPolygonList2[i].VisibleMask = cbHideRenderPolygon2.Checked ? gviViewportMask.gviViewNone : gviViewportMask.gviViewAllNormalView;
        }

        private void cbHideRenderMultiPoint_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < rPointList.Count; i++)
                rPointList[i].VisibleMask = cbHideRenderMultiPoint.Checked ? gviViewportMask.gviViewNone : gviViewportMask.gviViewAllNormalView;
        }

        private void cbShowModelPointLayer_CheckedChanged(object sender, EventArgs e)
        {
            _featureLayerModelPoint.VisibleMask = cbShowModelPointLayer.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
        }

        private void btnDelRenderTMesh_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            if (rTMeshList.Count > 0)
            {
                for (int i = 0; i < rTMeshList.Count; i++)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(rTMeshList[i].Guid);
                }
                rTMeshList.Clear();
                MessageBox.Show("清空完成");
            }
            else
                MessageBox.Show("无要清空对象");
        }

        private void btnDelRenderPolygon_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            if (rPolygonList.Count > 0)
            {
                for (int i = 0; i < rPolygonList.Count; i++)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(rPolygonList[i].Guid);
                }
                rPolygonList.Clear();
                MessageBox.Show("清空完成");
            }
            else
                MessageBox.Show("无要清空对象");
        }

        private void btnDelRenderPolygon2_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            if (rPolygonList2.Count > 0)
            {
                for (int i = 0; i < rPolygonList2.Count; i++)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(rPolygonList2[i].Guid);
                }
                rPolygonList2.Clear();
                MessageBox.Show("清空完成");
            }
            else
                MessageBox.Show("无要清空对象");
        }

        private void btnDelRenderMultiPoint_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            if (rPointList.Count > 0)
            {
                for (int i = 0; i < rPointList.Count; i++)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(rPointList[i].Guid);
                }
                rPointList.Clear();
                MessageBox.Show("清空完成");
            }
            else
                MessageBox.Show("无要清空对象");
        }
        #endregion
    }
}
    