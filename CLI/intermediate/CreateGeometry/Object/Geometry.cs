using System.ComponentModel;
using Gvitech.CityMaker.FdeGeometry;

namespace CreateGeometry
{
    class Geometry
    {
        private gviGeometryDimension dimension;
        [Category("Geometry属性"), ReadOnly(true), Description("三维空间中几何实例的拓扑维度")]
        public gviGeometryDimension Dimension
        {
            get
            {
                return dimension;
            }
            set
            {
                if (value != dimension)
                {
                    dimension = value;
                }
            }
        }

        private double maxX;
        [Category("Geometry属性"), ReadOnly(true), Description("外接长方体的X最大值")]
        [DisplayName("Envelope.MaxX")]
        public double MaxX
        {
            get { return maxX; }
            set
            {
                if (value != maxX)
                {
                    maxX = value;
                }
            }
        }

        private double maxY;
        [Category("Geometry属性"), ReadOnly(true), Description("外接长方体的Y最大值")]
        [DisplayName("Envelope.MaxY")]
        public double MaxY
        {
            get { return maxY; }
            set
            {
                if (value != maxY)
                {
                    maxY = value;
                }
            }
        }

        private double maxZ;
        [Category("Geometry属性"), ReadOnly(true), Description("外接长方体的Z最大值")]
        [DisplayName("Envelope.MaxZ")]
        public double MaxZ
        {
            get { return maxZ; }
            set
            {
                if (value != maxZ)
                {
                    maxZ = value;
                }
            }
        }

        private double minX;
        [Category("Geometry属性"), ReadOnly(true), Description("外接长方体的X最小值")]
        [DisplayName("Envelope.MinX")]
        public double MinX
        {
            get { return minX; }
            set
            {
                if (value != minX)
                {
                    minX = value;
                }
            }
        }

        private double minY;
        [Category("Geometry属性"), ReadOnly(true), Description("外接长方体的Y最小值")]
        [DisplayName("Envelope.MinY")]
        public double MinY
        {
            get { return minY; }
            set
            {
                if (value != minY)
                {
                    minY = value;
                }
            }
        }

        private double minZ;
        [Category("Geometry属性"), ReadOnly(true), Description("外接长方体的Z最小值")]
        [DisplayName("Envelope.MinZ")]
        public double MinZ
        {
            get { return minZ; }
            set
            {
                if (value != minZ)
                {
                    minZ = value;
                }
            }
        }

        private gviGeometryType geometryType;
        [Category("Geometry属性"), ReadOnly(true), Description("几何体的类型")]
        public gviGeometryType GeometryType
        {
            get
            {
                return geometryType;
            }
            set
            {
                if (value != geometryType)
                {
                    geometryType = value;
                }
            }
        }

        private bool isEmpty;
        [Category("Geometry属性"), ReadOnly(true), Description("是否为空几何体")]
        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                if (value != isEmpty)
                {
                    isEmpty = value;
                }
            }
        }

        private bool isValid;
        [Category("Geometry属性"), ReadOnly(true), Description("几何体是否合法(有效)")]
        public bool IsValid
        {
            get { return isValid; }
            set
            {
                if (value != isValid)
                {
                    isValid = value;
                }
            }
        }

        private gviVertexAttribute vertexAttribute;
        [Category("Geometry属性"), ReadOnly(true), Description("几何体的顶点属性")]
        public gviVertexAttribute VertexAttribute
        {
            get
            {
                return vertexAttribute;
            }
            set
            {
                if (value != vertexAttribute)
                {
                    vertexAttribute = value;
                }
            }
        }

        private bool hasId;
        [Category("Geometry属性"), ReadOnly(true), Description("是否带有几何外键")]
        public bool HasId
        {
            get { return hasId; }
            set
            {
                if (value != hasId)
                {
                    hasId = value;
                }
            }
        }

        private bool hasM;
        [Category("Geometry属性"), ReadOnly(true), Description("是否带有测量值")]
        public bool HasM
        {
            get { return hasM; }
            set
            {
                if (value != hasM)
                {
                    hasM = value;
                }
            }
        }

        private bool hasZ;
        [Category("Geometry属性"), ReadOnly(true), Description("是否带有高度值")]
        public bool HasZ
        {
            get { return hasZ; }
            set
            {
                if (value != hasZ)
                {
                    hasZ = value;
                }
            }
        }

        //private string wkt;
        //[Category("Geometry属性"), ReadOnly(true), Description("符合OGC WKT标准的字符串")]
        //public string WKT
        //{
        //    get { return wkt; }
        //    set
        //    {
        //        if (value != wkt)
        //        {
        //            wkt = value;
        //        }
        //    }
        //}
    }
}
