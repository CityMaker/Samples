using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Resource;
using System.Collections;
using Gvitech.CityMaker.Controls;


namespace GeometryConvert3
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private IModel ModelSrc = null;
        private IMatrix MatrixSrc = null;
        private IModelPoint ModelPointSrc = null;
        private IRenderModelPoint RenderModelPointSrc = null;

        private IGeometryFactory _geoFactory = null;
        private IObjectEditor _geoEditor = null;
        private IMultiPolygon _multiPolygon = null;

        IPolygon fde_polygon = null;
        IRenderPolygon rpolygon = null;
        List<IPolygon> polygonList = new List<IPolygon>();
        List<Guid> guidToDelList = new List<Guid>();

        IGeometryConvertor _gc = null;
        IModel model = null;
        IModelPoint mp = null;
        IRenderModelPoint rmp = null;

        IModel modelInterior = null;
        IModelPoint mpInterior = null;
        IRenderModelPoint rmpInterior = null;
        IModel modelExterior = null;
        IModelPoint mpExterior = null;
        IRenderModelPoint rmpExterior = null;

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

            
            if(_geoFactory == null)
                _geoFactory = new GeometryFactory();
            if (_gc == null)
                _gc = new GeometryConvertor();

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

            #region 创建地面模型
            Array imgNames = null;
            try
            {
                string osgPath = (strMediaPath + @"\mdb+osg\Ground\00650100agc4001.osg");
                IResourceFactory resFac = new ResourceFactory();
                IPropertySet imgs = new PropertySet();
                resFac.CreateModelAndImageFromFile(osgPath, out imgs, out ModelSrc, out MatrixSrc);
                this.axRenderControl1.ObjectManager.AddModel("test", ModelSrc);

                string[] keys = imgs.GetAllKeys();
                foreach(string imgName in keys)
                {
                    IImage img = imgs.GetProperty(imgName) as IImage;
                    this.axRenderControl1.ObjectManager.AddImage(imgName, img);
                }

                ModelPointSrc = _geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                ModelPointSrc.ModelEnvelope = ModelSrc.Envelope;
                ModelPointSrc.FromMatrix(MatrixSrc);
                ModelPointSrc.ModelName = "test";

                RenderModelPointSrc = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(ModelPointSrc, null, rootId);
                this.axRenderControl1.Camera.FlyToObject(RenderModelPointSrc.Guid, gviActionCode.gviActionFlyTo);
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
            #endregion

            _geoEditor = this.axRenderControl1.ObjectEditor;
            _multiPolygon = _geoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "GeometryConvert3.html";
            }
        }

        private void btnCreatePolygon_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;

            fde_polygon = (IPolygon)_geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ);
            rpolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(fde_polygon, null, rootId);

            if (!_geoEditor.StartEditRenderGeometry(rpolygon, gviGeoEditType.gviGeoEditCreator))
            {
                MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
                return;
            }
        }
         

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            fde_polygon = Geometry as IPolygon;
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            polygonList.Add(fde_polygon.Clone() as IPolygon);
            guidToDelList.Add(rpolygon.Guid);

            this.axRenderControl1.RcObjectEditing -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish -=new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        private void btnClearPolygon_Click(object sender, EventArgs e)
        {
            polygonList.Clear();
            _multiPolygon.Clear();
            foreach (Guid g in guidToDelList)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(g);
            }

            cbHideModelPoint.Enabled = false;
            btnDelModelPoint.Enabled = false;
            cbHideExteriorModelPoint.Enabled = false;
            cbHideInteriorModelPoint.Enabled = false;
            btnDelExteriorModelPoint.Enabled = false;
            btnDelInteriorModelPoint.Enabled = false;
        }      
          
        private void cbShowSrcModelPoint_CheckedChanged(object sender, EventArgs e)
        {
            RenderModelPointSrc.VisibleMask = cbShowSrcModelPoint.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
        }

        private void btnIntersection_Click(object sender, EventArgs e)
        {
            IGeometry p = polygonList[0];
            for (int i = 0; i < polygonList.Count - 1; i++)
            {
                IPolygon piplus = polygonList[i + 1];
                ITopologicalOperator2D topoOpera = p as ITopologicalOperator2D;
                if (topoOpera == null) continue;
                p = topoOpera.Intersection2D(piplus);
            }
            if (p.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                _multiPolygon = p as IMultiPolygon;
            else if (p.GeometryType == gviGeometryType.gviGeometryPolygon)
                _multiPolygon.AddGeometry(p);
        }

        private void btnUnion_Click(object sender, EventArgs e)
        {
            IGeometry p = polygonList[0];
            for (int i = 0; i < polygonList.Count - 1; i++)
            {
                IPolygon piplus = polygonList[i + 1];
                ITopologicalOperator2D topoOpera = p as ITopologicalOperator2D;
                if (topoOpera == null) continue;
                p = topoOpera.Union2D(piplus);
            }
            if (p.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                _multiPolygon = p as IMultiPolygon;
            else if (p.GeometryType == gviGeometryType.gviGeometryPolygon)
                _multiPolygon.AddGeometry(p);
        }

        private void btnDifference_Click(object sender, EventArgs e)
        {
            IGeometry p = polygonList[0];
            for (int i = 0; i < polygonList.Count - 1; i++)
            {
                IPolygon piplus = polygonList[i + 1];
                ITopologicalOperator2D topoOpera = p as ITopologicalOperator2D;
                if (topoOpera == null) continue;
                p = topoOpera.Difference2D(piplus);
            }
            if (p.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                _multiPolygon = p as IMultiPolygon;
            else if (p.GeometryType == gviGeometryType.gviGeometryPolygon)
                _multiPolygon.AddGeometry(p);
        }

        private void btnSymmetricDifference_Click(object sender, EventArgs e)
        {
            IGeometry p = polygonList[0];
            for (int i = 0; i < polygonList.Count - 1; i++)
            {
                IPolygon piplus = polygonList[i + 1];
                ITopologicalOperator2D topoOpera = p as ITopologicalOperator2D;
                if (topoOpera == null) continue;
                p = topoOpera.SymmetricDifference2D(piplus);
            }
            if (p.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                _multiPolygon = p as IMultiPolygon;
            else if (p.GeometryType == gviGeometryType.gviGeometryPolygon)
                _multiPolygon.AddGeometry(p);
        }        

        private void btnCutModelPoint_Click(object sender, EventArgs e)
        {
            if (_multiPolygon.GeometryCount == 0)
                _multiPolygon.AddGeometry(fde_polygon);

            if(cbCutWithZ.Checked)
                _gc.CutModelPointByPolygon2DWithZ(_multiPolygon, ModelSrc, ModelPointSrc, double.Parse(numMinZ.Value.ToString()), double.Parse(numMaxZ.Value.ToString()), out model, out mp);
            else
                _gc.CutModelPointByPolygon2D(_multiPolygon, ModelSrc, ModelPointSrc, out model, out mp);
            cbShowSrcModelPoint.Checked = false;

            this.axRenderControl1.ObjectManager.AddModel(mp.ModelName, model);
            rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, null, rootId);
            guidToDelList.Add(rmp.Guid);

            cbHideModelPoint.Enabled = true;
            btnDelModelPoint.Enabled = true;
        }

        private void cbHideModelPoint_CheckedChanged(object sender, EventArgs e)
        {
            rmp.VisibleMask = cbHideModelPoint.Checked ? gviViewportMask.gviViewNone : gviViewportMask.gviViewAllNormalView;
        }

        private void btnDelModelPoint_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.ObjectManager.DeleteObject(rmp.Guid);
            guidToDelList.Remove(rmp.Guid);
            cbHideModelPoint.Enabled = false;
        }

        private void btnSplitModelPoint_Click(object sender, EventArgs e)
        {
            cbShowSrcModelPoint.Checked = false;
            
            if (_multiPolygon.GeometryCount == 0)
                _multiPolygon.AddGeometry(fde_polygon);

            _gc.SplitModelPointByPolygon2D(_multiPolygon, ModelSrc, ModelPointSrc, out modelInterior, out mpInterior, out modelExterior, out mpExterior);            

            if (modelInterior != null && mpInterior != null)
            {
                this.axRenderControl1.ObjectManager.AddModel(mpInterior.ModelName, modelInterior);
                rmpInterior = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mpInterior, null, rootId);
                guidToDelList.Add(rmpInterior.Guid);
                cbHideInteriorModelPoint.Enabled = true;
                btnDelInteriorModelPoint.Enabled = true;
            }

            if (modelExterior != null && mpExterior != null)
            {
                this.axRenderControl1.ObjectManager.AddModel(mpExterior.ModelName, modelExterior);
                rmpExterior = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mpExterior, null, rootId);
                guidToDelList.Add(rmpExterior.Guid);
                cbHideExteriorModelPoint.Enabled = true;
                btnDelExteriorModelPoint.Enabled = true;
            }
        }

        private void cbHideInteriorModelPoint_CheckedChanged(object sender, EventArgs e)
        {
            rmpInterior.VisibleMask = cbHideInteriorModelPoint.Checked ? gviViewportMask.gviViewNone : gviViewportMask.gviViewAllNormalView;
        }

        private void cbHideExteriorModelPoint_CheckedChanged(object sender, EventArgs e)
        {
            rmpExterior.VisibleMask = cbHideExteriorModelPoint.Checked ? gviViewportMask.gviViewNone : gviViewportMask.gviViewAllNormalView;
        }

        private void btnDelInteriorModelPoint_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.ObjectManager.DeleteObject(rmpInterior.Guid);
            guidToDelList.Remove(rmpInterior.Guid);
            cbHideInteriorModelPoint.Enabled = false;
        }

        private void btnDelExteriorModelPoint_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.ObjectManager.DeleteObject(rmpExterior.Guid);
            guidToDelList.Remove(rmpExterior.Guid);
            cbHideExteriorModelPoint.Enabled = false;
        }

    }
}
    