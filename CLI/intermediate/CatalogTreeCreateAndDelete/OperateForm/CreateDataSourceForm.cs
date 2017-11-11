using System;
using System.Windows.Forms;

namespace CatalogTreeCreateAndDelete
{
    public partial class CreateDataSourceForm : Form
    {
        public string Server
        {
            get { return txtHost.Text; }
            set { txtHost.Text = value; }
        }
        public string ConnectionType
        {
            get { return cbConnectionType.Text; }
            set { cbConnectionType.Text = value; }
        }
        public uint Port
        {
            get { return txtPort.Text == "" ? 0 : uint.Parse(txtPort.Text); }
            set { txtPort.Text = value.ToString(); }
        }
        public string Database
        {
            get { return txtDatabase.Text; }
            set { txtDatabase.Text = value; }
        }
        public string UserName
        {
            get { return txtUserName.Text; }
            set { txtUserName.Text = value; }
        }
        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        private bool isCreate;
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        public CreateDataSourceForm(bool b)
        {
            InitializeComponent();
            isCreate = b;
        }


        private void DataSourceForm_Load(object sender, EventArgs e)
        {
            this.cbConnectionType.SelectedIndex = 0;
        }
        
        private void cbConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cbConnectionType.SelectedIndex)
            {
                case 0:   //gviConnectionMySql5x
                    {
                        this.txtHost.Enabled = true;
                        this.txtPort.Enabled = true;
                        this.txtUserName.Enabled = true;
                        this.txtPassword.Enabled = true;
                        this.txtDatabase.Enabled = true;
                        this.btnFileSelect.Enabled = false;
                    }
                    break;
                case 1:   //gviConnectionFireBird2x
                case 2:   //gviConnectionSQLite3
                    {
                        this.txtHost.Text = "";
                        this.txtPort.Text = "";
                        this.txtUserName.Text = "";
                        this.txtPassword.Text = "";

                        this.txtHost.Enabled = false;
                        this.txtPort.Enabled = false;
                        this.txtUserName.Enabled = false;
                        this.txtPassword.Enabled = false;
                        this.txtDatabase.Enabled = true;
                        this.btnFileSelect.Enabled = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// 为大文件选择存储路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            if(isCreate)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                if (this.cbConnectionType.SelectedIndex == 1) //gviConnectionFireBird2x
                {
                    dlg.Filter = "FDB File(*.fdb)|*.fdb";
                    dlg.DefaultExt = ".fdb";
                }
                else if (this.cbConnectionType.SelectedIndex == 2) //gviConnectionSQLite3
                {
                    dlg.Filter = "SDB File(*.sdb)|*.sdb";
                    dlg.DefaultExt = ".sdb";
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                    this.txtDatabase.Text = dlg.FileName;
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog();
                if(System.IO.Directory.Exists(strMediaPath))
                {
                    dlg.InitialDirectory = strMediaPath;
                }
                dlg.RestoreDirectory = true;
                if (this.cbConnectionType.SelectedIndex == 1) //gviConnectionFireBird2x
                {
                    dlg.Filter = "FDB File(*.fdb)|*.fdb";
                    dlg.DefaultExt = ".fdb";
                }
                else if (this.cbConnectionType.SelectedIndex == 2) //gviConnectionSQLite3
                {
                    dlg.Filter = "SDB File(*.sdb)|*.sdb";
                    dlg.DefaultExt = ".sdb";
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                    this.txtDatabase.Text = dlg.FileName;
            }
        }
    }

}
