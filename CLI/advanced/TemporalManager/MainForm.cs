using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using Gvitech;
using Gvitech.CityMaker.Controls;

namespace TemporalManager
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcGeosMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private Hashtable fcLayerMap = null;
        private IEulerAngle angle = new EulerAngle();
        private IVector3 position = new Vector3();

        private TreeNode selectNode = null;  //标记treeView控件中当前被选中的节点
        private IObjectEditor geoEditor = null;
        private IRenderPoint renderGeometry = null;
        private IGeometry geometry = null;
        private IFeatureLayer curLayer = null;  //当前选中的layer
        private int insertType = -1;  // 0插入新要素 1插入新时序  2更新新时序

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

            // 加载时态FDB
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\HeatMap.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);

                //Add TreeNode
                myTreeNode sourceNode = new myTreeNode(ci.Database, ci);
                this.treeView1.Nodes.Add(sourceNode);

                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);

                //Add TreeNode
                TreeNode setNode = new TreeNode(dataset.Name, 1, 1);
                sourceNode.Nodes.Add(setNode);

                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                fcGeosMap = new Hashtable(fcnames.Length);
                fcLayerMap = new Hashtable(fcnames.Length);
                foreach (string name in fcnames)
                {
                    IFeatureClass fc = dataset.OpenFeatureClass(name);

                    //Add TreeNode
                    TreeNode fcNode = new TreeNode(name, 2, 2);
                    fcNode.ContextMenuStrip = this.contextMenuStrip1;  // 绑定右键菜单
                    setNode.Nodes.Add(fcNode);

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
                    fcGeosMap.Add(fc.Guid, geoNames);

                    fc.Dispose();
                    fc = null;
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            // 加载配套场景FDB
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
                        
                        IFeatureLayer layer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, fieldinfo.Name, null, null, rootId);
                        IEnvelope env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;
                        this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
                        break;
                    }

                    fc.Dispose();
                    fc = null;
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            this.treeView1.ExpandAll();

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "TemporalManager.html";
            }
        }


        /// <summary>
        /// 单击行头，highlight对应要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                IRowBuffer fdeRow = fc.GetRow(oid);
                if (fdeRow == null)
                {
                    if (fc.HasTemporal())
                    {
                        ITemporalManager tm = fc.TemporalManager;
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = tm.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                fdeRow = tinstance.GetRowBuffer();
                            }
                        }
                        tcursor.Dispose();
                        tcursor = null;
                    }
                }
                int nPos = fdeRow.FieldIndex("Geometry");
                if (nPos == -1) return;
                IPoint mp = fdeRow.GetValue(nPos) as IPoint;
                if (mp != null)
                {
                    this.axRenderControl1.FeatureManager.UnhighlightFeatureClass(fc);
                    this.axRenderControl1.FeatureManager.HighlightFeature(fc, oid, System.Drawing.Color.Yellow);
                }

                //获取当前要素的所有时序
                if (fc.HasTemporal())
                {
                    ITemporalManager tm = fc.TemporalManager;
                    TemporalFilter tfilter = new TemporalFilter();
                    tfilter.WhereClause = "oid=" + oid;
                    ITemporalCursor tcursor = tm.Search(tfilter);
                    if (tcursor.MoveNext())
                    {
                        ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                        ITemporalInstance tinstance = null;
                        List<DateTime> timelist = new List<DateTime>();
                        while ((tinstance = tinstanceCursor.NextInstance()) != null)
                        {
                            timelist.Add(tinstance.StartDatetime);
                            timelist.Add(tinstance.EndDatetime);
                        }
                        this.listBoxTemporalInstanceTime.DataSource = timelist;
                        tinstanceCursor.Dispose();
                        tinstanceCursor = null;
                    }
                    tcursor.Dispose();
                    tcursor = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        /// <summary>
        /// 双击行头，相机定位到要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                IRowBuffer fdeRow = fc.GetRow(oid);
                if (fdeRow == null)
                {
                    if (fc.HasTemporal())
                    {
                        ITemporalManager tm = fc.TemporalManager;
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = tm.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                fdeRow = tinstance.GetRowBuffer();
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                    }
                }
                int nPos = fdeRow.FieldIndex("Geometry");
                if (nPos == -1) return;
                IPoint mp = fdeRow.GetValue(nPos) as IPoint;
                if (mp != null)
                {
                    this.axRenderControl1.Camera.LookAtEnvelope(mp.Envelope);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            selectNode = this.treeView1.GetNodeAt(e.X, e.Y);
            if (selectNode == null) return;

            if (selectNode.Level == 2)
            {
                string fc_name = selectNode.Text;
                string set_name = selectNode.Parent.Text;
                myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
                IConnectionInfo ci = node.con;

                IDataSource ds = null;
                IFeatureClass fc = null;
                try
                {
                    IDataSourceFactory dsFactory = new DataSourceFactory();
                    ds = dsFactory.OpenDataSource(ci);
                    IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                    fc = dataset.OpenFeatureClass(fc_name);

                    curLayer = null;  //置空
                    foreach (Guid fcInMap in fcLayerMap.Keys)
                    {
                        if (fcInMap.Equals(fc.Guid))
                            curLayer = fcLayerMap[fcInMap] as IFeatureLayer;
                    }

                    if (curLayer != null)
                    {
                        if (curLayer.EnableTemporal)
                        {
                            this.cbLayerEnableTemporal.Checked = true;
                        }
                        else
                        {
                            this.cbLayerEnableTemporal.Checked = false;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    fc.Dispose();
                    fc = null;
                    ds.Dispose();
                    ds = null;
                }
            }
        }

        private void hasTemporalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                MessageBox.Show(fc.HasTemporal().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void enableTemporalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                fc.LockType = gviLockType.gviLockExclusiveSchema;
                DateTime defaultBirthDatetime = new DateTime(2000, 1, 1);
                fc.EnableTemporal(defaultBirthDatetime, "", "");
                fc.LockType = gviLockType.gviLockSharedSchema;

                MessageBox.Show("开启时态成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void disableTemporalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);
                fc.LockType = gviLockType.gviLockExclusiveSchema;
                fc.DisableTemporal();
                fc.LockType = gviLockType.gviLockSharedSchema;

                MessageBox.Show("关闭时态成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void searchAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);
                if (fc.HasTemporal())
                {
                    ITemporalManager tm = fc.TemporalManager;
                    TemporalFilter filter = new TemporalFilter();
                    // 初始化表格
                    DataTable dt = CreateDataTable(fc);
                    // 查找数据
                    GetResultSet(fc, filter, dt, false);
                    // 显示表格
                    this.dataGridView1.DataSource = dt;
                }
                else
                {
                    IQueryFilter filter = new QueryFilter();
                    // 初始化表格
                    DataTable dt = CreateDataTable(fc);
                    // 查找数据
                    GetResultSet(fc, filter, dt);
                    // 显示表格
                    this.dataGridView1.DataSource = dt;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void searchBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);
                if (fc.HasTemporal())
                {
                    ITemporalManager tm = fc.TemporalManager;
                    TemporalFilter filter = new TemporalFilter();
                    // 初始化表格
                    DataTable dt = CreateDataTable(fc);
                    // 查找数据
                    GetResultSet(fc, filter, dt, true);
                    // 显示表格
                    this.dataGridView1.DataSource = dt;
                }
                else
                    MessageBox.Show("未开启时态");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void getKeyDatetimesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);
                ITemporalManager tm = fc.TemporalManager;
                DateTime[] times = tm.GetKeyDatetimes();
                if (tm.GetKeyDatetimes().Count() > 1)
                {
                    this.trackBarTime.Minimum = (int)times[0].Ticks;
                    this.trackBarTime.Maximum = (int)times[times.Length - 1].Ticks;
                }
                this.trackBarTime.Value = this.trackBarTime.Minimum;
                DateTime d = DateTime.FromOADate((double)this.trackBarTime.Value);
                this.labelTime.Text = d.ToString("d");
                //设置两个时间控件
                this.dateTimePickerTrackbarBeginDate.Value = (times[0]);
                if ((times[times.Length - 1]).Equals(tm.InfinityDatetime))
                    this.dateTimePickerTrackbarEndDate.Value = this.dateTimePickerTrackbarEndDate.MaxDate;
                else
                    this.dateTimePickerTrackbarEndDate.Value = (times[times.Length - 1]);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void stopRenderingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                foreach (Guid fcGuidInMap in fcLayerMap.Keys)
                {
                    if (fc.Guid.Equals(fcGuidInMap))
                    {
                        this.axRenderControl1.ObjectManager.DeleteObject((fcLayerMap[fcGuidInMap] as IFeatureLayer).Guid);
                        fcLayerMap.Remove(fcGuidInMap);
                        break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void startRenderingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("一旦开启图层渲染，再操作时态时需关闭exe程序，否则锁的问题会导致操作时态失败。是否确认开启渲染？", "", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;

            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                foreach (Guid fcInFCMap in fcGeosMap.Keys)
                {
                    if (fc.Guid.Equals(fcInFCMap))
                    {
                        List<string> geoNames = (List<string>)fcGeosMap[fcInFCMap];
                        foreach (string geoName in geoNames)
                        {
                            if (!geoName.Equals("Geometry"))
                                continue;

                            IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                            fc, geoName, null, null, rootId);
                            fcLayerMap.Add(fc.Guid, featureLayer);

                            IFieldInfoCollection fieldinfos = fc.GetFields();
                            IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                            IEnvelope env = geometryDef.Envelope;
                            if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                                env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                                continue;
                            angle.Set(0, -20, 0);
                            this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void trackBarTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime d = DateTime.FromOADate((double)this.trackBarTime.Value);
            this.labelTime.Text = d.ToString("");

            if (curLayer != null)
                curLayer.Time = d;
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


        private void GetResultSet(IFeatureClass fc, TemporalFilter filter, DataTable dt, bool searchBase)
        {
            if (fc != null)
            {
                ITemporalCursor cursor = null;
                try
                {
                    if (filter != null)
                    {
                        filter.PostfixClause = "order by oid asc";
                    }
                    // 查找所有记录
                    ITemporalManager tm = fc.TemporalManager;
                    cursor = tm.Search(filter);
                    if (cursor != null)
                    {
                        dt.BeginLoadData();
                        DataRow dr = null;
                        while (cursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = cursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            IRowBuffer fdeRow = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                fdeRow = tinstance.GetRowBuffer();
                                if (searchBase)  //查询base表
                                {
                                    if (tinstance.EndDatetime == tm.InfinityDatetime)
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
                                }
                                else  //查出所有数据
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
                            }
                            if (tinstanceCursor != null)
                            {
                                tinstanceCursor.Dispose();
                                tinstanceCursor = null;
                            }
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

        private void cbLayerEnableTemporal_CheckedChanged(object sender, EventArgs e)
        {
            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                foreach (Guid fcInMap in fcLayerMap.Keys)
                {
                    if (fcInMap.Equals(fc.Guid))
                        curLayer = fcLayerMap[fcInMap] as IFeatureLayer;
                }

                if (this.cbLayerEnableTemporal.Checked)
                {
                    curLayer.EnableTemporal = true;
                }
                else
                {
                    curLayer.EnableTemporal = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void btnInsertFeature_Click(object sender, EventArgs e)
        {
            if (renderGeometry != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderGeometry.Guid);
                renderGeometry = null;
            }

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int nPos = fc.GetFields().IndexOf("Geometry");
                if (nPos == -1) return;
                IFieldInfo fieldInfo = fc.GetFields().Get(nPos);
                gviVertexAttribute va = fieldInfo.GeometryDef.VertexAttribute;

                //注册事件
                geoEditor = this.axRenderControl1.ObjectEditor;
                this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
                
                this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
                

                IGeometryFactory geoFactory = new GeometryFactory();
                IPoint point = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, va) as IPoint;
                point.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                point.SetCoords(0, 0, 0, 0, 0);

                ISimplePointSymbol symbol = new SimplePointSymbol();
                symbol.Size = 10;
                renderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPoint(point, symbol, rootId);
                renderGeometry.MaxVisibleDistance = 10000;
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                bool bRet = geoEditor.StartEditRenderGeometry(renderGeometry, gviGeoEditType.gviGeoEditCreator);
                if (!bRet)
                {
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                    MessageBox.Show("编辑错误");
                }
                insertType = 0;
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                insertType = -1;
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

   
        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            geometry = Geometry;
            
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);
                if (fc == null) return;   

                this.axRenderControl1.ObjectManager.DeleteObject(renderGeometry.Guid);
                renderGeometry = null;                             

                //fc
                IRowBuffer row = null;
                if (insertType == 0)
                {
                    row = fc.CreateRowBuffer();
                }
                else if (insertType == 1)
                {
                    int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                    TemporalFilter tfilter = new TemporalFilter();
                    tfilter.WhereClause = "oid=" + oid;
                    ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                    if (tcursor.MoveNext())
                    {
                        ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                        ITemporalInstance tinstance = null;
                        while ((tinstance = tinstanceCursor.NextInstance()) != null)
                        {
                            row = tinstance.GetRowBuffer();
                        }
                        tinstanceCursor.Dispose();
                        tinstanceCursor = null;
                    }
                    tcursor.Dispose();
                    tcursor = null;
                }
                else if (insertType == 2)
                {
                    int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                    if (this.listBoxTemporalInstanceTime.SelectedIndex % 2 == 0)
                    {
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                if (tinstance.StartDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                                {
                                    row = tinstance.GetRowBuffer();
                                    break;
                                }
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                    }
                    else
                    {
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                if (tinstance.EndDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                                {
                                    row = tinstance.GetRowBuffer();
                                    break;
                                }
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                    }
                }

                double hotValue = 0.0;
                UserDataForm form = new UserDataForm();
                if (insertType == 2)
                {
                    int nnPoseName = row.FieldIndex("HotValue");
                    if (nnPoseName == -1)
                        return;
                    form.SetHotValue(double.Parse(row.GetValue(nnPoseName).ToString()));
                }
                if (form.ShowDialog() == DialogResult.OK)
                {
                    hotValue = form.hotvalue;
                }

                int nPoseName = row.FieldIndex("HotValue");
                if (nPoseName == -1)
                    return;
                row.SetValue(nPoseName, hotValue);

                IPoint point = geometry as IPoint;
                if (point != null)
                {
                    int geoPose = fc.GetFields().IndexOf("Geometry");
                    row.SetValue(geoPose, point);
                    nPoseName = row.FieldIndex("pos");
                    row.SetValue(nPoseName, point.X + "_" + point.Y);
                }                

                ITemporalManager tm = fc.TemporalManager;
                if (insertType == 0)
                {
                    tm.Insert(this.dateTimePickerBirthDate.Value, row);
                    this.axRenderControl1.FeatureManager.CreateFeature(fc, row);
                }
                else if (insertType == 1)
                {
                    int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                    TemporalFilter tfilter = new TemporalFilter();
                    tfilter.WhereClause = "oid=" + oid;
                    ITemporalCursor tcursor = tm.Search(tfilter);
                    if (tcursor.MoveNext())
                    {
                        tcursor.Insert(this.dateTimePickerTemporalBirthDate.Value, row);
                    }
                    tcursor.Dispose();
                    tcursor = null;
                }
                else if (insertType == 2)
                {
                    int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                    if (this.listBoxTemporalInstanceTime.SelectedIndex % 2 == 0)
                    {
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                if (tinstance.StartDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                                {
                                    tinstanceCursor.Update(row);   
                                    break;
                                }                                                                 
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                    }
                    else
                    {
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {  
                                if (tinstance.EndDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                                {
                                    tinstanceCursor.Update(row);
                                    break;
                                } 
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                    }
                }

                this.axRenderControl1.Refresh();

                TemporalFilter filter = new TemporalFilter();
                // 初始化表格
                DataTable dt = CreateDataTable(fc);
                // 查找数据
                GetResultSet(fc, filter, dt);
                // 显示表格
                this.dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;

                this.axRenderControl1.RcObjectEditFinish -= new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
                
                this.axRenderControl1.RcObjectEditing -= new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
                
            }
        }

        private void btnSetBeginDate_Click(object sender, EventArgs e)
        {
            this.trackBarTime.Minimum = (int)Utils.DateToDouble(this.dateTimePickerTrackbarBeginDate.Value);
            this.trackBarTime.Value = this.trackBarTime.Minimum;
            DateTime d = DateTime.FromOADate((double)this.trackBarTime.Value);
            this.labelTime.Text = d.ToString("d");
        }

        private void btnSetEndDate_Click(object sender, EventArgs e)
        {
            this.trackBarTime.Maximum = (int)Utils.DateToDouble(this.dateTimePickerTrackbarEndDate.Value);
            this.trackBarTime.Value = this.trackBarTime.Maximum;
            DateTime d = DateTime.FromOADate((double)this.trackBarTime.Value);
            this.labelTime.Text = d.ToString("d");
        }

        private void btnSetFeatureDeadDate_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要设定死亡时间的要素");
                return;
            }

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                ITemporalManager tm = fc.TemporalManager;
                TemporalFilter tfilter = new TemporalFilter();
                tfilter.WhereClause = "oid=" + oid;
                ITemporalCursor tcursor = tm.Search(tfilter);
                if (tcursor.MoveNext())
                {
                    tcursor.Dead(this.dateTimePickerDeadDate.Value);
                }
                tcursor.Dispose();
                tcursor = null;
                this.axRenderControl1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void btnResetBirthDate_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要重置出生时间的要素");
                return;
            }

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                ITemporalManager tm = fc.TemporalManager;
                TemporalFilter tfilter = new TemporalFilter();
                tfilter.WhereClause = "oid=" + oid;
                ITemporalCursor tcursor = tm.Search(tfilter);
                if (tcursor.MoveNext())
                {
                    tcursor.ResetBirthDatetime(this.dateTimePickerBirthDate.Value);
                }
                tcursor.Dispose();
                tcursor = null;
                this.axRenderControl1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void btnDeleteFeature_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的要素");
                return;
            }

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                IQueryFilter qFilter = new QueryFilter();
                qFilter.WhereClause = "oid=" + oid;
                fc.Delete(qFilter);

                this.axRenderControl1.Refresh();
                
                TemporalFilter filter = new TemporalFilter();
                // 初始化表格
                DataTable dt = CreateDataTable(fc);
                // 查找数据
                GetResultSet(fc, filter, dt);
                // 显示表格
                this.dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void btnInsertTemporal_Click(object sender, EventArgs e)
        {
            if (renderGeometry != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderGeometry.Guid);
                renderGeometry = null;
            }

            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要插入新时序的要素");
                return;
            }

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int nPos = fc.GetFields().IndexOf("Geometry");
                if (nPos == -1) return;
                IFieldInfo fieldInfo = fc.GetFields().Get(nPos);
                gviVertexAttribute va = fieldInfo.GeometryDef.VertexAttribute;

                //注册事件
                geoEditor = this.axRenderControl1.ObjectEditor;
                this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
                
                this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
                

                IGeometryFactory geoFactory = new GeometryFactory();
                IPoint point = geoFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, va) as IPoint;
                point.SpatialCRS = fc.FeatureDataSet.SpatialReference;
                point.SetCoords(0, 0, 0, 0, 0);

                ISimplePointSymbol symbol = new SimplePointSymbol();
                symbol.Size = 10;
                renderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPoint(point, symbol, rootId);
                renderGeometry.MaxVisibleDistance = 10000;
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                bool bRet = geoEditor.StartEditRenderGeometry(renderGeometry, gviGeoEditType.gviGeoEditCreator);
                if (!bRet)
                {
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                    MessageBox.Show("编辑错误");
                }
                insertType = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                insertType = -1;
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void btnDeleteTemporal_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除指定时序的要素");
                return;
            }
            if (this.listBoxTemporalInstanceTime.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要删除的时序");
                return;
            }

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                if (this.listBoxTemporalInstanceTime.SelectedIndex % 2 == 0)
                {
                    if (MessageBox.Show("确定是否删除起始时间为" + listBoxTemporalInstanceTime.SelectedItem.ToString() + "的时序？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                if (tinstance.StartDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                                    tinstanceCursor.Delete();
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                    }
                }
                else
                {
                    if (MessageBox.Show("确定是否删除结束时间为" + listBoxTemporalInstanceTime.SelectedItem.ToString() + "的时序？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                if (tinstance.EndDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                                    tinstanceCursor.Delete();
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                    }
                }

                this.axRenderControl1.Refresh();

                //获取当前要素的所有时序
                if (fc.HasTemporal())
                {
                    ITemporalManager tm = fc.TemporalManager;
                    TemporalFilter tfilter = new TemporalFilter();
                    tfilter.WhereClause = "oid=" + oid;
                    ITemporalCursor tcursor = tm.Search(tfilter);
                    if (tcursor.MoveNext())
                    {
                        ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                        ITemporalInstance tinstance = null;
                        List<DateTime> timelist = new List<DateTime>();
                        while ((tinstance = tinstanceCursor.NextInstance()) != null)
                        {
                            timelist.Add(tinstance.StartDatetime);
                            timelist.Add(tinstance.EndDatetime);
                        }
                        this.listBoxTemporalInstanceTime.DataSource = timelist;
                        tinstanceCursor.Dispose();
                        tinstanceCursor = null;
                    }
                    tcursor.Dispose();
                    tcursor = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void btnUpdateTemporal_Click(object sender, EventArgs e)
        {
            if (renderGeometry != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderGeometry.Guid);
                renderGeometry = null;
            }

            if (this.listBoxTemporalInstanceTime.SelectedIndex == -1)
                return;

            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要更新指定时序的要素");
                return;
            }
            if (this.listBoxTemporalInstanceTime.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要更新的时序");
                return;
            }

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                if (this.listBoxTemporalInstanceTime.SelectedIndex % 2 == 0)
                {
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                if (tinstance.StartDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                                {
                                    IRowBuffer row = tinstance.GetRowBuffer();
                                    int nPos = fc.GetFields().IndexOf("Geometry");
                                    if (nPos == -1) return;
                                    IPoint point = row.GetValue(nPos) as IPoint;

                                    //注册事件
                                    geoEditor = this.axRenderControl1.ObjectEditor;
                                    this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
                                    
                                    this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
                                    
                                    ISimplePointSymbol symbol = new SimplePointSymbol();
                                    symbol.Size = 10;
                                    renderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPoint(point, symbol, rootId);
                                    renderGeometry.MaxVisibleDistance = 10000;
                                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                                    bool bRet = geoEditor.StartEditRenderGeometry(renderGeometry, gviGeoEditType.gviGeoEdit2DMove);
                                    if (!bRet)
                                    {
                                        this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                                        MessageBox.Show("编辑错误");
                                    }
                                    insertType = 2;

                                    break;
                                }
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                }
                else
                {
                        TemporalFilter tfilter = new TemporalFilter();
                        tfilter.WhereClause = "oid=" + oid;
                        ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                        if (tcursor.MoveNext())
                        {
                            ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                            ITemporalInstance tinstance = null;
                            while ((tinstance = tinstanceCursor.NextInstance()) != null)
                            {
                                if (tinstance.EndDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                                    {
                                    IRowBuffer row = tinstance.GetRowBuffer();
                                    int nPos = fc.GetFields().IndexOf("Geometry");
                                    if (nPos == -1) return;
                                    IPoint point = row.GetValue(nPos) as IPoint;

                                    //注册事件
                                    geoEditor = this.axRenderControl1.ObjectEditor;
                                    this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
                                    
                                    this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
                                    
                                    ISimplePointSymbol symbol = new SimplePointSymbol();
                                    symbol.Size = 10;
                                    renderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPoint(point, symbol, rootId);
                                    renderGeometry.MaxVisibleDistance = 10000;
                                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                                    bool bRet = geoEditor.StartEditRenderGeometry(renderGeometry, gviGeoEditType.gviGeoEdit2DMove);
                                    if (!bRet)
                                    {
                                        this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                                        MessageBox.Show("编辑错误");
                                    }
                                    insertType = 2;

                                    break;
                                }
                            }
                            tinstanceCursor.Dispose();
                            tinstanceCursor = null;
                        }
                        tcursor.Dispose();
                        tcursor = null;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

        private void listBoxTemporalInstanceTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (renderGeometry != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(renderGeometry.Guid);
                renderGeometry = null;
            }

            if (this.listBoxTemporalInstanceTime.SelectedIndex == -1)
                return;

            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要更新指定时序的要素");
                return;
            }
            if (this.listBoxTemporalInstanceTime.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要更新的时序");
                return;
            }

            this.treeView1.Focus();
            if (selectNode == null) { MessageBox.Show("请先选择指定fc"); return; }
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureClass fc = null;
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                if (this.listBoxTemporalInstanceTime.SelectedIndex % 2 == 0)
                {
                    TemporalFilter tfilter = new TemporalFilter();
                    tfilter.WhereClause = "oid=" + oid;
                    ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                    if (tcursor.MoveNext())
                    {
                        ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                        ITemporalInstance tinstance = null;
                        while ((tinstance = tinstanceCursor.NextInstance()) != null)
                        {
                            if (tinstance.StartDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                            {
                                IRowBuffer row = tinstance.GetRowBuffer();
                                int nPos = fc.GetFields().IndexOf("Geometry");
                                if (nPos == -1) return;
                                IPoint point = row.GetValue(nPos) as IPoint;

                                ISimplePointSymbol symbol = new SimplePointSymbol();
                                symbol.Size = 10;
                                renderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPoint(point, symbol, rootId);
                                renderGeometry.MaxVisibleDistance = 10000;
                            }
                        }
                        tinstanceCursor.Dispose();
                        tinstanceCursor = null;
                    }
                    tcursor.Dispose();
                    tcursor = null;
                }
                else
                {
                    TemporalFilter tfilter = new TemporalFilter();
                    tfilter.WhereClause = "oid=" + oid;
                    ITemporalCursor tcursor = fc.TemporalManager.Search(tfilter);
                    if (tcursor.MoveNext())
                    {
                        ITemporalInstanceCursor tinstanceCursor = tcursor.GetTemporalInstances(false);
                        ITemporalInstance tinstance = null;
                        while ((tinstance = tinstanceCursor.NextInstance()) != null)
                        {
                            if (tinstance.EndDatetime.Equals((DateTime)listBoxTemporalInstanceTime.SelectedItem))
                            {
                                IRowBuffer row = tinstance.GetRowBuffer();
                                int nPos = fc.GetFields().IndexOf("Geometry");
                                if (nPos == -1) return;
                                IPoint point = row.GetValue(nPos) as IPoint;

                                ISimplePointSymbol symbol = new SimplePointSymbol();
                                symbol.Size = 10;
                                renderGeometry = this.axRenderControl1.ObjectManager.CreateRenderPoint(point, symbol, rootId);
                                renderGeometry.MaxVisibleDistance = 10000;
                            }
                        }
                        tinstanceCursor.Dispose();
                        tinstanceCursor = null;
                    }
                    tcursor.Dispose();
                    tcursor = null;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fc.Dispose();
                fc = null;
                ds.Dispose();
                ds = null;
            }
        }

    }

    class myTreeNode : TreeNode
    {
        public string name;
        public IConnectionInfo con;

        public myTreeNode(string s, IConnectionInfo c)
        {
            name = s;
            con = c;
            this.Text = s;
        }
    }
}
