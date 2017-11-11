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
using Gvitech.CityMaker.FdeDataInterop;

namespace ImageryLayer
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

            // 注册地形
            string tmpTedPath = (strMediaPath + @"\terrain\world.ted");
            this.axRenderControl1.Terrain.RegisterTerrain(tmpTedPath, "");
            this.axRenderControl1.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
            // 添加节点到界面控件上
            myListNode item = new myListNode("world", TreeNodeType.NT_TERRAINLAYER, this.axRenderControl1.Terrain);
            item.Checked = true;
            listView1.Items.Add(item);

            // 创建影像图层
            string tmpImageryPath = (strMediaPath + @"\imagery\jk1.tif");
            IImageryLayer imagelayer = this.axRenderControl1.ObjectManager.CreateImageryLayer(tmpImageryPath, rootId);
            this.axRenderControl1.Camera.FlyToObject(imagelayer.Guid, gviActionCode.gviActionFlyTo);
            // 添加节点到界面控件上
            item = new myListNode("jk1", TreeNodeType.NT_IMAGELAYER, imagelayer);
            item.Checked = true;
            listView1.Items.Add(item);

            IRasterSourceFactory rasFac = new RasterSourceFactory();
            IRasterSource source = rasFac.OpenRasterSource(@"http://192.168.2.254:6163/igs/rest/ogc/WMTSServer/1.0.0/WMTSCapabilities.xml", gviRasterConnectionType.gviRasterConnectionWMTS);
            if (source != null)
            {
                string[] names = source.GetRasterNames();
                for (int n = 0; n < names.Length; n++ )
                {
                    IRaster raster = source.OpenRaster(names[n]);
                    IImageryLayer imagelayer2 = this.axRenderControl1.ObjectManager.CreateImageryLayer(raster.ConnStr, rootId);
                    // 添加节点到界面控件上
                    item = new myListNode(raster.Name, TreeNodeType.NT_IMAGELAYER, imagelayer2);
                    item.Checked = true;
                    listView1.Items.Add(item);
                }                
            }
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "ImageryLayer.html";
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
                    case TreeNodeType.NT_IMAGELAYER:
                        IImageryLayer img = item.obj as IImageryLayer;
                        img.VisibleMask = gviViewportMask.gviViewAllNormalView;
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
                    case TreeNodeType.NT_IMAGELAYER:
                        IImageryLayer img = item.obj as IImageryLayer;
                        img.VisibleMask = gviViewportMask.gviViewNone;
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
                case TreeNodeType.NT_IMAGELAYER:
                    IImageryLayer img = item.obj as IImageryLayer;
                    this.axRenderControl1.Camera.FlyToObject(img.Guid, gviActionCode.gviActionFlyTo);
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

        private void toolStripButtonAddImageryLayer_Click(object sender, System.EventArgs e)
        {
            try
            {
                OpenImageLayerForm form = new OpenImageLayerForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    gviRasterSourceType selectType = gviRasterSourceType.gviRasterUnknown;
                    if (form.Strtype.Equals("Image File"))
                        selectType = gviRasterSourceType.gviRasterSourceFile;
                    else if (form.Strtype.Equals("WMS"))
                        selectType = gviRasterSourceType.gviRasterSourceWMS;
                    else if (form.Strtype.Equals("WMTS"))
                        selectType = gviRasterSourceType.gviRasterSourceWMTS;
                    else if (form.Strtype.Equals("GeoRaster"))
                        selectType = gviRasterSourceType.gviRasterSourceGeoRaster;
                    else if (form.Strtype.Equals("MapServer"))
                        selectType = gviRasterSourceType.gviRasterSourceMapServer;
                    string cnnStr = this.axRenderControl1.ObjectManager.OpenRasterSourceDialog(selectType);
                    //cnnStr为空，取消操作，关闭窗口
                    if (!cnnStr.Equals(""))
                    {
                        IImageryLayer imagelayer = this.axRenderControl1.ObjectManager.CreateImageryLayer(cnnStr, rootId);
                        if (this.axRenderControl1.GetLastError() == 0)
                        {
                            if (imagelayer != null)
                            {
                                this.axRenderControl1.Camera.FlyToObject(imagelayer.Guid, gviActionCode.gviActionFlyTo);
                                // 添加节点到界面控件上
                                myListNode item = new myListNode(cnnStr, TreeNodeType.NT_IMAGELAYER, imagelayer);
                                item.Checked = true;
                                listView1.Items.Add(item);
                            }
                            else
                                MessageBox.Show("ImageLayer Create Failed");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
