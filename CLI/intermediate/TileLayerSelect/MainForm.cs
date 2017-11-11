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

namespace TileLayerSelect
{
    public enum TreeNodeType
    {
        NT_TiltedLAYER,
        NT_FeatureLayer
    }

    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储fc及对应的类型为polygon的空间列名  
        private Hashtable fcuidMap = null; //FeatureClassUID,FeatureClass
        private System.Guid rootId = new System.Guid();

        private ArrayList rPolylinelist = new ArrayList();  //拾取时DrawEnvelope产生的RenderPolyline   
        private IPoint fde_point = null;
        private ArrayList tableLabelList = new ArrayList();
        private ArrayList modelpointList = new ArrayList();

        private IPoint cameraPoint = null;
        private IEulerAngle cameraAngle = new EulerAngle();
        private ArrayList rPolylinelistWhole = new ArrayList();  //拾取时DrawEnvelope产生的RenderPolyline   

        private IGeometryFactory geoFactory = new GeometryFactory();
        private IRenderPolyline rpl1 = null;
        private IRenderPolyline rpl2 = null;

        private System.Byte Mode = new System.Byte();

        private double factor = 1.0;

        private ISpatialCRS _datasetCRS = null;
        private ISpatialCRS _currentCRS = null;

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

            ICRSFactory crsfac = new CRSFactory();
            _currentCRS = (crsfac.CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT())) as ISpatialCRS;

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

            // 加载瓦片图层
            string tilelayerString = (strMediaPath + @"\sdk.tdb");
            I3DTileLayer layer = this.axRenderControl1.ObjectManager.Create3DTileLayer(tilelayerString, "", rootId);
            this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
            // 添加节点到界面控件上
            myListNode item = new myListNode("tilelayer", TreeNodeType.NT_TiltedLAYER, layer);
            item.Checked = true;
            listView1.Items.Add(item);

            #region 加载FDB
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
                _datasetCRS = dataset.SpatialReference;

                //遍历FeatureClass
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                fcMap = new Hashtable(fcnames.Length);
                fcuidMap = new Hashtable(fcnames.Length);
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
                        if(geometryDef.GeometryColumnType == gviGeometryColumnType.gviGeometryColumnPolygon)
                            geoNames.Add(fieldinfo.Name);
                    }
                    fcMap.Add(fc, geoNames);
                    fcuidMap.Add(fc.Guid, fc);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
            #endregion  
          
            //CreateFeautureLayer for 矢量贴地
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                if (geoNames.Count == 0)
                    continue;

                #region 定义几何物体渲染风格
                ICurveSymbol cs = new CurveSymbol();
                cs.Color = System.Drawing.Color.Empty;  //防止贴瓦片时出现绿色竖边
                IValueMapGeometryRender geoRender = new ValueMapGeometryRender();
                {
                    IRangeRenderRule rangeRule = new RangeRenderRule();
                    rangeRule.LookUpField = "oid";
                    rangeRule.MaxValue = 1100;
                    rangeRule.MinValue = 1000;
                    rangeRule.IncludeMin = false;

                    ISurfaceSymbol geoSymbol = new SurfaceSymbol();
                    geoSymbol.Color = System.Drawing.Color.Yellow;  
                    geoSymbol.BoundarySymbol = cs;

                    IGeometryRenderScheme grs = new GeometryRenderScheme();
                    grs.AddRule(rangeRule);
                    grs.Symbol = geoSymbol;
                    geoRender.AddScheme(grs);
                }
                {
                    IRangeRenderRule rangeRule = new RangeRenderRule();
                    rangeRule.LookUpField = "oid";
                    rangeRule.MaxValue = 1200;
                    rangeRule.MinValue = 1100;
                    rangeRule.IncludeMin = false;

                    ISurfaceSymbol geoSymbol = new SurfaceSymbol();
                    geoSymbol.Color = System.Drawing.Color.Blue;  
                    geoSymbol.BoundarySymbol = cs;

                    IGeometryRenderScheme grs = new GeometryRenderScheme();
                    grs.AddRule(rangeRule);
                    grs.Symbol = geoSymbol;
                    geoRender.AddScheme(grs);
                }
                {
                    IRangeRenderRule rangeRule = new RangeRenderRule();
                    rangeRule.LookUpField = "oid";
                    rangeRule.MaxValue = 1300;
                    rangeRule.MinValue = 1200;
                    rangeRule.IncludeMin = false;

                    ISurfaceSymbol geoSymbol = new SurfaceSymbol();
                    geoSymbol.Color = System.Drawing.Color.Green;  
                    geoSymbol.BoundarySymbol = cs;

                    IGeometryRenderScheme grs = new GeometryRenderScheme();
                    grs.AddRule(rangeRule);
                    grs.Symbol = geoSymbol;
                    geoRender.AddScheme(grs);
                }
                {
                    IRangeRenderRule rangeRule = new RangeRenderRule();
                    rangeRule.LookUpField = "oid";
                    rangeRule.MaxValue = 1400;
                    rangeRule.MinValue = 1300;
                    rangeRule.IncludeMin = false;

                    ISurfaceSymbol geoSymbol = new SurfaceSymbol();
                    geoSymbol.Color = System.Drawing.Color.Goldenrod;  
                    geoSymbol.BoundarySymbol = cs;

                    IGeometryRenderScheme grs = new GeometryRenderScheme();
                    grs.AddRule(rangeRule);
                    grs.Symbol = geoSymbol;
                    geoRender.AddScheme(grs);
                }
                {
                    ISurfaceSymbol geoSymbol = new SurfaceSymbol();
                    geoSymbol.Color = System.Drawing.Color.Fuchsia;  
                    geoSymbol.BoundarySymbol = cs;

                    IGeometryRenderScheme geoSchemeOther = new GeometryRenderScheme();
                    geoSchemeOther.Symbol = geoSymbol;
                    geoRender.AddScheme(geoSchemeOther);
                }
                #endregion
                geoRender.HeightStyle = gviHeightStyle.gviHeightOnEverything;                

                IFeatureLayer fcLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoNames[0], null, geoRender, rootId);
                if (fcLayer != null)
                {
                    fcLayer.VisibleMask = gviViewportMask.gviViewNone;

                    // 添加节点到界面控件上
                    myListNode item2 = new myListNode(fc.Name + "_" + geoNames[0], TreeNodeType.NT_FeatureLayer, fcLayer);
                    item.Checked = false;
                    listView1.Items.Add(item2);                
                }
                else
                {
                    MessageBox.Show("Create FeatureLayer Failed! " + this.axRenderControl1.GetLastError().ToString());
                }
            }

            // 注册事件
            this.axRenderControl1.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            

            //设置highlight可用
            this.axRenderControl1.HighlightHelper.VisibleMask = 1;



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
                    {
                        I3DTileLayer ted = item.obj as I3DTileLayer;
                        ted.VisibleMask = e.Item.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
                    }
                    break;
                case TreeNodeType.NT_FeatureLayer:
                    {
                        IFeatureLayer layer = item.obj as IFeatureLayer;
                        layer.VisibleMask = e.Item.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
                        this.Text = layer.GeometryType.ToString() + ":" + layer.VisibleMask;
                    }
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
                    {
                        I3DTileLayer layer = item.obj as I3DTileLayer;
                        this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
                    }                   
                    break;
                case TreeNodeType.NT_FeatureLayer:
                    {
                        IFeatureLayer layer = item.obj as IFeatureLayer;
                        this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
                    }
                    break;
            }
        }

        private void toolStripButtonPan_Click(object sender, System.EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            this.Text = "当前是漫游模式";
        }

        private void toolStripButtonWithinSelect_Click(object sender, System.EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectTileLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            Mode = 1;
            this.Text = "当前是WithinSelect模式";
        }

        private void toolStripButtonIntersectSelect_Click(object sender, System.EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectTileLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            Mode = 2;
            this.Text = "当前是IntersectSelect模式";
        }


        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            //删除包围框
            for (int i = 0; i < rPolylinelist.Count; i++)
                this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
            rPolylinelist.Clear();
            for (int i = 0; i < rPolylinelistWhole.Count; i++)
                this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelistWhole[i] as IRenderPolyline).Guid);
            rPolylinelistWhole.Clear();

            for (int i = 0; i < tableLabelList.Count; i++)
                this.axRenderControl1.ObjectManager.DeleteObject((tableLabelList[i] as ITableLabel).Guid);
            tableLabelList.Clear();

            for (int i = 0; i < modelpointList.Count; i++)
                this.axRenderControl1.ObjectManager.DeleteObject((modelpointList[i] as IRenderModelPoint).Guid);
            modelpointList.Clear();

            if (rpl1 != null)
                this.axRenderControl1.ObjectManager.DeleteObject(rpl1.Guid);
            if (rpl2 != null)
                this.axRenderControl1.ObjectManager.DeleteObject(rpl2.Guid);

            this.axRenderControl1.HighlightHelper.SetRegion(null);

            if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                IPickResult pr = PickResult;
                if (pr == null)
                {                    
                    return;
                }
                if (pr.Type == gviObjectType.gviObject3DTileLayer)
                {
                    if(Mode == 1)
                    {
                        IPoint intersectPoint = IntersectPoint;
                        IRelationalOperator2D relation = intersectPoint as IRelationalOperator2D;

                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            List<string> geoNames = fcMap[fc] as List<string>;
                            if (geoNames.Count == 0)
                                continue;

                            IFdeCursor cursor = null;
                            IRowBuffer row = null;
                            List<IRowBuffer> list = new List<IRowBuffer>();
                            try
                            {
                                ISpatialFilter filter = new SpatialFilter();
                                filter.Geometry = intersectPoint;
                                filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;
                                filter.GeometryField = "Geometry";
                                cursor = fc.Search(filter, false);
                                while ((row = cursor.NextRow()) != null)
                                {
                                    list.Add(row);
                                }
                                //开始遍历
                                foreach (IRowBuffer r in list)
                                {
                                    int geometryIndex = -1;
                                    geometryIndex = r.FieldIndex(geoNames[0].ToString());
                                    if (geometryIndex != -1)
                                    {
                                        IGeometry polygon = r.GetValue(geometryIndex) as IGeometry;
                                        if (relation.Within2D(polygon))
                                        {
                                            this.axRenderControl1.HighlightHelper.SetRegion(polygon);                                          
                                        }
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
                            finally
                            {
                                if (cursor != null)
                                {
                                    //System.Runtime.InteropServices.//Marshal.ReleaseComObject(cursor);
                                    cursor = null;
                                }
                            }

                            if (Mask != gviModKeyMask.gviModKeyCtrl && Mask != gviModKeyMask.gviModKeyShift)
                                if (list.Count > 0)
                                    break;
                        }
                    }
                    else if(Mode == 2)
                    {
                        this.axRenderControl1.Camera.GetCamera2(out cameraPoint, out cameraAngle);
                        IProximityOperator disOperator = cameraPoint as IProximityOperator;
                        double length = disOperator.Distance3D(IntersectPoint);
                        //向相机方向延伸n米:n跟眼睛到交点距离有关，当距离远时n大，当距离近时n小。
                        factor = length * 0.001;

                        IPoint aimingPoint = this.axRenderControl1.Camera.GetAimingPoint2(IntersectPoint, cameraAngle, factor);
                        IPoint sourcePoint = this.axRenderControl1.Camera.GetAimingPoint2(IntersectPoint, cameraAngle, -factor);
                        IPolyline intersetPolyline = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                        intersetPolyline.SpatialCRS = _currentCRS;
                        intersetPolyline.AppendPoint(sourcePoint);
                        //瓦片焦点可能在ModelPoint内部，导致拾取不上。因此要向intersectPoint内外各拉一定距离。
                        //intersetPolyline.AppendPoint(IntersectPoint);
                        intersetPolyline.AppendPoint(aimingPoint);

                        ICurveSymbol cs = new CurveSymbol();
                        cs.Color = System.Drawing.Color.Yellow;
                        cs.Width = -5;
                        rpl1 = this.axRenderControl1.ObjectManager.CreateRenderPolyline(intersetPolyline, cs, rootId);
                        rpl1.Glow(-1);

                        //IPolyline intersetPolyline2 = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                        //intersetPolyline2.AppendPoint(cameraPoint);
                        //intersetPolyline2.AppendPoint(IntersectPoint);
                        //cs.Color = System.Drawing.Color.Yellow;
                        //rpl2 = this.axRenderControl1.ObjectManager.CreateRenderPolyline(intersetPolyline2, cs, rootId);
                        //rpl2.Glow(-1);
                        

                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            ISpatialFilter sp = new SpatialFilter();
                            sp.Geometry = intersetPolyline;
                            sp.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                            sp.GeometryField = "Geometry";
                            IFdeCursor cursor = null;
                            try
                            {
                                cursor = fc.Search(sp, false);
                                IRowBuffer row = null;
                                while ((row = cursor.NextRow()) != null)
                                {
                                    int index = row.FieldIndex("Geometry");
                                    IModelPoint mp = row.GetValue(index) as IModelPoint;
                                    //DrawEnvelope(mp.Envelope, mp.SpatialCRS, out rPolylinelist);
                                    //rPolylinelistWhole.AddRange(rPolylinelist);

                                    //创建RenderModelPoint，显示轮廓线
                                    //IModelPointSymbol mps = new ModelPointSymbol();
                                    //mps.Color = 0; //防止与瓦片同时显示时打架
                                    //mps.EnableColor = true;
                                    //mps.SetResourceDataSet(fc.FeatureDataSet);
                                    //IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, mps, rootId);
                                    //rmp.ShowOutline = true;
                                    //modelpointList.Add(rmp);

                                    //创建RenderModelPoint，禁止深度检测
                                    IModelPointSymbol mps = new ModelPointSymbol();
                                    mps.Color = System.Drawing.Color.Red;  
                                    mps.EnableColor = true;
                                    mps.EnableTexture = false;
                                    mps.SetResourceDataSet(fc.FeatureDataSet);
                                    IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, mps, rootId);
                                    rmp.DepthTestMode = gviDepthTestMode.gviDepthTestDisable; //防止与瓦片同时显示时打架
                                    modelpointList.Add(rmp);

                                    //创建TableLabel
                                    if (fde_point == null)
                                        fde_point = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                    fde_point.Position = mp.Position;
                                    fde_point.SpatialCRS = mp.SpatialCRS;
                                    tableLabelList.Add(DrawTableLabel(row, fde_point));
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
                                   //System.Runtime.InteropServices.//Marshal.ReleaseComObject(cursor);
                                   cursor = null;
                               }    
                            }
                         }                    
                    }
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

            ICurveSymbol cSymbol = new CurveSymbol();
            cSymbol.Color = System.Drawing.Color.White;
            cSymbol.Width = 2;

            point.SetCoords(env.MinX, env.MinY, env.MinZ, 0, 0);  //0                                             
            polyline.AppendPoint(point);

            point.SetCoords(env.MaxX, env.MinY, env.MinZ, 0, 1);  //1
            polyline.AppendPoint(point);

            point.SetCoords(env.MaxX, env.MaxY, env.MinZ, 0, 2);   //2
            polyline.AppendPoint(point);

            point.SetCoords(env.MinX, env.MaxY, env.MinZ, 0, 3);   //3
            polyline.AppendPoint(point);

            point.SetCoords(env.MinX, env.MinY, env.MinZ, 0, 4);  //0  
            polyline.AppendPoint(point);  //close                                                   
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, cSymbol, rootId));

            polyline.SetEmpty();
            point.SetCoords(env.MinX, env.MaxY, env.MaxZ, 0, 0);  //4
            polyline.AppendPoint(point);

            point.SetCoords(env.MaxX, env.MaxY, env.MaxZ, 0, 0);  //5
            polyline.AppendPoint(point);

            point.SetCoords(env.MaxX, env.MinY, env.MaxZ, 0, 0);  //6
            polyline.AppendPoint(point);

            point.SetCoords(env.MinX, env.MinY, env.MaxZ, 0, 0);  //7
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

        private ITableLabel DrawTableLabel(IRowBuffer row, IPoint point)
        {
            // 创建一个有3行2列的TableLabel
            ITableLabel tableLabel = this.axRenderControl1.ObjectManager.CreateTableLabel(row.FieldCount, 2, rootId);
            // 设定表头文字
            tableLabel.TitleText = "瓦片信息展示";
            IFieldInfoCollection fieldCols = row.Fields;
            for (int k = 0; k < row.FieldCount; k++)
            {
                tableLabel.SetRecord(k, 0, fieldCols.Get(k).Name);
                tableLabel.SetRecord(k, 1, row.GetValue(k).ToString());
            }

            //标牌的位置
            tableLabel.Position = point;

            // 列宽度
            tableLabel.SetColumnWidth(0, 100);
            tableLabel.SetColumnWidth(1, 100);

            // 表的边框颜色
            tableLabel.BorderColor = System.Drawing.Color.White;
            // 表的边框的宽度
            tableLabel.BorderWidth = 2;
            // 表的背景色
            tableLabel.TableBackgroundColor = System.Drawing.Color.Gray;

            // 标题背景色
            tableLabel.TitleBackgroundColor = System.Drawing.Color.Red;

            // 第一列文本样式
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = System.Drawing.Color.White;
            headerTextAttribute.OutlineColor = System.Drawing.Color.Red;
            headerTextAttribute.Font = "黑体";
            headerTextAttribute.Bold = true;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

            // 第二列文本样式
            TextAttribute contentTextAttribute = new TextAttribute();
            contentTextAttribute.TextColor = System.Drawing.Color.Black;
            contentTextAttribute.OutlineColor = System.Drawing.Color.Red;
            contentTextAttribute.Font = "黑体";
            contentTextAttribute.Bold = false;
            contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(1, contentTextAttribute);

            // 标题文本样式
            TextAttribute capitalTextAttribute = new TextAttribute();
            capitalTextAttribute.TextColor = System.Drawing.Color.White;
            capitalTextAttribute.OutlineColor = System.Drawing.Color.Gray;
            capitalTextAttribute.Font = "华文新魏";
            capitalTextAttribute.TextSize = 14;
            capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.Bold = true;
            tableLabel.TitleTextAttribute = capitalTextAttribute;

            return tableLabel;
        }

        #region 右键弹出菜单
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listView1.GetItemAt(e.X, e.Y) == null)
            {
                属性ToolStripMenuItem.Enabled = false;
                删除ToolStripMenuItem.Enabled = false;
                return;
            }                
            if (e.Button == MouseButtons.Right)
            {
                myListNode selectNode = this.listView1.GetItemAt(e.X, e.Y) as myListNode;                

            }
        }

        private void 属性ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            myListNode selectNode = this.listView1.SelectedItems[0] as myListNode;
            if (selectNode != null)
            {

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
