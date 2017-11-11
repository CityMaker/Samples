using System.ComponentModel;

namespace CreateGeometry
{
    class Surface : Geometry
    {
        private double area;
        [Category("Surface属性"), ReadOnly(true), Description("表面的表面积")]
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

        private double centroidX;
        [Category("Surface属性"), ReadOnly(true), Description("表面质心的X坐标")]
        [DisplayName("Centroid.X")]
        public double CentroidX
        {
            get
            {
                return centroidX;
            }
            set
            {
                if (value != centroidX)
                {
                    centroidX = value;
                }
            }
        }

        private double centroidY;
        [Category("Surface属性"), ReadOnly(true), Description("表面质心的Y坐标")]
        [DisplayName("Centroid.Y")]
        public double CentroidY
        {
            get
            {
                return centroidY;
            }
            set
            {
                if (value != centroidY)
                {
                    centroidY = value;
                }
            }
        }

        private double centroidZ;
        [Category("Surface属性"), ReadOnly(true), Description("表面质心的Z坐标")]
        [DisplayName("Centroid.Z")]
        public double CentroidZ
        {
            get
            {
                return centroidZ;
            }
            set
            {
                if (value != centroidZ)
                {
                    centroidZ = value;
                }
            }
        }

        private bool isClosed;
        [Category("Surface属性"), ReadOnly(true), Description("曲面是否封闭")]
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

        private double pointOnSurfaceX;
        [Category("Surface属性"), ReadOnly(true), Description("表面上的任意一点(默认为质心)的X坐标")]
        [DisplayName("PointOnSurface.X")]
        public double PointOnSurfaceX
        {
            get
            {
                return pointOnSurfaceX;
            }
            set
            {
                if (value != pointOnSurfaceX)
                {
                    pointOnSurfaceX = value;
                }
            }
        }

        private double pointOnSurfaceY;
        [Category("Surface属性"), ReadOnly(true), Description("表面上的任意一点(默认为质心)的Y坐标")]
        [DisplayName("PointOnSurface.Y")]
        public double PointOnSurfaceY
        {
            get
            {
                return pointOnSurfaceY;
            }
            set
            {
                if (value != pointOnSurfaceY)
                {
                    pointOnSurfaceY = value;
                }
            }
        }

        private double pointOnSurfaceZ;
        [Category("Surface属性"), ReadOnly(true), Description("表面上的任意一点(默认为质心)质心的Z坐标")]
        [DisplayName("PointOnSurface.Z")]
        public double PointOnSurfaceZ
        {
            get
            {
                return pointOnSurfaceZ;
            }
            set
            {
                if (value != pointOnSurfaceZ)
                {
                    pointOnSurfaceZ = value;
                }
            }
        }
    }
}
