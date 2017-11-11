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
using System.Linq;
using System.Text;
using System.ComponentModel;
using Gvitech.CityMaker.RenderControl;
using System.Runtime.InteropServices;
using System.Drawing;
using Gvitech.CityMaker.Math;
using System.IO;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeGeometry;

namespace ParticleEffect
{
    public class ParticleEffect
    {
        /// <summary>
        /// 发射器类型
        /// </summary>
        public enum EmitterTypeCategory
        {
            /// <summary>
            /// 点
            /// </summary>
            Point = 0,

            /// <summary>
            /// 圆形
            /// </summary>
            Circle = 1,

            /// <summary>
            /// 矩形
            /// </summary>
            Box = 2
        }

        public delegate void PropertyChanged(string changeType);
        public event PropertyChanged NotifyPropertyChangeEvent;
        IParticleEffect particleEffect = null;

        public System.Guid _Guid = new System.Guid();
        public string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        [Browsable(false)]
        public IParticleEffect ParticleEffectObject
        {
            get
            {
                return this.particleEffect;
            }
            set
            {
                this.particleEffect = value;
            }
        }

        #region 粒子参数
        private EmitterTypeCategory emitterType;
        [Category("发射器参数"), Description("发射器类型")]
        public EmitterTypeCategory EmitterType
        {
            get { return emitterType; }
            set
            {
                if (value != emitterType)
                {
                    emitterType = value;
                    NotifyPropertyChange("EmitterType");
                }
            }
        }

        private double emitterWidth;
        [Category("发射器参数"), Description("矩形发射器宽")]
        public double EmitterWidth
        {
            get { return emitterWidth; }
            set
            {
                if (value != emitterWidth)
                {
                    emitterWidth = value;
                    NotifyPropertyChange("EmitterWidth");
                }
            }
        }

        private double emitterHeight;
        [Category("发射器参数"), Description("矩形发射器高")]
        public double EmitterHeight
        {
            get { return emitterHeight; }
            set
            {
                if (value != emitterHeight)
                {
                    emitterHeight = value;
                    NotifyPropertyChange("EmitterHeight");
                }
            }
        }

        private double emitterDepth;
        [Category("发射器参数"), Description("矩形发射器长")]
        public double EmitterDepth
        {
            get { return emitterDepth; }
            set
            {
                if (value != emitterDepth)
                {
                    emitterDepth = value;
                    NotifyPropertyChange("EmitterLength");
                }
            }
        }

        private double emitterRadius;
        [Category("发射器参数"), Description("圆形发射器半径")]
        public double EmitterRadius
        {
            get { return emitterRadius; }
            set
            {
                if (value != emitterRadius)
                {
                    emitterRadius = value;
                    NotifyPropertyChange("EmitterLength");
                }
            }
        }

        private double emissionMaxRate;
        [Category("粒子系统参数"), Description("最大发射速率.")]
        public double EmissionMaxRate
        {
            get { return emissionMaxRate; }
            set
            {
                if (value != emissionMaxRate)
                {
                    emissionMaxRate = value;
                    NotifyPropertyChange("EmissionMaxRate");
                }
            }
        }

        private double emissionMinRate;
        [Category("粒子系统参数"), Description("最小发射速率.")]
        public double EmissionMinRate
        {
            get { return emissionMinRate; }
            set
            {
                if (value != emissionMinRate)
                {
                    emissionMinRate = value;
                    NotifyPropertyChange("EmissionMinRate");
                }
            }
        }

        private double emissionMaxMoveSpeed;
        [Category("粒子系统参数"), Description("最大移动速率."), DefaultValue(8)]
        public double EmissionMaxMoveSpeed
        {
            get
            {
                return emissionMaxMoveSpeed;
            }
            set
            {
                if (value != emissionMaxMoveSpeed)
                {
                    emissionMaxMoveSpeed = value;
                    NotifyPropertyChange("EmissionMaxMoveSpeed");
                }
            }
        }

        private byte visibleMask;
        [Category("粒子系统参数"), Description("可见性.")]
        public byte VisibleMask
        {
            get
            {
                return visibleMask;
            }
            set
            {
                if (value != visibleMask)
                {
                    visibleMask = value;
                    NotifyPropertyChange("VisibleMask");
                }
            }
        }

        private string textureFileName;
        [Category("粒子系统参数"), Description("贴图名称.")]
        public string ImageName
        {
            get
            {
                return textureFileName;
            }
            set
            {
                if (value != textureFileName)
                {
                    textureFileName = value;
                    NotifyPropertyChange("textureFileName");
                }


            }
        }
        private double particleMaxLifeTime;
        [Category("粒子系统参数"), Description("粒子最大生命周期.")]
        public double ParticleMaxLifeTime
        {
            get
            {
                return particleMaxLifeTime;
            }
            set
            {
                if (value != particleMaxLifeTime)
                {
                    particleMaxLifeTime = value;
                    NotifyPropertyChange("ParticleMaxLifeTime");
                }
            }
        }

        private double particleMinLifeTime;
        [Category("粒子系统参数"), Description("粒子最小生命周期.")]
        public double ParticleMinLifeTime
        {
            get
            {
                return particleMinLifeTime;
            }
            set
            {
                if (value != particleMinLifeTime)
                {
                    particleMinLifeTime = value;
                    NotifyPropertyChange("ParticleMinLifeTime");
                }
            }
        }

        private double windDirection;
        [Category("粒子系统参数"), Description("风向.")]
        public double WindDirection
        {
            get
            {
                return windDirection;
            }
            set
            {
                if (value != windDirection)
                {
                    windDirection = value;
                    NotifyPropertyChange("WindDirection");
                }
            }
        }

        private double windAcceleration;
        [Category("粒子系统参数"), Description("风阻力.")]
        public double WindAcceleration
        {
            get
            {
                return windAcceleration;
            }
            set
            {
                if (value != windAcceleration)
                {
                    windAcceleration = value;
                    NotifyPropertyChange("WindAcceleration");
                }
            }
        }

        private gviParticleBillboardType particleBillboardType;
        [Category("粒子系统参数"), Description("Billboard类型.")]
        public gviParticleBillboardType ParticleBillboardType
        {
            get
            {
                return particleBillboardType;
            }
            set
            {
                if (value != particleBillboardType)
                {
                    particleBillboardType = value;
                    NotifyPropertyChange("ParticleBillboardType");
                }
            }
        }

        private Color particleBirthColor = ColorHelper.UintToColor(4294954035);
        [Category("粒子系统参数"), Description("粒子诞生颜色.")]
        public Color ParticleBirthColor
        {
            get
            {
                return particleBirthColor;
            }
            set
            {
                if (value != particleBirthColor)
                {
                    particleBirthColor = value;
                    NotifyPropertyChange("ParticleBirthColor");
                }
            }
        }

        private Color particleDeathColor = ColorHelper.UintToColor(16731187);
        [Category("粒子系统参数"), Description("粒子死亡颜色.")]
        public Color ParticleDeathColor
        {
            get
            {
                return particleDeathColor;
            }
            set
            {
                if (value != particleDeathColor)
                {
                    particleDeathColor = value;
                    NotifyPropertyChange("ParticleDeathColor");
                }
            }
        }

        private double emissionMinScaleSpeed;
        [Category("粒子系统参数"), Description("扩散比例速度.")]
        public double EmissionMinScaleSpeed
        {
            get
            {
                return emissionMinScaleSpeed;
            }
            set
            {
                if (value != emissionMinScaleSpeed)
                {
                    emissionMinScaleSpeed = value;
                    NotifyPropertyChange("EmissionMinScaleSpeed");
                }
            }
        }

        private double emissionMaxScaleSpeed;
        [Category("粒子系统参数"), Description("扩散比例速度.")]
        public double EmissionMaxScaleSpeed
        {
            get
            {
                return emissionMaxScaleSpeed;
            }
            set
            {
                if (value != emissionMaxScaleSpeed)
                {
                    emissionMaxScaleSpeed = value;
                    NotifyPropertyChange("EmissionMaxScaleSpeed");
                }
            }
        }

        private double verticalAcceleration;
        [Category("粒子系统参数"), Description("垂直加速度.")]
        public double VerticalAcceleration
        {
            get
            {
                return verticalAcceleration;
            }
            set
            {
                if (value != verticalAcceleration)
                {
                    verticalAcceleration = value;
                    NotifyPropertyChange("VerticalAcceleration");
                }
            }
        }

        private double emissionMaxAngle;
        [Category("粒子系统参数"), Description("最大扩散角度.")]
        public double EmissionMaxAngle
        {
            get
            {
                return emissionMaxAngle;
            }
            set
            {
                if (value != emissionMaxAngle)
                {
                    emissionMaxAngle = value;
                    NotifyPropertyChange("EmissionMaxAngle");
                }
            }
        }

        private double emissionMinAngle;
        [Category("粒子系统参数"), Description("最小扩散角度.")]
        public double EmissionMinAngle
        {
            get
            {
                return emissionMinAngle;
            }
            set
            {
                if (value != emissionMinAngle)
                {
                    emissionMinAngle = value;
                    NotifyPropertyChange("EmissionMinAngle");
                }
            }
        }

        private double emissionMinMoveSpeed;
        [Category("粒子系统参数"), Description("最小移动速度.")]
        public double EmissionMinMoveSpeed
        {
            get
            {
                return emissionMinMoveSpeed;
            }
            set
            {
                if (value != emissionMinMoveSpeed)
                {
                    emissionMinMoveSpeed = value;
                    NotifyPropertyChange("EmissionMinMoveSpeed");
                }
            }
        }

        private double emissionMinParticleSize;
        [Category("粒子系统参数"), Description("最小粒子大小.")]
        public double EmissionMinParticleSize
        {
            get
            {
                return emissionMinParticleSize;
            }
            set
            {
                if (value != emissionMinParticleSize)
                {
                    emissionMinParticleSize = value;
                    NotifyPropertyChange("EmissionMinParticleSize");
                }
            }
        }

        private double emissionMaxParticleSize;
        [Category("粒子系统参数"), Description("最大粒子大小.")]
        public double EmissionMaxParticleSize
        {
            get
            {
                return emissionMaxParticleSize;
            }
            set
            {
                if (value != emissionMaxParticleSize)
                {
                    emissionMaxParticleSize = value;
                    NotifyPropertyChange("EmissionMaxParticleSize");
                }
            }
        }

        private double emissionMinRotationSpeed;
        [Category("粒子系统参数"), Description("粒子最小旋转速度.")]
        public double EmissionMinRotationSpeed
        {
            get
            {
                return emissionMinRotationSpeed;
            }
            set
            {
                if (value != emissionMinRotationSpeed)
                {
                    emissionMinRotationSpeed = value;
                    NotifyPropertyChange("EmissionMinRotationSpeed");
                }
            }
        }

        private double emissionMaxRotationSpeed;
        [Category("粒子系统参数"), Description("粒子最大旋转速度.")]
        public double EmissionMaxRotationSpeed
        {
            get
            {
                return emissionMaxRotationSpeed;
            }
            set
            {
                if (value != emissionMaxRotationSpeed)
                {
                    emissionMaxRotationSpeed = value;
                    NotifyPropertyChange("EmissionMaxRotationSpeed");
                }
            }
        }
        private double particleAspectRatio;
        [Category("粒子的宽高比例"), Description("粒子的宽高比例")]
        public double ParticleAspectRatio
        {
            get
            {
                return this.particleAspectRatio;
            }
            set
            {
                if (value != this.particleAspectRatio)
                {
                    this.particleAspectRatio = value;
                    NotifyPropertyChange("ParticleAspectRatio");
                }
            }
        }

        private double damping;
        [Category("粒子系统参数"), Description("阻力.")]
        public double Damping
        {
            get
            {
                return damping;
            }
            set
            {
                if (value != damping)
                {
                    damping = value;
                    NotifyPropertyChange("Damping");
                }
            }
        }

        private string textureTileRange;
        [Category("粒子系统参数"), Description("贴图切片参数.")]
        public string TextureTileRange
        {
            get
            {
                return textureTileRange;
            }
            set
            {
                if (value != textureTileRange)
                {
                    textureTileRange = value;
                    NotifyPropertyChange("TextureTileRange");
                }
            }
        }
        private string emissionDirectionEulerAngleStr;
        [Category("粒子系统参数"), Description("粒子发射器的朝向")]
        public string EmissionDirectionEulerAngle
        {
            get
            {
                return emissionDirectionEulerAngleStr.ToString();
            }
            set
            {
                if (value != emissionDirectionEulerAngleStr)
                {
                    emissionDirectionEulerAngleStr = value;
                    NotifyPropertyChange("EmissionDirectionEulerAngle");
                }                
            }
        }
        #endregion

        protected IEulerAngle v3 = new EulerAngle();
        public ParticleEffect()
        {
            ImageName = (strMediaPath + @"\png\Partial.png");
            EmissionMaxMoveSpeed = 8;
            EmissionMinMoveSpeed = 5;
            EmissionMinRate = 64;
            EmissionMaxRate = 90;
            ParticleMaxLifeTime = 1.8;
            ParticleMinLifeTime = 1;
            ParticleBillboardType = gviParticleBillboardType.gviParticleBillboardOrientedCamera;
            WindDirection = 0;
            WindAcceleration = 0.2;
            ParticleBirthColor = ColorHelper.UintToColor(4294954035);
            ParticleDeathColor = ColorHelper.UintToColor(16731187);
            emissionMinScaleSpeed = 0;
            EmissionMaxScaleSpeed = 0;
            VerticalAcceleration = 0.8;
            EmissionMaxAngle = 40;
            EmissionMinAngle = 0.1;
            EmissionMinParticleSize = 0.2;
            EmissionMaxParticleSize = 0.3;
            EmissionMinRotationSpeed = -1;
            EmissionMaxRotationSpeed = 1;
            Damping = 0.5;
            particleAspectRatio = 1;
            v3.Set(0, 0, 0);
            EmissionDirectionEulerAngle = v3.Heading.ToString() + "," + v3.Tilt.ToString() + "," + v3.Roll.ToString();
        }

        private void NotifyPropertyChange(string type)
        {
            if (NotifyPropertyChangeEvent != null)
            {
                NotifyPropertyChangeEvent(type);
            }
        }

        public virtual void Draw()
        {
            try
            {
                if (this.particleEffect == null)
                {
                    this.particleEffect = CommonUnity.RenderHelper.ObjectManager.CreateParticleEffect(_Guid);
                }

            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
        }

        public virtual void Start()
        {
            particleEffect.Start(-1);
        }

        public virtual void Refresh()
        {
            try
            {
                particleEffect.ImageName = this.ImageName;
                particleEffect.EmissionMaxMoveSpeed = EmissionMaxMoveSpeed;
                particleEffect.EmissionMinMoveSpeed = EmissionMinMoveSpeed;
                particleEffect.EmissionMinRate = EmissionMinRate;
                particleEffect.EmissionMaxRate = EmissionMaxRate;
                particleEffect.ParticleMaxLifeTime = ParticleMaxLifeTime;
                particleEffect.ParticleMinLifeTime = ParticleMinLifeTime;
                particleEffect.ParticleBillboardType = ParticleBillboardType;
                particleEffect.WindDirection = WindDirection;
                particleEffect.WindAcceleration = WindAcceleration;
                particleEffect.ParticleBirthColor = (ParticleBirthColor);
                particleEffect.ParticleDeathColor = (ParticleDeathColor);
                particleEffect.EmissionMinScaleSpeed = EmissionMinScaleSpeed;
                particleEffect.EmissionMaxScaleSpeed = EmissionMaxScaleSpeed;
                particleEffect.VerticalAcceleration = VerticalAcceleration;
                particleEffect.EmissionMaxAngle = EmissionMaxAngle;
                particleEffect.EmissionMinAngle = EmissionMinAngle;
                particleEffect.EmissionMinParticleSize = EmissionMinParticleSize;
                particleEffect.EmissionMaxParticleSize = EmissionMaxParticleSize;
                particleEffect.EmissionMinRotationSpeed = EmissionMinRotationSpeed;
                particleEffect.EmissionMaxRotationSpeed = EmissionMaxRotationSpeed;
                particleEffect.Damping = Damping;
                particleEffect.MaxVisibleDistance = 1000;
                particleEffect.ParticleAspectRatio = this.particleAspectRatio;
                if (emissionDirectionEulerAngleStr != null)
                {
                    string[] arr = emissionDirectionEulerAngleStr.Split(',');
                    if (arr.Length == 3)
                    {
                        v3.Set(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
                        particleEffect.EmissionDirectionEulerAngle = v3;
                    }
                }
                if (textureTileRange != null)
                {
                    string[] arr = textureTileRange.Split(',');
                    if (arr.Length == 4)
                    {
                        particleEffect.SetTextureTileRange(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]), int.Parse(arr[3]));
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
            switch (EmitterType)
            {
                case EmitterTypeCategory.Point:
                    particleEffect.SetPointEmitter(v3);
                    break;
                case EmitterTypeCategory.Circle:
                    particleEffect.SetCircleEmitter(v3, EmitterRadius);
                    break;
                case EmitterTypeCategory.Box:
                    particleEffect.SetBoxEmitter(v3, EmitterWidth, EmitterHeight, EmitterDepth);
                    break;
                default:
                    particleEffect.SetCircleEmitter(v3, 1.4);
                    break;
            }
        }
    }
}
