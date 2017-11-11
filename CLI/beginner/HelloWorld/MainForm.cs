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
using Gvitech.CityMaker.FdeUndoRedo;
using Gvitech.CityMaker.Controls;

namespace HelloWorld
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private ProcessDialog processdlg;
        private IEnvelope env;//加载数据时，初始化的矩形范围

        // 全局COM对象
        private ICameraTour tour = null;

        private System.Guid rootId = Guid.Empty;

        // 线程转发
        public int MainThreadId = 0;
        private _IRenderControlEvents_RcCameraUndoRedoStatusChangedEventHandler _rcCameraUndoRedo;
        
        
        

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {           
            init();
        }

        void init()
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
            
            // 注册相机“返回”和“前进”事件
            this.axRenderControl1.RcCameraUndoRedoStatusChanged += new _IRenderControlEvents_RcCameraUndoRedoStatusChangedEventHandler(axRenderControl1_RcCameraUndoRedoStatusChanged);
            _rcCameraUndoRedo = new _IRenderControlEvents_RcCameraUndoRedoStatusChangedEventHandler(axRenderControl1_RcCameraUndoRedoStatusChanged);

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

            // 加载动画导航
            BindingSource bindingSource1 = new BindingSource();
            {
                string tmpXMLPath = (strMediaPath + @"\xml\CameraTour_1.xml");
                tour = this.axRenderControl1.ObjectManager.CreateCameraTour(rootId);
                if (File.Exists(tmpXMLPath))
                {
                    StreamReader sr = new StreamReader(tmpXMLPath);
                    string xmlstring = "";
                    string line = sr.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        xmlstring += line;
                        line = sr.ReadLine();
                    }
                    sr.Close();
                    tour.FromXml(xmlstring);
                }
                bindingSource1.Add(new Knight("CameraTour_1", tour));
            }
            {
                string tmpXMLPath = (strMediaPath + @"\xml\CameraTour_2.xml");
                tour = this.axRenderControl1.ObjectManager.CreateCameraTour(rootId);
                if (File.Exists(tmpXMLPath))
                {
                    StreamReader sr = new StreamReader(tmpXMLPath);
                    string xmlstring = "";
                    string line = sr.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        xmlstring += line;
                        line = sr.ReadLine();
                    }
                    sr.Close();
                    tour.FromXml(xmlstring);
                }
                bindingSource1.Add(new Knight("CameraTour_2", tour));
            }

            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Columns[1].Visible = false;
            this.btnPause.Enabled = false;
            this.btnStop.Enabled = false;
            this.toolStripComboBoxWeather.SelectedIndex = 0;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "HelloWorld.html";
            }
        } 

        #region 更换天空盒事件
        // 更换天空盒
        private void skyboxListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.skyboxListView.SelectedItems.Count > 0)
            {
                string tmpSkyboxPath = strMediaPath + @"\skybox";
                ISkyBox skybox = this.axRenderControl1.ObjectManager.GetSkyBox(0);
                switch (skyboxListView.SelectedItems[0].Text)
                {                        
                    case "无":                        
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\00_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\00_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\00_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\00_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\00_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\00_UP.jpg");
                        break;
                    case "金色晨曦":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\1_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\1_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\1_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\1_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\1_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\1_UP.jpg");   
                        break;
                    case "光暗之手":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\2_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\2_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\2_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\2_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\2_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\2_UP.jpg");
                        break;
                    case "天马行空":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\04_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\04_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\04_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\04_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\04_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\04_UP.jpg");
                        break;
                    case "飘絮人间":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\7_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\7_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\7_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\7_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\7_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\7_UP.jpg");
                        break;
                    case "七彩紫罗":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\9_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\9_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\9_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\9_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\9_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\9_UP.jpg");
                        break;
                    case "云中之触":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\10_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\10_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\10_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\10_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\10_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\10_UP.jpg");
                        break;
                    case "鲲鹏万里":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\11_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\11_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\11_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\11_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\11_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\11_UP.jpg");
                        break;
                    case "血色苍穹":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\12_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\12_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\12_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\12_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\12_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\12_UP.jpg");
                        break;
                    case "白云旋天":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\13_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\13_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\13_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\13_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\13_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\13_UP.jpg");
                        break;
                    case "长空破日":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\22_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\22_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\22_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\22_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\22_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\22_UP.jpg");
                        break;
                    case "霞光掩影":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\44_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\44_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\44_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\44_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\44_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\44_UP.jpg");
                        break;
                    case "混沌沧海":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\99_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\99_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\99_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\99_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\99_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\99_UP.jpg");
                        break;
                    case "梦境之末":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\100_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\100_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\100_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\100_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\100_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\100_UP.jpg");
                        break;
                    case "玄浑宇宙":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\120_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\120_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\120_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\120_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\120_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\120_UP.jpg");
                        break;
                    case "月神之眼":
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\130_BK.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\130_DN.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\130_FR.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\130_LF.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\130_RT.jpg");
                        skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\130_UP.jpg");
                        break;
                }
            }
        }       

        #endregion

        #region 播放动画导航控制
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tour = (ICameraTour)this.dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            if (tour != null && tour.WaypointsNumber > 0)
            {
                tour.Play();
                this.btnPause.Enabled = true;
                this.btnStop.Enabled = true;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            tour.Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tour.Stop();
            this.btnPause.Enabled = false;
            this.btnStop.Enabled = false;
        }

        #endregion                

        #region 菜单按钮：全屏、出图
        private void toolStripFullScreen_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FullScreen = !this.axRenderControl1.FullScreen;
        }

        private void toolStripCaptureScreen_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
            dlg.DefaultExt = ".bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bool higquality = false;
                bool b = this.axRenderControl1.ExportManager.ExportImage(dlg.FileName, 1024, 1024 ,higquality);
                if (!b)
                {
                    MessageBox.Show("Capture Screen Failed, Please check it.");
                }
            }
        }

        void axRenderControl1_RcPictureExportEnd(double Time, bool IsAborted)
        {
            

            MessageBox.Show("Congratulation! Done Exported!");
            processdlg.Close();
            processdlg.Dispose();
        }

        void axRenderControl1_RcPictureExporting(int Index, float Percentage)
        {
            

            processdlg.progressBar1.Value = Int16.Parse((100 * Percentage).ToString());
            processdlg.label1.Text = (100 * Percentage).ToString();
        }

        void axRenderControl1_RcPictureExportBegin(int NumberOfWidth, int NumberOfHeight)
        {
            

            processdlg = new ProcessDialog();
            processdlg.progressBar1.Minimum = 0;
            processdlg.progressBar1.Maximum = 100;
            processdlg.Show();
        }
        #endregion

        // 设置天气
        private void toolStripComboBoxWeather_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(toolStripComboBoxWeather.Text)
            {
                case "晴天":
                    this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherSunShine;
                    break;
                case "小雨":
                    this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherLightRain;
                    break;
                case "中雨":
                    this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherModerateRain;
                    break;
                case "大雨":
                    this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherHeavyRain;
                    break;
                case "小雪":
                    this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherLightSnow;
                    break;
                case "中雪":
                    this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherModerateSnow;
                    break;
                case "大雪":
                    this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherHeavySnow;
                    break;
            }
        }

        /// <summary>
        /// 关于菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripAbout_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 开启关闭雾效果菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripFog_Click(object sender, EventArgs e)
        {
            bool fogCheck = (sender as ToolStripMenuItem).Checked;

            ISkyBox skybox = this.axRenderControl1.ObjectManager.GetSkyBox(0);
            if (!fogCheck)
            {
                skybox.FogStartDistance = 0;
                skybox.FogEndDistance = 500;
                skybox.FogMode = gviFogMode.gviFogLinear;
                IEulerAngle angle = new EulerAngle();
                angle.Set(0, -20, 0);
                this.axRenderControl1.Camera.LookAt(env.Center, 500, angle);
                toolStripFog.Text = "关闭雾效";
            }
            else
            {
                skybox.FogMode = gviFogMode.gviFogNone;
                toolStripFog.Text = "开启雾效";
            }
            (sender as ToolStripMenuItem).Checked = !fogCheck;
        }

        #region 相机的“返回”和“前进”
        private void 上一视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.Camera.Undo();
        }

        private void 下一视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.Camera.Redo();
        }

        
        void axRenderControl1_RcCameraUndoRedoStatusChanged()
        {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId != MainThreadId)
            {
                this.BeginInvoke(_rcCameraUndoRedo);
                return;
            }

            if (this.axRenderControl1.Camera.CanUndo)
                this.上一视图ToolStripMenuItem.Enabled = true;
            else
                this.上一视图ToolStripMenuItem.Enabled = false;
            if (this.axRenderControl1.Camera.CanRedo)
                this.下一视图ToolStripMenuItem.Enabled = true;
            else
                this.下一视图ToolStripMenuItem.Enabled = false;
        }
        #endregion
    }

    #region "business object"
    public class Knight
    {
        private string tourName;
        private object tourObject;

        public Knight(string name, object tour)
        {
            tourName = name;
            tourObject = tour;
        }

        public string TourName
        {
            get
            {
                return tourName;
            }

            set
            {
                tourName = value;
            }
        }

        public object TourObject
        {
            get
            {
                return tourObject;
            }
            set
            {
                tourObject = value;
            }
        }
    }
    #endregion
}
