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
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Controls;

namespace DriveRoad
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private System.Guid rootId = new System.Guid();
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEnvelope env;//加载数据时，初始化的矩形范围
        private ISpatialCRS datasetCRS = null;
        IPolyline polyline = null; //拾取时选中的polyline 
        int fid = 0;
        double width = 0.0;
        double length = 0.0;
        double lastV = 0.0;
        IVector3 center = new Vector3(); //选中的polyline外包框的中心点

        IPoint curPoint = null;
        IPoint nextPoint = null;
        IPoint P = null;
        IPoint Q = null;

        IVector3 vecCurPos = new Vector3();
        IVector3 vecNextPos = new Vector3();
        IVector3 vecDirect = new Vector3();
        IVector3 vecZ = new Vector3();
        IVector3 vecTarget = new Vector3();
        IVector3 vecP = new Vector3();
        IVector3 vecQ = new Vector3();
        
        ArrayList rPointToDelList = new ArrayList();  //拾取时DrawEnvelope产生的RenderPolyline
        ArrayList rModelpointToDelList = new ArrayList();  //之前生成的RenderModelPoint待删除

        // 线程转发
        public int MainThreadId = 0;
        

        private void init()
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
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\7_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\7_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\7_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\7_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\7_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\7_UP.jpg");
            }
            else
            {
                MessageBox.Show("请不要随意更改SDK目录名");
                return;
            }

            #region 加载shp
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionShapeFile;
                string tmpFDBPath = (strMediaPath + @"\shp\road\road.shp");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);
                datasetCRS = dataset.SpatialReference;
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
                        IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        point.Position = env.Center;
                        point.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                        this.axRenderControl1.Camera.LookAt2(point, 1000, angle);
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


            init();

            // 注册控件拾取事件
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DriveRoad.html";
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
                    // 高亮Polygon
                    this.axRenderControl1.FeatureManager.UnhighlightAll();
                    IFeatureLayerPickResult flpr = pr as IFeatureLayerPickResult;
                    fid = flpr.FeatureId;
                    IFeatureLayer fl = flpr.FeatureLayer;
                    foreach (IFeatureClass fc in fcMap.Keys)
                    {
                        if (fc.Guid.Equals(fl.FeatureClassId))
                        {
                            IRowBuffer fdeRow = fc.GetRow(fid);
                            IFieldInfoCollection col = fdeRow.Fields;
                            for (int i = 0; i < col.Count; ++i)
                            {
                                IFieldInfo info = col.Get(i);
                                if (info.GeometryDef != null &&
                                    info.GeometryDef.GeometryColumnType == gviGeometryColumnType.gviGeometryColumnPolyline)
                                {
                                    int nPos = fdeRow.FieldIndex(info.Name);
                                    polyline = fdeRow.GetValue(nPos) as IPolyline;
                                    this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Yellow);
                                    //获取路宽
                                    nPos = fdeRow.FieldIndex("WIDTH");
                                    //width = (double)fdeRow.GetValue(nPos);  
                                    width = 10;
                                }
                            }
                        } // end " if (fc.Guid.Equals(fl.FeatureClassId))"
                    } // end "foreach (IFeatureClass fc in fcMap.Keys)"
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
                if (polyline != null)
                {
                    center = polyline.Envelope.Center;
                    IModelPoint mp = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                    mp.SpatialCRS = datasetCRS;
                    mp.Position = center;

                    IModel model = new ResourceFactory().CreateModel();
                    IDrawGroup group = new DrawGroup();
                    IDrawPrimitive primitive = new DrawPrimitive();
                    IDrawMaterial material = new DrawMaterial();
                    material.TextureName = (strMediaPath + @"\shp\road\textrure.jpg");
                    material.CullMode = gviCullFaceMode.gviCullNone;
                    material.WrapModeS = gviTextureWrapMode.gviTextureWrapRepeat;
                    material.WrapModeT = gviTextureWrapMode.gviTextureWrapRepeat;
                    IFloatArray va = new FloatArray();
                    IFloatArray ta = new FloatArray();
                    // 逐点外扩
                    for (int i = 0; i < polyline.PointCount; i++)
                    {
                        #region 单独处理最后一个点
                        if (i == polyline.PointCount - 1)
                        {
                            curPoint = polyline.GetPoint(i);
                            vecCurPos = curPoint.Position;
                            // 最后一个点重用最后的方向向量
                            vecTarget = vecDirect.CrossProduct(vecZ);
                            vecTarget.Normalize();
                            vecTarget.MultiplyByScalar(width / 2);
                            vecP = vecCurPos.Add(vecTarget);
                            vecTarget.MultiplyByScalar(-1);
                            vecQ = vecCurPos.Add(vecTarget);
                            // 设置外扩点
                            P = curPoint.Clone() as IPoint;
                            P.Position = vecP;
                            Q = curPoint.Clone() as IPoint;
                            Q.Position = vecQ;
                            // 把点坐标加进顶点数组
                            va.Append((float)(vecP.X - center.X));
                            va.Append((float)(vecP.Y - center.Y));
                            va.Append((float)(vecP.Z - center.Z));
                            va.Append((float)(vecQ.X - center.X));
                            va.Append((float)(vecQ.Y - center.Y));
                            va.Append((float)(vecQ.Z - center.Z));
                            // 加纹理坐标
                            ta.Append(0);  //P点纹理
                            if (i == 0)
                                lastV = 0.0;
                            else
                                lastV = lastV + length / 10;  //v方向上每隔10米重复一次
                            ta.Append((float)lastV);
                            ta.Append(1);  //Q点纹理
                            ta.Append((float)lastV);

                            {
                                ISimplePointSymbol ps = new SimplePointSymbol();
                                ps.FillColor = System.Drawing.Color.Yellow;
                                ps.Size = 5;
                                rPointToDelList.Add(this.axRenderControl1.ObjectManager.CreateRenderPoint(P, ps, rootId));
                                rPointToDelList.Add(this.axRenderControl1.ObjectManager.CreateRenderPoint(Q, ps, rootId));     
                            }
                            
                            break;
                        }
                        #endregion

                        // 当不是最后一个点时：
                        curPoint = polyline.GetPoint(i);
                        nextPoint = polyline.GetPoint(i + 1);
                        vecCurPos = curPoint.Position;
                        vecNextPos = nextPoint.Position;
                        // 运算
                        vecNextPos.MultiplyByScalar(-1);
                        vecDirect = vecCurPos.Add(vecNextPos);  //方向向量
                        vecZ.Set(0, 0, 1);
                        vecTarget = vecDirect.CrossProduct(vecZ);
                        vecTarget.Normalize();
                        vecTarget.MultiplyByScalar(width / 2);
                        vecP = vecCurPos.Add(vecTarget);
                        vecTarget.MultiplyByScalar(-1);
                        vecQ = vecCurPos.Add(vecTarget);
                        // 设置外扩点
                        P = curPoint.Clone() as IPoint;
                        P.Position = vecP;
                        Q = curPoint.Clone() as IPoint;
                        Q.Position = vecQ;
                        // 把点坐标加进顶点数组
                        va.Append((float)(vecP.X - center.X));
                        va.Append((float)(vecP.Y - center.Y));
                        va.Append((float)(vecP.Z - center.Z));
                        va.Append((float)(vecQ.X - center.X));
                        va.Append((float)(vecQ.Y - center.Y));
                        va.Append((float)(vecQ.Z - center.Z));
                        // 加纹理坐标
                        ta.Append(0);  //P点纹理
                        if (i == 0)
                            lastV = 0.0;
                        else
                            lastV = lastV + length / 5;  //v方向上每隔10米重复一次
                        length = vecDirect.Length;  //计算长度给奇数点用
                        ta.Append((float)lastV);
                        ta.Append(1);  //Q点纹理
                        ta.Append((float)lastV);

                        {
                            ISimplePointSymbol ps = new SimplePointSymbol();
                            ps.FillColor = System.Drawing.Color.Yellow;
                            ps.Size = 5;
                            rPointToDelList.Add(this.axRenderControl1.ObjectManager.CreateRenderPoint(P, ps, rootId));
                            rPointToDelList.Add(this.axRenderControl1.ObjectManager.CreateRenderPoint(Q, ps, rootId));                                                 
                        }                        
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
                    primitive.TexcoordArray = ta;
                    primitive.IndexArray = ia;
                    primitive.Material = material;
                    group.AddPrimitive(primitive);
                    model.AddGroup(group);

                    // 在内存中临时存储模型
                    string modelName = fid.ToString();
                    this.axRenderControl1.ObjectManager.AddModel(modelName, model);
                    mp.ModelName = modelName;
                    mp.ModelEnvelope = model.Envelope;
                    // 可视化临时模型
                    IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, null, rootId);
                    rmp.MaxVisibleDistance = 100000;
                    rmp.MouseSelectMask = gviViewportMask.gviViewNone;
                    rModelpointToDelList.Add(rmp);
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

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            //删除RenderPoint
            for (int j = 0; j < rPointToDelList.Count; j++)
                this.axRenderControl1.ObjectManager.DeleteObject((rPointToDelList[j] as IRenderPoint).Guid);
            rPointToDelList.Clear();
            //删除RenderModelPoint
            for (int k = 0; k < rModelpointToDelList.Count; k++)
                this.axRenderControl1.ObjectManager.DeleteObject((rModelpointToDelList[k] as IRenderModelPoint).Guid);
            rModelpointToDelList.Clear();
        }
    }

}
