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
//date	2013/11/20
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
using Gvitech.CityMaker.Controls;


namespace ScreenToWorld
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private ITableLabel tableLabel = null;
        private IPoint point = null;
        private IPickResult pic = null;

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

            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            this.axRenderControl1.RcMouseHover += new _IRenderControlEvents_RcMouseHoverEventHandler(axRenderControl1_RcMouseHover);
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "ScreenToWorld.html";
            }
        }

        /// <summary>
        /// 鼠标悬停事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        bool axRenderControl1_RcMouseHover(uint Flags, int X, int Y)
        {
            

            pic = this.axRenderControl1.Camera.ScreenToWorld(X, Y, out point);
            if (point == null || pic == null)
                return false;

            //////////////////////////////测试WorldToScreen接口////////////////////////////////////////////
            double screenx = 0.0, screeny = 0.0;
            bool inScreen = false;
            bool pval = this.axRenderControl1.Camera.WorldToScreen(point.X, point.Y, point.Z-10, out screenx, out screeny, 1, out inScreen);
            this.Text = pval.ToString();
            ///////////////////////////////////////////////////////////////////////////////////////////////

            IFeatureLayerPickResult pr = pic as IFeatureLayerPickResult;
            if (pr != null)
            {
                string featureId = pr.FeatureId.ToString();
                IFeatureClass featureClass = null;
                IFeatureLayer fl = pr.FeatureLayer;
                foreach (IFeatureClass fc in fcMap.Keys)
                {
                    if (fc.Guid.ToString() == fl.FeatureClassId.ToString())
                    {
                        featureClass = fc;
                        break;
                    }
                }
                if (featureClass != null)
                {
                    DataTable dt = CreateDataTable(featureClass);
                    string filterString = string.Format("oid={0}", featureId);
                    IQueryFilter filter = new QueryFilter();
                    filter.WhereClause = filterString;
                    GetResultSet(featureClass, filter, dt);
                    showInfo(dt, point);
                }
            }
            return false;
        }

        private void showInfo(DataTable dt, IPoint location)
        {
            if (tableLabel != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(tableLabel.Guid);
            }
            if (dt != null && dt.Rows.Count == 1)
            {
                int row = dt.Columns.Count;
                //创建一个TableLabel 指定 row ，column
                tableLabel = this.axRenderControl1.ObjectManager.CreateTableLabel(row, 2, rootId);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string columnName = dt.Columns[i].ColumnName;
                    tableLabel.SetRecord(i, 0, columnName);
                    tableLabel.SetRecord(i, 1, dt.Rows[0][columnName].ToString());
                }

                // 设定表位置
                tableLabel.Position = location;
                
                //表头
                tableLabel.TitleText = "拾取模型信息";
                //表头背景色
                tableLabel.TitleBackgroundColor = System.Drawing.Color.FromArgb(255, 255, 255, 200);

                tableLabel.SetColumnWidth(0, 80);

                tableLabel.SetColumnWidth(1, 200);
                
                //表的边框颜色
                tableLabel.BorderColor = System.Drawing.Color.Red;
                //表的边框的宽度
                tableLabel.BorderWidth = 2;
                //表的背景色
                tableLabel.TableBackgroundColor = System.Drawing.Color.FromArgb(200, 255, 255, 200);

                // 表头样式
                TextAttribute headerTextAttribute = new TextAttribute();
                headerTextAttribute.TextColor = System.Drawing.Color.FromArgb(255, 0, 0, 0);
                headerTextAttribute.OutlineColor = System.Drawing.Color.Red;
                headerTextAttribute.Font = "宋体";
                headerTextAttribute.TextSize = 12;
                headerTextAttribute.Bold = false;
                headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
                tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

                // 内容样式  列的内容 样式
                TextAttribute contentTextAttribute = new TextAttribute();
                contentTextAttribute.TextColor = System.Drawing.Color.FromArgb(255, 0, 0, 0); ;
                contentTextAttribute.OutlineColor = System.Drawing.Color.Red;
                contentTextAttribute.Font = "宋体";
                contentTextAttribute.Bold = false;
                contentTextAttribute.TextSize = 12;
                contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
                tableLabel.SetColumnTextAttribute(1, contentTextAttribute);

                // 标题样式
                TextAttribute capitalTextAttribute = new TextAttribute();
                capitalTextAttribute.TextColor = System.Drawing.Color.White;
                capitalTextAttribute.OutlineColor = System.Drawing.Color.Red;
                capitalTextAttribute.Font = "华文新魏";
                capitalTextAttribute.TextSize = 15;
                capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
                capitalTextAttribute.Bold = true;
                tableLabel.TitleTextAttribute = capitalTextAttribute;

                tableLabel.MouseSelectMask = 0;
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
                        cursor.Dispose();
                        cursor = null;
                    }
                }
            }
        }

    }
}
