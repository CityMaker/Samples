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


namespace RenderPipeLine
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IFeatureLayer featureLayer = null;
        private System.Guid rootId = new System.Guid();

        private Hashtable pipeMap = null;  //rowId, IRenderPipeLine 
        private gviRenderPipeLinePlayMode playMode = gviRenderPipeLinePlayMode.gviPipeLinePlayShowTrack;
        private float duration = 0.0f;
        private bool needLoop = false;
        private ArrayList currentPLs = new ArrayList();
        private ArrayList highlightPLs = new ArrayList();

        public MainForm()
        {
            InitializeComponent();

            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;
            this.axRenderControl1.Camera.FlyTime = 1;

            // 设置天空盒
            
            if(System.IO.Directory.Exists(strMediaPath))
            {
                //string tmpSkyboxPath = strMediaPath + @"\skybox";
                //ISkyBox skybox = this.axRenderControl1.ObjectManager.GetSkyBox(0);
                //skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\1_BK.jpg");
                //skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\1_DN.jpg");
                //skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\1_FR.jpg");
                //skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\1_LF.jpg");
                //skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\1_RT.jpg");
                //skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\1_UP.jpg");   
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
                string tmpFDBPath = (strMediaPath + @"\polyline.FDB");
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

                    ISimpleTextRender tr = new SimpleTextRender();
                    tr.Expression = "$(oid)";
                    tr.MinimizeOverlap = true;
                    tr.DynamicPlacement = true;
                    ITextSymbol ts = new TextSymbol();
                    tr.Symbol = ts;
                    featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, tr, null, rootId);

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
                pipeMap = new Hashtable();
                GetResultSet(fc, null, dt);
                this.dataGridViewPolyline.DataSource = dt;
            }
            #endregion

            this.comboBoxPlayMode.SelectedIndex = 2;

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectRenderPipeLine;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectDrag;
            //this.axRenderControl1.RcMouseClickSelect += AxRenderControl1_RcMouseClickSelect;
            this.axRenderControl1.RcMouseDragSelect += AxRenderControl1_RcMouseDragSelect;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "RenderPipeLine.html";
            }
        }

        private void AxRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            if (PickResult == null)
                return;

            IRenderPipeLinePickResult pr = PickResult as IRenderPipeLinePickResult;
            if (pr == null)
                return;

            GetParam();
            IRenderPipeLine rpl = pr.RenderPipeLine;
            rpl.Color = System.Drawing.Color.Yellow;
            rpl.Play(playMode, duration, needLoop);
            currentPLs.Add(rpl);
        }

        private void AxRenderControl1_RcMouseDragSelect(IPickResultCollection PickResults, gviModKeyMask Mask)
        {
            if (PickResults == null)
                return;

            for (int i = 0; i < PickResults.Count; i++)
            {
                IRenderPipeLinePickResult pr = PickResults.Get(i) as IRenderPipeLinePickResult;
                if (pr == null)
                    continue;

                GetParam();
                IRenderPipeLine rpl = pr.RenderPipeLine;
                rpl.Color = System.Drawing.Color.Yellow;
                rpl.Play(playMode, duration, needLoop);
                currentPLs.Add(rpl);
            }
        }

        private void GetParam()
        {
            switch (comboBoxPlayMode.SelectedIndex)
            {
                case 0:
                    playMode = gviRenderPipeLinePlayMode.gviPipeLinePlayShowTrack;
                    break;
                case 1:
                    playMode = gviRenderPipeLinePlayMode.gviPipeLinePlayNoTrack;
                    break;
                case 2:
                    playMode = gviRenderPipeLinePlayMode.gviPipeLinePlayDrawTrack;
                    break;
            }

            needLoop = checkBoxNeedLoop.Checked;
            duration = (float)numericUpDownDuration.Value * 1000;
        }


        /// <summary>
        /// 定位和高亮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            object o = (sender as DataGridView).CurrentRow.Cells[0].Value;
            foreach (int oid in pipeMap.Keys)
            {
                if (oid == (int)o)
                {
                    if (highlightPLs != null && highlightPLs.Count > 0)
                    {
                        foreach (IRenderPipeLine pl in highlightPLs)
                        {
                            pl.Unhighlight();
                        }
                    }

                    highlightPLs = pipeMap[o] as ArrayList;
                    if (highlightPLs != null && highlightPLs.Count > 0)
                    {
                        bool needFly = true;
                        foreach (IRenderPipeLine pl in highlightPLs)
                        {
                            pl.Highlight(System.Drawing.Color.Yellow);
                            if (needFly)
                            {
                                this.axRenderControl1.Camera.FlyToObject(pl.Guid, gviActionCode.gviActionJump);
                                needFly = false;
                            }                            
                        }
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
                    if (fInfo.FieldType == gviFieldType.gviFieldBlob || fInfo.FieldType == gviFieldType.gviFieldGeometry)
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

                                if (i == 0)
                                {
                                    // 创建管子
                                    int geoPos = fdeRow.FieldIndex("Geometry");
                                    IGeometry geo = (IGeometry)fdeRow.GetValue(geoPos);
                                    if (geo.GeometryType == gviGeometryType.gviGeometryPolyline)
                                    {
                                        IPolyline pl = (IPolyline)fdeRow.GetValue(geoPos);
                                        IRenderPipeLine rpl = this.axRenderControl1.ObjectManager.CreateRenderPipeLine(pl, rootId);
                                        rpl.Radius = 10;
                                        rpl.Color = System.Drawing.Color.Red;
                                        ArrayList rpls = new ArrayList();
                                        rpls.Add(rpl);
                                        pipeMap.Add(v, rpls);
                                    }
                                    else if (geo.GeometryType == gviGeometryType.gviGeometryMultiPolyline)
                                    {
                                        IMultiPolyline multiPolyline = geo as IMultiPolyline;
                                        ArrayList rpls = new ArrayList();
                                        for (int g = 0; g < multiPolyline.GeometryCount; g++)
                                        {
                                            IPolyline plIndex = multiPolyline.GetGeometry(g) as IPolyline;
                                            IRenderPipeLine rpl = this.axRenderControl1.ObjectManager.CreateRenderPipeLine(plIndex, rootId);
                                            rpl.Radius = 10;
                                            rpl.Color = System.Drawing.Color.Red;
                                            rpls.Add(rpl);
                                        }
                                        pipeMap.Add(v, rpls);
                                    }
                                }
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

        private void checkBoxHideLayer_CheckedChanged(object sender, EventArgs e)
        {
            featureLayer.VisibleMask = checkBoxHideLayer.Checked ? gviViewportMask.gviViewNone : gviViewportMask.gviViewAllNormalView;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (currentPLs != null && currentPLs.Count > 0)
            {
                foreach (IRenderPipeLine pl in currentPLs)
                {
                    pl.Stop();
                }
                currentPLs.Clear();
            }
        }

    }
}
