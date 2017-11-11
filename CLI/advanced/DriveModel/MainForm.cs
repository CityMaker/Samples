using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using Gvitech;
using System;
using Gvitech.CityMaker.Controls;

namespace DriveModel
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private System.Guid rootId = new System.Guid();
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEnvelope env;//加载数据时，初始化的矩形范围
        private IEulerAngle angle = new EulerAngle();
        private IVector3 v = new Vector3();
        private IMatrix mat = new Matrix();

        IPolygon polygon = null; //拾取时选中的polygon 
        IPolygon polygonInLayer = null; //记录拾取时选中的polygon，供重置时用
        string modelNameToModify = null; // 待修改的模型名称
        string tmpTexturePath; // 模型外立面贴图
        Hashtable rModelpointList = new Hashtable(); //建模时创建的RenderModelPoint
        Hashtable colorList = new Hashtable();  //模型颜色十六进制字符串，用于修改模型顶点时用
        ArrayList rPolylinelist = new ArrayList();  //拾取时DrawEnvelope产生的RenderPolyline        
        ArrayList rModelpointToDelList = new ArrayList();  //之前生成的RenderModelPoint待删除
        ArrayList polygonlist = new ArrayList(); //存储每一层RenderModelPoint对应的polygon
        ArrayList rPolygonlist = new ArrayList(); //拾取层时创建的侧面RenderPolygon
        ArrayList labelList = new ArrayList(); //编辑侧面时显示顶点号的标签
        Hashtable modelList = new Hashtable(); //建模时创建的Model对象

        private IObjectEditor _geoEditor = null;
        IRenderPolygon editingRPolygon = null; //用于编辑的RenderPolygon
        IPolygon polygonEditing = null;  //编辑过程中的polygon

        private ITransformHelper helper = null;
        IRenderPolygon sideRPolygon = null; //编辑侧面的RenderPolygon
        Double beforeX = 0.0;
        Double beforeY = 0.0;
        Double beforeZ = 0.0;

        IModel modelWhole = null;
        IEnvelope envWhole = null;
        double totalHeight = 0.0, lastHeight = 0.0;
        IModelPoint mpWhole = null;
        IRenderModelPoint rmpWhole = null;
        IMatrix M0 = new Matrix();
        IMatrix M1 = new Matrix();

        IPolygon polygonForBoxScale = null; //BoxScale时每次ObjectEditing作用的polygon（顶点坐标系是世界坐标系）

        double rotateAngle = 0.0;

        // 线程转发
        public int MainThreadId = 0;
        
        
        
        
        private _IRenderControlEvents_RcTransformHelperBeginEventHandler _rcTransformHelperBegin;
        private _IRenderControlEvents_RcTransformHelperMovingEventHandler _rcTransformHelperMoving;
        private _IRenderControlEvents_RcTransformHelperEndEventHandler _rcTransformHelperEnd;

        private void init()
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;

            this.axRenderControl1.Camera.FlyTime = 0;

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

            #region 加载FDB场景
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polygon.FDB");
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
                        env = geometryDef.Envelope;
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
            #endregion 
        }

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            //错误日志
            Logger.Create(Environment.CurrentDirectory); //Application.LocalUserAppDataPath

            init();

            ILicenseServer license = new LicenseServer();
            license.SetHost("192.168.2.200", 8588, "");

            // 注册控件拾取事件
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            // 初始化颜色
            colorBox.BackColor = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
            
            // 编辑顶点时用
            _geoEditor = this.axRenderControl1.ObjectEditor;
            // 注册控件编辑事件
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            

            cbEditSideMode.SelectedIndex = 0;
            // 绑定esc退出TransformHelper模式
            this.axRenderControl1.RcKeyUp += new _IRenderControlEvents_RcKeyUpEventHandler(axRenderControl1_RcKeyUp);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DriveModel.html";
            }
        }



        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            IPickResult pr = PickResult;
            if (pr == null)
                return;

            if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                if (pr.Type == gviObjectType.gviObjectFeatureLayer)
                {
                    // 一旦拾取新的Polygon，之前生成的RenderModelPoint都不再能被拾取
                    // 将之前创建的RenderModelPoint置为不可拾取
                    if (rModelpointList.Count != 0)
                    {
                        for(int i = 0; i< rModelpointList.Count; i++)
                        {
                            Knight myObject = rModelpointList[i] as Knight;
                            IRenderModelPoint rmp = myObject.MpObject as IRenderModelPoint;
                            rmp.MouseSelectMask = gviViewportMask.gviViewNone;
                            rModelpointToDelList.Add(myObject); // 移至待删除队列，供重置使用
                        }
                        rModelpointList.Clear();
                        colorList.Clear();
                        polygonlist.Clear();
                    }
                    //删除画包围框的RenderPolyline
                    for (int i = 0; i < rPolylinelist.Count; i++)
                        this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
                    rPolylinelist.Clear();
                    //清除了界面选中的RenderModelPoint，同时需要清除待修改的模型名称
                    modelNameToModify = null;

                    // 高亮Polygon
                    this.axRenderControl1.FeatureManager.UnhighlightAll();
                    IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                    int fid = flpr.FeatureId;
                    IFeatureLayer fl = flpr.FeatureLayer;
                    foreach (IFeatureClass fc in fcMap.Keys)
                    {
                        if (fc.Guid.Equals(fl.FeatureClassId))
                        {
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
                                                info.GeometryDef.GeometryColumnType == gviGeometryColumnType.gviGeometryColumnPolygon)
                                            {
                                                int nPos = fdeRow.FieldIndex(info.Name);
                                                polygon = fdeRow.GetValue(nPos) as IPolygon;
                                                polygonInLayer = polygon;
                                                this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, Color.Red);
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
                                    cursor.Dispose();
                                    cursor = null;
                                }
                            } //end try
                        } // end " if (fc.Guid.Equals(fl.FeatureClassId))"
                    } // end "foreach (IFeatureClass fc in fcMap.Keys)"
                }
                else if (pr.Type == gviObjectType.gviObjectRenderModelPoint)
                {                    
                    // 删除画包围框的RenderPolyline
                    for (int i = 0; i < rPolylinelist.Count; i++)
                        this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
                    rPolylinelist.Clear();
                    // 获取模型名
                    IRenderModelPointPickResult rmpr = pr as IRenderModelPointPickResult;
                    IRenderModelPoint rmp = rmpr.ModelPoint;
                    IModelPoint mp = rmp.GetFdeGeometry() as IModelPoint;
                    modelNameToModify = rmp.ModelName;
                    // 从模型名中分解出索引值
                    if (modelNameToModify == null)
                    {
                        MessageBox.Show("请选择楼层");
                        return;
                    }
                    string[] strs = modelNameToModify.Split('#');
                    int floorIndex = int.Parse(strs[1]);
                    // 画包围框
                    polygon = polygonlist[floorIndex] as IPolygon;
                    DrawEnvelope(polygon, mp.Z, mp.Envelope.Depth, new CRSFactory().CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS, out rPolylinelist, true);
                    // 根据索引值得到楼层号
                    this.numFloorNum.Value = decimal.Parse(floorIndex.ToString());
                    // 根据索引值得到楼层高
                    Knight modifyObject = rModelpointList[floorIndex] as Knight;
                    this.numFloorHeight.Value = decimal.Parse(modifyObject.Height.ToString());
                    // 根据索引值得到楼层颜色
                    colorBox.Text = colorList[floorIndex].ToString();
                    string RGBStr = colorBox.Text.Substring(2, 6);
                    string colorStr = "FF" + RGBStr;
                    colorBox.BackColor = Color.FromArgb(Convert.ToInt32(colorStr, 16));
                }
                else if (pr.Type == gviObjectType.gviObjectRenderPolygon)
                {
                    // 恢复之前侧面
                    if (sideRPolygon != null)
                    {
                        ISurfaceSymbol defaultSymbol = new SurfaceSymbol();
                        sideRPolygon.Symbol = defaultSymbol;
                    }                   
                    // 拾取到新的侧面
                    IRenderPolygonPickResult rppr = pr as IRenderPolygonPickResult;
                    sideRPolygon = rppr.Polygon;
                    // 高亮拾取到的侧面
                    ISurfaceSymbol ss = new SurfaceSymbol();
                    ss.Color = Color.Red;
                    sideRPolygon.Symbol = ss;
                    //求欧拉角
                    // 默认三维系统相机眼睛看的方向是y轴：绕z轴转是heading、绕x轴转是tilt(抬头低头)、绕y轴转是roll(翻滚)
                    IVector3 normal = (sideRPolygon.GetFdeGeometry() as IPolygon).QueryNormal();
                    IVector3 p = new Vector3();
                    p.Set(normal.X, normal.Y, 0);
                    p.Normalize();
                    IVector3 y = new Vector3();
                    y.Set(0, 1, 0);
                    //先把y轴绕z轴转到法相在xy平面上的投影处
                    double hLength = p.DotProduct(y);
                    double heading = Math.Acos(hLength);
                    if (normal.X < 0)
                    {
                        heading = -heading;
                    }
                    //再把z轴绕x轴向下转到与法相重合
                    normal.Normalize();
                    IVector3 z = new Vector3();
                    z.Set(0, 0, 1);
                    double tLength = z.DotProduct(normal);
                    double tilt = -Math.Acos(tLength);
                    angle.Set(heading * 180 / 3.14, tilt * 180 / 3.14, 0);
                    helper.SetPosition2(sideRPolygon.Envelope.Center, angle);
                    beforeX = sideRPolygon.Envelope.Center.X;
                    beforeY = sideRPolygon.Envelope.Center.Y;
                    beforeZ = sideRPolygon.Envelope.Center.Z; 
                }
            }
        }

        private void toolStripButtonNormal_Click(object sender, System.EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer | gviMouseSelectObjectMask.gviSelectRenderGeometry;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        private void toolStripButtonSelect_Click(object sender, System.EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer | gviMouseSelectObjectMask.gviSelectRenderGeometry;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        /// <summary>
        /// 参数化建模
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDriveModel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (polygon != null)
                {
                    // 一旦重复驱动模型，就将之前创建的RenderModelPoint置为不可拾取不可见，同时移至待删除队列
                    if (rModelpointList.Count != 0)
                    {
                        for (int i = 0; i < rModelpointList.Count; i++)
                        {
                            Knight myObject = rModelpointList[i] as Knight;
                            IRenderModelPoint rmp = myObject.MpObject as IRenderModelPoint;
                            rmp.MouseSelectMask = gviViewportMask.gviViewNone;
                            rmp.VisibleMask = gviViewportMask.gviViewNone;
                            rModelpointToDelList.Add(myObject); // 移至待删除队列，供重置使用
                        }
                        rModelpointList.Clear();
                        colorList.Clear();
                        polygonlist.Clear();
                    }
                    //删除画包围框的RenderPolyline
                    for (int i = 0; i < rPolylinelist.Count; i++)
                        this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
                    rPolylinelist.Clear();
                    //删除模型数组
                    modelList.Clear();

                    IParametricModelling pm = new ParametricModelling();
                    int floorNum = int.Parse(numFloorNum.Value.ToString());
                    double floorHeight = double.Parse(numFloorHeight.Value.ToString());
                    // 逐层建模
                    IPropertySet ps = new PropertySet();
                    ps.SetProperty("FloorNumber", 1);
                    ps.SetProperty("FloorHeight", floorHeight);
                    ps.SetProperty("HeightOffset", 0);
                    tmpTexturePath = (strMediaPath + @"\dds\facade\floor.dds");
                    ps.SetProperty("FacadeTexture", tmpTexturePath);
                    IModelPoint mp = null;
                    IModel model = null;
                    for (int i = 0; i < floorNum; i++)
                    {
                        // 顶层加女儿墙
                        if (i == floorNum - 1)
                        {
                            //ps.Remove("HeightOffset");
                            ps.SetProperty("HeightOffset", 1);
                        }
                        if (pm.PolygonToBuilding(polygon, ps, out mp, out model))
                        {
                            // 设置模型颜色
                            for (int g = 0; g < model.GroupCount; g++)
                            {
                                for (int p = 0; p < model.GetGroup(g).PrimitiveCount; p++)
                                {
                                    IDrawPrimitive primitive = model.GetGroup(g).GetPrimitive(p);
                                    IDrawMaterial material = primitive.Material;
                                    // 从界面读取颜色
                                    Color userColor = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
                                    material.DiffuseColor = userColor;
                                }
                            }
                            colorList.Add(i, colorBox.Text);
                            polygonlist.Add(polygon.Clone());

                            // 给模型名加上层数标记
                            string modelName = string.Format("{0}#{1}", mp.ModelName, i);
                            mp.ModelName = modelName;
                            // 在内存中临时存储模型
                            this.axRenderControl1.ObjectManager.AddModel(modelName, model);
                            modelList.Add(modelName, model);
                            // 向上平移中心点坐标
                            mp.Z = mp.Z + floorHeight * i;
                            // 可视化临时模型
                            IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, null, rootId);
                            rmp.MaxVisibleDistance = 100000;
                            rModelpointList.Add(i, new Knight(modelName, rmp, floorHeight));
                        }
                        else
                            MessageBox.Show("参数化建模失败");
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

        private void btnModifyModel_Click(object sender, System.EventArgs e)
        {
            if (polygon != null)
            {
                // 从模型名中分解出索引值
                if (modelNameToModify == null)
                {
                    MessageBox.Show("请选择楼层");
                    return;
                }
                string[] strs = modelNameToModify.Split('#');
                int floorIndex = int.Parse(strs[1]);

                IParametricModelling pm = new ParametricModelling();
                double floorHeight = double.Parse(numFloorHeight.Value.ToString());
                // 用一层模型重新建模
                IPropertySet ps = new PropertySet();
                ps.SetProperty("FloorNumber", 1);
                ps.SetProperty("FloorHeight", floorHeight);
                if (floorIndex != rModelpointList.Count - 1)
                    ps.SetProperty("HeightOffset", 0); //非顶层不需要女儿墙
                else
                    ps.SetProperty("HeightOffset", 1);
                tmpTexturePath = (strMediaPath + @"\dds\facade\floor.dds");
                ps.SetProperty("FacadeTexture", tmpTexturePath);
                IModelPoint mp = null;
                IModel model = null;

                if (pm.PolygonToBuilding(polygon, ps, out mp, out model))
                {
                    // 设置模型颜色
                    for (int g = 0; g < model.GroupCount; g++)
                    {
                        for (int p = 0; p < model.GetGroup(g).PrimitiveCount; p++)
                        {
                            IDrawPrimitive primitive = model.GetGroup(g).GetPrimitive(p);
                            IDrawMaterial material = primitive.Material;
                            // 从界面读取颜色
                            Color userColor = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
                            material.DiffuseColor = userColor;
                        }
                    }
                    colorList[floorIndex] = colorBox.Text;
                    mp.ModelName = modelNameToModify;
                    // 更新内存中的临时存储模型
                    this.axRenderControl1.ObjectManager.AddModel(modelNameToModify, model);
                    modelList[modelNameToModify] = model;

                    Knight modifyObject = rModelpointList[floorIndex] as Knight;
                    IRenderModelPoint currentMP = modifyObject.MpObject as IRenderModelPoint;
                    // 平移中心点坐标
                    mp.Z = currentMP.Position.Z; // 生成模型z都在底面处，所以当前模型不用加delta

                    double delta = 0;
                    if (floorIndex != rModelpointList.Count - 1)
                        delta = floorHeight - modifyObject.Height;
                    // 平移选中模型以上的模型
                    int currentIndex = floorIndex;
                    for (int ii = currentIndex + 1; ii < rModelpointList.Count; ii++)
                    {
                        Knight upperObject = rModelpointList[ii] as Knight;
                        IRenderModelPoint upperMP = upperObject.MpObject as IRenderModelPoint;
                        v.Set(upperMP.Position.X, upperMP.Position.Y, upperMP.Position.Z + delta);
                        upperMP.Position = v;
                        upperObject.MpObject = upperMP;
                        rModelpointList[ii] = upperObject;
                    }

                    // 删除之前的RenderModelPoint
                    this.axRenderControl1.ObjectManager.DeleteObject(modifyObject.MpObject.Guid);
                    // 可视化临时模型
                    IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, null, rootId);
                    rmp.MaxVisibleDistance = 100000;
                    rModelpointList[floorIndex] = new Knight(modelNameToModify, rmp, floorHeight);

                    //重新画包围框
                    for (int i = 0; i < rPolylinelist.Count; i++)
                        this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
                    rPolylinelist.Clear();
                    //DrawEnvelope(rmp.Envelope, new CRSFactory().CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS, out rPolylinelist);
                    DrawEnvelope(polygon, mp.Z, mp.Envelope.Depth, new CRSFactory().CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS, out rPolylinelist, true);
                }
                else
                    MessageBox.Show("参数化建模失败");
            }
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            //删除RenderModelPoint
            for (int j = 0; j < rModelpointList.Count; j++)
            {
                Knight myObject = rModelpointList[j] as Knight;
                this.axRenderControl1.ObjectManager.DeleteObject(myObject.MpObject.Guid);
            }
            rModelpointList.Clear();
            //删除RenderModelPoint
            for (int k = 0; k < rModelpointToDelList.Count; k++)
            {
                Knight myObject = rModelpointToDelList[k] as Knight;
                this.axRenderControl1.ObjectManager.DeleteObject(myObject.MpObject.Guid);
            }
            rModelpointToDelList.Clear();
            //删除包围框
            for (int i = 0; i < rPolylinelist.Count; i++)
                this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
            rPolylinelist.Clear();
            //清除了界面选中的RenderModelPoint，同时需要清除待修改的模型名称
            modelNameToModify = null;
            //清除每层对应的记录
            colorList.Clear();
            polygonlist.Clear();
            // 恢复默认楼层数设置
            this.numFloorHeight.Value = 3;
            this.numFloorNum.Value = 10;
            polygon = polygonInLayer;
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
            string imageName = (strMediaPath + @"\gif\gg.gif");
            cSymbol.ImageName = imageName;
            cSymbol.RepeatLength = 5;
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

        private void DrawEnvelope(IPolygon polygon, double bottomHeight, double roofHeight, ISpatialCRS crs, out ArrayList rPolylineList, bool needBuffer)
        {
            rPolylineList = new ArrayList();

            IPolyline polyline1 = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            polyline1.SpatialCRS = crs;
            IPolyline polyline2 = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            polyline2.SpatialCRS = crs;
            IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            point.SpatialCRS = crs;

            ICurveSymbol cSymbol = new CurveSymbol();
            cSymbol.Color = System.Drawing.Color.Yellow; 
            cSymbol.Width = -2;

            IRing ring = polygon.ExteriorRing.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IRing;
            if (needBuffer)
            {
                ITopologicalOperator2D topo = ring as ITopologicalOperator2D;
                IPolygon buffer = topo.Buffer2D(0.1, gviBufferStyle.gviBufferCapround) as IPolygon;
                ring = buffer.ExteriorRing.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IRing;
            }
            
            for (int i = 0; i < ring.PointCount; i++)
            {
                point = ring.GetPoint(i);
                point.Z = bottomHeight;
                polyline1.AppendPoint(point);
            }
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline1, cSymbol, rootId));

            for (int i = 0; i < ring.PointCount; i++)
            {
                point = ring.GetPoint(i);
                point.Z = bottomHeight + roofHeight;
                polyline2.AppendPoint(point);
            }
            rPolylineList.Add(this.axRenderControl1.ObjectManager.CreateRenderPolyline(polyline2, cSymbol, rootId));
        }

        #region 设置颜色
        /// <summary>
        /// 改变颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeColor_Click(object sender, System.EventArgs e)
        {
            this.colorDialog1.Color = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                int a = this.trackBarOpacity.Value;
                Color newColor = Color.FromArgb(a, this.colorDialog1.Color.R, this.colorDialog1.Color.G, this.colorDialog1.Color.B);
                this.colorBox.Text = newColor.ToArgb().ToString("X");
                colorBox.BackColor = this.colorDialog1.Color;
            }
        }

        /// <summary>
        /// 设置颜色透明度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarOpacity_Scroll(object sender, System.EventArgs e)
        {
            int a = this.trackBarOpacity.Value;
            Color userColor = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
            Color newColor = Color.FromArgb(a, userColor.R, userColor.G, userColor.B);
            this.colorBox.Text = newColor.ToArgb().ToString("X");
            //colorBox.BackColor = newColor; //Winform控件背景色不支持透明度，打开此句会有崩溃
        }
        #endregion

        private void btnEditPolygon_Click(object sender, System.EventArgs e)
        {
            if (polygon != null)
            {
                // 从模型名中分解出索引值
                if (modelNameToModify == null)
                {
                    MessageBox.Show("请选择楼层");
                    return;
                }
                string[] strs = modelNameToModify.Split('#');
                int floorIndex = int.Parse(strs[1]);

                // 透明化选中模型以上的模型
                int currentIndex = floorIndex;
                for (int ii = currentIndex + 1; ii < rModelpointList.Count; ii++)
                {
                    Knight upperObject = rModelpointList[ii] as Knight;
                    IRenderModelPoint upperMP = upperObject.MpObject as IRenderModelPoint;
                    IModelPointSymbol mpsymbol = new ModelPointSymbol();
                    mpsymbol.EnableColor = true;
                    mpsymbol.Color = Color.FromArgb(Convert.ToInt32("40ffffff", 16));
                    upperMP.Symbol = mpsymbol;
                    upperMP.MouseSelectMask = gviViewportMask.gviViewNone;
                    upperObject.MpObject = upperMP;
                    rModelpointList[ii] = upperObject;
                }

                Knight currentObj = rModelpointList[floorIndex] as Knight;
                IRenderModelPoint currentRMP = currentObj.MpObject as IRenderModelPoint;
                IModelPoint mp = currentRMP.GetFdeGeometry() as IModelPoint;

                // 开启顶点编辑模式
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectRenderGeometry;
                this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                _geoEditor.FinishEdit();

                IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                point.SpatialCRS = new CRSFactory().CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS;
                IPolygon polygonZ = polygon.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                IRing ring = polygonZ.ExteriorRing;
                for (int jj = 0; jj < ring.PointCount; jj++)
                {
                    point = ring.GetPoint(jj);
                    point.Z = mp.Z + mp.Envelope.Depth;
                    ring.UpdatePoint(jj, point);
                }
                ISurfaceSymbol ss = new SurfaceSymbol();
                Color userColor = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
                ss.Color = userColor;
                editingRPolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(polygonZ, ss, rootId);
                if (!_geoEditor.StartEditRenderGeometry(editingRPolygon, gviGeoEditType.gviGeoEditVertex))
                    MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
            }
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            #region BoxScale
            if ((Geometry as IModelPoint) != null)
            {
                //原始rmp不可见
                for (int i = 0; i < rModelpointList.Count; i++)
                {
                    Knight upperObject = rModelpointList[i] as Knight;
                    IRenderModelPoint myMP = upperObject.MpObject as IRenderModelPoint;
                    myMP.VisibleMask = gviViewportMask.gviViewNone;
                }

                M0 = mpWhole.AsMatrix();
                //Logger.WriteMsg("M0: ");
                //printfMatrix(M0);

                M1 = (Geometry as IModelPoint).AsMatrix();
                IVector3 offset = new Vector3();
                offset.Set(M1.M41 - M0.M41, M1.M42 - M0.M42, M1.M43 - M0.M43);
                IVector3 baseV = new Vector3();
                baseV.Set(M0.M41, M0.M42, M0.M43);
                M0.M41 = M0.M42 = M0.M43 = 0;
                M1.SetTranslate(offset);

                IMatrix M0_Inverse = M0.Clone();
                M0_Inverse.Inverse();
                IMatrix MS = MultiplyMatrix(M0_Inverse, M1);  //此处注意!!!
                IVector3 scaleV = MS.GetScale();

                //Logger.WriteMsg("MS: ");
                //printfMatrix(MS);

                #region 增减层
                double delta = mpWhole.Envelope.Depth * (scaleV.Z - 1);
                if (delta > 0)
                {
                    int modelCountBeforeAdd = rModelpointList.Count;
                    Knight topObject = rModelpointList[rModelpointList.Count - 1] as Knight;
                    int floorDelta = (int)(delta / topObject.Height);

                    for (int k = 0; k < floorDelta; k++)
                    {
                        //往list里添加新增对象
                        IModel topModel = (IModel)modelList[topObject.ModelName];
                        IPolygon topPolygon = polygonlist[rModelpointList.Count - 1] as IPolygon;
                        polygonlist.Add(topPolygon.Clone());
                        string topColor = colorList[colorList.Count - 1].ToString();
                        colorList.Add(colorList.Count, topColor);
                        string modelName = string.Format("{0}#{1}", Guid.NewGuid(), modelList.Count);
                        IModel modelToAdd = CloneModel(topModel);
                        modelList.Add(modelName, modelToAdd);
                        this.axRenderControl1.ObjectManager.AddModel(modelName, modelToAdd);
                        IModelPoint mpToAdd = (topObject.MpObject as IRenderModelPoint).GetFdeGeometry().Clone() as IModelPoint;
                        mpToAdd.ModelName = modelName;
                        mpToAdd.Z = mpToAdd.Z + topObject.Height * (k + 1);
                        IRenderModelPoint rmpToAdd = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mpToAdd, null, rootId);
                        rmpToAdd.MaxVisibleDistance = 100000;
                        rmpToAdd.VisibleMask = gviViewportMask.gviViewNone;
                        rModelpointList.Add(rModelpointList.Count, new Knight(modelName, rmpToAdd, topObject.Height));
                    }
                }
                else
                {
                    int rModelpointListCount = rModelpointList.Count;
                    for (int k = rModelpointListCount - 1; k >= 0; k--)
                    {
                        Knight toDelObject = rModelpointList[k] as Knight;
                        delta += toDelObject.Height;
                        if (delta < 0) // 降低不到一层楼高度的不减
                        {
                            //从list里删除对象
                            polygonlist.RemoveAt(k);
                            colorList.Remove(k);
                            modelList.Remove(toDelObject.ModelName);
                            this.axRenderControl1.ObjectManager.DeleteModel(toDelObject.ModelName);
                            rModelpointList.Remove(k);
                        }
                        else
                            break;
                    }
                } //end if...else
                #endregion
                
                #region 计算修正矩阵
                IMatrix m_rotate = new Matrix();
                m_rotate = M0.Clone();
                m_rotate.M41 = 0;  //去掉M0中的位移，得到旋转信息
                m_rotate.M42 = 0;
                m_rotate.M43 = 0;
                //angle.Set(rotateAngle * 180 / Math.PI, 0, 0);
                //m_rotate.SetRotation(angle);
                IMatrix mr_Inverse = m_rotate.Clone();
                mr_Inverse.Inverse(); //求转置即可
                //IMatrix mr_Inverse = new Matrix();
                //IEulerAngle angle2 = new EulerAngle();
                //angle2.Set(-angle.Heading, -angle.Tilt, -angle.Roll);
                //mr_Inverse.SetRotation(angle2);

                //解方程RTST`R`=MS，得到剥离了R的矩阵
                IMatrix m_temp1 = MultiplyMatrix(mr_Inverse, MS);
                IMatrix m_TST = MultiplyMatrix(m_temp1, m_rotate);
                IVector3 v_TST = m_TST.GetScale();

                //解方程TST`= m_scale，得到位移量
                double centerX = 0.0, centerY = 0.0, centerZ = 0.0;
                if (Math.Abs(v_TST.X - 1.0) < 0.0000001)
                    centerX = 0.0;
                else
                    centerX = m_TST.M41 / (v_TST.X - 1.0);

                if (Math.Abs(v_TST.Y - 1.0) < 0.0000001)
                    centerY = 0.0;
                else
                    centerY = m_TST.M42 / (v_TST.Y - 1.0);

                if (Math.Abs(v_TST.Z - 1.0) < 0.0000001)
                    centerZ = 0.0;
                else
                    centerZ = m_TST.M43 / (v_TST.Z - 1.0);

                v.Set(centerX, centerY, centerZ);

                //位移矩阵T
                IMatrix m_trans = new Matrix();
                m_trans.SetTranslate(v);
                IMatrix mt_Inverse = m_trans.Clone();
                mt_Inverse.Inverse();
                //IMatrix mt_Inverse = new Matrix();
                //v.MultiplyByScalar(-1.0);
                //mt_Inverse.SetTranslate(v);

                // 保持面积不变
                if (cbKeepArea.Checked)
                {
                    //修改缩放矩阵
                    if (v_TST.X != v_TST.Y)
                    {
                        if (Math.Abs(v_TST.X - 1.0) < 0.0000001)
                        {
                            double x = 1 / v_TST.Y;
                            v_TST.Set(x, v_TST.Y, v_TST.Z);
                        }
                        else if (Math.Abs(v_TST.Y - 1.0) < 0.0000001)
                        {
                            double y = 1 / v_TST.X;
                            v_TST.Set(v_TST.X, y, v_TST.Z);
                        }
                    }
                }

                //最内层的缩放矩阵S
                IMatrix m_scale = new Matrix();
                m_scale.SetScale(v_TST);

                //回算得到修正后的MS
                IMatrix m_temp4 = MultiplyMatrix(m_trans, m_scale);
                m_TST = MultiplyMatrix(m_temp4, mt_Inverse);
                IMatrix m_temp5 = MultiplyMatrix(m_rotate, m_TST);
                MS = MultiplyMatrix(m_temp5, mr_Inverse);

                //Logger.WriteMsg("MS`: ");
                //printfMatrix(MS);
                #endregion

                // 对Polygon各个顶点进行缩放变换
                for (int i = 0; i < polygonForBoxScale.ExteriorRing.PointCount; i++)
                {
                    IPoint curP = polygonForBoxScale.ExteriorRing.GetPoint(i);

                    // MS是相对于MO做变换，故需要把polygon移回原点再做变换，之后再加回来!!!
                    curP.X -= baseV.X;
                    curP.Y -= baseV.Y;
                    curP.Z -= baseV.Z;

                    IVector3 newPos = MS.MultiplyVector(curP.Position);

                    //IVector3 newPos = new Vector3();
                    //M0_Inverse.MultiplyVector(curP.Position, ref newPos);
                    //IVector3 newPos1 = new Vector3();
                    //MS.MultiplyVector(newPos, ref newPos1);
                    //IVector3 newPos2 = new Vector3();
                    //M0.MultiplyVector(newPos1, ref newPos2);

                    curP.Position = newPos;
                    curP.X += baseV.X;
                    curP.Y += baseV.Y;
                    curP.Z += baseV.Z;
                    polygonForBoxScale.ExteriorRing.UpdatePoint(i, curP);
                }

                Polygon2Model(polygonForBoxScale.Clone() as IPolygon, mpWhole.Envelope.Depth * scaleV.Z);

                #region 当有xy方向的缩放时，需重新驱动以前的模型
                if (scaleV.X != 1 || scaleV.Y != 1)
                {
                    IEnumerator enumerator = modelList.Keys.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        string name = enumerator.Current.ToString();
                        this.axRenderControl1.ObjectManager.DeleteModel(name);
                    }
                    modelList.Clear();

                    double newTotalHeight = 0.0;
                    for (int i = 0; i < rModelpointList.Count; i++)
                    {
                        Knight oldObject = rModelpointList[i] as Knight;
                        IPolygon oldPolygon = polygonlist[i] as IPolygon;

                        // 对Polygon各个顶点进行缩放变换
                        for (int p = 0; p < oldPolygon.ExteriorRing.PointCount; p++)
                        {
                            IPoint curP = oldPolygon.ExteriorRing.GetPoint(p);

                            // MS是相对于MO做变换，故需要把polygon移回原点再做变换，之后再加回来
                            curP.X -= baseV.X;
                            curP.Y -= baseV.Y;
                            curP.Z -= baseV.Z;

                            IVector3 newPos = MS.MultiplyVector(curP.Position);

                            curP.Position = newPos;
                            curP.X += baseV.X;
                            curP.Y += baseV.Y;
                            curP.Z += baseV.Z;
                            oldPolygon.ExteriorRing.UpdatePoint(p, curP);
                        }
                        polygonlist[i] = oldPolygon.Clone();

                        IParametricModelling pm2 = new ParametricModelling();
                        IPropertySet ps2 = new PropertySet();
                        ps2.SetProperty("FloorNumber", 1);
                        ps2.SetProperty("FloorHeight", oldObject.Height);
                        ps2.SetProperty("HeightOffset", 0);
                        tmpTexturePath = (strMediaPath + @"\dds\facade\floor.dds");
                        ps2.SetProperty("FacadeTexture", tmpTexturePath);
                        IModelPoint newMP = null;
                        IModel newModel = null;
                        if (i == rModelpointList.Count - 1)
                        {
                            //ps2.Remove("HeightOffset");
                            ps2.SetProperty("HeightOffset", 1);
                        }
                        if (pm2.PolygonToBuilding(oldPolygon, ps2, out newMP, out newModel))
                        {
                            // 设置模型颜色
                            for (int g = 0; g < newModel.GroupCount; g++)
                            {
                                for (int p = 0; p < newModel.GetGroup(g).PrimitiveCount; p++)
                                {
                                    IDrawPrimitive primitive = newModel.GetGroup(g).GetPrimitive(p);
                                    IDrawMaterial material = primitive.Material;
                                    Color userColor = Color.FromArgb(Convert.ToInt32(colorList[i].ToString(), 16));
                                    material.DiffuseColor = userColor;
                                }
                            }
                            // 给模型名加上层数标记
                            string modelName = string.Format("{0}#{1}", newMP.ModelName, i);
                            newMP.ModelName = modelName;
                            // 在内存中临时存储模型
                            this.axRenderControl1.ObjectManager.AddModel(modelName, newModel);
                            modelList.Add(modelName, newModel);
                            // 向上平移中心点坐标
                            newMP.Z = newTotalHeight;
                            newTotalHeight += oldObject.Height;
                            // 可视化临时模型
                            IRenderModelPoint newRMP = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(newMP, null, rootId);
                            newRMP.MaxVisibleDistance = 100000;
                            newRMP.VisibleMask = gviViewportMask.gviViewNone;
                            rModelpointList[i] = new Knight(modelName, newRMP, oldObject.Height);
                        }
                        else
                            MessageBox.Show("参数化建模失败");
                    }
                }
                #endregion

                this.axRenderControl1.ObjectManager.DeleteObject(rmpWhole.Guid);
                rmpWhole = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mpWhole, null, rootId);
                rmpWhole.MaxVisibleDistance = 100000;

                _geoEditor.FinishEdit();
                if (!_geoEditor.StartEditRenderGeometry(rmpWhole, gviGeoEditType.gviGeoEditBoxScale))
                    MessageBox.Show(this.axRenderControl1.GetLastError().ToString());

                // 显示真实模型
                for (int i = 0; i < rModelpointList.Count; i++)
                {
                    Knight upperObject = rModelpointList[i] as Knight;
                    IRenderModelPoint myMP = upperObject.MpObject as IRenderModelPoint;
                    myMP.VisibleMask = gviViewportMask.gviView0;
                }

                return;
            }
            #endregion


            polygonEditing = Geometry as IPolygon;
            if (polygonEditing == null)
                return;
            polygon = polygonEditing;

            // 从模型名中分解出索引值
            if (modelNameToModify == null)
            {
                MessageBox.Show("请选择楼层");
                return;
            }
            string[] strs = modelNameToModify.Split('#');
            int floorIndex = int.Parse(strs[1]);

            Knight currentObj = rModelpointList[floorIndex] as Knight;
            IRenderModelPoint currentRMP = currentObj.MpObject as IRenderModelPoint;
            IModelPoint mp = currentRMP.GetFdeGeometry() as IModelPoint;

            // 降低polygon的z值
            IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            point.SpatialCRS = new CRSFactory().CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS;
            IPolygon polygonZ = polygonEditing.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            IRing ring = polygonZ.ExteriorRing;
            for (int jj = 0; jj < ring.PointCount; jj++)
            {
                point = ring.GetPoint(jj);
                point.Z = point.Z - mp.Envelope.Depth;
                ring.UpdatePoint(jj, point);
            }

            // 修改模型形状
            IParametricModelling pm = new ParametricModelling();
            double floorHeight = double.Parse(numFloorHeight.Value.ToString());
            // 用一层模型重新建模
            IPropertySet ps = new PropertySet();
            ps.SetProperty("FloorNumber", 1);
            ps.SetProperty("FloorHeight", floorHeight);
            if (floorIndex != rModelpointList.Count - 1)
                ps.SetProperty("HeightOffset", 0); //非顶层不需要女儿墙
            else
                ps.SetProperty("HeightOffset", 1);
            tmpTexturePath = (strMediaPath + @"\dds\facade\floor.dds");
            ps.SetProperty("FacadeTexture", tmpTexturePath);

            //IModelPoint mp = null;
            IModel model = null;
            if (pm.PolygonToBuilding(polygonZ, ps, out mp, out model))
            {
                // 设置模型颜色
                for (int g = 0; g < model.GroupCount; g++)
                {
                    for (int p = 0; p < model.GetGroup(g).PrimitiveCount; p++)
                    {
                        IDrawPrimitive primitive = model.GetGroup(g).GetPrimitive(p);
                        IDrawMaterial material = primitive.Material;
                        // 从界面读取颜色
                        Color userColor = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
                        material.DiffuseColor = userColor;
                    }
                }
                mp.ModelName = modelNameToModify;
                // 更新内存中的临时存储模型
                this.axRenderControl1.ObjectManager.AddModel(modelNameToModify, model);
                modelList[modelNameToModify] = model;

                Knight modifyObject = rModelpointList[floorIndex] as Knight;
                // 删除之前的RenderModelPoint
                this.axRenderControl1.ObjectManager.DeleteObject(modifyObject.MpObject.Guid);
                // 可视化临时模型
                IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, null, rootId);
                rmp.MaxVisibleDistance = 100000;
                rModelpointList[floorIndex] = new Knight(modelNameToModify, rmp, floorHeight);

                //重新画包围框
                for (int i = 0; i < rPolylinelist.Count; i++)
                    this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
                rPolylinelist.Clear();
                DrawEnvelope(polygonZ, mp.Z, mp.Envelope.Depth, new CRSFactory().CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS, out rPolylinelist, true);
            }
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            #region BoxScale
            if (modelNameToModify.Contains("whole"))
            {
                this.axRenderControl1.ObjectManager.DeleteModel(modelNameToModify);
                this.axRenderControl1.ObjectManager.DeleteObject(rmpWhole.Guid);
                rmpWhole = null;

                for (int i = 0; i < rModelpointList.Count; i++)
                {
                    Knight upperObject = rModelpointList[i] as Knight;
                    IRenderModelPoint myMP = upperObject.MpObject as IRenderModelPoint;
                    myMP.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }
            #endregion
            else
            {
                // 从模型名中分解出索引值
                if (modelNameToModify == null)
                {
                    MessageBox.Show("请选择楼层");
                    return;
                }
                string[] strs = modelNameToModify.Split('#');
                int floorIndex = int.Parse(strs[1]);

                // 正常化选中模型以上的模型
                int currentIndex = floorIndex;
                for (int ii = currentIndex + 1; ii < rModelpointList.Count; ii++)
                {
                    Knight upperObject = rModelpointList[ii] as Knight;
                    IRenderModelPoint upperMP = upperObject.MpObject as IRenderModelPoint;
                    IModelPointSymbol mpsymbol = new ModelPointSymbol();
                    mpsymbol.EnableColor = false;
                    upperMP.Symbol = mpsymbol;
                    upperMP.MouseSelectMask = gviViewportMask.gviViewAllNormalView;
                    upperObject.MpObject = upperMP;
                    rModelpointList[ii] = upperObject;
                }

                // 删除用于编辑的RenderPolygon对象
                this.axRenderControl1.ObjectManager.DeleteObject(editingRPolygon.Guid);
                // 更新list中的polygon对象
                polygonlist[floorIndex] = polygonEditing.Clone();
                polygon = polygonEditing;
            }

            // 退出顶点编辑模式，进入漫游模式
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer | gviMouseSelectObjectMask.gviSelectRenderGeometry;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        private void btnEditSide_Click(object sender, System.EventArgs e)
        {
            if (polygon != null)
            {
                // 从模型名中分解出索引值
                if (modelNameToModify == null)
                {
                    MessageBox.Show("请选择楼层");
                    return;
                }
                string[] strs = modelNameToModify.Split('#');
                int floorIndex = int.Parse(strs[1]);
                Knight currentObj = rModelpointList[floorIndex] as Knight;
                IRenderModelPoint currentRMP = currentObj.MpObject as IRenderModelPoint;
                IModelPoint mp = currentRMP.GetFdeGeometry() as IModelPoint;
                //重新画包围框
                for (int i = 0; i < rPolylinelist.Count; i++)
                    this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
                rPolylinelist.Clear();
                DrawEnvelope(polygon, mp.Z, mp.Envelope.Depth, new CRSFactory().CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS, out rPolylinelist, false);
                                
                //透明当前选中模型
                IModelPointSymbol mps = new ModelPointSymbol();
                string colorStr = "aa" + colorBox.Text.Substring(2, 6);
                Color userColor = Color.FromArgb(Convert.ToInt32(colorStr, 16));
                mps.Color = userColor;
                mps.EnableColor = true;
                currentRMP.Symbol = mps;

                //删除侧面RenderPolygon
                for (int i = 0; i < rPolygonlist.Count; i++)
                    this.axRenderControl1.ObjectManager.DeleteObject((rPolygonlist[i] as IRenderPolygon).Guid);
                rPolygonlist.Clear();

                // 创建侧面RenderPolygon
                if (rPolylinelist.Count == 2)
                {
                    IPolyline plyBottom = (rPolylinelist[0] as IRenderPolyline).GetFdeGeometry() as IPolyline;
                    IPolyline plyRoof = (rPolylinelist[1] as IRenderPolyline).GetFdeGeometry() as IPolyline;
                    for (int i = 0; i < plyBottom.PointCount - 1; i++)
                    {
                        IPolygon plySide = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                        IRing ring = plySide.ExteriorRing;
                        ring.AppendPoint(plyBottom.GetPoint(i));
                        ring.AppendPoint(plyRoof.GetPoint(i));
                        ring.AppendPoint(plyRoof.GetPoint(i + 1));
                        ring.AppendPoint(plyBottom.GetPoint(i + 1));
                        //ring.AppendPoint(plyBottom.GetPoint(i));
                        //ring.AppendPoint(plyBottom.GetPoint(i + 1));
                        //ring.AppendPoint(plyRoof.GetPoint(i + 1));
                        //ring.AppendPoint(plyRoof.GetPoint(i));                       
                        ring.Close();

                        rPolygonlist.Add(this.axRenderControl1.ObjectManager.CreateRenderPolygon(plySide, null, rootId));

                        ILabel label = this.axRenderControl1.ObjectManager.CreateLabel(rootId);
                        label.Text = i.ToString();
                        label.Position = plyRoof.GetPoint(i);
                        labelList.Add(label);
                    }
                }

                // 进入拾取模式，选择待编辑的侧面
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectRenderGeometry;
                this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                // 设置RenderModelPoint不可拾取
                if (rModelpointList.Count != 0)
                {
                    for (int i = 0; i < rModelpointList.Count; i++)
                    {
                        Knight myObject = rModelpointList[i] as Knight;
                        IRenderModelPoint rmp = myObject.MpObject as IRenderModelPoint;
                        rmp.MouseSelectMask = gviViewportMask.gviViewNone;
                    }
                }

                // 绑定TransformHelperEvents
                helper = this.axRenderControl1.TransformHelper;
                helper.Type = gviEditorType.gviEditorZMove;
                v.Set(0, 0, 0);
                helper.SetPosition(v);
                this.axRenderControl1.RcTransformHelperBegin +=new _IRenderControlEvents_RcTransformHelperBeginEventHandler(axRenderControl1_RcTransformHelperBegin);
                _rcTransformHelperBegin = new _IRenderControlEvents_RcTransformHelperBeginEventHandler(axRenderControl1_RcTransformHelperBegin);
                this.axRenderControl1.RcTransformHelperMoving += new _IRenderControlEvents_RcTransformHelperMovingEventHandler(axRenderControl1_RcTransformHelperMoving);
                _rcTransformHelperMoving = new _IRenderControlEvents_RcTransformHelperMovingEventHandler(axRenderControl1_RcTransformHelperMoving);
                this.axRenderControl1.RcTransformHelperEnd += new _IRenderControlEvents_RcTransformHelperEndEventHandler(axRenderControl1_RcTransformHelperEnd);
                _rcTransformHelperEnd = new _IRenderControlEvents_RcTransformHelperEndEventHandler(axRenderControl1_RcTransformHelperEnd);
            }         
        }


 
 

        int moveCount = 0;
        int verIndex = 0;
        void axRenderControl1_RcTransformHelperBegin()
        {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId != MainThreadId)
            {
                this.BeginInvoke(_rcTransformHelperBegin);
                return;
            }

            moveCount = 0;
            verIndex = 0;
        }

        void axRenderControl1_RcTransformHelperMoving(IVector3 Position)
        {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId != MainThreadId)
            {
                this.BeginInvoke(_rcTransformHelperMoving, new object[] { Position });
                return;
            }

            double dirX = Position.X - beforeX;
            double dirY = Position.Y - beforeY;
            double dirZ = Position.Z - beforeZ;

            if (dirX == 0.0 && dirY == 0.0 && dirZ == 0.0)
                return;

            beforeX = Position.X;
            beforeY = Position.Y;
            beforeZ = Position.Z;

            // 外包框跟着变化
            for (int i = 0; i < rPolygonlist.Count; i++)
            {
                if (sideRPolygon.Guid == (rPolygonlist[i] as IRenderPolygon).Guid)
                {
                    verIndex = i;
                    IRenderPolyline rlineRoof = rPolylinelist[1] as IRenderPolyline;
                    IPolyline lineRoof = rlineRoof.GetFdeGeometry() as IPolyline;
                    IRenderPolyline rlineBottom = rPolylinelist[0] as IRenderPolyline;
                    IPolyline lineBottom = rlineBottom.GetFdeGeometry() as IPolyline;
                    #region 加点
                    if (cbEditSideMode.SelectedIndex == 0)
                    {
                        if (moveCount == 0)
                        {
                            #region 初始时加点
                            {
                                IPoint p1 = lineRoof.GetPoint(i).Clone() as IPoint;
                                IPoint p2 = null;
                                if (i == lineRoof.PointCount - 1)
                                    p2 = lineRoof.GetPoint(0).Clone() as IPoint;
                                else
                                {
                                    p2 = lineRoof.GetPoint(i + 1).Clone() as IPoint;
                                    lineRoof.RemoveSegments(i, 1);
                                }
                                IVector3 posOld = p1.Position;
                                IVector3 posNew = new Vector3();
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p1.Position = posNew;
                                lineRoof.AddPointAfter(i, p1);
                                posOld = p2.Position;
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p2.Position = posNew;
                                lineRoof.AddPointAfter(i + 1, p2);
                                ICurveSymbol cs = new CurveSymbol();
                                cs.Color = Color.Green;
                                cs.Width = -2;
                                rPolylinelist[1] = this.axRenderControl1.ObjectManager.CreateRenderPolyline(lineRoof, cs, rootId);
                                this.axRenderControl1.ObjectManager.DeleteObject(rlineRoof.Guid);
                            }
                            {
                                IPoint p1 = lineBottom.GetPoint(i).Clone() as IPoint;
                                IPoint p2 = null;
                                if (i == lineBottom.PointCount - 1)
                                    p2 = lineBottom.GetPoint(0).Clone() as IPoint;
                                else
                                {
                                    p2 = lineBottom.GetPoint(i + 1).Clone() as IPoint;
                                    lineBottom.RemoveSegments(i, 1);
                                }
                                IVector3 posOld = p1.Position;
                                IVector3 posNew = new Vector3();
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p1.Position = posNew;
                                lineBottom.AddPointAfter(i, p1);
                                posOld = p2.Position;
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p2.Position = posNew;
                                lineBottom.AddPointAfter(i + 1, p2);
                                ICurveSymbol cs = new CurveSymbol();
                                cs.Color = Color.Green;
                                cs.Width = -2;
                                rPolylinelist[0] = this.axRenderControl1.ObjectManager.CreateRenderPolyline(lineBottom, cs, rootId);
                                this.axRenderControl1.ObjectManager.DeleteObject(rlineBottom.Guid);
                            }
                            #endregion
                        }
                        else
                        {
                            #region 继续移动时更新点
                            {
                                IPoint p1 = lineRoof.GetPoint(i + 1).Clone() as IPoint;
                                IPoint p2 = lineRoof.GetPoint(i + 2).Clone() as IPoint;
                                IVector3 posOld = p1.Position;
                                IVector3 posNew = new Vector3();
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p1.Position = posNew;
                                lineRoof.UpdatePoint(i + 1, p1);
                                posOld = p2.Position;
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p2.Position = posNew;
                                lineRoof.UpdatePoint(i + 2, p2);
                                ICurveSymbol cs = new CurveSymbol();
                                cs.Color = Color.Green;
                                cs.Width = -2;
                                rPolylinelist[1] = this.axRenderControl1.ObjectManager.CreateRenderPolyline(lineRoof, cs, rootId);
                                this.axRenderControl1.ObjectManager.DeleteObject(rlineRoof.Guid);
                            }
                            {
                                IPoint p1 = lineBottom.GetPoint(i + 1).Clone() as IPoint;
                                IPoint p2 = lineBottom.GetPoint(i + 2).Clone() as IPoint;
                                IVector3 posOld = p1.Position;
                                IVector3 posNew = new Vector3();
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p1.Position = posNew;
                                lineBottom.UpdatePoint(i + 1, p1);
                                posOld = p2.Position;
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p2.Position = posNew;
                                lineBottom.UpdatePoint(i + 2, p2);
                                ICurveSymbol cs = new CurveSymbol();
                                cs.Color = Color.Green;
                                cs.Width = -2;
                                rPolylinelist[0] = this.axRenderControl1.ObjectManager.CreateRenderPolyline(lineBottom, cs, rootId);
                                this.axRenderControl1.ObjectManager.DeleteObject(rlineBottom.Guid);
                            }
                            #endregion
                        }
                        moveCount++;
                    }
                    #endregion
                    #region 更新点
                    else
                    {
                        {
                            IPoint p1 = lineRoof.GetPoint(i).Clone() as IPoint;
                            IPoint p2 = lineRoof.GetPoint(i + 1).Clone() as IPoint;
                            IVector3 posOld = p1.Position;
                            IVector3 posNew = new Vector3();
                            posNew.X = posOld.X + dirX;
                            posNew.Y = posOld.Y + dirY;
                            posNew.Z = posOld.Z + dirZ;
                            p1.Position = posNew;
                            lineRoof.UpdatePoint(i, p1);
                            if (i == 0) //同时更新最后一个与之重叠的点坐标
                            {
                                p1 = lineRoof.GetPoint(lineRoof.PointCount - 1).Clone() as IPoint;
                                posOld = p1.Position;
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p1.Position = posNew;
                                lineRoof.UpdatePoint(lineRoof.PointCount - 1, p1);
                            }
                            posOld = p2.Position;
                            posNew.X = posOld.X + dirX;
                            posNew.Y = posOld.Y + dirY;
                            posNew.Z = posOld.Z + dirZ;
                            p2.Position = posNew;
                            lineRoof.UpdatePoint(i + 1, p2);
                            if (i == lineRoof.PointCount - 2) //默认第1个点和最后一个点是重合在0点处
                            {
                                p2 = lineRoof.GetPoint(0).Clone() as IPoint;
                                posOld = p2.Position;
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p2.Position = posNew;
                                lineRoof.UpdatePoint(0, p2);
                            }
                            ICurveSymbol cs = new CurveSymbol();
                            cs.Color = System.Drawing.Color.Yellow;
                            cs.Width = -2;
                            rPolylinelist[1] = this.axRenderControl1.ObjectManager.CreateRenderPolyline(lineRoof, cs, rootId);
                            this.axRenderControl1.ObjectManager.DeleteObject(rlineRoof.Guid);
                        }
                        {
                            IPoint p1 = lineBottom.GetPoint(i).Clone() as IPoint;
                            IPoint p2 = lineBottom.GetPoint(i + 1).Clone() as IPoint;
                            IVector3 posOld = p1.Position;
                            IVector3 posNew = new Vector3();
                            posNew.X = posOld.X + dirX;
                            posNew.Y = posOld.Y + dirY;
                            posNew.Z = posOld.Z + dirZ;
                            p1.Position = posNew;
                            lineBottom.UpdatePoint(i, p1);
                            if (i == 0) //同时更新最后一个与之重叠的点坐标
                            {
                                p1 = lineBottom.GetPoint(lineBottom.PointCount - 1).Clone() as IPoint;
                                posOld = p1.Position;
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p1.Position = posNew;
                                lineBottom.UpdatePoint(lineBottom.PointCount - 1, p1);
                            }
                            posOld = p2.Position;
                            posNew.X = posOld.X + dirX;
                            posNew.Y = posOld.Y + dirY;
                            posNew.Z = posOld.Z + dirZ;
                            p2.Position = posNew;
                            lineBottom.UpdatePoint(i + 1, p2);
                            if (i == lineBottom.PointCount - 2) //默认第1个点和最后一个点是重合在0点处
                            {
                                p2 = lineBottom.GetPoint(0).Clone() as IPoint;
                                posOld = p2.Position;
                                posNew.X = posOld.X + dirX;
                                posNew.Y = posOld.Y + dirY;
                                posNew.Z = posOld.Z + dirZ;
                                p2.Position = posNew;
                                lineBottom.UpdatePoint(0, p2);
                            }
                            ICurveSymbol cs = new CurveSymbol();
                            cs.Color = System.Drawing.Color.Yellow;
                            cs.Width = -2;
                            rPolylinelist[0] = this.axRenderControl1.ObjectManager.CreateRenderPolyline(lineBottom, cs, rootId);
                            this.axRenderControl1.ObjectManager.DeleteObject(rlineBottom.Guid);
                        }
                    }
                    #endregion
                }
            }
        }

        void axRenderControl1_RcTransformHelperEnd()
        {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId != MainThreadId)
            {
                this.BeginInvoke(_rcTransformHelperEnd);
                return;
            }

            #region 更新模型
            // 由polyline构造polygon
            IRenderPolyline rlineBottom = rPolylinelist[0] as IRenderPolyline;
            IPolyline lineBottom = rlineBottom.GetFdeGeometry() as IPolyline;
            polygon = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            IRing exteriorRing = polygon.ExteriorRing;
            for (int ii = 0; ii < lineBottom.PointCount; ii++)
            {
                exteriorRing.AppendPoint(lineBottom.GetPoint(ii));
            }
            exteriorRing.Close();

            // 从模型名中分解出索引值
            if (modelNameToModify == null)
            {
                MessageBox.Show("请选择楼层");
                return;
            }
            string[] strs = modelNameToModify.Split('#');
            int floorIndex = int.Parse(strs[1]);

            Knight currentObj = rModelpointList[floorIndex] as Knight;
            IRenderModelPoint currentRMP = currentObj.MpObject as IRenderModelPoint;
            IModelPoint mp = currentRMP.GetFdeGeometry() as IModelPoint;

            // 修改模型形状
            IParametricModelling pm = new ParametricModelling();
            double floorHeight = double.Parse(numFloorHeight.Value.ToString());
            // 用一层模型重新建模
            IPropertySet ps = new PropertySet();
            ps.SetProperty("FloorNumber", 1);
            ps.SetProperty("FloorHeight", floorHeight);
            if (floorIndex != rModelpointList.Count - 1)
                ps.SetProperty("HeightOffset", 0); //非顶层不需要女儿墙
            else
                ps.SetProperty("HeightOffset", 1);
            tmpTexturePath = (strMediaPath + @"\dds\facade\floor.dds");
            ps.SetProperty("FacadeTexture", tmpTexturePath);

            IModel model = null;
            if (pm.PolygonToBuilding(polygon, ps, out mp, out model))
            {
                // 设置模型颜色
                for (int g = 0; g < model.GroupCount; g++)
                {
                    for (int p = 0; p < model.GetGroup(g).PrimitiveCount; p++)
                    {
                        IDrawPrimitive primitive = model.GetGroup(g).GetPrimitive(p);
                        IDrawMaterial material = primitive.Material;
                        // 从界面读取颜色
                        Color userColor = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
                        material.DiffuseColor = userColor;
                    }
                }
                mp.ModelName = modelNameToModify;
                // 更新内存中的临时存储模型
                this.axRenderControl1.ObjectManager.AddModel(modelNameToModify, model);
                modelList[modelNameToModify] = model;

                Knight modifyObject = rModelpointList[floorIndex] as Knight;
                // 删除之前的RenderModelPoint
                this.axRenderControl1.ObjectManager.DeleteObject(modifyObject.MpObject.Guid);
                // 可视化临时模型
                IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, null, rootId);
                rmp.MaxVisibleDistance = 100000;
                rmp.MouseSelectMask = gviViewportMask.gviViewNone;
                rModelpointList[floorIndex] = new Knight(modelNameToModify, rmp, floorHeight);

                {
                    //透明当前选中模型
                    IModelPointSymbol mps = new ModelPointSymbol();
                    string colorStr = "aa" + colorBox.Text.Substring(2, 6);
                    Color userColor = Color.FromArgb(Convert.ToInt32(colorStr, 16));
                    mps.Color = userColor;
                    mps.EnableColor = true;
                    rmp.Symbol = mps;
                }                
            }
            #endregion

            //重新画包围框
            for (int i = 0; i < rPolylinelist.Count; i++)
                this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
            rPolylinelist.Clear();
            DrawEnvelope(polygon, mp.Z, mp.Envelope.Depth, new CRSFactory().CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS, out rPolylinelist, false);

            //更新polygonlist，供拾取用
            polygonlist[floorIndex] = polygon.Clone();

            //重新画顶点标签
            for (int ll = 0; ll < labelList.Count; ll++)
                this.axRenderControl1.ObjectManager.DeleteObject((labelList[ll] as ILabel).Guid);
            labelList.Clear();

            //删除侧面RenderPolygon
            for (int i = 0; i < rPolygonlist.Count; i++)
                this.axRenderControl1.ObjectManager.DeleteObject((rPolygonlist[i] as IRenderPolygon).Guid);
            rPolygonlist.Clear();

            // 创建侧面RenderPolygon
            if (rPolylinelist.Count == 2)
            {
                IPolyline plyBottom = (rPolylinelist[0] as IRenderPolyline).GetFdeGeometry() as IPolyline;
                IPolyline plyRoof = (rPolylinelist[1] as IRenderPolyline).GetFdeGeometry() as IPolyline;
                for (int i = 0; i < plyBottom.PointCount - 1; i++)
                {
                    IPolygon plySide = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                    IRing ring = plySide.ExteriorRing;
                    ring.AppendPoint(plyBottom.GetPoint(i));
                    ring.AppendPoint(plyRoof.GetPoint(i));
                    ring.AppendPoint(plyRoof.GetPoint(i + 1));
                    ring.AppendPoint(plyBottom.GetPoint(i + 1));
                    ring.Close();
                    rPolygonlist.Add(this.axRenderControl1.ObjectManager.CreateRenderPolygon(plySide, null, rootId));

                    ILabel label = this.axRenderControl1.ObjectManager.CreateLabel(rootId);
                    label.Text = i.ToString();
                    label.Position = plyRoof.GetPoint(i);
                    labelList.Add(label);
                }
            }

            // 更新当前编辑RenderPolygon
            if (cbEditSideMode.SelectedIndex == 0)
                sideRPolygon = rPolygonlist[verIndex + 1] as IRenderPolygon;
            else
                sideRPolygon = rPolygonlist[verIndex] as IRenderPolygon;
            beforeX = sideRPolygon.Envelope.Center.X;
            beforeY = sideRPolygon.Envelope.Center.Y;
            beforeZ = sideRPolygon.Envelope.Center.Z;
            // 高亮拾取到的侧面
            ISurfaceSymbol ss = new SurfaceSymbol();
            ss.Color = System.Drawing.Color.Yellow;
            sideRPolygon.Symbol = ss;
        }

        bool axRenderControl1_RcKeyUp(uint Flags, uint Ch)
        {
            

            if (Ch == 27)   //esc
            {
                // 恢复半透的模型
                if (modelNameToModify == null)
                {
                    MessageBox.Show("请选择楼层");
                }
                string[] strs = modelNameToModify.Split('#');
                int floorIndex = int.Parse(strs[1]);
                Knight currentObj = rModelpointList[floorIndex] as Knight;
                IRenderModelPoint currentRMP = currentObj.MpObject as IRenderModelPoint;
                IModelPointSymbol defaultSymbol = new ModelPointSymbol();
                currentRMP.Symbol = defaultSymbol;

                //删除侧面RenderPolygon
                for (int i = 0; i < rPolygonlist.Count; i++)
                    this.axRenderControl1.ObjectManager.DeleteObject((rPolygonlist[i] as IRenderPolygon).Guid);
                rPolygonlist.Clear();
                
                //删除顶点标签
                for (int ll = 0; ll < labelList.Count; ll++)
                    this.axRenderControl1.ObjectManager.DeleteObject((labelList[ll] as ILabel).Guid);
                labelList.Clear();

                if (sideRPolygon != null)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(sideRPolygon.Guid);
                    sideRPolygon = null;
                }                

                // 退出编辑模式，进入漫游模式
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer | gviMouseSelectObjectMask.gviSelectRenderGeometry;
                this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                // 设置RenderModelPoint参与拾取
                if (rModelpointList.Count != 0)
                {
                    for (int i = 0; i < rModelpointList.Count; i++)
                    {
                        Knight myObject = rModelpointList[i] as Knight;
                        IRenderModelPoint rmp = myObject.MpObject as IRenderModelPoint;
                        rmp.MouseSelectMask = gviViewportMask.gviViewAllNormalView;
                    }
                }

                // 解除绑定TransformHelperEvents
                helper.Type = gviEditorType.gviEditorNone;
                this.axRenderControl1.RcTransformHelperBegin -=new Gvitech.CityMaker.Controls._IRenderControlEvents_RcTransformHelperBeginEventHandler(axRenderControl1_RcTransformHelperBegin);
                _rcTransformHelperBegin = null;
                this.axRenderControl1.RcTransformHelperMoving -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcTransformHelperMovingEventHandler(axRenderControl1_RcTransformHelperMoving);
                _rcTransformHelperMoving = null;
                this.axRenderControl1.RcTransformHelperEnd -=new Gvitech.CityMaker.Controls._IRenderControlEvents_RcTransformHelperEndEventHandler(axRenderControl1_RcTransformHelperEnd);
                _rcTransformHelperEnd = null;
            }
            return false;
        }

        private void btnBoxScale_Click(object sender, EventArgs e)
        {
            if (rModelpointList.Count == 0)
                return;

            //删除画包围框的RenderPolyline
            for (int i = 0; i < rPolylinelist.Count; i++)
                this.axRenderControl1.ObjectManager.DeleteObject((rPolylinelist[i] as IRenderPolyline).Guid);
            rPolylinelist.Clear();

            // 计算z方向上总高度
            totalHeight = 0.0;
            for (int i = 0; i < rModelpointList.Count; i++)
            {
                Knight myObject = rModelpointList[i] as Knight;
                totalHeight += myObject.Height;
            }

            polygonForBoxScale = polygonInLayer as IPolygon;  //留给Editing事件用，累计缩放变换

            rotateAngle = Polygon2Model(polygonForBoxScale.Clone() as IPolygon, totalHeight);

            rmpWhole = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mpWhole, null, rootId);
            rmpWhole.MaxVisibleDistance = 100000;

            // 开启顶点编辑模式
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectRenderGeometry;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            _geoEditor.FinishEdit();
            if(!_geoEditor.StartEditRenderGeometry(rmpWhole, gviGeoEditType.gviGeoEditBoxScale))
                MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
        }

        double Polygon2Model(IPolygon polygon, double height/*, out IModel model, out IModelPoint mp*/)
        {
            // 找最长边
            double maxEdge = 0.0;
            int maxVerIndex = 0, maxVerIndexNext = 0;
            int vertexCount = polygon.ExteriorRing.PointCount;
            for (int i = 0; i < vertexCount; i++)
            {
                IPoint curP = polygon.ExteriorRing.GetPoint(i);
                IProximityOperator oper = curP as IProximityOperator;
                int nextPIndex = 0;
                if (i != vertexCount - 1)
                    nextPIndex = i + 1;
                IPoint nextP = polygon.ExteriorRing.GetPoint(nextPIndex);
                double dis = oper.Distance2D(nextP);
                if (dis > maxEdge)
                {
                    maxEdge = dis;
                    maxVerIndex = i;
                    maxVerIndexNext = nextPIndex;
                }
            }
            // 构建局部坐标系
            IVector3 v0 = new Vector3();
            v0.SetByVector(polygon.ExteriorRing.GetPoint(maxVerIndex).Position);
            IVector3 v1 = new Vector3();
            v1.SetByVector(polygon.ExteriorRing.GetPoint(maxVerIndexNext).Position);
            v0.MultiplyByScalar(-1);
            IVector3 axisX = v1.Add(v0);
            axisX.Normalize();
            if (axisX.X < 0)
                axisX.MultiplyByScalar(-1);
            // 计算二维旋转角度
            IVector3 oldX = new Vector3();
            oldX.Set(1, 0, 0);
            double dotProduct = axisX.DotProduct(oldX);
            double theta = Math.Acos(dotProduct);
            if (axisX.Y < 0)
                theta = -theta;
            // 原Polygon各顶点乘上旋转矩阵
            for (int i = 0; i < vertexCount; i++)
            {
                IPoint curP = polygon.ExteriorRing.GetPoint(i);
                double newX = curP.X * Math.Cos(-theta) - curP.Y * Math.Sin(-theta);
                double newY = curP.X * Math.Sin(-theta) + curP.Y * Math.Cos(-theta);
                curP.X = newX;
                curP.Y = newY;
                polygon.ExteriorRing.UpdatePoint(i, curP);
            }
            // 计算ModelEnvelope
            double minX = double.MaxValue, minY = double.MaxValue, maxX = double.MinValue, maxY = double.MinValue;
            for (int i = 0; i < vertexCount; i++)
            {
                IPoint curP = polygon.ExteriorRing.GetPoint(i);
                if (curP.X < minX)
                    minX = curP.X;
                if (curP.X > maxX)
                    maxX = curP.X;
                if (curP.Y < minY)
                    minY = curP.Y;
                if (curP.Y > maxY)
                    maxY = curP.Y;
            }
            double w = maxX - minX;
            double h = maxY - minY;
            envWhole = new Envelope();
            envWhole.Set(-0.5 * w, 0.5 * w, -0.5 * h, 0.5 * h, 0, height);
            // 计算中心点坐标
            IVector3 centerPos = new Vector3();
            centerPos.X = minX + 0.5 * w;
            centerPos.Y = minY + 0.5 * h;
            centerPos.Z = 0;
            mat.Set(Math.Cos(theta), Math.Sin(theta), 0, 0,
                    -Math.Sin(theta), Math.Cos(theta), 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1);
            IVector3 centerPosOld = mat.MultiplyVector(centerPos);
            // 计算modelpoint的matrix33
            IVector3 axisY = new Vector3();
            axisY.Set(-Math.Sin(theta), Math.Cos(theta), 0);
            axisY.Normalize();
            IVector3 axisZ = axisX.CrossProduct(axisY);
            float[] Matrix = new float[9];
            Matrix[0] = (float)axisX.X; Matrix[1] = (float)axisX.Y; Matrix[2] = (float)axisX.Z;
            Matrix[3] = (float)axisY.X; Matrix[4] = (float)axisY.Y; Matrix[5] = (float)axisY.Z;
            Matrix[6] = (float)axisZ.X; Matrix[7] = (float)axisZ.Y; Matrix[8] = (float)axisZ.Z;

            // 构建model
            modelWhole = new ResourceFactory().CreateModel();
            DrawGroup group = new DrawGroup();
            IDrawPrimitive primitive = new DrawPrimitive();
            IDrawMaterial material = new DrawMaterial();
            material.DiffuseColor = Color.FromArgb(Convert.ToInt32("0x550000ff", 16)); 
            material.CullMode = gviCullFaceMode.gviCullNone;

            IFloatArray va = new FloatArray();
            for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
            {
                IPoint curP = polygon.ExteriorRing.GetPoint(i);
                va.Append((float)(curP.X - centerPos.X));
                va.Append((float)(curP.Y - centerPos.Y));
                va.Append((float)(curP.Z - centerPos.Z));
                va.Append((float)(curP.X - centerPos.X));
                va.Append((float)(curP.Y - centerPos.Y));
                va.Append((float)(curP.Z - centerPos.Z + height));
            }
            // 计算索引坐标
            IUInt16Array ia = new UInt16Array();
            for (int i = 0; i < va.Length / 6 - 1; i++)
            {
                ia.Append((ushort)(2 * i));
                ia.Append((ushort)(2 * i + 1));
                ia.Append((ushort)(2 * i + 2));
                ia.Append((ushort)(2 * i + 1));
                ia.Append((ushort)(2 * i + 3));
                ia.Append((ushort)(2 * i + 2));
            }
            primitive.VertexArray = va;
            primitive.IndexArray = ia;
            primitive.Material = material;
            group.AddPrimitive(primitive);
            modelWhole.AddGroup(group);

            string modelName = string.Format("{0}#whole", Guid.NewGuid());
            modelNameToModify = modelName;
            this.axRenderControl1.ObjectManager.AddModel(modelName, modelWhole);

            mpWhole = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
            mpWhole.ModelName = modelName;
            mpWhole.Position = centerPosOld;
            mpWhole.ModelEnvelope = envWhole;
            mpWhole.Matrix33 = Matrix;

            return theta;
        }

        void CombineModel()
        {
            //把零散的modelpoint整合成一整个modelpoint
            modelWhole = new ResourceFactory().CreateModel();
            totalHeight = 0.0;
            lastHeight = 0.0;
            envWhole = new Envelope();

            for (int i = 0; i < rModelpointList.Count; i++)
            {
                Knight myObject = rModelpointList[i] as Knight;
                //合并model
                IModel myModel = CloneModel((IModel)modelList[myObject.ModelName]);
                mat.Set(1, 0, 0, 0,
                        0, 1, 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1);
                // 计算z方向上位移
                totalHeight += lastHeight;
                lastHeight = myObject.Height;
                // 计算xy方向上位移
                IPolygon pp = polygonlist[i] as IPolygon;
                if (pp.Envelope.Center != polygonInLayer.Envelope.Center)
                {
                    double deltaX = pp.Envelope.Center.X - polygonInLayer.Envelope.Center.X;
                    double deltaY = pp.Envelope.Center.Y - polygonInLayer.Envelope.Center.Y;
                    v.Set(deltaX, deltaY, totalHeight);
                }
                else
                    v.Set(0, 0, totalHeight);
                mat.SetTranslate(v);
                myModel.MultiplyMatrix(mat);
                for (int j = 0; j < myModel.GroupCount; j++)
                    modelWhole.AddGroup(myModel.GetGroup(j));
                envWhole.ExpandByEnvelope(myModel.Envelope);
            }
        }

        IMatrix MultiplyMatrix(IMatrix matrix1, IMatrix matrix2)
        {
            IMatrix newMatrix = new Matrix();
            newMatrix.M11 = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
            newMatrix.M12 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
            newMatrix.M13 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
            newMatrix.M14 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;

            newMatrix.M21 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
            newMatrix.M22 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
            newMatrix.M23 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
            newMatrix.M24 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;

            newMatrix.M31 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
            newMatrix.M32 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
            newMatrix.M33 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
            newMatrix.M34 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;

            newMatrix.M41 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
            newMatrix.M42 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
            newMatrix.M43 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
            newMatrix.M44 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
            return newMatrix;
        }

        IModel CloneModel(IModel model)
        {
            IModel newModel = new ResourceFactory().CreateModel();
            for (int i = 0; i < model.GroupCount; i++)
            {
                IDrawGroup newGroup = new DrawGroup();
                IDrawGroup group = model.GetGroup(i);
                newGroup.CompleteMapFactor = group.CompleteMapFactor;
                newGroup.CompleteMapTextureName = group.CompleteMapTextureName;
                newGroup.LightMapTextureName = group.LightMapTextureName;
                for (int j = 0; j < group.PrimitiveCount; j++)
                {
                    IDrawPrimitive newPrimitive = new DrawPrimitive();
                    IDrawPrimitive primitive = group.GetPrimitive(j);

                    if (primitive.BakedTexcoordArray != null)
                    {
                        IFloatArray newBakedTexcoordArray = new FloatArray();
                        for (int k = 0; k < primitive.BakedTexcoordArray.Length; k++)
                            newBakedTexcoordArray.Append(primitive.BakedTexcoordArray.Get(k));
                        newPrimitive.BakedTexcoordArray = newBakedTexcoordArray;
                    }

                    if (primitive.ColorArray != null)
                    {
                        IUInt32Array newColorArray = new UInt32Array();
                        for (int k = 0; k < primitive.ColorArray.Length; k++)
                            newColorArray.Append(primitive.ColorArray.Get(k));
                        newPrimitive.ColorArray = newColorArray;
                    }

                    if (primitive.IndexArray != null)
                    {
                        IUInt16Array newIndexArray = new UInt16Array();
                        for (int k = 0; k < primitive.IndexArray.Length; k++)
                            newIndexArray.Append(primitive.IndexArray.Get(k));
                        newPrimitive.IndexArray = newIndexArray;
                    }

                    if (primitive.NormalArray != null)
                    {
                        IFloatArray newNormalArray = new FloatArray();
                        for (int k = 0; k < primitive.NormalArray.Length; k++)
                            newNormalArray.Append(primitive.NormalArray.Get(k));
                        newPrimitive.NormalArray = newNormalArray;
                    }

                    if (primitive.TexcoordArray != null)
                    {
                        IFloatArray newTexcoordArray = new FloatArray();
                        for (int k = 0; k < primitive.TexcoordArray.Length; k++)
                            newTexcoordArray.Append(primitive.TexcoordArray.Get(k));
                        newPrimitive.TexcoordArray = newTexcoordArray;
                    }

                    if (primitive.VertexArray != null)
                    {
                        IFloatArray newVertexArray = new FloatArray();
                        for (int k = 0; k < primitive.VertexArray.Length; k++)
                            newVertexArray.Append(primitive.VertexArray.Get(k));
                        newPrimitive.VertexArray = newVertexArray;
                    }

                    newPrimitive.Material = primitive.Material;
                    newPrimitive.PrimitiveMode = primitive.PrimitiveMode;
                    newPrimitive.PrimitiveType = primitive.PrimitiveType;

                    newGroup.AddPrimitive(newPrimitive);
                }
                newModel.AddGroup(newGroup);
            }

            return newModel;
        }

        void printfMatrix(IMatrix mm)
        {
            Logger.WriteMsg(mm.M11 + "\t" + mm.M12 + "\t" + mm.M13 + "\t" + mm.M14);
            Logger.WriteMsg(mm.M21 + "\t" + mm.M22 + "\t" + mm.M23 + "\t" + mm.M24);
            Logger.WriteMsg(mm.M31 + "\t" + mm.M32 + "\t" + mm.M33 + "\t" + mm.M34);
            Logger.WriteMsg(mm.M41 + "\t" + mm.M42 + "\t" + mm.M43 + "\t" + mm.M44);
            Logger.WriteMsg("");
        }

        void printfEulerAngle(IEulerAngle ang)
        {
            Logger.WriteMsg(ang.Heading + "\t" + ang.Tilt + "\t" + ang.Roll);
            Logger.WriteMsg("");
        }

        void printfVector(IVector3 vec)
        {
            Logger.WriteMsg(vec.X + "\t" + vec.Y + "\t" + vec.Z);
            Logger.WriteMsg("");
        }
    }

    #region "business object"
    public class Knight
    {
        private string modelName;
        private IRenderModelPoint mpObject;
        //private double zValue;
        private double height;

        public Knight(string n, IRenderModelPoint o, /*double z,*/ double h)
        {
            modelName = n;
            mpObject = o;
            //zValue = z;
            height = h;
        }

        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        public IRenderModelPoint MpObject
        {
            get { return mpObject; }
            set { mpObject = value; }
        }

        //public double ZValue
        //{
        //    get { return zValue; }
        //    set { zValue = value; }
        //}

        public double Height
        {
            get { return height; }
            set { height = value; }
        }
    }
    #endregion
}
