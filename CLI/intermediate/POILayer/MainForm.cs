using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Data;
using System;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;


namespace POILayer
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        //private IHTMLWindow htmlwindow = null;

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
            this.axRenderControl1.Initialize(false, ps);
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
                string tmpFDBPath = (strMediaPath + @"\Beijing_subway.FDB");
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
                        IPoint point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        point.Position = env.Center;
                        point.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                        this.axRenderControl1.Camera.LookAt2(point, 1000, angle);
                    }
                    hasfly = true;
                }
            }

            // 查询所有值
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                DataTable dt = CreateDataTable(fc);
                GetResultSet(fc, null, dt);              
                this.dataGridView1.DataSource = dt;
            }

            //设置飞行时间
            this.axRenderControl1.Camera.FlyTime = 3;


            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "POILayer.html";
            }    
        }

        private void toolStripPan_Click(object sender, System.EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;

            // 取消控件拾取事件
            this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
        }

        private void toolStripSelect_Click(object sender, System.EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;

            // 注册控件拾取事件
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            IPickResult pr = PickResult;
            this.axRenderControl1.FeatureManager.UnhighlightAll();

            if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                if (PickResult != null)
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
                                this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Yellow);

                                //string htmlPath = Path.Combine(Application.StartupPath.Substring(0, flag), @"Samples\CSharp\bin\HTMLWindowSample.html");
                                //WindowParam wp = htmlwindow.CreateWindowParam();
                                //wp.FilePath = htmlPath + "?id=" + fid;
                                //wp.SizeX = 200;
                                //wp.SizeY = 200;
                                //wp.Hastitle = true;
                                //wp.Round = 10;
                                //wp.HideOnClick = false;
                                //wp.WinId = fid;
                                //htmlwindow.ShowPopupWindowEx(IntersectPoint, wp, true);
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int featureId = int.Parse((sender as DataGridView).CurrentRow.Cells[0].Value.ToString());
            IFeatureClass curFeatureClass = null;
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                curFeatureClass = fc;
                IRowBuffer fdeRow = curFeatureClass.GetRow(featureId);
                if (fdeRow != null)
                {
                    int nPos = fdeRow.FieldIndex("Geometry");
                    if (nPos != -1 && !fdeRow.IsNull(nPos))
                    {
                        IPOI mp = fdeRow.GetValue(nPos) as IPOI; // 从库中读取值
                        IPoint pos = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        pos.Position = mp.Position;
                        pos.SpatialCRS = mp.SpatialCRS;
                        IEulerAngle angle = new EulerAngle();
                        angle.Heading = 0;
                        angle.Tilt = -60;
                        angle.Roll = 0;
                        this.axRenderControl1.Camera.LookAt2(pos, 3000, angle);
                        this.axRenderControl1.FeatureManager.UnhighlightAll();
                        this.axRenderControl1.FeatureManager.HighlightFeature(curFeatureClass, int.Parse(fdeRow.GetValue(0).ToString()), System.Drawing.Color.Yellow);

                        mp.ImageName = (strMediaPath + @"\png\lan.png");
                        IRenderPOI rpoi = this.axRenderControl1.ObjectManager.CreateRenderPOI(mp);
                        rpoi.Highlight(System.Drawing.Color.Red);
                        this.axRenderControl1.ObjectManager.DelayDelete(rpoi.Guid, 60000);
                    }
                }
            }
        }

        // 初始化表结构
        public DataTable CreateDataTable(IFeatureClass fc)
        {
            DataTable dt = new DataTable();
            if (fc != null)
            {
                IFieldInfoCollection fInfoColl = fc.GetFields();
                DataColumn dc = null;
                for (int i = 0; i < fInfoColl.Count; ++i)
                {
                    IFieldInfo fInfo = fInfoColl.Get(i);
                    if (fInfo.FieldType == gviFieldType.gviFieldGeometry ||
                        fInfo.FieldType == gviFieldType.gviFieldBlob)
                        continue;

                    dc = new DataColumn(fInfo.Name);
                    switch (fInfo.FieldType)
                    {
                        case gviFieldType.gviFieldInt16:
                            {
                                dc.DataType = typeof(Int16);
                            }
                            break;
                        case gviFieldType.gviFieldInt32:
                            {
                                dc.DataType = typeof(Int32);
                            }
                            break;
                        case gviFieldType.gviFieldFID:
                            {
                                dc.DataType = typeof(Int32);
                                dc.ReadOnly = true;
                            }
                            break;
                        case gviFieldType.gviFieldInt64:
                            dc.DataType = typeof(Int64);
                            break;
                        case gviFieldType.gviFieldString:
                        case gviFieldType.gviFieldUUID:
                            dc.DataType = typeof(String);
                            break;
                        case gviFieldType.gviFieldFloat:
                            {
                                dc.DataType = typeof(float);
                            }
                            break;
                        case gviFieldType.gviFieldDouble:
                            dc.DataType = typeof(Double);
                            break;
                        case gviFieldType.gviFieldDate:
                            dc.DataType = typeof(DateTime);
                            break;
                        case gviFieldType.gviFieldGeometry:
                            dc.DataType = typeof(object);
                            break;
                        default:
                            dc.DataType = typeof(string);
                            break;
                    }
                    dt.Columns.Add(dc);
                }

                dt.DefaultView.Sort = "oid asc";
            }
            return dt;
        }

        private void GetResultSet(IFeatureClass fc, IQueryFilter filter, DataTable dt)
        {
            if (fc != null)
            {
                IFdeCursor cursor = null;
                try
                {
                    if (filter != null)
                    {
                        filter.PostfixClause = "order by oid asc";
                    }
                    // 查找所有记录
                    cursor = fc.Search(filter, true);
                    if (cursor != null)
                    {
                        dt.BeginLoadData();
                        IRowBuffer fdeRow = null;
                        DataRow dr = null;
                        while ((fdeRow = cursor.NextRow()) != null)
                        {
                            dr = dt.NewRow();
                            for (int i = 0; i < dt.Columns.Count; ++i)
                            {
                                string strColName = dt.Columns[i].ColumnName;
                                int nPos = fdeRow.FieldIndex(strColName);
                                if (nPos == -1 || fdeRow.IsNull(nPos))
                                    continue;
                                object v = fdeRow.GetValue(nPos);  // 从库中读取值
                                dr[i] = v;
                            }
                            dt.Rows.Add(dr);
                        }
                        dt.EndLoadData();
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
        }


    }
}
