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
using Gvitech.CityMaker.Controls;

namespace CameraTour
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEulerAngle angle = new EulerAngle();
        private IVector3 position = new Vector3();

        private ICameraTour tour = null;
        private DataTable dt = null;
        private int selectPointIndex = -1;
        private ProcessDialog processdlg;

        private bool isPlaying = false;
        private System.Guid rootId = new System.Guid();

        // 线程转发
        public int MainThreadId = 0;
        private _IRenderControlEvents_RcFrameEventHandler _rcFrame;


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
                        IEnvelope env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "CameraTour.html";
            }

            // 注册播放事件
            this.axRenderControl1.RcCameraTourWaypointChanged += new _IRenderControlEvents_RcCameraTourWaypointChangedEventHandler(axRenderControl1_RcCameraTourWaypointChanged);
            this.axRenderControl1.RcCameraFlyFinished += new _IRenderControlEvents_RcCameraFlyFinishedEventHandler(axRenderControl1_RcCameraFlyFinished);
            // 注册输出视频事件
            this.axRenderControl1.RcVideoExportBegin += new _IRenderControlEvents_RcVideoExportBeginEventHandler(axRenderControl1_RcVideoExportBegin);
            this.axRenderControl1.RcVideoExporting  +=new _IRenderControlEvents_RcVideoExportingEventHandler(axRenderControl1_RcVideoExporting);
            this.axRenderControl1.RcVideoExportEnd += new _IRenderControlEvents_RcVideoExportEndEventHandler(axRenderControl1_RcVideoExportEnd);

            // 指定每帧刷新事件
            this.axRenderControl1.RcFrame += new _IRenderControlEvents_RcFrameEventHandler(axRenderControl1_RcFrame);
            _rcFrame = new _IRenderControlEvents_RcFrameEventHandler(axRenderControl1_RcFrame);
        }

        void axRenderControl1_RcFrame(int FrameIndex, double ReferencedTime)
        {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId != MainThreadId)
            {
                this.BeginInvoke(_rcFrame, new object[] { FrameIndex, ReferencedTime });
                return;
            }

            if (this.tour != null && this.isPlaying == true)
            {
                //设置Time滑动条
                decimal mathRound = Math.Round((decimal)tour.Time, 2);
                this.trackBarTime.Value = int.Parse(((double)mathRound * 100.0).ToString());
            }
        }

        #region RenderControl事件
        // 播放过程中
        void axRenderControl1_RcCameraTourWaypointChanged(int Index)
        {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId != MainThreadId)
            {
                return;
            }

            this.dataGridView1.ClearSelection();
            this.dataGridView1.Rows[Index].Selected = true;
        }

        // 播放结束
        void axRenderControl1_RcCameraFlyFinished(byte Type)
        {

            if (tour == null)
                return;

            if (tour.AutoRepeat == false)
            {
                tour.Stop();
                isPlaying = false;

                //设置Time滑动条
                this.trackBarTime.Value = this.trackBarTime.Maximum;
            }
            else
            {
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Rows[0].Selected = true;

                //设置Time滑动条
                this.trackBarTime.Value = this.trackBarTime.Minimum;
            }
        }

        // 开始输出视频
        void axRenderControl1_RcVideoExportBegin(double TotalTime)
        {
            processdlg = new ProcessDialog();
            processdlg.progressBar1.Minimum = 0;
            processdlg.progressBar1.Maximum = 100;
            processdlg.ShowDialog();          
        }

        // 输出视频过程中
        void axRenderControl1_RcVideoExporting(float Percentage)
        {
            processdlg.progressBar1.Value = Int16.Parse((100 * Percentage).ToString());
            processdlg.label1.Text = (100 * Percentage).ToString();
        }



        // 输出视频结束
        void axRenderControl1_RcVideoExportEnd(double Time, bool IsAborted)
        {
            MessageBox.Show("Congratulation! Done Exported!");
            processdlg.Close();
            processdlg.Dispose();
        }
        #endregion

        private void init()
        {
            if (tour != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(tour.Guid);
                tour = null;
            }
        }

        private void btnCreateTour_Click(object sender, EventArgs e)
        {
            init();
            tour = this.axRenderControl1.ObjectManager.CreateCameraTour(rootId);

            //初始化节点表格
            dt = new DataTable();
            DataColumn dc0 = new DataColumn("index", typeof(int));
            DataColumn dc1 = new DataColumn("x", typeof(double));
            DataColumn dc2 = new DataColumn("y", typeof(double));
            DataColumn dc3 = new DataColumn("z", typeof(double));
            DataColumn dc4 = new DataColumn("heading", typeof(double));
            DataColumn dc5 = new DataColumn("tilt", typeof(double));
            DataColumn dc6 = new DataColumn("roll", typeof(double));
            DataColumn dc7 = new DataColumn("duration", typeof(double));
            DataColumn dc8 = new DataColumn("mode", typeof(string));
            dt.Columns.AddRange(new DataColumn[] { dc0, dc1, dc2, dc3, dc4, dc5, dc6, dc7, dc8 });
            this.dataGridView1.DataSource = dt;

            //初始化节点索引
            selectPointIndex = -1;

            cbMode.SelectedIndex = 0;

            //初始化Time滑动条
            this.trackBarTime.Maximum = 0;
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            if (tour != null)
            {
                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                tour.AddWaypoint(position, angle, Convert.ToDouble(this.numDuration.Text), getModeEnum(this.cbMode.Text));

                //将值写入表格
                DataRow dr = dt.NewRow();
                dr[0] = tour.WaypointsNumber - 1;
                dr[1] = position.X;
                dr[2] = position.Y;
                dr[3] = position.Z;
                dr[4] = angle.Heading;
                dr[5] = angle.Tilt;
                dr[6] = angle.Roll;
                dr[7] = Convert.ToDouble(this.numDuration.Value);
                dr[8] = getModeEnum(this.cbMode.Text);
                dt.Rows.Add(dr);
                this.dataGridView1.Update();

                //更新Time滑动条最大值
                decimal mathRound = Math.Round((decimal)tour.TotalTime, 2);
                this.trackBarTime.Maximum = int.Parse(((double)mathRound * 100.0).ToString());
            }
        }

        private void btnDelPoint_Click(object sender, EventArgs e)
        {
            if (tour != null && selectPointIndex != -1)
            {
                tour.DeleteWaypoint(selectPointIndex);

                //更新表格
                dt.Rows.RemoveAt(selectPointIndex);
                for (int i = selectPointIndex; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dr[0] = i;
                }

                //更新Time滑动条最大值
                decimal mathRound = Math.Round((decimal)tour.TotalTime, 2);
                this.trackBarTime.Maximum = int.Parse(((double)mathRound * 100.0).ToString());
            }
        }

        private void btnReplacePoint_Click(object sender, EventArgs e)
        {
            if (tour != null && selectPointIndex != -1)
            {
                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                tour.ModifyWaypoint(selectPointIndex, position, angle, Convert.ToDouble(this.numDuration.Text), getModeEnum(this.cbMode.Text));

                //更新表格
                DataRow newDr = dt.NewRow();
                newDr[0] = selectPointIndex;
                newDr[1] = position.X;
                newDr[2] = position.Y;
                newDr[3] = position.Z;
                newDr[4] = angle.Heading;
                newDr[5] = angle.Tilt;
                newDr[6] = angle.Roll;
                newDr[7] = Convert.ToDouble(this.numDuration.Text);
                newDr[8] = getModeEnum(this.cbMode.Text);
                dt.Rows.InsertAt(newDr, selectPointIndex);
                dt.Rows.RemoveAt(selectPointIndex + 1);
                this.dataGridView1.Update();
                selectPointIndex = -1;

                //更新Time滑动条最大值
                decimal mathRound = Math.Round((decimal)tour.TotalTime, 2);
                this.trackBarTime.Maximum = int.Parse(((double)mathRound * 100.0).ToString());
            }
        }

        private void cbAutoRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (tour != null)
            {
                tour.AutoRepeat = this.cbAutoRepeat.Checked;
            }
        }
        
        /// <summary>
        /// 从指定节点处开始播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayTourFromIndex_Click(object sender, EventArgs e)
        {
            if (selectPointIndex == -1)
                selectPointIndex = 0;

            if (tour != null)
            {
                tour.Index = selectPointIndex;
                tour.Play();
                isPlaying = true;
            }
        }

        /// <summary>
        /// 从指定时刻开始播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayTourFromTime_Click(object sender, EventArgs e)
        {
            if (tour != null)
            {
                tour.Time = this.trackBarTime.Value / 100.0;
                tour.Play();
                isPlaying = true;
            }
        }

        private void btnPauseTour_Click(object sender, EventArgs e)
        {
            if (tour != null)
            {
                tour.Pause();
                isPlaying = false;
            }
        }

        private void btnStopTour_Click(object sender, EventArgs e)
        {
            if (tour != null)
            {
                tour.Stop();
                isPlaying = false;
            }
        }

        private void btnImportFromXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XML文件|*.xml";
            if(System.IO.Directory.Exists(strMediaPath))
            {
                od.InitialDirectory = strMediaPath + @"\xml";
            }
            od.RestoreDirectory = true;
            if (od.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(od.FileName))
                {
                    StreamReader sr = new StreamReader(od.FileName);
                    string xmlstring = sr.ReadToEnd();
                    sr.Close();
                    tour = null;
                    tour = this.axRenderControl1.ObjectManager.CreateCameraTour(rootId);
                    tour.FromXml(xmlstring);

                    if (tour.WaypointsNumber > 0)
                    {
                        //初始化节点表格
                        dt = new DataTable();
                        DataColumn dc0 = new DataColumn("index", typeof(int));
                        DataColumn dc1 = new DataColumn("x", typeof(double));
                        DataColumn dc2 = new DataColumn("y", typeof(double));
                        DataColumn dc3 = new DataColumn("z", typeof(double));
                        DataColumn dc4 = new DataColumn("heading", typeof(double));
                        DataColumn dc5 = new DataColumn("tilt", typeof(double));
                        DataColumn dc6 = new DataColumn("roll", typeof(double));
                        DataColumn dc7 = new DataColumn("duration", typeof(double));
                        DataColumn dc8 = new DataColumn("mode", typeof(string));
                        dt.Columns.AddRange(new DataColumn[] { dc0, dc1, dc2, dc3, dc4, dc5, dc6, dc7, dc8 });
                        this.dataGridView1.DataSource = dt;

                        for (int i = 0; i < tour.WaypointsNumber; i++)
                        {
                            double duration;
                            gviCameraTourMode mode;
                            tour.GetWaypoint(i, out position, out angle, out duration, out mode);

                            //将值写入表格
                            DataRow dr = dt.NewRow();
                            dr[0] = i;
                            dr[1] = position.X;
                            dr[2] = position.Y;
                            dr[3] = position.Z;
                            dr[4] = angle.Heading;
                            dr[5] = angle.Tilt;
                            dr[6] = angle.Roll;
                            dr[7] = duration;
                            dr[8] = getModeString(mode);
                            dt.Rows.Add(dr);
                        }
                        this.dataGridView1.Update();

                        //更新Time滑动条最大值
                        decimal mathRound = Math.Round((decimal)tour.TotalTime, 2);
                        this.trackBarTime.Maximum = int.Parse(((double)mathRound * 100.0).ToString());
                    }
                }
            }
        }

        private void btnExportAsXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.AddExtension = true;
            sd.DefaultExt = "xml";
            sd.Filter = "XML文件|*.xml";
            if(System.IO.Directory.Exists(strMediaPath))
            {
                sd.InitialDirectory = strMediaPath + @"\xml";
            }
            sd.RestoreDirectory = true;
            if (sd.ShowDialog() == DialogResult.OK)
            {
                String final = sd.FileName;
                if (sd.FileName.LastIndexOf(".xml") == -1)
                    final = String.Format("{0}.xml", sd.FileName);

                String xmlstring = null;
                if (tour != null)
                    xmlstring = tour.AsXml();
                System.IO.File.WriteAllText(final, xmlstring);
                MessageBox.Show("导出成功");
            }
        }

        private void btnExportAsVideo_Click(object sender, EventArgs e)
        {
            if (tour != null && tour.WaypointsNumber > 2)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.AddExtension = true;
                sd.DefaultExt = "avi";
                sd.Filter = "AVI文件|*.avi";
                if(System.IO.Directory.Exists(strMediaPath))
                {
                    sd.InitialDirectory = strMediaPath;
                }
                sd.RestoreDirectory = true;
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    String final = sd.FileName;
                    if (sd.FileName.LastIndexOf(".avi") == -1)
                        final = String.Format("{0}.avi", sd.FileName);

                    bool b = tour.ExportVideo(final, 25);
                    if (!b)
                        MessageBox.Show("输出视频失败!");
                }
            }
            else
                MessageBox.Show("当前动画导航为空或者节点数小于2，不支持视频输出!");
        }

        private void btnExportAsFramePictures_Click(object sender, EventArgs e)
        {
            if (tour != null && tour.WaypointsNumber > 2)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.AddExtension = true;
                sd.DefaultExt = "bmp";
                sd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
                if(System.IO.Directory.Exists(strMediaPath))
                {
                    sd.InitialDirectory = strMediaPath;
                }
                sd.RestoreDirectory = true;
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    String final = sd.FileName;
                    if (sd.FileName.LastIndexOf(".bmp") == -1 && sd.FileName.LastIndexOf(".png") == -1 && sd.FileName.LastIndexOf(".jpg") == -1
                            && sd.FileName.LastIndexOf(".BMP") == -1 && sd.FileName.LastIndexOf(".PNG") == -1 && sd.FileName.LastIndexOf(".JPG") == -1)
                        final = String.Format("{0}.bmp", sd.FileName);

                    bool b = tour.ExportVideo(final, 25);
                    if (!b)
                        MessageBox.Show("输出序列帧失败!");
                }
            }
            else
                MessageBox.Show("当前动画导航为空或者节点数小于2，不支持输出序列帧!");
        }

        #region Datagrid事件
        /// <summary>
        /// 单击行头，设置index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selectPointIndex = e.RowIndex;
        }

        /// <summary>
        /// 双击行头，相机飞入节点所在位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tour != null)
            {
                selectPointIndex = e.RowIndex;
                double speed;
                gviCameraTourMode mode;

                int pointIndex = Convert.ToInt32(dt.Rows[selectPointIndex][0]);
                tour.GetWaypoint(pointIndex, out position, out angle, out speed, out mode);

                this.axRenderControl1.Camera.FlyTime = 0;
                this.axRenderControl1.Camera.SetCamera(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);
            }
        }
        #endregion

        #region 自定义方法
        string getModeString(gviCameraTourMode mode)
        {
            switch (mode)
            {
                case gviCameraTourMode.gviCameraTourBounce:
                    return "Bounce";
                case gviCameraTourMode.gviCameraTourSmooth:
                    return "Smooth";
                case gviCameraTourMode.gviCameraTourLinear:
                    return "Linear";
            }
            return "";
        }

        gviCameraTourMode getModeEnum(string modestring)
        {
            switch (modestring)
            {
                case "Bounce":
                    return gviCameraTourMode.gviCameraTourBounce;
                case "Smooth":
                    return gviCameraTourMode.gviCameraTourSmooth;
                case "Linear":
                    return gviCameraTourMode.gviCameraTourLinear;
            }
            return gviCameraTourMode.gviCameraTourSmooth;
        }
        #endregion

        private void trackBarTime_ValueChanged(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.trackBarTime, (this.trackBarTime.Value / 100.0).ToString());
            this.labelTime.Text = (this.trackBarTime.Value / 100.0).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.axRenderControl1.OnFrameInvoke = null; //解除回调绑定
            Application.Exit();  //关闭应用程序
        }

        private void btnExportAsPanoramaFramePictures_Click(object sender, EventArgs e)
        {
            if (tour != null && tour.WaypointsNumber > 2)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.AddExtension = true;
                sd.DefaultExt = "bmp";
                sd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
                if(System.IO.Directory.Exists(strMediaPath))
                {
                    sd.InitialDirectory = strMediaPath;
                }
                sd.RestoreDirectory = true;
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    String final = sd.FileName;
                    if (sd.FileName.LastIndexOf(".bmp") == -1 && sd.FileName.LastIndexOf(".png") == -1 && sd.FileName.LastIndexOf(".jpg") == -1
                            && sd.FileName.LastIndexOf(".BMP") == -1 && sd.FileName.LastIndexOf(".PNG") == -1 && sd.FileName.LastIndexOf(".JPG") == -1)
                        final = String.Format("{0}.bmp", sd.FileName);

                    bool b = tour.ExportPanoramaFrameSequence(final, 256, 25);
                    if (!b)
                        MessageBox.Show("输出全景图序列帧失败!");
                }
            }
            else
                MessageBox.Show("当前动画导航为空或者节点数小于2，不支持输出全景图序列帧!");
        }

    }
}
