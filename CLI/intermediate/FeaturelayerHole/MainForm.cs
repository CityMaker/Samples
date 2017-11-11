using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using System;
using Gvitech.CityMaker.FdeCore;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Controls;

namespace FeaturelayerHole
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private IGeometryFactory geoFactory = new GeometryFactory();
        private ICRSFactory crsFactory = new CRSFactory();
        private ICoordinateReferenceSystem crs = null;

        private IRenderPolygon currentRenderGeometry = null;
        private IGeometry currentGeometry = null;

        private IGeometryConvertor geoConvertor = null;
        private IMultiPolygon mPolygon = null;

        private System.Guid rootId = new System.Guid();
        private List<IRenderModelPoint> lll = new List<IRenderModelPoint>();

        private IHighlightHelper helper = null;

        private double minz = 0;
        private double maxz = 0;

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

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;
            this.axRenderControl1.Camera.FlyTime = 1;

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
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);                
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

                    this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, null, null, rootId);                   

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
                        IPoint p = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        p.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                        p.Position = env.Center;
                        this.axRenderControl1.Camera.LookAt2(p, 1000, angle);
                    }
                    hasfly = true;
                }
            }

            crs = crsFactory.CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT());

            if (helper == null)
            {
                helper = this.axRenderControl1.HighlightHelper;
                helper.VisibleMask = 1;
            }

            // 注册事件
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "FeaturelayerHole.html";
            }    
        }


        private void toolStripButtonCreateHole_Click(object sender, System.EventArgs e)
        {
            currentGeometry = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            currentGeometry.SpatialCRS = crs as ISpatialCRS;

            ISurfaceSymbol sfbottom = new SurfaceSymbol();
            sfbottom.Color = System.Drawing.Color.Red;
            currentRenderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPolygon(currentGeometry as IPolygon, sfbottom, rootId);
            currentRenderGeometry.ViewingDistance = 200;
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(currentRenderGeometry, gviGeoEditType.gviGeoEditCreator);
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            SearchModelPoint(currentGeometry as IPolygon);
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;            
            currentRenderGeometry.VisibleMask = gviViewportMask.gviViewNone;
            helper.SetRegion(null);
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            currentGeometry = Geometry;
            helper.SetRegion(currentGeometry);
        }

        public void SearchModelPoint(IPolygon tarPolygon)
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();

            IRowBuffer row = null;
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                if (!fc.Name.Equals("Building"))
                    continue;

                IFdeCursor cursor = null;
                try
                {
                    ISpatialFilter filter = new SpatialFilter();
                    filter.Geometry = tarPolygon;
                    filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;   //用不相离查不出来
                    filter.GeometryField = "Geometry";
                    cursor = fc.Search(filter, false);
                    while ((row = cursor.NextRow()) != null)
                    {
                        int oid = (int)row.GetValue(0);
                        this.axRenderControl1.FeatureManager.SetFeatureVisibleMask(fc, oid, gviViewportMask.gviViewNone);

                        int geoPos = row.FieldIndex("Geometry");
                        IModelPoint mp = row.GetValue(geoPos) as IModelPoint;
                        if (geoConvertor == null)
                            geoConvertor = new GeometryConvertor();
                        if (mPolygon == null)
                        {
                            mPolygon = (new GeometryFactory()).CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
                        }
                        mPolygon.Clear();
                        mPolygon.AddGeometry(tarPolygon);

                        GetZToolStripTextBox();

                        IResourceManager rm = fc.FeatureDataSet as IResourceManager;
                        IModel model = rm.GetModel(mp.ModelName);
                        IModel modelInterior = null;
                        IModelPoint mpInterior = null;
                        IModel modelExterior = null;
                        IModelPoint mpExterior = null;
                        if(minz == maxz)
                            geoConvertor.SplitModelPointByPolygon2D(mPolygon, model, mp, out modelInterior, out mpInterior, out modelExterior, out mpExterior);
                        else
                            geoConvertor.SplitModelPointByPolygon2DWithZ(mPolygon, model, mp, minz, maxz, out modelInterior, out mpInterior, out modelExterior, out mpExterior);

                        if (modelExterior != null)
                        {
                            this.axRenderControl1.ObjectManager.AddModel(fc.Name + oid + "Exterior", modelExterior);
                            mpExterior.ModelName = fc.Name + oid + "Exterior";

                            string[] imagenames = modelExterior.GetImageNames();
                            for (int j = 0; j < imagenames.Length; j++)
                            {
                                IImage image = rm.GetImage(imagenames[j]);
                                this.axRenderControl1.ObjectManager.AddImage(imagenames[j], image);
                            }
                            IRenderModelPoint rrrr = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mpExterior, null, rootId);
                            lll.Add(rrrr);   
                        }                        
                    }
                }
                catch (COMException ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
                finally
                {
                    if (cursor != null)
                    {
                        //Marshal.ReleaseComObject(cursor);
                        cursor = null;
                    }
                }
            } // end of foreach (IFeatureClass fc in fcMap.Keys)
        }

        private void toolStripButtonDeleteHole_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.FeatureManager.ResetAllVisibleMask();
            
            if (this.currentRenderGeometry != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(currentRenderGeometry.Guid);
                this.currentRenderGeometry = null;
            }

            for (int i = 0; i < lll.Count; i++)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(lll[i].Guid);
            }
            lll.Clear();
        }

        private void GetZToolStripTextBox()
        {
            try
            {
                minz = double.Parse(this.toolStripTextBoxMinZ.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请输入正确的MinZ");
            }

            try
            {
                maxz = double.Parse(this.toolStripTextBoxMaxZ.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请输入正确的MaxZ");
            }

            if(minz > maxz)
                MessageBox.Show("minz > maxz, 请输入正确的值");
        }

    }
}
