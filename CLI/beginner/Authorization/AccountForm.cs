using System;
using System.Windows.Forms;

namespace Authorization
{
    public partial class AccountForm : Form
    {
        public AccountForm()
        {
            InitializeComponent();
        }

        private void btnNoOption_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkOptionDlg netDlg = new NetworkOptionDlg();
            this.DialogResult = netDlg.ShowDialog();
        }
    }
}
