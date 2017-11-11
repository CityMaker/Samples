using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RenderArrow
{
    public partial class RenderArrowSettingForm : Form
    {
        private RenderArrow arrow = null;
        public RenderArrowSettingForm(object oo)
        {
            InitializeComponent();

            this.arrow = (RenderArrow)oo;
            this.arrow.NotifyPropertyChangeEvent += new RenderArrow.PropertyChanged(arrow_NotifyPropertyChange);
            this.propertyGrid1.SelectedObject = arrow;
        }

        void arrow_NotifyPropertyChange()
        {
            if (arrow != null)
            {
                arrow.Refresh();
            }
        }
    }
}
