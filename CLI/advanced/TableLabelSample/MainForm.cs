// Copyright 2012 CityMaker SDK
// 
// All rights reserved under the copyright laws of the China
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See Sample at <your CityMaker install location>/CityMaker SDK/Samples.
// 
//author	gs
//date	2011/09/26
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Controls;


namespace TableLabelSample
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        

        private IObjectEditor _geoEditor = null;     
        private bool resultCode;


        private ITableLabel tableLabel = null;
        private ITableLabel dynamicTableLabel = null;
        private IMotionPath motionPath = null;
        private IRenderModelPoint renderModelPoint = null;
        private IMotionable m = null;

        private IPoint fde_point = null;
        private IVector3 position = new Vector3();  // 指定label位置
        private IVector3 scale = new Vector3();
        private IEulerAngle angle = new EulerAngle();
        private IVector3 v3 = new Vector3();

        private double firstX = 15060.95, firstY = 35654.16, firstZ = 5, firstH = 60, firstP = 0, firstR = -0, firstSX = 1, firstSY = 1, firstSZ = 1, firtWhen = 0;
        private double secondX = 15082.86, secondY = 35668.23, secondZ = 5, secondH = 60, secondP = 0, secondR = 0, secondSX = 1, secondSY = 1, secondSZ = 1, secondWhen = 4;
        private double thirdX = 15110.94, thirdY = 35685.96, thirdZ = 5, thirdH = 60, thirdP = 0, thirdR = 0, thirdSX = 1, thirdSY = 1, thirdSZ = 1, thirdWhen = 7;
        private double fourthX = 15173.22, fourthY = 35725.95, fourthZ = 5, fourthH = 60, fourthP = 0, fourthR = 0, fourthSX = 1, fourthSY = 1, fourthSZ = 1, fourthWhen = 10;

        private System.Guid rootId = new System.Guid();

        // 线程转发
        public int MainThreadId = 0;
        private _IRenderControlEvents_RcFrameEventHandler _rcFrame;
        

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
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
                        IEnvelope env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }
            #endregion 加载FDB场景

            cbDepthTestMode.SelectedIndex = 1;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "TableLabelSample.html";
            }
        }

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

            

            
            init();

            #region 指定每帧刷新事件
            axRenderControl1.RcFrame += new _IRenderControlEvents_RcFrameEventHandler(axRenderControl1_RcFrame);
            _rcFrame = new _IRenderControlEvents_RcFrameEventHandler(axRenderControl1_RcFrame);
            #endregion 指定每帧刷新事件
        }

        void axRenderControl1_RcFrame(int FrameIndex, double ReferencedTime)
        {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId != MainThreadId)
            {
                this.BeginInvoke(_rcFrame, new object[] { FrameIndex, ReferencedTime });
                return;
            }

            if (this.dynamicTableLabel != null && this.renderModelPoint != null)
            {
                dynamicTableLabel.SetRecord(0, 1, this.renderModelPoint.Position.X.ToString());
                dynamicTableLabel.SetRecord(1, 1, this.renderModelPoint.Position.Y.ToString());
            }
        }

        /// <summary>
        /// 加载第一种样式的静态指示标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirstLabel_Click(object sender, EventArgs e)
        {
            // 创建一个有3行2列的TableLabel
            tableLabel = axRenderControl1.ObjectManager.CreateTableLabel(3, 2, rootId);
            // 设定表头文字
            tableLabel.TitleText = "大楼信息展示";
            // 设定表格中第1行，第1列的显示文字
            tableLabel.SetRecord(0, 0, "大楼层数");
            // 第1行，第2列
            tableLabel.SetRecord(0, 1, "2");
            // 第2行，第1列
            tableLabel.SetRecord(1, 0, "大楼高度");
            // 第2行，第2列
            tableLabel.SetRecord(1, 1, "5米");
            // 第3行，第1列
            tableLabel.SetRecord(2, 0, "施工单位");
            // 第3行，第2列
            tableLabel.SetRecord(2, 1, "金隅集团");

            //标牌的位置
            position.Set(15293.62, 35805.17, 17.92);
            if(fde_point == null)
                fde_point = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            fde_point.Position = position;
            tableLabel.Position = fde_point;

            // 列宽度
            tableLabel.SetColumnWidth(0, 80);
            tableLabel.SetColumnWidth(1, 80);

            // 表的边框颜色
            tableLabel.BorderColor = System.Drawing.Color.White;
            // 表的边框的宽度
            tableLabel.BorderWidth = 2;
            // 表的背景色
            tableLabel.TableBackgroundColor = System.Drawing.Color.Gray;

            // 标题背景色
            tableLabel.TitleBackgroundColor = System.Drawing.Color.Red;

            // 第一列文本样式
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = System.Drawing.Color.White;
            headerTextAttribute.OutlineColor = System.Drawing.Color.Red;
            headerTextAttribute.Font = "黑体";
            headerTextAttribute.Bold = true;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

            // 第二列文本样式
            TextAttribute contentTextAttribute = new TextAttribute();
            contentTextAttribute.TextColor = System.Drawing.Color.Black;
            contentTextAttribute.OutlineColor = System.Drawing.Color.Red;
            contentTextAttribute.Font = "黑体";
            contentTextAttribute.Bold = false;
            contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(1, contentTextAttribute);

            // 标题文本样式
            TextAttribute capitalTextAttribute = new TextAttribute();
            capitalTextAttribute.TextColor = System.Drawing.Color.White;
            capitalTextAttribute.OutlineColor = System.Drawing.Color.Gray;
            capitalTextAttribute.Font = "华文新魏";
            capitalTextAttribute.TextSize = 14;
            capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.Bold = true;
            tableLabel.TitleTextAttribute = capitalTextAttribute;

            switch (cbDepthTestMode.SelectedIndex)
            {
                case 0:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestEnable;
                    break;
                case 1:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
                    break;
                case 2:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                    break;
            }

            angle.Set(0, -20, 0);
            axRenderControl1.Camera.LookAt(position, 30, angle);
        }

        /// <summary>
        /// 加载第一种样式的静态指示标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSecondLabel_Click(object sender, EventArgs e)
        {
            // 创建一个有3行2列的TableLabel
            tableLabel = axRenderControl1.ObjectManager.CreateTableLabel(3, 2, rootId);

            // 设定文字
            tableLabel.TitleText = "设备信息展示";
            tableLabel.SetRecord(0, 0, "生产日期");
            tableLabel.SetRecord(0, 1, "2012.8.22");
            tableLabel.SetRecord(1, 0, "生产厂商");
            tableLabel.SetRecord(1, 1, "河南许继");
            tableLabel.SetRecord(2, 0, "上次维修日期");
            tableLabel.SetRecord(2, 1, "2012.9.12");

            //标牌的位置
            position.Set(15277.55, 35737.44, 29.50);
            if (fde_point == null)
                fde_point = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            fde_point.Position = position;
            tableLabel.Position = fde_point;

            // 列宽度
            tableLabel.SetColumnWidth(0, 110);
            tableLabel.SetColumnWidth(1, 100);

            tableLabel.BorderColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            tableLabel.BorderWidth = 2;
            tableLabel.TableBackgroundColor = System.Drawing.Color.FromArgb(200, 255, 255, 165);
            tableLabel.TitleBackgroundColor = System.Drawing.Color.FromArgb(180, 122, 122, 122);

            // 第一列文本样式
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = System.Drawing.Color.FromArgb(120, 127, 64, 0);
            headerTextAttribute.OutlineColor = System.Drawing.Color.Red;
            headerTextAttribute.Font = "细黑";
            headerTextAttribute.Bold = true;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

            // 第二列文本样式
            TextAttribute contentTextAttribute = new TextAttribute();
            contentTextAttribute.TextColor = System.Drawing.Color.Black;
            contentTextAttribute.OutlineColor = System.Drawing.Color.FromArgb(125, 255, 127, 64);
            contentTextAttribute.Font = "黑体";
            contentTextAttribute.Bold = false;
            contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(1, contentTextAttribute);

            // 标题文本样式
            TextAttribute capitalTextAttribute = new TextAttribute();
            capitalTextAttribute.TextColor = System.Drawing.Color.White;
            capitalTextAttribute.OutlineColor = System.Drawing.Color.Gray;
            capitalTextAttribute.Font = "华文新魏";
            capitalTextAttribute.TextSize = 14;
            capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.Bold = true;
            tableLabel.TitleTextAttribute = capitalTextAttribute;

            switch (cbDepthTestMode.SelectedIndex)
            {
                case 0:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestEnable;
                    break;
                case 1:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
                    break;
                case 2:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                    break;
            }

            angle.Set(0, -20, 0);
            axRenderControl1.Camera.LookAt(position, 30, angle);
        }

        private void btnThirdLabel_Click(object sender, EventArgs e)
        {
            // 创建一个有3行2列的TableLabel
            tableLabel = axRenderControl1.ObjectManager.CreateTableLabel(3, 3, rootId);

            // 设定文字
            tableLabel.TitleText = "火焰特效";
            tableLabel.SetRecord(0, 0, "发射速率");
            tableLabel.SetRecord(0, 1, "50");
            tableLabel.SetRecord(0, 2, "100");
            tableLabel.SetRecord(1, 0, "移动速度");
            tableLabel.SetRecord(1, 1, "0");
            tableLabel.SetRecord(1, 2, "1");
            tableLabel.SetRecord(2, 0, "旋转速度");
            tableLabel.SetRecord(2, 1, "-1");
            tableLabel.SetRecord(2, 2, "1");

            //标牌的位置
            position.Set(15038.20, 35681.01, 5.00);
            if (fde_point == null)
                fde_point = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            fde_point.Position = position;
            tableLabel.Position = fde_point;

            tableLabel.BorderColor = System.Drawing.Color.FromArgb(255, 25, 25, 25);
            tableLabel.BorderWidth = 1.6f;
            tableLabel.TableBackgroundColor = System.Drawing.Color.FromArgb(255, 15, 23, 0);
            tableLabel.TitleBackgroundColor = System.Drawing.Color.FromArgb(255, 122, 122, 122);
            string tmpPicturePath = (strMediaPath + @"\bmp\Tulips.bmp");
            tableLabel.BackgroundImageName = tmpPicturePath;

            // 第一列文本样式
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = System.Drawing.Color.FromArgb(255, 127, 64, 0);
            headerTextAttribute.OutlineColor = System.Drawing.Color.FromArgb(255, 230, 230, 230);
            headerTextAttribute.Font = "黑体";
            headerTextAttribute.Bold = true;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

            // 第二列文本样式
            TextAttribute contentTextAttribute = new TextAttribute();
            contentTextAttribute.TextColor = System.Drawing.Color.FromArgb(255, 64, 25, 0);
            contentTextAttribute.OutlineColor = System.Drawing.Color.FromArgb(255, 230, 230, 230);
            contentTextAttribute.Font = "黑体";
            contentTextAttribute.Bold = false;
            contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(1, contentTextAttribute);
            tableLabel.SetColumnTextAttribute(2, contentTextAttribute);

            // 标题文本样式
            TextAttribute capitalTextAttribute = new TextAttribute();
            capitalTextAttribute.TextColor = System.Drawing.Color.White;
            capitalTextAttribute.OutlineColor = System.Drawing.Color.Gray;
            capitalTextAttribute.Font = "华文新魏";
            capitalTextAttribute.TextSize = 14;
            capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.Bold = true;
            tableLabel.TitleTextAttribute = capitalTextAttribute;

            switch (cbDepthTestMode.SelectedIndex)
            {
                case 0:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestEnable;
                    break;
                case 1:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
                    break;
                case 2:
                    tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                    break;
            }

            angle.Set(0, -20, 0);
            axRenderControl1.Camera.LookAt(position, 30, angle);
        }

        private void LoadDynamicTableLabel()
        {
            #region 加载一个标签
            dynamicTableLabel = axRenderControl1.ObjectManager.CreateTableLabel(2, 2, rootId);

            dynamicTableLabel.TitleText = "消防车当前位置";
            dynamicTableLabel.SetRecord(0, 0, "X:");
            dynamicTableLabel.SetRecord(0, 1, firstX.ToString());
            dynamicTableLabel.SetRecord(1, 0, "Y:");
            dynamicTableLabel.SetRecord(1, 1, firstY.ToString());

            position.Set(firstX, firstY, firstZ + 2.4);
            if (fde_point == null)
                fde_point = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            fde_point.Position = position;
            dynamicTableLabel.Position = fde_point;

            dynamicTableLabel.BorderColor = System.Drawing.Color.White;
            dynamicTableLabel.BorderWidth = 2;
            dynamicTableLabel.TableBackgroundColor = System.Drawing.Color.Gray;
            dynamicTableLabel.TitleBackgroundColor = System.Drawing.Color.Red;

            // 表头样式
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = System.Drawing.Color.Black;
            headerTextAttribute.OutlineColor = System.Drawing.Color.Red;
            headerTextAttribute.Font = "黑体";
            headerTextAttribute.Bold = true;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            dynamicTableLabel.SetColumnTextAttribute(0, headerTextAttribute);

            // 内容样式
            TextAttribute contentTextAttribute = new TextAttribute();
            contentTextAttribute.TextColor = System.Drawing.Color.Black;
            contentTextAttribute.OutlineColor = System.Drawing.Color.Red;
            contentTextAttribute.Font = "黑体";
            contentTextAttribute.Bold = false;
            contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            dynamicTableLabel.SetColumnTextAttribute(1, contentTextAttribute);

            // 标题样式
            TextAttribute capitalTextAttribute = new TextAttribute();
            capitalTextAttribute.TextColor = System.Drawing.Color.White;
            capitalTextAttribute.OutlineColor = System.Drawing.Color.Gray;
            capitalTextAttribute.Font = "华文新魏";
            capitalTextAttribute.TextSize = 14;
            capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.Bold = true;
            dynamicTableLabel.TitleTextAttribute = capitalTextAttribute;

            angle.Set(0, -20, 0);
            axRenderControl1.Camera.LookAt(position, 30, angle);
            #endregion

            #region 加载一个模型
            if (renderModelPoint == null)
            {
                string modelName = (strMediaPath + @"\osg\Vehicles\XiaoFangChe\xiaoFangChe3.OSG");
                IGeometryFactory geoFactory = new GeometryFactory();
                IModelPoint modePoint = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
                modePoint.ModelName = modelName;
                modePoint.SetCoords(firstX, firstY, firstZ, 0, 0);
                renderModelPoint = axRenderControl1.ObjectManager.CreateRenderModelPoint(modePoint, null, rootId);
            }
            #endregion

            #region 加载一个运动路径
            if (motionPath == null)
            {
                motionPath = axRenderControl1.ObjectManager.CreateMotionPath(rootId);
                // 为MotionPath添加第一个点
                position.Set(firstX, firstY, firstZ);
                angle.Set(firstH, firstP, firstR);
                scale.Set(firstSX, firstSY, firstSZ);
                motionPath.AddWaypoint(position, angle, scale, firtWhen);
                //为MotionPath添加第二个点
                position.Set(secondX, secondY, secondZ);
                angle.Set(secondH, secondP, secondR);
                scale.Set(secondSX, secondSY, secondSZ);
                motionPath.AddWaypoint(position, angle, scale, secondWhen);
                //为MotionPath添加第三个点
                position.Set(thirdX, thirdY, thirdZ);
                angle.Set(thirdH, thirdP, thirdR);
                scale.Set(thirdSX, thirdSY, thirdSZ);
                motionPath.AddWaypoint(position, angle, scale, thirdWhen);
                //为MotionPath添加第四个点
                position.Set(fourthX, fourthY, fourthZ);
                angle.Set(fourthH, fourthP, fourthR);
                scale.Set(fourthSX, fourthSY, fourthSZ);
                motionPath.AddWaypoint(position, angle, scale, fourthWhen);
            }
            #endregion

            #region 将模型和标牌同时绑定在路径上
            m = renderModelPoint as IMotionable;
            //将模型绑定到路径上

            v3.Set(0, 0, 0);
            m.Bind(motionPath, v3, 0, 0, 0);

            // 将标牌绑定到路径上
            if (dynamicTableLabel != null && motionPath != null)
            {
                m = dynamicTableLabel as IMotionable;
                v3.Set(0, 0, 6);
                m.Bind(motionPath, v3, 0, 0, 0);
            }
            #endregion
        }

        private void btnLoadLabelBindOnCar_Click(object sender, EventArgs e)
        {
            LoadDynamicTableLabel();

            position.Set(15013.714200397741, 35620.582091043034, 56.87099679938563);
            angle.Set(60.75, -26.97, 0);
            axRenderControl1.Camera.SetCamera(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (motionPath != null)
            {
                motionPath.Play();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (motionPath != null)
            {
                motionPath.Pause();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (motionPath != null)
            {
                motionPath.Stop();
            }
        }

        /// <summary>
        /// 设置三维场景可选择RenderModelPoint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveMotionObject_Click(object sender, EventArgs e)
        {
            if (renderModelPoint != null)
            {
                if (_geoEditor == null)
                {
                    _geoEditor = this.axRenderControl1.ObjectEditor;
                }
                _geoEditor.FinishEdit();
                axRenderControl1.FeatureManager.UnhighlightAll();

                axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;

                resultCode = _geoEditor.StartEditRenderGeometry(renderModelPoint, gviGeoEditType.gviGeoEdit3DMove);
                if (!resultCode)
                {
                    MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
                }
            }
        }

        private void btnRotateMotionObject_Click(object sender, EventArgs e)
        {
            if (renderModelPoint != null)
            {
                if (_geoEditor == null)
                {
                    _geoEditor = this.axRenderControl1.ObjectEditor;
                }
                _geoEditor.FinishEdit();
                axRenderControl1.FeatureManager.UnhighlightAll();

                axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;

                resultCode = _geoEditor.StartEditRenderGeometry(renderModelPoint, gviGeoEditType.gviGeoEdit3DRotate);
                if (!resultCode)
                {
                    MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
                }
            }
        }

        /// <summary>
        /// 将当前renderModelPoint所在的位置插入MotionPath中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertWaypoint_Click(object sender, EventArgs e)
        {
            if (renderModelPoint != null)
            {
                IModelPoint modelPoint = renderModelPoint.GetFdeGeometry() as IModelPoint;
                IMatrix matrix = new Matrix();
                matrix = modelPoint.AsMatrix();
                motionPath.AddWaypointByMatrix(matrix, motionPath.TotalDuration + 4);
            }
        }
        /// <summary>
        /// 取消设置三维场景可选择renderModelPoint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (_geoEditor != null)
            {
                _geoEditor.FinishEdit();
            }
            axRenderControl1.FeatureManager.UnhighlightAll();

            axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        /// <summary>
        /// 是否拾取TableLabel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSelectTableLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbSelectTableLabel.Checked)
            {
                axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
                axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectLable;
                
                axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(g_RcMouseClickSelect);
                
            }
            else
            {
                axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(g_RcMouseClickSelect);
                
            }
        }

        void g_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (PickResult == null) return;
            else
            {
                if (PickResult.Type == gviObjectType.gviObjectTableLabel)
                {
                    ITableLabelPickResult tlpr = PickResult as ITableLabelPickResult;
                    gviObjectType type = tlpr.Type;
                    ITableLabel fl = tlpr.TableLabel;
                    MessageBox.Show("拾取到" + type + "类型，名称为" + fl.TitleText);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();  //关闭应用程序
        }

    }

}
