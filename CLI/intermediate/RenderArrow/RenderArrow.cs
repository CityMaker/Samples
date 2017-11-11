using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Gvitech.CityMaker.RenderControl;

namespace RenderArrow
{
    public class RenderArrow
    {
        public delegate void PropertyChanged();
        public event PropertyChanged NotifyPropertyChangeEvent;
        IRenderArrow renderArrow = null;

        [Browsable(false)]
        public IRenderArrow RenderArrowObj
        {
            get
            {
                return this.renderArrow;
            }
            set
            {
                this.renderArrow = value;
            }
        }

        private void NotifyPropertyChange()
        {
            if (NotifyPropertyChangeEvent != null)
            {
                NotifyPropertyChangeEvent();
            }
        }

        #region 参数
        private gviArrowType arrowType;
        [Category("RenderArrow"), Description("箭头类型")]
        public gviArrowType ArrowType
        {
            get { return arrowType; }
            set
            {
                if (value != arrowType)
                {
                    arrowType = value;
                    NotifyPropertyChange();
                }
            }
        }

        private double bottomWidth;
        [Category("RenderArrow"), Description("箭头底宽")]
        public double BottomWidth
        {
            get { return bottomWidth; }
            set
            {
                if (value != bottomWidth)
                {
                    bottomWidth = value;
                    NotifyPropertyChange();
                }
            }
        }

        private double chordHeight;
        [Category("RenderArrow"), Description("双箭头的箭身弦高")]
        public double ChordHeight
        {
            get { return chordHeight; }
            set
            {
                if (value != chordHeight)
                {
                    chordHeight = value;
                    NotifyPropertyChange();
                }
            }
        }

        private bool dualArrowFollow;
        [Category("RenderArrow"), Description("双箭头是否跟随连动")]
        public bool DualArrowFollow
        {
            get { return dualArrowFollow; }
            set
            {
                if (value != dualArrowFollow)
                {
                    dualArrowFollow = value;
                    NotifyPropertyChange();
                }
            }
        }

        private double headHeight;
        [Category("RenderArrow"), Description("箭头到箭身的高度")]
        public double HeadHeight
        {
            get { return headHeight; }
            set
            {
                if (value != headHeight)
                {
                    headHeight = value;
                    NotifyPropertyChange();
                }
            }
        }

        private double tolerance;
        [Category("RenderArrow"), Description("箭头曲线离散化的弦高容差")]
        public double Tolerance
        {
            get { return tolerance; }
            set
            {
                if (value != tolerance)
                {
                    tolerance = value;
                    NotifyPropertyChange();
                }
            }
        }

        private double wingAngle;
        [Category("RenderArrow"), Description("箭头前翼与箭身的夹角")]
        public double WingAngle
        {
            get { return wingAngle; }
            set
            {
                if (value != wingAngle)
                {
                    wingAngle = value;
                    NotifyPropertyChange();
                }
            }
        }

        private double wingBottomLength;
        [Category("RenderArrow"), Description("箭尾长度")]
        public double WingBottomLength
        {
            get { return wingBottomLength; }
            set
            {
                if (value != wingBottomLength)
                {
                    wingBottomLength = value;
                    NotifyPropertyChange();
                }
            }
        }

        private double wingLength;
        [Category("RenderArrow"), Description("箭头前翼的长度")]
        public double WingLength
        {
            get { return wingLength; }
            set
            {
                if (value != wingLength)
                {
                    wingLength = value;
                    NotifyPropertyChange();
                }
            }
        }
        #endregion


        public RenderArrow(IRenderArrow initArrow)
        {
            ArrowType = initArrow.ArrowType;
            BottomWidth = initArrow.BottomWidth;
            ChordHeight = initArrow.ChordHeight;
            DualArrowFollow = initArrow.DualArrowFollow;
            HeadHeight = initArrow.HeadHeight;
            Tolerance = initArrow.Tolerance;
            WingAngle = initArrow.WingAngle;
            WingBottomLength = initArrow.WingBottomLength;
            WingLength = initArrow.WingLength;

            RenderArrowObj = initArrow;
        }

        public void Refresh()
        {
            renderArrow.ArrowType = ArrowType;
            renderArrow.BottomWidth = BottomWidth;
            renderArrow.ChordHeight = ChordHeight;
            renderArrow.DualArrowFollow = DualArrowFollow;
            renderArrow.HeadHeight = HeadHeight;
            renderArrow.Tolerance = Tolerance;
            renderArrow.WingAngle = WingAngle;
            renderArrow.WingBottomLength = WingBottomLength;
            renderArrow.WingLength = WingLength;
        }


    }
}
