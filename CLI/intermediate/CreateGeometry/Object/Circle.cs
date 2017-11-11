using System.ComponentModel;

namespace CreateGeometry
{
    class Circle : Curve
    {
        private double centerPointX;
        [Category("Circle属性"), ReadOnly(true), Description("圆心的X坐标")]
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
        [Category("Circle属性"), ReadOnly(true), Description("圆心的Y坐标")]
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
        [Category("Circle属性"), ReadOnly(true), Description("圆心的Z坐标")]
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

        private double radius;
        [Category("Circle属性"), ReadOnly(true), Description("半径长度")]
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

        private double normalX;
        [Category("Circle属性"), ReadOnly(true), Description("垂直于圆所在平面的单位法向量-X")]
        [DisplayName("Normal.X")]
        public double NormalX
        {
            get
            {
                return normalX;
            }
            set
            {
                if (value != normalX)
                {
                    normalX = value;
                }
            }
        }

        private double normalY;
        [Category("Circle属性"), ReadOnly(true), Description("垂直于圆所在平面的单位法向量-Y")]
        [DisplayName("Normal.Y")]
        public double NormalY
        {
            get
            {
                return normalY;
            }
            set
            {
                if (value != normalY)
                {
                    normalY = value;
                }
            }
        }

        private double normalZ;
        [Category("Circle属性"), ReadOnly(true), Description("垂直于圆所在平面的单位法向量-Z")]
        [DisplayName("Normal.Z")]
        public double NormalZ
        {
            get
            {
                return normalZ;
            }
            set
            {
                if (value != NormalZ)
                {
                    normalZ = value;
                }
            }
        }
    }
}
