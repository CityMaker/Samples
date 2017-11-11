using Gvitech.CityMaker.FdeGeometry;
using System.ComponentModel;

namespace CreateGeometry
{
    class SurfacePatch : Surface
    {
        private gviSurfaceInterpolationType surfaceInterpolationType;
        [Category("SurfacePatch属性"), ReadOnly(true), Description("表面的插值类型")]
        public gviSurfaceInterpolationType SurfaceInterpolationType
        {
            get
            {
                return surfaceInterpolationType;
            }
            set
            {
                if (value != surfaceInterpolationType)
                {
                    surfaceInterpolationType = value;
                }
            }
        }
    }
}
