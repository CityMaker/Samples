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


namespace IfcTree
{
    public partial class MainForm : Form
    {
        // 以下为解析xml中用到的节点属性名
        public const String ID = "ID";
        public const String NAME = "Name";

        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";        

        private TreeNode root = null; //存储树根节点  
        private IFeatureLayer layer = null;

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
            this.axRenderControl1.Initialize(true, ps);

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

            this.axRenderControl1.Camera.FlyTime = 0;

            // 加载FDB场景
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\IFC.FDB");
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
                IFeatureClass fc = dataset.OpenFeatureClass(fcnames[0]);
                IFieldInfoCollection fieldinfos = fc.GetFields();
                for (int i = 0; i < fieldinfos.Count; i++)
                {
                    IFieldInfo fieldinfo = fieldinfos.Get(i);
                    if (null == fieldinfo)
                        continue;
                    IGeometryDef geometryDef = fieldinfo.GeometryDef;
                    if (null == geometryDef)
                        continue;
                    ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                    geoRender.RenderGroupField = "ParentObjectId";
                    layer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, fieldinfo.Name, null, geoRender, rootId);
                    IEnvelope env = geometryDef.Envelope;
                    if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                        env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                        continue;
                    IEulerAngle angle = new EulerAngle();
                    angle.Set(0, -20, 0);
                    this.axRenderControl1.Camera.LookAt(env.Center, 100, angle);

                    Hashtable cusData = fc.CustomData.AsHashtable();
                    IEnumerator etor = cusData.Keys.GetEnumerator();
                    etor.MoveNext();
                    string strXML = (string)cusData[etor.Current];
                    ShowIFCTree(strXML);
                    break;
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "IfcTree.html";
            }
        }


        #region 解析逻辑树xml加载到界面控件
        private void ShowIFCTree(String lc)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(lc);

                //读取DocumentElement节点
                XmlElement element = xmlDoc.DocumentElement;
                root = AddNode(element, this.treeView1.Nodes);

                //遍历所有子节点
                LoadXml2TreeList(xmlDoc.DocumentElement.ChildNodes, root.Nodes);

                this.treeView1.ExpandAll();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 从xml导入逻辑图层树
        /// </summary>
        private int LoadXml2TreeList(XmlNodeList list, TreeNodeCollection pNodes)
        {
            int nCount = 0;
            foreach (XmlNode nd in list)
            {
                if (nd.HasChildNodes)
                {
                    TreeNode node = AddNode(nd, pNodes);

                    layer.SetGroupVisibleMask((int)node.Tag, gviViewportMask.gviViewAllNormalView);

                    int childNodeCount = LoadXml2TreeList(nd.ChildNodes, node.Nodes);
                    node.Text = node.Text + "(" + childNodeCount + ")";
                }
                else
                {
                    //叶子节点不上树
                    nCount++;
                }
            }
            return nCount;
        }

        private TreeNode AddNode(XmlNode xmlNode, TreeNodeCollection col)
        {
            int id = 0;
            String name = null;
            if (!int.TryParse(xmlNode.Attributes[ID].Value, out id))
            {
                return null;
            }
            name = xmlNode.Attributes[NAME].Value;

            TreeNode node = new TreeNode(name, 0, 0);
            node.Tag = id;
            node.Checked = true;
            col.Add(node);

            return node;
        }

        #endregion

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                TravelTreeNode(e.Node, e.Node.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone);
            }
        }

        private void TravelTreeNode(TreeNode node, gviViewportMask mask)
        {
            if (mask == gviViewportMask.gviViewAllNormalView)
                node.Checked = true;
            else
                node.Checked = false;

            if (node.Nodes.Count == 0)
            {
                layer.SetGroupVisibleMask((int)node.Tag, mask);                
                return;
            }

            foreach (TreeNode n in node.Nodes)
            {
                TravelTreeNode(n, mask);
            }

            layer.SetGroupVisibleMask((int)node.Tag, mask);   
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode nnn = this.treeView1.GetNodeAt(e.X, e.Y);
            if (nnn == null)
                UnHighlightAll(root);
            else
            {
                //判断的目的在于：当选中复选框时仅仅控制显示隐藏，而不改变颜色
                if (nnn.Bounds.Contains(e.X, e.Y))
                {
                    UnHighlightAll(root);
                    TravelTreeNode(nnn);
                }
            }
        }

        private void TravelTreeNode(TreeNode node)
        {
            if (node.Nodes.Count == 0)
            {
                if (!layer.GetEnableGroupColor((int)node.Tag))
                    layer.SetEnableGroupColor((int)node.Tag, true);
                layer.SetGroupColor((int)node.Tag, System.Drawing.Color.Red);
                return;
            }

            foreach (TreeNode n in node.Nodes)
            {
                TravelTreeNode(n);
            }

            if (!layer.GetEnableGroupColor((int)node.Tag))
                layer.SetEnableGroupColor((int)node.Tag, true);
            layer.SetGroupColor((int)node.Tag, System.Drawing.Color.Red);
        }

        private void UnHighlightAll(TreeNode node)
        {
            if (node.Nodes.Count == 0)
            {
                layer.SetEnableGroupColor((int)node.Tag, false);
                return;
            }

            foreach (TreeNode n in node.Nodes)
            {
                UnHighlightAll(n);
            }

            layer.SetEnableGroupColor((int)node.Tag, false);
        }

    }

}
