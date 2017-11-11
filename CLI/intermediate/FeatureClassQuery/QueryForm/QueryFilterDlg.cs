using System;
using System.Windows.Forms;

namespace FeatureClassQuery
{
    public partial class QueryFilterDlg : Form
    {
        public QueryFilterDlg()
        {
            InitializeComponent();
        }

        private void FieldList_listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FieldList_listBox.SelectedItem!=null)
            {
                QueryFilter_txt.SelectedText = FieldList_listBox.SelectedItem.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button1.Text + " ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button2.Text + " ";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button3.Text + " ";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button6.Text + " ";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button5.Text + " ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button4.Text + " ";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button7.Text + " ";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button8.Text + " ";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button9.Text + " ";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button12.Text + " ";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button11.Text + " ";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            QueryFilter_txt.SelectedText = " " + button10.Text + " ";
        }
    }
}
