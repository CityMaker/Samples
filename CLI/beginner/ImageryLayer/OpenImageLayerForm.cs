using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageryLayer
{
    public partial class OpenImageLayerForm : Form
    {
        public OpenImageLayerForm()
        {
            InitializeComponent();

            this.comboBox1.SelectedIndex = 0;
        }

        private string strtype = "";
        public string Strtype
        {
            get { return this.strtype; }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            strtype = this.comboBox1.Text;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            strtype = this.comboBox1.Text;
        }
    }
}
