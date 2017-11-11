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
//date	2013/06/18
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

namespace HideSelectedArea
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private bool CTRL = true;  //标记拾取时是否支持“ctrl”键用于多选
        private List<FCAndFeature> featureList = new List<FCAndFeature>();

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
            #endregion 加载FDB场景
            
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "HideSelectedArea.html";
            }

            // 注册控件拾取事件
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.RcMouseDragSelect += new _IRenderControlEvents_RcMouseDragSelectEventHandler(axRenderControl1_RcMouseDragSelect);
            
            
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectDrag;
        }

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

            init();

            this.comboBoxViewportMode.SelectedIndex = 0;
        }

        /// <summary>
        /// 改变RenderControl视口模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxViewportMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBoxViewportMode.Text)
            {
                case "gviViewportSinglePerspective 单视口透视投影":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
                    break;
                case "gviViewportL1R1  左一右一 ":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL1R1;
                    break;
                case "gviViewportT1B1  上一下一 ":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportT1B1;
                    break;
                case "gviViewportL1M1R1  左一中一右一 ":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL1M1R1;
                    break;
                case "gviViewportT1M1B1  上一中一下一 ":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportT1M1B1;
                    break;
                case "gviViewportL2R1  左边两个视口，右边一个 ":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL2R1;
                    break;
                case "gviViewportL1R2  左一右二 ":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportL1R2;
                    break;
                case "gviViewportQuad  四视口 ":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportQuad;
                    break;
                case "gviViewportPIP  大小两视图（画中画）":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportPIP;
                    break;
                case "gviViewportQuadH  水平四视口":
                    this.axRenderControl1.Viewport.ViewportMode = gviViewportMode.gviViewportQuadH;
                    break;
            }
            this.axRenderControl1.Viewport.CameraViewBindMask = gviViewportMask.gviViewAllNormalView;
        }

         void axRenderControl1_RcMouseDragSelect(IPickResultCollection PickResults, gviModKeyMask Mask)
        {
            

            IPickResultCollection prc = PickResults;
            if (prc == null && CTRL && Mask == gviModKeyMask.gviModKeyCtrl)
                return;

            if (!CTRL || (CTRL && Mask != gviModKeyMask.gviModKeyCtrl))  //ctrl键
            {
                this.axRenderControl1.FeatureManager.UnhighlightAll();
                featureList.Clear();
            }

            if (prc != null)
            {
                for (int i = 0; i < prc.Count; i++)
                {
                    IPickResult pr = prc.Get(i);
                    if (pr.Type == gviObjectType.gviObjectFeatureLayer)
                    {
                        IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                        int fid = flpr.FeatureId;
                        IFeatureLayer fl = flpr.FeatureLayer;
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Guid.Equals(fl.FeatureClassId))
                            {
                                this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Yellow);
                                FCAndFeature item = new FCAndFeature();
                                item.fc = fc;
                                item.fid = fid;
                                featureList.Add(item);
                            }                            
                        }                       
                    }
                }
            }
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            IPickResult pr = PickResult;
            if (pr == null && CTRL && Mask == gviModKeyMask.gviModKeyCtrl)
                return;

            if (!CTRL || (CTRL && Mask != gviModKeyMask.gviModKeyCtrl))	  //ctrl键
            {
                this.axRenderControl1.FeatureManager.UnhighlightAll();
                featureList.Clear();
            }

            if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                if (PickResult != null)
                {
                    if (pr.Type == gviObjectType.gviObjectFeatureLayer)
                    {
                        IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                        int fid = flpr.FeatureId;
                        IFeatureLayer fl = flpr.FeatureLayer;
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Guid.Equals(fl.FeatureClassId))
                            {
                                this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Yellow);
                                FCAndFeature item = new FCAndFeature();
                                item.fc = fc;
                                item.fid = fid;
                                featureList.Add(item);
                            }
                        }
                    }
                }
            }
        }

        private void btnSetFeatureVisibleMask_Click(object sender, EventArgs e)
        {
            foreach(FCAndFeature item in featureList)
            {
                this.axRenderControl1.FeatureManager.SetFeatureVisibleMask(item.fc, item.fid, getViewportMask());
            }
        }

        private void btnResetFeatureVisibleMask_Click(object sender, EventArgs e)
        {
            foreach (FCAndFeature item in featureList)
            {
                this.axRenderControl1.FeatureManager.ResetFeatureVisibleMask(item.fc, item.fid);
            }
        }

        private gviViewportMask getViewportMask()
        {
            switch (this.comboBoxVisibleMask.Text)
            {
                case "gviView0":
                    return gviViewportMask.gviView0;
                case "gviView1":
                    return gviViewportMask.gviView1;
                case "gviView2":
                    return gviViewportMask.gviView2;
                case "gviView3":
                    return gviViewportMask.gviView3;
                case "gviViewAllNormalView":
                    return gviViewportMask.gviViewAllNormalView;
                case "gviViewNone":
                    return gviViewportMask.gviViewNone;
                case "gviViewPIP":
                    return gviViewportMask.gviViewPIP;
            }
            return gviViewportMask.gviViewNone;
        }

    }

    class FCAndFeature
    {
        public IFeatureClass fc;
        public int fid;
    }
}
