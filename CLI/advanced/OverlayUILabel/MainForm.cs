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

namespace OverlayUILabel
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private IEnvelope env;//加载数据时，初始化的矩形范围
        private System.Guid rootId = System.Guid.Empty;
        private IEulerAngle ang = new EulerAngle();
        private IVector3 vec = new Vector3();

        private void init()
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;
            this.axRenderControl1.Camera.FlyTime = 1;

            // 设置天空盒
            
            if(System.IO.Directory.Exists(strMediaPath))
            {
                string tmpSkyboxPath = strMediaPath + @"\skybox";
                ISkyBox skybox = this.axRenderControl1.ObjectManager.GetSkyBox(0);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\13_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\13_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\13_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\13_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\13_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\13_UP.jpg");
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
            
        }


        private IUIDim uiLeft = new UIDim();
        private IUIDim uiTop = new UIDim();
        private IUIDim uiRight = new UIDim();
        private IUIDim uiBottom = new UIDim();
        private IUIDim uiWidth = new UIDim();
        private IUIDim uiHeight = new UIDim();
        private IUIRect uiRect = new UIRect();

        private IUIWindowManager manager = null;
        private IUIWindow rootWindow = null;

        private IOverlayLabel heightText = null;
        private IOverlayLabel pointer = null;
        private IUIStaticImage stcImg_pop = null;
        private IUIStaticLabel stcTxt_pop = null;

        private string font_8 = null;
        private string font_10 = null;
        private string font_11 = null;
        private string font_12 = null;
        private string font_16 = null;

        private ArrayList arrStars = new ArrayList();
        private ArrayList arrTempers = new ArrayList();

        public MainForm()
        {
            InitializeComponent();            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "OverlayUILabel.html";
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            init();
            this.axRenderControl1.RcCameraFlyFinished += AxRenderControl1_RcCameraFlyFinished;

            manager = this.axRenderControl1.UIWindowManager;
            rootWindow = manager.UIRootWindow;
            this.axRenderControl1.RcUIWindowEvent += AxRenderControl1_RcUIWindowEvent;

            LoadImageButtons();
            LoadOverlayUILabel_Doing();
            LoadOverlayUILabel_ToDo();
            LoadOverlayUILabel_Done();
            LoadOverlayUILabel_Tempers();
        }

        private void AxRenderControl1_RcCameraFlyFinished(byte Type)
        {
            if(Type == 0)
            {
                this.axRenderControl1.Camera.GetCamera(out vec, out ang);
                LoadTextLabel(vec);
                LoadPicLabel();
                LoadPointerLabel(vec.Z);
                this.axRenderControl1.RcCameraFlyFinished -= AxRenderControl1_RcCameraFlyFinished;
                this.axRenderControl1.RcFrame += AxRenderControl1_RcFrame;
            }
        }

        private void AxRenderControl1_RcFrame(int FrameIndex, double ReferencedTime)
        {
            this.axRenderControl1.Camera.GetCamera(out vec, out ang);
            LoadTextLabel(vec);
            LoadPointerLabel(vec.Z);
        }

        private void LoadTextLabel(IVector3 v)
        {
            // 自定义左下角Logo
            if(heightText == null)
            {
                heightText = this.axRenderControl1.ObjectManager.CreateOverlayLabel(rootId);
                ITextAttribute ta = new TextAttribute();
                ta.TextSize = 15;
                ta.Bold = true;
                heightText.TextStyle = ta;
                heightText.SetWidth(85, 0, 0);
                heightText.SetHeight(40, 0, 0);
                heightText.SetX(heightText.GetWidth() / 2 + 20, 0, 0);
                heightText.SetY(heightText.GetHeight() / 2 + 50, 0, 0);
            }

            heightText.Text = "H:" + System.Math.Round(v.Z, 1) + "m";            
        }

        private void LoadPicLabel()
        {
            IOverlayLabel pic = this.axRenderControl1.ObjectManager.CreateOverlayLabel(rootId);
            pic.ImageName = (strMediaPath + @"\png\overlayUI\graduation.png");
            pic.SetWidth(235, 0, 0);
            pic.SetHeight(30, 0, 0);
            pic.SetX(pic.GetWidth() / 2 + 20, 0, 0);
            pic.SetY(pic.GetHeight() / 2 + 20, 0, 0);
        }

        private void LoadPointerLabel(double height)
        {
            if(pointer == null)
            {
                pointer = this.axRenderControl1.ObjectManager.CreateOverlayLabel(rootId);
                pointer.ImageName = (strMediaPath + @"\png\overlayUI\pointer.png");
                pointer.SetWidth(12, 0, 0);
                pointer.SetHeight(20, 0, 0);
                pointer.SetY(pointer.GetHeight() / 2, 0, 0);
            }
            if(height>=500)
            {
                pointer.SetX(43, 0, 0);                
            }
            else if(height<500 && height>=150)
            {
                pointer.SetX(106, 0, 0);
            }
            else if(height<150 && height>=80)
            {
                pointer.SetX(170, 0, 0);
            }
            else if(height<80)
            {
                pointer.SetX(232, 0, 0);
            }
        }

        private void LoadImageButtons()
        {           
            IUIImageButton button1 = manager.CreateImageButton();
            rootWindow.AddChild(button1);
            uiLeft.Init(1, -80);
            uiTop.Init(0.2f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 44);
            uiHeight.Init(0, 44);
            uiRect.SetSize(uiWidth, uiHeight);
            button1.SetArea(uiRect);
            button1.Name = "工具";
            button1.IsVisible = true;
            button1.NormalImage = (strMediaPath + @"\png\overlayUI\tool.png");
            button1.HoverImage = (strMediaPath + @"\png\overlayUI\tool.png");
            button1.PushedImage = (strMediaPath + @"\png\overlayUI\tool.png");            
            button1.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            
            IUIImageButton button2 = manager.CreateImageButton();
            rootWindow.AddChild(button2);
            uiTop.Init(0.3f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            button2.SetArea(uiRect);
            button2.Name = "功能";
            button2.IsVisible = true;
            button2.NormalImage = (strMediaPath + @"\png\overlayUI\function.png");
            button2.HoverImage = (strMediaPath + @"\png\overlayUI\function.png");
            button2.PushedImage = (strMediaPath + @"\png\overlayUI\function.png");            
            button2.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            
            IUIImageButton button3 = manager.CreateImageButton();
            rootWindow.AddChild(button3);
            uiTop.Init(0.4f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            button3.SetArea(uiRect);
            button3.Name = "设置";
            button3.IsVisible = true;
            button3.NormalImage = (strMediaPath + @"\png\overlayUI\setting.png");
            button3.HoverImage = (strMediaPath + @"\png\overlayUI\setting.png");
            button3.PushedImage = (strMediaPath + @"\png\overlayUI\setting.png");            
            button3.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            
            IUIImageButton button4 = manager.CreateImageButton();
            rootWindow.AddChild(button4);
            uiTop.Init(0.5f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            button4.SetArea(uiRect);
            button4.Name = "属性";
            button4.IsVisible = true;
            button4.NormalImage = (strMediaPath + @"\png\overlayUI\property.png");
            button4.HoverImage = (strMediaPath + @"\png\overlayUI\property.png");
            button4.PushedImage = (strMediaPath + @"\png\overlayUI\property.png");            
            button4.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            
            IUIImageButton button5 = manager.CreateImageButton();
            rootWindow.AddChild(button5);
            uiTop.Init(0.6f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            button5.SetArea(uiRect);
            button5.Name = "搜索";
            button5.IsVisible = true;
            button5.NormalImage = (strMediaPath + @"\png\overlayUI\search.png");
            button5.HoverImage = (strMediaPath + @"\png\overlayUI\search.png");
            button5.PushedImage = (strMediaPath + @"\png\overlayUI\search.png");            
            button5.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            
            IUIImageButton button6 = manager.CreateImageButton();
            rootWindow.AddChild(button6);
            uiTop.Init(0.7f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            button6.SetArea(uiRect);
            button6.Name = "演示脚本";
            button6.IsVisible = true;
            button6.NormalImage = (strMediaPath + @"\png\overlayUI\presentation.png");
            button6.HoverImage = (strMediaPath + @"\png\overlayUI\presentation.png");
            button6.PushedImage = (strMediaPath + @"\png\overlayUI\presentation.png");            
            button6.SubscribeEvent(gviUIEventType.gviUIMouseClick);
        }

        private void LoadOverlayUILabel_Doing()
        {
            IOverlayUILabel uilabel1 = this.axRenderControl1.ObjectManager.CreateOverlayUILabel(rootId);
            uiLeft.Init(0, 20);
            uiTop.Init(1.0f, -1);
            uilabel1.SetAnchor(uiLeft, uiTop);
            IPoint pos1 = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            pos1.SetCoords(15418.77, 35708.32, 39.08, 0, 0);
            uilabel1.WorldPosition = pos1;
            uilabel1.MaxVisibleDistance = 800; //直线距离的可见范围
            uilabel1.MinVisibleDistance = 0;
            IUIWindow wLabel1 = uilabel1.GetCanvas();
            uiWidth.Init(0, 250);
            uiHeight.Init(0, 180);
            uiRect.SetSize(uiWidth, uiHeight);
            wLabel1.SetArea(uiRect);  //设置画布宽高

            IUIStaticImage stcImg_bg = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_bg);
            string img_bg = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\background.png"));  
            stcImg_bg.SetImage(img_bg);
            uiLeft.Init(0, 0);
            uiTop.Init(0, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(1, 0);
            uiHeight.Init(1, 0);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_bg.SetArea(uiRect);
            stcImg_bg.IsMousePassThroughEnabled = true;  //会响应鼠标滚轮事件，不会响应UI事件
            stcImg_bg.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来            

            //状态标记：待维修，正在维修，维修完毕
            IUIStaticImage stcImg_ss_doing = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_ss_doing);
            string img_ss_doing = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\ss_doing.png"));
            stcImg_ss_doing.SetImage(img_ss_doing);
            uiLeft.Init(0, 27f);
            uiTop.Init(0, 27f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 37);
            uiHeight.Init(0, 37);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_ss_doing.SetArea(uiRect);
            stcImg_ss_doing.IsMousePassThroughEnabled = true;  //会响应鼠标滚轮事件，不会响应UI事件
            stcImg_ss_doing.IsZOrderingEnabled = false;            

            //65%
            stcImg_pop = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_pop);
            string img_pop = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\pop.png"));
            stcImg_pop.SetImage(img_pop);
            uiLeft.Init(0, 175f);
            uiTop.Init(0, 6);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 28);
            uiHeight.Init(0, 20);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_pop.SetArea(uiRect);
            stcImg_pop.IsMousePassThroughEnabled = true;  //会响应鼠标滚轮事件，不会响应UI事件
            stcImg_pop.IsZOrderingEnabled = false;
            stcImg_pop.IsVisible = false;            

            stcTxt_pop = manager.CreateStaticLabel();
            wLabel1.AddChild(stcTxt_pop);
            font_8 = manager.CreateUIFont(8f, "aa");
            stcTxt_pop.Text = "[font='" + font_8 + "'][colour='FFFFFFFF']65%";
            uiLeft.Init(0, 177f);
            uiTop.Init(0, 7);
            stcTxt_pop.SetPosition(uiLeft, uiTop);            
            stcTxt_pop.IsMousePassThroughEnabled = true;  //会响应鼠标滚轮事件，不会响应UI事件
            stcTxt_pop.IsZOrderingEnabled = false;
            stcTxt_pop.IsVisible = false;            

            //进度条
            IUIStaticImage stcImg_probar_none = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_probar_none);
            string img_probar_none = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\ProgressBar\none.png"));
            stcImg_probar_none.SetImage(img_probar_none);
            uiLeft.Init(0, 66f);
            uiTop.Init(0, 27f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 170);
            uiHeight.Init(0, 6);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_probar_none.SetArea(uiRect);
            //stcImg_probar_none.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_probar_none.IsZOrderingEnabled = false;
            stcImg_probar_none.Name = "stcImg_probar_none";            
            stcImg_probar_none.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);
            stcImg_probar_none.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);

            IUIStaticImage stcImg_probar_done = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_probar_done);
            string img_probar_done = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\ProgressBar\done.png"));
            stcImg_probar_done.SetImage(img_probar_done);
            uiLeft.Init(0, 66f);
            uiTop.Init(0, 27f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 110.5f);
            uiHeight.Init(0, 6);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_probar_done.SetArea(uiRect);
            //stcImg_probar_done.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_probar_done.IsZOrderingEnabled = false;
            stcImg_probar_done.Name = "stcImg_probar_done";            
            stcImg_probar_done.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);
            stcImg_probar_done.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);

            //创建文字：无法设置粗体行间距
            IUIStaticLabel staticTxt = manager.CreateStaticLabel();
            wLabel1.AddChild(staticTxt);
            font_16 = manager.CreateUIFont(16f, "aa");
            font_11 = manager.CreateUIFont(11f, "aa");
            staticTxt.Text = "[font='" + font_16 + "'][colour='FFFFFFFF']环球中心主楼\n[font='" + font_11 + "'][colour='FFFFFFFF']时间：2016.1.22 10:25:00\n原因：玻璃裂纹\n处理：进行中";
            uiLeft.Init(0, 66f);
            uiTop.Init(0, 35);
            staticTxt.SetPosition(uiLeft, uiTop);
            staticTxt.IsMousePassThroughEnabled = true;
            staticTxt.IsZOrderingEnabled = false;
            staticTxt.HorzFormatting = "LeftAligned";               
        }

        private void LoadOverlayUILabel_ToDo()
        {
            IOverlayUILabel uilabel1 = this.axRenderControl1.ObjectManager.CreateOverlayUILabel(rootId);
            uiLeft.Init(0, 20);
            uiTop.Init(1.0f, -1);
            uilabel1.SetAnchor(uiLeft, uiTop);
            IPoint pos1 = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            pos1.SetCoords(15261.38, 35725.24, 29.50, 0, 0);
            uilabel1.WorldPosition = pos1;
            uilabel1.MaxVisibleDistance = 800; //直线距离的可见范围
            uilabel1.MinVisibleDistance = 0;
            IUIWindow wLabel1 = uilabel1.GetCanvas();
            uiWidth.Init(0, 250);
            uiHeight.Init(0, 150);
            uiRect.SetSize(uiWidth, uiHeight);
            wLabel1.SetArea(uiRect);  //设置画布宽高

            IUIStaticImage stcImg_bg = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_bg);
            string img_bg = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\background.png"));
            stcImg_bg.SetImage(img_bg);
            uiLeft.Init(0, 0);
            uiTop.Init(0, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(1, 0);
            uiHeight.Init(1, 0);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_bg.SetArea(uiRect);
            stcImg_bg.IsMousePassThroughEnabled = true;  //响应鼠标事件
            stcImg_bg.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来            

            //状态标记：待维修，正在维修，维修完毕
            IUIStaticImage stcImg_ss_todo = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_ss_todo);
            string img_ss_todo = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\ss_todo.png"));
            stcImg_ss_todo.SetImage(img_ss_todo);
            uiLeft.Init(0, 27f);
            uiTop.Init(0, 15f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 37);
            uiHeight.Init(0, 37);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_ss_todo.SetArea(uiRect);
            stcImg_ss_todo.IsMousePassThroughEnabled = true;
            stcImg_ss_todo.IsZOrderingEnabled = false;            

            //创建文字：无法设置粗体行间距
            IUIStaticLabel staticTxt = manager.CreateStaticLabel();
            wLabel1.AddChild(staticTxt);
            staticTxt.Text = "[font='" + font_16 + "'][colour='FFFFFFFF']茂业中心B座\n[font='" + font_11 + "'][colour='FFFFFFFF']时间：2016.1.22 10:25:00\n原因：玻璃裂纹\n处理：待处理";
            uiLeft.Init(0, 66f);
            uiTop.Init(0, 15f);
            staticTxt.SetPosition(uiLeft, uiTop);            
            staticTxt.IsMousePassThroughEnabled = true;
            staticTxt.IsZOrderingEnabled = false;
            staticTxt.HorzFormatting = "LeftAligned";            
        }

        private void LoadOverlayUILabel_Done()
        {
            IOverlayUILabel uilabel1 = this.axRenderControl1.ObjectManager.CreateOverlayUILabel(rootId);
            uiLeft.Init(0, 20);
            uiTop.Init(1.0f, -1);
            uilabel1.SetAnchor(uiLeft, uiTop);
            IPoint pos1 = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            pos1.SetCoords(15373.86, 36009.12, 20.87, 0, 0);
            uilabel1.WorldPosition = pos1;
            uilabel1.MaxVisibleDistance = 800; //直线距离的可见范围
            uilabel1.MinVisibleDistance = 0;
            uilabel1.MaxViewHeight = 800;  //相机高度和label高度的差值必须在ViewHeight范围内才能显示
            uilabel1.MinViewHeight = 50;
            IUIWindow wLabel1 = uilabel1.GetCanvas();
            uiWidth.Init(0, 250);
            uiHeight.Init(0, 180);
            uiRect.SetSize(uiWidth, uiHeight);
            wLabel1.SetArea(uiRect);  //设置画布宽高

            IUIStaticImage stcImg_bg = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_bg);
            string img_bg = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\background.png"));
            stcImg_bg.SetImage(img_bg);
            uiLeft.Init(0, 0);
            uiTop.Init(0, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(1, 0);
            uiHeight.Init(1, 0);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_bg.SetArea(uiRect);
            stcImg_bg.IsMousePassThroughEnabled = true;  //会响应鼠标滚轮事件，不会响应UI事件
            stcImg_bg.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来            

            //状态标记：待维修，正在维修，维修完毕
            IUIStaticImage stcImg_ss_done = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_ss_done);
            string img_ss_done = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\ss_done.png"));
            stcImg_ss_done.SetImage(img_ss_done);
            uiLeft.Init(0, 27f);
            uiTop.Init(0, 15f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 37);
            uiHeight.Init(0, 37);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_ss_done.SetArea(uiRect);
            stcImg_ss_done.IsMousePassThroughEnabled = true;  //会响应鼠标滚轮事件，不会响应UI事件
            stcImg_ss_done.IsZOrderingEnabled = false;    

            //创建文字：无法设置粗体行间距
            IUIStaticLabel staticTxt = manager.CreateStaticLabel();
            wLabel1.AddChild(staticTxt);
            font_10 = manager.CreateUIFont(10f, "aa");
            staticTxt.Text = "[font='" + font_16 + "'][colour='FFFFFFFF']人民医院\n[font='" + font_10 + "'][colour='FFFFFFFF']时间：2016.1.22 10:25:00\n原因：胶水老化引起玻璃松动\n处理：已完成";
            uiLeft.Init(0, 66f);
            uiTop.Init(0, 15f);
            staticTxt.SetPosition(uiLeft, uiTop);            
            staticTxt.IsMousePassThroughEnabled = true;
            staticTxt.IsZOrderingEnabled = false;
            staticTxt.HorzFormatting = "LeftAligned";            

            //创建文字：无法设置粗体行间距
            IUIStaticLabel staticTxt_clpj = manager.CreateStaticLabel();
            wLabel1.AddChild(staticTxt_clpj);
            font_12 = manager.CreateUIFont(12f, "aa");
            staticTxt_clpj.Text = "[font='" + font_11 + "'][colour='FFFFFF00']处理评价：";
            uiLeft.Init(0, 66f);
            uiTop.Init(0, 82f);
            staticTxt_clpj.SetPosition(uiLeft, uiTop);           
            staticTxt_clpj.IsMousePassThroughEnabled = true;
            staticTxt_clpj.IsZOrderingEnabled = false;
            staticTxt_clpj.HorzFormatting = "LeftAligned";            

            //小星星
            string img_star_gray = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\graystar.png"));
            string img_star_green = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\greenstar.png"));

            IUIStaticImage stcImg_star_gray1 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_gray1);
            stcImg_star_gray1.SetImage(img_star_gray);
            uiLeft.Init(0, 140f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_gray1.SetArea(uiRect);
            //stcImg_star_gray1.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_gray1.IsZOrderingEnabled = false;
            stcImg_star_gray1.Name = "stcImg_star_gray1";            
            stcImg_star_gray1.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);

            IUIStaticImage stcImg_star_green1 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_green1);
            stcImg_star_green1.SetImage(img_star_green);
            uiLeft.Init(0, 140f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_green1.SetArea(uiRect);
            //stcImg_star_green1.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_green1.IsZOrderingEnabled = false;
            stcImg_star_green1.Name = "stcImg_star_green1";
            stcImg_star_green1.IsVisible = false;            
            stcImg_star_green1.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            stcImg_star_green1.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);
            arrStars.Add(stcImg_star_green1);

            IUIStaticImage stcImg_star_gray2 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_gray2);
            stcImg_star_gray2.SetImage(img_star_gray);
            uiLeft.Init(0, 156f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_gray2.SetArea(uiRect);
            //stcImg_star_gray2.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_gray2.IsZOrderingEnabled = false;
            stcImg_star_gray2.Name = "stcImg_star_gray2";            
            stcImg_star_gray2.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);

            IUIStaticImage stcImg_star_green2 = manager.CreateStaticImage();
            arrStars.Add(stcImg_star_green2);
            stcImg_star_green2.SetImage(img_star_green);
            uiLeft.Init(0, 156f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_green2.SetArea(uiRect);
            //stcImg_star_green2.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_green2.IsZOrderingEnabled = false;
            stcImg_star_green2.Name = "stcImg_star_green2";
            stcImg_star_green2.IsVisible = false;
            wLabel1.AddChild(stcImg_star_green2);
            stcImg_star_green2.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            stcImg_star_green2.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);            

            IUIStaticImage stcImg_star_gray3 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_gray3);
            stcImg_star_gray3.SetImage(img_star_gray);
            uiLeft.Init(0, 171f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_gray3.SetArea(uiRect);
            //stcImg_star_gray3.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_gray3.IsZOrderingEnabled = false;
            stcImg_star_gray3.Name = "stcImg_star_gray3";            
            stcImg_star_gray3.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);

            IUIStaticImage stcImg_star_green3 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_green3);
            stcImg_star_green3.SetImage(img_star_green);
            uiLeft.Init(0, 171f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_green3.SetArea(uiRect);
            //stcImg_star_green3.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_green3.IsZOrderingEnabled = false;
            stcImg_star_green3.Name = "stcImg_star_green3";
            stcImg_star_green3.IsVisible = false;            
            stcImg_star_green3.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            stcImg_star_green3.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);
            arrStars.Add(stcImg_star_green3);

            IUIStaticImage stcImg_star_gray4 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_gray4);
            stcImg_star_gray4.SetImage(img_star_gray);
            uiLeft.Init(0, 186f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_gray4.SetArea(uiRect);
            //stcImg_star_gray4.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_gray4.IsZOrderingEnabled = false;
            stcImg_star_gray4.Name = "stcImg_star_gray4";            
            stcImg_star_gray4.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);

            IUIStaticImage stcImg_star_green4 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_green4);
            stcImg_star_green4.SetImage(img_star_green);
            uiLeft.Init(0, 186f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_green4.SetArea(uiRect);
            //stcImg_star_green4.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_green4.IsZOrderingEnabled = false;
            stcImg_star_green4.Name = "stcImg_star_green4";
            stcImg_star_green4.IsVisible = false;            
            stcImg_star_green4.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            stcImg_star_green4.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);
            arrStars.Add(stcImg_star_green4);

            IUIStaticImage stcImg_star_gray5 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_gray5);
            stcImg_star_gray5.SetImage(img_star_gray);
            uiLeft.Init(0, 201f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_gray5.SetArea(uiRect);
            //stcImg_star_gray5.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_gray5.IsZOrderingEnabled = false;
            stcImg_star_gray5.Name = "stcImg_star_gray5";            
            stcImg_star_gray5.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);

            IUIStaticImage stcImg_star_green5 = manager.CreateStaticImage();
            wLabel1.AddChild(stcImg_star_green5);
            stcImg_star_green5.SetImage(img_star_green);
            uiLeft.Init(0, 201f);
            uiTop.Init(0, 82f);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 15);
            uiHeight.Init(0, 15);
            uiRect.SetSize(uiWidth, uiHeight);
            stcImg_star_green5.SetArea(uiRect);
            //stcImg_star_green5.IsMousePassThroughEnabled = true;  //设为true时会导致无法响应UI事件
            stcImg_star_green5.IsZOrderingEnabled = false;
            stcImg_star_green5.Name = "stcImg_star_green5";
            stcImg_star_green5.IsVisible = false;            
            stcImg_star_green5.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            stcImg_star_green5.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);
            arrStars.Add(stcImg_star_green5);
        }

        private void LoadOverlayUILabel_Tempers()
        {
            {
                IOverlayUILabel uilabel1 = this.axRenderControl1.ObjectManager.CreateOverlayUILabel(rootId);
                uiLeft.Init(0, 5);
                uiTop.Init(1.0f, -5);
                uilabel1.SetAnchor(uiLeft, uiTop);
                IPoint pos1 = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                pos1.SetCoords(15049.64, 35694.76, 17.25, 0, 0);
                uilabel1.WorldPosition = pos1;
                uilabel1.MaxVisibleDistance = 1200; //直线距离的可见范围
                uilabel1.MinVisibleDistance = 0;
                IUIWindow wLabel1 = uilabel1.GetCanvas();
                uiWidth.Init(0, 38);
                uiHeight.Init(0, 119);
                uiRect.SetSize(uiWidth, uiHeight);
                wLabel1.SetArea(uiRect);  //设置画布宽高
                wLabel1.Name = "temper1";
                wLabel1.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);
                wLabel1.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);
                arrTempers.Add(uilabel1);

                IUIStaticImage stcImg_bg = manager.CreateStaticImage();
                wLabel1.AddChild(stcImg_bg);
                string img_bg = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\bg_temper.png"));
                stcImg_bg.SetImage(img_bg);
                uiLeft.Init(0, 0);
                uiTop.Init(0, 0);
                uiRect.SetPosition(uiLeft, uiTop);
                uiWidth.Init(0, 38);
                uiHeight.Init(0, 119);
                uiRect.SetSize(uiWidth, uiHeight);
                stcImg_bg.SetArea(uiRect);
                stcImg_bg.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来                

                IUIStaticImage stcImg_detail = manager.CreateStaticImage();
                wLabel1.AddChild(stcImg_detail);
                string img_detail = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\bg_detail.png"));
                stcImg_detail.SetImage(img_detail);
                uiLeft.Init(0, 37f);
                uiTop.Init(0, 0);
                uiRect.SetPosition(uiLeft, uiTop);
                uiWidth.Init(0, 140);
                uiHeight.Init(0, 34);
                uiRect.SetSize(uiWidth, uiHeight);
                stcImg_detail.SetArea(uiRect);
                stcImg_detail.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来
                stcImg_detail.Name = "detail1";
                stcImg_detail.SubscribeEvent(gviUIEventType.gviUIMouseClick);                

                //创建文字：无法设置粗体行间距
                IUIStaticLabel staticTxt = manager.CreateStaticLabel();
                wLabel1.AddChild(staticTxt);
                staticTxt.Text = "[font='" + font_16 + "'][colour='FFFFFFFF']24          45";
                uiLeft.Init(0, 42f);
                uiTop.Init(0, 2f);
                staticTxt.SetPosition(uiLeft, uiTop);                
                staticTxt.IsMousePassThroughEnabled = true;
                staticTxt.IsZOrderingEnabled = false;
                staticTxt.HorzFormatting = "LeftAligned";                
            }

            {
                IOverlayUILabel uilabel1 = this.axRenderControl1.ObjectManager.CreateOverlayUILabel(rootId);
                uiLeft.Init(0, 5);
                uiTop.Init(1.0f, -5);
                uilabel1.SetAnchor(uiLeft, uiTop);
                IPoint pos1 = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                pos1.SetCoords(15117.75, 35739.19, 17.51, 0, 0);
                uilabel1.WorldPosition = pos1;
                uilabel1.MaxVisibleDistance = 1200; //直线距离的可见范围
                uilabel1.MinVisibleDistance = 0;
                IUIWindow wLabel1 = uilabel1.GetCanvas();
                uiWidth.Init(0, 38);
                uiHeight.Init(0, 119);
                uiRect.SetSize(uiWidth, uiHeight);
                wLabel1.SetArea(uiRect);  //设置画布宽高
                wLabel1.Name = "temper2";
                wLabel1.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);
                wLabel1.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);
                arrTempers.Add(uilabel1);

                IUIStaticImage stcImg_bg = manager.CreateStaticImage();
                wLabel1.AddChild(stcImg_bg);
                string img_bg = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\bg_temper.png"));
                stcImg_bg.SetImage(img_bg);
                uiLeft.Init(0, 0);
                uiTop.Init(0, 0);
                uiRect.SetPosition(uiLeft, uiTop);
                uiWidth.Init(0, 38);
                uiHeight.Init(0, 119);
                uiRect.SetSize(uiWidth, uiHeight);
                stcImg_bg.SetArea(uiRect);
                stcImg_bg.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来                

                IUIStaticImage stcImg_detail = manager.CreateStaticImage();
                wLabel1.AddChild(stcImg_detail);
                string img_detail = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\bg_detail.png"));
                stcImg_detail.SetImage(img_detail);
                uiLeft.Init(0, 37f);
                uiTop.Init(0, 0);
                uiRect.SetPosition(uiLeft, uiTop);
                uiWidth.Init(0, 140);
                uiHeight.Init(0, 34);
                uiRect.SetSize(uiWidth, uiHeight);
                stcImg_detail.SetArea(uiRect);
                stcImg_detail.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来
                stcImg_detail.Name = "detail2";
                stcImg_detail.SubscribeEvent(gviUIEventType.gviUIMouseClick);                

                //创建文字：无法设置粗体行间距
                IUIStaticLabel staticTxt = manager.CreateStaticLabel();
                wLabel1.AddChild(staticTxt);
                staticTxt.Text = "[font='" + font_16 + "'][colour='FFFFFFFF']24          45";
                uiLeft.Init(0, 42f);
                uiTop.Init(0, 2f);
                staticTxt.SetPosition(uiLeft, uiTop);                
                staticTxt.IsMousePassThroughEnabled = true;
                staticTxt.IsZOrderingEnabled = false;
                staticTxt.HorzFormatting = "LeftAligned";                
            }

            {
                IOverlayUILabel uilabel1 = this.axRenderControl1.ObjectManager.CreateOverlayUILabel(rootId);
                uiLeft.Init(0, 5);
                uiTop.Init(1.0f, -5);
                uilabel1.SetAnchor(uiLeft, uiTop);
                IPoint pos1 = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                pos1.SetCoords(15198.83, 35801.62, 16.42, 0, 0);
                uilabel1.WorldPosition = pos1;
                uilabel1.MaxVisibleDistance = 1200; //直线距离的可见范围
                uilabel1.MinVisibleDistance = 0;
                IUIWindow wLabel1 = uilabel1.GetCanvas();
                uiWidth.Init(0, 38);
                uiHeight.Init(0, 119);
                uiRect.SetSize(uiWidth, uiHeight);
                wLabel1.SetArea(uiRect);  //设置画布宽高
                wLabel1.Name = "temper3";
                wLabel1.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);
                wLabel1.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);
                arrTempers.Add(uilabel1);

                IUIStaticImage stcImg_bg = manager.CreateStaticImage();
                wLabel1.AddChild(stcImg_bg);
                string img_bg = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\bg_temper.png"));
                stcImg_bg.SetImage(img_bg);
                uiLeft.Init(0, 0);
                uiTop.Init(0, 0);
                uiRect.SetPosition(uiLeft, uiTop);
                uiWidth.Init(0, 38);
                uiHeight.Init(0, 119);
                uiRect.SetSize(uiWidth, uiHeight);
                stcImg_bg.SetArea(uiRect);
                stcImg_bg.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来                

                IUIStaticImage stcImg_detail = manager.CreateStaticImage();
                wLabel1.AddChild(stcImg_detail);
                string img_detail = manager.CreateImageFromFile((strMediaPath + @"\png\overlayUI\bg_detail.png"));
                stcImg_detail.SetImage(img_detail);
                uiLeft.Init(0, 37f);
                uiTop.Init(0, 0);
                uiRect.SetPosition(uiLeft, uiTop);
                uiWidth.Init(0, 140);
                uiHeight.Init(0, 34);
                uiRect.SetSize(uiWidth, uiHeight);
                stcImg_detail.SetArea(uiRect);
                stcImg_detail.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来
                stcImg_detail.Name = "detail3";
                stcImg_detail.SubscribeEvent(gviUIEventType.gviUIMouseClick);                

                //创建文字：无法设置粗体行间距
                IUIStaticLabel staticTxt = manager.CreateStaticLabel();
                wLabel1.AddChild(staticTxt);
                staticTxt.Text = "[font='" + font_16 + "'][colour='FFFFFFFF']24          45";
                uiLeft.Init(0, 42f);
                uiTop.Init(0, 2f);
                staticTxt.SetPosition(uiLeft, uiTop);                
                staticTxt.IsMousePassThroughEnabled = true;
                staticTxt.IsZOrderingEnabled = false;
                staticTxt.HorzFormatting = "LeftAligned";                
            }
        }

        private void AxRenderControl1_RcUIWindowEvent(IUIEventArgs EventArgs, gviUIEventType EventType)
        {
            IUIMouseEventArgs args = EventArgs as IUIMouseEventArgs;
            if (args.UIEventWindow == null)
                return;

            string windowName = args.UIEventWindow.Name;
            if (EventType == gviUIEventType.gviUIMouseClick)
            {
                switch (windowName)
                {
                    case "工具":
                    case "功能":
                    case "设置":
                    case "属性":
                    case "搜索":
                    case "演示脚本":
                        MessageBox.Show(args.UIEventWindow.Name);
                        break;
                    case "stcImg_star_green1":
                    case "stcImg_star_green2":
                    case "stcImg_star_green3":
                    case "stcImg_star_green4":
                    case "stcImg_star_green5":
                        {
                            System.Diagnostics.Debug.WriteLine("gviUIMouseClick");
                            arrStars.Clear();
                        }
                        break;
                    case "detail1":
                    case "detail2":
                    case "detail3":
                        {
                            string strIdx = windowName.Substring(6);
                            int idx = int.Parse(strIdx);
                            IOverlayUILabel label = arrTempers[idx - 1] as IOverlayUILabel;

                            WindowParam wp = new WindowParam();
                            wp.FilePath = "this is " + windowName;
                            wp.Position = gviHTMLWindowPosition.gviWinPosMousePosition;
                            wp.SizeX = 200;
                            wp.SizeY = 150;
                            wp.Hastitle = true;
                            wp.IsPopupWindow = false;
                            wp.HideOnClick = true;
                            wp.WinId = 1;
                            IHTMLWindow hw = this.axRenderControl1 as IHTMLWindow;
                            hw.ShowPopupWindowEx(label.WorldPosition, wp, true);
                        }
                        break;
                }
            }
            else if (EventType == gviUIEventType.gviUIMouseEntersArea)
            {
                switch (windowName)
                {
                    case "stcImg_probar_none":
                    case "stcImg_probar_done":
                        {
                            stcImg_pop.IsVisible = true;
                            stcTxt_pop.IsVisible = true;
                        }
                        break;
                    case "stcImg_star_gray1":
                    case "stcImg_star_gray2":
                    case "stcImg_star_gray3":
                    case "stcImg_star_gray4":
                    case "stcImg_star_gray5":
                        {
                            System.Diagnostics.Debug.WriteLine("gviUIMouseEntersArea");
                            string strIdx = windowName.Substring(16);
                            int idx = int.Parse(strIdx);
                            for (int ii = 0; ii < arrStars.Count; ii++)
                            {
                                IUIStaticImage stcImgInArr = (IUIStaticImage)arrStars[ii];
                                if (ii < idx && stcImgInArr.IsVisible != true)
                                    stcImgInArr.IsVisible = true;
                                else if(ii >= idx)
                                    stcImgInArr.IsVisible = false;
                            }
                        }
                        break;
                    case "temper1":
                    case "temper2":
                    case "temper3":
                        {
                            string strIdx = windowName.Substring(6);
                            int idx = int.Parse(strIdx);
                            IOverlayUILabel label = arrTempers[idx-1] as IOverlayUILabel;
                            IUIWindow wLabel1 = label.GetCanvas();
                            //必须设置position
                            IUIRect area = wLabel1.GetArea();
                            IUIDim m_left, m_top;
                            area.GetPosition(out m_left, out m_top);
                            uiRect.SetPosition(m_left, m_top);
                            uiWidth.Init(0, 178);
                            uiHeight.Init(0, 119);
                            uiRect.SetSize(uiWidth, uiHeight);
                            wLabel1.SetArea(uiRect);  //设置画布宽高
                        }
                        break;
                }
            }
            else if (EventType == gviUIEventType.gviUIMouseLeavesArea)
            {
                switch (windowName)
                {
                    case "stcImg_probar_none":
                    case "stcImg_probar_done":
                        {
                            stcImg_pop.IsVisible = false;
                            stcTxt_pop.IsVisible = false;
                        }
                        break;
                    case "stcImg_star_green1":
                    case "stcImg_star_green2":
                    case "stcImg_star_green3":
                    case "stcImg_star_green4":
                    case "stcImg_star_green5":
                        {
                            System.Diagnostics.Debug.WriteLine("gviUIMouseLeavesArea");
                            string strIdx = windowName.Substring(17);
                            int idx = int.Parse(strIdx);
                            for (int ii = 0; ii < arrStars.Count; ii++)
                            {
                                IUIStaticImage stcImgInArr = (IUIStaticImage)arrStars[ii];
                                if (ii < idx && stcImgInArr.IsVisible != false)
                                    stcImgInArr.IsVisible = false;
                                else if(ii >= idx)
                                    stcImgInArr.IsVisible = false;
                            }
                        }
                        break;
                    case "temper1":
                    case "temper2":
                    case "temper3":
                        {
                            string strIdx = windowName.Substring(6);
                            int idx = int.Parse(strIdx);
                            IOverlayUILabel label = arrTempers[idx-1] as IOverlayUILabel;
                            IUIWindow wLabel1 = label.GetCanvas();
                            //必须设置position
                            IUIRect area = wLabel1.GetArea();
                            IUIDim m_left, m_top;
                            area.GetPosition(out m_left, out m_top);
                            uiRect.SetPosition(m_left, m_top);
                            uiWidth.Init(0, 38);
                            uiHeight.Init(0, 119);
                            uiRect.SetSize(uiWidth, uiHeight);
                            wLabel1.SetArea(uiRect);  //设置画布宽高
                        }
                        break;
                }
            }
        }

    
    }
}
