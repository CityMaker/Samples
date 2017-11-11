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

namespace TerrainModifier
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
        NT_RenderGeomtry
    }

    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        IGeometryFactory geoFactory = new GeometryFactory();
        ICRSFactory crsFactory = new CRSFactory();
        ICoordinateReferenceSystem crs = null;
        IRenderGeometry currentRenderGeometry = null;
        IGeometry currentGeometry = null;

        int order = 0;
        gviElevationBehaviorMode mode;

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
            
            this.axRenderControl1.RcObjectEditFinish += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "TerrainModifier.html";
            }
        }


        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            myListNode item = (myListNode)e.Item;
            if (e.Item.Checked)
            {
                switch (item.type)
                {
                    case TreeNodeType.NT_TERRAINLAYER:
                        ITerrain ted = item.obj as ITerrain;
                        ted.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        break;
                    case TreeNodeType.NT_TerrainModifier:
                        ITerrainModifier modifier = item.obj as ITerrainModifier;
                        modifier.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        break;
                    case TreeNodeType.NT_RenderGeomtry:
                        IRenderGeometry geo = item.obj as IRenderGeometry;
                        geo.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        break;
                }
            }
            else
            {
                switch (item.type)
                {
                    case TreeNodeType.NT_TERRAINLAYER:
                        ITerrain ted = item.obj as ITerrain;
                        ted.VisibleMask = gviViewportMask.gviViewNone;
                        break;
                    case TreeNodeType.NT_TerrainModifier:
                        ITerrainModifier modifier = item.obj as ITerrainModifier;
                        modifier.VisibleMask = gviViewportMask.gviViewNone;
                        break;
                    case TreeNodeType.NT_RenderGeomtry:
                        IRenderGeometry geo = item.obj as IRenderGeometry;
                        geo.VisibleMask = gviViewportMask.gviViewNone;
                        break;
                }
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
                case TreeNodeType.NT_TerrainModifier:
                    ITerrainModifier modifier = item.obj as ITerrainModifier;
                    modifier.Highlight(System.Drawing.Color.Yellow);
                    this.axRenderControl1.Camera.FlyToObject(modifier.Guid, gviActionCode.gviActionFlyTo);
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
        private void toolStripButtonCreateTerrainModifier_Click(object sender, System.EventArgs e)
        {
            TerrainModifierSettingForm form = new TerrainModifierSettingForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                order = form.Order;
                switch (form.Strtype)
                {
                    case "用定义了高程的多边形替代相应地形区域中大于多边形高程部分的高程值":
                        mode = gviElevationBehaviorMode.gviElevationBehaviorAbove;
                        break;
                    case "用定义了高程的多边形替代相应地形区域中小于多边形高程部分的高程值":
                        mode = gviElevationBehaviorMode.gviElevationBehaviorBelow;
                        break;
                    case "用定义了高程的多边形替代相应的地形区域的高程值":
                        mode = gviElevationBehaviorMode.gviElevationBehaviorReplace;
                        break;
                }

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
            

            ITerrainModifier modifier = this.axRenderControl1.ObjectManager.CreateTerrainModifier(currentGeometry as IPolygon, rootId);
            if (modifier != null)
            {
                modifier.DrawOrder = order;
                modifier.ElevationBehavior = mode;

                // 添加节点到界面控件上
                myListNode item = new myListNode(string.Format("TerrainModifier_{0}", modifier.Guid), TreeNodeType.NT_TerrainModifier, modifier);
                item.Checked = true;
                listView1.Items.Add(item);

                // 添加节点到界面控件上
                item = new myListNode(string.Format("RenderPolygon_{0}", modifier.Guid), TreeNodeType.NT_RenderGeomtry, currentRenderGeometry);
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
                生成地形编辑ToolStripMenuItem.Enabled = false;
                return;
            }                
            if (e.Button == MouseButtons.Right)
            {
                myListNode selectNode = this.listView1.GetItemAt(e.X, e.Y) as myListNode;                
                if (selectNode.type == TreeNodeType.NT_TerrainModifier)
                {
                    属性ToolStripMenuItem.Enabled = true;
                    删除ToolStripMenuItem.Enabled = true;
                    生成地形编辑ToolStripMenuItem.Enabled = false;
                }
                else if (selectNode.type == TreeNodeType.NT_RenderGeomtry)
                {
                    属性ToolStripMenuItem.Enabled = false;
                    删除ToolStripMenuItem.Enabled = true;
                    生成地形编辑ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    属性ToolStripMenuItem.Enabled = false;
                    删除ToolStripMenuItem.Enabled = false;
                    生成地形编辑ToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void 属性ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            myListNode selectNode = this.listView1.SelectedItems[0] as myListNode;
            if (selectNode != null)
            {
                ITerrainModifier modifier = selectNode.obj as ITerrainModifier;
                if (modifier == null)
                    return;
                int index = 0;
                switch (modifier.ElevationBehavior)
                {
                    case gviElevationBehaviorMode.gviElevationBehaviorAbove:
                        index = 2;
                        break;
                    case gviElevationBehaviorMode.gviElevationBehaviorBelow:
                        index = 1;
                        break;
                    case gviElevationBehaviorMode.gviElevationBehaviorReplace:
                        index = 0;
                        break;
                }
                TerrainModifierSettingForm form = new TerrainModifierSettingForm(modifier.DrawOrder, index);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    order = form.Order;
                    switch (form.Strtype)
                    {
                        case "用定义了高程的多边形替代相应地形区域中大于多边形高程部分的高程值":
                            mode = gviElevationBehaviorMode.gviElevationBehaviorAbove;
                            break;
                        case "用定义了高程的多边形替代相应地形区域中小于多边形高程部分的高程值":
                            mode = gviElevationBehaviorMode.gviElevationBehaviorBelow;
                            break;
                        case "用定义了高程的多边形替代相应的地形区域的高程值":
                            mode = gviElevationBehaviorMode.gviElevationBehaviorReplace;
                            break;
                    }
                    modifier.DrawOrder = order;
                    modifier.ElevationBehavior = mode;
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

        private void 生成地形编辑ToolStripMenuItem_Click(object sender, System.EventArgs e)
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
                center.Y = env.Center.Y - 10;
                ring.AppendPoint(center);
                center.X = env.Center.X + 10;
                ring.AppendPoint(center);
                center.Y = env.Center.Y;
                ring.AppendPoint(center);
                polygon.AddInteriorRing(ring);
                // To here

                ISurfaceSymbol sfbottom = new SurfaceSymbol();
                sfbottom.Color = System.Drawing.Color.Red;
                IRenderPolygon rgeoNew = this.axRenderControl1.ObjectManager.CreateRenderPolygon(polygon, sfbottom, rootId);

                TerrainModifierSettingForm form = new TerrainModifierSettingForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    order = form.Order;
                    switch (form.Strtype)
                    {
                        case "用定义了高程的多边形替代相应地形区域中大于多边形高程部分的高程值":
                            mode = gviElevationBehaviorMode.gviElevationBehaviorAbove;
                            break;
                        case "用定义了高程的多边形替代相应地形区域中小于多边形高程部分的高程值":
                            mode = gviElevationBehaviorMode.gviElevationBehaviorBelow;
                            break;
                        case "用定义了高程的多边形替代相应的地形区域的高程值":
                            mode = gviElevationBehaviorMode.gviElevationBehaviorReplace;
                            break;
                    }

                    ITerrainModifier modifier = this.axRenderControl1.ObjectManager.CreateTerrainModifier(polygon, rootId);
                    if (modifier != null)
                    {
                        modifier.DrawOrder = order;
                        modifier.ElevationBehavior = mode;

                        // 添加节点到界面控件上
                        myListNode item = new myListNode(string.Format("TerrainModifier_{0}", modifier.Guid), TreeNodeType.NT_TerrainModifier, modifier);
                        item.Checked = true;
                        listView1.Items.Add(item);

                        // 添加节点到界面控件上
                        item = new myListNode(string.Format("RenderPolygon_{0}", modifier.Guid), TreeNodeType.NT_RenderGeomtry, rgeoNew);
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
