using System.ComponentModel;

namespace CreateGeometry
{
    class CirculeArc : Curve
    {
        private double centerPointX;
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧中心点的X坐标")]
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
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧中心点的Y坐标")]
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
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧中心点的Z坐标")]
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
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧对应的张角(弧度)")]
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

        private double chordHeight;
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧玄高")]
        public double ChordHeight
        {
            get
            {
                return chordHeight;
            }
            set
            {
                if (value != chordHeight)
                {
                    chordHeight = value;
                }
            }
        }

        private double chordLength;
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧玄长")]
        public double ChordLength
        {
            get
            {
                return chordLength;
            }
            set
            {
                if (value != chordLength)
                {
                    chordLength = value;
                }
            }
        }

        private bool isLine;
        [Category("CirculeArc属性"), ReadOnly(true), Description("判断该圆弧是否近似直线，即半径无限大(共直线)")]
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
        [Category("CirculeArc属性"), ReadOnly(true), Description("判断该圆弧是小弧还是大弧")]
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
        [Category("CirculeArc属性"), ReadOnly(true), Description("判断该圆弧是否近似为一点，即半径=0(共点)")]
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

        private double pointOnArcX;
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧上第三点的X坐标")]
        [DisplayName("PointOnArc.X")]
        public double PointOnArcX
        {
            get
            {
                return pointOnArcX;
            }
            set
            {
                if (value != pointOnArcX)
                {
                    pointOnArcX = value;
                }
            }
        }

        private double pointOnArcY;
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧上第三点的Y坐标")]
        [DisplayName("PointOnArc.Y")]
        public double PointOnArcY
        {
            get
            {
                return pointOnArcY;
            }
            set
            {
                if (value != pointOnArcY)
                {
                    pointOnArcY = value;
                }
            }
        }

        private double pointOnArcZ;
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧上第三点的Z坐标")]
        [DisplayName("PointOnArc.Z")]
        public double PointOnArcZ
        {
            get
            {
                return pointOnArcZ;
            }
            set
            {
                if (value != pointOnArcZ)
                {
                    pointOnArcZ = value;
                }
            }
        }

        private double radius;
        [Category("CirculeArc属性"), ReadOnly(true), Description("圆弧半径长度")]
        public double Radius
        {
            get
            {
                return radius;
            }
            set
            {
                if (value != radius)
                {
                    radius = value;
                }
            }
        }
    }
}
