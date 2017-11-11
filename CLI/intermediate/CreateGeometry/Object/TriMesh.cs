using System;
using System.ComponentModel;

namespace CreateGeometry
{
    class TriMesh : Surface
    {
        private Int32 directedEdgeCount;
        [Category("TriMesh属性"), ReadOnly(true), Description("三角网方向边数量")]
        public Int32 DirectedEdgeCount
        {
            get
            {
                return directedEdgeCount;
            }
            set
            {
                if (value != directedEdgeCount)
                {
                    directedEdgeCount = value;
                }
            }
        }

        private Int32 facetCount;
        [Category("TriMesh属性"), ReadOnly(true), Description("三角网面数量")]
        public Int32 FacetCount
        {
            get
            {
                return facetCount;
            }
            set
            {
                if (value != facetCount)
                {
                    facetCount = value;
                }
            }
        }

        private Int32 vertexCount;
        [Category("TriMesh属性"), ReadOnly(true), Description("三角网顶点数量")]
        public Int32 VertexCount
        {
            get
            {
                return vertexCount;
            }
            set
            {
                if (value != vertexCount)
                {
                    vertexCount = value;
                }
            }
        }
    }
}
