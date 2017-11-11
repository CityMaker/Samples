using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Common;
using System.Xml;


namespace FeatureEdit
{
    public partial class MainForm : Form
    {
        private IDataSourceFactory dsFactory = null;

        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";        
        private TreeNode selectNode = null;  //标记treeView控件中当前被选中的节点

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            

            // 加载datasource
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\point.FDB");
                ci.Database = tmpFDBPath;

                bindDataToCatalogTree(ci);
            }
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\community.FDB");
                ci.Database = tmpFDBPath;

                bindDataToCatalogTree(ci);
            }  

            {
                this.helpProvider1.SetShowHelp(this.treeView1, true);
                this.helpProvider1.SetHelpString(this.treeView1, "");
                this.helpProvider1.HelpNamespace = "FeatureEdit.html";
            }    
        }

        // 公共方法
        private void bindDataToCatalogTree(IConnectionInfo ci)
        {
            try
            {
                if (dsFactory == null)
                    dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);

                myTreeNode sourceNode = null;
                if (ci.ConnectionType == gviConnectionType.gviConnectionMySql5x)
                    sourceNode = new myTreeNode(ci.Database + "@" + ci.Server, ci);
                else
                    sourceNode = new myTreeNode(ci.Database, ci);
                this.treeView1.Nodes.Add(sourceNode);

                // 获取dataset
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                foreach (string setname in setnames)
                {
                    IFeatureDataSet dataset = ds.OpenFeatureDataset(setname);

                    TreeNode setNode = new TreeNode(setname, 1, 1);
                    sourceNode.Nodes.Add(setNode);

                    // 获取featureclass
                    string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                    if (fcnames == null || fcnames.Length == 0)
                        continue;
                    foreach (string fcname in fcnames)
                    {
                        IFeatureClass fc = dataset.OpenFeatureClass(fcname);

                        TreeNode fcNode = new TreeNode(fcname, 2, 2);
                        fcNode.ContextMenuStrip = this.contextMenuStrip2;
                        setNode.Nodes.Add(fcNode);

                        // 获取属性字段
                        IFieldInfoCollection fieldinfos = fc.GetFields();
                        for (int i = 0; i < fieldinfos.Count; i++)
                        {
                            IFieldInfo fieldinfo = fieldinfos.Get(i);
                            if (null == fieldinfo)
                                continue;

                            TreeNode fieldinfoNode = new TreeNode(fieldinfo.Name);
                            fieldinfoNode.ContextMenuStrip = this.contextMenuStrip1;  // 绑定右键菜单
                            fcNode.Nodes.Add(fieldinfoNode);
                        }
                    }
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
        }

        private void toolStripAddDatasource_Click(object sender, EventArgs e)
        {
            DataSourceForm dsForm = new DataSourceForm(false);
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

            bindDataToCatalogTree(ci);
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            selectNode = this.treeView1.GetNodeAt(e.X, e.Y);
        }

        private void toolStripMenuItemFieldInfo_Click(object sender, EventArgs e)
        {
            string fieldinfo_name = selectNode.Text;
            string fc_name = selectNode.Parent.Text;
            string set_name = selectNode.Parent.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureDataSet dataset = null;
            IFeatureClass fc = null;
            IFieldInfoCollection fieldinfos = null;
            try
            {
                ds = dsFactory.OpenDataSource(ci);
                dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);
                fieldinfos = fc.GetFields();
                for (int i = 0; i < fieldinfos.Count; i++)
                {
                    IFieldInfo fieldinfo = fieldinfos.Get(i);
                    if (null == fieldinfo)
                        continue;
                    if (fieldinfo_name == fieldinfo.Name)
                    {
                        FieldInfoForm form = new FieldInfoForm(fieldinfo);
                        form.Show();
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
                if (fieldinfos != null)
                {
                    //Marshal.ReleaseComObject(fieldinfos);
                    fieldinfos = null;
                }
            }
        }

        private void toolStripMenuItemViewData_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureDataSet dataset = null;
            IFeatureClass fc = null;            
            try
            {
                ds = dsFactory.OpenDataSource(ci);
                dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                IQueryFilter filter = new QueryFilter();
                int nCount = fc.GetCount(filter);
                if(nCount == 0)
                    return;

                // 初始化表格
                DataTable dt = CreateDataTable(fc);
                // 查找数据
                GetResultSet(fc, filter, dt);
                // 显示表格
                FCConnectionInfo info = new FCConnectionInfo(ci, set_name, fc_name);
                new AttributeFrm(dt, info, filter.WhereClause).Show();
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

        private void toolStripMenuItemQuery_Click(object sender, EventArgs e)
        {
            string fc_name = selectNode.Text;
            string set_name = selectNode.Parent.Text;
            myTreeNode node = (myTreeNode)selectNode.Parent.Parent;
            IConnectionInfo ci = node.con;

            IDataSource ds = null;
            IFeatureDataSet dataset = null;
            IFeatureClass fc = null;
            try
            {
                ds = dsFactory.OpenDataSource(ci);
                dataset = ds.OpenFeatureDataset(set_name);
                fc = dataset.OpenFeatureClass(fc_name);

                QueryFilterDlg pQueryFilterDlg = new QueryFilterDlg();
                for (int index = 0; index < fc.GetFields().Count; index++)
                {
                    pQueryFilterDlg.FieldList_listBox.Items.Add(fc.GetFields().Get(index).Name);
                }
                if (DialogResult.OK == pQueryFilterDlg.ShowDialog())
                {
                    IQueryFilter filter = new QueryFilter();
                    filter.WhereClause = pQueryFilterDlg.QueryFilter_txt.Text;
                    int nCount = fc.GetCount(filter);
                    if (nCount == 0)
                        return;

                    // 初始化表格
                    DataTable dt = CreateDataTable(fc);
                    // 查找数据
                    GetResultSet(fc, filter, dt);
                    // 显示表格
                    FCConnectionInfo info = new FCConnectionInfo(ci, set_name, fc_name);
                    new AttributeFrm(dt, info, filter.WhereClause).Show();
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
                        fInfo.FieldType == gviFieldType.gviFieldBlob ||
                        fInfo.Name.ToUpper() == "GROUPID")
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
                //groupid
                dc = new DataColumn("GroupId");
                dc.DataType = typeof(int);
                dt.Columns.Add(dc);
                //layerName
                dc = new DataColumn("GroupName");
                dc.DataType = typeof(string);
                dt.Columns.Add(dc);

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
                    // 通过解析逻辑树获取GroupId对应的GroupName
                    GroupId2LayerName(dt, fc.FeatureDataSet);
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

        // 解析GroupId对应的GroupName
        public void GroupId2LayerName(DataTable dt, IFeatureDataSet dataset)
        {
            List<LogicLayerNodeInfo> infoList = GetLogicLayerInfos(dataset);
            foreach (DataRow dr in dt.Rows)
            {
                string strGrpId = dr["GroupId"].ToString();
                int grpId;
                if (int.TryParse(strGrpId, out grpId))
                {
                    string layerName = FindGroupName(grpId, infoList);
                    dr["GroupName"] = layerName;
                }
            }
        }

        private string FindGroupName(int id, List<LogicLayerNodeInfo> infoList)
        {
            if (infoList == null)
                return string.Empty;
            foreach (LogicLayerNodeInfo logicLayer in infoList)
            {
                if (logicLayer.layerId == id)
                {
                    return logicLayer.layerName;
                }
            }

            return string.Empty;
        }

        #region 读数据库获取逻辑树xml内容
        private byte[] GetLogicTreeContent(IFeatureDataSet dataset)
        {
            byte[] strContent = null;

            try
            {
                IQueryDef qd = dataset.DataSource.CreateQueryDef();
                qd.AddSubField("content");

                qd.Tables = new String[] { "cm_logictree", "cm_group" };
                qd.WhereClause = String.Format("cm_group.groupuid = cm_logictree.groupid "
                                + " and cm_group.DataSet = '{0}'", dataset.Name);

                IFdeCursor cursor = qd.Execute(false);
                IRowBuffer row = null;
                if ((row = cursor.NextRow()) != null)
                {
                    //content
                    int nPose = row.FieldIndex("content");
                    if (nPose != -1)
                    {
                        IBinaryBuffer bb = row.GetValue(nPose) as IBinaryBuffer;
                        strContent = (byte[])bb.AsByteArray();
                    }
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return null;
            }

            return strContent;
        }
        #endregion
        
        #region 解析逻辑树xml获取GroupId及其对应的GroupName
        private List<LogicLayerNodeInfo> GetLogicLayerInfos(IFeatureDataSet dataset)
        {
            byte[] bb = GetLogicTreeContent(dataset);
            if (bb == null)
                return null;
            List<LogicLayerNodeInfo> layerList = new List<LogicLayerNodeInfo>();
            using (MemoryStream ms = new MemoryStream(bb))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ms);
                XmlNode rootNode = doc.DocumentElement;
                if (rootNode != null && rootNode.HasChildNodes)
                {
                    TravelXML(rootNode.ChildNodes[0], layerList);
                }
            }
            return layerList;
        }

        private void TravelXML(XmlNode pNode, List<LogicLayerNodeInfo> layerList)
        {
            if (pNode == null || layerList == null || pNode.Attributes == null)
                return;

            string layerName = pNode.Attributes["Name"].Value;
            int grpId = int.Parse(pNode.Attributes["ID"].Value);
            LogicLayerNodeInfo info = new LogicLayerNodeInfo(grpId, layerName);
            layerList.Add(info);

            if (pNode.HasChildNodes)
            {
                foreach (XmlNode node in pNode.ChildNodes)
                {
                    TravelXML(node, layerList);
                }
            }
        }
        #endregion
       
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

    class LogicLayerNodeInfo
    {
        public int layerId;
        public string layerName;

        public LogicLayerNodeInfo(int layerId, string layerName)
        {
            this.layerId = layerId;
            this.layerName = layerName;
        }
    }

    public class FCConnectionInfo
    {
        public IConnectionInfo ci;
        public string datasetName;
        public string featureclassName;

        public FCConnectionInfo(IConnectionInfo ci, string datasetName, string featureclassName)
        {
            this.ci = ci;
            this.datasetName = datasetName;
            this.featureclassName = featureclassName;
        }
    }
}
