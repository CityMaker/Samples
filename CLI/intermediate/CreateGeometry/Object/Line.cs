using System.ComponentModel;

namespace CreateGeometry
{
    class Line : Curve
    {
        private double midpointX;
        [Category("Line属性"), ReadOnly(true), Description("中点的X坐标")]
        [DisplayName("Midpoint.X")]
        public double MidpointX
        {
            get
            {
                return midpointX;
            }
            set
            {
                if (value != midpointX)
                {
                    midpointX = value;
                }
            }
        }

        private double midpointY;
        [Category("Line属性"), ReadOnly(true), Description("中点的Y坐标")]
        [DisplayName("Midpoint.Y")]
        public double MidpointY
        {
            get
            {
                return midpointY;
            }
            set
            {
                if (value != midpointY)
                {
                    midpointY = value;
                }
            }
        }

        private double midpointZ;
        [Category("Line属性"), ReadOnly(true), Description("中点的Z坐标")]
        [DisplayName("Midpoint.Z")]
        public double MidpointZ
        {
            get
            {
                return midpointZ;
            }
            set
            {
                if (value != midpointZ)
                {
                    midpointZ = value;
                }
            }
        }

        private bool isPoint;
        [Category("Line属性"), ReadOnly(true), Description("线段是否退化为点")]
        public bool IsPoint
        {
            get { return isPoint; }
            set
            {
                if (value != isPoint)
                {
                    isPoint = value;
                }
            }
        }
    }
}
