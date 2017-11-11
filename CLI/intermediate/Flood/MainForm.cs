using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections.Generic;
using System.Collections;
using Gvitech.CityMaker.Controls;

namespace Flood
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        IGeometryFactory geoFactory = new GeometryFactory();
        ICRSFactory crsFactory = new CRSFactory();
        ICoordinateReferenceSystem crs = null;

        double radius;
        IPoint waterPoint = null;
        IPolygon bufPolygon = null;
        IRenderPolygon renderBufPolygon = null;
        IRenderMultiPolygon boundaryrmp = null;
        List<IRenderMultiPolygon> boundaryrmpList = new List<IRenderMultiPolygon>();

        double waterHNow = 0.0;

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


            this.btnOnProcess.Enabled = false;
            this.btnSimulate.Enabled = false;

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

         
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "Flood.html";
            }    
        }

        private void toolStripButtonLoadTerrain_Click(object sender, System.EventArgs e)
        {
            boundaryrmp.VisibleMask = gviViewportMask.gviViewNone;

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
                if (crsFactory.CreateFromWKT(wkt).IsGeographic())
                {
                    MessageBox.Show("球面不能进行水淹分析，经纬度不能计算体积，面积");
                    return;
                }
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

        /// <summary>
        /// 选择水源点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonSelectWaterSource_Click(object sender, System.EventArgs e)
        {
            if (renderBufPolygon != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderBufPolygon.Guid);
                renderBufPolygon = null;
            }
            deleteMPolygon();

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectTerrain;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            //获取水源点
            waterPoint = IntersectPoint;
            //设置参数
            this.numWaterHStart.Value = (decimal)IntersectPoint.Z;
            this.numWaterHEnd.Value = (decimal)(IntersectPoint.Z + 30);
            //获取缓冲区bufPolygon\renderBufPolygon
            radius = (double)numBufferRadius.Value;
            ITopologicalOperator2D to = waterPoint as ITopologicalOperator2D;
            bufPolygon = to.Buffer2D(radius, gviBufferStyle.gviBufferCapround) as IPolygon;
            ISurfaceSymbol sf = new SurfaceSymbol();
            sf.Color = System.Drawing.Color.Yellow;
            ICurveSymbol cs = new CurveSymbol();
            cs.Color = System.Drawing.Color.Yellow;
            sf.BoundarySymbol = cs;
            renderBufPolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(bufPolygon, sf, rootId);
            renderBufPolygon.HeightStyle = gviHeightStyle.gviHeightOnTerrain;

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.btnOnProcess.Enabled = true;
            this.btnSimulate.Enabled = true;
        }

        /// <summary>
        /// 开始分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOnProcess_Click(object sender, EventArgs e)
        {
            deleteMPolygon();

            ISurfaceSymbol sf = new SurfaceSymbol();
            sf.Color = System.Drawing.Color.Red;
            ICurveSymbol cs = new CurveSymbol();
            cs.Color = System.Drawing.Color.Red;
            sf.BoundarySymbol = cs;
            boundaryrmp = this.axRenderControl1.ObjectManager.CreateRenderMultiPolygon(DoAnalyse((double)numWaterHEnd.Value), sf, rootId);
            if(boundaryrmp != null)
                boundaryrmp.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
            this.txtWaterHNow.Text = this.numWaterHEnd.Value.ToString();
        }

        private IMultiPolygon DoAnalyse(double waterHEnd)
        {
            TerrainAnalyse ta = new TerrainAnalyse();
            ta.Processing += new _ITerrainAnalyseEvents_ProcessingEventHandler(ta_Processing);
            return ta.FindWaterSinkBoundary(bufPolygon, (double)numSampling.Value, waterHEnd);           
        }

        double[] ta_Processing(gviTerrainAnalyseOperation Operation, double[] ptArray)
        {
            List<double> ret = new List<double>();
            if (ptArray != null)
            {
                for (int i = 0; i < ptArray.Length / 2; i++)
                {
                    double x = ptArray[2 * i];
                    double y = ptArray[2 * i + 1];
                    double z = this.axRenderControl1.Terrain.GetElevation(x, y, gviGetElevationType.gviGetElevationFromDatabase);
                    ret.Add(z);
                }
            }
            return ret.ToArray();
        }

        /// <summary>
        /// 动态模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSimulate_Click(object sender, EventArgs e)
        {
            deleteMPolygon();

            waterHNow = 0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double start = (double)numWaterHStart.Value;
            double end = (double)numWaterHEnd.Value;
            double inc = (double)numWaterHInc.Value;
            if (waterHNow <= end)
            {
                //确定水深
                if (waterHNow < start)
                    waterHNow = start;
                else
                    waterHNow = waterHNow + inc;
                this.txtWaterHNow.Text = waterHNow.ToString();
                //画水面
                ISurfaceSymbol sf = new SurfaceSymbol();
                sf.Color = System.Drawing.Color.Red;
                ICurveSymbol cs = new CurveSymbol();
                cs.Color = System.Drawing.Color.Blue;
                sf.BoundarySymbol = cs;
                boundaryrmp = this.axRenderControl1.ObjectManager.CreateRenderMultiPolygon(DoAnalyse(waterHNow), sf, rootId);
                if (boundaryrmp != null)
                {
                    boundaryrmp.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                    boundaryrmpList.Add(boundaryrmp);
                }                
            }
            else
            {
                timer1.Stop();
            }
        }



        private void deleteMPolygon()
        {
            this.axRenderControl1.PauseRendering(false);
            if (boundaryrmpList.Count != 0)
            {
                foreach (IRenderMultiPolygon mp in boundaryrmpList)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(mp.Guid);
                }
            }
            if (boundaryrmp != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(boundaryrmp.Guid);
                boundaryrmp = null;
            }
            this.axRenderControl1.ResumeRendering();
        }
    }
}
