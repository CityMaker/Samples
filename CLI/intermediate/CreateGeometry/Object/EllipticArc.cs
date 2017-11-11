using System.ComponentModel;

namespace CreateGeometry
{
    class EllipticArc : Curve
    {
        private double centerPointX;
        [Category("EllipticArc属性"), ReadOnly(true), Description("椭圆中心点的X坐标")]
        [DisplayName("CenterPoint.X")]
        public double CenterPointX
        {
            get
            {
                return centerPointX;
            }
            set
            {
                if (value != centerPointX)
                {
                    centerPointX = value;
                }
            }
        }

        private double centerPointY;
        [Category("EllipticArc属性"), ReadOnly(true), Description("椭圆中心点的Y坐标")]
        [DisplayName("CenterPoint.Y")]
        public double CenterPointY
        {
            get
            {
                return centerPointY;
            }
            set
            {
                if (value != centerPointY)
                {
                    centerPointY = value;
                }
            }
        }

        private double centerPointZ;
        [Category("EllipticArc属性"), ReadOnly(true), Description("椭圆中心点的Z坐标")]
        [DisplayName("CenterPoint.Z")]
        public double CenterPointZ
        {
            get
            {
                return centerPointZ;
            }
            set
            {
                if (value != centerPointZ)
                {
                    centerPointZ = value;
                }
            }
        }

        private double centralAngle;
        [Category("EllipticArc属性"), ReadOnly(true), Description("椭圆弧对应的张角(弧度)")]
        public double CentralAngle
        {
            get
            {
                return centralAngle;
            }
            set
            {
                if (value != centralAngle)
                {
                    centralAngle = value;
                }
            }
        }

        private bool isCircular;
        [Category("EllipticArc属性"), ReadOnly(true), Description("判断该椭圆弧是否是圆弧")]
        public bool IsCircular
        {
            get { return isCircular; }
            set
            {
                if (value != isCircular)
                {
                    isCircular = value;
                }
            }
        }

        private bool isLine;
        [Category("EllipticArc属性"), ReadOnly(true), Description("判断该椭圆弧是否近似直线，即半径无限大(共直线)")]
        public bool IsLine
        {
            get { return isLine; }
            set
            {
                if (value != isLine)
                {
                    isLine = value;
                }
            }
        }

        private bool isMinor;
        [Category("EllipticArc属性"), ReadOnly(true), Description("判断该椭圆弧是小弧还是大弧")]
        public bool IsMinor
        {
            get { return isMinor; }
            set
            {
                if (value != isMinor)
                {
                    isMinor = value;
                }
            }
        }

        private bool isPoint;
        [Category("EllipticArc属性"), ReadOnly(true), Description("判断该椭圆弧是否近似为一点，即半径=0(共点)")]
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
