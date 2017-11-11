using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections;
using System.Drawing;
using Gvitech.CityMaker.Controls;

namespace Ocean
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private System.Guid rootId = new System.Guid();

        IGeometryFactory geoFactory = new GeometryFactory();
        ICRSFactory crsFactory = new CRSFactory();
        ICoordinateReferenceSystem crs = null;
        IRenderGeometry currentRenderGeometry = null;
        IGeometry currentGeometry = null;

        CheckBox enableOceanEffect;  //是否开启动态海水
        ToolStripControlHost host;

        // 线程转发
        public int MainThreadId = 0;
        
        

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             

            enableOceanEffect = new CheckBox();
            enableOceanEffect.Text = "是否开启海水特效";            
            enableOceanEffect.CheckedChanged += new EventHandler(enableOceanEffect_CheckedChanged);
            host = new ToolStripControlHost(enableOceanEffect);
            toolStrip1.Items.Add(host);
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

            enableOceanEffect.Checked = this.axRenderControl1.Terrain.EnableOceanEffect;

            // 注册事件
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            

            this.toolStripTextBoxWindSpeed.Text = this.axRenderControl1.Terrain.OceanWindSpeed.ToString();
            this.toolStripTextBoxWindDirection.Text = this.axRenderControl1.Terrain.OceanWindDirection.ToString();

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "Ocean.html";
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
                crs = crsFactory.CreateFromWKT(wkt);
                this.axRenderControl1.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
            }
        }

        private void toolStripButtonSetOceanRegion_Click(object sender, System.EventArgs e)
        {
            currentGeometry = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            currentGeometry.SpatialCRS = crs as ISpatialCRS;

            ISurfaceSymbol sfbottom = new SurfaceSymbol();
            sfbottom.Color = Color.YellowGreen;
            currentRenderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPolygon(currentGeometry as IPolygon, sfbottom, rootId);
            (currentRenderGeometry as IRenderPolygon).HeightStyle = gviHeightStyle.gviHeightOnTerrain;
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(currentRenderGeometry, gviGeoEditType.gviGeoEditCreator);
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            IMultiPolygon multiPolygon = currentRenderGeometry.GetFdeGeometry() as IMultiPolygon;
            if (multiPolygon == null)
            {
                multiPolygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
                IPolygon polygon = currentRenderGeometry.GetFdeGeometry() as IPolygon;
                multiPolygon.AddPolygon(polygon);
            }
            this.axRenderControl1.Terrain.SetOceanRegion(multiPolygon);
            currentRenderGeometry.VisibleMask = gviViewportMask.gviViewNone;
            currentRenderGeometry.ViewingDistance = 10000;
            this.axRenderControl1.Camera.FlyToObject(currentRenderGeometry.Guid, gviActionCode.gviActionFollowAbove);
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            currentGeometry = Geometry;
        }


        private void toolStripTextBoxDeep_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void toolStripButtonDeleteHole_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.Terrain.SetOceanRegion(null);
            if (this.currentRenderGeometry != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(currentRenderGeometry.Guid);
                this.currentRenderGeometry = null;
            }            
        }

        private void toolStripTextBoxWindSpeed_TextChanged(object sender, EventArgs e)
        {
            this.axRenderControl1.Terrain.OceanWindSpeed = Double.Parse(this.toolStripTextBoxWindSpeed.Text);
        }

        private void toolStripTextBoxWindDirection_TextChanged(object sender, EventArgs e)
        {
            this.axRenderControl1.Terrain.OceanWindDirection = Double.Parse(this.toolStripTextBoxWindDirection.Text);
        }




        void enableOceanEffect_CheckedChanged(object sender, EventArgs e)
        {
            this.axRenderControl1.Terrain.EnableOceanEffect = this.enableOceanEffect.Checked;
        }

    }
}
