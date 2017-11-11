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
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Resource;
using Gvitech;
using Gvitech.CityMaker.Controls;

namespace GeometryEdit
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private Hashtable fcGUIDMap = null;  //featureclass.GUID, IFeatureClass 存储featureclass及对应的GUID
        private double envX = 0.0, envY = 0.0, envZ = 0.0;  //定位FeatureLayer时用

        private IObjectEditor _geoEditor = null; 
        private bool resultCode;

        private IGeometryFactory gfactory = null;
        private IModelPoint fde_modelpoint = null;
        private IRenderModelPoint rmodelpoint = null;
        private IPoint fde_point = null;
        private IRenderPoint rpoint = null;
        private IPolyline fde_polyline = null;
        private IRenderPolyline rpolyline = null;
        private IPolygon fde_polygon = null;
        private IRenderPolygon rpolygon = null;        

        private IFeatureClass _featureClass = null;   
        private IFeatureLayer _featureLayer = null;
        private IRowBuffer _buffer = null;  //标记当前正在编辑的Feature
        private IGeometry oldfdeGeometry = null;  //记录原始Feature，用于存数据库失败时恢复位置

        private IRenderGeometry currentGeometry = null;

        private ISpatialCRS crs = null;

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


            //错误日志
            Logger.Create(Environment.CurrentDirectory); //Application.LocalUserAppDataPath

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

            #region 加载FDB场景
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
                fcGUIDMap = new Hashtable(fcnames.Length);
                foreach (string name in fcnames)
                {
                    IFeatureClass fc = dataset.OpenFeatureClass(name);
                    fcGUIDMap.Add(fc.Guid, fc);
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
                        // 获取定位featurelayer时要飞到的坐标信息
                        envX = env.Center.X;
                        envY = env.Center.Y;
                        envZ = env.Center.Z;
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "GeometryEditor.html";
            }

            // 进入编辑模式
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            setEnableEditButtons(true, false);

            //// 俯视
            //IVector3 position = new Vector3();
            //position.Set(28.66, -169.75, 258.83);
            //IEulerAngle angle = new EulerAngle();
            //angle.Set(0, -20, 0);
            //this.axRenderControl1.Camera.SetCamera(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);

            // 注册交互编辑事件
            _geoEditor = this.axRenderControl1.ObjectEditor;
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish+= new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            
        }

        private void btnFlyToFeatureLayer_Click(object sender, EventArgs e)
        {
            IVector3 position = new Vector3();
            position.Set(envX, envY, envZ);
            IEulerAngle angle = new EulerAngle();
            angle.Set(0, -20, 0);
            this.axRenderControl1.Camera.LookAt(position, 800, angle);
        }

        private void setEnableEditButtons(bool isUseful1, bool isUseful2)
        {
            this.btnCreateModelPoint.Enabled = isUseful1;
            this.btnCreatePoint.Enabled = isUseful1;
            this.btnCreatePolygon.Enabled = isUseful1;
            this.btnCreatePolyline.Enabled = isUseful1;
            this.btnCreateFeature.Enabled = isUseful1;

            this.rbMove.Enabled = isUseful2;
            this.rbRotate.Enabled = isUseful2;
            this.rbScale.Enabled = isUseful2;
            this.rbVertex.Enabled = isUseful2;
            this.rbZrotate.Enabled = isUseful2;
            this.rbZscale.Enabled = isUseful2;
            this.rbBoxScale.Enabled = isUseful2;
            this.btnCancelEdit.Enabled = isUseful2;
        }

        #region 创建
        private void btnCreateModelPoint_Click(object sender, EventArgs e)
        {
            _geoEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时  

            if (gfactory == null)
                gfactory = new GeometryFactory();

            string tmpOsgPath = (strMediaPath + @"\osg\Buildings\Apartment\Apartment.osg");
            fde_modelpoint = gfactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint,
                gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
            fde_modelpoint.SetCoords(0, 0, 0, 0, 0);
            fde_modelpoint.ModelName = tmpOsgPath;
            //****获取包围盒
            IResourceFactory resfac = new ResourceFactory();
            IPropertySet images = new PropertySet();
            IModel model = null;
            IMatrix matrix = null;
            resfac.CreateModelAndImageFromFile(tmpOsgPath, out images, out model, out matrix);
            fde_modelpoint.ModelEnvelope = model.Envelope;
            //*************
            rmodelpoint = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(fde_modelpoint, null, rootId);

            resultCode = _geoEditor.StartEditRenderGeometry(rmodelpoint, gviGeoEditType.gviGeoEditCreator);
            if (!resultCode)
                MessageBox.Show(this.axRenderControl1.GetLastError().ToString());

            oldfdeGeometry = fde_modelpoint;
        }

        private void btnCreatePoint_Click(object sender, EventArgs e)
        {
            _geoEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时  

            if (gfactory == null)
                gfactory = new GeometryFactory();

            fde_point = (IPoint)gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint,
                gviVertexAttribute.gviVertexAttributeZ);
            fde_point.SetCoords(0, 0, 0, 0, 0);
            ISimplePointSymbol pointSymbol = new SimplePointSymbol();
            pointSymbol.Size = 10;
            rpoint = this.axRenderControl1.ObjectManager.CreateRenderPoint(fde_point, pointSymbol, rootId);

            resultCode = _geoEditor.StartEditRenderGeometry(rpoint, gviGeoEditType.gviGeoEditCreator);
            if (!resultCode)
                MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
        }

        private void btnCreatePolyline_Click(object sender, EventArgs e)
        {
            _geoEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时  

            if (gfactory == null)
                gfactory = new GeometryFactory();

            fde_polyline = (IPolyline)gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ);
            rpolyline = this.axRenderControl1.ObjectManager.CreateRenderPolyline(fde_polyline, null, rootId);
            
            resultCode = _geoEditor.StartEditRenderGeometry(rpolyline, gviGeoEditType.gviGeoEditCreator);
            if (!resultCode)
                MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
        }

        private void btnCreatePolygon_Click(object sender, EventArgs e)
        {
            _geoEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时  
                        
            if (gfactory == null)
                gfactory = new GeometryFactory();

            fde_polygon = (IPolygon)gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                gviVertexAttribute.gviVertexAttributeZ);
            rpolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(fde_polygon, null, rootId);            
                       
            resultCode = _geoEditor.StartEditRenderGeometry(rpolygon, gviGeoEditType.gviGeoEditCreator);
            if (!resultCode)
                MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
        }

        private void btnCreateFeature_Click(object sender, EventArgs e)
        {
            _geoEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时  

            IVector3 position = new Vector3();
            position.Set(envX, envY, envZ);
            IEulerAngle angle = new EulerAngle();
            angle.Set(0, -20, 0);
            this.axRenderControl1.Camera.LookAt(position, 800, angle);
            MessageBox.Show("请选择一个Feature");            
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect_CreateFeature);
            
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect_CreateFeature);
            
        }

        #endregion

        #region RenderControl事件
        IMatrix M0 = new Matrix();
        IGeometry geoObjEditing = null;
        IMatrix M1 = new Matrix();

        void axRenderControl1_RcObjectEditFinish()
        {
            

            // 创建结束时可以什么都不干
            // 编辑结束时可以什么都不干
            // 真正操作时，上层应该存储Editing消息返回的fdegeometry,并在此时存储进数据库
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {


            // 创建时可以什么都不干            
            // 编辑时可以什么都不干

            /************二次开发自己计算变化矩阵**************/
            /*
            if (geoObjEditing != null)
            {
                oldfdeGeometry = geoObjEditing;
                rmodelpoint.VisibleMask = gviViewportMask.gviViewNone;
            }
            geoObjEditing = Geometry;

            Logger.WriteMsg("-----------------------------");

            M0 = (oldfdeGeometry as IModelPoint).AsMatrix();
            Logger.WriteMsg("ObjectEditing返回的M0: ");
            printfMatrix(M0);


            M1 = (geoObjEditing as IModelPoint).AsMatrix();
            Logger.WriteMsg("ObjectEditing返回的M1: ");
            printfMatrix(M1);


            IMatrix M0_Inverse = M0.Clone();
            M0_Inverse.Inverse();
            IMatrix MS = MultiplyMatrix(M0_Inverse, M1);
            Logger.WriteMsg("(左乘)得到转换矩阵MS: ");
            printfMatrix(MS);
            Logger.WriteMsg("MS.GetScale: ");
            printfVector(MS.GetScale());

            IMatrix MS3 = MultiplyMatrix(M0, MS);
            Logger.WriteMsg("验证M0*MS=: ");
            printfMatrix(MS3);


            if (geoObjEditing != null)
            {
                ITransform transfrom = (oldfdeGeometry as IModelPoint) as ITransform;
                IVector3 scaleV = MS.GetScale();
                double centerX = 0.0, centerY = 0.0, centerZ = 0.0;
                centerX = MS.M41 / (1.0 - scaleV.X);
                centerY = MS.M42 / (1.0 - scaleV.Y);
                centerZ = MS.M43 / (1.0 - scaleV.Z);
                if (scaleV.X == 1)
                    centerX = MS.M41;
                if (scaleV.Y == 1)
                    centerY = MS.M42;
                if (scaleV.Z == 1)
                    centerZ = MS.M43;
                transfrom.Scale3D(scaleV.X, scaleV.Y, scaleV.Z, centerX, centerY, centerZ);
                IModelPoint newModelPoint = transfrom as IModelPoint;
                Logger.WriteMsg("自己转换得到的MP: ");
                printfMatrix(newModelPoint.AsMatrix());

                this.axRenderControl1.ObjectManager.CreateRenderModelPoint(newModelPoint, null);
            }
            */
        }

        void scaleCalculate(IModelPoint oldMP, IModelPoint newMP, 
            out double scaleX, out double scaleY, out double scaleZ, 
            out double centerX, out double centerY, out double centerZ)
        {
            M0 = oldMP.AsMatrix();
            M1 = newMP.AsMatrix();
            IMatrix M0_Inverse = M0.Clone();
            M0_Inverse.Inverse();
            IMatrix MS = MultiplyMatrix(M0_Inverse, M1);
            IVector3 scaleV = MS.GetScale();
            scaleX = scaleV.X;
            scaleY = scaleV.Y;
            scaleZ = scaleV.Z;
            centerX = MS.M41 / (1.0 - scaleV.X);
            centerY = MS.M42 / (1.0 - scaleV.Y);
            centerZ = MS.M43 / (1.0 - scaleV.Z);
            if (scaleV.X == 1)
                centerX = MS.M41;
            if (scaleV.Y == 1)
                centerY = MS.M42;
            if (scaleV.Z == 1)
                centerZ = MS.M43;
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
            Logger.WriteMsg(ang.Heading + "\t" +ang.Tilt + "\t" + ang.Roll);
            Logger.WriteMsg("");
        }

        void printfVector(IVector3 vec)
        {
            Logger.WriteMsg(vec.X + "\t" + vec.Y + "\t" + vec.Z);
            Logger.WriteMsg("");
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
        
        void axRenderControl1_RcMouseClickSelect_CreateFeature(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            _geoEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时  
            this.axRenderControl1.FeatureManager.UnhighlightAll();

            if (PickResult != null)
            {
                switch (PickResult.Type)
                {
                    case gviObjectType.gviObjectFeatureLayer:
                        {
                            IFeatureLayerPickResult flpr = PickResult as IFeatureLayerPickResult;
                            int fid = flpr.FeatureId;
                            //加载多FeatureClass时要每次重新获取
                            _featureClass = (IFeatureClass)fcGUIDMap[flpr.FeatureLayer.FeatureClassId];
                            _featureLayer = flpr.FeatureLayer;

                            IFdeCursor cursor = null;
                            try
                            {
                                _buffer = _featureClass.CreateRowBuffer();
                                IQueryFilter filter = new QueryFilter();
                                //filter.AddSubField("oid");  //注意：StartEditFeatureGeometry里必须传入一个完整的rowbuffer，所以这里不能限定字段
                                filter.WhereClause = "oid =" + fid;
                                cursor = _featureClass.Search(filter, false);
                                IRowBuffer row = null;
                               
                                if ((row = cursor.NextRow()) != null)
                                {
                                    _buffer = row as IRowBuffer;
                                    int pos = _buffer.FieldIndex("Geometry");
                                    oldfdeGeometry = _buffer.GetValue(pos) as IGeometry;
                                    _buffer.SetValue(0, _featureClass.GetCount(null));  //修改fid为不同值，否则不是创建而是编辑
                                }
                            }
                            catch (COMException ex)
                            {
                                System.Diagnostics.Trace.WriteLine(ex.Message);
                            }
                            finally
                            {
                                
                            }

                            this.axRenderControl1.FeatureManager.HighlightFeature(_featureClass, fid, System.Drawing.Color.Yellow);

                            resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEditCreator);
                            if (!resultCode)
                                MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
                        }
                        break;
                }
            }
        }

        void EditGeometry()
        {
            _geoEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时
            if (currentGeometry != null)
            {
                if (this.rbMove.Checked)
                    resultCode = _geoEditor.StartEditRenderGeometry(currentGeometry, gviGeoEditType.gviGeoEdit3DMove);
                else if (this.rbRotate.Checked)
                    resultCode = _geoEditor.StartEditRenderGeometry(currentGeometry, gviGeoEditType.gviGeoEdit3DRotate);
                else if (this.rbScale.Checked)
                    resultCode = _geoEditor.StartEditRenderGeometry(currentGeometry, gviGeoEditType.gviGeoEdit3DScale);
                else if (this.rbVertex.Checked)
                    resultCode = _geoEditor.StartEditRenderGeometry(currentGeometry, gviGeoEditType.gviGeoEditVertex);
                else if (this.rbZrotate.Checked)
                    resultCode = _geoEditor.StartEditRenderGeometry(currentGeometry, gviGeoEditType.gviGeoEditZRotate);
                else if (this.rbZscale.Checked)
                    resultCode = _geoEditor.StartEditRenderGeometry(currentGeometry, gviGeoEditType.gviGeoEditZScale);
                else if (this.rbBoxScale.Checked)
                    resultCode = _geoEditor.StartEditRenderGeometry(currentGeometry, gviGeoEditType.gviGeoEditBoxScale);

                if (!resultCode)
                    MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
            }
            else if (_buffer != null && _featureLayer != null)
            {
                if (this.rbMove.Checked)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEdit3DMove);
                else if (this.rbRotate.Checked)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEdit3DRotate);
                else if (this.rbScale.Checked)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEdit3DScale);
                else if (this.rbVertex.Checked)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEditVertex);
                else if (this.rbZrotate.Checked)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEditZRotate);
                else if (this.rbZscale.Checked)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEditZScale);
                else if (this.rbBoxScale.Checked)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEditBoxScale);

                if (!resultCode)
                    MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
            }
        }

        void axRenderControl1_RcMouseClickSelect_Edit(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            this.axRenderControl1.FeatureManager.UnhighlightAll();        
            
            // 置空
            currentGeometry = null;
            _buffer = null;
            _featureLayer = null;

            if (PickResult != null)
            {
                switch (PickResult.Type)
                {
                    case gviObjectType.gviObjectFeatureLayer:
                        {
                            IFeatureLayerPickResult flpr = PickResult as IFeatureLayerPickResult;
                            int fid = flpr.FeatureId;
                            //加载多FeatureClass时要每次重新获取
                            _featureClass = (IFeatureClass)fcGUIDMap[flpr.FeatureLayer.FeatureClassId];
                            _featureLayer = flpr.FeatureLayer;

                            IFdeCursor cursor = null;
                            try
                            {
                                _buffer = _featureClass.CreateRowBuffer();
                                QueryFilter filter = new QueryFilter();
                                //filter.AddSubField("oid");  //注意：StartEditFeatureGeometry里必须传入一个完整的rowbuffer，所以这里不能限定字段
                                filter.WhereClause = "oid =" + fid;
                                cursor = _featureClass.Search(filter, false);
                                IRowBuffer row = null;
                                if ((row = cursor.NextRow()) != null)
                                {
                                    _buffer = row as IRowBuffer;
                                    int pos = _buffer.FieldIndex("Geometry");
                                    oldfdeGeometry = _buffer.GetValue(pos) as IGeometry;
                                }
                            }
                            catch (COMException ex)
                            {
                                System.Diagnostics.Trace.WriteLine(ex.Message);
                            }
                            finally
                            {
                              
                            }

                            this.axRenderControl1.FeatureManager.HighlightFeature(_featureClass, fid, System.Drawing.Color.Yellow);
                            EditGeometry();
                        }
                        break;
                    case gviObjectType.gviObjectRenderModelPoint:
                        {
                            IRenderModelPointPickResult flpr = PickResult as IRenderModelPointPickResult;
                            rmodelpoint = flpr.ModelPoint;
                            currentGeometry = rmodelpoint;
                            oldfdeGeometry = rmodelpoint.GetFdeGeometry();
                            EditGeometry();      
                        }
                        break;
                    case gviObjectType.gviObjectRenderPoint:
                        {
                            IRenderPointPickResult flpr = PickResult as IRenderPointPickResult;
                            rpoint = flpr.Point;
                            currentGeometry = rpoint;
                            EditGeometry();      
                        }
                        break;
                    case gviObjectType.gviObjectRenderPolyline:
                        {
                            IRenderPolylinePickResult flpr = PickResult as IRenderPolylinePickResult;
                            rpolyline = flpr.Polyline;
                            currentGeometry = rpolyline;
                            EditGeometry();      
                        }
                        break;
                    case gviObjectType.gviObjectRenderPolygon:
                        {
                            IRenderPolygonPickResult flpr = PickResult as IRenderPolygonPickResult;
                            rpolygon = flpr.Polygon;
                            currentGeometry = rpolygon;
                            EditGeometry();                           
                        }
                        break;
                }
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 我要编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBeginEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbBeginEdit.Checked)
            {
                this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect_CreateFeature);
                
                this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect_Edit);
                
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer | gviMouseSelectObjectMask.gviSelectRenderGeometry;
                this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                setEnableEditButtons(true, true);
                MessageBox.Show("请在屏幕中点选需要编辑的对象");
            }
            else
            {
                this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect_Edit);
                
                this.axRenderControl1.FeatureManager.UnhighlightAll();
                setEnableEditButtons(true, false);
            }
        }

        private void rbMove_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMove.Checked)
                EditGeometry();
        }

        private void rbRotate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotate.Checked)
                EditGeometry();
        }

        private void rbScale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbScale.Checked)
                EditGeometry();
        }

        private void rbVertex_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVertex.Checked)
                EditGeometry();
        }

        private void rbZrotate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbZrotate.Checked)
                EditGeometry();
        }

        private void rbZscale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbZscale.Checked)
                EditGeometry();
        }

        private void rbBoxScale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBoxScale.Checked)
                EditGeometry();
        }

        /// <summary>
        /// 取消编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            _geoEditor.CancelEdit();
        }
        #endregion


    }
}
    