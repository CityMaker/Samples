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

namespace TemporalOldSummerPalace
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEulerAngle angle = new EulerAngle();
        private IVector3 position = new Vector3();
        private ArrayList layers = new ArrayList();  //要开启时态的layer
        private ArrayList timelist = new ArrayList();
        private IOverlayLabel label = null;

        private System.Guid rootId = new System.Guid();

        public MainForm()
        {
            InitializeComponent();
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
                string tmpFDBPath = (strMediaPath + @"\TemporalOldSummerPalace.FDB");
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

                bool hasSetTime = false;
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

                    if (!fc.Name.Equals("水系"))
                    {
                        ///* 准备数据时打开，执行完后注释掉 
                        // 开启时态
                        //if (!fc.HasTemporal())
                        //{
                        //    fc.LockType = gviLockType.gviLockExclusiveSchema;
                        //    DateTime defaultBirthDatetime = new DateTime(2010, 1, 1);
                        //    fc.EnableTemporal(defaultBirthDatetime, "StartDate", "EndDate");
                        //    fc.LockType = gviLockType.gviLockSharedSchema;
                        //}

                        if (!hasSetTime)
                        {
                            ITemporalManager tm = fc.TemporalManager;
                            DateTime[] times = tm.GetKeyDatetimes();
                            for (int tt = 0; tt < times.Length; tt++)
                            {
                                double keytime = times[tt].ToOADate();
                                timelist.Add(keytime);
                            }

                            if (timelist.Count > 3)
                            {
                                this.trackBarTime.Minimum = (int)((double)timelist[0]);
                                this.trackBarTime.Maximum = (int)((double)timelist[timelist.Count - 1] - 1);
                            }
                            // 设置时间轴
                            this.trackBarTime.Value = this.trackBarTime.Minimum;

                            // 创建OverlayLabel
                            label = this.axRenderControl1.ObjectManager.CreateOverlayLabel(rootId);
                            label.Text = "乾隆初期";
                            label.SetX(0, 0.5f, 0);
                            label.SetY(0, 0, 0.5f);
                            label.SetWidth(0, 1, 0);
                            label.SetHeight(0, 0, 1);
                            label.Alignment = gviPivotAlignment.gviPivotAlignTopLeft;
                            TextAttribute att = new TextAttribute();
                            att.Font = "幼圆";
                            att.TextColor = System.Drawing.Color.Red;
                            att.TextSize = 25;
                            label.TextStyle = att;

                            hasSetTime = true;
                        }
                    }
                } //end of fc                
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
                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                fc, geoName, null, null, rootId);

                    if (!fc.Name.Equals("水系") && geoName.Equals("Geometry"))
                    {
                        featureLayer.EnableTemporal = true;
                        layers.Add(featureLayer);

                        //设置curLayer的时刻
                        if (featureLayer != null)
                        {
                            DateTime d = DateTime.FromOADate((double)this.trackBarTime.Value);
                            featureLayer.Time = d;
                        }
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
                        angle.Set(0, -40, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 100, angle);
                    }
                    hasfly = true;
                }
            }

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "TemporalOldSummerPalace.html";
            }
        }

        private void trackBarTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime d = DateTime.FromOADate((double)this.trackBarTime.Value);
            DateTime qianlongZhongwanqi = new DateTime(2010, 1, 2, 0, 0, 0);
            DateTime daoguangShiqi = new DateTime(2010, 1, 3, 0, 0, 0);
            DateTime xianfengShiqi = new DateTime(2010, 1, 4, 0, 0, 0);
            DateTime yizhiShiqi = new DateTime(2010, 1, 5, 0, 0, 0);
            if (d.CompareTo(qianlongZhongwanqi) < 0)
            {
                this.toolTip1.SetToolTip(this.trackBarTime, "乾隆初期");                          
                label.Text = "乾隆初期";
            }
            else if (d.CompareTo(qianlongZhongwanqi) >= 0 && d.CompareTo(daoguangShiqi) < 0)
            {
                this.toolTip1.SetToolTip(this.trackBarTime, "乾隆中晚期");                          
                label.Text = "乾隆中晚期";
            }
            else if (d.CompareTo(daoguangShiqi) >= 0 && d.CompareTo(xianfengShiqi) < 0)
            {
                this.toolTip1.SetToolTip(this.trackBarTime, "道光时期");                          
                label.Text = "道光时期";
            }
            else if (d.CompareTo(xianfengShiqi) >= 0 && d.CompareTo(yizhiShiqi) < 0)
            {
                this.toolTip1.SetToolTip(this.trackBarTime, "咸丰时期");                          
                label.Text = "咸丰时期";
            }
            else if (d.CompareTo(yizhiShiqi) >= 0)
            {
                this.toolTip1.SetToolTip(this.trackBarTime, "遗址时期");                          
                label.Text = "遗址时期";
            }

            foreach (IFeatureLayer curLayer in layers)
            {
                if (curLayer != null)
                    curLayer.Time = d;
            }
        }
    }
}
