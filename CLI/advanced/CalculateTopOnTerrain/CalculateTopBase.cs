using System;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using System.Drawing;

namespace CalculateTopOnTerrain
{
    /*==========================================================
     *Product:     CityMaker
     *Author：     Reason
     *Date：       2013/5/15
     *Module：     最高点计算基类
     *Description: 负责标注TableLabel，最高点计算
     ===========================================================*/

    /// <summary>
    /// 计算最高点基类
    /// </summary>
    public class CalculateTopBase : ICalculateTop
    {
        #region 成员

        /// <summary>
        /// 点样式
        /// </summary>
        protected IPointSymbol _PointSymbol = new SimplePointSymbol() { FillColor =  Color.Red };

        /// <summary>
        /// 面样式
        /// </summary>
        protected ISurfaceSymbol _SurfaceSymbol = new SurfaceSymbol() { Color = Color.Blue };

        /// <summary>
        /// 曲线样式
        /// </summary>
        protected ICurveSymbol _CurveSymbol = new CurveSymbol() { Color = Color.Yellow };

        /// <summary>
        /// 起始点
        /// </summary>
        protected IRenderPoint _StartRenderPoint;

        /// <summary>
        /// 结束点
        /// </summary>
        protected IRenderPoint _EndRenderPoint;

        /// <summary>
        /// 缓冲多边形
        /// </summary>
        protected IRenderPolygon _RenderPolygon;

        /// <summary>
        /// 文本标签
        /// </summary>
        protected ITableLabel _TableLabel;

        /// <summary>
        /// 最高点
        /// </summary>
        private IPoint _TopPoint;

        /// <summary>
        /// 三维控件
        /// </summary>
        protected Gvitech.CityMaker.Controls.AxRenderControl _AxRenderControl;

        protected System.Guid rootId = new System.Guid();

        protected int _MainThreadId = 0;
        protected System.Windows.Forms.Form _Form = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="axRenderControl">三维控件</param>
        public CalculateTopBase(Gvitech.CityMaker.Controls.AxRenderControl axRenderControl, int mainThreadId, System.Windows.Forms.Form form)
        {
            if (axRenderControl == null) { throw new ArgumentNullException("控件无效！"); }

            _AxRenderControl = axRenderControl;
            _SurfaceSymbol.BoundarySymbol = _CurveSymbol;

            rootId = _AxRenderControl.ObjectManager.GetProjectTree().RootID;

            _MainThreadId = mainThreadId;
            _Form = form;
        }

        #endregion

        #region ICalculateTop 方法

        /// <summary>
        /// 计算最高点
        /// </summary>
        public virtual void CalculateTop()
        {
            return;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <remarks>不再使用时调用</remarks>
        public virtual void Reset()
        {
            if (_StartRenderPoint != null)
            {
                _AxRenderControl.ObjectManager.DeleteObject(_StartRenderPoint.Guid);
                _StartRenderPoint = null;
            }
            if (_EndRenderPoint != null)
            {
                _AxRenderControl.ObjectManager.DeleteObject(_EndRenderPoint.Guid);
                _EndRenderPoint = null;
            }
            if (_RenderPolygon != null)
            {
                _AxRenderControl.ObjectManager.DeleteObject(_RenderPolygon.Guid);
                _RenderPolygon = null;
            }
            if (_TableLabel != null)
            {
                _AxRenderControl.ObjectManager.DeleteObject(_TableLabel.Guid);
                _TableLabel = null;
            }

            return;
        }

        #endregion

        #region 保护方法

        /// <summary>
        /// 计算最高点值
        /// </summary>
        /// <param name="envelope">计算范围</param>
        /// <returns>最高点高程</returns>
        /// <remarks>计算方式为在区域内按步长值依次选取采样点，求最大值</remarks>
        protected double GetTopValue(IPolygon calcPolygon)
        {
            IPolygon polygon = calcPolygon.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolygon;
            IEnvelope envelope = polygon.Envelope;

            // 采样点数量 可根据精度修改
            const double pickCount = 10000;

            // 高程计算模式
            gviGetElevationType mode = gviGetElevationType.gviGetElevationFromDatabase;

            // 最高点高程值和位置
            double topValue = double.MinValue;
            double topPointX = double.MinValue, topPointY = double.MinValue;

            double step = Math.Sqrt(envelope.Width * envelope.Height / pickCount);
            double xStart = envelope.MinX;
            double yStart = envelope.MinY;
            bool hasTopPoint = false;

            while (xStart <= envelope.MaxX)
            {
                yStart = envelope.MinY;
                while (yStart <= envelope.MaxY)
                {
                    double elevatation = _AxRenderControl.Terrain.GetElevation(xStart, yStart, mode);
                    IGeometryFactory factory = new GeometryFactory();
                    IPoint point = factory.CreatePoint(gviVertexAttribute.gviVertexAttributeNone);
                    IVector3 vector = new Vector3();
                    vector.Set(xStart, yStart, 0);
                    point.Position = vector;
                    bool pointOnSurface = (polygon.IsPointOnSurface(point));
                    if (topValue < elevatation && pointOnSurface)
                    {
                        topValue = elevatation;
                        topPointX = xStart;
                        topPointY = yStart;
                        hasTopPoint = true;
                    }

                    yStart += step;
                }
                xStart += step;
            }

            //MessageBox.Show(string.Format("最高点高度：{0}。坐标为X:{1} Y:{2} Z:{3}：", topValue, topPointX, topPointY, topValue));

            if (hasTopPoint)
            {
                DrawTableLabel(topPointX, topPointY, topValue);

                DrawTopPoint(topPointX, topPointY, topValue);
            }

            return topValue;
        }

        /// <summary>
        /// 绘制标签
        /// </summary>
        /// <param name="x">标签X坐标</param>
        /// <param name="y">标签Y坐标</param>
        /// <param name="z">标签Z坐标</param>
        protected void DrawTableLabel(double x, double y, double z)
        {
            int rowCount = 3, columnCount = 2;
            IObjectManager objManager = _AxRenderControl.ObjectManager;

            if (_TableLabel == null)
            {
                _TableLabel = objManager.CreateTableLabel(rowCount, columnCount, rootId);

                _TableLabel.TitleText = "最高点";

                _TableLabel.SetRecord(0, 0, "x:");
                _TableLabel.SetRecord(1, 0, "y:");
                _TableLabel.SetRecord(2, 0, "z:");

                _TableLabel.BorderColor = System.Drawing.Color.Yellow;
                _TableLabel.BorderWidth = 2;
                _TableLabel.TableBackgroundColor = System.Drawing.Color.Gray;

                // 内容标题样式
                TextAttribute columeCapitalTextAttribute = new TextAttribute() { 
                    TextColor = System.Drawing.Color.Black,
                    OutlineColor = System.Drawing.Color.Gray,
                    Font = "Calibri", 
                    TextSize = 15, 
                    Bold = true,
                    MultilineJustification = gviMultilineJustification.gviMultilineCenter 
                };
                _TableLabel.SetColumnTextAttribute(0, columeCapitalTextAttribute);

                // 内容值样式
                TextAttribute columeValueTextAttribute = new TextAttribute() {
                    TextColor = System.Drawing.Color.Black,
                    OutlineColor = System.Drawing.Color.Blue,
                    Font = "Calibri", 
                    TextSize = 13, 
                    Bold = true,
                    MultilineJustification = gviMultilineJustification.gviMultilineLeft
                };
                _TableLabel.SetColumnTextAttribute(1, columeValueTextAttribute);

                // 标题样式
                _TableLabel.TitleBackgroundColor = System.Drawing.Color.Yellow;
                TextAttribute titleTextAttribute = new TextAttribute() { TextColor = System.Drawing.Color.Black, OutlineColor = System.Drawing.Color.Green, Font = "宋体", TextSize = 15, MultilineJustification = gviMultilineJustification.gviMultilineCenter, Bold = true, BackgroundColor = System.Drawing.Color.Gray };
                _TableLabel.TitleTextAttribute = titleTextAttribute;
            }

            IPoint poi = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            poi.SetCoords(x, y, z, 0, 0);
            _TableLabel.Position = poi;

            _TableLabel.SetRecord(0, 1, x.ToString());
            _TableLabel.SetRecord(1, 1, y.ToString());
            _TableLabel.SetRecord(2, 1, z.ToString());

            _TableLabel.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }

        /// <summary>
        /// 清空
        /// </summary>
        protected virtual void Clear()
        {
            if (_StartRenderPoint != null) { _StartRenderPoint.VisibleMask = gviViewportMask.gviViewNone; }
            if (_EndRenderPoint != null) { _EndRenderPoint.VisibleMask = gviViewportMask.gviViewNone; }
            if (_RenderPolygon != null) { _RenderPolygon.VisibleMask = gviViewportMask.gviViewNone; }
            if (_TableLabel != null) { _TableLabel.VisibleMask = gviViewportMask.gviViewNone; }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="x">X值</param>
        /// <param name="y">Y值</param>
        /// <param name="z">Z值</param>
        private void DrawTopPoint(double x, double y, double z)
        {
            if (_TopPoint == null)
            {
                IGeometryFactory geometryFactory = new GeometryFactory();
                _TopPoint = geometryFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            }

            _TopPoint.SetCoords(x, y, z, 0, 0);

            if (_EndRenderPoint == null)
            {
                _EndRenderPoint = _AxRenderControl.ObjectManager.CreateRenderPoint(_TopPoint, _PointSymbol, rootId);
                _EndRenderPoint.MaxVisibleDistance = double.MaxValue;
                _EndRenderPoint.MinVisiblePixels = 1;
            }
            else
            {
                _EndRenderPoint.SetFdeGeometry(_TopPoint);
            }

            _EndRenderPoint.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }

        #endregion
    }
}
