using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using System.Collections;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Resource;
using Gvitech;
using Gvitech.CityMaker.Controls;
using System.Drawing;


namespace GetSolidProfile
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEnvelope env;//加载数据时，初始化的矩形范围
        private IEulerAngle angle = new EulerAngle();

        IGeometryFactory geoFactory = new GeometryFactory();
        IGeometryConvertor gc = new GeometryConvertor();
        ICRSFactory crsFactory = new CRSFactory();
        ICoordinateReferenceSystem crs = null;
        
        IPolygon polygonDraw = null;
        IRenderPolygon renderpolygonDraw = null;
        List<IPolygon> PolygonList = new List<IPolygon>();

        IGeometry currentGeometry = null;  //for objectEditor

        IMultiPolygon multiPolygon = null;
        List<IRenderPolygon> RenderPolygonList = new List<IRenderPolygon>();
        List<IRenderModelPoint> RenderMPInteriorList = new List<IRenderModelPoint>();
        List<IRenderModelPoint> RenderMPExteriorList = new List<IRenderModelPoint>();
        List<IRenderModelPoint> RenderMPProfileList = new List<IRenderModelPoint>();

        int __fid = -1;
        IFeatureClass __fc = null;
        IFeatureLayer __fl = null;
        int nPolygon = 0;
        double minZ = 0.0, maxZ = 0.0;

        private System.Guid rootId = System.Guid.Empty;
        private ILabel label = null;

        // 线程转发
        public int MainThreadId = 0;
        
        
        

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;         
        }

        private void MainForm_Load(object sender, EventArgs e)
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

            #region 加载FDB场景
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\ClosedTrimesh.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);
                crs = dataset.SpatialReference;
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
                    __fc = fc;
                    __fl = featureLayer;

                    if (!hasfly)
                    {
                        IFieldInfoCollection fieldinfos = fc.GetFields();
                        IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;
                        angle.Set(0, -20, 0);
                        IPoint p = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        p.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                        p.Position = env.Center;
                        this.axRenderControl1.Camera.LookAt2(p, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion


            // 注册事件
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            


            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "GetSolidProfile.html";
            }  
        }

        private void toolStripButtonCreatePolygon_Click(object sender, System.EventArgs e)
        {
            //创建日志文件
            Logger.Create(Application.StartupPath);

            if (renderpolygonDraw != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderpolygonDraw.Guid);
                renderpolygonDraw = null;
            }

            polygonDraw = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            polygonDraw.SpatialCRS = crs as ISpatialCRS;
            ISurfaceSymbol sf = new SurfaceSymbol();
            sf.Color = Color.FromArgb(Convert.ToInt32("0x55ffff80", 16));
            ICurveSymbol cs = new CurveSymbol();
            cs.Color = Color.FromArgb(Convert.ToInt32("0x55ffff80", 16));
            sf.BoundarySymbol = cs;
            renderpolygonDraw = this.axRenderControl1.ObjectManager.CreateRenderPolygon(polygonDraw as IPolygon, sf, rootId);
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
            this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(renderpolygonDraw, gviGeoEditType.gviGeoEditCreator);
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            currentGeometry = Geometry;
        }


        void axRenderControl1_RcObjectEditFinish()
        {
            

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            polygonDraw = currentGeometry as IPolygon;
            //抬高一点
            for (int i = 0; i < polygonDraw.ExteriorRing.PointCount; i++)
            {
                IPoint pointOnExr = polygonDraw.ExteriorRing.GetPoint(i).Clone() as IPoint;
                pointOnExr.Z += 1;
                polygonDraw.ExteriorRing.UpdatePoint(i, pointOnExr);
            }

            renderpolygonDraw.SetFdeGeometry(polygonDraw);
            renderpolygonDraw.VisibleMask = gviViewportMask.gviViewNone;

            double height = 0.0;
            try
            {
                height = double.Parse(this.numHeight.Value.ToString());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请检查挖洞高度");
                return;
            }

            //构造底面polygon
            IPolygon polygonBottom = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            for (int i = 0; i < polygonDraw.ExteriorRing.PointCount; i++)
            {
                IPoint pointOnExr = polygonDraw.ExteriorRing.GetPoint(i).Clone() as IPoint;
                maxZ = pointOnExr.Z;
                pointOnExr.Z -= height;
                minZ = pointOnExr.Z;
                polygonBottom.ExteriorRing.AppendPoint(pointOnExr);
            }
            polygonBottom.Close();
            PolygonList.Add(polygonBottom);
            //CreateRenderPolygon(polygonBottom);

            
            //构造侧面polygon            
            for (int i = 0; i < polygonDraw.ExteriorRing.PointCount; i++)
            {
                IPoint pointOnExr = polygonDraw.ExteriorRing.GetPoint(i).Clone() as IPoint;
                pointOnExr.Z -= height;

                IPoint pointOnExr2 = null;
                if (i == polygonDraw.ExteriorRing.PointCount -1)
                    pointOnExr2 = polygonDraw.ExteriorRing.GetPoint(0).Clone() as IPoint;
                else
                    pointOnExr2 = polygonDraw.ExteriorRing.GetPoint(i + 1).Clone() as IPoint;                
                pointOnExr2.Z -= height;

                IPolygon polygonTemp = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygonTemp.ExteriorRing.AppendPoint(polygonDraw.ExteriorRing.GetPoint(i));
                polygonTemp.ExteriorRing.AppendPoint(pointOnExr);
                polygonTemp.ExteriorRing.AppendPoint(pointOnExr2);
                if (i == polygonDraw.ExteriorRing.PointCount - 1)
                    polygonTemp.ExteriorRing.AppendPoint(polygonDraw.ExteriorRing.GetPoint(0));
                else
                    polygonTemp.ExteriorRing.AppendPoint(polygonDraw.ExteriorRing.GetPoint(i + 1));
                polygonTemp.Close();
                PolygonList.Add(polygonTemp);
                //CreateRenderPolygon(polygonTemp);
            }
            

            List<IRowBuffer> list = new List<IRowBuffer>();
            ISpatialFilter filter = new SpatialFilter();
            filter.Geometry = polygonDraw;
            filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;
            filter.GeometryField = "Geometry";
            IRowBuffer row = null;
            IFdeCursor cursor = __fc.Search(filter, false);
            while ((row = cursor.NextRow()) != null)
            {
                list.Add(row);
            }

            foreach (IRowBuffer r in list)
            {
                __fid = (Int32)r.GetValue(0);
                int geometryIndex = -1;
                geometryIndex = r.FieldIndex("Geometry");
                int nameIndex = -1;
                nameIndex = r.FieldIndex("Name");
                if (geometryIndex != -1)
                {
                    
                    // 获取ModelPoint
                    IModelPoint mp = r.GetValue(geometryIndex) as IModelPoint;

                    // 获取Model
                    string modelName = mp.ModelName;
                    IModel m = (__fc.FeatureDataSet as IResourceManager).GetModel(modelName);

                    Logger.WriteMsg(LogLevel.Message, "---------开始ModelPointToTriMesh----------", DateTime.Now);
                    // 获取MultiTriMesh                    
                    IMultiTriMesh multiTM = gc.ModelPointToTriMesh(m, mp, false);
                    Logger.WriteMsg(LogLevel.Message, string.Format("---------完成ModelPointToTriMesh:{0}----------", multiTM.GeometryCount), DateTime.Now);

                    // 获取Name
                    string strName = r.GetValue(nameIndex).ToString();
                    for (int i = 0; i < multiTM.GeometryCount; i++)
                    {
                        ITriMesh tm = multiTM.GetGeometry(i) as ITriMesh;
                        //if (!tm.IsClosed)
                        //    continue;

                        // 生成剖面
                        for (int p = 0; p < PolygonList.Count; p++ )
                        {
                            IPolygon curPolygon = PolygonList[p];
                            Logger.WriteMsg(LogLevel.Message, string.Format("TM:{0} PG:{1}", i, p), DateTime.Now);
                            if (gc.GetSolidProfile(tm, curPolygon, out multiPolygon))
                            {
                                Logger.WriteMsg(LogLevel.Message, string.Format("TM:{0} PG:{1} TRUE", i, p), DateTime.Now);
                                if (multiPolygon != null && multiPolygon.GeometryCount > 0)
                                {
                                    for (int j = 0; j < multiPolygon.GeometryCount; j++)
                                    {
                                        IPolygon tm2 = multiPolygon.GetGeometry(j) as IPolygon;
                                        //RenderPolygonList.Add(CreateRenderPolygon(tm2));

                                        ICurveSymbol cs = new CurveSymbol();
                                        cs.Color = Color.FromArgb(0, Color.White);
                                        ISurfaceSymbol ss = new SurfaceSymbol();
                                        ss.BoundarySymbol = cs;
                                        ss.Color = m.GetGroup(0).GetPrimitive(0).Material.DiffuseColor;
                                        IRenderPolygon tmPolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(tm2, ss, rootId);
                                        tmPolygon.SetClientData("Name", strName);
                                        RenderPolygonList.Add(tmPolygon);

                                        #region 用当前选中的模型材质，生成截面模型
                                        /*
                                        IModel outModel = null;
                                        IModelPoint outMP = null;
                                        gc.PolygonToModelPoint(tm2, out outModel, out outMP);

                                        if (outModel != null && outMP != null)
                                        {
                                            
                                            IDrawGroup dgroup = outModel.GetGroup(0);
                                            IDrawPrimitive dpri = dgroup.GetPrimitive(0);
                                            IDrawMaterial dmar = dpri.Material;                                       
                                            dmar.CullMode = gviCullFaceMode.gviCullNone;                                                                                        
                                        
                                            //处理贴图
                                            //IPropertySet psOut = new PropertySet();
                                            string imgName = m.GetGroup(0).GetPrimitive(0).Material.TextureName;
                                            if (imgName != "")
                                            {
                                                dmar.WrapModeS = gviTextureWrapMode.gviTextureWrapRepeat;
                                                dmar.WrapModeT = gviTextureWrapMode.gviTextureWrapRepeat;

                                                //string tmpImgPath = (strMediaPath + @"\1\") + imgName + ".dds";                                                
                                                //IImage imageOut = (__fc.FeatureDataSet as IResourceManager).GetImage(imgName);                                              
                                                //imageOut.WriteFile(tmpImgPath);      
                                                //psOut.SetProperty(imgName, imageOut);
                                                //dmar.TextureName = imgName;

                                                IImage image = (__fc.FeatureDataSet as IResourceManager).GetImage(imgName);
                                                this.axRenderControl1.ObjectManager.AddImage(imgName, image);
                                                dmar.TextureName = imgName;

                                                if (dpri.VertexArray.Length == 12)
                                                {
                                                    IFloatArray texcoords = new FloatArray();
                                                    texcoords.Append(0);
                                                    texcoords.Append(0);
                                                    texcoords.Append(1.0f);
                                                    texcoords.Append(0);
                                                    texcoords.Append(1.0f);
                                                    texcoords.Append(1.0f);
                                                    texcoords.Append(0);
                                                    texcoords.Append(1.0f);
                                                    dpri.TexcoordArray = texcoords;
                                                }
                                                else 
                                                {                                                    
                                                    IFloatArray texcoords = new FloatArray();
                                                    texcoords.Append(0);
                                                    texcoords.Append(0);
                                                    for (int v = 3; v < dpri.VertexArray.Length - 2; )
                                                    {
                                                        texcoords.Append(Math.Abs(dpri.VertexArray.Get(v) - dpri.VertexArray.Get(0)));
                                                        texcoords.Append(Math.Abs(dpri.VertexArray.Get(v+2) - dpri.VertexArray.Get(2)));
                                                        v += 3;
                                                    }                                                  
                                                    dpri.TexcoordArray = texcoords;
                                                }
                                            }
                                            else
                                            {
                                                dmar.DepthBias = m.GetGroup(0).GetPrimitive(0).Material.DepthBias;
                                                dmar.DiffuseColor = m.GetGroup(0).GetPrimitive(0).Material.DiffuseColor;
                                                dmar.EnableBlend = m.GetGroup(0).GetPrimitive(0).Material.EnableBlend;
                                                dmar.EnableLight = m.GetGroup(0).GetPrimitive(0).Material.EnableLight;
                                                dmar.SpecularColor = m.GetGroup(0).GetPrimitive(0).Material.SpecularColor;
                                                dmar.WrapModeS = m.GetGroup(0).GetPrimitive(0).Material.WrapModeS;
                                                dmar.WrapModeT = m.GetGroup(0).GetPrimitive(0).Material.WrapModeT;  
                                            }

                                            dpri.Material = dmar;
                                            dgroup.SetPrimitive(0, dpri);
                                            outModel.SetGroup(0, dgroup);

                                            //处理模型
                                            //string osgNameOut = __fid + "_" + p + "_" + j;
                                            //string tmpOSGPath = (strMediaPath + @"\1\" + osgNameOut + ".osg");
                                            //outModel.WriteFile(tmpOSGPath, psOut);
                                            //outMP.ModelName = tmpOSGPath;    


                                            string osgName = __fid + "_" + p + "_" + j;
                                            this.axRenderControl1.ObjectManager.AddModel(osgName, outModel);
                                            outMP.ModelName = osgName;

                                            //可视化
                                            outMP.ModelEnvelope = outModel.Envelope;
                                            IRenderModelPoint outRenderMP = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(outMP, null, rootId);
                                            //this.axRenderControl1.RefreshModel(null, tmpOSGPath);
                                            outRenderMP.ShowOutline = true;
                                            //outRenderMP.VisibleMask = gviViewportMask.gviViewNone;
                                            RenderMPProfileList.Add(outRenderMP);
                                        }
                                        */
                                        #endregion                                       
                                    }
                                }
                            }
                        }//遍历PolygonList结束                        

                    }//遍历multiTM结束
                    


                    //去掉内环模型
                    __fl.VisibleMask = gviViewportMask.gviViewNone;
                    IModel modelInterior = null;
                    IModelPoint mpInterior = null;
                    IRenderModelPoint rmpInterior = null;
                    IModel modelExterior = null;
                    IModelPoint mpExterior = null;
                    IRenderModelPoint rmpExterior = null;
                    IMultiPolygon mltiPolygon = geoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
                    mltiPolygon.AddGeometry(polygonDraw);
                    Logger.WriteMsg(LogLevel.Message, "---------开始SplitModelPointByPolygon2DWithZ----------", DateTime.Now);
                    if (gc.SplitModelPointByPolygon2DWithZ(mltiPolygon, m, mp, minZ, maxZ, out modelInterior, out mpInterior, out modelExterior, out mpExterior))
                    {
                        Logger.WriteMsg(LogLevel.Message, "SplitModelPointByPolygon2DWithZ返回值为TRUE", DateTime.Now);
                        if (modelExterior != null && mpExterior != null)
                        {
                            this.axRenderControl1.ObjectManager.AddModel(mpExterior.ModelName, modelExterior);
                            string[] imagenames = modelExterior.GetImageNames();
                            for (int q = 0; q < imagenames.Length; q++ )
                            {
                                IImage image = (__fc.FeatureDataSet as IResourceManager).GetImage(imagenames[q]);
                                this.axRenderControl1.ObjectManager.AddImage(imagenames[q], image);
                            }
                            rmpExterior = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mpExterior, null, rootId);
                            RenderMPExteriorList.Add(rmpExterior);
                        }
                        if (modelInterior != null && mpInterior != null)
                        {
                            this.axRenderControl1.ObjectManager.AddModel(mpInterior.ModelName, modelInterior);
                            string[] imagenames = modelInterior.GetImageNames();
                            for (int q = 0; q < imagenames.Length; q++)
                            {
                                IImage image = (__fc.FeatureDataSet as IResourceManager).GetImage(imagenames[q]);
                                this.axRenderControl1.ObjectManager.AddImage(imagenames[q], image);
                            }
                            rmpInterior = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mpInterior, null, rootId);
                            rmpInterior.VisibleMask = gviViewportMask.gviViewNone;
                            RenderMPInteriorList.Add(rmpInterior);
                        }
                    }
                }
            }//遍历RowBufferList结束
            //MessageBox.Show("执行成功");
        }

        private IRenderPolygon CreateRenderPolygon(IPolygon poly)
        {
            Random randObj = new Random(nPolygon);
            int aColor = randObj.Next(0, 255);
            int gColor = randObj.Next(0, 255);
            int rColor = randObj.Next(0, 255);
            uint ranCor = (uint)(rColor | gColor << 8 | aColor << 16 | 255 << 24);
            ISurfaceSymbol ss = new SurfaceSymbol();
            ss.Color = ColorHelper.UintToColor(ranCor);
            IRenderPolygon rtm2 = this.axRenderControl1.ObjectManager.CreateRenderPolygon(poly, ss, rootId);
            nPolygon++;
            return rtm2;
        }


        private void checkBoxCompareInterior_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompareInterior.Checked == true)
            {
                if (RenderMPInteriorList.Count > 0)
                {
                    for (int i = 0; i < RenderMPInteriorList.Count; i++)
                    {
                        RenderMPInteriorList[i].VisibleMask = gviViewportMask.gviViewAllNormalView; 
                    }
                }
            }
            else
            {
                if (RenderMPInteriorList.Count > 0)
                {
                    for (int i = 0; i < RenderMPInteriorList.Count; i++)
                    {
                        RenderMPInteriorList[i].VisibleMask = gviViewportMask.gviViewNone;
                    }
                }
            }
        }

        private void checkBoxCompareExterior_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompareExterior.Checked == true)
            {
                if (RenderMPExteriorList.Count > 0)
                {
                    for (int i = 0; i < RenderMPExteriorList.Count; i++)
                    {
                        RenderMPExteriorList[i].VisibleMask = gviViewportMask.gviViewAllNormalView;
                    }
                }
            }
            else
            {
                if (RenderMPExteriorList.Count > 0)
                {
                    for (int i = 0; i < RenderMPExteriorList.Count; i++)
                    {
                        RenderMPExteriorList[i].VisibleMask = gviViewportMask.gviViewNone;
                    }
                }
            }
        }

        private void checkBoxCompareProfile_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompareProfile.Checked == true)
            {
                if (RenderMPProfileList.Count > 0)
                {
                    for (int i = 0; i < RenderMPProfileList.Count; i++)
                    {
                        RenderMPProfileList[i].VisibleMask = gviViewportMask.gviViewAllNormalView;
                        RenderMPProfileList[i].Glow(1000);
                    }
                }
            }
            else
            {
                if (RenderMPProfileList.Count > 0)
                {
                    for (int i = 0; i < RenderMPProfileList.Count; i++)
                    {
                        RenderMPProfileList[i].VisibleMask = gviViewportMask.gviViewNone;
                    }
                }
            }
        }

        private void checkBoxCompareLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompareLayer.Checked == true)
            {
                __fl.VisibleMask = gviViewportMask.gviViewAllNormalView;
            }
            else
            {
                __fl.VisibleMask = gviViewportMask.gviViewNone;
            }
        }


        private void checkBoxCompareDrawPolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (renderpolygonDraw == null)
                return;

            if (checkBoxCompareDrawPolygon.Checked == true)
            {
                renderpolygonDraw.VisibleMask = gviViewportMask.gviViewAllNormalView;
            }
            else
            {
                renderpolygonDraw.VisibleMask = gviViewportMask.gviViewNone;
            }
        }


        private void buttonClearProfile_Click(object sender, EventArgs e)
        {
            if (renderpolygonDraw != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderpolygonDraw.Guid);
                renderpolygonDraw = null;
            }

            if (RenderPolygonList.Count > 0)
            {
                for (int i = 0; i < RenderPolygonList.Count; i++)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(RenderPolygonList[i].Guid);
                }
                RenderPolygonList.Clear();
            }

            if (RenderMPExteriorList.Count > 0)
            {
                for (int i = 0; i < RenderMPExteriorList.Count; i++)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(RenderMPExteriorList[i].Guid);
                }
                RenderMPExteriorList.Clear();
            }
            if (RenderMPInteriorList.Count > 0)
            {
                for (int i = 0; i < RenderMPInteriorList.Count; i++)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(RenderMPInteriorList[i].Guid);
                }
                RenderMPInteriorList.Clear();
            }
            if (RenderMPProfileList.Count > 0)
            {
                for (int i = 0; i < RenderMPProfileList.Count; i++)
                {
                    this.axRenderControl1.ObjectManager.DeleteObject(RenderMPProfileList[i].Guid);
                }
                RenderMPProfileList.Clear();
            }

            PolygonList.Clear();

            if (label != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(label.Guid);
                label = null;
            }
        }

        private void checkBoxCompareProfilePolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompareProfilePolygon.Checked == true)
            {
                if (RenderPolygonList.Count > 0)
                {
                    for (int i = 0; i < RenderPolygonList.Count; i++)
                    {
                        RenderPolygonList[i].VisibleMask = gviViewportMask.gviViewAllNormalView;
                    }
                }
            }
            else
            {
                if (RenderPolygonList.Count > 0)
                {
                    for (int i = 0; i < RenderPolygonList.Count; i++)
                    {
                        RenderPolygonList[i].VisibleMask = gviViewportMask.gviViewNone;
                    }
                }
            }
        }

        private void checkBoxSelectRenderPolygon_CheckedChanged(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            if (checkBoxSelectRenderPolygon.Checked)
            {                
                this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectRenderGeometry;
            }
            else
            {
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            }
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (PickResult == null)
                return;

            IRenderPolygonPickResult rpPickResult = PickResult as IRenderPolygonPickResult;
            if (rpPickResult != null)
            {
                IRenderPolygon rp = rpPickResult.Polygon;
                string strToShow = rp.GetClientData("Name");

                if (label == null)
                    label = this.axRenderControl1.ObjectManager.CreateLabel(rootId);
                label.Text = strToShow;
                label.Position = IntersectPoint;
            }
        }





    }
}
