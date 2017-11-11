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

namespace FiveDShow
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEulerAngle angle = new EulerAngle();
        private IVector3 position = new Vector3();
        
        List<IFeatureLayer> layerList = new List<IFeatureLayer>();
        List<DateTime> keyDatetimesList = new List<DateTime>();
        DateTime dayLongLongAgo = new DateTime(1900, 1, 1);

        private System.Guid rootId = new System.Guid();
        private IPropertySet pset = new PropertySet();

        public MainForm()
        {
            InitializeComponent();

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
                string tmpFDBPath = (strMediaPath + @"\BIMTIME.FDB");
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

                    bool bHasTemp = fc.HasTemporal();
                    if (bHasTemp)
                    {
                        ITemporalManager tm = fc.TemporalManager;
                        if (tm != null)
                        {
                            try
                            {
                                DateTime[] times = tm.GetKeyDatetimes();
                                for (int i = 0; i < times.Length; i++)
                                {
                                    if (keyDatetimesList.Contains(times[i]))
                                        continue;
                                    keyDatetimesList.Add(times[i]);
                                }
                            }
                            catch (COMException)
                            {
                            }
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
            keyDatetimesList.Sort();
            if (keyDatetimesList.Count > 3)
            {
                TimeSpan tsmin = keyDatetimesList[0] - dayLongLongAgo;
                this.trackBarTime.Minimum = tsmin.Days - 1;
                this.trackBarTimeCompare.Minimum = tsmin.Days - 1;
                TimeSpan tsmax = keyDatetimesList[keyDatetimesList.Count - 2] - dayLongLongAgo;
                this.trackBarTime.Maximum = tsmax.Days + 1;
                this.trackBarTimeCompare.Maximum = tsmax.Days + 1;
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
                    if (featureLayer == null)
                        continue;

                    #region 设置时态
                    featureLayer.EnableTemporal = true;
                    layerList.Add(featureLayer);

                    //设置curLayer的时刻
                    this.trackBarTime.Value = this.trackBarTime.Minimum;
                    DateTime d = dayLongLongAgo.AddDays((double)this.trackBarTime.Value);
                    this.labelTime.Text = d.ToString();
                    featureLayer.Time = d;
                    #endregion

                    #region 比较规则
                    pset.SetProperty("提前", "$(starttime)");
                    featureLayer.CompareRenderRuleVariants = pset;

                    IValueMapGeometryRender render = new ValueMapGeometryRender();
                    {
                        IGeometryRenderScheme scheme = new GeometryRenderScheme();
                        IComparedRenderRule rule = new ComparedRenderRule();
                        rule.LookUpField = "实际开始时间";
                        rule.CompareVariant = "提前";
                        rule.CompareOperator = gviCompareType.gviCompareLess;
                        scheme.AddRule(rule);
                        IModelPointSymbol symbol = new ModelPointSymbol();
                        symbol.EnableColor = true;
                        symbol.Color = System.Drawing.Color.Green;
                        scheme.Symbol = symbol;
                        render.AddScheme(scheme);
                    }
                    {
                        IGeometryRenderScheme scheme = new GeometryRenderScheme();
                        IComparedRenderRule rule = new ComparedRenderRule();
                        rule.LookUpField = "实际开始时间";
                        rule.CompareVariant = "提前";
                        rule.CompareOperator = gviCompareType.gviCompareGreaterOrEqual;
                        scheme.AddRule(rule);
                        IModelPointSymbol symbol = new ModelPointSymbol();
                        symbol.EnableColor = false;                        
                        scheme.Symbol = symbol;
                        render.AddScheme(scheme);
                    }
                    featureLayer.SetGeometryRender(render);
                    #endregion

                    if (!hasfly)
                    {
                        IFieldInfoCollection fieldinfos = fc.GetFields();
                        IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        IEnvelope env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;
                        angle.Set(0, -52, 0);
                        position.Set(-13.44, -88.77, 59.28);
                        this.axRenderControl1.Camera.LookAt(position, 100, angle);
                        hasfly = true;
                    }                    
                }
            }

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "FiveDShow.html";
            }
        }

        private void trackBarTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime d = dayLongLongAgo.AddDays((double)this.trackBarTime.Value);
            this.labelTime.Text = d.ToString();

            foreach (IFeatureLayer layer in layerList)
            {
                layer.Time = d;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.trackBarTime.Value < this.trackBarTime.Maximum)
            {
                this.trackBarTime.Value++;
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}
