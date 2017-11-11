// Copyright 2012 CityMaker SDK
// 
// All rights reserved under the copyright laws of the China
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See Sample at <your CityMaker install location>/CityMaker SDK/Samples.
// 
//author	yuanying
//date	2013/05/28
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
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;
using Gvitech;
using System.Drawing;
using Gvitech.CityMaker.Controls;

namespace ViewshedAnalysis
{
    public partial class MainForm : Form
    {
        
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private Hashtable layerMap = new Hashtable();  //FC+geoName, featurelayer

        private int mouseClicks = 0;

        private IGeometryFactory geoFactory = new GeometryFactory();
        private IPoint fde_point1 = null;
        private IPoint fde_point2 = null; 
        private IGeometry geoRegion = null;
        private IGeometry geoInFC = null;

        private IFdeCursor cursor = null;
        private IRowBuffer row = null;

        private IFeatureClass buildingFC = null;
        private IFeatureLayer buildingFL = null;

        private System.Guid rootId = new System.Guid();

        // 线程转发
        public int MainThreadId = 0;
        

        /// <summary>
        /// 初始化
        /// </summary>
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
            bool hasfly = false;
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    if (geoName.Equals("Geometry"))
                    {
                        IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                  fc, geoName, null, null, rootId);

                        if (fc.Name.Equals("Building"))
                        {
                            buildingFL = featureLayer;
                            buildingFC = fc;
                        }

                        if (!hasfly)
                        {
                            IFieldInfoCollection fieldinfos = fc.GetFields();
                            IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                            IEnvelope env = geometryDef.Envelope;
                            if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                                env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                                continue;
                            IEulerAngle angle = new EulerAngle();
                            angle.Set(0, -20, 0);
                            this.axRenderControl1.Camera.LookAt(env.Center, 500, angle);
                        }
                        hasfly = true;
                    }
                }
            }
            #endregion 加载FDB场景
            
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "ShadowAnalysis.html";
            }
        }

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

            init();

            this.colorBox.Text = axRenderControl1.SunConfig.ShadowColor.ToArgb().ToString("X");
            this.trackBarOpacity.Value = axRenderControl1.SunConfig.ShadowColor.A;
            this.comboBoxSunMode.SelectedIndex = 0;
            this.numHeading.Value = 0;
            this.numTilt.Value = 0;
            this.numYear.Value = (decimal)DateTime.UtcNow.Year;
            this.numMonth.Value = (decimal)DateTime.UtcNow.Month;
            this.numDay.Value = (decimal)DateTime.UtcNow.Day;
            this.numHour.Value = (decimal)DateTime.UtcNow.Hour;
            this.numMinute.Value = (decimal)DateTime.UtcNow.Minute;
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.colorBox.Text = this.colorDialog1.Color.ToArgb().ToString("X");
                axRenderControl1.SunConfig.ShadowColor = this.colorDialog1.Color;
            }
        }

        /// <summary>
        /// 设置颜色透明度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarOpacity_Scroll(object sender, EventArgs e)
        {
            int a = this.trackBarOpacity.Value;
            Color oldColor = axRenderControl1.SunConfig.ShadowColor;
            Color newColor = Color.FromArgb(a, oldColor.R, oldColor.G, oldColor.B);
            axRenderControl1.SunConfig.ShadowColor = newColor;
            if (axRenderControl1.SunConfig.ShadowColor == Color.Empty)
                this.colorBox.Text = "00000000";
            else
                this.colorBox.Text = axRenderControl1.SunConfig.ShadowColor.ToArgb().ToString("X");
        }

        private void comboBoxSunMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSunMode.SelectedIndex == 0)
            {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
            }
            else if (this.comboBoxSunMode.SelectedIndex == 1)
            {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = true;
                this.btnPlay.Enabled = false;
                this.btnStop.Enabled = false;
                this.btnNow.Enabled = false;
            }
            else
            {
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = false;
            }
        }

        /// <summary>
        /// 开始分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartAnalyse_Click(object sender, EventArgs e)
        {
            this.label7.Text = "请鼠标点击选择观察点";
            this.btnStartAnalyse.Enabled = false;           

            axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(g_RcMouseClickSelect);
            

            axRenderControl1.HighlightHelper.VisibleMask = 1;  //默认为不可见
            axRenderControl1.VisualAnalysis.ClearOccluders();  //清除遮挡物

            axRenderControl1.FeatureManager.UnhighlightAll();
        }

        /// <summary>
        /// 停止分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopAnalyse_Click(object sender, EventArgs e)
        {
            axRenderControl1.VisualAnalysis.StopAnalyse();
        }

        /// <summary>
        /// 鼠标点击 拾取分析点
        /// </summary>
        /// <param name="PickResult"></param>
        /// <param name="IntersectPoint"></param>
        /// <param name="Mask"></param>
        /// <param name="EventSender"></param>
        void g_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (IntersectPoint == null)
                return;
            if (EventSender.Equals(gviMouseSelectMode.gviMouseSelectClick))
            {
                mouseClicks++;
                if (mouseClicks % 2 == 1)
                {
                    this.label7.Text = "请鼠标点击选择目标点";
                    fde_point1 = IntersectPoint;
                    axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
                }
                else
                {
                    this.label7.Text = "选择结束";
                    this.btnStartAnalyse.Enabled = true;

                    axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                    axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                    axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                    axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(g_RcMouseClickSelect);
                    

                    switch (this.comboBoxSunMode.SelectedIndex)
                    {
                        case 0:
                            {
                                
                            }
                            break;
                        case 1:
                            {
                                axRenderControl1.SunConfig.SunCalculateMode = gviSunCalculateMode.gviSunModeAccordingToGMT;
                                DateTime time = new DateTime(int.Parse(this.numYear.Value.ToString()),
                                    int.Parse(this.numMonth.Value.ToString()),
                                    int.Parse(this.numDay.Value.ToString()), 
                                    int.Parse(this.numHour.Value.ToString()),
                                    int.Parse(this.numMinute.Value.ToString()),
                                    0);
                                axRenderControl1.SunConfig.SetGMT(time);
                                this.btnPlay.Enabled = true;
                                this.btnStop.Enabled = true;
                                this.btnNow.Enabled = true;
                            }
                            break;
                        case 2:
                            {
                                axRenderControl1.SunConfig.SunCalculateMode = gviSunCalculateMode.gviSunModeUserDefined;
                                IEulerAngle angle = new EulerAngle();
                                angle.Set(double.Parse(numHeading.Value.ToString()), double.Parse(numTilt.Value.ToString()), 0);
                                axRenderControl1.SunConfig.SetSunEuler(angle);
                            }
                            break;
                    }

                    geoRegion = axRenderControl1.HighlightHelper.GetRegion();
                    //开始空间查询
                    try
                    {
                        this.label7.Text = "正在做空间查询";
                        ISpatialFilter sfilter = new SpatialFilter();
                        sfilter.Geometry = geoRegion;
                        sfilter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                        sfilter.GeometryField = "footprint";
                        cursor = buildingFC.Search(sfilter, false);
                        row = null;
                        while ((row = cursor.NextRow()) != null)
                        {
                            int geopos = row.FieldIndex("Geometry");
                            if (geopos > -1)
                                geoInFC = row.GetValue(geopos) as IGeometry;
                            if (geoInFC != null)
                            {
                                //AddOccluder
                                axRenderControl1.VisualAnalysis.AddOccluder(buildingFL, geoInFC);

                                //highlight occluder
                                int fidpos = row.FieldIndex("oid");
                                if (fidpos > -1)
                                    axRenderControl1.FeatureManager.HighlightFeature(buildingFC, int.Parse(row.GetValue(fidpos).ToString()), Color.Red);
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }
                    finally
                    {
                        if (cursor != null)
                        {
                            //Marshal.ReleaseComObject(cursor);
                            cursor = null;
                        }
                    }

                    this.label7.Text = "正在日照分析中...";
                    //StartAnalyse
                    axRenderControl1.VisualAnalysis.StartShadowAnalyse();
                    axRenderControl1.HighlightHelper.VisibleMask = 0;
                    this.label7.Text = "分析结束";
                }
            }
            else if (EventSender.Equals(gviMouseSelectMode.gviMouseSelectMove))
            {
                fde_point2 = IntersectPoint;
                ILine fde_line = geoFactory.CreateGeometry(gviGeometryType.gviGeometryLine, gviVertexAttribute.gviVertexAttributeZ) as ILine;
                fde_line.StartPoint = fde_point1;
                fde_line.EndPoint = fde_point2;
                axRenderControl1.HighlightHelper.SetCircleRegion(fde_point1, fde_line.Length);
            }
        }

        #region GMT
        private void numHour_ValueChanged(object sender, EventArgs e)
        {
            DateTime time = new DateTime(int.Parse(this.numYear.Value.ToString()),
                                     int.Parse(this.numMonth.Value.ToString()),
                                     int.Parse(this.numDay.Value.ToString()),
                                     int.Parse(this.numHour.Value.ToString()),
                                     int.Parse(this.numMinute.Value.ToString()),
                                     0);
            axRenderControl1.SunConfig.SetGMT(time);
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
            this.numYear.Value = (decimal)DateTime.UtcNow.Year;
            this.numMonth.Value = (decimal)DateTime.UtcNow.Month;
            this.numDay.Value = (decimal)DateTime.UtcNow.Day;
            this.numHour.Value = (decimal)DateTime.UtcNow.Hour;
            this.numMinute.Value = (decimal)DateTime.UtcNow.Minute;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.numMinute.Value == 59)
            {
                this.numMinute.Value = 0;
                if (this.numHour.Value == 23)
                    this.numHour.Value = 0;
                else
                    this.numHour.Value = int.Parse(this.numHour.Value.ToString()) + 1;
            }
            else
                this.numMinute.Value++;
        }
        #endregion


        private void numSunDirection_ValueChanged(object sender, EventArgs e)
        {
            if (mouseClicks > 0)
            {
                IEulerAngle angle = new EulerAngle();
                angle.Set(double.Parse(numHeading.Value.ToString()), double.Parse(numTilt.Value.ToString()), 0);
                axRenderControl1.SunConfig.SetSunEuler(angle);
            }
        }

    }
}
