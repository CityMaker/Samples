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

namespace StereoDisplay
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
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
            ps.SetProperty("QuadBufferStereo", true);  //设置四缓冲立体启用
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
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion

            // 下拉框控件默认选中第一项
            this.toolStripComboBoxViewportMode.SelectedIndex = 0;

            this.txtEyeSeparation.Text = this.axRenderControl1.GetRenderParam(gviRenderControlParameters.gviRenderParamStereoEyeSeparation).ToString();
            this.txtFusionDistance.Text = this.axRenderControl1.GetRenderParam(gviRenderControlParameters.gviRenderParamStereoFusionDistance).ToString();
            this.txtScreenDistance.Text = this.axRenderControl1.GetRenderParam(gviRenderControlParameters.gviRenderParamStereoScreenDistance).ToString();

            this.txtFusionDistance.TextChanged += new System.EventHandler(this.txtFusionDistance_TextChanged);
            this.txtEyeSeparation.TextChanged += new System.EventHandler(this.txtEyeSeparation_TextChanged);
            this.txtScreenDistance.TextChanged += new System.EventHandler(this.txtScreenDistance_TextChanged);

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "StereoDisplay.html";
            }    
        }

        private void toolStripComboBoxViewportMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.toolStripComboBoxViewportMode.Text.Trim())
            {
                case "ViewportSinglePerspective":
                    {
                        this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
                    }
                    break;
                case "ViewportStereoAnaglyph":
                    {
                        this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportStereoAnaglyph;
                    }
                    break;
                case "ViewportStereoQuadbuffer":
                    {
                        this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportStereoQuadbuffer;
                    }
                    break;
                case "ViewportStereoDualView":
                    {
                        this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportStereoDualView;
                    }
                    break;
            }
        }

        private void txtFusionDistance_TextChanged(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamStereoFusionDistance, txtFusionDistance.Text);
        }

        private void txtEyeSeparation_TextChanged(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamStereoEyeSeparation, txtEyeSeparation.Text);
        }

        private void txtScreenDistance_TextChanged(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamStereoScreenDistance, txtScreenDistance.Text);
        }
    }
}
