using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using System;
using Gvitech.CityMaker.Controls;

namespace Network
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap_Road = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private Hashtable fcMap_POI = null;
        private System.Guid rootId = new System.Guid();

        private IFeatureDataSet dataset_Road = null;
        private INetworkManager dsManager = null;
        private INetworkLoader dsLoader = null;
        private IEdgeNetworkSource edgeNS = null;
        private INetwork network = null;

        private INetworkRouteSolver routeSolver = null;
        private IRenderPolyline renderLine = null;
        private IRenderMultiPolyline multiRenderLine = null;
        private List<IRenderPolyline> renderLineArray = new List<IRenderPolyline>();
        private List<IRenderMultiPolyline> multiRenderLineArray = new List<IRenderMultiPolyline>();
        private List<IRenderPolyline> tmpRenderLineArray = new List<IRenderPolyline>();

        private IDynamicObject tour = null;
        private IPoint fdepoint = null;
        private IModelPoint mp = null;
        private IRenderModelPoint renderModelPoint = null;
        private ISkinnedMesh skinMesh = null;
        private IGeometryFactory geoFac = null;
        private IVector3 pos = new Vector3();
        private IEulerAngle ang = new EulerAngle();
        private IRenderPoint renderPoint = null;

        private INetworkClosestFacilitySolver closestFacilitySolver = null;

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
            this.cbOrderPolicy.SelectedIndex = 0;

            if (geoFac == null)
                geoFac = new GeometryFactory();

            #region 加载road FDB
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\Network.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                dataset_Road = ds.OpenFeatureDataset(setnames[0]);
                string[] fcnames = (string[])dataset_Road.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                fcMap_Road = new Hashtable(fcnames.Length);
                foreach (string name in fcnames)
                {
                    IFeatureClass fc = dataset_Road.OpenFeatureClass(name);
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
                    fcMap_Road.Add(fc, geoNames);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            // CreateFeautureLayer
            bool hasfly = false;
            foreach (IFeatureClass fc in fcMap_Road.Keys)
            {
                List<string> geoNames = (List<string>)fcMap_Road[fc];
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

            Thread.Sleep(2000);

            // 加载网络
            try
            {
                dsManager = dataset_Road.GetNetworkManager();
                string[] networkDatasetNames = dsManager.GetNetworkNames();
                bool hasNetworkDataset = false;
                for (int i = 0; i < networkDatasetNames.Length; i++)
                {
                    if (networkDatasetNames[i] == "newNetworkDataset")
                        hasNetworkDataset = true;
                }

                if (!hasNetworkDataset)
                {
                    dsLoader = dsManager.CreateNetworkLoader();
                    dsLoader.Name = "newNetworkDataset";
                    dsLoader.XYTolerance = double.Parse(txtLoaderTolerance.Text);
                    edgeNS = new EdgeNetworkSource();
                    edgeNS.SourceName = "road";
                    edgeNS.ConnectivityPolicy = gviNetworkEdgeConnectivityPolicy.gviEndVertex;
                    edgeNS.GeoColumnName = "Geometry";
                    edgeNS.ClassConnectivityGroup = 1;
                    dsLoader.AddSource(edgeNS);
                    INetworkAttribute attr = new NetworkAttribute();
                    attr.FieldType = gviFieldType.gviFieldDouble;
                    attr.Name = "Length";
                    attr.UsageType = gviNetworkAttributeUsageType.gviUseAsCost;
                    INetworkFieldEvaluator fieldEvaluator = new NetworkFieldEvaluator();
                    fieldEvaluator.FieldName = "Geometry";
                    attr.SetEvaluator(edgeNS, gviEdgeDirection.gviAlongDigitized, fieldEvaluator);
                    attr.SetEvaluator(edgeNS, gviEdgeDirection.gviAgainstDigitized, fieldEvaluator);
                    dsLoader.AddNetworkAttribute(attr);
                    dsLoader.LoadNetwork();
                }
                network = dsManager.GetNetwork("newNetworkDataset");

                routeSolver = network.CreateRouteSolver();
                routeSolver.ImpedanceAttributeName = "Length";
                routeSolver.LocationSearchTolerance = double.Parse(txtSearchTolerance.Text);
                ////Marshal.ReleaseComObject(dsManager);
                ////Marshal.ReleaseComObject(dsLoader);
                ////Marshal.ReleaseComObject(attr);
                ////Marshal.ReleaseComObject(edgeNS);
                ////Marshal.ReleaseComObject(fieldEvaluator);
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            #endregion

            #region 加载POI FDB
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\POI-1.FDB");
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
                fcMap_POI = new Hashtable(fcnames.Length);
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
                    fcMap_POI.Add(fc, geoNames);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            //解析配置文件
            string featurelayerName = "";
            IValueMapGeometryRender render_JingDian = new ValueMapGeometryRender();
            IValueMapGeometryRender render_MeiShi = new ValueMapGeometryRender();
            IValueMapGeometryRender render_ZhuSu = new ValueMapGeometryRender();
            IValueMapGeometryRender render_HuoDong = new ValueMapGeometryRender();
            IValueMapGeometryRender render_GouWu = new ValueMapGeometryRender();
            IValueMapGeometryRender render_ATM = new ValueMapGeometryRender();
            IValueMapGeometryRender render_WC = new ValueMapGeometryRender();

            string[] fn = Directory.GetFiles((strMediaPath + @"\xml\xml"));
            foreach (string str in fn)
            {
                IValueMapGeometryRender render = new ValueMapGeometryRender();
                try
                {
                    XmlDocument docReader = new XmlDocument();
                    docReader.Load(str);
                    if (docReader.SelectSingleNode("FeatureLayer") != null)
                    {
                        featurelayerName = docReader.SelectSingleNode("FeatureLayer").Attributes["Name"].Value;

                        XmlNode GeometryRenderNode = docReader.SelectSingleNode("FeatureLayer").ChildNodes[0];
                        switch(GeometryRenderNode.Attributes["HeightStyle"].Value)
                        {
                            case "gviHeightAbsolute":
                                render.HeightStyle = gviHeightStyle.gviHeightAbsolute;
                                break;
                            case "gviHeightOnTerrain":
                                render.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                                break;
                            case "gviHeightRelative":
                                render.HeightStyle = gviHeightStyle.gviHeightRelative;
                                break;
                        }
                        render.RenderGroupField = GeometryRenderNode.Attributes["GroupField"].Value;

                        XmlNodeList xnl = docReader.SelectSingleNode("FeatureLayer").ChildNodes[0].ChildNodes[0].ChildNodes;
                        if (xnl.Count > 0)
                        {
                            foreach (XmlNode RenderSchemeNode in xnl)
                            {
                                IGeometryRenderScheme renderScheme = new GeometryRenderScheme();

                                IUniqueValuesRenderRule uniqueRenderRule = new UniqueValuesRenderRule();
                                IImagePointSymbol imagePointSym = new ImagePointSymbol();
                                XmlNodeList renderSchemeChilds = RenderSchemeNode.ChildNodes;
                                if (renderSchemeChilds.Count > 0)
                                {
                                    XmlNode renderRuleNode = renderSchemeChilds.Item(0);
                                    uniqueRenderRule.LookUpField = renderRuleNode.Attributes["LookUpField"].Value;
                                    uniqueRenderRule.AddValue(renderRuleNode.Attributes["UniqueValue"].Value);

                                    XmlNode geometrySymbolNode = renderSchemeChilds.Item(1);
                                    imagePointSym.ImageName = geometrySymbolNode.Attributes["ImageName"].Value;
                                    //imagePointSym.Size = int.Parse(geometrySymbolNode.Attributes["Size"].Value);
                                    imagePointSym.Size = 60;
                                    imagePointSym.Alignment = gviPivotAlignment.gviPivotAlignBottomCenter;
                                }
                                renderScheme.AddRule(uniqueRenderRule);
                                renderScheme.Symbol = imagePointSym;
                                render.AddScheme(renderScheme);
                            }
                        }

                        switch (featurelayerName)
                        {
                            case "景点347":
                                render_JingDian = render;
                                break;
                            case "ATM611":
                                render_ATM = render;
                                break;
                            case "购物67":
                                render_GouWu = render;
                                break;
                            case "活动927":
                                render_HuoDong = render;
                                break;
                            case "WC968":
                                render_WC = render;
                                break;
                            case "美食563":
                                render_MeiShi = render;
                                break;
                            case "住宿513":
                                render_ZhuSu = render;
                                break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(str + " 解析失败");
                    return;
                }
            }

            ISimpleTextRender textRender = new SimpleTextRender();
            textRender.Expression = "$(Name)";
            textRender.DynamicPlacement = true;
            textRender.MinimizeOverlap = true;
            ITextSymbol textSymbol = new TextSymbol();
            TextAttribute textAttribute = new TextAttribute();
            textAttribute.TextColor = System.Drawing.Color.Blue;
            textAttribute.Font = "微软雅黑";
            textSymbol.TextAttribute = textAttribute;
            textSymbol.PivotAlignment = gviPivotAlignment.gviPivotAlignBottomCenter;
            textSymbol.VerticalOffset = 10;
            textRender.Symbol = textSymbol; 

            // CreateFeautureLayer
            foreach (IFeatureClass fc in fcMap_POI.Keys)
            {
                List<string> geoNames = (List<string>)fcMap_POI[fc];
                foreach (string geoName in geoNames)
                {
                    if (!geoName.Equals("Geometry"))
                        continue;

                    switch (fc.Name)
                    {
                        case "景点347":
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, textRender, render_JingDian, rootId);
                            break;
                        case "ATM611":
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, textRender, render_ATM, rootId);
                            break;
                        case "购物67":
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, textRender, render_GouWu, rootId);
                            break;
                        case "活动927":
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, textRender, render_HuoDong, rootId);
                            break;
                        case "WC968":
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, textRender, render_WC, rootId);
                            break;
                        case "美食563":
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, textRender, render_MeiShi, rootId);
                            break;
                        case "住宿513":
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, textRender, render_ZhuSu, rootId);
                            break;
                    }
                }
            }
            #endregion

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "Network.html";
            }    
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            IPickResult pr = PickResult;

            if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                if (PickResult != null)
                {
                    if (pr.Type == gviObjectType.gviObjectFeatureLayer)
                    {
                        IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                        int fid = flpr.FeatureId;
                        IFeatureLayer fl = flpr.FeatureLayer;
                        foreach (IFeatureClass fc in fcMap_POI.Keys)
                        {
                            if (fc.Guid.Equals(fl.FeatureClassId))
                            {
                                IRowBuffer row = fc.GetRow(fid);

                                int pos = row.FieldIndex("Geometry");
                                IPoint point = row.GetValue(pos) as IPoint;
                                try
                                {
                                    INetworkLocation location = new NetworkLocation();
                                    location.Position = point;
                                    routeSolver.AddLocation(location);
                                }
                                catch (COMException ex)
                                {
                                    MessageBox.Show("所选点距离网络太远，请调整LocationSearchTolerance大小");
                                    return;
                                }

                                pos = row.FieldIndex("Name");
                                if (row.GetValue(pos) == null)
                                    pos = 0;
                                if (txtLocationNames.Text == "")
                                    txtLocationNames.Text = row.GetValue(pos).ToString();
                                else
                                    txtLocationNames.Text = txtLocationNames.Text + Environment.NewLine + row.GetValue(pos).ToString();
                                this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Yellow);

                            }
                        }
                    }
                }
            }
        }

        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    dsManager.DeleteNetworkDataset("newNetworkDataset");
        //}

        private void btnSelectLocation_Click(object sender, System.EventArgs e)
        {
            clear();
            clearDeep();

            #region 我在这（两点间最优路径）
            if (((Button)sender).Name == "btnImHere")
            {
                this.axRenderControl1.Camera.GetCamera2(out fdepoint, out ang);
                try
                {
                    INetworkLocation location = new NetworkLocation();
                    location.Position = fdepoint;
                    routeSolver.AddLocation(location);
                }
                catch (COMException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                if (txtLocationNames.Text == "")
                    txtLocationNames.Text = "I'm Here!";
                else
                    txtLocationNames.Text = txtLocationNames.Text + Environment.NewLine + "I'm Here!";
                IImagePointSymbol ips = new ImagePointSymbol();
                ips.ImageName = "#(i)";
                ips.Size = 50;
                renderPoint = this.axRenderControl1.ObjectManager.CreateRenderPoint(fdepoint, ips, rootId);
                renderPoint.MaxVisibleDistance = 10000;
                MessageBox.Show("请选择您想去的终点");
            }
            #endregion

            // 注册控件拾取事件
            this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        private void btnBuildRoute_Click(object sender, System.EventArgs e)
        {
            clear();
            this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;

            switch (cbOrderPolicy.SelectedIndex)
            {
                case 0:
                    routeSolver.LocationOrderPolicy = gviNetworkLocationOrderPolicy.gviSequence;
                    break;
                case 1:
                    routeSolver.LocationOrderPolicy = gviNetworkLocationOrderPolicy.gviFixStart;
                    break;
                case 2:
                    routeSolver.LocationOrderPolicy = gviNetworkLocationOrderPolicy.gviFixStartAndReturn;
                    break;
                case 3:
                    routeSolver.LocationOrderPolicy = gviNetworkLocationOrderPolicy.gviFixStartEnd;
                    break;
                case 4:
                    routeSolver.LocationOrderPolicy = gviNetworkLocationOrderPolicy.gviFree;
                    break;
            }
            if (routeSolver.Solve())
            {
                INetworkRoute route = routeSolver.GetRoute();
                if (route != null)
                {
                    ICurveSymbol lineSym = new CurveSymbol();
                    lineSym.Color = System.Drawing.Color.Yellow;
                    lineSym.Width = -2;
                    IGeometry geo = route.GetRouteGeometry();
                    if(geo.GeometryType == gviGeometryType.gviGeometryPolyline)
                    {
                        IPolyline line = geo as IPolyline;
                        renderLine = this.axRenderControl1.ObjectManager.CreateRenderPolyline(line, lineSym, rootId);
                        renderLine.MaxVisibleDistance = 10000;
                    }
                    else if (geo.GeometryType == gviGeometryType.gviGeometryMultiPolyline)
                    {
                        IMultiPolyline line = geo as IMultiPolyline;
                        multiRenderLine = this.axRenderControl1.ObjectManager.CreateRenderMultiPolyline(line, lineSym, rootId);
                        multiRenderLine.MaxVisibleDistance = 10000;
                    }
                    btnNavigate.Enabled = true;

                    drawTempLine(route);
                }
            }
            else
                MessageBox.Show("查找路径失败");
        }

        private void drawTempLine(INetworkRoute route)
        {
            // 画出停靠点到映射点的虚线
            int segmentCount = route.SegmentCount;
            INetworkRouteSegment segment = null;
            INetworkLocation startLocation = null;
            INetworkLocation endLocation = null;
            IPoint pointOnNetwork = null;
            ICurveSymbol tmpLineSym = new CurveSymbol();
            tmpLineSym.Color = System.Drawing.Color.Yellow;
            tmpLineSym.Width = -2;
            for (int i = 0; i < segmentCount; i++)
            {
                segment = route.GetSegment(i);
                startLocation = segment.StartLocation;
                pointOnNetwork = startLocation.NetworkPosition;
                fdepoint = startLocation.Position;
                IPolyline tmpLine = geoFac.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                tmpLine.AppendPoint(fdepoint);
                tmpLine.AppendPoint(pointOnNetwork);
                tmpRenderLineArray.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(tmpLine, tmpLineSym, rootId));
                if (i == segmentCount - 1)
                {
                    endLocation = segment.EndLocation;
                    pointOnNetwork = endLocation.NetworkPosition;
                    fdepoint = endLocation.Position;
                    tmpLine = geoFac.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                    tmpLine.AppendPoint(fdepoint);
                    tmpLine.AppendPoint(pointOnNetwork);
                    tmpRenderLineArray.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(tmpLine, tmpLineSym, rootId));
                }
            }
        }

        private void btnNavigate_Click(object sender, EventArgs e)
        {
            if (tour != null)
            {
                //是否相机跟随
                if (cbCameraFollow.Checked)
                {
                    if (renderModelPoint != null)
                        this.axRenderControl1.Camera.FlyToObject(renderModelPoint.Guid, gviActionCode.gviActionFollowBehindAndAbove);
                    else if (skinMesh != null)
                        this.axRenderControl1.Camera.FlyToObject(skinMesh.Guid, gviActionCode.gviActionFollowBehindAndAbove);
                }
                tour.Play();
                return;
            }

            tour = this.axRenderControl1.ObjectManager.CreateDynamicObject(rootId);
            tour.CrsWKT = dataset_Road.SpatialReference.AsWKT();
            tour.TurnSpeed = 100000;

            if (renderLine != null)
            {
                IPolyline line = renderLine.GetFdeGeometry() as IPolyline;
                for (int i = 0; i < line.PointCount; i++)
                {
                    fdepoint = line.GetPoint(i);
                    if (txtLocationNames.Text.Contains("I'm Here!"))
                        tour.AddWaypoint2(fdepoint, 30);
                    else
                        tour.AddWaypoint2(fdepoint, 100);
                }
            }
            else if (multiRenderLine != null)
            {
                IMultiPolyline mline = multiRenderLine.GetFdeGeometry() as IMultiPolyline;
                for (int i = 0; i < mline.GeometryCount; i++)
                {
                    IPolyline line = mline.GetPolyline(i);
                    for (int j = 0; j < line.PointCount; j++)
                    {
                        fdepoint = line.GetPoint(j);
                        if (txtLocationNames.Text.Contains("I'm Here!"))
                            tour.AddWaypoint2(fdepoint, 30);
                        else
                            tour.AddWaypoint2(fdepoint, 100);
                    }
                }
            }
            if (txtLocationNames.Text.Contains("I'm Here!"))
                LoadPeople(tour);
            else
                LoadCar(tour);
            //是否相机跟随
            if (cbCameraFollow.Checked)
            {
                if (renderModelPoint != null)
                    this.axRenderControl1.Camera.FlyToObject(renderModelPoint.Guid, gviActionCode.gviActionFollowBehindAndAbove);
                else if (skinMesh != null)
                    this.axRenderControl1.Camera.FlyToObject(skinMesh.Guid, gviActionCode.gviActionFollowBehindAndAbove);
            }
            tour.Play();
        }

        
        private void LoadCar(IDynamicObject dynamicObject)
        {
            if (renderModelPoint == null)
            {
                // 构造ModelPoint
                mp = geoFac.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                string modelName = (strMediaPath + @"\osg\Vehicles\Car\TCJ006.osg");
                mp.ModelName = modelName;
                mp.SpatialCRS = dataset_Road.SpatialReference;
                // 设置位置
                IMatrix matrix = new Matrix();
                matrix.MakeIdentity();
                IPoint startPoint = null;
                double speed = 0.0;
                dynamicObject.GetWaypoint2(0, out startPoint, out speed);
                matrix.SetTranslate(startPoint.Position);
                mp.FromMatrix(matrix);
                mp.SelfScale(0.05, 0.05, 0.05);
                // 创建骨骼动画
                renderModelPoint = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, null, rootId);
                renderModelPoint.MaxVisibleDistance = 10000;
                renderModelPoint.ViewingDistance = 200;
                // 绑定到运动路径
                IMotionable m = renderModelPoint as IMotionable;
                pos.Set(0, 0, 0);
                m.Bind2(dynamicObject, pos, 90, 0, 0);
            }
        }

        private void LoadPeople(IDynamicObject dynamicObject)
        {
            if (skinMesh == null)
            {
                // 构造ModelPoint
                mp = geoFac.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                string modelName = (strMediaPath + @"\x\Character\QiYeYuanGong.X");
                mp.ModelName = modelName;
                mp.SpatialCRS = dataset_Road.SpatialReference;
                // 设置位置
                IMatrix matrix = new Matrix();
                matrix.MakeIdentity();
                IPoint startPoint = null;
                double speed = 0.0;
                dynamicObject.GetWaypoint2(0, out startPoint, out speed);
                matrix.SetTranslate(startPoint.Position);
                mp.FromMatrix(matrix);
                mp.SelfScale(5, 5, 5);
                // 创建骨骼动画
                skinMesh = this.axRenderControl1.ObjectManager.CreateSkinnedMesh(mp, rootId);
                if (skinMesh == null)
                {
                    MessageBox.Show("骨骼动画创建失败！");
                    return;
                }
                skinMesh.Loop = true;
                skinMesh.Play();
                skinMesh.MaxVisibleDistance = 10000;
                skinMesh.ViewingDistance = 100;
                // 绑定到运动路径
                IMotionable m = skinMesh as IMotionable;
                pos.Set(0, 0, 0);
                m.Bind2(dynamicObject, pos, 0, 0, 0);
            }
        }

        private void cbCameraFollow_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCameraFollow.Checked)
            {
                if (renderModelPoint != null)
                    this.axRenderControl1.Camera.FlyToObject(renderModelPoint.Guid, gviActionCode.gviActionFollowBehindAndAbove);
                else if (skinMesh != null)
                    this.axRenderControl1.Camera.FlyToObject(skinMesh.Guid, gviActionCode.gviActionFollowBehindAndAbove);
            }
            else
            {
                this.axRenderControl1.Camera.GetCamera2(out fdepoint, out ang);
                this.axRenderControl1.Camera.SetCamera2(fdepoint, ang, gviSetCameraFlags.gviSetCameraNoFlags);
            }
        }

        private void btnFindWC_Click(object sender, EventArgs e)
        {
            clear();
            clearDeep();

            IFdeCursor cursor = null;
            try
            {
                if (closestFacilitySolver == null)
                {
                    closestFacilitySolver = network.CreateClosestFacilitySolver();
                    closestFacilitySolver.ImpedanceAttributeName = "Length";
                }
                closestFacilitySolver.LocationSearchTolerance = double.Parse(txtSearchTolerance.Text);
                closestFacilitySolver.ClearFacilityLocations();
                closestFacilitySolver.ClearEventLocations();
                
                // 添加WC设施点
                foreach (IFeatureClass fc in fcMap_POI.Keys)
                {
                    if (fc.Name.Contains("WC"))
                    {
                        cursor = fc.Search(null, true);
                        IRowBuffer row = null;
                        while ((row = cursor.NextRow()) != null)
                        {
                            try
                            {
                                INetworkLocation facility = new NetworkLocation();
                                int pos = row.FieldIndex("Geometry");
                                IPoint point = row.GetValue(pos) as IPoint;
                                facility.Position = point;
                                facility.Name = fc.Guid.ToString() + "_" + row.GetValue(0).ToString(); //设定名字"fcGUID_oid"
                                closestFacilitySolver.AddFacilityLocation(facility);
                            }
                            catch (COMException ex)
                            {
                            }
                        }
                        break;
                    }
                }
                if(closestFacilitySolver.FacilityLocationCount == 0)
                {
                    MessageBox.Show("添加的厕所数为0，请调整LocationSearchTolerance大小");
                    return;
                }

                // 添加人所在的位置
                INetworkEventLocation location = new NetworkEventLocation();
                this.axRenderControl1.Camera.GetCamera2(out fdepoint, out ang);
                location.Position = fdepoint;
                location.Name = "I'mHere";
                location.TargetFacilityCount = int.Parse(txtMaxNum.Text);
                location.SetCutoff("Length", double.Parse(txtCutoff.Text));
                closestFacilitySolver.AddEventLocation(location);
                // 可视化人的位置
                IImagePointSymbol ips = new ImagePointSymbol();
                ips.ImageName = "#(i)";
                ips.Size = 50;
                renderPoint = this.axRenderControl1.ObjectManager.CreateRenderPoint(fdepoint, ips, rootId);

                if (closestFacilitySolver.Solve())
                {
                    int routeCount = closestFacilitySolver.RouteCount;
                    if (routeCount == 0)
                    {
                        MessageBox.Show("没有厕所在指定范围内");
                        return;
                    }
                    for (int i = 0; i < routeCount; i++)
                    {
                        INetworkRoute route = closestFacilitySolver.GetRoute(i);
                        if (route != null)
                        {
                            // 可视化线路
                            ICurveSymbol lineSym = new CurveSymbol();
                            lineSym.Color = System.Drawing.Color.Yellow;
                            lineSym.Width = -2;
                            IGeometry geo = route.GetRouteGeometry();
                            if (geo.GeometryType == gviGeometryType.gviGeometryPolyline)
                            {
                                IPolyline line = geo as IPolyline;
                                renderLine = this.axRenderControl1.ObjectManager.CreateRenderPolyline(line, lineSym, rootId);
                                renderLine.MaxVisibleDistance = 10000;
                                renderLineArray.Add(renderLine);
                            }
                            else if (geo.GeometryType == gviGeometryType.gviGeometryMultiPolyline)
                            {
                                IMultiPolyline line = geo as IMultiPolyline;
                                multiRenderLine = this.axRenderControl1.ObjectManager.CreateRenderMultiPolyline(line, lineSym, rootId);
                                multiRenderLine.MaxVisibleDistance = 10000;
                                multiRenderLineArray.Add(multiRenderLine);
                            }

                            drawTempLine(route);

                            // 高亮厕所
                            int segmentCount = route.SegmentCount;
                            for (int j = 0; j < segmentCount; j++)
                            {
                                INetworkLocation endLocation = route.GetSegment(j).EndLocation;
                                string[] strs = endLocation.Name.Split('_');
                                foreach (IFeatureClass fc in fcMap_POI.Keys)
                                {
                                    if (fc.Guid.ToString() == strs[0])
                                    {
                                        this.axRenderControl1.FeatureManager.HighlightFeature(fc, int.Parse(strs[1]), System.Drawing.Color.Yellow);
                                        break;
                                    }
                                }

                                //////////////////////测试NetworkElement相关//////////////////////////////////
                                INetworkElementCollection elementCols = route.GetSegment(j).GetNetworkElements();
                                for (int c = 0; c < elementCols.Count; c++)
                                {
                                    INetworkElement element = elementCols.Get(c);
                                    if (element.Type == gviNetworkElementType.gviEdge)
                                    {
                                        INetworkEdge edge = element as INetworkEdge;
                                        int subId = edge.SubID;
                                    }
                                    else
                                    {
                                        INetworkJunction junction = element as INetworkJunction;
                                        INetworkEdgeCollection edgeCol = junction.IncomingEdges;
                                        for (int ee = 0; ee < edgeCol.Count; ee++)
                                        {
                                            INetworkEdge edge = edgeCol.Get(ee);
                                            int subId = edge.SubID;
                                        }
                                    }
                                }
                                //////////////////////////////////////////////////////////////////////////
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("查找失败");
            }
            catch (COMException ex)
            {
            	MessageBox.Show(ex.Message);
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

        private void clear()
        {
            if (renderLine != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderLine.Guid);
                renderLine = null;
            }
            if (multiRenderLine != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(multiRenderLine.Guid);
                multiRenderLine = null;
            }
            if (tour != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(tour.Guid);
                tour = null;
            }
            if (renderModelPoint != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderModelPoint.Guid);
                renderModelPoint = null;
            }
            if (skinMesh != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(skinMesh.Guid);
                skinMesh = null;
            }

            if (renderLineArray.Count != 0)
            {
                foreach (IRenderPolyline line in renderLineArray)
                    this.axRenderControl1.ObjectManager.DeleteObject(line.Guid);
                renderLineArray.Clear();
            }
            if (multiRenderLineArray.Count != 0)
            {
                foreach (IRenderMultiPolyline line in multiRenderLineArray)
                    this.axRenderControl1.ObjectManager.DeleteObject(line.Guid);
                multiRenderLineArray.Clear();
            }
            if (tmpRenderLineArray.Count != 0)
            {
                foreach (IRenderPolyline line in tmpRenderLineArray)
                    this.axRenderControl1.ObjectManager.DeleteObject(line.Guid);
                tmpRenderLineArray.Clear();
            }
        }

        private void clearDeep()
        {
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            txtLocationNames.Text = "";
            routeSolver.ClearLocations();
            routeSolver.LocationSearchTolerance = double.Parse(txtSearchTolerance.Text);
            if (renderPoint != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderPoint.Guid);
                renderPoint = null;
            }
        }

    }
}


