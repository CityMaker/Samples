using System.ComponentModel;

namespace CreateGeometry
{
    class Ellipse : Curve
    {
        private double centerPointX;
        [Category("Ellipse属性"), ReadOnly(true), Description("圆心的X坐标")]
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
        [Category("Ellipse属性"), ReadOnly(true), Description("圆心的Y坐标")]
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
        [Category("Ellipse属性"), ReadOnly(true), Description("圆心的Z坐标")]
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
    }
}
