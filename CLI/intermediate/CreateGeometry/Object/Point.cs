using System.ComponentModel;

namespace CreateGeometry
{
    class Point : Geometry
    {
        private int id;
        [Category("Point属性"), ReadOnly(true), Description("几何外键")]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                }
            }
        }

        private double m;
        [Category("Point属性"), ReadOnly(true), Description("测量值")]
        public double M
        {
            get
            {
                return m;
            }
            set
            {
                if (value != m)
                {
                    m = value;
                }
            }
        }

        private double x;
        [Category("Point属性"), ReadOnly(true), Description("X坐标")]
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (value != x)
                {
                    x = value;
                }
            }
        }

        private double y;
        [Category("Point属性"), ReadOnly(true), Description("Y坐标")]
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value != y)
                {
                    y = value;
                }
            }
        }

        private double z;
        [Category("Point属性"), ReadOnly(true), Description("Z坐标")]
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                if (value != z)
                {
                    z = value;
                }
            }
        }
    }
}
