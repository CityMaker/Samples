using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using System.Xml;
using Gvitech.CityMaker.Common;


namespace CatalogTreeCreateAndDelete
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";        
        private TreeNode selectNode = null;  //标记treeView控件中当前被选中的节点

        private IDataSourceFactory dsFactory = new DataSourceFactory();   
        private ICRSFactory coorFactory = new CRSFactory();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            

            {
                this.helpProvider1.SetShowHelp(this.treeView1, true);
                this.helpProvider1.SetHelpString(this.treeView1, "");
                this.helpProvider1.HelpNamespace = "CatalogTreeCreateAndDelete.html";
            }
        }

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createDataSourceToolStripButton_Click(object sender, EventArgs e)
        {
            CreateDataSourceForm dsForm = new CreateDataSourceForm(true);
            if (dsForm.ShowDialog() != DialogResult.OK)
                return;

            IConnectionInfo ci = new ConnectionInfo();
            switch (dsForm.ConnectionType)
            {
                case "gviConnectionMySql5x":
                    ci.ConnectionType = gviConnectionType.gviConnectionMySql5x;
                    break;
                case "gviConnectionFireBird2x":
                    ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                    break;
                case "gviConnectionSQLite3":
                    ci.ConnectionType = gviConnectionType.gviConnectionSQLite3;
                    break;
            }
            ci.Server = dsForm.Server;
            ci.Port = dsForm.Port;
            ci.Database = dsForm.Database;
            ci.UserName = dsForm.UserName;
            ci.Password = dsForm.Password;

            IDataSource ds = null;
            try
            {            
                if (!dsFactory.HasDataSource(ci))
                {
                    ds = dsFactory.CreateDataSource(ci, null);
                    if (ds != null && CreateLogicTreeTable(ds))
                    {
                        myTreeNode sourceNode = null;
                        if (ci.ConnectionType == gviConnectionType.gviConnectionMySql5x)
                            sourceNode = new myTreeNode(ci.Database + "@" + ci.Server, ci);
                        else
                            sourceNode = new myTreeNode(ci.Database, ci);
                        this.treeView1.Nodes.Add(sourceNode);
                        sourceNode.ContextMenuStrip = this.contextMenuStrip1;
                    }
                }
                else
                {
                    MessageBox.Show("数据源已存在");
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
            }
        }

        /// <summary>
        /// 添加数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDatasourceToolStripButton_Click(object sender, EventArgs e)
        {
            OpenDataSourceForm dsForm = new OpenDataSourceForm(false);
            if (dsForm.ShowDialog() != DialogResult.OK)
                return;

            IConnectionInfo ci = new ConnectionInfo();
            switch (dsForm.ConnectionType)
            {
                case "gviConnectionMySql5x":
                    ci.ConnectionType = gviConnectionType.gviConnectionMySql5x;
                    break;
                case "gviConnectionFireBird2x":
                    ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                    break;
                case "gviConnectionSQLite3":
                    ci.ConnectionType = gviConnectionType.gviConnectionSQLite3;
                    break;
            }
            ci.Server = dsForm.Server;
            ci.Port = dsForm.Port;
            ci.Database = dsForm.Database;
            ci.UserName = dsForm.UserName;
            ci.Password = dsForm.Password;

            IDataSource ds = null;
            IFeatureDataSet dataset = null;
            IFeatureClass fc = null;
            try
            {
                if (dsFactory == null)
                    dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);

                myTreeNode sourceNode = null;
                if (ci.ConnectionType == gviConnectionType.gviConnectionMySql5x)
                    sourceNode = new myTreeNode(ci.Database + "@" + ci.Server, ci);
                else
                    sourceNode = new myTreeNode(ci.Database, ci);
                sourceNode.ContextMenuStrip = this.contextMenuStrip1;
                this.treeView1.Nodes.Add(sourceNode);

                // 获取dataset
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                foreach (string setname in setnames)
                {
                    dataset = ds.OpenFeatureDataset(setname);

                    TreeNode setNode = new TreeNode(setname, 1, 1);
                    setNode.ContextMenuStrip = this.contextMenuStrip2;
                    sourceNode.Nodes.Add(setNode);

                    // 获取featureclass
                    string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                    if (fcnames == null || fcnames.Length == 0)
                        continue;
                    foreach (string fcname in fcnames)
                    {
                        fc = dataset.OpenFeatureClass(fcname);

                        TreeNode fcNode = new TreeNode(fcname, 2, 2);
                        fcNode.ContextMenuStrip = this.contextMenuStrip3;
                        setNode.Nodes.Add(fcNode);

                        // 获取属性字段
                        IFieldInfoCollection fieldinfos = fc.GetFields();
                        for (int i = 0; i < fieldinfos.Count; i++)
                        {
                            IFieldInfo fieldinfo = fieldinfos.Get(i);
                            if (null == fieldinfo)
                                continue;

                            TreeNode fieldinfoNode = new TreeNode(fieldinfo.Name);
                            fieldinfoNode.ContextMenuStrip = this.contextMenuStrip4;  // 绑定右键菜单
                            fcNode.Nodes.Add(fieldinfoNode);
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
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
                if (dataset != null)
                {
                    //Marshal.ReleaseComObject(dataset);
                    dataset = null;
                }
                if (fc != null)
                {
                    //Marshal.ReleaseComObject(fc);
                    fc = null;
                }
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            selectNode = this.treeView1.GetNodeAt(e.X, e.Y);
        }

        private void createDataSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSetForm setForm = new DataSetForm();
            if (setForm.ShowDialog() != DialogResult.OK)
                return;

            IDataSource ds = null;
            IFeatureDataSet dset = null;
            try
            {
                myTreeNode node = (myTreeNode)selectNode;
                IConnectionInfo ci = node.con;
                ds = dsFactory.OpenDataSource(ci);
                ICoordinateReferenceSystem coorSys = coorFactory.CreateFromWKT(setForm.CoordString);
                dset = ds.CreateFeatureDataset(setForm.DatasetName, coorSys as SpatialCRS);

                //开起事务:往普通表里插入记录
                ds.StartEditing();
                bool saveEditing = false;
                if (CreateLCRecordOfFDS(ds, dset.Name))
                    saveEditing = true;
                else
                    saveEditing = false;
                ds.StopEditing(saveEditing);
                // 如果事务失败，则回退创建dataset动作
                if (saveEditing == false)
                {
                    ds.DeleteFeatureDataset(dset.Name);
                    MessageBox.Show("创建失败");
                    return;
                }

                // 往树上挂节点
                TreeNode setNode = new TreeNode(setForm.DatasetName, 1, 1);
                node.Nodes.Add(setNode);
                setNode.ContextMenuStrip = this.contextMenuStrip2;
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
                if (dset != null)
                {
                    //Marshal.ReleaseComObject(dset);
                    dset = null;
                }
            }
        }

        #region 逻辑图层树相关的普通表
        /// <summary>
        /// 创建普通表
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public bool CreateLogicTreeTable(IDataSource ds)
        {
            IFieldInfoCollection fields = new FieldInfoCollection();
            IFieldInfo field = new FieldInfo();
            try
            {
                if (ds != null)
                {
                    field.Name = "id";
                    field.FieldType = gviFieldType.gviFieldFID;
                    field.Nullable = false;
                    fields.Add(field);

                    field = new FieldInfo();
                    field.Name = "groupid";
                    field.FieldType = gviFieldType.gviFieldString;
                    field.Length = 255;
                    field.Nullable = false;
                    fields.Add(field);

                    field = new FieldInfo();
                    field.Name = "name";
                    field.FieldType = gviFieldType.gviFieldString;
                    field.Length = 255;
                    field.Nullable = false;
                    fields.Add(field);

                    field = new FieldInfo();
                    field.Name = "founder";
                    field.FieldType = gviFieldType.gviFieldString;
                    field.Length = 255;
                    fields.Add(field);

                    field = new FieldInfo();
                    field.Name = "content";
                    field.FieldType = gviFieldType.gviFieldBlob;
                    field.Nullable = false;
                    fields.Add(field);
                    ds.CreateTable("cm_logictree", "id", fields);

                    fields.Clear();
                    field = new FieldInfo();
                    field.Name = "id";
                    field.FieldType = gviFieldType.gviFieldFID;
                    field.Nullable = false;
                    fields.Add(field);

                    field = new FieldInfo();
                    field.Name = "groupuid";
                    field.FieldType = gviFieldType.gviFieldString;
                    field.Length = 255;
                    field.Nullable = false;
                    fields.Add(field);

                    field = new FieldInfo();
                    field.Name = "DataSet";
                    field.FieldType = gviFieldType.gviFieldString;
                    field.Length = 255;
                    field.Nullable = false;
                    fields.Add(field);
                    ds.CreateTable("cm_group", "id", fields);
                    return true;
                }
                else
                    return false;
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
                return false;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                //Marshal.ReleaseComObject(field);
                field = null;
                //Marshal.ReleaseComObject(fields);
                fields = null;
            }
        }

        /// <summary>
        /// 创建默认逻辑树xml
        /// </summary>
        /// <param name="name"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public byte[] CreateLogicTree(String name, String guid)
        {
            //设置XmlWriterSettings对象
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.CloseOutput = true;

            MemoryStream ms = new MemoryStream();
            XmlWriter xw = XmlWriter.Create(ms, setting);
            //写xml文件头：包括[Name]、[guid]、[isChecked]属性
            xw.WriteStartDocument();
            xw.WriteStartElement("LogicTree");
            xw.WriteAttributeString("Name", name);
            xw.WriteAttributeString("Camera", "");
            if (guid == null)
                guid = System.Guid.NewGuid().ToString();
            xw.WriteAttributeString("GUID", guid);
            xw.WriteAttributeString("IsChecked", "Unchecked");
            //根节点
            xw.WriteStartElement("PGroups");
            xw.WriteAttributeString("ID", "0");
            xw.WriteAttributeString("Name", name);
            xw.WriteAttributeString("ParentID", "0");
            xw.WriteAttributeString("Recycle", "");
            xw.WriteAttributeString("Camera", "");
            xw.WriteAttributeString("CheckState", "Checked");
            //回收站
            xw.WriteStartElement("PGroups");
            xw.WriteAttributeString("ID", "1");
            xw.WriteAttributeString("Name", "回收站");
            xw.WriteAttributeString("ParentID", "0");
            xw.WriteAttributeString("Recycle", "ChuckNode");
            xw.WriteAttributeString("Camera", "");
            xw.WriteAttributeString("CheckState", "Checked");
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndDocument();
            xw.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// 返回table查询记录，返回0条为false，大于0条为true
        /// </summary>
        /// <param name="whereclause">查询条件字符串</param>
        /// <param name="tablename">查询的表名</param>
        /// <param name="fds">查询的数据集</param>
        /// <returns></returns>
        public bool GetQueryResults(String whereclause, String tablename, IDataSource ds)
        {
            ITable table = null;
            QueryFilter filter = null;
            IFdeCursor cursor = null;
            try
            {
                table = ds.OpenTable(tablename);
                filter = new QueryFilter();
                if (whereclause == null)
                    filter.WhereClause = "1=1";
                else
                    filter.WhereClause = whereclause;
                cursor = table.Search(filter, true);
                if (cursor.NextRow() != null)
                    return true;
                else
                    return false;
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
                throw;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (cursor != null)
                {
                    //Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (filter != null)
                {
                    //Marshal.ReleaseComObject(filter);
                    filter = null;
                }
                if (table != null)
                {
                    //Marshal.ReleaseComObject(table);
                    table = null;
                }
            }
        }

        private bool CreateLCRecordOfFDS(IDataSource ds, String fdsname)
        {
            ITable table = null;
            IFdeCursor cursor = null;
            try
            {
                String lcguid = System.Guid.NewGuid().ToString();
                byte[] lcbuf = CreateLogicTree("LogicTree", lcguid);
                
                if (!GetQueryResults(String.Format("groupid='{0}'", lcguid), "cm_logictree", ds))
                {
                    table = ds.OpenTable("cm_logictree");
                    cursor = table.Insert();
                    RowBufferFactory rbf = new RowBufferFactory();
                    IRowBuffer rb = rbf.CreateRowBuffer(table.GetFields());
                    rb.SetValue(1, lcguid);  //groupid
                    rb.SetValue(2, fdsname);  //name
                    rb.SetValue(3, fdsname);  //founder
                    IBinaryBuffer bb = new BinaryBuffer();
                    bb.FromByteArray(lcbuf);
                    rb.SetValue(4, bb);   //content
                    cursor.InsertRow(rb);
                }
                table = ds.OpenTable("cm_group");
                cursor = table.Insert();
                RowBufferFactory rbf1 = new RowBufferFactory();
                IRowBuffer rb1 = rbf1.CreateRowBuffer(table.GetFields());
                rb1.SetValue(1, lcguid);  //groupuid
                rb1.SetValue(2, fdsname);  //DataSet
                cursor.InsertRow(rb1);
                return true;
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
                return false;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (cursor != null)
                {
                    //Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (table != null)
                {
                    //Marshal.ReleaseComObject(table);
                    table = null;
                }
            }
        }

        public void DeleteLogicTree(IDataSource ds, String fdsName)
        {
            ITable t2 = null;
            QueryFilter filter2 = null;
            IFdeCursor cursor2 = null;
            ITable t1 = null;
            QueryFilter filter1 = null;
            IFdeCursor cursor1 = null;
            try
            {
                t2 = ds.OpenTable("cm_group");
                filter2 = new QueryFilter();
                filter2.WhereClause = String.Format("DataSet='{0}'", fdsName);
                cursor2 = t2.Update(filter2);
                cursor2.NextRow();
                cursor2.DeleteRow();
                if (GetQueryResults(String.Format("founder='{0}'", fdsName), "cm_logictree", ds))
                {
                    t1 = ds.OpenTable("cm_logictree");
                    filter1 = new QueryFilter();
                    filter1.WhereClause = String.Format("founder='{0}'", fdsName);
                    cursor1 = t1.Update(filter1);
                    cursor1.NextRow();
                    cursor1.DeleteRow();
                }
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
                throw comEx;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (filter2 != null)
                {
                    //Marshal.ReleaseComObject(filter2);
                    filter2 = null;
                }
                if (cursor2 != null)
                {
                    //Marshal.ReleaseComObject(cursor2);
                    cursor2 = null;
                }
                if (t2 != null)
                {
                    //Marshal.ReleaseComObject(t2);
                    t2 = null;
                }
                if (filter1 != null)
                {
                    //Marshal.ReleaseComObject(filter1);
                    filter1 = null;
                }
                if (cursor1 != null)
                {
                    //Marshal.ReleaseComObject(cursor1);
                    cursor1 = null;
                }
                if (t1 != null)
                {
                    //Marshal.ReleaseComObject(t1);
                    t1 = null;
                }
            }
        }
        #endregion

        private void deleteDataSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataSource ds = null;
            IFeatureDataSet dset = null;

            try
            {
                if (MessageBox.Show("该操作无法恢复，请确定是否删除数据集？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    myTreeNode node = (myTreeNode)selectNode.Parent;
                    IConnectionInfo ci = node.con;
                    ds = dsFactory.OpenDataSource(ci);
                    dset = ds.OpenFeatureDataset(selectNode.Text);

                    Array tem = dset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                    if (tem != null)
                    {
                        foreach (String strFC in tem)
                        {
                            IFeatureClass fc = dset.OpenFeatureClass(strFC);
                            dset.DeleteByName(strFC);
                            //Marshal.ReleaseComObject(fc);
                            fc = null;
                        }
                    }
                    ds.DeleteFeatureDataset(selectNode.Text);

                    // 从普通表里删除dataset对应的记录
                    DeleteLogicTree(ds, selectNode.Text);                  

                    // 从树上删节点
                    node.Nodes.Remove(selectNode);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
                if (dset != null)
                {
                    //Marshal.ReleaseComObject(dset);
                    dset = null;
                }
            }
        }

        private void createFeatureClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeatureClassForm fcForm = new FeatureClassForm();
            if (fcForm.ShowDialog() != DialogResult.OK)
                return;

            IDataSource ds = null;
            IFeatureDataSet dset = null;
            IFeatureClass fc = null;
            try
            {
                myTreeNode node = (myTreeNode)selectNode.Parent;
                IConnectionInfo ci = node.con;
                ds = dsFactory.OpenDataSource(ci);
                dset = ds.OpenFeatureDataset(selectNode.Text);

                // 定义字段
                IFieldInfoCollection fields = new FieldInfoCollection();
                IFieldInfo field = new FieldInfo();
                field.Name = "Name";
                field.FieldType = gviFieldType.gviFieldString;
                field.Length = 255;
                fields.Add(field);

                field = new FieldInfo();
                field.Name = "Groupid";
                field.FieldType = gviFieldType.gviFieldInt32;
                field.RegisteredRenderIndex = true;  //为支持逻辑图层树控制图层显示，需要注册渲染索引
                fields.Add(field);

                field = new FieldInfo();
                field.Name = "Geometry";
                field.FieldType = gviFieldType.gviFieldGeometry;
                field.GeometryDef = new GeometryDef();
                field.GeometryDef.GeometryColumnType = gviGeometryColumnType.gviGeometryColumnModelPoint;  //模型
                field.RegisteredRenderIndex = true;  //为支持空间列在RenderControl中显示出来，必须注册渲染索引
                fields.Add(field);

                fc = dset.CreateFeatureClass(fcForm.FeatureClassName, fields);

                //创建空间索引
                IGridIndexInfo index = new GridIndexInfo();
                //index.Name = fc.Guid;
                index.L1 = 500;
                index.L2 = 2000;
                index.L3 = 10000;
                index.GeoColumnName = "Geometry";
                fc.AddSpatialIndex(index as IIndexInfo);

                IRenderIndexInfo rInfo = new RenderIndexInfo();
                rInfo.GeoColumnName = "Geometry";
                rInfo.L1 = 500;
                fc.AddRenderIndex(rInfo);

                // 往树上挂节点
                TreeNode fcNode = new TreeNode(fcForm.FeatureClassName, 1, 1);
                fcNode.ContextMenuStrip = this.contextMenuStrip3;
                selectNode.Nodes.Add(fcNode);
                TreeNode fieldNode = new TreeNode("oid", 1, 1);
                fieldNode.ContextMenuStrip = this.contextMenuStrip4;
                fcNode.Nodes.Add(fieldNode);
                fieldNode = new TreeNode("Name", 1, 1);
                fieldNode.ContextMenuStrip = this.contextMenuStrip4;
                fcNode.Nodes.Add(fieldNode);
                fieldNode = new TreeNode("Groupid", 1, 1);
                fieldNode.ContextMenuStrip = this.contextMenuStrip4;
                fcNode.Nodes.Add(fieldNode);
                fieldNode = new TreeNode("Geometry", 1, 1);
                fieldNode.ContextMenuStrip = this.contextMenuStrip4;
                fcNode.Nodes.Add(fieldNode);
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
                if (dset != null)
                {
                    //Marshal.ReleaseComObject(dset);
                    dset = null;
                }
                if (fc != null)
                {
                    //Marshal.ReleaseComObject(fc);
                    fc = null;
                }
            }
        }

        private void deleteFeatureClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataSource ds = null;
            IFeatureDataSet dset = null;
            try
            {
                myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
                IConnectionInfo ci = node.con;
                ds = dsFactory.OpenDataSource(ci);
                dset = ds.OpenFeatureDataset(selectNode.Parent.Text);

                if (MessageBox.Show("该操作无法恢复，请确定是否删除要素类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String fcname = selectNode.Text;
                    if (dset.DeleteByName(fcname))
                        selectNode.Parent.Nodes.Remove(selectNode);
                    else
                        MessageBox.Show("删除失败");
                }
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
            }
            catch (System.Exception ex)
            {
                if (ex.Source == "物理数据源不存在")
                {
                    this.treeView1.Nodes.Remove(selectNode.Parent.Parent);
                    MessageBox.Show(ex.Source);
                }
                else
                    MessageBox.Show("删除失败");

                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (dset != null)
                {
                    //Marshal.ReleaseComObject(dset);
                    dset = null;
                }
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
            }
        }

        private void createFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureDataSet dset = null;
            IFeatureClass fc = null;
            try
            {
                ds = dsFactory.OpenDataSource(ci);
                dset = ds.OpenFeatureDataset(set_name);
                fc = dset.OpenFeatureClass(fc_name);

                OperateFieldInfoForm form = new OperateFieldInfoForm(null);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IFieldInfoCollection fieldinfos = fc.GetFields();
                    IFieldInfo newFieldInfo = form.newFieldInfo;

                    for (int i = 0; i < fieldinfos.Count; i++)
                    {
                        IFieldInfo fieldinfo = fieldinfos.Get(i);
                        if (null == fieldinfo)
                            continue;
                        if (newFieldInfo.Name == fieldinfo.Name)
                        {
                            MessageBox.Show("已有同名字段，添加失败");
                            return;
                        }
                    }

                    fc.LockType = gviLockType.gviLockExclusiveSchema;
                    fc.AddField(newFieldInfo);
                    fc.LockType = gviLockType.gviLockSharedSchema;

                    // 往树上挂节点
                    TreeNode fieldNode = new TreeNode(newFieldInfo.Name, 1, 1);
                    selectNode.Nodes.Add(fieldNode);
                    fieldNode.ContextMenuStrip = this.contextMenuStrip4;
                }               
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
                MessageBox.Show("添加失败");
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                MessageBox.Show("添加失败");
            }
            finally
            {
                fc.LockType = gviLockType.gviLockSharedSchema;
                if (fc != null)
                {
                    //Marshal.ReleaseComObject(fc);
                    fc = null;
                }
                if (dset != null)
                {
                    //Marshal.ReleaseComObject(dset);
                    dset = null;
                }
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
            }
        }

        private void deleteFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fieldInfo_name = selectNode.Text;
            string fc_name = selectNode.Parent.Text;
            string set_name = selectNode.Parent.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureDataSet dset = null;
            IFeatureClass fc = null;
            try
            {
                ds = dsFactory.OpenDataSource(ci);
                dset = ds.OpenFeatureDataset(set_name);
                fc = dset.OpenFeatureClass(fc_name);

                if (fieldInfo_name == "oid")
                {
                    MessageBox.Show("此字段不支持删除");
                    return;
                }

                if (MessageBox.Show("该操作无法恢复，请确定是否删除字段？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    fc.LockType = gviLockType.gviLockExclusiveSchema;
                    fc.DeleteField(fieldInfo_name);
                    fc.LockType = gviLockType.gviLockSharedSchema;

                    // 从树上删除节点
                    selectNode.Parent.Nodes.Remove(selectNode);
                    MessageBox.Show("删除成功");
                }
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
                MessageBox.Show("删除失败");
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                MessageBox.Show("删除失败");
            }
            finally
            {
                fc.LockType = gviLockType.gviLockSharedSchema;
                if (fc != null)
                {
                    //Marshal.ReleaseComObject(fc);
                    fc = null;
                }
                if (dset != null)
                {
                    //Marshal.ReleaseComObject(dset);
                    dset = null;
                }
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
            }
        }

        private void modifyFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fieldInfo_name = selectNode.Text;
            string fc_name = selectNode.Parent.Text;
            string set_name = selectNode.Parent.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureDataSet dset = null;
            IFeatureClass fc = null;
            IFieldInfo newFieldInfo = null;
            try
            {
                ds = dsFactory.OpenDataSource(ci);
                dset = ds.OpenFeatureDataset(set_name);
                fc = dset.OpenFeatureClass(fc_name);

                // 获取fieldinfo对象
                IFieldInfoCollection fieldinfos = fc.GetFields();
                for (int i = 0; i < fieldinfos.Count; i++)
                {
                    IFieldInfo fieldinfo = fieldinfos.Get(i);
                    if (null == fieldinfo)
                        continue;
                    if (fieldInfo_name == fieldinfo.Name)
                    {
                        newFieldInfo = fieldinfo;
                        break;
                    }
                }

                OperateFieldInfoForm form = new OperateFieldInfoForm(newFieldInfo);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    newFieldInfo = form.newFieldInfo;

                    fc.LockType = gviLockType.gviLockExclusiveSchema;
                    fc.ModifyField(newFieldInfo);
                    fc.LockType = gviLockType.gviLockSharedSchema;

                    // 修改树上的节点
                    selectNode.Text = newFieldInfo.Name;
                    MessageBox.Show("修改成功");
                }
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
                MessageBox.Show("修改失败");
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                MessageBox.Show("修改失败");
            }
            finally
            {
                fc.LockType = gviLockType.gviLockSharedSchema;
                if (newFieldInfo != null)
                {
                    //Marshal.ReleaseComObject(newFieldInfo);
                    newFieldInfo = null;
                }
                if (fc != null)
                {
                    //Marshal.ReleaseComObject(fc);
                    fc = null;
                }
                if (dset != null)
                {
                    //Marshal.ReleaseComObject(dset);
                    dset = null;
                }
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
            }
        }

        private void viewFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fieldinfo_name = selectNode.Text;
            string fc_name = selectNode.Parent.Text;
            string set_name = selectNode.Parent.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureDataSet dset = null;
            IFeatureClass fc = null;
            try
            {
                ds = dsFactory.OpenDataSource(ci);
                dset = ds.OpenFeatureDataset(set_name);
                fc = dset.OpenFeatureClass(fc_name);
                IFieldInfoCollection fieldinfos = fc.GetFields();
                for (int i = 0; i < fieldinfos.Count; i++)
                {
                    IFieldInfo fieldinfo = fieldinfos.Get(i);
                    if (null == fieldinfo)
                        continue;
                    if (fieldinfo_name == fieldinfo.Name)
                    {
                        ViewFieldInfoForm form = new ViewFieldInfoForm(fieldinfo);
                        form.Show();
                    }
                }
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Trace.WriteLine(comEx.Message);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (fc != null)
                {
                    //Marshal.ReleaseComObject(fc);
                    fc = null;
                }
                if (dset != null)
                {
                    //Marshal.ReleaseComObject(dset);
                    dset = null;
                }
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
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
