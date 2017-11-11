using System;
using System.ComponentModel;

namespace CreateGeometry
{
    class ModelPoint : Point
    {
        private Array matrix33;
        [Category("ModelPoint属性"), ReadOnly(true), Description("矩阵")]
        public Array Matrix33
        {
            get
            {
                return matrix33;
            }
            set
            {
                if (value != matrix33)
                {
                    matrix33 = value;
                }
            }
        }

        private double modelEnvelopeMaxX;
        [Category("ModelPoint属性"), ReadOnly(true), Description("外接长方体的X最大值")]
        [DisplayName("ModelEnvelope.MaxX")]
        public double ModelEnvelopeMaxX
        {
            get { return modelEnvelopeMaxX; }
            set
            {
                if (value != modelEnvelopeMaxX)
                {
                    modelEnvelopeMaxX = value;
                }
            }
        }

        private double modelEnvelopeMaxY;
        [Category("ModelPoint属性"), ReadOnly(true), Description("外接长方体的Y最大值")]
        [DisplayName("ModelEnvelope.MaxY")]
        public double ModelEnvelopeMaxY
        {
            get { return modelEnvelopeMaxY; }
            set
            {
                if (value != modelEnvelopeMaxY)
                {
                    modelEnvelopeMaxY = value;
                }
            }
        }

        private double modelEnvelopeMaxZ;
        [Category("ModelPoint属性"), ReadOnly(true), Description("外接长方体的Z最大值")]
        [DisplayName("ModelEnvelope.MaxZ")]
        public double ModelEnvelopeMaxZ
        {
            get { return modelEnvelopeMaxZ; }
            set
            {
                if (value != modelEnvelopeMaxZ)
                {
                    modelEnvelopeMaxZ = value;
                }
            }
        }

        private double modelEnvelopeMinX;
        [Category("ModelPoint属性"), ReadOnly(true), Description("外接长方体的X最小值")]
        [DisplayName("ModelEnvelope.MinX")]
        public double ModelEnvelopeMinX
        {
            get { return modelEnvelopeMinX; }
            set
            {
                if (value != modelEnvelopeMinX)
                {
                    modelEnvelopeMinX = value;
                }
            }
        }

        private double modelEnvelopeMinY;
        [Category("ModelPoint属性"), ReadOnly(true), Description("外接长方体的Y最小值")]
        [DisplayName("ModelEnvelope.MinY")]
        public double ModelEnvelopeMinY
        {
            get { return modelEnvelopeMinY; }
            set
            {
                if (value != modelEnvelopeMinY)
                {
                    modelEnvelopeMinY = value;
                }
            }
        }

        private double modelEnvelopeMinZ;
        [Category("ModelPoint属性"), ReadOnly(true), Description("外接长方体的Z最小值")]
        [DisplayName("ModelEnvelope.MinZ")]
        public double ModelEnvelopeMinZ
        {
            get { return modelEnvelopeMinZ; }
            set
            {
                if (value != modelEnvelopeMinZ)
                {
                    modelEnvelopeMinZ = value;
                }
            }
        }

        private string modelName;
        [Category("ModelPoint属性"), ReadOnly(true), Description("模型文件路径")]
        public string ModelName
        {
            get
            {
                return modelName;
            }
            set
            {
                if (value != modelName)
                {
                    modelName = value;
                }
            }
        }
    }
}
