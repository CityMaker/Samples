using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerrainHole
{
    public partial class TerrainHoleSettingForm : Form
    {
        public TerrainHoleSettingForm()
        {
            InitializeComponent();
        }

        public TerrainHoleSettingForm(int orderValue, int modeIndex)
        {
            InitializeComponent();

            this.numericUpDownDrawOrder.Value = orderValue;
        }

        private string strtype = "";
        public string Strtype
        {
            get { return this.strtype; }
        }

        private int order = 0;
        public int Order
        {
            get { return this.order; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            order = int.Parse(this.numericUpDownDrawOrder.Value.ToString());
        }
    }
}
