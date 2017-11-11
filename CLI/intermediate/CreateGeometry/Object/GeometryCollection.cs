using System;
using System.ComponentModel;

namespace CreateGeometry
{
    class GeometryCollection : Geometry
    {
        private Int32 geometryCount;
        [Category("GeometryCollection属性"), ReadOnly(true), Description("几何聚集内部Geometry个数")]
        public Int32 GeometryCount 
        {
            get
            {
                return geometryCount;
            }
            set
            {
                if (value != geometryCount)
                {
                    geometryCount = value;
                }
            }
        }

        private bool isOverlap;
        [Category("GeometryCollection属性"), ReadOnly(true), Description("加入集合的几何体是否在空间上重叠")]
        public bool IsOverlap
        {
            get
            {
                return isOverlap;
            }
            set
            {
                if (value != isOverlap)
                {
                    isOverlap = value;
                }
            }
        }
    }
}
