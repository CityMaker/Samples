using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Resource;
using Gvitech;
using System;

namespace CreateFixedBillboard
{
    public partial class MainForm : Form
    {
        private int flag = -1;  // 标记"Samples"文件夹在目录中的索引号
        RenderControl g = null;
        private IGeometryFactory gfactory = null;
        private IOverlayLabel label = null;
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

        //private ILabel label = null;
        private ITextSymbol textSymbol = null;
        //private TextAttribute textAttribute = null;

        private CheckBox check = null;
        private CheckBox checkShowOutline = null;

        private System.Guid rootId = new System.Guid();
        private TextAttribute ta = new TextAttribute();

        public MainForm()
        {
            InitializeComponent();


            // 初始化RenderControl控件


            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;

            // 设置天空盒
            flag = Application.StartupPath.LastIndexOf("Samples");
            if (flag > -1)
            {
                string tmpSkyboxPath = Path.Combine(Application.StartupPath.Substring(0, flag), @"Samples\Media\skybox");
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
            //this.toolStripComboBoxObjectManager.SelectedIndex = 0;
            //this.toolStripComboBoxColor.SelectedIndex = 0;

            // 注册控件拾取事件
            this.axRenderControl1.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "CreateFixedBillboard.html";
            }
            label = this.axRenderControl1.ObjectManager.CreateOverlayLabel(rootId);
            label.Text = "伟景行";
            this.txtLabelText.Text = label.Text;
        }

        void axRenderControl1_RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e)
        {
            if (e.pickResult.Type == gviObjectType.gviObjectLabel)
            {
                ILabelPickResult tlpr = e.pickResult as ILabelPickResult;
                gviObjectType type = tlpr.Type;
                ILabel fl = tlpr.Label;
                MessageBox.Show("拾取到" + type + "类型，内容为" + fl.Text);
            }
            else if (e.pickResult.Type == gviObjectType.gviObjectRenderModelPoint)
            {
                IRenderModelPointPickResult tlpr = e.pickResult as IRenderModelPointPickResult;
                gviObjectType type = tlpr.Type;
                IRenderModelPoint fl = tlpr.ModelPoint;
                MessageBox.Show("拾取到" + type + "类型，模型名称为" + fl.ModelName);
            }
            else if (e.pickResult.Type == gviObjectType.gviObjectRenderPoint)
            {
                IRenderPointPickResult tlpr = e.pickResult as IRenderPointPickResult;
                gviObjectType type = tlpr.Type;
                IRenderPoint fl = tlpr.Point;
                MessageBox.Show("拾取到" + type + "类型，大小为" + fl.Symbol.Size);
            }
            else if (e.pickResult.Type == gviObjectType.gviObjectRenderPolyline)
            {
                IRenderPolylinePickResult tlpr = e.pickResult as IRenderPolylinePickResult;
                gviObjectType type = tlpr.Type;
                IRenderPolyline fl = tlpr.Polyline;
                MessageBox.Show("拾取到" + type + "类型，GUID为" + fl.Guid);
            }
            else if (e.pickResult.Type == gviObjectType.gviObjectRenderPolygon)
            {
                IRenderPolygonPickResult tlpr = e.pickResult as IRenderPolygonPickResult;
                gviObjectType type = tlpr.Type;
                IRenderPolygon fl = tlpr.Polygon;
                MessageBox.Show("拾取到" + type + "类型，GUID为" + fl.Guid);
            }
            else if (e.pickResult.Type == gviObjectType.gviObjectRenderPOI)
            {
                IRenderPOIPickResult tlpr = e.pickResult as IRenderPOIPickResult;
                gviObjectType type = tlpr.Type;
                IRenderPOI fl = tlpr.POI;
                MessageBox.Show("拾取到" + type + "类型，名称为" + ((IPOI)fl.GetFdeGeometry()).Name);
            }
            else if (e.pickResult.Type == gviObjectType.gviObjectReferencePlane)
            {
                //ta = new TextAttribute();
                 ta.TextSize =  Convert.ToInt32(this.toolstripFontSize.Text.ToString());
               //  ta.TextColor = olec;
                IImage image = null;
                IModel model = null;
                string imageName = "";
                this.axRenderControl1.Utility.CreateFixedBillboard(label.Text, ta, 50, 100, true, out model, out image, out imageName);
                this.axRenderControl1.ObjectManager.AddModel("fixedModel", model);
                this.axRenderControl1.ObjectManager.AddImage(imageName, image);

                if (gfactory == null)
                    gfactory = new GeometryFactoryClass();
                fde_modelpoint = gfactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint,
                    gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                fde_modelpoint.SetCoords(e.intersectPoint.X, e.intersectPoint.Y, e.intersectPoint.Z, 0, 0);
                fde_modelpoint.ModelName = "fixedModel";
                rmodelpoint = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(fde_modelpoint, null, rootId);
                rmodelpoint.MaxVisibleDistance = double.MaxValue;
                rmodelpoint.MinVisiblePixels = 0;
                rmodelpoint.ShowOutline = checkShowOutline.Checked;
                IEulerAngle angle = new EulerAngle();
                angle.Set(0, -20, 0);
                this.axRenderControl1.Camera.LookAt(e.intersectPoint.Position, 100, angle);




            }
        }
        private void MainForm_Load(object sender, System.EventArgs e)
        {
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
        private void txtLabelText_TextChanged(object sender, System.EventArgs e)
        {
            label.Text = this.txtLabelText.Text;
        }


        private void btnChangeColor_Click(object sender, System.EventArgs e)
        {
            this.colorDialog1.Color = Utils.HexNumberToColor(colorBox.Text);
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                uint olec = (uint)(this.colorDialog1.Color.A << 24 | this.colorDialog1.Color.R << 16 | this.colorDialog1.Color.G << 8 | this.colorDialog1.Color.B);
                this.colorBox.Text = olec.ToString("X");
                //ta.TextColor = this.colorBox.Text;
                ta.TextColor = olec; 
            }
        }

        private void fontsize_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void fontsize_TextChanged(object sender, System.EventArgs e)
        {
           // ta.TextSize = long.Parse(this.toolstripFontSize.Text);
            //ta.TextSize = Convert.ToInt32(this.toolstripFontSize.Text.ToString());
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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
                        this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamOutlineColor, 0xffff0000);
                        break;
                    case 1:
                        this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamOutlineColor, 0xffffff00);
                        break;
                    case 2:
                        this.axRenderControl1.SetRenderParam(gviRenderControlParameters.gviRenderParamOutlineColor, 0xff0000ff);
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
