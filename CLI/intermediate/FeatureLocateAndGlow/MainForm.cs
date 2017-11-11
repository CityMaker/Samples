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
//author	yuanying
//date	2012/07/30
using System;
using System.Collections.Generic;
using System.Data;
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


namespace FeatureLocateAndGlow
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private System.Guid rootId = new System.Guid();

        public MainForm()
        {
            InitializeComponent();
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
                        this.axRenderControl1.Camera.LookAt(env.Center, 600, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion 加载FDB场景

            #region 查询所有值
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                DataTable dt = CreateDataTable(fc);
                GetResultSet(fc, null, dt);
                switch (fc.Name)
                {
                    case "Road":
                        this.dataGridView0Road.DataSource = dt;
                        break;
                    case "Building":
                        this.dataGridView1Building.DataSource = dt;
                        break;
                    case "Trees":
                        this.dataGridView2Trees.DataSource = dt;
                        break;
                    case "Facility":
                        this.dataGridView3Facility.DataSource = dt;
                        break;
                    case "Landscape":
                        this.dataGridView4LandScape.DataSource = dt;
                        break;
                }
            }
            #endregion

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "FeatureLocateAndGlow.html";
            }
        }


        /// <summary>
        /// 定位和闪烁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int featureId = int.Parse((sender as DataGridView).CurrentRow.Cells[0].Value.ToString());
            IFeatureClass curFeatureClass = null;
            switch (tabControl1.SelectedTab.Name)
            {
                case "tabPage0Road":
                    {
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Name == "Road")
                                curFeatureClass = fc;
                        }
                    }
                    break;
                case "tabPage1Building":
                    {
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Name == "Building")
                                curFeatureClass = fc;
                        }
                    }
                    break;
                case "tabPage2Trees":
                    {
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Name == "Trees")
                                curFeatureClass = fc;
                        }
                    }
                    break;
                case "tabPage3Facility":
                    {
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Name == "Facility")
                                curFeatureClass = fc;
                        }
                    }
                    break;
                case "tabPage4LandScape":
                    {
                        foreach (IFeatureClass fc in fcMap.Keys)
                        {
                            if (fc.Name == "Landscape")
                                curFeatureClass = fc;
                        }
                    }
                    break;
            }

            string filterString = string.Format("oid={0}", featureId);
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = filterString;
            IFdeCursor cursor = null;
            try
            {
                cursor = curFeatureClass.Search(filter, true);
                if (cursor != null)
                {
                    IRowBuffer fdeRow = null;
                    if ((fdeRow = cursor.NextRow()) != null)
                    {

                        int nPos = fdeRow.FieldIndex("Geometry");
                        if (nPos != -1 && !fdeRow.IsNull(nPos))
                        {
                            IModelPoint mp = fdeRow.GetValue(nPos) as IModelPoint;  // 从库中读取值
                            IModelPointSymbol symbol = new ModelPointSymbol();
                            symbol.SetResourceDataSet(curFeatureClass.FeatureDataSet);
                            IRenderModelPoint rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(mp, symbol, rootId);
                            if (rmp != null)
                            {
                                // 定位
                                this.axRenderControl1.Camera.FlyToObject(rmp.Guid, gviActionCode.gviActionJump);
                                // 闪烁
                                rmp.Glow(2000);
                                this.axRenderControl1.ObjectManager.DelayDelete(rmp.Guid, 2000);
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
