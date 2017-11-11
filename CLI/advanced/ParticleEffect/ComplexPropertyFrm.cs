// Copyright 2015 CityMaker SDK
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
//author	yuanying
//date	2015/09/17

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
    public partial class ComplexPropertyFrm : Form
    {

        private ComplexParticleEffect peffect = null;
        public ComplexPropertyFrm(object oo)
        {
            InitializeComponent();
            this.peffect = (ComplexParticleEffect)oo;
            this.peffect.NotifyPropertyChangeEvent += new ComplexParticleEffect.PropertyChanged(peffect_NotifyPropertyChange);
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
