using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerrainVideo
{
    public partial class PropertyFrm : Form
    {

        private VideoObject video = null;
        public PropertyFrm(object oo)
        {
            InitializeComponent();
            this.video = (VideoObject)oo;
            this.video.NotifyPropertyChangeEvent += new VideoObject.PropertyChanged(video_NotifyPropertyChange);
            this.propertyGrid1.SelectedObject = video;
        }

        public void SetSource(VideoObject o)
        {
            this.propertyGrid1.SelectedObject = o;
        }

        public bool hasClosed()
        {
            if (this.propertyGrid1.SelectedObject == null)
                return true;
            else
                return false;
        }

        void video_NotifyPropertyChange(string changeType)
        {
            if (video != null)
            {
                video.Update();
            }
        }
    }
}
