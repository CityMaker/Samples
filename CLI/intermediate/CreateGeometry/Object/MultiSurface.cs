using System.ComponentModel;

namespace CreateGeometry
{
    class MultiSurface : GeometryCollection
    {
        private double area;
        [Category("MultiSurface属性"), ReadOnly(true), Description("多表面的总面积")]
        public double Area
        {
            get
            {
                return area;
            }
            set
            {
                if (value != area)
                {
                    area = value;
                }
            }
        }
    }
}
