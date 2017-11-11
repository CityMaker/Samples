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
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace ParticleEffect
{
    /// <summary>
    /// \brief：烟特效
    /// \date：2012.9.12
    /// \author：高爽
    /// </summary>
    public class SmokeEffect : ParticleEffect
    {
        public SmokeEffect(System.Guid guid)
        {
            _Guid = guid;

            this.ParticleBillboardType = 0;  // gviParticleBillboardOrientedCamera 要朝着相机
            this.EmitterType = EmitterTypeCategory.Circle;  // 用Circle效果较好
            this.TextureTileRange = "8,8,63,63";  // 58,58位置的图有问题
            this.EmissionMinRate = 20;
            this.EmissionMaxRate = 30;
            this.EmissionMinAngle = 0;
            this.EmissionMaxAngle = 3.14 * 2.0;   // 改成一个圆
            this.EmissionMinMoveSpeed = 0;
            this.EmissionMaxMoveSpeed = 1;
            this.EmissionMinRotationSpeed = -1;
            this.EmissionMaxRotationSpeed = 1;

            this.ParticleMinLifeTime = 10;
            this.ParticleMaxLifeTime = 12;
            this.EmissionMinParticleSize = 0.75;
            this.EmissionMinParticleSize = 0.9;

            this.EmissionMinScaleSpeed = 1.5;
            this.EmissionMaxScaleSpeed = 1.85;
            this.ParticleBirthColor = Color.FromArgb(255, 255, 255, 255);
            this.ParticleDeathColor = Color.FromArgb(0, 255, 255, 255);
            this.VerticalAcceleration = -2;
            this.Damping = 0;
            this.WindAcceleration = 0;
            this.WindDirection = 0;
        }
    }
}
