using System;
using System.Windows.Forms;

namespace CatalogTreeCreateAndDelete
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

        public EditForm(Double min, Double max, Double value)
        {
            InitializeComponent();
            this.numericUpDown1.Visible = true;

            if (min == double.MinValue)
                this.numericUpDown1.Minimum = decimal.MinValue;
            else
                this.numericUpDown1.Minimum = (decimal)min;
            if (max == double.MaxValue)
                this.numericUpDown1.Maximum = decimal.MaxValue;
            else
                this.numericUpDown1.Maximum = (decimal)max;

            this.numericUpDown1.Value = (decimal)value;
        }

        public EditForm(int min, int max, int value)
        {
            InitializeComponent();
            this.numericUpDown1.Visible = true;

            if (min == int.MinValue)
                this.numericUpDown1.Minimum = decimal.MinValue;
            else
                this.numericUpDown1.Minimum = min;
            if (max == int.MaxValue)
                this.numericUpDown1.Maximum = decimal.MaxValue;
            else
                this.numericUpDown1.Maximum = max;

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
