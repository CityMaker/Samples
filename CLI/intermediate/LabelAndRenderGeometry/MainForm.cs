using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Resource;
using System.Collections;
using Gvitech.CityMaker.Controls;

namespace LabelAndRenderGeometry
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        private IGeometryFactory gfactory = null;

        private IModelPoint fde_modelpoint = null;
        private IRenderModelPoint rmodelpoint = null;
        
        private IPoint fde_point = null;
        private IRenderPoint rpoint = null;
        
        private IPolyline fde_polyline = null;
        private IRenderPolyline rpolyline = null;
        
        private IPolygon fde_polygon = null;
        private IRenderPolygon rpolygon = null;

        private IPOI fde_poi = null;
        private IRenderPOI rpoi = null;
        private int poiCount = 0;

        private ISimplePointSymbol pointSymbol = null;
        private ISurfaceSymbol surfaceSymbol = null;
        private ICurveSymbol lineSymbol = null;

        private ILabel label = null;
        private ITextSymbol textSymbol = null;
        private TextAttribute textAttribute = null;

        private CheckBox check = null;
        private CheckBox checkShowOutline = null;

        private System.Guid rootId = new System.Guid();

        // 线程转发
        public int MainThreadId = 0;
        

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             

            check = new CheckBox();
            check.Text = "进入漫游模式";
            check.Width = 80;
            check.Checked = false;
            check.CheckedChanged += new System.EventHandler(check_CheckedChanged);
            ToolStripControlHost host = new ToolStripControlHost(check);
            toolStrip1.Items.Add(host);

            checkShowOutline = new CheckBox();
            checkShowOutline.Text = "显示外轮廓线";
            checkShowOutline.Width = 80;
            checkShowOutline.Checked = false;
            checkShowOutline.CheckedChanged += new System.EventHandler(checkShowOutline_CheckedChanged);
            ToolStripControlHost hostShowOutline = new ToolStripControlHost(checkShowOutline);
            toolStrip1.Items.Add(hostShowOutline);
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);
            this.axRenderControl1.Camera.FlyTime = 1;

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;

            // 设置天空盒
            
            if(System.IO.Directory.Exists(strMediaPath))
            {
                string tmpSkyboxPath = strMediaPath + @"\skybox";
                ISkyBox skybox = this.axRenderControl1.ObjectManager.GetSkyBox(0);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\1_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\1_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\1_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\1_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\1_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\1_UP.jpg");   
            }
            else
            {
                MessageBox.Show("请不要随意更改SDK目录名");
                return;
            }

            // 下拉框控件默认选中第一项
            this.toolStripComboBoxObjectManager.SelectedIndex = 0;
            this.toolStripComboBoxColor.SelectedIndex = 0;

            // 注册控件拾取事件
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "LabelAndRenderGeometry.html";
            }    
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (PickResult.Type == gviObjectType.gviObjectLabel)
            {
                ILabelPickResult tlpr = PickResult as ILabelPickResult;
                gviObjectType type = tlpr.Type;
                ILabel fl = tlpr.Label;
                MessageBox.Show("拾取到" + type + "类型，内容为" + fl.Text);
            }
            else if (PickResult.Type == gviObjectType.gviObjectRenderModelPoint)
            {
                IRenderModelPointPickResult tlpr = PickResult as IRenderModelPointPickResult;
                gviObjectType type = tlpr.Type;
                IRenderModelPoint fl = tlpr.ModelPoint;
                MessageBox.Show("拾取到" + type + "类型，模型名称为" + fl.ModelName);
            }
            else if (PickResult.Type == gviObjectType.gviObjectRenderPoint)
            {
                IRenderPointPickResult tlpr = PickResult as IRenderPointPickResult;
                gviObjectType type = tlpr.Type;
                IRenderPoint fl = tlpr.Point;
                MessageBox.Show("拾取到" + type + "类型，大小为" + fl.Symbol.Size);
            }
            else if (PickResult.Type == gviObjectType.gviObjectRenderPolyline)
            {
                IRenderPolylinePickResult tlpr = PickResult as IRenderPolylinePickResult;
                gviObjectType type = tlpr.Type;
                IRenderPolyline fl = tlpr.Polyline;
                MessageBox.Show("拾取到" + type + "类型，GUID为" + fl.Guid);
            }
            else if (PickResult.Type == gviObjectType.gviObjectRenderPolygon)
            {
                IRenderPolygonPickResult tlpr = PickResult as IRenderPolygonPickResult;
                gviObjectType type = tlpr.Type;
                IRenderPolygon fl = tlpr.Polygon;
                MessageBox.Show("拾取到" + type + "类型，GUID为" + fl.Guid);
            }
            else if (PickResult.Type == gviObjectType.gviObjectRenderPOI)
            {
                IRenderPOIPickResult tlpr = PickResult as IRenderPOIPickResult;
                gviObjectType type = tlpr.Type;
                IRenderPOI fl = tlpr.POI;
                MessageBox.Show("拾取到" + type + "类型，名称为" + ((IPOI)fl.GetFdeGeometry()).Name);
            }
            else if (PickResult.Type == gviObjectType.gviObjectTerrainRegularPolygon)
            {
                ITerrainRegularPolygonPickResult regPolygonPick = PickResult as ITerrainRegularPolygonPickResult;
                gviObjectType type = regPolygonPick.Type;
                ITerrainRegularPolygon regPolygon = regPolygonPick.TerrainRegularPolygon;
                MessageBox.Show("拾取到" + type + "类型，geometryCount为" + regPolygon.GetFdeGeometry().GeometryType);
            }
            else if (PickResult.Type == gviObjectType.gviObjectTerrainArrow)
            {
                ITerrainArrowPickResult arrowPickResult = PickResult as ITerrainArrowPickResult;
                gviObjectType type = arrowPickResult.Type;
                ITerrainArrow arrow = arrowPickResult.TerrainArrow;
                MessageBox.Show("拾取到" + type + "类型，geometryType" + arrow.GetFdeGeometry().GeometryType);
            }
            else if (PickResult.Type == gviObjectType.gviObjectReferencePlane)
            {
                switch (this.toolStripComboBoxObjectManager.Text)
                {
                    case "CreateLabel":
                        {
                            label = this.axRenderControl1.ObjectManager.CreateLabel(rootId);
                            label.Text = "我是testlabel";
                            label.Position = IntersectPoint;
                            textSymbol = new TextSymbol();
                            textAttribute = new TextAttribute();
                            textAttribute.TextColor = System.Drawing.Color.Yellow;
                            textAttribute.TextSize = 20;
                            textAttribute.Underline = true;
                            textAttribute.Font = "楷体";
                            textSymbol.TextAttribute = textAttribute;
                            textSymbol.VerticalOffset = 10;
                            textSymbol.DrawLine = true;
                            textSymbol.MarginColor = System.Drawing.Color.Yellow;
                            label.TextSymbol = textSymbol;
                            this.axRenderControl1.Camera.FlyToObject(label.Guid, gviActionCode.gviActionFlyTo);
                        }
                        break;
                    case "CreateRenderModelPoint":
                        {
                            if (gfactory == null)
                                gfactory = new GeometryFactory();

                            string tmpOSGPath = (strMediaPath + @"\osg\Buildings\Apartment\Apartment.osg");
                            fde_modelpoint = gfactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint,
                                gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                            fde_modelpoint.SetCoords(IntersectPoint.X, IntersectPoint.Y, IntersectPoint.Z, 0, 0);
                            fde_modelpoint.ModelName = tmpOSGPath;
                            rmodelpoint = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(fde_modelpoint, null, rootId);
                            rmodelpoint.MaxVisibleDistance = double.MaxValue;
                            rmodelpoint.MinVisiblePixels = 0;
                            rmodelpoint.ShowOutline = checkShowOutline.Checked;
                            IEulerAngle angle = new EulerAngle();
                            angle.Set(0, -20, 0);
                            this.axRenderControl1.Camera.LookAt(IntersectPoint.Position, 100, angle);
                        }
                        break;
                    case "CreateRenderPoint":
                        {
                            if (gfactory == null)
                                gfactory = new GeometryFactory();

                            fde_point = (IPoint)gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint,
                                gviVertexAttribute.gviVertexAttributeZ);
                            fde_point.SetCoords(IntersectPoint.X, IntersectPoint.Y, IntersectPoint.Z, 0, 0);

                            pointSymbol = new SimplePointSymbol();
                            pointSymbol.FillColor = System.Drawing.Color.Red;
                            pointSymbol.Size = 10;
                            rpoint = this.axRenderControl1.ObjectManager.CreateRenderPoint(fde_point, pointSymbol, rootId);
                            rpoint.ShowOutline = checkShowOutline.Checked;
                            this.axRenderControl1.Camera.FlyToObject(rpoint.Guid, gviActionCode.gviActionFlyTo);
                        }
                        break;
                    case "CreateRenderPolyline":
                        {
                            if (gfactory == null)
                                gfactory = new GeometryFactory();

                            fde_polyline = (IPolyline)gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                                gviVertexAttribute.gviVertexAttributeZ);
                            fde_point = (IPoint)gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint,
                                gviVertexAttribute.gviVertexAttributeZ);
                            fde_point.SetCoords(IntersectPoint.X, IntersectPoint.Y, IntersectPoint.Z, 0, 0);
                            fde_polyline.AppendPoint(fde_point);
                            fde_point.SetCoords(IntersectPoint.X + 20, IntersectPoint.Y, IntersectPoint.Z, 0, 0);
                            fde_polyline.AppendPoint(fde_point);
                            fde_point.SetCoords(IntersectPoint.X + 20, IntersectPoint.Y + 20, IntersectPoint.Z, 0, 0);
                            fde_polyline.AppendPoint(fde_point);
                            fde_point.SetCoords(IntersectPoint.X + 20, IntersectPoint.Y + 20, IntersectPoint.Z + 20, 0, 0);
                            fde_polyline.AppendPoint(fde_point);

                            lineSymbol = new CurveSymbol();
                            lineSymbol.Color = System.Drawing.Color.Red;  // 紫红色
                            rpolyline = this.axRenderControl1.ObjectManager.CreateRenderPolyline(fde_polyline, lineSymbol, rootId);
                            rpolyline.ShowOutline = checkShowOutline.Checked;
                            this.axRenderControl1.Camera.FlyToObject(rpolyline.Guid, gviActionCode.gviActionFlyTo);
                        }
                        break;
                    case "CreateRenderPolygon":
                        {
                            if (gfactory == null)
                                gfactory = new GeometryFactory();

                            fde_polygon = (IPolygon)gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                                gviVertexAttribute.gviVertexAttributeZ);

                            fde_point = (IPoint)gfactory.CreateGeometry(gviGeometryType.gviGeometryPoint,
                                gviVertexAttribute.gviVertexAttributeZ);
                            fde_point.SetCoords(IntersectPoint.X, IntersectPoint.Y, IntersectPoint.Z, 0, 0);
                            fde_polygon.ExteriorRing.AppendPoint(fde_point);
                            fde_point.SetCoords(IntersectPoint.X + 10, IntersectPoint.Y, IntersectPoint.Z, 0, 0);
                            fde_polygon.ExteriorRing.AppendPoint(fde_point);
                            fde_point.SetCoords(IntersectPoint.X + 10, IntersectPoint.Y + 10, IntersectPoint.Z, 0, 0);
                            fde_polygon.ExteriorRing.AppendPoint(fde_point);
                            fde_point.SetCoords(IntersectPoint.X, IntersectPoint.Y + 10, IntersectPoint.Z, 0, 0);
                            fde_polygon.ExteriorRing.AppendPoint(fde_point);

                            surfaceSymbol = new SurfaceSymbol();
                            surfaceSymbol.Color = System.Drawing.Color.Blue;  // 蓝色
                            rpolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(fde_polygon, surfaceSymbol, rootId);
                            rpolygon.ShowOutline = checkShowOutline.Checked;
                            this.axRenderControl1.Camera.FlyToObject(rpolygon.Guid, gviActionCode.gviActionFlyTo);
                        }
                        break;
                    case "CreateRenderPOI":
                        {
                            if (gfactory == null)
                                gfactory = new GeometryFactory();

                            fde_poi = (IPOI)gfactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ);
                            fde_poi.SetCoords(IntersectPoint.X, IntersectPoint.Y, IntersectPoint.Z, 0, 0);
                            fde_poi.ImageName = "#(1)";
                            fde_poi.Name = (++poiCount).ToString();
                            fde_poi.Size = 50;
                            rpoi = this.axRenderControl1.ObjectManager.CreateRenderPOI(fde_poi);
                            rpoi.ShowOutline = checkShowOutline.Checked;
                            this.axRenderControl1.Camera.FlyToObject(rpoi.Guid, gviActionCode.gviActionFlyTo);
                        }
                        break;
                    case "CreateFixedBillboard":
                        {
                            TextAttribute ta = new TextAttribute();
                            ta.TextSize = 10;
                            ta.TextColor = System.Drawing.Color.Yellow;
                            IImage image = null;
                            IModel model = null;
                            string imageName = "";
                            this.axRenderControl1.Utility.CreateFixedBillboard("I'm fixed billboard!", ta, 50, 100, true, out model, out image, out imageName);
                            this.axRenderControl1.ObjectManager.AddModel("fixedModel", model);
                            this.axRenderControl1.ObjectManager.AddImage(imageName, image);    

                            if (gfactory == null)
                                gfactory = new GeometryFactory();
                            fde_modelpoint = gfactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint,
                                gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                            fde_modelpoint.SetCoords(IntersectPoint.X, IntersectPoint.Y, IntersectPoint.Z, 0, 0);
                            fde_modelpoint.ModelName = "fixedModel";
                            rmodelpoint = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(fde_modelpoint, null, rootId);
                            rmodelpoint.MaxVisibleDistance = double.MaxValue;
                            rmodelpoint.MinVisiblePixels = 0;
                            rmodelpoint.ShowOutline = checkShowOutline.Checked;
                            IEulerAngle angle = new EulerAngle();
                            angle.Set(0, -20, 0);
                            this.axRenderControl1.Camera.LookAt(IntersectPoint.Position, 100, angle);
                        }
                        break;
                    case "CreateRegularPolygon":
                        {
                            IPosition pos = new Position();
                            pos.X = IntersectPoint.X;
                            pos.Y = IntersectPoint.Y;
                            pos.Altitude = IntersectPoint.Z;
                            ITerrainRegularPolygon regPolygon = this.axRenderControl1.ObjectManager.CreateRegularPolygon(pos, 10, 10, System.Drawing.Color.Red, System.Drawing.Color.White, rootId);
                            this.axRenderControl1.Camera.FlyToObject(regPolygon.Guid, gviActionCode.gviActionFlyTo);
                        }
                        break;
                    case "CreateArrow":
                        {
                            IPosition pos = new Position();
                            pos.X = IntersectPoint.X;
                            pos.Y = IntersectPoint.Y;
                            pos.Altitude = IntersectPoint.Z;
                            ITerrainArrow regArrow = this.axRenderControl1.ObjectManager.CreateArrow(pos, 30, 4, System.Drawing.Color.Red, System.Drawing.Color.White, rootId);
                            this.axRenderControl1.Camera.FlyToObject(regArrow.Guid, gviActionCode.gviActionFlyTo);
                        }
                        break;
                }
            }
        }



        void check_CheckedChanged(object sender, System.EventArgs e)
        {
            if (check.Checked)
            {
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            }
            else
            {
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            }
        }

        void checkShowOutline_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkShowOutline == null)
                return;

            if (checkShowOutline.Checked)
            {
                switch (toolStripComboBoxColor.SelectedIndex)
                {
                    case 0:
                        this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamOutlineColor, System.Drawing.Color.Yellow);
                        break;
                    case 1:
                        this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamOutlineColor, System.Drawing.Color.Yellow);
                        break;
                    case 2:
                        this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamOutlineColor, System.Drawing.Color.Red);
                        break;
                }
            }

            if (rmodelpoint != null)
                rmodelpoint.ShowOutline = checkShowOutline.Checked;
            if (rpoint != null)
                rpoint.ShowOutline = checkShowOutline.Checked;
            if (rpolyline != null)
                rpolyline.ShowOutline = checkShowOutline.Checked;
            if (rpolygon != null)
                rpolygon.ShowOutline = checkShowOutline.Checked;
            if (rpoi != null)
                rpoi.ShowOutline = checkShowOutline.Checked;
        }

    }
}
