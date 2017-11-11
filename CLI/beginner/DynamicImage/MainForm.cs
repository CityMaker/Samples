using System;
using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.FdeGeometry;

namespace DynamicImage
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        TrackBar frameIntervalTrack;  //序列帧播放间隔，单位是毫秒，默认1000 
        ToolStripControlHost host;

        IFeatureDataSet dataset = null;  //需要更新贴图的数据集
        IImage imgStatic = null;  //默认的静态贴图
        IResourceManager resManager = null;
        IResourceFactory resFactory = null;
        string imgpath = string.Empty;

        private System.Guid rootId = new System.Guid();

        public MainForm()
        {
            InitializeComponent();

            frameIntervalTrack = new TrackBar();
            frameIntervalTrack.Minimum = 1;
            frameIntervalTrack.Maximum = 1500;
            frameIntervalTrack.TickFrequency = 100;
            frameIntervalTrack.LargeChange = 100;
            frameIntervalTrack.SmallChange = 10;
            frameIntervalTrack.Value = 1000;
            frameIntervalTrack.Size = new System.Drawing.Size(200, 45);
            frameIntervalTrack.ValueChanged += new EventHandler(track_ValueChanged);
            host = new ToolStripControlHost(frameIntervalTrack);
            toolStrip1.Items.Add(host);
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

            // 加载FDB场景
            //try
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
            //catch (COMException ex)
            //{
            //    System.Diagnostics.Trace.WriteLine(ex.Message);
            //    return;
            //}

            // CreateFeautureLayer
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    if (!geoName.Equals("Geometry"))
                        continue;

                    this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, null, null, rootId);
                }
            }

            IPoint position = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            position.SetCoords(15371.768457028507, 35951.723472296966, 20.9338220668297, 0, 0);
            position.SpatialCRS = dataset.SpatialReference;
            IEulerAngle angle = new EulerAngle();
            angle.Set(11.76, -19.26, 0);
            this.axRenderControl1.Camera.SetCamera2(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);

            //获取默认的静态贴图
            //try
            {
                resManager = dataset as IResourceManager;
                imgStatic = resManager.GetImage("paizi6");
            }
            //catch (System.Exception ex)
            //{
            //    if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
            //        MessageBox.Show("需要标准runtime授权");
            //    else
            //        MessageBox.Show(ex.Message);
            //}

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DynamicImage.html";
            }
        }



        void track_ValueChanged(object sender, EventArgs e)
        {
            //try
            {
                this.toolTip1.SetToolTip(this.frameIntervalTrack, frameIntervalTrack.Value.ToString());
                IImage img = resManager.GetImage("paizi6");
                if (img.ImageType == gviImageType.gviImageDynamic)
                {
                    img.FrameInterval = frameIntervalTrack.Value;
                    resManager.UpdateImage("paizi6", img);
                    this.axRenderControl1.RefreshImage(dataset, "paizi6");
                }
            }
            //catch (System.Exception ex)
            //{
            //    if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
            //        MessageBox.Show("需要标准runtime授权");
            //    else
            //        MessageBox.Show(ex.Message);
            //}
        }

        private void toolStripDynamicImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            {
                if (resFactory == null)
                    resFactory = new ResourceFactory();

                switch (this.toolStripDynamicImage.SelectedIndex)
                {
                    case 0:
                        {
                            imgpath = string.Empty;
                        }
                        break;
                    case 1:
                        {
                            imgpath = (strMediaPath + @"\frameImage\NiHongDeng");
                        }
                        break;
                    case 2:
                        {
                            imgpath = (strMediaPath + @"\frameImage\Gvitech");
                        }
                        break;
                }

                if (imgpath.Equals(string.Empty))
                {
                    resManager.UpdateImage("paizi6", imgStatic);
                    this.axRenderControl1.RefreshImage(dataset, "paizi6");
                }
                else
                {
                    IImage img = resFactory.CreateImageFromFile(imgpath);
                    img.FrameInterval = frameIntervalTrack.Value;
                    this.Text = "正在转换中，请耐心等待...";
                    img.ConvertFormat(gviImageFormat.gviImageDDS);
                    resManager.UpdateImage("paizi6", img);
                    this.axRenderControl1.RefreshImage(dataset, "paizi6");
                }
                this.Text = "转换完成";
            }
            //catch (System.Exception ex)
            //{
            //    if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
            //        MessageBox.Show("需要标准runtime授权");
            //    else
            //        MessageBox.Show(ex.Message);

            //    this.Text = "转换失败";
            //}
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            resManager.UpdateImage("paizi6", imgStatic);
        } 
    }
}
