using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.FdeCore;


namespace CatalogTree
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


            

            // 获取datasource
            IConnectionInfo ci = new ConnectionInfo();
            ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
            string tmpFDBPath = (strMediaPath + @"\SDKDEMO.FDB");
            ci.Database = tmpFDBPath;

            bindDataToCatalogTree(ci);

            {
                this.helpProvider1.SetShowHelp(this.treeView1, true);
                this.helpProvider1.SetHelpString(this.treeView1, "");
                this.helpProvider1.HelpNamespace = "CatalogTree.html";
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

            IDataSource ds = dsFactory.OpenDataSource(ci);
            IFeatureDataSet dataset = ds.OpenFeatureDataset(set_name);
            IFeatureClass fc = dataset.OpenFeatureClass(fc_name);
            IFieldInfoCollection fieldinfos = fc.GetFields();
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
