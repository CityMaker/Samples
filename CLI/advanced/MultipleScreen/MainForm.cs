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
//author	gs
//date	2011/09/26
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Controls;

namespace MultipleScreen
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        

        IRenderModelPoint renderModelPoint1;
        IRenderModelPoint renderModelPoint2;
        IRenderModelPoint renderModelPoint3;
        IRenderModelPoint renderModelPoint4;

        double dx = 15370.86;
        double dy = 35977.49;
        double dz = 5.28 + 115;

        private IGeometryFactory geoFactory = null;

        private System.Guid rootId = new System.Guid();

        TrackBar splitTrackH;  //SplitRatio滑动条
        TrackBar splitTrackV;

        ISpatialCRS _datasetCRS = null;

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;
            axRenderControl1.Camera.FlyTime = 1;
            
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

                skybox = this.axRenderControl1.ObjectManager.GetSkyBox(1);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\2_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\2_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\2_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\2_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\2_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\2_UP.jpg");

                skybox = this.axRenderControl1.ObjectManager.GetSkyBox(2);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\04_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\04_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\04_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\04_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\04_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\04_UP.jpg");

                skybox = this.axRenderControl1.ObjectManager.GetSkyBox(3);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\9_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\9_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\9_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\9_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\9_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\9_UP.jpg");    
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
                _datasetCRS = dataset.SpatialReference;
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
                        IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        point.SpatialCRS = _datasetCRS;
                        point.SetCoords(env.Center.X, env.Center.Y, env.Center.Z, 0, 0);
                        this.axRenderControl1.Camera.LookAt2(point, 500, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion 加载FDB场景
         

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "MultipleScreen.html";
            }
        }

        public MainForm()
        {
            InitializeComponent();

            splitTrackH = new TrackBar();
            splitTrackH.Minimum = 1;
            splitTrackH.Maximum = 99;
            splitTrackH.TickFrequency = 1;            
            splitTrackH.Location = new System.Drawing.Point(SplitRatioH.Location.X + 60, SplitRatioH.Location.Y);
            splitTrackH.Size = new System.Drawing.Size(800, 45);
            splitTrackH.ValueChanged += new EventHandler(track_ValueChanged);
            this.panel3.Controls.Add(splitTrackH);

            splitTrackV = new TrackBar();
            splitTrackV.Minimum = 1;
            splitTrackV.Maximum = 99;
            splitTrackV.TickFrequency = 1;            
            splitTrackV.Location = new System.Drawing.Point(SplitRatioV.Location.X + 60, SplitRatioV.Location.Y);
            splitTrackV.Size = new System.Drawing.Size(800, 45);
            splitTrackV.ValueChanged += new EventHandler(splitTrackV_ValueChanged);
            this.panel4.Controls.Add(splitTrackV);

        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            
            init();

            splitTrackH.Value = (int)(axRenderControl1.Viewport.SplitRatioH * 100);
            splitTrackV.Value = (int)(axRenderControl1.Viewport.SplitRatioH * 100);

            axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportQuad;

            this.cbCompassVisibleMask.SelectedIndex = 5;
            this.cbCameraBind.Checked = true;
            this.cbShowBorder.Checked = true;
            axRenderControl1.Viewport.CameraViewBindMask = gviViewportMask.gviViewAllNormalView;

            LoadData();
        }

        private void LoadData()
        {
            if (geoFactory == null)
                geoFactory = new GeometryFactory();

            string modelName = (strMediaPath + @"\osg\Schemes\scheme-01.osg");
            IModelPoint modelPoint = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            modelPoint.ModelName = modelName;
            modelPoint.SetCoords(dx, dy, dz + 15, 0, 0);
            modelPoint.SpatialCRS = _datasetCRS;
            renderModelPoint1 = axRenderControl1.ObjectManager.CreateRenderModelPoint(modelPoint, null, rootId);
            renderModelPoint1.VisibleMask = gviViewportMask.gviView0;
            IVector3 position = new Vector3();
            position.Set(dx, dy, dz);
            IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            point.SpatialCRS = _datasetCRS;
            point.SetCoords(position.X, position.Y, position.Z, 0, 0);
            IEulerAngle angle = new EulerAngle();
            angle.Set(0, -20, 0);
            axRenderControl1.Camera.LookAt2(point, 300, angle);

            modelName = (strMediaPath + @"\osg\Schemes\scheme-02.osg");
            modelPoint = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            modelPoint.ModelName = modelName;
            modelPoint.SetCoords(dx, dy, dz - 15, 0, 0);
            modelPoint.SpatialCRS = _datasetCRS;
            renderModelPoint2 = axRenderControl1.ObjectManager.CreateRenderModelPoint(modelPoint, null, rootId);
            renderModelPoint2.VisibleMask = gviViewportMask.gviView1;


            modelName = (strMediaPath + @"\osg\Schemes\scheme-03.osg");
            modelPoint = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            modelPoint.ModelName = modelName;
            modelPoint.SetCoords(dx, dy, dz - 10, 0, 0);
            modelPoint.SpatialCRS = _datasetCRS;
            renderModelPoint3 = axRenderControl1.ObjectManager.CreateRenderModelPoint(modelPoint, null, rootId);
            renderModelPoint3.VisibleMask = gviViewportMask.gviView2;


            modelName = (strMediaPath + @"\osg\Schemes\scheme-04.osg");
            modelPoint = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            modelPoint.ModelName = modelName;
            modelPoint.SetCoords(dx, dy, dz - 13, 0, 0);
            modelPoint.SpatialCRS = _datasetCRS;
            renderModelPoint4 = axRenderControl1.ObjectManager.CreateRenderModelPoint(modelPoint, null, rootId);
            renderModelPoint4.VisibleMask = gviViewportMask.gviView3;
        }

        /// <summary>
        /// 立体模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonMode_CheckedChanged(object sender, EventArgs e)
        {
            //恢复renderModelPoint1位置
            axRenderControl1.Viewport.ActiveView = 0;
            renderModelPoint1.VisibleMask = gviViewportMask.gviView0;

            if (radioButtonQuad.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportQuad;
            else if (radioButtonQuadH.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportQuadH;
            else if (radioButtonT1B1.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportT1B1;
            else if (radioButtonT1M1B1.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportT1M1B1;
            else if (radioButtonL1R1.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL1R1;
            else if (radioButtonL1M1R1.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL1M1R1;
            else if (radioButtonL1R2.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL1R2;
            else if (radioButtonL2R1.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL2R1;
            else if (radioButtonL1R1SingleFrustum.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL1R1SingleFrustum;
            else if (radioButtonT1B1SingleFrustum.Checked)
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportT1B1SingleFrustum;
            else if (radioButtonPIP.Checked)
            {
                //获取原始相机位置                
                IVector3 cameraPos = new Vector3();
                IEulerAngle cameraAngle = new EulerAngle();
                axRenderControl1.Camera.GetCamera(out cameraPos, out cameraAngle);

                //设值PIP视口相机位置
                axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportPIP;
                axRenderControl1.Viewport.ActiveView = 4;
                renderModelPoint1.VisibleMask = gviViewportMask.gviViewPIP;
                //注意：PIP窗口不支持FlyToObject，支持SetCamera和LookAt
                //axRenderControl1.Camera.FlyToObject(renderModelPoint1.Guid, gviActionCode.gviActionFlyTo);
                axRenderControl1.Camera.SetCamera(cameraPos, cameraAngle, gviSetCameraFlags.gviSetCameraNoFlags);
                //axRenderControl1.Camera.LookAt(cameraPos, 50, cameraAngle);
            }

            // 目前多视口模式切换时，底层未记住相机是否绑定，故此手动设置
            if (this.cbCameraBind.Checked)
            {
                axRenderControl1.Viewport.CameraViewBindMask = gviViewportMask.gviViewAllNormalView;
            }
            else
            {
                axRenderControl1.Viewport.CameraViewBindMask = gviViewportMask.gviViewNone;
            }
        }

        private void cbCameraInfoVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbCameraInfoVisible.Checked)
            {
                axRenderControl1.Viewport.CameraInfoVisible = true;
            }
            else
            {
                axRenderControl1.Viewport.CameraInfoVisible = false;
            }
        }

        private void cbCompassVisibleMask_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cbCompassVisibleMask.SelectedIndex)
            {
                case 0:
                    axRenderControl1.Viewport.CompassVisibleMask = gviViewportMask.gviViewNone;
                    break; 
                case 1:
                    axRenderControl1.Viewport.CompassVisibleMask = gviViewportMask.gviView0;
                    break;
                case 2:
                    axRenderControl1.Viewport.CompassVisibleMask = gviViewportMask.gviView1;
                    break;
                case 3:
                    axRenderControl1.Viewport.CompassVisibleMask = gviViewportMask.gviView2;
                    break;
                case 4:
                    axRenderControl1.Viewport.CompassVisibleMask = gviViewportMask.gviView3;
                    break;
                case 5:
                    axRenderControl1.Viewport.CompassVisibleMask = gviViewportMask.gviViewAllNormalView;
                    break;
                case 6:
                    axRenderControl1.Viewport.CompassVisibleMask = gviViewportMask.gviViewPIP;
                    break;
            }
        }

        /// <summary>
        /// 相机是否绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCameraBind_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbCameraBind.Checked)
            {
                axRenderControl1.Viewport.CameraViewBindMask = gviViewportMask.gviViewAllNormalView;
            }
            else
            {
                axRenderControl1.Viewport.CameraViewBindMask = gviViewportMask.gviViewNone;
            }
        }



        void splitTrackV_ValueChanged(object sender, EventArgs e)
        {
            double opacity = splitTrackV.Value / 100.0;
            axRenderControl1.Viewport.SplitRatioV = opacity;
        }

        void track_ValueChanged(object sender, EventArgs e)
        {
            double opacity = splitTrackH.Value / 100.0;
            axRenderControl1.Viewport.SplitRatioH = opacity;
        }

        private void cbShowBorder_CheckedChanged(object sender, EventArgs e)
        {
            axRenderControl1.Viewport.ShowBorderLine = this.cbShowBorder.Checked;            
        }

    }
}
