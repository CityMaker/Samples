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
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;

namespace ParticleEffect
{
    /// <summary>
    /// \brief   水的粒子特效
    /// \date   2012.9.24
    /// \author 高爽
    /// </summary>
    public class WaterEffect : ParticleEffect
    {
        public WaterEffect(System.Guid guid)
        {
            _Guid = guid;

            ImageName = (strMediaPath + @"\png\water.png");

            ParticleBillboardType = Gvitech.CityMaker.RenderControl.gviParticleBillboardType.gviParticleBillboardOrientedMoveDirection;
            EmissionMinRate = 1600;
            EmissionMaxRate = 1300;
            EmissionMinMoveSpeed = 25;
            EmissionMaxMoveSpeed = 30;
            EmissionMinAngle = 0;
            EmissionMaxAngle = 0.025 * (Math.PI);
            EmissionMinParticleSize = 0.2;
            EmissionMaxParticleSize = 0.25;
            EmissionMinScaleSpeed = 0;
            EmissionMaxScaleSpeed = 0;
            ParticleBirthColor = Color.FromArgb(255, 255, 255, 255);
            ParticleDeathColor = Color.FromArgb(0, 255, 255, 255);
            ParticleMinLifeTime = 4.5;
            ParticleMaxLifeTime = 5.5;
            ParticleAspectRatio = 0.3;
            VerticalAcceleration = 5;
            Damping = 0.5;
            WindAcceleration = 0.1;
            WindDirection = 0;
            EmissionMinRotationSpeed = 0;
            EmissionMaxRotationSpeed = 0;
            v3.Set(90, 45, 0);
            EmissionDirectionEulerAngle = v3.Heading.ToString() + "," + v3.Tilt.ToString() + "," + v3.Roll.ToString();
        }
    }
}
