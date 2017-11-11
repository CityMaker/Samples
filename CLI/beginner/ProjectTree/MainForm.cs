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
using System.Windows.Forms.VisualStyles;
using Gvitech.CityMaker.FdeGeometry;


namespace ProjectTree
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private string cepPath = "";

        private TreeNode selectedNode = null; //被选中的树节点
        private Guid selectedId = Guid.Empty;

        private System.Guid rootId = System.Guid.Empty;

        private double fireX = 15045.35, fireY = 35686.89, fireZ = 11.50;
        private IVector3 position = new Vector3();
        private IEulerAngle angle = new EulerAngle();
        private IMotionPath motionPath = null;
        private IVector3 scale = new Vector3();

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

            rootId = this.axRenderControl1.ProjectTree.RootID;

            
            if(System.IO.Directory.Exists(strMediaPath))
            {
                // 打开工程
                cepPath = (strMediaPath + @"\Package_sdk\sdk.cep");
                this.axRenderControl1.Project.Open(cepPath, false, "");
                this.axRenderControl1.Camera.FlyTime = 1;
            }
            else
            {
                MessageBox.Show("请不要随意更改SDK目录名");
                return;
            }            

            // 加载projectTree到界面控件
            IProjectTree pTree = this.axRenderControl1.ProjectTree;
            if (pTree != null)
            {
                LoopSubNode(null, pTree, rootId);
            }

            treeView1.UpdateView();

            // 绑定事件
            this.axRenderControl1.RcResPacking += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcResPackingEventHandler(axRenderControl1_RcResPacking);
            this.axRenderControl1.RcObjectEditFinish += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);

            this.axRenderControl1.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "ProjectTree.html";
            }  
        }
         



        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            if (PickResult == null)
                return;
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            if (PickResult.Type == gviObjectType.gviObjectTerrainImageLabel)
            {
                ITerrainImageLabelPickResult pr = PickResult as ITerrainImageLabelPickResult;
                pr.TerrainImageLabel.Highlight(Color.Red);
            }
        }

        private void LoopSubNode(TreeNode pNode, IProjectTree pTree, Guid guid)
        {
            Guid firstChildGuid = pTree.GetNextItem(guid, gviItemCode.gviItemCodeChild);
            if (firstChildGuid.Equals(Guid.Empty))
                return;
            AppendTreeNode(pTree, pNode, firstChildGuid);

            Guid nxGuid = pTree.GetNextItem(firstChildGuid, gviItemCode.gviItemCodeNext);
            while (!nxGuid.Equals(Guid.Empty))
            {
                AppendTreeNode(pTree, pNode, nxGuid);
                nxGuid = pTree.GetNextItem(nxGuid, gviItemCode.gviItemCodeNext);
            }
        }

        private void AppendTreeNode(IProjectTree pTree, TreeNode pParentNode, Guid nodeObjectGuid)
        {
            TreeNode myNode = null;
            int iCheckState;
            string nodeName = string.Empty;
            if (pTree.IsGroup(nodeObjectGuid))
            {
                nodeName = pTree.GetItemName(nodeObjectGuid);
                if (this.treeView1.Nodes.Count == 0 || pParentNode == null)
                {
                    TreeNode node = new TreeNode(nodeName, 0, 0);
                    node.Tag = nodeObjectGuid;
                    this.treeView1.Nodes.Add(node);

                    int count = this.treeView1.Nodes.Count;
                    myNode = this.treeView1.Nodes[count - 1];
                }
                else
                {
                    TreeNode node = new TreeNode(nodeName, 0, 0);
                    node.Tag = nodeObjectGuid;
                    pParentNode.Nodes.Add(node);

                    int count = pParentNode.Nodes.Count;
                    myNode = pParentNode.Nodes[count - 1];
                }

                LoopSubNode(myNode, pTree, nodeObjectGuid);
            }
            else
            {
                nodeName = pTree.GetItemName(nodeObjectGuid);
                if (this.treeView1.Nodes.Count == 0 || pParentNode == null)
                {
                    TreeNode node = new TreeNode(nodeName, 1, 1);
                    node.Tag = nodeObjectGuid;
                    this.treeView1.Nodes.Add(node);                    

                    int count = this.treeView1.Nodes.Count;
                    myNode = this.treeView1.Nodes[count-1];
                }
                else
                {
                    TreeNode node = new TreeNode(nodeName, 1, 1);
                    node.Tag = nodeObjectGuid;
                    pParentNode.Nodes.Add(node);

                    int count = pParentNode.Nodes.Count;
                    myNode = pParentNode.Nodes[count - 1];
                }            
            }

            iCheckState = pTree.GetVisibility(nodeObjectGuid);
            SetNodeCheckState(myNode, iCheckState);
        }

        private void SetNodeCheckState(TreeNode pNode, int checkState)
        {
            if (pNode == null)
            {
                return;
            }
            if (checkState == 1)
            {
                pNode.Checked = true;
            }
            else if (checkState == 2)
            {
                pNode.Checked = false;
            }
        }

        void treeView1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeViewHitTestInfo info = treeView1.HitTest(e.Location);
                if (info.Node != null && info.Location == TreeViewHitTestLocations.StateImage)
                {
                    TreeNode node = info.Node;
                    Guid id = (Guid)node.Tag;
                    switch (node.StateImageIndex)
                    {
                        case TriStateTreeView.STATE_UNCHECKED:
                        case TriStateTreeView.STATE_MIXED:
                            {
                                this.axRenderControl1.ProjectTree.SetVisibility(id, 1);
                            }
                            break;
                        case TriStateTreeView.STATE_CHECKED:
                            {
                                this.axRenderControl1.ProjectTree.SetVisibility(id, 0);
                            }
                            break;
                    }                    
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                TreeViewHitTestInfo info = treeView1.HitTest(e.Location);
                if (info.Node != null && info.Location != TreeViewHitTestLocations.StateImage)
                {
                    selectedNode = info.Node;
                    selectedId = (Guid)selectedNode.Tag;
                    IRObject obj = this.axRenderControl1.ObjectManager.GetObjectById(selectedId);
                    if (this.axRenderControl1.ProjectTree.IsGroup(selectedId))
                        this.contextMenuStrip1.Show(e.Location.X, e.Location.Y + 50);
                    else if(obj.Type == gviObjectType.gviObjectPresentation)
                        this.contextMenuStrip1.Show(e.Location.X, e.Location.Y + 50);
                }
            }
        }

        private void ToolStripMenuItemRename_Click(object sender, EventArgs e)
        {          
            RenameForm rnForm = new RenameForm();
            if(rnForm.ShowDialog() == DialogResult.OK)
            {                
                this.axRenderControl1.ProjectTree.RenameGroup(selectedId, rnForm.textBox1.Text);
                selectedNode.Text = rnForm.textBox1.Text;                
            }            
        }

        private void ToolStripMenuItemCreateComplexParticleEffect_Click(object sender, EventArgs e)
        {
            IComplexParticleEffect p = this.axRenderControl1.ObjectManager.CreateComplexParticleEffect(gviComplexParticleEffectType.gviComplexParticleEffectFire_3, selectedId);
            p.ScalingFactor = 5;
            p.Name = "粒子火Fire_0";

            IPoint pos = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            pos.SetCoords(fireX, fireY, fireZ, 0, 0);
            p.Position = pos;
            this.axRenderControl1.Camera.FlyToObject(p.Guid, gviActionCode.gviActionFlyTo);

            TreeNode node = new TreeNode("粒子火Fire_0", 1, 1);
            node.Tag = p.Guid;
            node.Checked = true;
            selectedNode.Nodes.Add(node);
            this.treeView1.UpdateView();

            //this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            //this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectParticleEffect;
            //this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

            //this.axRenderControl1.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);

            if (motionPath != null)
            {
                IMotionable m = p as IMotionable;
                position.Set(0, 0, 0);
                m.Bind(motionPath, position, 0, 45, 90);
                motionPath.Play();
                this.axRenderControl1.Camera.FlyToObject(p.Guid, gviActionCode.gviActionFollowBehindAndAbove);
            }
        }

        //void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        //{
        //    IPickResult pr = PickResult;
        //    if (pr == null)
        //    {
        //        return;
        //    }
        //    if (pr.Type == gviObjectType.gviObjectComplexParticleEffect)
        //    {
        //        IComplexParticleEffectPickResult cpr = PickResult as IComplexParticleEffectPickResult;
        //        IComplexParticleEffect p = cpr.ComplexParticleEffect;
        //        p.Highlight(System.Drawing.Color.Yellow);
        //    }
        //}

        private void toolStripButtonSaveProject_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.Project.SaveAs(cepPath);
        }

        private void ToolStripMenuItemCreateRenderPolyline_Click(object sender, EventArgs e)
        {
            motionPath = this.axRenderControl1.ObjectManager.CreateMotionPath(rootId);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + @"\MotionPath.xml");
            string wkt = xmlDoc.SelectSingleNode("root/WKT").InnerText;
            motionPath.CrsWKT = wkt;
            IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            point.SpatialCRS = new CRSFactory().CreateFromWKT(wkt) as ISpatialCRS;
            IPolyline line = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            line.SpatialCRS = new CRSFactory().CreateFromWKT(wkt) as ISpatialCRS;

            XmlNodeList nodes = xmlDoc.SelectNodes("root/Waypoint");
            int i = 0;
            foreach (XmlNode node in nodes)
            {
                double x = double.Parse(node.SelectSingleNode("X").InnerText);
                double y = double.Parse(node.SelectSingleNode("Y").InnerText);
                double z = double.Parse(node.SelectSingleNode("Z").InnerText);
                position.Set(x, y, z);
                point.Position = position;
                if (line.PointCount == 0)
                {
                    line.StartPoint = point;
                }
                else
                    line.AddPointAfter(i - 1, point);
                i++;

                double heading = double.Parse(node.SelectSingleNode("Heading").InnerText);
                double tilt = double.Parse(node.SelectSingleNode("Tilt").InnerText);
                double roll = double.Parse(node.SelectSingleNode("Roll").InnerText);
                double when = double.Parse(node.SelectSingleNode("When").InnerText);
                angle.Set(heading, tilt, roll);
                scale.Set(1, 1, 1);
                motionPath.AddWaypoint2(point, angle, scale, when);
            }

            IRenderPolyline rpl = this.axRenderControl1.ObjectManager.CreateRenderPolyline(line, null, selectedId);
            rpl.Name = "RenderPolyline";
            TreeNode treeNode = new TreeNode("RenderPolyline", 1, 1);
            treeNode.Tag = rpl.Guid;
            treeNode.Checked = true;
            selectedNode.Nodes.Add(treeNode);
            this.treeView1.UpdateView();
        }

        private void ToolStripMenuItemEditPolyline_Click(object sender, EventArgs e)
        {
            Guid g = this.axRenderControl1.ProjectTree.FindItem("root\\图标标签\\RenderPolyline");
            IRenderPolyline rpl = this.axRenderControl1.ObjectManager.GetObjectById(g) as IRenderPolyline;

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            IObjectEditor objEditor = this.axRenderControl1.ObjectEditor;
            objEditor.StartEditRenderGeometry(rpl, gviGeoEditType.gviGeoEditVertex);
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        private void toolStripButtonPackProject_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ulong totalSize, freeSize;
                uint fileNum;
                this.axRenderControl1.Project.PackGetInfo(dialog.SelectedPath, false, out freeSize, out totalSize, out fileNum);
                if (freeSize < totalSize)
                {
                    MessageBox.Show("磁盘空间不足");
                }
                else
                {
                    this.axRenderControl1.Project.PackResFile();
                }                
            }            
        }

        bool axRenderControl1_RcResPacking(int TotalResNo, int CurResIndex)
        {
            this.Text = "第" + CurResIndex + "个/共" + TotalResNo + "个";
            if (CurResIndex == 9999)
                this.Text = "打包结束";
            return false;
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }
    }

}
