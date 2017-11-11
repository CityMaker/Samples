using System;
using System.ComponentModel;

namespace CreateGeometry
{
    class Polygon : SurfacePatch
    {
        private Int64 interiorRingCount;
        [Category("Polygon属性"), ReadOnly(true), Description("内环数量")]
        public Int64 InteriorRingCount
        {
            get
            {
                return interiorRingCount;
            }
            set
            {
                if (value != interiorRingCount)
                {
                    interiorRingCount = value;
                }
            }
        }

        private bool isCoplanar;
        [Category("Polygon属性"), ReadOnly(true), Description("外环内环是否共面")]
        public bool IsCoplanar 
        {
            get
            {
                return isCoplanar;
            }
            set
            {
                if (value != isCoplanar)
                {
                    isCoplanar = value;
                }
            }
        }
    }
}
