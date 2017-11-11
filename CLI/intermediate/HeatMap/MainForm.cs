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

namespace HeatMap
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEulerAngle angle = new EulerAngle();
        private IVector3 position = new Vector3();
        List<IFeatureLayer> flayerList = new List<IFeatureLayer>();

        List<DateTime> keyDatetimesList = new List<DateTime>();
        IHeatMapPlayer player = null;
        IHeatMap heatmap = null;

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

            this.axRenderControl1.Camera.FlyTime = 1;

            #region 加载SDKDEMO场景
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
                //遍历FeatureClass
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
                    flayerList.Add(featureLayer);

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
                        angle.Set(0, -90, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion

            //注册地形
            string tmpTedPath = (strMediaPath + @"\terrain\SingaporePlanarTerrain.ted");
            this.axRenderControl1.Terrain.RegisterTerrain(tmpTedPath, "");

            if (player == null)
                player = this.axRenderControl1.HeatMapPlayer;

            // 加载FDB场景
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\HeatMap.FDB");
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

                    if (fc.HasTemporal())
                    {
                        ITemporalManager tm = fc.TemporalManager;
                        DateTime[] times = tm.GetKeyDatetimes();
                        for (int i = 0; i < times.Length; i++)
                        {
                            if (keyDatetimesList.Contains(times[i]))
                                continue;
                            keyDatetimesList.Add(times[i]);
                        }
                    }                    
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            // 设置时间轴
            if (keyDatetimesList.Count == 0)
            {
                this.cbKeyTimes.Enabled = false;
                this.btnPlay.Enabled = false;
                this.btnPause.Enabled = false;
                this.btnStop.Enabled = false;
            }
            else
            {
                keyDatetimesList.Sort();
                for (int t = 0; t < keyDatetimesList.Count; t++)
                {
                    DateTime d = keyDatetimesList[t];
                    cbKeyTimes.Items.Add(d);
                }
            }

            // CreateFeautureLayer
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    heatmap = this.axRenderControl1.ObjectManager.CreateHeatMap(
                    fc, geoName, "HotValue", rootId);
                    if (heatmap == null)
                        continue;
                    this.txtMinValue.Text = heatmap.MinHeatValue.ToString();
                    this.txtMaxValue.Text = heatmap.MaxHeatValue.ToString();
                }
            }

            //设置curLayer的时刻为最新时刻
            if (keyDatetimesList.Count > 0)
            {
                DateTime dmax = (keyDatetimesList[keyDatetimesList.Count - 1]);
                player.SetTime(dmax);

                //设置时间轴为最小
                cbKeyTimes.SelectedIndex = keyDatetimesList.Count - 1;
            }

            //测试自定义颜色
            //System.Drawing.Color[] colors = new System.Drawing.Color[2];
            //colors[0] = System.Drawing.Color.White;
            //colors[1] = System.Drawing.Color.Gray;
            //bool suc = player.SetColor(colors, 2);
            //if(suc)
            //{
            //    System.Drawing.Color[] colorvec;
            //    byte c;
            //    bool suc2 = player.GetColor(out colorvec, out c);
            //    if (suc2)
            //        this.Text = c.ToString();
            //}            

            this.cbShowHeatMap.Checked = true;
            this.cbShowFeatureLayer.Checked = true;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "Temporal.html";
            }
        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (this.btnPlay.Text == "播放")
            {
                double duration = 10;
                try
                {
                    duration = double.Parse(this.txtDuration.Text);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("请输入正确的播放时长");
                }

                DateTime startTime = (keyDatetimesList[0]);
                DateTime endTime = (keyDatetimesList[keyDatetimesList.Count - 2]);
                player.StartPlay(startTime, endTime, duration);
            }
            else if (this.btnPlay.Text == "继续播放")
            {
                player.Continue();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            player.Pause();
            this.btnPlay.Text = "继续播放";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
            this.btnPlay.Text = "播放";
        }

        private void cbShowHeatMap_CheckedChanged(object sender, EventArgs e)
        {
            heatmap.VisibleMask = cbShowHeatMap.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
        }

        private void cbKeyTimes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime d = (keyDatetimesList[this.cbKeyTimes.SelectedIndex]);
            player.SetTime(d);
        }

        private void txtRadius_TextChanged(object sender, EventArgs e)
        {
            double radius = 100;
            try
            {
                radius = double.Parse(this.txtRadius.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请输入正确的半径");
            }
            if(heatmap != null)
                heatmap.AttenRadius = radius;
        }

        private void cbShowFeatureLayer_CheckedChanged(object sender, EventArgs e)
        {
            foreach (IFeatureLayer layer in flayerList)
            {
                layer.VisibleMask = cbShowFeatureLayer.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
            }
        }

        private void txtMinValue_TextChanged(object sender, EventArgs e)
        {
            double min = 100;
            try
            {
                min = double.Parse(this.txtMinValue.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请输入正确的值");
            }
            if (heatmap != null)
                heatmap.MinHeatValue = min;
        }

        private void txtMaxValue_TextChanged(object sender, EventArgs e)
        {
            double max = 100;
            try
            {
                max = double.Parse(this.txtMaxValue.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请输入正确的值");
            }
            if (heatmap != null)
                heatmap.MaxHeatValue = max;
        }        

    }
}
