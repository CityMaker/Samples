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
                this.helpProvider1.HelpNamespace = "ViewshedAnalysis.html";
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
        }

        #region 视域分析
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
            axRenderControl1.HighlightHelper.SetRegion(null);
            //axRenderControl1.VisualAnalysis.ClearOccluders();  //清除遮挡物
            axRenderControl1.VisualAnalysis.StopAnalyse();
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
                    this.startX.Text = IntersectPoint.X.ToString();
                    this.startY.Text = IntersectPoint.Y.ToString();
                    this.startZ.Text = (IntersectPoint.Z + double.Parse(numZOffset.Value.ToString())).ToString();

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
                    

                    geoRegion = axRenderControl1.HighlightHelper.GetRegion();
                    //开始空间查询
                    try
                    {
                        this.label7.Text = "开始空间查询";
                        ISpatialFilter sfilter = new SpatialFilter();
                        sfilter.Geometry = geoRegion;
                        sfilter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                        sfilter.GeometryField = "footprint";
                        cursor = buildingFC.Search(sfilter, false);
                        row = null;
                        while ((row = cursor.NextRow()) != null)
                        {
                            int nfidpos = row.FieldIndex("Geometry");
                            if (nfidpos > -1)
                                geoInFC = row.GetValue(nfidpos) as IGeometry;
                            if (geoInFC != null)
                            {
                                //AddOcluder
                                axRenderControl1.VisualAnalysis.AddOccluder(buildingFL, geoInFC);

                                //highlight occluder
                                int fidpos = row.FieldIndex("oid");
                                if (fidpos > -1)
                                    axRenderControl1.FeatureManager.HighlightFeature(buildingFC, int.Parse(row.GetValue(fidpos).ToString()), System.Drawing.Color.Red);
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

                    this.label7.Text = "正在视域分析中...";
                    //StartAnalyse
                    axRenderControl1.VisualAnalysis.StartViewshedAnalyse(fde_point1, fde_point2, double.Parse(this.numHorizontalAngle.Value.ToString()));
                    axRenderControl1.HighlightHelper.VisibleMask = 0;
                    this.label7.Text = "分析结束";
                }
            }
            else if (EventSender.Equals(gviMouseSelectMode.gviMouseSelectMove))
            {
                this.endX.Text = IntersectPoint.X.ToString();
                this.endY.Text = IntersectPoint.Y.ToString();
                this.endZ.Text = IntersectPoint.Z.ToString();

                fde_point1 = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPoint,
                  gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                fde_point1.SetCoords(double.Parse(this.startX.Text), double.Parse(this.startY.Text), double.Parse(this.startZ.Text), 0, 0);
                fde_point2 = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPoint,
                   gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                fde_point2.SetCoords(double.Parse(this.endX.Text), double.Parse(this.endY.Text), double.Parse(this.endZ.Text), 0, 0);
                axRenderControl1.HighlightHelper.SetSectorRegion(fde_point1, fde_point2, double.Parse(this.numHorizontalAngle.Value.ToString()));
            }
        }
        #endregion

        /// <summary>
        /// 飞入观察点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlyToSourcePoint_Click(object sender, EventArgs e)
        {
            if (this.startX.Text != "" && this.startY.Text != "" && this.startZ.Text != ""
                    && this.endX.Text != "" && this.endY.Text != "" && this.endZ.Text != "")
            {
                IVector3 position1 = new Vector3();
                position1.Set(double.Parse(startX.Text), double.Parse(startY.Text), double.Parse(startZ.Text));
                IVector3 position2 = new Vector3();
                position2.Set(double.Parse(endX.Text), double.Parse(endY.Text), double.Parse(endZ.Text));
                IEulerAngle angle = new EulerAngle();
                angle = axRenderControl1.Camera.GetAimingAngles(position1, position2);
                axRenderControl1.Camera.LookAt(position1, 0, angle);
            }
        }

        /// <summary>
        /// 飞入目标点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlyToTargetPoint_Click(object sender, EventArgs e)
        {
            if (this.startX.Text != "" && this.startY.Text != "" && this.startZ.Text != ""
                    && this.endX.Text != "" && this.endY.Text != "" && this.endZ.Text != "")
            {
                IVector3 position1 = new Vector3();
                position1.Set(double.Parse(endX.Text), double.Parse(endY.Text), double.Parse(endZ.Text));
                IVector3 position2 = new Vector3();
                position2.Set(double.Parse(startX.Text), double.Parse(startY.Text), double.Parse(startZ.Text));
                IEulerAngle angle = new EulerAngle();
                angle = axRenderControl1.Camera.GetAimingAngles(position1, position2);
                axRenderControl1.Camera.LookAt(position1, 0, angle);
            }
        }

    }
}
