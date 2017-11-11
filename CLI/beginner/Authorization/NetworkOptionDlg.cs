using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gvitech.CityMaker.Common;

namespace Authorization
{
    public partial class NetworkOptionDlg : Form
    {
        public NetworkOptionDlg()
        {
            InitializeComponent();
            this.te_KeyserviceIP.Text = NetAuthorization.m_sNetHost;
            this.te_Port.Text = NetAuthorization.m_sNetPort;
            this.te_Pwd.Text = NetAuthorization.m_sNetPwd;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            NetAuthorization.UpdateServerInfo(te_KeyserviceIP.Text.Trim(), te_Port.Text.Trim(), te_Pwd.Text.Trim());
            this.DialogResult = DialogResult.OK;
        }
    }
}
