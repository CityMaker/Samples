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

namespace FeatureSelect
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private bool CTRL = false;  //标记拾取时是否支持“ctrl”键用于多选

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

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "FeatureSelect.html";
            }    

            // 注册控件拾取事件
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.RcMouseDragSelect += new _IRenderControlEvents_RcMouseDragSelectEventHandler(axRenderControl1_RcMouseDragSelect);
            
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;

            // 设置控件默认值
            this.toolStripSelectModeSetting.SelectedIndex = 0;


            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

            this.axRenderControl1.Camera.FlyTime = 0;

            this.axRenderControl1.RcHTMLWindowMouseHover += AxRenderControl1_RcHTMLWindowMouseHover;
        }

        private void AxRenderControl1_RcHTMLWindowMouseHover(int WinId, bool IsMouseHover)
        {
            IHTMLWindow htmWnd = this.axRenderControl1 as IHTMLWindow;
            IWindowParam wp = htmWnd.GetWindowParam(WinId);
            if(IsMouseHover)
            {
                wp.OffsetX -= 50;
                wp.OffsetY -= 100;
                wp.SizeX += 100;
                wp.SizeY += 100;
            }
            else
            {
                wp.OffsetX += 50;
                wp.OffsetY += 100;
                wp.SizeX -= 100;
                wp.SizeY -= 100;
            }
            htmWnd.SetWindowParam(wp);
        }


        #region RenderControl事件
        void axRenderControl1_RcMouseDragSelect(IPickResultCollection PickResults, gviModKeyMask Mask)
        {

            IPickResultCollection prc = PickResults;
            if (prc == null && CTRL && Mask == gviModKeyMask.gviModKeyCtrl)
                return;

            if (!CTRL || (CTRL && Mask != gviModKeyMask.gviModKeyCtrl))  //ctrl键
            {
                this.axRenderControl1.FeatureManager.UnhighlightAll();
            }

            if (prc != null)
            {
                for (int i = 0; i < prc.Count; i++)
                {
                    IPickResult pr = prc.Get(i);
                    if (pr != null && pr.Type == gviObjectType.gviObjectFeatureLayer)
                    {
                        IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                        int fid = flpr.FeatureId;
                        IFeatureLayer fl = flpr.FeatureLayer;
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Guid.Equals(fl.FeatureClassId))
                            {
                                this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Yellow);
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
                            }
                        }

                        WindowParam wp = new WindowParam();
                        wp.FilePath = @"D:\05_CityMaker_DeveloperKit\trunk\SDK\Samples\JS\beginner\HTMLWindow\PropertyWindow.html";
                        wp.Position = gviHTMLWindowPosition.gviWinPosCenterParent;
                        wp.SizeX = 200;
                        wp.SizeY = 150;
                        wp.Hastitle = false;
                        wp.IsPopupWindow = false;
                        wp.UseMoveHoverEvent = true;
                        wp.HideOnClick = false;
                        wp.WinId = fid;
                        IHTMLWindow hw = this.axRenderControl1 as IHTMLWindow;
                        hw.ShowPopupWindowEx(IntersectPoint, wp, true);
                    }
                }
            }
        }
        #endregion

        private void toolStripSelectModeSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox cb = (sender as ToolStripComboBox);
            switch (cb.Text)
            {
                case "漫游不拾取":
                    {
                        this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                    }
                    break;
                case "仅点选":
                    {
                        this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                        this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                    }
                    break;
                case "仅框选":
                    {
                        this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                        this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectDrag;
                    }
                    break;
                case "点选+框选":
                    {
                        this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                        this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectDrag;
                    }
                    break;
            }
        }

        private void toolStripUnHightLight_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();
        }

        private void cbCtrlEnable_CheckedChanged(object sender, EventArgs e)
        {
            CTRL = this.cbCtrlEnable.Checked;
        }
    }
}
