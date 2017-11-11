using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OverlayLabel
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private int offset;
        public int Offset
        {
            get { return this.offset; }
        }
        private float windowWidthRatio;
        public float WindowWidthRatio
        {
            get { return this.windowWidthRatio; }
        }
        private float windowHightRatio;
        public float WindowHightRatio
        {
            get { return this.windowHightRatio; }
        }

        private void numOffset_ValueChanged(object sender, EventArgs e)
        {
            offset = int.Parse(this.numOffset.Value.ToString());
        }

        private void numWindowWidthRatio_ValueChanged(object sender, EventArgs e)
        {
            windowWidthRatio = float.Parse(this.numWindowWidthRatio.Value.ToString());
        }

        private void numWindowHeightRatio_ValueChanged(object sender, EventArgs e)
        {
            windowHightRatio = float.Parse(this.numWindowHeightRatio.Value.ToString());
        }
    }
}
