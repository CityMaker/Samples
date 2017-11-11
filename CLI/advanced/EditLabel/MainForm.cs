using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Controls;

namespace EditLabel
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";    
        private System.Guid rootId = new System.Guid();
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEnvelope env;//加载数据时，初始化的矩形范围
        private ISpatialCRS datasetCRS = null;
        private IGeometryFactory geoFactory = null;
        private IObjectManager objManager = null;
        private ILabel currentLabel = null;        
        private int labelCount = 1;
        private int mode = 0;
        private int clickCount = 0;

        // 线程转发
        public int MainThreadId = 0;
        

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;   
            
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "EditLabel.html";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(false, ps);

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;
            this.axRenderControl1.Camera.FlyTime = 1;
            objManager = this.axRenderControl1.ObjectManager;

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
                datasetCRS = dataset.SpatialReference;
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
                        env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        geoFactory = new GeometryFactory();
                        IPoint p = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        p.SpatialCRS = datasetCRS;
                        p.Position = env.Center;
                        this.axRenderControl1.Camera.LookAt2(p, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion

            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (PickResult == null)
                return;

            if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
            {
                if (currentLabel != null)
                    currentLabel.Position = IntersectPoint;
            }
            else if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                switch (mode)
                {
                    case 1:
                        {
                            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                        }
                        break;
                    case 2:
                        {
                            if (clickCount == 0)
                            {
                                ILabelPickResult pr = PickResult as ILabelPickResult;
                                if (pr != null)
                                {
                                    currentLabel = pr.Label;
                                    clickCount++;
                                }
                            }
                            else if (clickCount == 1)
                            {
                                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                            }
                        }
                        break;
                }
            }
        }

        private void toolStripButtonCreateLabel_Click(object sender, EventArgs e)
        {
            ILabel label = objManager.CreateLabel(rootId);
            label.Text = "label" + labelCount.ToString();
            IPoint p = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            p.SetCoords(0, 0, 0, 0, 0);
            label.Position = p;
            labelCount++;

            currentLabel = label;

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove | gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer | gviMouseSelectObjectMask.gviSelectTerrain;

            mode = 1;
        }

        private void toolStripButtonEditLabel_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove | gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectLable | gviMouseSelectObjectMask.gviSelectFeatureLayer | gviMouseSelectObjectMask.gviSelectTerrain;

            string tmpCursorPath = (strMediaPath + @"\cursors\Cross.cur");
            this.axRenderControl1.MouseCursor = tmpCursorPath;

            currentLabel = null;
            mode = 2;
            clickCount = 0;
        }



    }
}
