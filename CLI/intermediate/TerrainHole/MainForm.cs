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

namespace TerrainHole
{
    public enum TreeNodeType
    {
        NT_DATASOURCE ,
        NT_DATASET ,
        NT_FEATURECLASS ,
        NT_SUBTYPE ,
        NT_CODEVALUE ,
        NT_IMAGELAYER ,
        NT_TERRAINLAYER,
        NT_TiltedLAYER,
        NT_KmlGroup,
        NT_TerrainModifier,
        NT_RenderGeomtry,
        NT_TerrainHole
    }

    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private System.Guid rootId = new System.Guid();

        IGeometryFactory geoFactory = new GeometryFactory();
        ICRSFactory crsFactory = new CRSFactory();
        ICoordinateReferenceSystem crs = null;
        IRenderGeometry currentRenderGeometry = null;
        IGeometry currentGeometry = null;

        int order = 0;

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

            // 注册地形
            string tmpTedPath = (strMediaPath + @"\terrain\terrain.ted");
            this.axRenderControl1.Terrain.RegisterTerrain(tmpTedPath, "");
            crs = crsFactory.CreateFromWKT(this.axRenderControl1.GetTerrainCrsWKT(tmpTedPath, ""));
            this.axRenderControl1.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
            // 添加节点到界面控件上
            myListNode item = new myListNode("terrain", TreeNodeType.NT_TERRAINLAYER, this.axRenderControl1.Terrain);
            item.Checked = true;
            listView1.Items.Add(item);

            // 注册事件
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "TerrainHole.html";
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            myListNode item = (myListNode)e.Item;

            switch (item.type)
            {
                case TreeNodeType.NT_TERRAINLAYER:
                    ITerrain ted = item.obj as ITerrain;
                    ted.VisibleMask = e.Item.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
                    break;
                case TreeNodeType.NT_TerrainHole:
                    ITerrainHole hole = item.obj as ITerrainHole;
                    hole.VisibleMask = e.Item.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
                    break;
                case TreeNodeType.NT_RenderGeomtry:
                    IRenderGeometry geo = item.obj as IRenderGeometry;
                    geo.VisibleMask = e.Item.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
                    break;
            }
        }
            
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0) return;
            myListNode item = (myListNode)this.listView1.SelectedItems[0];
            item.Checked = true;
            switch (item.type)
            {
                case TreeNodeType.NT_TERRAINLAYER:
                    this.axRenderControl1.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
                    break;
                case TreeNodeType.NT_TerrainHole:
                    ITerrainHole hole = item.obj as ITerrainHole;
                    hole.Highlight(System.Drawing.Color.Yellow);
                    this.axRenderControl1.Camera.FlyToObject(hole.Guid, gviActionCode.gviActionFlyTo);
                    break;
                case TreeNodeType.NT_RenderGeomtry:
                    IRenderGeometry geo = item.obj as IRenderGeometry;
                    this.axRenderControl1.Camera.FlyToObject(geo.Guid, gviActionCode.gviActionFlyTo);
                    break;
            }
        }

        private void toolStripButtonLoadTerrain_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "地形文件(*.ted)|*.ted";
            if(System.IO.Directory.Exists(strMediaPath))
            {
                od.InitialDirectory = strMediaPath + @"\terrain";
            }
            od.RestoreDirectory = true;
            if (DialogResult.OK == od.ShowDialog())
            {
                string wkt = this.axRenderControl1.GetTerrainCrsWKT(od.FileName, "");
                this.axRenderControl1.Reset2(wkt);
                this.axRenderControl1.Terrain.RegisterTerrain(od.FileName, "");
                if (this.axRenderControl1.Terrain.IsPlanarTerrain)
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
                this.axRenderControl1.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
                // 添加节点到界面控件上
                myListNode item = new myListNode(od.FileName, TreeNodeType.NT_TERRAINLAYER, this.axRenderControl1.Terrain);
                item.Checked = true;
                listView1.Items.Add(item);
            }
        }

        #region 创建地形编辑
        private void toolStripButtonCreateTerrainHole_Click(object sender, System.EventArgs e)
        {
            TerrainHoleSettingForm form = new TerrainHoleSettingForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                order = form.Order;

                currentGeometry = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                currentGeometry.SpatialCRS = crs as ISpatialCRS;

                ISurfaceSymbol sfbottom = new SurfaceSymbol();
                sfbottom.Color = System.Drawing.Color.Red;
                currentRenderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPolygon(currentGeometry as IPolygon, sfbottom, rootId);
                (currentRenderGeometry as IRenderPolygon).HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(currentRenderGeometry, gviGeoEditType.gviGeoEditCreator);
            }
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            ITerrainHole hole = this.axRenderControl1.ObjectManager.CreateTerrainHole(currentGeometry as IPolygon, rootId);
            if (hole != null)
            {
                hole.DrawOrder = order;

                // 添加节点到界面控件上
                myListNode item = new myListNode(string.Format("TerrainHole_{0}", hole.Guid), TreeNodeType.NT_TerrainHole, hole);
                item.Checked = true;
                listView1.Items.Add(item);

                // 添加节点到界面控件上
                item = new myListNode(string.Format("RenderPolygon_{0}", hole.Guid), TreeNodeType.NT_RenderGeomtry, currentRenderGeometry);
                item.Checked = true;
                listView1.Items.Add(item);
            }

            // 恢复漫游模式
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            currentGeometry = Geometry;
        }
        #endregion

        #region 右键弹出菜单
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listView1.GetItemAt(e.X, e.Y) == null)
            {
                属性ToolStripMenuItem.Enabled = false;
                删除ToolStripMenuItem.Enabled = false;
                生成地形挖洞ToolStripMenuItem.Enabled = false;
                return;
            }                
            if (e.Button == MouseButtons.Right)
            {
                myListNode selectNode = this.listView1.GetItemAt(e.X, e.Y) as myListNode;                
                if (selectNode.type == TreeNodeType.NT_TerrainHole)
                {
                    属性ToolStripMenuItem.Enabled = true;
                    删除ToolStripMenuItem.Enabled = true;
                    生成地形挖洞ToolStripMenuItem.Enabled = false;
                }
                else if (selectNode.type == TreeNodeType.NT_RenderGeomtry)
                {
                    属性ToolStripMenuItem.Enabled = false;
                    删除ToolStripMenuItem.Enabled = true;
                    生成地形挖洞ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    属性ToolStripMenuItem.Enabled = false;
                    删除ToolStripMenuItem.Enabled = false;
                    生成地形挖洞ToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void 属性ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            myListNode selectNode = this.listView1.SelectedItems[0] as myListNode;
            if (selectNode != null)
            {
                ITerrainHole hole = selectNode.obj as ITerrainHole;
                if (hole == null)
                    return;
                int index = 0;
                TerrainHoleSettingForm form = new TerrainHoleSettingForm(hole.DrawOrder, index);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    order = form.Order;
                    hole.DrawOrder = order;
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            myListNode selectNode = this.listView1.SelectedItems[0] as myListNode;
            if (selectNode != null)
            {
                IRenderable rgeo = selectNode.obj as IRenderable;
                this.axRenderControl1.ObjectManager.DeleteObject(rgeo.Guid);
                this.listView1.Items.Remove(selectNode);
            }
        }

        private void 生成地形挖洞ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            myListNode selectNode = this.listView1.SelectedItems[0] as myListNode;
            if (selectNode != null)
            {
                IRenderPolygon rgeo = selectNode.obj as IRenderPolygon;
                IPolygon polygon = rgeo.GetFdeGeometry() as IPolygon;
                
                // 生成带洞polygon，可注释掉
                IEnvelope env = rgeo.Envelope;
                IRing ring = (new GeometryFactory()).CreateGeometry(gviGeometryType.gviGeometryRing, gviVertexAttribute.gviVertexAttributeZ) as IRing;
                IPoint center = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                center.Position = env.Center;
                center.SpatialCRS = crs as ISpatialCRS;
                ring.AppendPoint(center);
                center.Y = env.Center.Y - 50;
                ring.AppendPoint(center);
                center.X = env.Center.X + 50;
                ring.AppendPoint(center);
                center.Y = env.Center.Y;
                ring.AppendPoint(center);
                polygon.AddInteriorRing(ring);
                // To here

                ISurfaceSymbol sfbottom = new SurfaceSymbol();
                sfbottom.Color = System.Drawing.Color.Red;
                IRenderPolygon rgeoNew = this.axRenderControl1.ObjectManager.CreateRenderPolygon(polygon, sfbottom, rootId);

                TerrainHoleSettingForm form = new TerrainHoleSettingForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    order = form.Order;

                    ITerrainHole hole = this.axRenderControl1.ObjectManager.CreateTerrainHole(polygon, rootId);
                    if (hole != null)
                    {
                        hole.DrawOrder = order;

                        // 添加节点到界面控件上
                        myListNode item = new myListNode(string.Format("TerrainHole_{0}", hole.Guid), TreeNodeType.NT_TerrainHole, hole);
                        item.Checked = true;
                        listView1.Items.Add(item);

                        // 添加节点到界面控件上
                        item = new myListNode(string.Format("RenderPolygon_{0}", hole.Guid), TreeNodeType.NT_RenderGeomtry, rgeoNew);
                        item.Checked = true;
                        listView1.Items.Add(item);
                    }
                }
            }
        }
        #endregion
    }

    class myListNode : ListViewItem
    {
        public string name;
        public TreeNodeType type;
        public IRObject obj;

        public myListNode(string n, TreeNodeType t, IRObject o)
        {
            name = n;
            type = t;
            obj = o;
            this.Text = n;
        }
    }
}
