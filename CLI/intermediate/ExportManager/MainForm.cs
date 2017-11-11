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
using Gvitech.CityMaker.Controls;

namespace ExportManager
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private ProcessDialog processdlg;
        private IEnvelope env;  //加载数据时，初始化的矩形范围
        private IFeatureDataSet dataset = null;

        private enum ExportType { Export25D = 0, ExportDEM, ExportDOM, ExportOrthoImage, ExportPanorama };
        private ExportType type = ExportType.Export25D;

        private IGeometryFactory geoFactory = new GeometryFactory();
        private IPolyline polyline = null;
        private IRenderPolyline rpolyline = null;

        private List<System.Guid> fcGUIDs = new List<System.Guid>();

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

            // 注册出图事件
            this.axRenderControl1.RcPictureExportBegin += new _IRenderControlEvents_RcPictureExportBeginEventHandler(axRenderControl1_RcPictureExportBegin);
            
            this.axRenderControl1.RcPictureExporting += new _IRenderControlEvents_RcPictureExportingEventHandler(axRenderControl1_RcPictureExporting);
            
            this.axRenderControl1.RcPictureExportEnd += new _IRenderControlEvents_RcPictureExportEndEventHandler(axRenderControl1_RcPictureExportEnd);
            

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
            bool hasfly = false;
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    if (!geoName.Equals("Geometry"))
                        continue;

                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                    fc, geoName, null, null, rootId);

                    if (!hasfly)
                    {
                        IFieldInfoCollection fieldinfos = fc.GetFields();
                        IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }

            //// 注册地形
            //string tmpTedPath = (strMediaPath + @"\terrain\SingaporePlanarTerrain.ted");
            //this.axRenderControl1.Terrain.RegisterTerrain(tmpTedPath, "");

            // 绑定框选事件
            this.axRenderControl1.RcMouseDragSelect += new _IRenderControlEvents_RcMouseDragSelectEventHandler(axRenderControl1_RcMouseDragSelect);
            
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectDrag;

            env = null;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "ExportManager.html";
            }
        }


        private void toolStripCaptureScreen_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
            dlg.DefaultExt = ".bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bool b = this.axRenderControl1.ExportManager.ExportImage(dlg.FileName, 5120, 5120, false);
                if (!b)
                {
                    MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError().ToString());
                }
            }
        }

        private void toolStripExport25D_Click(object sender, EventArgs e)
        {
            if (env == null)
            {
                MessageBox.Show("请先框选出图范围");
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                type = ExportType.Export25D;
            }
            else
            {
                Export25D();
            }
        }

        private void Export25D()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files(*.IMG)|*.IMG";
            dlg.DefaultExt = ".img";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                IVector3 position = new Vector3();
                IEulerAngle angle = new EulerAngle();
                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                float meterPerPixel = (float)env.Width / 1024;
                bool b = this.axRenderControl1.ExportManager.Export25DEx(dlg.FileName, env, meterPerPixel, angle, fcGUIDs.ToArray(), false);
                if (!b)
                {
                    MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError().ToString());
                }
            }
        }

        private void toolStripExportDOM_Click(object sender, EventArgs e)
        {
            if (env == null)
            {
                MessageBox.Show("请先框选出图范围");
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                type = ExportType.ExportDOM;
            }
            else
            {
                ExportDOM();
            }
        }

        private void ExportDOM()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files(*.IMG)|*.IMG";
            dlg.DefaultExt = ".img";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                float meterPerPixel = (float)env.Width / 1024;
                bool b = this.axRenderControl1.ExportManager.ExportDOM(dlg.FileName, env, meterPerPixel);
                if (!b)
                {
                    MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError().ToString());
                }
            }
        }

        private void toolStripExportDEM_Click(object sender, EventArgs e)
        {
            if (env == null)
            {
                MessageBox.Show("请先框选出图范围");
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                type = ExportType.ExportDEM;
            }
            else
            {
                ExportDEM();
            }
        }

        private void ExportDEM()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files(*.IMG)|*.IMG";
            dlg.DefaultExt = ".img";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                float meterPerPixel = (float)env.Width / 1024;
                bool b = this.axRenderControl1.ExportManager.ExportDEM(dlg.FileName, env, meterPerPixel);
                if (!b)
                {
                    MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError().ToString());
                }
            }
        }

        private void toolStripExportOrthoImage_Click(object sender, EventArgs e)
        {
            if (env == null)
            {
                MessageBox.Show("请先框选出图范围");
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                type = ExportType.ExportOrthoImage;
            }
            else
            {
                ExportOrthoImage();
            }
        }

        private void ExportOrthoImage()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
            dlg.DefaultExt = ".bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                IPoint center = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                center.SpatialCRS = dataset.SpatialReference;
                center.SetCoords(env.Center.X, env.Center.Y, env.Center.Z, 0, 0);
                IVector3 position = new Vector3();
                IEulerAngle angle = new EulerAngle();
                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                IEnvelope newEnv = new Envelope();  //newEnv表示相机出图范围
                newEnv.MinX = env.MinX - env.Center.X;  //偏移到原点处
                newEnv.MaxX = env.MaxX - env.Center.X;
                newEnv.MinY = env.MinY - env.Center.Y;
                newEnv.MaxY = env.MaxY - env.Center.Y;
                newEnv.MinZ = -100;  //相机出图深度
                newEnv.MaxZ = 100;
                bool b = this.axRenderControl1.ExportManager.ExportOrthoImage(dlg.FileName, 1024, center, angle, newEnv, false, System.Drawing.Color.Blue);
                if (!b)
                {
                    MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError().ToString());
                }
            }
        }

        /// <summary>
        /// 清除选择区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripClearSelection_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            env = null;
            if (rpolyline != null)
                this.axRenderControl1.ObjectManager.DeleteObject(rpolyline.Guid);
        }

        void axRenderControl1_RcMouseDragSelect(IPickResultCollection PickResults, gviModKeyMask Mask)
        {
            

            IPickResultCollection prc = PickResults;
            if (prc == null)
                return;

            Hashtable fcEnvMap = new Hashtable();
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            for (int i = 0; i < prc.Count; i++)
            {
                IPickResult pr = prc.Get(i);
                if (pr.Type == gviObjectType.gviObjectFeatureLayer)
                {
                    IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                    int fid = flpr.FeatureId;
                    IFeatureLayer fl = flpr.FeatureLayer;
                    foreach (IFeatureClass fc in fcMap.Keys)
                    {
                        if (fc.Guid.Equals(fl.FeatureClassId))
                        {
                            this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Yellow);
                            
                            if (fcEnvMap.ContainsKey(fc))
                            {
                                List<int> fids = fcEnvMap[fc] as List<int>;
                                fids.Add(fid);
                                fcEnvMap.Remove(fc);
                                fcEnvMap.Add(fc, fids);
                            }
                            else
                            {
                                List<int> fids = new List<int>();
                                fids.Add(fid);
                                fcEnvMap.Add(fc, fids);

                                fcGUIDs.Add(fc.Guid);
                            }
                        }
                        
                    }
                }
            }

            // 计算Envelope
            env = null;
            foreach (IFeatureClass fc in fcEnvMap.Keys)
            {
                List<int> fids = fcEnvMap[fc] as List<int>;
                if (env == null)
                    env = fc.GetFeaturesEnvelope(fids.ToArray(), "Geometry");
                else
                    env.ExpandByEnvelope(fc.GetFeaturesEnvelope(fids.ToArray(), "Geometry"));
            }

            // 设定出图参数
            switch (type)
            {
                case ExportType.Export25D:
                    {
                        Export25D();
                    }
                    break;
                case ExportType.ExportDEM:
                    {
                        ExportDEM();
                    }
                    break;
                case ExportType.ExportDOM:
                    {
                        ExportDOM();
                    }
                    break;
                case ExportType.ExportOrthoImage:
                    {
                        ExportOrthoImage();
                    }
                    break;
            }

            // 恢复漫游模式
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        void axRenderControl1_RcPictureExportEnd(double Time, bool IsAborted)
        {
            

            MessageBox.Show("Congratulation! Done Exported!");
            processdlg.Close();
            processdlg.Dispose();
        }

        void axRenderControl1_RcPictureExporting(int Index, float Percentage)
        {          
            processdlg.progressBar1.Value = (int)(100 * Percentage);
            processdlg.label1.Text = (100 * Percentage).ToString();
        }

        void axRenderControl1_RcPictureExportBegin(int NumberOfWidth, int NumberOfHeight)
        {
            processdlg = new ProcessDialog();
            processdlg.progressBar1.Minimum = 0;
            processdlg.progressBar1.Maximum = 100;
            processdlg.ShowDialog();
        }

        /// <summary>
        /// 出沿街立面图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStreet_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            MessageBox.Show("请沿街道画一条线");
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            

            if (rpolyline != null)
                this.axRenderControl1.ObjectManager.DeleteObject(rpolyline.Guid);

            polyline = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            polyline.SpatialCRS = dataset.SpatialReference;
            rpolyline = this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, null, rootId);
            this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(rpolyline, gviGeoEditType.gviGeoEditCreator);    
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            polyline = Geometry as IPolyline;
            if (polyline.PointCount == 2)
            {
                this.axRenderControl1.ObjectEditor.FinishEdit();
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectDrag;
                this.axRenderControl1.RcObjectEditing -= new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
                

                IEulerAngle angle = this.axRenderControl1.Camera.GetAimingAngles2(polyline.GetPoint(0), polyline.GetPoint(1));
                IEulerAngle cameraAngle = new EulerAngle();
                cameraAngle.Heading = angle.Heading - 90;
                cameraAngle.Tilt = cameraAngle.Roll = 0;
                IPoint center = polyline.Midpoint;

                IEnvelope newEnv = new Envelope();  //newEnv表示相机出图范围
                newEnv.MinX = -polyline.Length / 2;  //相机出图宽度
                newEnv.MaxX = polyline.Length / 2;
                newEnv.MinY = -10;  //相机出图高度
                newEnv.MaxY = 100;
                newEnv.MinZ = -100;  //相机出图深度
                newEnv.MaxZ = 100;

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
                dlg.DefaultExt = ".bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bool b = this.axRenderControl1.ExportManager.ExportOrthoImage(dlg.FileName, 1024, center, cameraAngle, newEnv, false, System.Drawing.Color.Blue);
                    if (!b)
                    {
                        MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError().ToString());
                    }
                }
            }
        }

        private void toolStripExportPanorama_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
            dlg.DefaultExt = ".bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                IVector3 position = new Vector3();
                IEulerAngle angle = new EulerAngle();
                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                IPoint center = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                center.SpatialCRS = dataset.SpatialReference;
                center.Position = position;
                
                bool b = this.axRenderControl1.ExportManager.ExportPanorama(dlg.FileName, 1024, center, angle);
                if (!b)
                {
                    MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError().ToString());
                }
            }
        }

    }

}
