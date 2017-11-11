using System.ComponentModel;

namespace CreateGeometry
{
    class Curve : Geometry
    {
        private bool isClosed;
        [Category("Curve属性"), ReadOnly(true), Description("曲线是否封闭，即两个端点是否重合")]
        public bool IsClosed
        {
            get
            {
                return isClosed;
            }
            set
            {
                if (value != isClosed)
                {
                    isClosed = value;
                }
            }
        }

        private double length;
        [Category("Curve属性"), ReadOnly(true), Description("曲线长度")]
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

        private double startPointX;
        [Category("Curve属性"), ReadOnly(true), Description("起点的X坐标")]
        [DisplayName("StartPoint.X")]
        public double StartPointX
        {
            get
            {
                return startPointX;
            }
            set
            {
                if (value != startPointX)
                {
                    startPointX = value;
                }
            }
        }

        private double startPointY;
        [Category("Curve属性"), ReadOnly(true), Description("起点的Y坐标")]
        [DisplayName("StartPoint.Y")]
        public double StartPointY
        {
            get
            {
                return startPointY;
            }
            set
            {
                if (value != startPointY)
                {
                    startPointY = value;
                }
            }
        }

        private double startPointZ;
        [Category("Curve属性"), ReadOnly(true), Description("起点的Z坐标")]
        [DisplayName("StartPoint.Z")]
        public double StartPointZ
        {
            get
            {
                return startPointZ;
            }
            set
            {
                if (value != startPointZ)
                {
                    startPointZ = value;
                }
            }
        }

        private double endPointX;
        [Category("Curve属性"), ReadOnly(true), Description("终点的X坐标")]
        [DisplayName("EndPoint.X")]
        public double EndPointX
        {
            get
            {
                return endPointX;
            }
            set
            {
                if (value != endPointX)
                {
                    endPointX = value;
                }
            }
        }

        private double endPointY;
        [Category("Curve属性"), ReadOnly(true), Description("终点的Y坐标")]
        [DisplayName("EndPoint.Y")]
        public double EndPointY
        {
            get
            {
                return endPointY;
            }
            set
            {
                if (value != endPointY)
                {
                    endPointY = value;
                }
            }
        }

        private double endPointZ;
        [Category("Curve属性"), ReadOnly(true), Description("终点的Z坐标")]
        [DisplayName("EndPoint.Z")]
        public double EndPointZ
        {
            get
            {
                return endPointZ;
            }
            set
            {
                if (value != endPointZ)
                {
                    endPointZ = value;
                }
            }
        }
    }
}
