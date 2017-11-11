using System;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;

namespace CalculateTopOnTerrain
{
    /*==========================================================
     *Product:     CityMaker
     *Author：     Reason
     *Date：       2013/5/15
     *Module：     多边形区域最高点计算
     *Description: 
     ===========================================================*/

    /// <summary>
    /// 多边形计算最高点
    /// </summary>
    public class PolygonCalculateTop :CalculateTopBase, ICalculateTop
    {
        private _IRenderControlEvents_RcObjectEditFinishEventHandler _rcObjectEditFinish;

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="axRenderControl">主控件</param>
        public PolygonCalculateTop(AxRenderControl axRenderControl, int mainThreadId, System.Windows.Forms.Form form)
            : base(axRenderControl, mainThreadId, form)
        {
            
        }

        #endregion

        #region ICalculateTop 方法

        /// <summary>
        /// 计算最高点
        /// </summary>
        public override void CalculateTop()
        {
            _AxRenderControl.RcObjectEditFinish -= new _IRenderControlEvents_RcObjectEditFinishEventHandler(_AxRenderControl_RcObjectEditFinish);
            _rcObjectEditFinish = null;
            _AxRenderControl.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(_AxRenderControl_RcObjectEditFinish);
            _rcObjectEditFinish = new _IRenderControlEvents_RcObjectEditFinishEventHandler(_AxRenderControl_RcObjectEditFinish);

            _AxRenderControl.InteractMode = gviInteractMode.gviInteractEdit;
            _AxRenderControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectTerrain;
            _AxRenderControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

            StartDraw();
        }

     

        /// <summary>
        /// 重置
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            _AxRenderControl.RcObjectEditFinish -= new _IRenderControlEvents_RcObjectEditFinishEventHandler(_AxRenderControl_RcObjectEditFinish);
            _rcObjectEditFinish = null;

            _AxRenderControl.InteractMode = gviInteractMode.gviInteractNormal;
            _AxRenderControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            _AxRenderControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        #endregion

        #region 监听函数

        /// <summary>
        /// 编辑结束
        /// </summary>
        void _AxRenderControl_RcObjectEditFinish()
        {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId != _MainThreadId)
            {
                _Form.BeginInvoke(_rcObjectEditFinish);
                return;
            }

            IPolygon polygon = _RenderPolygon.GetFdeGeometry() as IPolygon;
            if (polygon == null) { return; }

            GetTopValue(polygon);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 绘制区域多边形
        /// </summary>
        private void StartDraw()
        {
            Clear();
            IGeometryFactory geometryFactory = new GeometryFactory();
            IPolygon polygon = (IPolygon)geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeNone);

            ISurfaceSymbol surfaceSymbol = new SurfaceSymbol() { Color = System.Drawing.Color.Yellow };

            IObjectManager objectManager = _AxRenderControl.ObjectManager;
            _RenderPolygon = objectManager.CreateRenderPolygon(polygon, surfaceSymbol, rootId);
            _RenderPolygon.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
            _RenderPolygon.MaxVisibleDistance = double.MaxValue;
            _RenderPolygon.MinVisiblePixels = 3;

            _AxRenderControl.ObjectEditor.StartEditRenderGeometry(_RenderPolygon, gviGeoEditType.gviGeoEditCreator);
        }

        #endregion
    }
}
