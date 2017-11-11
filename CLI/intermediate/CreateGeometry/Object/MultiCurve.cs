using System.ComponentModel;

namespace CreateGeometry
{
    class MultiCurve : GeometryCollection
    {
        private double length;
        [Category("MultiCurve属性"), ReadOnly(true), Description("多曲线的总长度")]
        public double Length
        {
            get
            {
                return length;
            }
            set
            {
                if (value != length)
                {
                    length = value;
                }
            }
        }
    }
}
