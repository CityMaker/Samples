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
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeGeometry;

namespace ParticleEffect
{
    public class ComplexParticleEffect
    {        
        public delegate void PropertyChanged(string changeType);
        public event PropertyChanged NotifyPropertyChangeEvent;
        IComplexParticleEffect complexParticleEffect = null;

        public System.Guid _Guid = new System.Guid();

        [Browsable(false)]
        public IComplexParticleEffect ComplexParticleEffectObject
        {
            get
            {
                return this.complexParticleEffect;
            }
            set
            {
                this.complexParticleEffect = value;
            }
        }

        #region 粒子参数
        private double emissionRate;
        [Category("发射器参数"), Description("发射速度")]
        public double EmissionRate
        {
            get { return emissionRate; }
            set
            {
                if (value != emissionRate)
                {
                    emissionRate = value;
                    NotifyPropertyChange("EmissionRate");
                }
            }
        }

        private double scalingFactor;
        [Category("其他参数"), Description("粒子的缩放比例")]
        public double ScalingFactor
        {
            get { return scalingFactor; }
            set
            {
                if (value != scalingFactor)
                {
                    scalingFactor = value;
                    NotifyPropertyChange("ScalingFactor");
                }
            }
        }

        private double windAcceleration;
        [Category("其他参数"), Description("风力大小")]
        public double WindAcceleration
        {
            get { return windAcceleration; }
            set
            {
                if (value != windAcceleration)
                {
                    windAcceleration = value;
                    NotifyPropertyChange("WindAcceleration");
                }
            }
        }

        private double windDirection;
        [Category("其他参数"), Description("风的角度")]
        public double WindDirection
        {
            get { return windDirection; }
            set
            {
                if (value != windDirection)
                {
                    windDirection = value;
                    NotifyPropertyChange("WindDirection");
                }
            }
        }

        private string rotateAngleStr;
        [Category("其他参数"), Description("粒子的发射角度")]
        public string RotateAngle
        {
            get
            {
                return rotateAngleStr.ToString();
            }
            set
            {
                if (value != rotateAngleStr)
                {
                    rotateAngleStr = value;
                    NotifyPropertyChange("RotateAngle");
                }
            }
        }
        #endregion

        protected IEulerAngle v3 = new EulerAngle();
        public ComplexParticleEffect()
        {
            EmissionRate = 1;
            ScalingFactor = 5;
            WindAcceleration = 0;
            WindDirection = 0;           
            v3.Set(0, 0, 0);
            RotateAngle = v3.Heading.ToString() + "," + v3.Tilt.ToString() + "," + v3.Roll.ToString();
        }

        private void NotifyPropertyChange(string type)
        {
            if (NotifyPropertyChangeEvent != null)
            {
                NotifyPropertyChangeEvent(type);
            }
        }

        public void Create(gviComplexParticleEffectType type)
        {
            try
            {
                if (this.complexParticleEffect == null)
                {
                    this.complexParticleEffect = CommonUnity.RenderHelper.ObjectManager.CreateComplexParticleEffect(type, _Guid);
                }

            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
        }

        public void Play()
        {
            complexParticleEffect.Play();
        }

        public void Stop()
        {
            complexParticleEffect.Stop();
        }

        public void Refresh()
        {
            try
            {
                complexParticleEffect.EmissionRate = EmissionRate;
                complexParticleEffect.ScalingFactor = ScalingFactor;
                complexParticleEffect.WindAcceleration = WindAcceleration;
                complexParticleEffect.WindDirection = WindDirection;                              
                complexParticleEffect.MaxVisibleDistance = 1000;

                if (rotateAngleStr != null)
                {
                    string[] arr = rotateAngleStr.Split(',');
                    if (arr.Length == 3)
                    {
                        v3.Set(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
                        complexParticleEffect.RotateAngle = v3;
                    }
                }


            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 设置粒子特效的位置 
        /// </summary>
        /// <param name="v3"></param>
        public void SetPosition(IPoint v3)
        {
            complexParticleEffect.Position = v3;
        }
    }
}
