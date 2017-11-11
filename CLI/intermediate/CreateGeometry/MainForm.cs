using System;
using System.Windows.Forms;
using System.IO;
using System.Collections;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;

namespace CreateGeometry
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";   

        private IGeometryFactory gfactory = null;

        private IPoint point = null;
        private IModelPoint modelPoint = null;
        private ICircle circle = null;
        private ICirculeArc circularArc = null;
        private ILine line = null;
        private ICompoundLine compoundLine = null;
        private IPolyline polyline = null;
        private IPolygon polygon = null;
        private ITriMesh triMesh = null;
        private IClosedTriMesh closedTriMesh = null;

        private IMultiPoint multiPoint = null;
        private IMultiPolyline multiPolyline = null;
        private IMultiPolygon multiPolygon = null;
        private IMultiTriMesh multiTriMesh = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            

            {
                this.helpProvider1.SetShowHelp(this.tabControl1, true);
                this.helpProvider1.SetHelpString(this.tabControl1, "");
                this.helpProvider1.HelpNamespace = "CreateGeometry.html";
            }
        }

        #region Point
        private void btnConstructPoint_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();

            point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            point.SetCoords(100, 101, 102, 3, 9);

            if (point == null)
                return;

            Point geo = new Point();
            // Geometry属性
            geo.Dimension = point.Dimension;
            if (point.Envelope != null)
            {
                geo.MaxX = point.Envelope.MaxX;
                geo.MaxY = point.Envelope.MaxY;
                geo.MaxZ = point.Envelope.MaxZ;
                geo.MinX = point.Envelope.MinX;
                geo.MinY = point.Envelope.MinY;
                geo.MinZ = point.Envelope.MinZ;
            }
            geo.GeometryType = point.GeometryType;
            geo.IsEmpty = point.IsEmpty;
            geo.IsValid = point.IsValid;
            geo.VertexAttribute = point.VertexAttribute;
            geo.HasId = point.HasId();
            geo.HasM = point.HasM();
            geo.HasZ = point.HasZ();
            // Point属性
            geo.Id = point.Id;
            geo.M = point.M;
            geo.X = point.X;
            geo.Y = point.Y;
            geo.Z = point.Z;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region ModelPoint
        private void btnConstructModelPoint_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();

            modelPoint = gfactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
            modelPoint.SetCoords(100, 200, 300, 0, 0);
            modelPoint.ModelName = "testModel";
            //IMatrix matrix = new Matrix();
            //matrix.MakeIdentity();
            //modelPoint.Matrix33 = matrix as Array;
            IEnvelope boundingBox = new Envelope();
            boundingBox.Set(50, 60, 70, 150, 160, 170);
            modelPoint.ModelEnvelope = boundingBox;

            if (modelPoint == null)
                return;

            ModelPoint geo = new ModelPoint();
            // Geometry属性
            geo.Dimension = modelPoint.Dimension;
            if (modelPoint.Envelope != null)
            {
                geo.MaxX = modelPoint.Envelope.MaxX;
                geo.MaxY = modelPoint.Envelope.MaxY;
                geo.MaxZ = modelPoint.Envelope.MaxZ;
                geo.MinX = modelPoint.Envelope.MinX;
                geo.MinY = modelPoint.Envelope.MinY;
                geo.MinZ = modelPoint.Envelope.MinZ;
            }
            geo.GeometryType = modelPoint.GeometryType;
            geo.IsEmpty = modelPoint.IsEmpty;
            geo.IsValid = modelPoint.IsValid;
            geo.VertexAttribute = modelPoint.VertexAttribute;
            geo.HasId = modelPoint.HasId();
            geo.HasM = modelPoint.HasM();
            geo.HasZ = modelPoint.HasZ();
            // Point属性
            geo.Id = modelPoint.Id;
            geo.M = modelPoint.M;
            geo.X = modelPoint.X;
            geo.Y = modelPoint.Y;
            geo.Z = modelPoint.Z;
            // modelPoint属性
            geo.Matrix33 = modelPoint.Matrix33;
            if (modelPoint.ModelEnvelope != null)
            {
                geo.ModelEnvelopeMaxX = modelPoint.ModelEnvelope.MaxX;
                geo.ModelEnvelopeMaxY = modelPoint.ModelEnvelope.MaxY;
                geo.ModelEnvelopeMaxZ = modelPoint.ModelEnvelope.MaxZ;
                geo.ModelEnvelopeMinX = modelPoint.ModelEnvelope.MinX;
                geo.ModelEnvelopeMinY = modelPoint.ModelEnvelope.MinY;
                geo.ModelEnvelopeMinZ = modelPoint.ModelEnvelope.MinZ;
            }
            geo.ModelName = modelPoint.ModelName;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region ICircle
        private void btnConstructCircle_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();

            // 圆心
            point = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            point.X = 5; point.Y = 6; point.Z = 7;
            // 半径
            double dRadius = 10.0;
            // 垂直于圆所在平面的单位法向量，用于确定圆在三维空间的位置
            // 如果只有圆心和半径那么在三维空间内有无数的这样的圆组成一个球
            IVector3 vNormal = new Vector3();
            vNormal.Set(0, 0, 1);
            circle = gfactory.CreateGeometry(gviGeometryType.gviGeometryCircle, gviVertexAttribute.gviVertexAttributeZ) as ICircle;
            circle.ConstructCenterAndRadius(point, dRadius, vNormal);
            if (circle == null)
                return;

            Circle geo = new Circle();
            // Geometry属性
            geo.Dimension = circle.Dimension;
            if (circle.Envelope != null)
            {
                geo.MaxX = circle.Envelope.MaxX;
                geo.MaxY = circle.Envelope.MaxY;
                geo.MaxZ = circle.Envelope.MaxZ;
                geo.MinX = circle.Envelope.MinX;
                geo.MinY = circle.Envelope.MinY;
                geo.MinZ = circle.Envelope.MinZ;
            }
            geo.GeometryType = circle.GeometryType;
            geo.IsEmpty = circle.IsEmpty;
            geo.IsValid = circle.IsValid;
            geo.VertexAttribute = circle.VertexAttribute;
            geo.HasId = circle.HasId();
            geo.HasM = circle.HasM();
            geo.HasZ = circle.HasZ();
            // Curve属性
            geo.IsClosed = circle.IsClosed;
            geo.Length = circle.Length;
            if (circle.StartPoint != null)
            {
                geo.StartPointX = circle.StartPoint.X;
                geo.StartPointY = circle.StartPoint.Y;
                geo.StartPointZ = circle.StartPoint.Z;
            }
            if (circle.EndPoint != null)
            {
                geo.EndPointX = circle.EndPoint.X;
                geo.EndPointY = circle.EndPoint.Y;
                geo.EndPointZ = circle.EndPoint.Z;
            }
            // Circle属性
            if (circle.CenterPoint != null)
            {
                geo.CenterPointX = circle.CenterPoint.X;
                geo.CenterPointY = circle.CenterPoint.Y;
                geo.CenterPointZ = circle.CenterPoint.Z;
            }
            geo.Radius = circle.Radius;
            if (circle.Normal != null)
            {
                geo.NormalX = circle.Normal.X;
                geo.NormalY = circle.Normal.Y;
                geo.NormalZ = circle.Normal.Z;
            }

            this.propertyGrid1.SelectedObject = geo;
        }

        #endregion

        #region CirculeArc
        private void btnConstructCirculeArc_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();

            IPoint point1 = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            point1.SetCoords(3.56, 4, 5, 0, 1);
            IPoint point2 = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            point2.SetCoords(1, -1, 0, 0, 2);
            IPoint point3 = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            point3.SetCoords(2, 0, 0, 0, 3);

            circularArc = gfactory.CreateGeometry(gviGeometryType.gviGeometryCircularArc, gviVertexAttribute.gviVertexAttributeZ) as ICirculeArc;
            circularArc.ConstructThreePoints(point1, point2, point3);
            if (circularArc == null)
                return;

            CirculeArc geo = new CirculeArc();
            // Geometry属性
            geo.Dimension = circularArc.Dimension;
            if (circularArc.Envelope != null)
            {
                geo.MaxX = circularArc.Envelope.MaxX;
                geo.MaxY = circularArc.Envelope.MaxY;
                geo.MaxZ = circularArc.Envelope.MaxZ;
                geo.MinX = circularArc.Envelope.MinX;
                geo.MinY = circularArc.Envelope.MinY;
                geo.MinZ = circularArc.Envelope.MinZ;
            }
            geo.GeometryType = circularArc.GeometryType;
            geo.IsEmpty = circularArc.IsEmpty;
            geo.IsValid = circularArc.IsValid;
            geo.VertexAttribute = circularArc.VertexAttribute;
            geo.HasId = circularArc.HasId();
            geo.HasM = circularArc.HasM();
            geo.HasZ = circularArc.HasZ();
            // Curve属性
            geo.IsClosed = circularArc.IsClosed;
            geo.Length = circularArc.Length;
            if (circularArc.StartPoint != null)
            {
                geo.StartPointX = circularArc.StartPoint.X;
                geo.StartPointY = circularArc.StartPoint.Y;
                geo.StartPointZ = circularArc.StartPoint.Z;
            }
            if (circularArc.EndPoint != null)
            {
                geo.EndPointX = circularArc.EndPoint.X;
                geo.EndPointY = circularArc.EndPoint.Y;
                geo.EndPointZ = circularArc.EndPoint.Z;
            }
            // CirculeArc属性
            //if (circularArc.CenterPoint != null)
            //{
            //    geo.CenterPointX = circularArc.CenterPoint.X;
            //    geo.CenterPointY = circularArc.CenterPoint.Y;
            //    geo.CenterPointZ = circularArc.CenterPoint.Z;
            //}
            geo.CentralAngle = circularArc.CentralAngle;
            geo.ChordHeight = circularArc.ChordHeight;
            geo.ChordLength = circularArc.ChordLength;
            geo.IsLine = circularArc.IsLine;
            geo.IsMinor = circularArc.IsMinor;
            geo.IsPoint = circularArc.IsPoint;
            if (circularArc.PointOnArc != null)
            {
                geo.PointOnArcX = circularArc.PointOnArc.X;
                geo.PointOnArcY = circularArc.PointOnArc.Y;
                geo.PointOnArcZ = circularArc.PointOnArc.Z;
            }
            geo.Radius = circularArc.Radius;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region Line
        private void btnConstructLine_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();

            IPoint pFrom = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            pFrom.SetCoords(100, 101, 102, 0, 1);
            IPoint pTo = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            pTo.SetCoords(200, 201, 202, 0, 2);

            line = gfactory.CreateGeometry(gviGeometryType.gviGeometryLine, gviVertexAttribute.gviVertexAttributeZ) as ILine;
            line.StartPoint = pFrom;
            line.EndPoint = pTo;
            if (line == null)
                return;

            Line geo = new Line();
            // Geometry属性
            geo.Dimension = line.Dimension;
            if (line.Envelope != null)
            {
                geo.MaxX = line.Envelope.MaxX;
                geo.MaxY = line.Envelope.MaxY;
                geo.MaxZ = line.Envelope.MaxZ;
                geo.MinX = line.Envelope.MinX;
                geo.MinY = line.Envelope.MinY;
                geo.MinZ = line.Envelope.MinZ;
            }
            geo.GeometryType = line.GeometryType;
            geo.IsEmpty = line.IsEmpty;
            geo.IsValid = line.IsValid;
            geo.VertexAttribute = line.VertexAttribute;
            geo.HasId = line.HasId();
            geo.HasM = line.HasM();
            geo.HasZ = line.HasZ();
            // Curve属性
            geo.IsClosed = line.IsClosed;
            geo.Length = line.Length;
            if (line.StartPoint != null)
            {
                geo.StartPointX = line.StartPoint.X;
                geo.StartPointY = line.StartPoint.Y;
                geo.StartPointZ = line.StartPoint.Z;
            }
            if (line.EndPoint != null)
            {
                geo.EndPointX = line.EndPoint.X;
                geo.EndPointY = line.EndPoint.Y;
                geo.EndPointZ = line.EndPoint.Z;
            }
            // Line属性
            if (line.Midpoint != null)
            {
                geo.MidpointX = line.Midpoint.X;
                geo.MidpointY = line.Midpoint.Y;
                geo.MidpointZ = line.Midpoint.Z;
            }
            geo.IsPoint = line.IsPoint;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region CompoundLine
        private void btnConstructCompoundLine_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();

            IPoint point1 = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            point1.SetCoords(3.56, 4, 5, 0, 1);
            IPoint point2 = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            point2.SetCoords(1, -1, 0, 0, 2);
            IPoint point3 = gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            point3.SetCoords(2, 0, 0, 0, 3);

            circularArc = gfactory.CreateGeometry(gviGeometryType.gviGeometryCircularArc, gviVertexAttribute.gviVertexAttributeZ) as ICirculeArc;
            circularArc.ConstructThreePoints(point1, point2, point3);
            if (circularArc == null)
                return;

            compoundLine = gfactory.CreateGeometry(gviGeometryType.gviGeometryCompoundLine, gviVertexAttribute.gviVertexAttributeZ) as ICompoundLine;
            compoundLine.AppendSegment(circularArc);
            if (compoundLine == null)
                return;

            CompoundLine geo = new CompoundLine();
            // Geometry属性
            geo.Dimension = compoundLine.Dimension;
            if (compoundLine.Envelope != null)
            {
                geo.MaxX = compoundLine.Envelope.MaxX;
                geo.MaxY = compoundLine.Envelope.MaxY;
                geo.MaxZ = compoundLine.Envelope.MaxZ;
                geo.MinX = compoundLine.Envelope.MinX;
                geo.MinY = compoundLine.Envelope.MinY;
                geo.MinZ = compoundLine.Envelope.MinZ;
            }
            geo.GeometryType = compoundLine.GeometryType;
            geo.IsEmpty = compoundLine.IsEmpty;
            geo.IsValid = compoundLine.IsValid;
            geo.VertexAttribute = compoundLine.VertexAttribute;
            geo.HasId = compoundLine.HasId();
            geo.HasM = compoundLine.HasM();
            geo.HasZ = compoundLine.HasZ();
            // Curve属性
            geo.IsClosed = compoundLine.IsClosed;
            geo.Length = compoundLine.Length;
            if (compoundLine.StartPoint != null)
            {
                geo.StartPointX = compoundLine.StartPoint.X;
                geo.StartPointY = compoundLine.StartPoint.Y;
                geo.StartPointZ = compoundLine.StartPoint.Z;
            }
            if (compoundLine.EndPoint != null)
            {
                geo.EndPointX = compoundLine.EndPoint.X;
                geo.EndPointY = compoundLine.EndPoint.Y;
                geo.EndPointZ = compoundLine.EndPoint.Z;
            }
            // CompoundLine属性
            geo.PointCount = compoundLine.PointCount;
            geo.SegmentCount = compoundLine.SegmentCount;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region PolyLine
        private void btnConstructPolyline_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();

            point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            polyline = gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;

            int length = 100;
            for (int i = 0; i < length; i++)
            {
                point.SetCoords(i, 2 * i, 0, 0, i + 1);
                polyline.AppendPoint(point);
            }
            if (polyline == null)
                return;

            CompoundLine geo = new CompoundLine();
            // Geometry属性
            geo.Dimension = polyline.Dimension;
            if (polyline.Envelope != null)
            {
                geo.MaxX = polyline.Envelope.MaxX;
                geo.MaxY = polyline.Envelope.MaxY;
                geo.MaxZ = polyline.Envelope.MaxZ;
                geo.MinX = polyline.Envelope.MinX;
                geo.MinY = polyline.Envelope.MinY;
                geo.MinZ = polyline.Envelope.MinZ;
            }
            geo.GeometryType = polyline.GeometryType;
            geo.IsEmpty = polyline.IsEmpty;
            geo.IsValid = polyline.IsValid;
            geo.VertexAttribute = polyline.VertexAttribute;
            geo.HasId = polyline.HasId();
            geo.HasM = polyline.HasM();
            geo.HasZ = polyline.HasZ();
            // Curve属性
            geo.IsClosed = polyline.IsClosed;
            geo.Length = polyline.Length;
            if (polyline.StartPoint != null)
            {
                geo.StartPointX = polyline.StartPoint.X;
                geo.StartPointY = polyline.StartPoint.Y;
                geo.StartPointZ = polyline.StartPoint.Z;
            }
            if (polyline.EndPoint != null)
            {
                geo.EndPointX = polyline.EndPoint.X;
                geo.EndPointY = polyline.EndPoint.Y;
                geo.EndPointZ = polyline.EndPoint.Z;
            }
            // CompoundLine属性
            geo.PointCount = polyline.PointCount;
            geo.SegmentCount = polyline.SegmentCount;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region Polygon
        private void btnConstructPolygon_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();
            polygon = gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZM) as IPolygon;
            if (polygon == null)
                return;

            /// 外环顺时针：1-4-3-2-1
            ///  4-------3
            ///  |       |
            ///  |       |
            ///  1-------2
            IRing exteriorRing = polygon.ExteriorRing;
            point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZM);
            point.SetCoords(0, 0, 0, 0, 1);
            exteriorRing.AppendPoint(point);
            point.SetCoords(0, 200, 0, 0, 2);
            exteriorRing.AppendPoint(point);
            point.SetCoords(100, 200, 0, 0, 3);
            exteriorRing.AppendPoint(point);
            point.SetCoords(100, 0, 0, 0, 4);
            exteriorRing.AppendPoint(point);
            point.SetCoords(0, 0, 0, 0, 5);
            exteriorRing.AppendPoint(point);  //闭合

            /// 内环逆时针：1-2-3-4-1
            ///  4-------3
            ///  |       |
            ///  |       |
            ///  1-------2
            IRing interiorRing1 = gfactory.CreateGeometry(gviGeometryType.gviGeometryRing, gviVertexAttribute.gviVertexAttributeZM) as IRing;
            IRing interiorRing2 = gfactory.CreateGeometry(gviGeometryType.gviGeometryRing, gviVertexAttribute.gviVertexAttributeZM) as IRing;
            point.SetCoords(25, 25, 0, 0, 1);
            interiorRing1.AppendPoint(point);
            point.SetCoords(75, 25, 0, 0, 2);
            interiorRing1.AppendPoint(point);
            point.SetCoords(75, 75, 0, 0, 3);
            interiorRing1.AppendPoint(point);
            point.SetCoords(25, 75, 0, 0, 4);
            interiorRing1.AppendPoint(point);
            point.SetCoords(25, 25, 0, 0, 5);
            interiorRing1.AppendPoint(point);  //闭合

            point.SetCoords(5, 6, 0, 8, 6);
            interiorRing2.AppendPoint(point);
            point.SetCoords(2, 3, 0, 5, 7);
            interiorRing2.AppendPoint(point);
            point.SetCoords(1, 1, 0, 1, 8);
            interiorRing2.AppendPoint(point);
            point.SetCoords(5, 6, 0, 8, 9);
            interiorRing2.AppendPoint(point);

            polygon.AddInteriorRing(interiorRing1);
            polygon.AddInteriorRing(interiorRing2);

            Polygon geo = new Polygon();
            // Geometry属性
            geo.Dimension = polygon.Dimension;
            if (polygon.Envelope != null)
            {
                geo.MaxX = polygon.Envelope.MaxX;
                geo.MaxY = polygon.Envelope.MaxY;
                geo.MaxZ = polygon.Envelope.MaxZ;
                geo.MinX = polygon.Envelope.MinX;
                geo.MinY = polygon.Envelope.MinY;
                geo.MinZ = polygon.Envelope.MinZ;
            }
            geo.GeometryType = polygon.GeometryType;
            geo.IsEmpty = polygon.IsEmpty;
            geo.IsValid = polygon.IsValid;
            geo.VertexAttribute = polygon.VertexAttribute;
            geo.HasId = polygon.HasId();
            geo.HasM = polygon.HasM();
            geo.HasZ = polygon.HasZ();
            // Surface属性
            geo.Area = polygon.Area();
            if (polygon.Centroid != null)
            {
                geo.CentroidX = polygon.Centroid.X;
                geo.CentroidY = polygon.Centroid.Y;
                geo.CentroidZ = polygon.Centroid.Z;
            }
            geo.IsClosed = polygon.IsClosed;
            if (polygon.PointOnSurface != null)
            {
                geo.PointOnSurfaceX = polygon.PointOnSurface.X;
                geo.PointOnSurfaceY = polygon.PointOnSurface.Y;
                geo.PointOnSurfaceZ = polygon.PointOnSurface.Z;
            }
            // SurfacePatch属性
            geo.SurfaceInterpolationType = polygon.SurfaceInterpolationType;
            // Polygon属性
            geo.InteriorRingCount = polygon.InteriorRingCount;
            geo.IsCoplanar = polygon.IsCoplanar;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region TriMesh
        private void btnConstructTriMesh_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();
            triMesh = gfactory.CreateGeometry(gviGeometryType.gviGeometryTriMesh, gviVertexAttribute.gviVertexAttributeZ) as ITriMesh;
            if (triMesh == null)
                return;

            ArrayList vertices = new ArrayList();  //ITopoNode数组
            point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            for (int i = 0; i < 17; ++i)
            {
                point.SetCoords(i, i, i, 0, 0);
                vertices.Add(triMesh.AddPoint(point));
            }

            point.SetCoords(100, 100, 100, 0, 0);
            triMesh.AddPoint(point);

            ArrayList facets = new ArrayList();  //ITopoFacet数组
            facets.Add(triMesh.AddTriangle(vertices[0] as ITopoNode, vertices[1] as ITopoNode, vertices[2] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[2] as ITopoNode, vertices[1] as ITopoNode, vertices[3] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[3] as ITopoNode, vertices[1] as ITopoNode, vertices[4] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[3] as ITopoNode, vertices[4] as ITopoNode, vertices[5] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[5] as ITopoNode, vertices[4] as ITopoNode, vertices[6] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[5] as ITopoNode, vertices[6] as ITopoNode, vertices[7] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[7] as ITopoNode, vertices[6] as ITopoNode, vertices[8] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[7] as ITopoNode, vertices[8] as ITopoNode, vertices[9] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[7] as ITopoNode, vertices[9] as ITopoNode, vertices[10] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[10] as ITopoNode, vertices[9] as ITopoNode, vertices[11] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[10] as ITopoNode, vertices[11] as ITopoNode, vertices[12] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[13] as ITopoNode, vertices[10] as ITopoNode, vertices[12] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[13] as ITopoNode, vertices[14] as ITopoNode, vertices[10] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[13] as ITopoNode, vertices[15] as ITopoNode, vertices[14] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[15] as ITopoNode, vertices[16] as ITopoNode, vertices[14] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[16] as ITopoNode, vertices[5] as ITopoNode, vertices[14] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[3] as ITopoNode, vertices[5] as ITopoNode, vertices[16] as ITopoNode));
            facets.Add(triMesh.AddTriangle(vertices[2] as ITopoNode, vertices[3] as ITopoNode, vertices[16] as ITopoNode));

            TriMesh geo = new TriMesh();
            // Geometry属性
            geo.Dimension = triMesh.Dimension;
            if (triMesh.Envelope != null)
            {
                geo.MaxX = triMesh.Envelope.MaxX;
                geo.MaxY = triMesh.Envelope.MaxY;
                geo.MaxZ = triMesh.Envelope.MaxZ;
                geo.MinX = triMesh.Envelope.MinX;
                geo.MinY = triMesh.Envelope.MinY;
                geo.MinZ = triMesh.Envelope.MinZ;
            }
            geo.GeometryType = triMesh.GeometryType;
            geo.IsEmpty = triMesh.IsEmpty;
            geo.IsValid = triMesh.IsValid;
            geo.VertexAttribute = triMesh.VertexAttribute;
            geo.HasId = triMesh.HasId();
            geo.HasM = triMesh.HasM();
            geo.HasZ = triMesh.HasZ();
            // Surface属性
            if (triMesh.Centroid != null)
            {
                geo.CentroidX = triMesh.Centroid.X;
                geo.CentroidY = triMesh.Centroid.Y;
                geo.CentroidZ = triMesh.Centroid.Z;
            }
            geo.IsClosed = triMesh.IsClosed;
            if (triMesh.PointOnSurface != null)
            {
                geo.PointOnSurfaceX = triMesh.PointOnSurface.X;
                geo.PointOnSurfaceY = triMesh.PointOnSurface.Y;
                geo.PointOnSurfaceZ = triMesh.PointOnSurface.Z;
            }
            // TriMesh属性
            geo.DirectedEdgeCount = triMesh.DirectedEdgeCount;
            geo.FacetCount = triMesh.FacetCount;
            geo.VertexCount = triMesh.VertexCount;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region ClosedTriMesh
        private void btnConstructClosedTriMesh_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();
            closedTriMesh = gfactory.CreateGeometry(gviGeometryType.gviGeometryClosedTriMesh, gviVertexAttribute.gviVertexAttributeZ) as IClosedTriMesh;
            if (closedTriMesh == null)
                return;

            string tmpFile = (strMediaPath + @"\off");
            if (File.Exists(String.Format("{0}\\oblong.off", tmpFile)))
            {
                StreamReader sr = new StreamReader(String.Format("{0}\\oblong.off", tmpFile));
                sr.ReadLine();
                string countParameter = sr.ReadLine();  //点数 面数
                string[] countParameters = countParameter.Split(' ');
                int nPointsCount = int.Parse(countParameters[0]);
                int nFacetsCount = int.Parse(countParameters[1]);
                if (nPointsCount == 0 && nFacetsCount == 0)
                {
                    sr.Close();
                    return;
                }

                sr.ReadLine();

                ArrayList vertices = new ArrayList();  //ITopoNode数组
                point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                for (int i = 0; i < nPointsCount; ++i)
                {
                    string[] pointCoords = sr.ReadLine().Split(' ');
                    double dx = double.Parse(pointCoords[0]);
                    double dy = double.Parse(pointCoords[1]);
                    double dz = double.Parse(pointCoords[2]);
                    point.SetCoords(dx, dy, dz, 0, 0);
                    vertices.Add(closedTriMesh.AddPoint(point));
                }

                ArrayList facets = new ArrayList();  //ITopoFacet数组
                for (int j = 0; j < nFacetsCount; ++j)
                {
                    string[] facetParas = sr.ReadLine().Split(' ');
                    int nFirst = int.Parse(facetParas[1]);
                    int nSecond = int.Parse(facetParas[2]);
                    int nThird = int.Parse(facetParas[3]);
                    facets.Add(closedTriMesh.AddTriangle(vertices[nFirst] as ITopoNode, vertices[nSecond] as ITopoNode, vertices[nThird] as ITopoNode));
                }

                sr.Close();
            }

            TriMesh geo = new TriMesh();
            // Geometry属性
            geo.Dimension = closedTriMesh.Dimension;
            if (closedTriMesh.Envelope != null)
            {
                geo.MaxX = closedTriMesh.Envelope.MaxX;
                geo.MaxY = closedTriMesh.Envelope.MaxY;
                geo.MaxZ = closedTriMesh.Envelope.MaxZ;
                geo.MinX = closedTriMesh.Envelope.MinX;
                geo.MinY = closedTriMesh.Envelope.MinY;
                geo.MinZ = closedTriMesh.Envelope.MinZ;
            }
            geo.GeometryType = closedTriMesh.GeometryType;
            geo.IsEmpty = closedTriMesh.IsEmpty;
            geo.IsValid = closedTriMesh.IsValid;
            geo.VertexAttribute = closedTriMesh.VertexAttribute;
            geo.HasId = closedTriMesh.HasId();
            geo.HasM = closedTriMesh.HasM();
            geo.HasZ = closedTriMesh.HasZ();
            // Surface属性
            if (closedTriMesh.Centroid != null)
            {
                geo.CentroidX = closedTriMesh.Centroid.X;
                geo.CentroidY = closedTriMesh.Centroid.Y;
                geo.CentroidZ = closedTriMesh.Centroid.Z;
            }
            geo.IsClosed = closedTriMesh.IsClosed;
            if (closedTriMesh.PointOnSurface != null)
            {
                geo.PointOnSurfaceX = closedTriMesh.PointOnSurface.X;
                geo.PointOnSurfaceY = closedTriMesh.PointOnSurface.Y;
                geo.PointOnSurfaceZ = closedTriMesh.PointOnSurface.Z;
            }
            // TriMesh属性
            geo.DirectedEdgeCount = closedTriMesh.DirectedEdgeCount;
            geo.FacetCount = closedTriMesh.FacetCount;
            geo.VertexCount = closedTriMesh.VertexCount;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region MultiPoint
        private void btnConstructMultiPoint_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();
            multiPoint = gfactory.CreateGeometry(gviGeometryType.gviGeometryMultiPoint, gviVertexAttribute.gviVertexAttributeZMID) as IMultiPoint;
            if (multiPoint == null)
                return;

            point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZMID); //point点属性必须与multiPoint相同，否则加不进去
            for (int i = 0; i < 100; i++)
            {
                point.SetCoords(i, 2 * i, 3 * i, 0, i + 1);
                multiPoint.AddPoint(point);
            }

            GeometryCollection geo = new GeometryCollection();
            // Geometry属性
            geo.Dimension = multiPoint.Dimension;
            if (multiPoint.Envelope != null)
            {
                geo.MaxX = multiPoint.Envelope.MaxX;
                geo.MaxY = multiPoint.Envelope.MaxY;
                geo.MaxZ = multiPoint.Envelope.MaxZ;
                geo.MinX = multiPoint.Envelope.MinX;
                geo.MinY = multiPoint.Envelope.MinY;
                geo.MinZ = multiPoint.Envelope.MinZ;
            }
            geo.GeometryType = multiPoint.GeometryType;
            geo.IsEmpty = multiPoint.IsEmpty;
            geo.IsValid = multiPoint.IsValid;
            geo.VertexAttribute = multiPoint.VertexAttribute;
            geo.HasId = multiPoint.HasId();
            geo.HasM = multiPoint.HasM();
            geo.HasZ = multiPoint.HasZ();
            // GeometryCollection属性
            geo.GeometryCount = multiPoint.GeometryCount;
            geo.IsOverlap = multiPoint.IsOverlap;
            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region MultiPolyline
        private void btnConstructMultiPolyline_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();
            multiPolyline = gfactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolyline;
            if (multiPolyline == null)
                return;

            // 添加两条Polyline
            point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            polyline = gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            int length = 100;
            for (int i = 0; i < length; i++)
            {
                point.SetCoords(i, 2 * i, 0, 0, i + 1);
                polyline.AppendPoint(point);
            }
            IPolyline polylineClone = polyline.Clone() as IPolyline;
            polylineClone.RemovePoints(50, 50);

            multiPolyline.AddPolyline(polyline);
            multiPolyline.AddPolyline(polylineClone);

            MultiCurve geo = new MultiCurve();
            // Geometry属性
            geo.Dimension = multiPolyline.Dimension;
            if (multiPolyline.Envelope != null)
            {
                geo.MaxX = multiPolyline.Envelope.MaxX;
                geo.MaxY = multiPolyline.Envelope.MaxY;
                geo.MaxZ = multiPolyline.Envelope.MaxZ;
                geo.MinX = multiPolyline.Envelope.MinX;
                geo.MinY = multiPolyline.Envelope.MinY;
                geo.MinZ = multiPolyline.Envelope.MinZ;
            }
            geo.GeometryType = multiPolyline.GeometryType;
            geo.IsEmpty = multiPolyline.IsEmpty;
            geo.IsValid = multiPolyline.IsValid;
            geo.VertexAttribute = multiPolyline.VertexAttribute;
            geo.HasId = multiPolyline.HasId();
            geo.HasM = multiPolyline.HasM();
            geo.HasZ = multiPolyline.HasZ();
            // GeometryCollection属性
            geo.GeometryCount = multiPolyline.GeometryCount;
            geo.IsOverlap = multiPolyline.IsOverlap;
            // MultiCurve属性
            geo.Length = multiPolyline.Length;

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region MultiPolygon
        private void btnConstructMultiPolygon_Click(object sender, EventArgs e)
        {
            if (gfactory == null)
                gfactory = new GeometryFactory();
            multiPolygon = gfactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
            if (multiPolygon == null)
                return;

            //添加第一个polygon
            IPolygon polygonFirst = gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            if (polygonFirst == null)
                return;
            /// 外环顺时针：1-4-3-2-1
            ///  4-------3
            ///  |       |
            ///  |       |
            ///  1-------2
            IRing exteriorRing = polygonFirst.ExteriorRing;
            point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            point.SetCoords(0, 0, 0, 0, 1);
            exteriorRing.AppendPoint(point);
            point.SetCoords(0, 200, 0, 0, 2);
            exteriorRing.AppendPoint(point);
            point.SetCoords(100, 200, 0, 0, 3);
            exteriorRing.AppendPoint(point);           
            point.SetCoords(100, 0, 0, 0, 4);
            exteriorRing.AppendPoint(point);
            point.SetCoords(0, 0, 0, 0, 5);
            exteriorRing.AppendPoint(point);  //闭合
            /// 内环逆时针：1-2-3-4-1
            ///  4-------3
            ///  |       |
            ///  |       |
            ///  1-------2
            IRing interiorRing = gfactory.CreateGeometry(gviGeometryType.gviGeometryRing, gviVertexAttribute.gviVertexAttributeZ) as IRing;
            point.SetCoords(25, 25, 0, 0, 1);
            interiorRing.AppendPoint(point);
            point.SetCoords(75, 25, 0, 0, 2);
            interiorRing.AppendPoint(point);
            point.SetCoords(75, 75, 0, 0, 3);
            interiorRing.AppendPoint(point);
            point.SetCoords(25, 75, 0, 0, 4);
            interiorRing.AppendPoint(point);
            point.SetCoords(25, 25, 0, 0, 5);
            interiorRing.AppendPoint(point);  //闭合
            polygonFirst.AddInteriorRing(interiorRing);

            //Clone一个新的内环
            IRing pInteriorRingNew = interiorRing.Clone() as IRing;
            polygonFirst.AddInteriorRing(pInteriorRingNew);
            multiPolygon.AddPolygon(polygonFirst);

            //Clone一个新的polygon
            IPolygon pPolygonNew = polygonFirst.Clone() as IPolygon;
            multiPolygon.AddPolygon(pPolygonNew);

            MultiSurface geo = new MultiSurface();
            // Geometry属性
            geo.Dimension = multiPolygon.Dimension;
            if (multiPolygon.Envelope != null)
            {
                geo.MaxX = multiPolygon.Envelope.MaxX;
                geo.MaxY = multiPolygon.Envelope.MaxY;
                geo.MaxZ = multiPolygon.Envelope.MaxZ;
                geo.MinX = multiPolygon.Envelope.MinX;
                geo.MinY = multiPolygon.Envelope.MinY;
                geo.MinZ = multiPolygon.Envelope.MinZ;
            }
            geo.GeometryType = multiPolygon.GeometryType;
            geo.IsEmpty = multiPolygon.IsEmpty;
            geo.IsValid = multiPolygon.IsValid;
            geo.VertexAttribute = multiPolygon.VertexAttribute;
            geo.HasId = multiPolygon.HasId();
            geo.HasM = multiPolygon.HasM();
            geo.HasZ = multiPolygon.HasZ();
            // GeometryCollection属性
            geo.GeometryCount = multiPolygon.GeometryCount;
            geo.IsOverlap = multiPolygon.IsOverlap;
            // MultiSurface属性
            geo.Area = multiPolygon.GetArea();

            this.propertyGrid1.SelectedObject = geo;
        }
        #endregion

        #region MultiTriMesh
        private void btnConstructMultiTriMesh_Click(object sender, EventArgs e)
        {
            try
            {
                if (gfactory == null)
                    gfactory = new GeometryFactory();
                multiTriMesh = gfactory.CreateGeometry(gviGeometryType.gviGeometryMultiTrimesh, gviVertexAttribute.gviVertexAttributeZ) as IMultiTriMesh;
                if (multiTriMesh == null)
                    return;

                triMesh = gfactory.CreateGeometry(gviGeometryType.gviGeometryTriMesh, gviVertexAttribute.gviVertexAttributeZ) as ITriMesh;
                if (triMesh == null)
                    return;
                ArrayList vertices = new ArrayList();  //ITopoNode数组
                point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                for (int i = 0; i < 17; ++i)
                {
                    point.SetCoords(i, i, i, 0, 0);
                    vertices.Add(triMesh.AddPoint(point));
                }
                triMesh.AddTriangle(vertices[0] as ITopoNode, vertices[1] as ITopoNode, vertices[2] as ITopoNode);
                triMesh.AddTriangle(vertices[2] as ITopoNode, vertices[1] as ITopoNode, vertices[3] as ITopoNode);
                triMesh.AddTriangle(vertices[3] as ITopoNode, vertices[1] as ITopoNode, vertices[4] as ITopoNode);
                triMesh.AddTriangle(vertices[3] as ITopoNode, vertices[4] as ITopoNode, vertices[5] as ITopoNode);
                triMesh.AddTriangle(vertices[5] as ITopoNode, vertices[4] as ITopoNode, vertices[6] as ITopoNode);
                triMesh.AddTriangle(vertices[5] as ITopoNode, vertices[6] as ITopoNode, vertices[7] as ITopoNode);
                triMesh.AddTriangle(vertices[7] as ITopoNode, vertices[6] as ITopoNode, vertices[8] as ITopoNode);
                triMesh.AddTriangle(vertices[7] as ITopoNode, vertices[8] as ITopoNode, vertices[9] as ITopoNode);
                triMesh.AddTriangle(vertices[7] as ITopoNode, vertices[9] as ITopoNode, vertices[10] as ITopoNode);
                triMesh.AddTriangle(vertices[10] as ITopoNode, vertices[9] as ITopoNode, vertices[11] as ITopoNode);
                triMesh.AddTriangle(vertices[10] as ITopoNode, vertices[11] as ITopoNode, vertices[12] as ITopoNode);
                triMesh.AddTriangle(vertices[13] as ITopoNode, vertices[10] as ITopoNode, vertices[12] as ITopoNode);
                triMesh.AddTriangle(vertices[13] as ITopoNode, vertices[14] as ITopoNode, vertices[10] as ITopoNode);
                triMesh.AddTriangle(vertices[13] as ITopoNode, vertices[15] as ITopoNode, vertices[14] as ITopoNode);
                triMesh.AddTriangle(vertices[15] as ITopoNode, vertices[16] as ITopoNode, vertices[14] as ITopoNode);
                triMesh.AddTriangle(vertices[16] as ITopoNode, vertices[5] as ITopoNode, vertices[14] as ITopoNode);
                triMesh.AddTriangle(vertices[3] as ITopoNode, vertices[5] as ITopoNode, vertices[16] as ITopoNode);
                triMesh.AddTriangle(vertices[2] as ITopoNode, vertices[3] as ITopoNode, vertices[16] as ITopoNode);

                multiTriMesh.AddGeometry(triMesh);

                MultiSurface geo = new MultiSurface();
                // Geometry属性
                geo.Dimension = multiTriMesh.Dimension;
                if (multiTriMesh.Envelope != null)
                {
                    geo.MaxX = multiTriMesh.Envelope.MaxX;
                    geo.MaxY = multiTriMesh.Envelope.MaxY;
                    geo.MaxZ = multiTriMesh.Envelope.MaxZ;
                    geo.MinX = multiTriMesh.Envelope.MinX;
                    geo.MinY = multiTriMesh.Envelope.MinY;
                    geo.MinZ = multiTriMesh.Envelope.MinZ;
                }
                geo.GeometryType = multiTriMesh.GeometryType;
                geo.IsEmpty = multiTriMesh.IsEmpty;
                geo.IsValid = multiTriMesh.IsValid;
                geo.VertexAttribute = multiTriMesh.VertexAttribute;
                geo.HasId = multiTriMesh.HasId();
                geo.HasM = multiTriMesh.HasM();
                geo.HasZ = multiTriMesh.HasZ();
                // GeometryCollection属性
                geo.GeometryCount = multiTriMesh.GeometryCount;
                geo.IsOverlap = multiTriMesh.IsOverlap;
                // MultiSurface属性
                geo.Area = multiTriMesh.GetArea();

                this.propertyGrid1.SelectedObject = geo;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        #endregion

    }
}
