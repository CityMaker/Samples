using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;

namespace OverlayLabel
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEnvelope env;//加载数据时，初始化的矩形范围

        private IOverlayLabel label = null;
        private IPoint point = null;
        private IPickResult pic = null;
        private System.Guid rootId = new System.Guid();

        private void init()
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);

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

            #region 加载FDB场景
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\SDKDEMO.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                fcMap = new Hashtable(fcnames.Length);
                foreach (string name in fcnames)
                {
                    IFeatureClass fc = dataset.OpenFeatureClass(name);
                    // 找到空间列字段
                    List<string> geoNames = new List<string>();
                    IFieldInfoCollection fieldinfos = fc.GetFields();
                    for (int i = 0; i < fieldinfos.Count; i++)
                    {
                        IFieldInfo fieldinfo = fieldinfos.Get(i);
                        if (null == fieldinfo)
                            continue;
                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        if (null == geometryDef)
                            continue;
                        geoNames.Add(fieldinfo.Name);
                    }
                    fcMap.Add(fc, geoNames);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            // CreateFeautureLayer
            bool hasfly = false;
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    if (!geoName.Equals("Geometry"))
                        continue;

                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                    fc, geoName, null, null, rootId);

                    if (!hasfly)
                    {
                        IFieldInfoCollection fieldinfos = fc.GetFields();
                        IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion 
        
            //this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectOverlayLabel;
            //this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            //this.axRenderControl1.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            this.axRenderControl1.RcMouseHover += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseHoverEventHandler(axRenderControl1_RcMouseHover);
        }

        bool axRenderControl1_RcMouseHover(uint Flags, int X, int Y)
        {
            pic = this.axRenderControl1.Camera.ScreenToWorld(X,Y, out point);
            if (point == null || pic == null)
                return false;
            IOverlayLabelPickResult pr = pic as IOverlayLabelPickResult;
            IOverlayLabel label = pr.OverlayLabel;
            this.Text = label.Text;
            return false;
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            if (PickResult == null) return;
            IOverlayLabelPickResult pr = PickResult as IOverlayLabelPickResult;
            IOverlayLabel label = pr.OverlayLabel;
            this.Text = label.Text;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            init();

            label = this.axRenderControl1.ObjectManager.CreateOverlayLabel(rootId);
            label.Text = "伟景行";

            #region 获取默认值
            this.txtLabelText.Text = label.Text;
            switch (label.Alignment)
            {
                case gviPivotAlignment.gviPivotAlignBottomCenter:
                    comboBox1.SelectedIndex = 8;
                    break;
                case gviPivotAlignment.gviPivotAlignBottomLeft:
                    comboBox1.SelectedIndex = 1;
                    break;
                case gviPivotAlignment.gviPivotAlignBottomRight:
                    comboBox1.SelectedIndex = 4;
                    break;
                case gviPivotAlignment.gviPivotAlignCenterCenter:
                    comboBox1.SelectedIndex = 7;
                    break;
                case gviPivotAlignment.gviPivotAlignCenterLeft:
                    comboBox1.SelectedIndex = 2;
                    break;
                case gviPivotAlignment.gviPivotAlignCenterRight:
                    comboBox1.SelectedIndex = 5;
                    break;
                case gviPivotAlignment.gviPivotAlignTopCenter:
                    comboBox1.SelectedIndex = 6;
                    break;
                case gviPivotAlignment.gviPivotAlignTopLeft:
                    comboBox1.SelectedIndex = 0;
                    break;
                case gviPivotAlignment.gviPivotAlignTopRight:
                    comboBox1.SelectedIndex = 3;
                    break;
            }
            this.txtImageName.Text = label.ImageName;
            this.numRotation.Value = decimal.Parse(label.Rotation.ToString());
            this.numDepth.Value = decimal.Parse(label.Depth.ToString());
            this.txtX.Text = label.GetX().ToString();
            this.txtY.Text = label.GetY().ToString();
            this.txtWidth.Text = label.GetWidth().ToString();
            this.txtHeight.Text = label.GetHeight().ToString();
            #endregion

            // 自定义右下角Logo
            IOverlayLabel logoLabel = this.axRenderControl1.ObjectManager.CreateOverlayLabel(rootId);
            string imageNamePath = (strMediaPath + @"\bmp\Tulips.bmp");
            logoLabel.ImageName = imageNamePath;
            logoLabel.SetWidth(128, 0, 0);
            logoLabel.SetHeight(96, 0, 0);
            logoLabel.SetX(-logoLabel.GetWidth() / 2, 1.0f, 0);
            logoLabel.SetY(logoLabel.GetHeight() / 2, 0, 0);
            this.axRenderControl1.Viewport.LogoVisible = false;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "OverlayLabel.html";
            }
        }

        private void txtLabelText_TextChanged(object sender, System.EventArgs e)
        {
            label.Text = this.txtLabelText.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (this.comboBox1.SelectedItem.ToString())
            {
                case "左上":
                    label.Alignment = gviPivotAlignment.gviPivotAlignTopLeft;
                    break;
                case "左下":
                    label.Alignment = gviPivotAlignment.gviPivotAlignBottomLeft;
                    break;
                case "左中":
                    label.Alignment = gviPivotAlignment.gviPivotAlignCenterLeft;
                    break;
                case "右上":
                    label.Alignment = gviPivotAlignment.gviPivotAlignTopRight;
                    break;
                case "右下":
                    label.Alignment = gviPivotAlignment.gviPivotAlignBottomRight;
                    break;
                case "右中":
                    label.Alignment = gviPivotAlignment.gviPivotAlignCenterRight;
                    break;
                case "中上":
                    label.Alignment = gviPivotAlignment.gviPivotAlignTopCenter;
                    break;
                case "中下":
                    label.Alignment = gviPivotAlignment.gviPivotAlignBottomCenter;
                    break;
                case "正中":
                    label.Alignment = gviPivotAlignment.gviPivotAlignCenterCenter;
                    break;
            }
        }

        private void linkLabel_ImageName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "所有文件(*.*)|*.*|bmp文件(*.bmp)|*.bmp";
            if(System.IO.Directory.Exists(strMediaPath))
            {
                od.InitialDirectory = strMediaPath + @"\bmp";
            }
            od.RestoreDirectory = true;
            if (DialogResult.OK == od.ShowDialog())
            {
                label.ImageName = od.FileName;
                this.txtImageName.Text = od.FileName;
            }
        }

        private void numRotation_ValueChanged(object sender, System.EventArgs e)
        {
            label.Rotation = float.Parse(numRotation.Value.ToString());
        }

        private void numDepth_ValueChanged(object sender, System.EventArgs e)
        {
            label.Depth = float.Parse(numDepth.Value.ToString());
        }

        private void linkLabel_X_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SettingForm form = new SettingForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                label.SetX(form.Offset, form.WindowWidthRatio, form.WindowHightRatio);
                txtX.Text = label.GetX().ToString();
            }
        }

        private void linkLabel_Y_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SettingForm form = new SettingForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                label.SetY(form.Offset, form.WindowWidthRatio, form.WindowHightRatio);
                txtY.Text = label.GetY().ToString();
            }
        }

        private void linkLabel_Width_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SettingForm form = new SettingForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                label.SetWidth(form.Offset, form.WindowWidthRatio, form.WindowHightRatio);
                txtWidth.Text = label.GetWidth().ToString();
            }
        }

        private void linkLabel_Height_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SettingForm form = new SettingForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                label.SetHeight(form.Offset, form.WindowWidthRatio, form.WindowHightRatio);
                txtHeight.Text = label.GetHeight().ToString();
            }
        }

        private void btnFullScreenLabel_Click(object sender, System.EventArgs e)
        {
            label.SetX(0, 0.5f, 0);
            label.SetY(0, 0, 0.5f);
            label.SetWidth(0, 1f, 0);
            label.SetHeight(0, 0, 1f);
            txtX.Text = label.GetX().ToString();
            txtY.Text = label.GetY().ToString();
            txtWidth.Text = label.GetWidth().ToString();
            txtHeight.Text = label.GetHeight().ToString();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            if (label == null)
                return;

            txtX.Text = label.GetX().ToString();
            txtY.Text = label.GetY().ToString();
            txtWidth.Text = label.GetWidth().ToString();
            txtHeight.Text = label.GetHeight().ToString();
        }
    
    }
}
