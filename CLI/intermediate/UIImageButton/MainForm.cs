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

namespace UIImageButton
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


        private IUIImageButton imgbtn1 = null;
        private IUIImageButton imgbtn2 = null;
        private IUIImageButton imgbtn3 = null;
        private IUIDim uiLeft = new UIDim();
        private IUIDim uiTop = new UIDim();
        private IUIDim uiRight = new UIDim();
        private IUIDim uiBottom = new UIDim();
        private IUIDim uiWidth = new UIDim();
        private IUIDim uiHeight = new UIDim();
        private IUIRect uiRect = new UIRect();

        public MainForm()
        {
            InitializeComponent();

            init();

            #region 加载上方按钮
            uiLeft.Init(0, 10);
            uiTop.Init(0, 10);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 32);
            uiHeight.Init(0, 32);
            uiRect.SetSize(uiWidth, uiHeight);
            IUIWindowManager manager = this.axRenderControl1.UIWindowManager;
            IUIWindow rootWindow = manager.UIRootWindow;
            IUIImageButton button1 = manager.CreateImageButton();
            button1.SetArea(uiRect);
            button1.Name = "漫游";
            button1.IsVisible = true;
            button1.NormalImage = (strMediaPath + @"\png\button\漫游\normal.png");
            button1.HoverImage = (strMediaPath + @"\png\button\漫游\hover.png");
            button1.PushedImage = (strMediaPath + @"\png\button\漫游\pushed.png");
            rootWindow.AddChild(button1);
            button1.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            this.axRenderControl1.RcUIWindowEvent += AxRenderControl1_RcUIWindowEvent;

            uiLeft.Init(0, 52);
            uiTop.Init(0, 10);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            IUIImageButton button2 = manager.CreateImageButton();
            button2.SetArea(uiRect);
            button2.Name = "点选";
            button2.IsVisible = true;
            button2.NormalImage = (strMediaPath + @"\png\button\点选\normal.png");
            button2.HoverImage = (strMediaPath + @"\png\button\点选\hover.png");
            button2.PushedImage = (strMediaPath + @"\png\button\点选\pushed.png");
            rootWindow.AddChild(button2);
            button2.SubscribeEvent(gviUIEventType.gviUIMouseClick);

            uiLeft.Init(0, 94);
            uiTop.Init(0, 10);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            IUIImageButton button3 = manager.CreateImageButton();
            button3.SetArea(uiRect);
            button3.Name = "天气";
            button3.IsVisible = true;
            button3.NormalImage = (strMediaPath + @"\png\button\天气\normal.png");
            button3.HoverImage = (strMediaPath + @"\png\button\天气\hover.png");
            button3.PushedImage = (strMediaPath + @"\png\button\天气\pushed.png");
            rootWindow.AddChild(button3);
            button3.SubscribeEvent(gviUIEventType.gviUIMouseEntersArea);
            //button3.SubscribeEvent(gviUIEventType.gviUIMouseLeavesArea);

            uiLeft.Init(0, 94);
            uiTop.Init(0, 52);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0, 80);
            uiHeight.Init(0, 31);
            uiRect.SetSize(uiWidth, uiHeight);
            imgbtn1 = manager.CreateImageButton();
            imgbtn1.SetArea(uiRect);
            imgbtn1.Name = "SunShine";
            imgbtn1.Text = "晴天";
            imgbtn1.IsVisible = false;
            imgbtn1.NormalImage = (strMediaPath + @"\png\button\晴天\normal.png");
            imgbtn1.PushedImage = (strMediaPath + @"\png\button\晴天\pushed.png");
            imgbtn1.HoverImage = (strMediaPath + @"\png\button\晴天\hover.png");
            rootWindow.AddChild(imgbtn1);
            imgbtn1.SubscribeEvent(gviUIEventType.gviUIMouseClick);

            uiLeft.Init(0, 94);
            uiTop.Init(0, 83);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            imgbtn2 = manager.CreateImageButton();
            imgbtn2.SetArea(uiRect);
            imgbtn2.Name = "HeavyRain";
            imgbtn2.Text = "大雨";
            imgbtn2.IsVisible = false;
            imgbtn2.NormalImage = (strMediaPath + @"\png\button\大雨\normal.png");
            imgbtn2.PushedImage = (strMediaPath + @"\png\button\大雨\pushed.png");
            imgbtn2.HoverImage = (strMediaPath + @"\png\button\大雨\hover.png");
            rootWindow.AddChild(imgbtn2);
            imgbtn2.SubscribeEvent(gviUIEventType.gviUIMouseClick);

            uiLeft.Init(0, 94);
            uiTop.Init(0, 114);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            imgbtn3 = manager.CreateImageButton();
            imgbtn3.SetArea(uiRect);
            imgbtn3.Name = "HeavySnow";
            imgbtn3.Text = "大雪";
            imgbtn3.IsVisible = false;
            imgbtn3.NormalImage = (strMediaPath + @"\png\button\大雪\normal.png");
            imgbtn3.PushedImage = (strMediaPath + @"\png\button\大雪\pushed.png");
            imgbtn3.HoverImage = (strMediaPath + @"\png\button\大雪\hover.png");
            rootWindow.AddChild(imgbtn3);
            imgbtn3.SubscribeEvent(gviUIEventType.gviUIMouseClick);
            #endregion

            #region 加载下方按钮
            uiLeft.Init(0, 0);
            uiTop.Init(0.8f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(0.2f, 0);
            uiHeight.Init(0.2f, 0);
            uiRect.SetSize(uiWidth, uiHeight);
            IUIImageButton button4 = manager.CreateImageButton();
            button4.SetArea(uiRect);
            button4.Name = "location1";
            button4.Text = "location1";
            button4.IsVisible = true;
            button4.NormalImage = (strMediaPath + @"\png\location\普通\2c495ffc-4641-447b-a5a4-636e4f3e7976.png");
            button4.PushedImage = (strMediaPath + @"\png\location\按下\2c495ffc-4641-447b-a5a4-636e4f3e7976.png");
            button4.HoverImage = (strMediaPath + @"\png\location\选中\2c495ffc-4641-447b-a5a4-636e4f3e7976.png");
            rootWindow.AddChild(button4);
            button4.SubscribeEvent(gviUIEventType.gviUIMouseClick);

            uiLeft.Init(0.2f, 0);
            uiTop.Init(0.8f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            IUIImageButton button5 = manager.CreateImageButton();
            button5.SetArea(uiRect);
            button5.Name = "location2";
            button5.Text = "location2";
            button5.IsVisible = true;
            button5.NormalImage = (strMediaPath + @"\png\location\普通\2e0ca5d1-73d2-4c28-9698-2b64c89cc806.png");
            button5.PushedImage = (strMediaPath + @"\png\location\按下\2e0ca5d1-73d2-4c28-9698-2b64c89cc806.png");
            button5.HoverImage = (strMediaPath + @"\png\location\选中\2e0ca5d1-73d2-4c28-9698-2b64c89cc806.png");
            rootWindow.AddChild(button5);
            button5.SubscribeEvent(gviUIEventType.gviUIMouseClick);

            uiLeft.Init(0.4f, 0);
            uiTop.Init(0.8f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            IUIImageButton button6 = manager.CreateImageButton();
            button6.SetArea(uiRect);
            button6.Name = "location3";
            button6.Text = "location3";
            button6.IsVisible = true;
            button6.NormalImage = (strMediaPath + @"\png\location\普通\76ba8729-0131-40f4-9713-9a9374a76936.png");
            button6.PushedImage = (strMediaPath + @"\png\location\按下\76ba8729-0131-40f4-9713-9a9374a76936.png");
            button6.HoverImage = (strMediaPath + @"\png\location\选中\76ba8729-0131-40f4-9713-9a9374a76936.png");
            rootWindow.AddChild(button6);
            button6.SubscribeEvent(gviUIEventType.gviUIMouseClick);

            uiLeft.Init(0.6f, 0);
            uiTop.Init(0.8f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            IUIImageButton button7 = manager.CreateImageButton();
            button7.SetArea(uiRect);
            button7.Name = "location4";
            button7.Text = "location4";
            button7.IsVisible = true;
            button7.NormalImage = (strMediaPath + @"\png\location\普通\84e489db-3f82-43fa-b068-c85f95f680f1.png");
            button7.PushedImage = (strMediaPath + @"\png\location\按下\84e489db-3f82-43fa-b068-c85f95f680f1.png");
            button7.HoverImage = (strMediaPath + @"\png\location\选中\84e489db-3f82-43fa-b068-c85f95f680f1.png");
            rootWindow.AddChild(button7);
            button7.SubscribeEvent(gviUIEventType.gviUIMouseClick);

            uiLeft.Init(0.8f, 0);
            uiTop.Init(0.8f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiRect.SetSize(uiWidth, uiHeight);
            IUIImageButton button9 = manager.CreateImageButton();
            button9.SetArea(uiRect);
            button9.Name = "location5";
            button9.Text = "location5";
            button9.IsVisible = true;
            button9.NormalImage = (strMediaPath + @"\png\location\普通\ffd10c67-373d-45d7-b901-b493ffc2741b.png");
            button9.PushedImage = (strMediaPath + @"\png\location\按下\ffd10c67-373d-45d7-b901-b493ffc2741b.png");
            button9.HoverImage = (strMediaPath + @"\png\location\选中\ffd10c67-373d-45d7-b901-b493ffc2741b.png");
            rootWindow.AddChild(button9);
            button9.SubscribeEvent(gviUIEventType.gviUIMouseClick);

            #endregion

            
            IOverlayUILabel uilabel1 = this.axRenderControl1.ObjectManager.CreateOverlayUILabel(rootId);
            uiLeft.Init(0.5f, 0);
            uiTop.Init(0.5f, 0);            
            uilabel1.SetAnchor(uiLeft, uiTop);
            IPoint pos1 = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            pos1.SetCoords(15195.99, 35807.57, 16.76, 0, 0);
            uilabel1.WorldPosition = pos1;
            uilabel1.MaxViewHeight = -100;  //垂直高度的可见范围
            uilabel1.MinViewHeight = 50;
            uilabel1.MaxVisibleDistance = 300; //直线距离的可见范围
            uilabel1.MinVisibleDistance = 0;
            IUIWindow wLabel1 = uilabel1.GetCanvas();
            //uiWidth.Init(1, 0);  //三维窗口宽
            //uiHeight.Init(1, 0);  ////三维窗口高
            uiWidth.Init(0, 200);  
            uiHeight.Init(0, 100);  
            uiRect.SetSize(uiWidth, uiHeight);
            wLabel1.SetArea(uiRect);  //设置画布宽高
            
            IUIStaticImage staticImg1 = manager.CreateStaticImage();
            string img1 = manager.CreateImageFromFile(@"D:\TestData\png\CH4.png");
            staticImg1.SetImage(img1);
            uiLeft.Init(0, 0);
            uiTop.Init(0, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            uiWidth.Init(1, 0);
            uiHeight.Init(1, 0);
            uiRect.SetSize(uiWidth, uiHeight);
            staticImg1.SetArea(uiRect);
            staticImg1.IsMousePassThroughEnabled = true;  //响应鼠标事件
            //staticImg1.IsZOrderingEnabled = false;   //鼠标点击后不会跑上面来
            wLabel1.AddChild(staticImg1);

            IUIStaticLabel staticLab1 = manager.CreateStaticLabel();
            string font1 = manager.CreateUIFont(15f, "aa");
            staticLab1.Text = "[font='" + font1 + "'][colour='FF0000FF']30/立方米";    
            //staticLab1.Text = "30/立方米";
            uiLeft.Init(0.5f, 0);
            uiTop.Init(0.5f, 0);
            uiRect.SetPosition(uiLeft, uiTop);
            float textWid = 0.0f;
            float textHeight = 0.0f;
            staticLab1.GetTextSize(out textWid, out textHeight);
            uiWidth.Init(0, textWid);
            uiHeight.Init(0, textHeight);
            uiRect.SetSize(uiWidth, uiHeight);            
            staticLab1.SetArea(uiRect);     
            wLabel1.AddChild(staticLab1);


            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "UIImageButton.html";
            }
        }

        private void AxRenderControl1_RcUIWindowEvent(IUIEventArgs EventArgs, gviUIEventType EventType)
        {
            IUIMouseEventArgs args = EventArgs as IUIMouseEventArgs;
            if (args.UIEventWindow == null)
                return;

            if (EventType == gviUIEventType.gviUIMouseClick)
            {
                gviUIWindowType winType = args.UIEventWindow.Type;
                if (winType == gviUIWindowType.gviUIImageButton)
                {
                    switch (args.UIEventWindow.Name)
                    {
                        case "漫游":
                            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                            break;
                        case "点选":
                            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                            break;
                        case "location1":
                            vec.Set(15415.510188040265, 35593.117437895737, 59.003287982044796);
                            ang.Set(47.7, -30.55, 0);
                            this.axRenderControl1.Camera.SetCamera(vec, ang, gviSetCameraFlags.gviSetCameraNoFlags);
                            break;
                        case "location2":
                            vec.Set(15243.39327614102, 35593.454101290568, 19.083291340718386);
                            ang.Set(-37.01, -15.27, 0);
                            this.axRenderControl1.Camera.SetCamera(vec, ang, gviSetCameraFlags.gviSetCameraNoFlags);
                            break;
                        case "location3":
                            vec.Set(15150.53692901546, 35785.206458874149, 21.492597977763278);
                            ang.Set(-136.66, -19.68, 0);
                            this.axRenderControl1.Camera.SetCamera(vec, ang, gviSetCameraFlags.gviSetCameraNoFlags);
                            break;
                        case "location4":
                            vec.Set(15562.369345366114, 36027.787538479148, 9.5395100144668721);
                            ang.Set(-98.11, -5.3, 0);
                            this.axRenderControl1.Camera.SetCamera(vec, ang, gviSetCameraFlags.gviSetCameraNoFlags);
                            break;
                        case "location5":
                            vec.Set(15290.261360847539, 35689.443985629681, 25.558723498791508);
                            ang.Set(-34.53, -34.98, 0);
                            this.axRenderControl1.Camera.SetCamera(vec, ang, gviSetCameraFlags.gviSetCameraNoFlags);
                            break;
                        case "SunShine":
                            this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherSunShine;
                            imgbtn1.IsVisible = false;
                            imgbtn2.IsVisible = false;
                            imgbtn3.IsVisible = false;
                            break;
                        case "HeavyRain":
                            this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherHeavyRain;
                            imgbtn1.IsVisible = false;
                            imgbtn2.IsVisible = false;
                            imgbtn3.IsVisible = false;
                            break;
                        case "HeavySnow":
                            this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherHeavySnow;
                            imgbtn1.IsVisible = false;
                            imgbtn2.IsVisible = false;
                            imgbtn3.IsVisible = false;
                            break;
                    }
                }
                else if (winType == gviUIWindowType.gviUITextButton)
                {
                    switch (args.UIEventWindow.Name)
                    {
                        case "SunShine":
                            this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherSunShine;
                            imgbtn1.IsVisible = false;
                            imgbtn2.IsVisible = false;
                            imgbtn3.IsVisible = false;
                            break;
                        case "HeavyRain":
                            this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherHeavyRain;
                            imgbtn1.IsVisible = false;
                            imgbtn2.IsVisible = false;
                            imgbtn3.IsVisible = false;
                            break;
                        case "HeavySnow":
                            this.axRenderControl1.ObjectManager.GetSkyBox(0).Weather = gviWeatherType.gviWeatherHeavySnow;
                            imgbtn1.IsVisible = false;
                            imgbtn2.IsVisible = false;
                            imgbtn3.IsVisible = false;
                            break;
                    }
                }
            }
            else if (EventType == gviUIEventType.gviUIMouseEntersArea)
            {
                gviUIWindowType winType = args.UIEventWindow.Type;
                if (winType == gviUIWindowType.gviUIImageButton)
                {
                    switch (args.UIEventWindow.Name)
                    {
                        case "天气":
                            imgbtn1.IsVisible = true;
                            imgbtn2.IsVisible = true;
                            imgbtn3.IsVisible = true;
                            break;
                    }
                }
            }
            //else if (EventType == gviUIEventType.gviUIMouseLeavesArea)
            //{
            //    gviUIWindowType winType = args.UIEventWindow.Type;
            //    if (winType == gviUIWindowType.gviUIImageButton)
            //    {
            //        switch (args.UIEventWindow.Name)
            //        {
            //            case "天气":
            //                imgbtn1.IsVisible = false;
            //                imgbtn2.IsVisible = false;
            //                imgbtn3.IsVisible = false;
            //                break;
            //        }
            //    }
            //}
        }


        private void MainForm_Load(object sender, System.EventArgs e)
        {
            // 自定义左下角Logo
            IOverlayLabel logoLabel = this.axRenderControl1.ObjectManager.CreateOverlayLabel(rootId);
            string imageNamePath = (strMediaPath + @"\bmp\tablelabel_pic.bmp");
            logoLabel.ImageName = imageNamePath;
            logoLabel.SetWidth(150, 0, 0);
            logoLabel.SetHeight(50, 0, 0);
            logoLabel.SetX(-logoLabel.GetWidth() / 2, 1, 0);
            logoLabel.SetY(logoLabel.GetHeight() / 2, 0, 0);
        }
    
    }
}
