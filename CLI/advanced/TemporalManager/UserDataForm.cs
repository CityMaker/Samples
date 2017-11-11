using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TemporalManager
{
    public partial class UserDataForm : Form
    {
        public double hotvalue = 100;

        public UserDataForm()
        {
            InitializeComponent();
        }

        private void txtHotValue_Leave(object sender, EventArgs e)
        {
            try
            {
                hotvalue = double.Parse(this.txtHotValue.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请输入正确的值");
            }
        }

        public void SetHotValue(double value)
        {
            this.txtHotValue.Text = value.ToString();
        }
    }
}
