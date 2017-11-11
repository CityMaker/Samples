using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections;
using Gvitech.CityMaker.Controls;

namespace DrawTerrainHole
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        IGeometryFactory geoFactory = new GeometryFactory();
        ICRSFactory crsFactory = new CRSFactory();
        ICoordinateReferenceSystem crs = null;
        IRenderGeometry currentRenderGeometry = null;
        IGeometry currentGeometry = null;

        ITerrainHole hole = null;
        IRenderMultiPolygon rmpolygon = null;
        IRenderPolygon rpolygon = null;

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

            // 注册事件
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DrawTerrainHole.html";
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

        private void toolStripButtonCreateTerrainHole_Click(object sender, System.EventArgs e)
        {
            currentGeometry = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            currentGeometry.SpatialCRS = crs as ISpatialCRS;

            ISurfaceSymbol sfbottom = new SurfaceSymbol();
            sfbottom.Color = System.Drawing.Color.Red;
            currentRenderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPolygon(currentGeometry as IPolygon, sfbottom, rootId);
            (currentRenderGeometry as IRenderPolygon).HeightStyle = gviHeightStyle.gviHeightOnTerrain;
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(currentRenderGeometry, gviGeoEditType.gviGeoEditCreator);
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            IPolygon pp = DiscretePolygon(currentGeometry as IPolygon, Convert.ToDouble(toolStripTextBoxDeep.Text.Trim()) / 100);
            DrawTerrainHole(pp, Convert.ToDouble(toolStripTextBoxDeep.Text.Trim()));
            currentRenderGeometry.ViewingDistance = 10000;
            this.axRenderControl1.Camera.FlyToObject(currentRenderGeometry.Guid, gviActionCode.gviActionFollowAbove);
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            currentGeometry = Geometry;
        }

        /// <summary>
        /// 离散Polygon
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="distance"></param>
        private IPolygon DiscretePolygon(IPolygon polygon, double distance)
        {
            string wkt = "PROJCS[\"<Custom Coordinate>\",GEOGCS[\"GCS_Beijing_1954\",DATUM[\"D_Beijing_1954\",SPHEROID[\"Krasovsky_1940\",6378245.0,298.3]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"false_easting\",64685.26],PARAMETER[\"false_northing\",-3267460.1405],PARAMETER[\"central_meridian\",120.0],PARAMETER[\"scale_factor\",1.0],PARAMETER[\"latitude_of_origin\",0.0],UNIT[\"Meter\",1.0]]";
            ICoordinateReferenceSystem tempcrs = crsFactory.CreateFromWKT(wkt);
            IRing ring = polygon.ExteriorRing;
            IPolygon resPolygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            resPolygon.SpatialCRS = crs as ISpatialCRS;
            for (int i = 0; i < ring.PointCount - 1; i++)
            {
                IPoint point1 = ring.GetPoint(i);
                IPoint point2 = ring.GetPoint(i + 1);
                resPolygon.ExteriorRing.AppendPoint(point1);
                point1.Project(tempcrs as ISpatialCRS);
                point2.Project(tempcrs as ISpatialCRS);
                IVector3 p1 = point1.Position;
                IVector3 p2 = point2.Position;
                IEulerAngle angle = this.axRenderControl1.Camera.GetAimingAngles(p1, p2);

                p2.MultiplyByScalar(-1);
                double length = p1.Add(p2).Length;
                for (int j = 0; j < (int)(length / distance); j++)
                {
                    IVector3 tempv3 = this.axRenderControl1.Camera.GetAimingPoint(p1, angle, (distance * (j + 1)));
                    IPoint tempPoint = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    tempPoint.Position = tempv3;
                    tempPoint.SpatialCRS = tempcrs as ISpatialCRS;
                    tempPoint.Project(crs as ISpatialCRS);
                    resPolygon.ExteriorRing.AppendPoint(tempPoint);
                }
            }
            resPolygon.Close();
            return resPolygon;
        }

        /// <summary>
        /// 挖洞
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="deep"></param>
        public void DrawTerrainHole(IPolygon polygon, double deep)
        {
            if (polygon == null) return;
            IRing ring = polygon.ExteriorRing;
            if (!ring.IsClosed) return;
            IPoint point0 = ring.GetPoint(0);
            double lowestPoint = this.axRenderControl1.Terrain.GetElevation(point0.X, point0.Y, gviGetElevationType.gviGetElevationFromDatabase);
            IPolygon pBottom = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            pBottom.SpatialCRS = crs as ISpatialCRS;
            for (int i = 0; i < ring.PointCount; i++)
            {
                IPoint point = ring.GetPoint(i);
                point.Z = this.axRenderControl1.Terrain.GetElevation(point.X, point.Y, gviGetElevationType.gviGetElevationFromDatabase);
                if (point.Z < lowestPoint)
                    lowestPoint = point.Z;
                ring.UpdatePoint(i, point);
                point = point.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                pBottom.ExteriorRing.AppendPoint(point);
            }
            //b、计算底面
            lowestPoint -= deep;
            for (int jj = 0; jj < pBottom.ExteriorRing.PointCount; jj++)
            {
                IPoint point1 = pBottom.ExteriorRing.GetPoint(jj);
                point1.Z = lowestPoint;
                pBottom.ExteriorRing.UpdatePoint(jj, point1);
            }
            ICurveSymbol cvSymbol = new CurveSymbol();
            cvSymbol.Color = System.Drawing.Color.Yellow;
            ISurfaceSymbol sfside = new SurfaceSymbol();
            sfside.BoundarySymbol = cvSymbol;
            sfside.Color = System.Drawing.Color.BurlyWood;
            ISurfaceSymbol sfbottom = new SurfaceSymbol();
            sfbottom.BoundarySymbol = cvSymbol;
            sfbottom.Color = System.Drawing.Color.Green;
            rpolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(pBottom, sfbottom, rootId);
            rpolygon.MaxVisibleDistance = 9999999;

            IMultiPolygon mpolygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
            mpolygon.SpatialCRS = crs as ISpatialCRS;
            for (int i = 0; i < ring.PointCount - 1; i++)
            {
                IPolygon pSide = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                pSide.SpatialCRS = crs as ISpatialCRS;
                pSide.ExteriorRing.AppendPoint(polygon.ExteriorRing.GetPoint(i));
                pSide.ExteriorRing.AppendPoint(pBottom.ExteriorRing.GetPoint(i));
                pSide.ExteriorRing.AppendPoint(pBottom.ExteriorRing.GetPoint(i + 1));
                pSide.ExteriorRing.AppendPoint(polygon.ExteriorRing.GetPoint(i + 1));
                pSide.Close();
                mpolygon.AddPolygon(pSide);
                //renderControl.ObjectManager.CreateRenderPolygon(pSide, sfside);
            }
            rmpolygon = this.axRenderControl1.ObjectManager.CreateRenderMultiPolygon(mpolygon, sfside, rootId);
            rmpolygon.MaxVisibleDistance = 99999999;
            hole = this.axRenderControl1.ObjectManager.CreateTerrainHole(polygon, rootId);
            currentRenderGeometry.VisibleMask = gviViewportMask.gviViewNone;
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
            if (this.hole != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(this.hole.Guid);
                this.hole = null;
            }
            if (this.rpolygon != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(this.rpolygon.Guid);
                this.rpolygon = null;
            }
            if (this.rmpolygon != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(this.rmpolygon.Guid);
                this.rmpolygon = null;
            }
            if (this.currentRenderGeometry != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(currentRenderGeometry.Guid);
                this.currentRenderGeometry = null;
            }            
        }

    }
}
