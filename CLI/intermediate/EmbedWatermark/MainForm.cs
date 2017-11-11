using System;
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
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;

namespace EmbedWatermark
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        ArrayList rPolylinelist = new ArrayList();  //拾取时DrawEnvelope产生的RenderPolyline

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

                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                    fc, geoName, null, null, rootId);

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
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }

            // 注册控件拾取事件
            this.axRenderControl1.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "EmbedWatermark.html";
            }    
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            try
            {

                IPickResult pr = PickResult;
                if (pr == null)
                    return;

                this.axRenderControl1.FeatureManager.UnhighlightAll();

                if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
                {
                    if (pr.Type == gviObjectType.gviObjectFeatureLayer)
                    {
                        IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                        int fid = flpr.FeatureId;
                        IFeatureLayer fl = flpr.FeatureLayer;
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Guid.Equals(fl.FeatureClassId))
                            {
                                //删除画包围框的RenderPolyline
                                for (int i = 0; i < rPolylinelist.Count; i++)
                                    this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
                                rPolylinelist.Clear();
                                DrawEnvelope(fid, fc, out rPolylinelist);

                                // 选择用作水印的图片
                                IImage waterImg = new ResourceFactory().CreateImageFromFile(this.toolStripTextBoxPicturePath.Text);
                                if (waterImg == null)
                                {
                                    MessageBox.Show("水印图片对象为空，请先更换图片");
                                    return;
                                }

                                DialogResult result = MessageBox.Show("加水印操作不可逆，是否继续进行", "警告", MessageBoxButtons.YesNo);
                                if (result == DialogResult.No)
                                    return;

                                // 准备Model对象
                                string filterString = string.Format("oid={0}", fid);
                                IQueryFilter filter = new QueryFilter();
                                filter.WhereClause = filterString;
                                IFdeCursor cursor = null;
                                try
                                {
                                    cursor = fc.Search(filter, true);
                                    if (cursor != null)
                                    {
                                        IRowBuffer fdeRow = null;
                                        while ((fdeRow = cursor.NextRow()) != null)
                                        {
                                            IFieldInfoCollection col = fdeRow.Fields;
                                            for (int i = 0; i < col.Count; ++i)
                                            {
                                                IFieldInfo info = col.Get(i);
                                                if (info.GeometryDef != null &&
                                                    info.GeometryDef.GeometryColumnType == gviGeometryColumnType.gviGeometryColumnModelPoint)
                                                {
                                                    int nPos = fdeRow.FieldIndex(info.Name);
                                                    IModelPoint mp = fdeRow.GetValue(nPos) as IModelPoint;
                                                    // 高亮选定模型边框
                                                    //DrawEnvelope(mp.Envelope, fc.FeatureDataSet.SpatialReference as ISpatialCRS);

                                                    IResourceManager rm = fc.FeatureDataSet as IResourceManager;
                                                    IModel model = rm.GetModel(mp.ModelName);
                                                    // 加上水印
                                                    string[] imagenames = model.GetImageNames();
                                                    for (int j = 0; j < imagenames.Length; j++)
                                                    {
                                                        this.Text = string.Format("正在处理第{0}个图片/共{1}个", j, imagenames.Length);
                                                        IImage image = rm.GetImage(imagenames[j]);
                                                        image.EmbedWatermark(waterImg);
                                                        rm.UpdateImage(imagenames[j], image);
                                                    }
                                                    this.axRenderControl1.FeatureManager.RefreshFeatureClass(fc);
                                                    this.Text = "处理完成!";
                                                }
                                            }
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
                                } //end try
                            } // end " if (fc.Guid.Equals(fl.FeatureClassId))"
                        } // end "foreach (IFeatureClass fc in fcMap.Keys)"
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
                    MessageBox.Show("需要标准runtime授权");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButtonNormal_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        private void toolStripButtonChangePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "所有文件(*.*)|*.*|bmp文件(*.bmp)|*.bmp";
            od.InitialDirectory = strMediaPath + @"\bmp";
            od.RestoreDirectory = true;
            if (DialogResult.OK == od.ShowDialog())
            {
                this.toolStripTextBoxPicturePath.Text = od.FileName.Trim();
            }
        }

        #region 自定义方法
        private void DrawEnvelope(int fid, IFeatureClass fc, out ArrayList rPolylineList)
        {
            rPolylineList = new ArrayList();

            string filterString = string.Format("oid={0}", fid);
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = filterString;
            IFdeCursor cursor = null;
            try
            {
                cursor = fc.Search(filter, true);
                if (cursor != null)
                {
                    IRowBuffer fdeRow = null;
                    while ((fdeRow = cursor.NextRow()) != null)
                    {
                        IFieldInfoCollection col = fdeRow.Fields;
                        for (int i = 0; i < col.Count; ++i)
                        {
                            IFieldInfo info = col.Get(i);
                            if (info.GeometryDef != null &&
                                info.GeometryDef.GeometryColumnType == gviGeometryColumnType.gviGeometryColumnModelPoint)
                            {
                                int nPos = fdeRow.FieldIndex(info.Name);
                                IModelPoint mp = fdeRow.GetValue(nPos) as IModelPoint;
                                IEnvelope env = mp.Envelope;

                                IPolyline polyline = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                                polyline.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                                IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                point.SpatialCRS = fc.FeatureDataSet.SpatialReference;

                                ISimplePointSymbol psy = new SimplePointSymbol();
                                psy.FillColor = System.Drawing.Color.Yellow;
                                psy.Size = 10;
                                ICurveSymbol cSymbol = new CurveSymbol();
                                cSymbol.Color = System.Drawing.Color.Yellow;
                                cSymbol.Width = 2;

                                point.SetCoords(env.MinX, env.MinY, env.MinZ, 0, 0);  //0                                             
                                //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
                                polyline.AppendPoint(point);

                                point.SetCoords(env.MaxX, env.MinY, env.MinZ, 0, 1);  //1
                                //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
                                polyline.AppendPoint(point);

                                point.SetCoords(env.MaxX, env.MaxY, env.MinZ, 0, 2);   //2
                                //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
                                polyline.AppendPoint(point);

                                point.SetCoords(env.MinX, env.MaxY, env.MinZ, 0, 3);   //3
                                //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
                                polyline.AppendPoint(point);

                                point.SetCoords(env.MinX, env.MinY, env.MinZ, 0, 4);  //0  
                                polyline.AppendPoint(point);  //close                                                   
                                rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

                                polyline.SetEmpty();
                                point.SetCoords(env.MinX, env.MaxY, env.MaxZ, 0, 0);  //4
                                //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
                                polyline.AppendPoint(point);

                                point.SetCoords(env.MaxX, env.MaxY, env.MaxZ, 0, 0);  //5
                                //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
                                polyline.AppendPoint(point);

                                point.SetCoords(env.MaxX, env.MinY, env.MaxZ, 0, 0);  //6
                                //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
                                polyline.AppendPoint(point);

                                point.SetCoords(env.MinX, env.MinY, env.MaxZ, 0, 0);  //7
                                //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
                                polyline.AppendPoint(point);

                                point.SetCoords(env.MinX, env.MaxY, env.MaxZ, 0, 0);  //4
                                polyline.AppendPoint(point);  //close
                                rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

                                polyline.SetEmpty();
                                point.SetCoords(env.MinX, env.MinY, env.MinZ, 0, 0);  //0  
                                polyline.AppendPoint(point);
                                point.SetCoords(env.MinX, env.MinY, env.MaxZ, 0, 0);  //7
                                polyline.AppendPoint(point);
                                rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

                                polyline.SetEmpty();
                                point.SetCoords(env.MaxX, env.MinY, env.MinZ, 0, 0);  //1 
                                polyline.AppendPoint(point);
                                point.SetCoords(env.MaxX, env.MinY, env.MaxZ, 0, 0);  //6
                                polyline.AppendPoint(point);
                                rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

                                polyline.SetEmpty();
                                point.SetCoords(env.MaxX, env.MaxY, env.MinZ, 0, 0);   //2
                                polyline.AppendPoint(point);
                                point.SetCoords(env.MaxX, env.MaxY, env.MaxZ, 0, 0);  //5
                                polyline.AppendPoint(point);
                                rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

                                polyline.SetEmpty();
                                point.SetCoords(env.MinX, env.MaxY, env.MinZ, 0, 0);   //3
                                polyline.AppendPoint(point);
                                point.SetCoords(env.MinX, env.MaxY, env.MaxZ, 0, 0);  //4
                                polyline.AppendPoint(point);
                                rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));
                            }
                        }
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
        }

        private void DrawEnvelope(IEnvelope env, ISpatialCRS crs, out ArrayList rPolylineList)
        {
            rPolylineList = new ArrayList();

            IPolyline polyline = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            polyline.SpatialCRS = crs;
            IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            point.SpatialCRS = crs;

            ISimplePointSymbol psy = new SimplePointSymbol();
            psy.FillColor = System.Drawing.Color.Yellow;
            psy.Size = 10;
            ICurveSymbol cSymbol = new CurveSymbol();
            cSymbol.Color = System.Drawing.Color.Yellow;
            cSymbol.Width = 2;

            point.SetCoords(env.MinX, env.MinY, env.MinZ, 0, 0);  //0                                             
            //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
            polyline.AppendPoint(point);

            point.SetCoords(env.MaxX, env.MinY, env.MinZ, 0, 1);  //1
            //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
            polyline.AppendPoint(point);

            point.SetCoords(env.MaxX, env.MaxY, env.MinZ, 0, 2);   //2
            //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
            polyline.AppendPoint(point);

            point.SetCoords(env.MinX, env.MaxY, env.MinZ, 0, 3);   //3
            //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
            polyline.AppendPoint(point);

            point.SetCoords(env.MinX, env.MinY, env.MinZ, 0, 4);  //0  
            polyline.AppendPoint(point);  //close                                                   
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

            polyline.SetEmpty();
            point.SetCoords(env.MinX, env.MaxY, env.MaxZ, 0, 0);  //4
            //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
            polyline.AppendPoint(point);

            point.SetCoords(env.MaxX, env.MaxY, env.MaxZ, 0, 0);  //5
            //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
            polyline.AppendPoint(point);

            point.SetCoords(env.MaxX, env.MinY, env.MaxZ, 0, 0);  //6
            //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
            polyline.AppendPoint(point);

            point.SetCoords(env.MinX, env.MinY, env.MaxZ, 0, 0);  //7
            //this.axRenderControl1.ObjectManager.CreateRenderPoint(point, psy);
            polyline.AppendPoint(point);

            point.SetCoords(env.MinX, env.MaxY, env.MaxZ, 0, 0);  //4
            polyline.AppendPoint(point);  //close
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

            polyline.SetEmpty();
            point.SetCoords(env.MinX, env.MinY, env.MinZ, 0, 0);  //0  
            polyline.AppendPoint(point);
            point.SetCoords(env.MinX, env.MinY, env.MaxZ, 0, 0);  //7
            polyline.AppendPoint(point);
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

            polyline.SetEmpty();
            point.SetCoords(env.MaxX, env.MinY, env.MinZ, 0, 0);  //1 
            polyline.AppendPoint(point);
            point.SetCoords(env.MaxX, env.MinY, env.MaxZ, 0, 0);  //6
            polyline.AppendPoint(point);
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

            polyline.SetEmpty();
            point.SetCoords(env.MaxX, env.MaxY, env.MinZ, 0, 0);   //2
            polyline.AppendPoint(point);
            point.SetCoords(env.MaxX, env.MaxY, env.MaxZ, 0, 0);  //5
            polyline.AppendPoint(point);
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

            polyline.SetEmpty();
            point.SetCoords(env.MinX, env.MaxY, env.MinZ, 0, 0);   //3
            polyline.AppendPoint(point);
            point.SetCoords(env.MinX, env.MaxY, env.MaxZ, 0, 0);  //4
            polyline.AppendPoint(point);
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));
        }
        #endregion

    }
}
