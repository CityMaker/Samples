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
using Gvitech.CityMaker.RenderControl;

namespace ParticleEffect
{
    /// <summary>
    /// \brief：火特效
    /// \date：2012.9.12
    /// \author：高爽
    /// </summary>
    public class FireEffect : ParticleEffect
    {
        public FireEffect(System.Guid guid)
        {
            _Guid = guid;

            this.ParticleBillboardType = gviParticleBillboardType.gviParticleBillboardOrientedMoveDirection;
            this.EmitterType = EmitterTypeCategory.Circle;
            this.TextureTileRange = "8,8,0,6";
            this.EmissionMinRate = 60;
            this.EmissionMaxRate = 110;
            this.EmissionMinAngle = 0;
            this.EmissionMaxAngle = 3.14 * 0.5;

            this.EmissionMinMoveSpeed = 0;
            this.EmissionMaxMoveSpeed = 1;
            this.EmissionMinRotationSpeed = -1;


            this.EmissionMaxRotationSpeed = 1;
            this.ParticleMinLifeTime = 1.7;
            this.ParticleMaxLifeTime = 1.5;

            this.EmissionMinParticleSize = 0.75;
            this.EmissionMinParticleSize = 0.9;

            this.EmissionMinScaleSpeed = 0.05;
            this.EmissionMaxScaleSpeed = 0.08;

            this.ParticleBirthColor = Color.FromArgb(255, 255, 194, 51);
            this.ParticleDeathColor = Color.FromArgb(0, 255, 76, 51);
            this.VerticalAcceleration = -2.5;
            this.Damping = -0.5;
            this.WindAcceleration = 0.2;
            this.WindDirection = 0;
        }
    }
}
