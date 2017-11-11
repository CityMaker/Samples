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

namespace DataIndex
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名  
        private Hashtable fcuidMap = null; //FeatureClassUID,FeatureClass
        private List<IFeatureLayer> layers = new List<IFeatureLayer>();

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
                //设置资源目录树DataSource节点
                myTreeNode catalogDsNode = new myTreeNode(System.IO.Path.GetFileNameWithoutExtension(tmpFDBPath), NodeObjectType.DataSource, ds);
                this.tv_CatalogTree.Nodes.Add(catalogDsNode);

                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);

                //设置资源目录树DataSet节点
                myTreeNode catalogFdsNode = new myTreeNode(setnames[0], NodeObjectType.DataSet, dataset);
                catalogDsNode.Nodes.Add(catalogFdsNode);

                //遍历FeatureClass
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                fcMap = new Hashtable(fcnames.Length);
                fcuidMap = new Hashtable(fcnames.Length);
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
                    fcuidMap.Add(fc.Guid, fc);
                    //设置资源目录FeatureClass节点
                    myTreeNode catalogFcNode = new myTreeNode(name, NodeObjectType.FeatureClass, fc);
                    catalogFdsNode.Nodes.Add(catalogFcNode);
                    this.tv_CatalogTree.ExpandAll();
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
                    layers.Add(featureLayer);

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

            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DataIndex.html";
            }

        }

        private void tv_CatalogTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                myTreeNode selectNode = this.tv_CatalogTree.GetNodeAt(e.X,e.Y) as myTreeNode;
                this.tv_CatalogTree.SelectedNode = selectNode;
                if (selectNode.nodeType == NodeObjectType.FeatureClass)
                {
                    cMenuCatalog.Items.Clear();
                    ToolStripMenuItem menuFeatureClassIndex = new ToolStripMenuItem("索引");
                    menuFeatureClassIndex.Click +=new EventHandler(menuFeatureClassIndex_Click);
                    cMenuCatalog.Items.Add(menuFeatureClassIndex);
                    cMenuCatalog.Show(Cursor.Position);
                }
                if (selectNode.nodeType == NodeObjectType.DataSet)
                {
                    cMenuCatalog.Items.Clear();
                    ToolStripMenuItem menuLoadData = new ToolStripMenuItem("加载数据");
                    menuLoadData.Click+=new EventHandler(menuLoadData_Click);
                    cMenuCatalog.Items.Add(menuLoadData);
                    cMenuCatalog.Show(Cursor.Position);
                }
            }
        }

        private void menuLoadData_Click(object sender, EventArgs e)
        {
            if (layers.Count > 0)
            {
                MessageBox.Show("数据已加载");
                return;
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
                    layers.Add(featureLayer);

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
        }

        private void menuFeatureClassIndex_Click(object sender, EventArgs e)
        {
            myTreeNode selectNode = this.tv_CatalogTree.SelectedNode as myTreeNode;
            IFeatureClass fc = selectNode.Tag as IFeatureClass;

            myTreeNode dsTreeNode = selectNode.Parent.Parent as myTreeNode;
            IDataSource myDs = dsTreeNode.Tag as IDataSource;
            bool hasDelFieldCap = myDs.HasCapability(gviFdbCapability.gviFdbCapDeleteField);
            bool hasMdiFieldCap = myDs.HasCapability(gviFdbCapability.gviFdbCapModifyField);
            if (layers.Count > 0)
            {
                MessageBox.Show("请先移除RenderControl中的图层");
                return;
            }

            FeatureClassIndexDlg myDlg = new FeatureClassIndexDlg(fc);
            if (myDlg.ShowDialog() == DialogResult.OK)
            {
 
            }
        }

        private void toolStripPan_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        private void toolStripSelect_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                this.axRenderControl1.FeatureManager.UnhighlightAll();
                IPickResult pr = PickResult;
                if (pr != null && pr.Type == gviObjectType.gviObjectFeatureLayer)
                {
                    IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                    IFeatureLayer fl = flpr.FeatureLayer;
                    this.axRenderControl1.FeatureManager.HighlightFeature(fcuidMap[fl.FeatureClassId] as IFeatureClass, flpr.FeatureId, System.Drawing.Color.Red);
                }
                else
                {
                    return;
                }
            }
        }

        private void tsb_ClearMap_Click(object sender, EventArgs e)
        {
            foreach (IFeatureLayer mylayer in layers)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(mylayer.Guid);
            }
            layers.Clear();
            this.axRenderControl1.FeatureManager.RefreshAll();
        }
    }

    public enum NodeObjectType
    {
        Unknown = 0,
        DataSource = 1,
        DataSet = 2,
        FeatureClass = 3,
        FeatureLayer = 4
    }

    public class myTreeNode : TreeNode
    {
        public NodeObjectType nodeType = NodeObjectType.Unknown;


        public myTreeNode(string sName,NodeObjectType type,object oTag)
        {
            this.Name = sName;
            nodeType = type;
            this.Text = sName;
            this.Tag = oTag;
        }
    }
}
