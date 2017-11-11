using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerrainModifier
{
    public partial class TerrainModifierSettingForm : Form
    {
        public TerrainModifierSettingForm()
        {
            InitializeComponent();

            this.comboBoxElevationBehaviorMode.SelectedIndex = 0;
        }

        public TerrainModifierSettingForm(int orderValue, int modeIndex)
        {
            InitializeComponent();

            this.comboBoxElevationBehaviorMode.SelectedIndex = modeIndex;
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
            strtype = this.comboBoxElevationBehaviorMode.Text;
            order = int.Parse(this.numericUpDownDrawOrder.Value.ToString());
        }
    }
}
