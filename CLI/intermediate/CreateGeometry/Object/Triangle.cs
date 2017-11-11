using System.ComponentModel;

namespace CreateGeometry
{
    class Triangle : SurfacePatch
    {
        private bool isDegenerate;
        [Category("Triangle属性"), ReadOnly(true), Description("判断该三角形是否退化为一点")]
        public bool IsDegenerate
        {
            get { return isDegenerate; }
            set
            {
                if (value != isDegenerate)
                {
                    isDegenerate = value;
                }
            }
        }

        private double pX;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点P的X坐标")]
        [DisplayName("P.X")]
        public double PX
        {
            get
            {
                return pX;
            }
            set
            {
                if (value != pX)
                {
                    pX = value;
                }
            }
        }

        private double pY;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点P的Y坐标")]
        [DisplayName("P.Y")]
        public double PY
        {
            get
            {
                return pY;
            }
            set
            {
                if (value != pY)
                {
                    pY = value;
                }
            }
        }

        private double pZ;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点P的Z坐标")]
        [DisplayName("P.Z")]
        public double PZ
        {
            get
            {
                return pZ;
            }
            set
            {
                if (value != pZ)
                {
                    pZ = value;
                }
            }
        }

        private double qX;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点Q的X坐标")]
        [DisplayName("Q.X")]
        public double QX
        {
            get
            {
                return qX;
            }
            set
            {
                if (value != qX)
                {
                    qX = value;
                }
            }
        }

        private double qY;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点Q的Y坐标")]
        [DisplayName("Q.Y")]
        public double QY
        {
            get
            {
                return qY;
            }
            set
            {
                if (value != qY)
                {
                    qY = value;
                }
            }
        }

        private double qZ;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点Q的Z坐标")]
        [DisplayName("Q.Z")]
        public double QZ
        {
            get
            {
                return qZ;
            }
            set
            {
                if (value != qZ)
                {
                    qZ = value;
                }
            }
        }

        private double rX;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点R的X坐标")]
        [DisplayName("R.X")]
        public double RX
        {
            get
            {
                return rX;
            }
            set
            {
                if (value != rX)
                {
                    rX = value;
                }
            }
        }

        private double rY;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点R的Y坐标")]
        [DisplayName("R.Y")]
        public double RY
        {
            get
            {
                return rY;
            }
            set
            {
                if (value != rY)
                {
                    rY = value;
                }
            }
        }

        private double rZ;
        [Category("Triangle属性"), ReadOnly(true), Description("三角形顶点R的Z坐标")]
        [DisplayName("R.Z")]
        public double RZ
        {
            get
            {
                return rZ;
            }
            set
            {
                if (value != rZ)
                {
                    rZ = value;
                }
            }
        }
    }
}
