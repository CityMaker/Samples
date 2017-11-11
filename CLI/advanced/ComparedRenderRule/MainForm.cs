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

namespace ComparedRender
{
    public partial class MainForm : Form
    {
        private int flag = -1;  // 标记"Samples"文件夹在目录中的索引号
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEulerAngle angle = new EulerAngle();
        private IVector3 position = new Vector3();

        IFeatureLayer buildlayer = null;

        private System.Guid rootId = new System.Guid();

        IPropertySet pset = null;

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
            flag = Application.StartupPath.LastIndexOf("Samples");
            if (flag > -1)
            {
                string tmpSkyboxPath = Path.Combine(Application.StartupPath.Substring(0, flag), @"Samples\Media\skybox");
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
                string tmpFDBPath = Path.Combine(Application.StartupPath.Substring(0, flag), @"Samples\Media\SDKDEMO.FDB");
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

            // 设置时间轴
            this.trackBarTime.Minimum = 0;
            this.trackBarTime.Maximum = 4;

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

                    if(fc.Name.Equals("Building") && geoName.Equals("Geometry"))
                    {
                        pset = new PropertySet();
                        pset.SetProperty("type", this.trackBarTime.Minimum);
                        featureLayer.CompareRenderRuleVariants = pset;

                        IValueMapGeometryRender render = new ValueMapGeometryRender();
                        {
                            IGeometryRenderScheme scheme = new GeometryRenderScheme();
                            IComparedRenderRule rule = new ComparedRenderRule();
                            rule.LookUpField = "BuildType";
                            rule.CompareVariant = "type";
                            rule.CompareOperator = gviCompareType.gviCompareEqual;
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
                            rule.LookUpField = "BuildType";
                            rule.CompareVariant = "type";
                            rule.CompareOperator = gviCompareType.gviCompareLessOrEqual;
                            scheme.AddRule(rule);
                            render.AddScheme(scheme);
                        }
                        featureLayer.SetGeometryRender(render);
                        buildlayer = featureLayer;
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
                        angle.Set(0, -30, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 300, angle);
                        hasfly = true;
                    }                    
                }
            }

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "ComparedRenderRule.html";
            }
        }

        private void trackBarTime_ValueChanged(object sender, EventArgs e)
        {
            this.labelTime.Text = this.trackBarTime.Value.ToString();

            pset.SetProperty("type", this.trackBarTime.Value);
            buildlayer.CompareRenderRuleVariants = pset;
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
