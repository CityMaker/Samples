using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToolTipShow
{
    public partial class SelectFieldForm : Form
    {
        public SelectFieldForm()
        {
            InitializeComponent();
        }

        public SelectFieldForm(object[] text)
        {
            InitializeComponent();
            this.comboBox1.DataSource = text;
        }
    }
}
