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

namespace TileHole
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
        NT_TerrainHole,
        NT_TileHole
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

        private I3DTileLayer __layerSelected = null;


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

            this.axRenderControl1.Camera.FlyTime = 1;

            // 加载瓦片图层
            string tilelayerString = (strMediaPath + @"\sdk.tdb");
            I3DTileLayer layer = this.axRenderControl1.ObjectManager.Create3DTileLayer(tilelayerString, "", rootId);
            this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
            // 添加节点到界面控件上
            myListNode item = new myListNode("tilelayer", TreeNodeType.NT_TiltedLAYER, layer);
            item.Checked = true;
            listView1.Items.Add(item);
            __layerSelected = layer;

            // 注册事件
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "TileHole.html";
            }
        }
         


        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            myListNode item = (myListNode)e.Item;

            switch (item.type)
            {
                case TreeNodeType.NT_TiltedLAYER:
                    I3DTileLayer ted = item.obj as I3DTileLayer;
                    ted.VisibleMask = e.Item.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
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
                case TreeNodeType.NT_TiltedLAYER:
                    I3DTileLayer layer = item.obj as I3DTileLayer;
                    layer.Highlight(System.Drawing.Color.Yellow);
                    this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
                    break;
                case TreeNodeType.NT_RenderGeomtry:
                    IRenderGeometry geo = item.obj as IRenderGeometry;
                    this.axRenderControl1.Camera.FlyToObject(geo.Guid, gviActionCode.gviActionFlyTo);
                    break;
            }
        }



       


        private void toolStripButtonCreateTileHole_Click(object sender, System.EventArgs e)
        {
            currentGeometry = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            currentGeometry.SpatialCRS = crs as ISpatialCRS;

            ISurfaceSymbol sfbottom = new SurfaceSymbol();
            sfbottom.Color = System.Drawing.Color.Red;
            currentRenderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPolygon(currentGeometry as IPolygon, sfbottom, rootId);
            (currentRenderGeometry as IRenderPolygon).HeightStyle = gviHeightStyle.gviHeightOnEverything;
            (currentRenderGeometry as IRenderPolygon).MaxVisibleDistance = 50000;
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(currentRenderGeometry, gviGeoEditType.gviGeoEditCreator);
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            IMultiPolygon multiPolygon = (new GeometryFactory()).CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
            multiPolygon.AddGeometry(currentGeometry as IPolygon);
            int ret = __layerSelected.SetHoles(multiPolygon);
            if (ret == 0)
            {
                // 添加节点到界面控件上
                myListNode item = new myListNode("RenderPolygon", TreeNodeType.NT_RenderGeomtry, currentRenderGeometry);
                item.Checked = true;
                listView1.Items.Add(item);
            }
            else
            {
                MessageBox.Show("Tilelayer SetHoles Failed!");
                this.axRenderControl1.ObjectManager.DeleteObject(currentRenderGeometry.Guid);
            }

            // 恢复漫游模式
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            currentGeometry = Geometry;
        }

        private void toolStripButtonDeleteTilehole_Click(object sender, System.EventArgs e)
        {
            __layerSelected.SetHoles(null);
        }



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
