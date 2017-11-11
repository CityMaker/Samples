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
//author	gs
//date	2011/09/26
using System;
using System.Collections.Generic;
using System.Linq;
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
using Gvitech.CityMaker.Controls;

namespace SpatialQuery
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private ISpatialCRS datasetCRS = null;
        private IGeometryFactory geoFactory = null;
        private ISpatialCRS shpCRS = null;


        private Dictionary<string, RowObject> rowMap = new Dictionary<string, RowObject>();

        private System.Guid rootId = new System.Guid();

        // 线程转发
        public int MainThreadId = 0;
        

        private void MainForm_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(false, ps);

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;
            this.axRenderControl1.Camera.FlyTime = 1;
            

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
                    featureLayer.MouseSelectMask = gviViewportMask.gviViewNone;

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
                            geoFactory = new GeometryFactory();
                        IPoint pos = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        pos.SpatialCRS = datasetCRS;
                        pos.Position = env.Center;
                        this.axRenderControl1.Camera.LookAt2(pos, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion 加载FDB场景

            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionShapeFile;
                string tmpShpPath = (strMediaPath + @"\shp\Singapore\BCA_EXISTING_BUILDING.shp");
                ci.Database = tmpShpPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);
                shpCRS = dataset.SpatialReference;
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                IFeatureClass fc = dataset.OpenFeatureClass(fcnames[0]);
                this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, "Geometry", null, null, rootId);               
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "SpatialQuery.html";
            }
        }



        public MainForm()
        {
            InitializeComponent();
            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             
        }


        #region 核心功能
        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (PickResult == null)
                return;

            this.dataGridView1.Rows.Clear();
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            rowMap.Clear();

            IPickResult pr = PickResult;
            if (pr.Type != gviObjectType.gviObjectFeatureLayer)
                return;
            IFeatureLayerPickResult fpr = pr as IFeatureLayerPickResult;
            IFeatureLayer layer = fpr.FeatureLayer;
            IFeatureClassInfo cinfo = layer.FeatureClassInfo;
            if (cinfo.DataSourceConnectionString.Contains("FireBird2x"))
                return;
            IDataSource ds = (new DataSourceFactory()).OpenDataSourceByString(cinfo.DataSourceConnectionString);
            IFeatureDataSet dset = ds.OpenFeatureDataset(cinfo.DataSetName);
            IFeatureClass shpfc = dset.OpenFeatureClass(cinfo.FeatureClassName);
            IRowBuffer shprow = shpfc.GetRow(fpr.FeatureId);
            int shpindex = shprow.FieldIndex("Geometry");
            IPolygon polygon = shprow.GetValue(shpindex) as IPolygon;
            if (polygon == null)
                return;

            IFdeCursor cursor = null;
            try
            {                  
                foreach (IFeatureClass fc in fcMap.Keys)
                {
                    IRowBuffer row = null;
                    List<IRowBuffer> list = new List<IRowBuffer>();

                    ISpatialFilter filter = new SpatialFilter();
                    filter.Geometry = polygon;
                    filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;
                    filter.GeometryField = "Geometry";
                    cursor = fc.Search(filter, false);
                    while ((row = cursor.NextRow()) != null)
                    {
                        list.Add(row);
                    }

                    foreach (IRowBuffer r in list)
                    {
                        int geometryIndex = -1;
                        geometryIndex = r.FieldIndex("Geometry");
                        if (geometryIndex != -1)
                        {
                            int oid = int.Parse(r.GetValue(0).ToString());
                            this.axRenderControl1.FeatureManager.HighlightFeature(fc, oid, System.Drawing.Color.Red);

                            string fid = "";
                            IEnvelope env = null;
                            for (int i = 0; i < r.FieldCount; i++)
                            {
                                string fieldName = r.Fields.Get(i).Name;
                                if (r.Fields.Get(i).Name == "oid")
                                {
                                    fid = r.GetValue(i).ToString();
                                }                                    
                                else if (r.Fields.Get(i).Name == "Geometry")
                                {
                                    IGeometry geometry = r.GetValue(i) as IModelPoint;
                                    env = geometry.Envelope;
                                }
                            }
                            RowObject ro = new RowObject() { FID = fid, FCGUID = fc.Guid.ToString(), FCName = fc.Name, FeatureClass = fc, Envelop = env };
                            if (!rowMap.ContainsKey(ro.FID + "/" + ro.FCGUID))
                            {
                                rowMap.Add(ro.FID + "/" + ro.FCGUID, ro);
                            }
                        }
                    } // end of foreach (IRowBuffer r in list)

                } // end of foreach (IFeatureClass fc in fcMap.Keys)
                this.Text = "分析完成！";
                LoadGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cursor != null)
                {
                    cursor.Dispose();
                    cursor = null;
                }
            }                
        }

        private void LoadGridView()
        {
            this.Text = string.Format("共有{0}个障碍物", rowMap.Count);
            this.dataGridView1.Rows.Clear();
            if (rowMap != null)
            {
                this.dataGridView1.RowCount = rowMap.Count;
                for (int i = 0; i < rowMap.Count; i++)
                {
                    this.dataGridView1.Rows[i].Cells["FID"].Value = rowMap.Values.ToArray()[i].FID;
                    this.dataGridView1.Rows[i].Cells["FID"].Tag = rowMap.Values.ToArray()[i].FeatureClass;
                    this.dataGridView1.Rows[i].Cells["FCName"].Value = rowMap.Values.ToArray()[i].FCName;
                }
            }
        }
        #endregion
              

        /// <summary>
        /// 单击表格记录进行变色和定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (string key in rowMap.Keys)
            {
                RowObject r = rowMap[key];
                int fid = int.Parse(r.FID);
                IFeatureClass fc = r.FeatureClass as IFeatureClass;
                this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Red);
            }
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            if (row != null)
            {
                string fid = row.Cells["FID"].Value.ToString();
                IFeatureClass fc = row.Cells["FID"].Tag as IFeatureClass;
                this.axRenderControl1.FeatureManager.HighlightFeature(fc, int.Parse(fid), System.Drawing.Color.Yellow);
                IEnvelope evn = rowMap[fid+"/"+fc.Guid].Envelop;
                IPoint pos = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pos.SpatialCRS = datasetCRS;
                pos.Position = evn.Center;
                IEulerAngle angle = new EulerAngle();
                angle.Set(0, -30, 0);
                this.axRenderControl1.Camera.LookAt2(pos, 40, angle);
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
        public string FCGUID
        {
            get;
            set;
        }
        public string FCName
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
    }
}
