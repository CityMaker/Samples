using System;
using System.Windows.Forms;

namespace FeatureLayerSimpleRender
{
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(bool b, string text)
        {
            InitializeComponent();
            this.checkBox1.Visible = true;
            this.checkBox1.Checked = b;
            this.checkBox1.Text = text;
        }

        public EditForm(Double min, Double value)
        {
            InitializeComponent();
            this.numericUpDown1.Visible = true;
            this.numericUpDown1.Minimum = (decimal)min;
            this.numericUpDown1.Maximum = decimal.MaxValue;
            this.numericUpDown1.Value = (decimal)value;
        }

        public EditForm(float min, float value)
        {
            InitializeComponent();
            this.numericUpDown1.Visible = true;
            this.numericUpDown1.Minimum = (decimal)min;
            this.numericUpDown1.Maximum = decimal.MaxValue;
            this.numericUpDown1.Value = (decimal)value;
        }

        public EditForm(int min, int value)
        {
            InitializeComponent();
            this.numericUpDown1.Visible = true;
            this.numericUpDown1.Minimum = (decimal)min;
            this.numericUpDown1.Maximum = decimal.MaxValue;
            this.numericUpDown1.Value = (decimal)value;
        }

        public EditForm(object[] items, int selectIndex)
        {
            InitializeComponent();
            this.comboBox1.Visible = true;
            this.comboBox1.Items.AddRange(items);
            this.comboBox1.SelectedIndex = selectIndex;
        }
    }
}
