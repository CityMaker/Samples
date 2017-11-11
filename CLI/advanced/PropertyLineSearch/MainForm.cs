// Copyright 2012 CityMaker SDK
// 
// All rights reserved under the copyright laws of the China
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See Sample at <your CityMaker install location>/CityMaker SDK/Samples.
// 
//author	FengZongMin
//date	2014/1/9
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;

namespace PropertyLineSearch
{
    public partial class MainForm : Form
    {
        
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private Hashtable fcGUIDMap = null;  //featureclass.GUID, IFeatureClass 存储featureclass及对应的GUID
        
        private bool flagx = false;
        private IPolyline polyline = null;
        private IPolygon polygon = null;
        private IRenderPolyline renderPolyline = null;
        private IGeometryFactory geoFactory = null;
        private ICRSFactory crsFactory = null;
        private ISpatialCRS currentCRS = null;
        private IProjectedCRS projectCRS = null;

        protected IRenderPolygon _RenderPolygon;
        protected ISurfaceSymbol _SurfaceSymbol = new SurfaceSymbol() { Color = System.Drawing.Color.YellowGreen };
        private Dictionary<string, RowObject> rowMap = new Dictionary<string, RowObject>();

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

            #region 加载FDB场景
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\MultiSpatialColumns.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);
                projectCRS = dataset.SpatialReference as IProjectedCRS;
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
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        if (geoFactory == null)
                        {
                            geoFactory = new GeometryFactory();
                        }                        
                        IPoint p = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        p.Position = env.Center;
                        p.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                        this.axRenderControl1.Camera.LookAt2(p, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion

            if (crsFactory == null)
            {
                crsFactory = new CRSFactory();
            }
            currentCRS = crsFactory.CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS; 

            this.axRenderControl1.HighlightHelper.VisibleMask = 1;
        }

        /// <summary>
        /// 开始绘制线段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatLine_Click(object sender, EventArgs e)
        {
            axRenderControl1.FeatureManager.UnhighlightAll();
            axRenderControl1.HighlightHelper.SetRegion(null);

            flagx = true;
            if (renderPolyline != null)
            {
                axRenderControl1.ObjectManager.DeleteObject(renderPolyline.Guid);
                renderPolyline = null;
            }
            if (polyline != null)
            {
                polyline = null;
            }
            if (_RenderPolygon != null)
            {
                _RenderPolygon.VisibleMask = gviViewportMask.gviViewNone; 
            }    
            if (this.trackBar1.Value > 0)
            {
                this.trackBar1.Value = 0;
            }
            if (this.textBox2.Text !=null)
            {
                this.textBox2.Text = null;
            }
            if (this.textBox1.Text != null)
            {
                this.textBox1.Text = null;
            }
            if (this.dataGridView1.Rows != null)
            {
                this.dataGridView1.Rows.Clear();
            }
            if (geoFactory == null)
            {
                geoFactory = new GeometryFactory();
            }
            if (polyline == null)
            {
                polyline = (IPolyline)geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ);
                polyline.SpatialCRS = currentCRS;
            }

            axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(g_RcMouseClickSelect);
            
        }

        /// <summary>
        /// 拾取线
        /// </summary>
        /// <param name="PickResult"></param>
        /// <param name="IntersectPoint"></param>
        /// <param name="Mask"></param>
        /// <param name="EventSender"></param>
        void g_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (IntersectPoint == null)
                return;

            if (renderPolyline == null)
            {
                renderPolyline = axRenderControl1.ObjectManager.CreateRenderPolyline(polyline, null, rootId);
                renderPolyline.Symbol = new CurveSymbol() { Color = System.Drawing.Color.Red, Width = 4 };
            }

            if (polyline.PointCount < 2)
            {
                polyline.AppendPoint(IntersectPoint);
            }
            if (polyline.PointCount == 2)
            {
                axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(g_RcMouseClickSelect);
                
                flagx = false;
            }
            renderPolyline.SetFdeGeometry(polyline);
        }

        /// <summary>
        /// 缓冲区半径设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int a = this.trackBar1.Value;
            this.textBox1.Text = a.ToString();
            if (a <= 0) { return; }
            DrawBufferPolygon(a);
        }

        /// <summary>
        /// 进行缓冲
        /// </summary>
        /// <param name="distance">缓冲距离</param>
        /// <returns>缓冲形成的多边形</returns>
        private IRenderPolygon DrawBufferPolygon(double distance)
        {
            IPolygon bufferPolygon = GetBufferPolygon(distance);
            axRenderControl1.HighlightHelper.SetRegion(bufferPolygon);

            if (_RenderPolygon != null)
            {
                _RenderPolygon.SetFdeGeometry(bufferPolygon);
            }
            else
            {
                _RenderPolygon = axRenderControl1.ObjectManager.CreateRenderPolygon(bufferPolygon, _SurfaceSymbol, rootId);
                _RenderPolygon.MaxVisibleDistance = double.MaxValue;
                _RenderPolygon.MinVisiblePixels = 3;
                _RenderPolygon.HeightStyle = gviHeightStyle.gviHeightAbsolute;
            }
            _RenderPolygon.VisibleMask = gviViewportMask.gviViewAllNormalView;
            
            return _RenderPolygon;
        }

        /// <summary>
        /// 获取缓冲多边形
        /// </summary>
        /// <param name="distance">缓冲半径</param>
        /// <returns>缓冲多边形</returns>
        private IPolygon GetBufferPolygon(double distance)
        {
            IPolyline startLine = renderPolyline.GetFdeGeometry() as IPolyline;
            if (startLine == null) { return null; }

            IPolyline drawSource = startLine.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            //如果是球面需投影到平面再做缓冲区计算
            if (currentCRS.CrsType == gviCoordinateReferenceSystemType.gviCrsGeographic)
            {
                drawSource.Project(projectCRS);
            }
            ITopologicalOperator2D drawBufferLine = drawSource as ITopologicalOperator2D;
            IPolygon bufferPolygon = drawBufferLine.Buffer2D(distance, gviBufferStyle.gviBufferCapbutt) as IPolygon;
            bufferPolygon.SpatialCRS = drawSource.SpatialCRS;
            if (currentCRS.CrsType == gviCoordinateReferenceSystemType.gviCrsGeographic)
            {
                bufferPolygon.Project(currentCRS);
            }
                    
            polygon = bufferPolygon;
            IPolygon ppolygon = bufferPolygon.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPolygon;  //设置三维坐标
            
            //不被地面遮挡
            for (int j = 0; j < ppolygon.ExteriorRing.PointCount; j++)
            {
                IPoint p = ppolygon.ExteriorRing.GetPoint(j);
                p.Z = 1;
                ppolygon.ExteriorRing.UpdatePoint(j, p);
            }
            return ppolygon;
        }

        /// <summary>
        /// 违建分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnalyze_Click(object sender, EventArgs e)
        {           
            string htext = this.textBox2.Text;
            if (htext.Equals(""))
            {
                this.errorProvider1.SetError(this.textBox2,"请输入高度限定值" );
                DialogResult ReturnDlg = MessageBox.Show(this,"高度限定值输入错误",
                    "信息提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if (polygon != null)
            {
                rowMap.Clear();
                SelectFeaturesFromBaseLyr(polygon);
            }
        }

        public void SelectFeaturesFromBaseLyr(IPolygon polygon)
        {
            float h = float.Parse(this.textBox2.Text);
            IFdeCursor cursor = null;
            
            try
            {                
                axRenderControl1.FeatureManager.UnhighlightAll();
                
                IRowBuffer row = null;
                List<IRowBuffer> list = new List<IRowBuffer>();
                foreach (IFeatureClass fc in fcMap.Keys)
                {
                    if (!fc.Name.Equals("建筑"))
                        continue;

                    ISpatialFilter filter = new SpatialFilter();
                    filter.Geometry = polygon;
                    filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;   //不相离
                    filter.GeometryField = "Polygon";
                    cursor = fc.Search(filter, false);
                    while ((row = cursor.NextRow()) != null)
                    {
                        list.Add(row);
                    }                                                                                                                     
                    
                    foreach (IRowBuffer r in list)
                    {
                        string fid = "";
                        string fName = "";
                        string groupId = "";
                        IEnvelope env = null;
                        IGeometry ge = null;
                        IModelPoint mp = null;
                        float hhh = 0;
                        int geometryIndex = -1;
                        geometryIndex = r.FieldIndex("Polygon");
                        if (geometryIndex != -1)
                        {
                            int storeyIndex = r.FieldIndex("storey");
                            int storey = int.Parse(r.GetValue(storeyIndex).ToString());
                            int heightIndex = r.FieldIndex("height");
                            float hh = float.Parse(r.GetValue(heightIndex).ToString());
                            hhh = hh * storey;
                            if (hhh > h)
                            {
                                for (int i = 0; i < r.FieldCount; i++)
                                {

                                    if (r.Fields.Get(i).Name == "oid")
                                    {
                                        fid = r.GetValue(i).ToString();
                                    }
                                    else if (r.Fields.Get(i).Name == "groupid")
                                    {
                                        groupId = r.GetValue(i).ToString();
                                    }
                                    else if (r.Fields.Get(i).Name == "Name")
                                    {
                                        fName = r.GetValue(i).ToString();
                                    }
                                    else if (r.Fields.Get(i).Name == "Geometry")
                                    {
                                        ge= r.GetValue(i) as IModelPoint;
                                        env = ge.Envelope;
                                        mp = r.GetValue(i) as IModelPoint;
                                    }
                                }
                                RowObject ro = new RowObject() { FID = fid, GroupId = groupId, Name = fName, 
                                    FeatureClass = fc, Envelop = env, Height = hhh.ToString(), Modelpoint = mp };
                                if (!rowMap.ContainsKey(ro.FID))
                                {
                                    rowMap.Add(ro.FID, ro);
                                }
                                
                                int hfid = int.Parse(ro.FID);
                                //高亮显示违章建筑
                                axRenderControl1.FeatureManager.HighlightFeature(fc, hfid, System.Drawing.Color.Yellow);

                            }
                        }
                    } // end of foreach (IRowBuffer r in list)
                } // end of foreach (IFeatureClass fc in fcMap.Keys)

                LoadGridView();
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

        private void LoadGridView()
        {          
            this.dataGridView1.Rows.Clear();
            if (rowMap != null)
            {
                this.dataGridView1.RowCount = rowMap.Count;
                for (int i = 0; i < rowMap.Count; i++)
                {
                    this.dataGridView1.Rows[i].Cells["FID"].Value = rowMap.Values.ToArray()[i].FID;
                    this.dataGridView1.Rows[i].Cells["FID"].Tag = rowMap.Values.ToArray()[i].FeatureClass;
                    this.dataGridView1.Rows[i].Cells["NameColumn"].Value = rowMap.Values.ToArray()[i].Name;
                    this.dataGridView1.Rows[i].Cells["GroupID"].Value = rowMap.Values.ToArray()[i].GroupId;
                    this.dataGridView1.Rows[i].Cells["height"].Value = rowMap.Values.ToArray()[i].Height;
                }
            }
        }

        /// <summary>
        /// 单击表格记录进行定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            if (row != null)
            {
                string fid = row.Cells["FID"].Value.ToString();
                IFeatureClass fc = row.Cells["FID"].Tag as IFeatureClass;                                              
                IModelPoint mp = rowMap[fid].Modelpoint;
                IModelPointSymbol symbol = new ModelPointSymbol();
                symbol.SetResourceDataSet(fc.FeatureDataSet);
                IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, symbol, rootId);
                if (rmp != null)
                {
                    //定位
                   this.axRenderControl1.Camera.FlyToObject(rmp.Guid, gviActionCode.gviActionJump);
                    //闪烁
                    rmp.Glow(2000);
                    this.axRenderControl1.ObjectManager.DelayDelete(rmp.Guid, 2000);
                }
            }
        }


        class RowObject
        {
            public string FID
            {
                get;
                set;
            }
            public string Name
            {
                get;
                set;
            }
            public string GroupId
            {
                get;
                set;
            }
            public string Height
            {
                get;
                set;
            }
            public object FeatureClass
            {
                get;
                set;
            }
            public IEnvelope Envelop
            {
                get;
                set;
            }
            public IModelPoint Modelpoint
            {
                get;
                set;
            }
        }
    }
}
