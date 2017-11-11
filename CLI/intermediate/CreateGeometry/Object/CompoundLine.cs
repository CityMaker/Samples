using System.ComponentModel;

namespace CreateGeometry
{
    class CompoundLine : Curve
    {
        private double pointCount;
        [Category("CompoundLine属性"), ReadOnly(true), Description("复合线的点数")]
        public double PointCount
        {
            get
            {
                return pointCount;
            }
            set
            {
                if (value != pointCount)
                {
                    pointCount = value;
                }
            }
        }

        private double segmentCount;
        [Category("CompoundLine属性"), ReadOnly(true), Description("Segment个数")]
        public double SegmentCount
        {
            get
            {
                return segmentCount;
            }
            set
            {
                if (value != segmentCount)
                {
                    segmentCount = value;
                }
            }
        }
    }
}
