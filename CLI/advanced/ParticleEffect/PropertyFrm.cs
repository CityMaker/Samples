// Copyright 2012 CityMaker SDK
// 
// All rights reserved under the copyright laws of the China
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See Sample at <your CityMaker install location>/CityMaker SDK/Samples.
// 
//author	gs
//date	2011/09/26
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParticleEffect
{
    public partial class PropertyFrm : Form
    {

        private ParticleEffect peffect = null;
        public PropertyFrm(object oo)
        {
            InitializeComponent();
            this.peffect = (ParticleEffect)oo;
            this.peffect.NotifyPropertyChangeEvent += new ParticleEffect.PropertyChanged(peffect_NotifyPropertyChange);
            this.propertyGrid1.SelectedObject = peffect;
        }

        void peffect_NotifyPropertyChange(string changeType)
        {
            if (peffect != null)
            {
                peffect.Refresh();
            }
        }
    }
}
