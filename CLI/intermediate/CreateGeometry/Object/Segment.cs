using System.ComponentModel;
using Gvitech.CityMaker.FdeGeometry;

namespace CreateGeometry
{
    class Segment : Curve
    {
        private gviCurveInterpolationType interpolationType;
        [Category("Segment属性"), ReadOnly(true), Description("片段segment的插值类型")]
        public gviCurveInterpolationType InterpolationType
        {
            get
            {
                return interpolationType;
            }
            set
            {
                if (value != interpolationType)
                {
                    interpolationType = value;
                }
            }
        }
    }
}
