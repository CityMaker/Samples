using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataImport
{
    public partial class DataImportProgressDlg : Form
    {
        public bool CallbackCancel = false;

        public DataImportProgressDlg()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            CallbackCancel = true;
            this.btn_Cancel.Enabled = false;
        }
    }
}
