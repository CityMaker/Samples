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

namespace RenderParam
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名        
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
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }

            // 注册地形
            string tmpTedPath = (strMediaPath + @"\terrain\SingaporePlanarTerrain.ted");
            this.axRenderControl1.Terrain.RegisterTerrain(tmpTedPath, "");

            #region 获取默认值
            int v = (int)this.axRenderControl1.GetRenderParam(gviRenderControlParameters.gviRenderParamLanguage);
            this.简体中文ToolStripMenuItem.Checked = false;
            this.繁体中文ToolStripMenuItem.Checked = false;
            this.英文ToolStripMenuItem.Checked = false;
            switch (v)
            {
                case 0:
                    this.简体中文ToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    this.繁体中文ToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    this.英文ToolStripMenuItem.Checked = true;
                    break;
            }

            v = (int)this.axRenderControl1.GetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementAreaUnit);
            平方米ToolStripMenuItem.Checked = false;
            平方公里ToolStripMenuItem.Checked = false;
            公顷ToolStripMenuItem.Checked = false;
            亩ToolStripMenuItem.Checked = false;
            顷ToolStripMenuItem.Checked = false;
            英亩ToolStripMenuItem.Checked = false;
            平方英里ToolStripMenuItem.Checked = false;
            switch (v)
            {
                case 0:
                    平方米ToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    平方公里ToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    公顷ToolStripMenuItem.Checked = true;
                    break;
                case 3:
                    亩ToolStripMenuItem.Checked = true;
                    break;
                case 4:
                    顷ToolStripMenuItem.Checked = true;
                    break;
                case 5:
                    英亩ToolStripMenuItem.Checked = true;
                    break;
                case 6:
                    平方英里ToolStripMenuItem.Checked = true;
                    break;
            }

            v = (int)this.axRenderControl1.GetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementLengthUnit);
            米ToolStripMenuItem.Checked = false;
            公里ToolStripMenuItem.Checked = false;
            英尺ToolStripMenuItem.Checked = false;
            英里ToolStripMenuItem.Checked = false;
            海里ToolStripMenuItem.Checked = false;
            switch (v)
            {
                case 0:
                    米ToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    公里ToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    英尺ToolStripMenuItem.Checked = true;
                    break;
                case 3:
                    英里ToolStripMenuItem.Checked = true;
                    break;
                case 4:
                    海里ToolStripMenuItem.Checked = true;
                    break;
            }

            bool ret = (bool)this.axRenderControl1.GetRenderParam(gviRenderControlParameters.gviRenderParamWireframeEffect);
            this.开启ToolStripMenuItem.Checked = false;
            this.关闭ToolStripMenuItem.Checked = false;
            if (ret)
                this.开启ToolStripMenuItem.Checked = true;
            else
                this.关闭ToolStripMenuItem.Checked = true;

            #endregion 获取默认值

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "RenderParam.html";
            }
        }

        #region InteractMode

        private void toolStripNormal_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;

            this.Text = "当前处于漫游模式";
        }

        private void toolStripSelect_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            

            this.Text = "当前处于选择模式";
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            MessageBox.Show(string.Format("拾取到{0}类型的物体", PickResult.Type.ToString()));
        }

        private void toolStripAerialDistance_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureAerialDistance;

            this.Text = "当前处于直线测距模式";
        }

        private void toolStripHorizontalDistance_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureHorizontalDistance;

            this.Text = "当前处于水平测距模式";
        }

        private void toolStripVerticalDistance_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureVerticalDistance;

            this.Text = "当前处于垂直测距模式";
        }

        private void toolStripCoordinate_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureCoordinate;

            this.Text = "当前处于拾取坐标模式";
        }

        private void toolStripGroundDistance_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureGroundDistance;

            this.Text = "当前处于地表距离测量模式";
        }

        private void toolStripArea_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureArea;

            this.Text = "当前处于投影面积测量模式";
        }

        private void toolStripGroundArea_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureGroundArea;

            this.Text = "当前处于地表面积测量模式";
        }

        private void toolStripGroupSightLine_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
            this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureGroupSightLine;

            this.Text = "当前处于地形通视分析测量模式";
        }

        private void toolStripWalk_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractWalk;

            this.Text = "当前处于步行模式";

            this.axRenderControl1.Focus();  //三维控件取得焦点，以便步行模式键盘有效
        }

        private void toolStrip2DMap_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteract2DMap;

            this.Text = "当前处于二维地图模式";
        }

        private void toolStripDisable_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractDisable;

            this.Text = "当前处于禁止交互模式";
        }
        #endregion InteractMode

        private void 米ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementLengthUnit, gviLengthUnit.gviLengthUnitMeter);
            米ToolStripMenuItem.Checked = true;
            公里ToolStripMenuItem.Checked = false;
            英尺ToolStripMenuItem.Checked = false;
            英里ToolStripMenuItem.Checked = false;
            海里ToolStripMenuItem.Checked = false;
        }

        private void 公里ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementLengthUnit, gviLengthUnit.gviLengthUnitKilometer);
            米ToolStripMenuItem.Checked = false;
            公里ToolStripMenuItem.Checked = true;
            英尺ToolStripMenuItem.Checked = false;
            英里ToolStripMenuItem.Checked = false;
            海里ToolStripMenuItem.Checked = false;
        }

        private void 英尺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementLengthUnit, gviLengthUnit.gviLengthUnitFoot);
            米ToolStripMenuItem.Checked = false;
            公里ToolStripMenuItem.Checked = false;
            英尺ToolStripMenuItem.Checked = true;
            英里ToolStripMenuItem.Checked = false;
            海里ToolStripMenuItem.Checked = false;
        }

        private void 英里ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementLengthUnit, gviLengthUnit.gviLengthUnitMile);
            米ToolStripMenuItem.Checked = false;
            公里ToolStripMenuItem.Checked = false;
            英尺ToolStripMenuItem.Checked = false;
            英里ToolStripMenuItem.Checked = true;
            海里ToolStripMenuItem.Checked = false;
        }

        private void 海里ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementLengthUnit, gviLengthUnit.gviLengthUnitSeaMile);
            米ToolStripMenuItem.Checked = false;
            公里ToolStripMenuItem.Checked = false;
            英尺ToolStripMenuItem.Checked = false;
            英里ToolStripMenuItem.Checked = false;
            海里ToolStripMenuItem.Checked = true;
        }

        private void 平方米ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementAreaUnit, gviAreaUnit.gviAreaUnitSquareMeter);
            平方米ToolStripMenuItem.Checked = true;
            平方公里ToolStripMenuItem.Checked = false;
            公顷ToolStripMenuItem.Checked = false;
            亩ToolStripMenuItem.Checked = false;
            顷ToolStripMenuItem.Checked = false;
            英亩ToolStripMenuItem.Checked = false;
            平方英里ToolStripMenuItem.Checked = false;
        }

        private void 平方公里ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementAreaUnit, gviAreaUnit.gviAreaUnitSquareKilometer);
            平方米ToolStripMenuItem.Checked = false;
            平方公里ToolStripMenuItem.Checked = true;
            公顷ToolStripMenuItem.Checked = false;
            亩ToolStripMenuItem.Checked = false;
            顷ToolStripMenuItem.Checked = false;
            英亩ToolStripMenuItem.Checked = false;
            平方英里ToolStripMenuItem.Checked = false;
        }

        private void 公顷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementAreaUnit, gviAreaUnit.gviAreaUnitHectare);
            平方米ToolStripMenuItem.Checked = false;
            平方公里ToolStripMenuItem.Checked = false;
            公顷ToolStripMenuItem.Checked = true;
            亩ToolStripMenuItem.Checked = false;
            顷ToolStripMenuItem.Checked = false;
            英亩ToolStripMenuItem.Checked = false;
            平方英里ToolStripMenuItem.Checked = false;
        }

        private void 亩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementAreaUnit, gviAreaUnit.gviAreaUnitMu);
            平方米ToolStripMenuItem.Checked = false;
            平方公里ToolStripMenuItem.Checked = false;
            公顷ToolStripMenuItem.Checked = false;
            亩ToolStripMenuItem.Checked = true;
            顷ToolStripMenuItem.Checked = false;
            英亩ToolStripMenuItem.Checked = false;
            平方英里ToolStripMenuItem.Checked = false;
        }

        private void 顷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementAreaUnit, gviAreaUnit.gviAreaUnitQing);
            平方米ToolStripMenuItem.Checked = false;
            平方公里ToolStripMenuItem.Checked = false;
            公顷ToolStripMenuItem.Checked = false;
            亩ToolStripMenuItem.Checked = false;
            顷ToolStripMenuItem.Checked = true;
            英亩ToolStripMenuItem.Checked = false;
            平方英里ToolStripMenuItem.Checked = false;
        }

        private void 英亩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementAreaUnit, gviAreaUnit.gviAreaUnitAcre);
            平方米ToolStripMenuItem.Checked = false;
            平方公里ToolStripMenuItem.Checked = false;
            公顷ToolStripMenuItem.Checked = false;
            亩ToolStripMenuItem.Checked = false;
            顷ToolStripMenuItem.Checked = false;
            英亩ToolStripMenuItem.Checked = true;
            平方英里ToolStripMenuItem.Checked = false;
        }

        private void 平方英里ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamMeasurementAreaUnit, gviAreaUnit.gviAreaUnitSquareMile);
            平方米ToolStripMenuItem.Checked = false;
            平方公里ToolStripMenuItem.Checked = false;
            公顷ToolStripMenuItem.Checked = false;
            亩ToolStripMenuItem.Checked = false;
            顷ToolStripMenuItem.Checked = false;
            英亩ToolStripMenuItem.Checked = false;
            平方英里ToolStripMenuItem.Checked = true;
        }

        private void 简体中文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamLanguage, gviLanguage.gviLanguageChineseSimple);
            简体中文ToolStripMenuItem.Checked = true;
            繁体中文ToolStripMenuItem.Checked = false;
            英文ToolStripMenuItem.Checked = false;
        }

        private void 繁体中文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamLanguage, gviLanguage.gviLanguageChineseTraditional);
            简体中文ToolStripMenuItem.Checked = false;
            繁体中文ToolStripMenuItem.Checked = true;
            英文ToolStripMenuItem.Checked = false;
        }

        private void 英文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamLanguage, gviLanguage.gviLanguageEnglish);
            简体中文ToolStripMenuItem.Checked = false;
            繁体中文ToolStripMenuItem.Checked = false;
            英文ToolStripMenuItem.Checked = true;
        }

        private void 开启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamWireframeEffect, true);
            开启ToolStripMenuItem.Checked = true;
            关闭ToolStripMenuItem.Checked = false;
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamWireframeEffect, false);
            开启ToolStripMenuItem.Checked = false;
            关闭ToolStripMenuItem.Checked = true;
        }

        

    }
}
