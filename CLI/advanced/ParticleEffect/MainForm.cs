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
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;

namespace ParticleEffect
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private ISpatialCRS datasetCRS = null;

        private IGeometryFactory gf = new GeometryFactory();
        private IPoint position = null;
        private IEulerAngle angle = new EulerAngle();
        private IVector3 scale = new Vector3();
        private IVector3 v3 = new Vector3();

        private ParticleEffect fire = null;
        private ParticleEffect smoke = null;
        private IMotionPath motionPath = null;
        private IRenderModelPoint renderModelPoint;
        private WaterEffect water = null;
        private ISkinnedMesh skinMeshCharacter = null;
        private ISkinnedMesh skinMeshPlane = null;
        private ISkinnedMesh skinMeshTree = null;
        private ISkinnedMesh skinMeshTree1 = null;
        private ISkinnedMesh skinMeshTree2 = null;
        private ISkinnedMesh curSkinMesh = null;
        private IMotionable m = null;

        private double fireX = 15045.35, fireY = 35686.89, fireZ = 11.50;
        private double smokeX = 15043.98, smokeY = 35692.61, smokeZ = 16.48;
        private double waterX = 15031.07, waterY = 35680.73, waterZ = 5;

        private double firstX = 15060.95, firstY = 35654.16, firstZ = 5, firstH = 60, firstP = 0, firstR = -0, firstSX = 1, firstSY = 1, firstSZ = 1, firtWhen = 0;
        private double secondX = 15082.86, secondY = 35668.23, secondZ = 5, secondH = 60, secondP = 0, secondR = 0, secondSX = 1, secondSY = 1, secondSZ = 1, secondWhen = 12;
        private double thirdX = 15110.94, thirdY = 35685.96, thirdZ = 5, thirdH = 60, thirdP = 0, thirdR = 0, thirdSX = 1, thirdSY = 1, thirdSZ = 1, thirdWhen = 21;
        private double fourthX = 15173.22, fourthY = 35725.95, fourthZ = 5, fourthH = 60, fourthP = 0, fourthR = 0, fourthSX = 1, fourthSY = 1, fourthSZ = 1, fourthWhen = 30;

        private double gugeX = 15044.62, gugeY = 35677.03, gugeZ = 5.20;
        private double treeX = 15070.56, treeY = 35664.54, treeZ = 5.28;
        private double tree1X = 15080.46, tree1Y = 35670.95, tree1Z = 5.28;
        private double tree2X = 15090.16, tree2Y = 35677.20, tree2Z = 5.28;
        private double planeX = 15050.90, planeY = 35697.24, planeZ = 28;

        private double fire0X = 15220.13, fire0Y = 35757.37, fire0Z = 5.20;
        private double smoke0X = 15025.25, smoke0Y = 35718.34, smoke0Z = 5.20;
        private double explosion0X = 15090.16, explosion0Y = 35677.20, explosion0Z = 5.20;
        private double rocketTailFlameX = 15050.90, rocketTailFlameY = 35697.24, rocketTailFlameZ = 28;

        private System.Guid rootId = new System.Guid();

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            // Unknown
            this.axRenderControl1.Initialize(true, ps);
            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;

            // 平面坐标系
            //string wkt = @"PROJCS['Beijing_1954_3_Degree_GK_CM_102E',GEOGCS['GCS_Beijing_1954',DATUM['D_Beijing_1954',SPHEROID['Krasovsky_1940',6378245.0,298.3]],PRIMEM['Greenwich',0.0],UNIT['Degree',0.0174532925199433]],PROJECTION['Gauss_Kruger'],PARAMETER['False_Easting',500000],PARAMETER['False_Northing',0],PARAMETER['Central_Meridian',102],PARAMETER['Scale_Factor',1],PARAMETER['Latitude_Of_Origin',0],UNIT['metre',1]]";
            //this.axRenderControl1.Initialize2(wkt, ps);
            // 球面1
            //this.axRenderControl1.Initialize(false, ps);
            // 球面2
            //IGeographicCRS crs = new CRSFactory().CreateWGS84();
            //this.axRenderControl1.Initialize2(crs.AsWKT(), ps);     

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
                datasetCRS = dataset.SpatialReference;
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

            this.axRenderControl1.Camera.FlyTime = 0;
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "ParticleEffect.html";
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            this.cbComplexType.SelectedIndex = 0;

            init();

            position = gf.CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            position.SpatialCRS = datasetCRS;

            CommonUnity.RenderHelper = this.axRenderControl1;
            CommonUnity.RenderHelper.Terrain.DemAvailable = false;
            try
            {
                LoadData();
                position.SetCoords(fireX, fireY, fireZ, 0, 0);
                angle.Set(0, -10, 0);
                CommonUnity.RenderHelper.Camera.LookAt2(position, 20, angle);
            }
            catch
            {
                MessageBox.Show("加载数据失败！");
            }
        }

      
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            LoadXFileCharacter();
            LoadXFileTrees();
            LoadWater();
            LoadFire();
            LoadSmoke();
            LoadMotionPath();
            LoadModel();
            BindPath();
            LoadXFilePlane();
            LoadComplexPaiticleEffect();
        }

        /// <summary>
        /// 加载人物骨骼动画
        /// </summary>
        private void LoadXFileCharacter()
        {
            string fileName = (strMediaPath + @"\x\Character\QiYeYuanGong.X");
            if (skinMeshCharacter == null)
            {
                // 构造ModelPoint
                IGeometryFactory gf = new GeometryFactory();
                IModelPoint mp = gf.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                mp.ModelName = fileName;
                mp.SpatialCRS = datasetCRS;
                // 设置位置
                IMatrix matrix = new Matrix();
                matrix.MakeIdentity();
                v3.Set(gugeX, gugeY, gugeZ);
                matrix.SetTranslate(v3);
                mp.FromMatrix(matrix);
                // 创建骨骼动画
                skinMeshCharacter = CommonUnity.RenderHelper.ObjectManager.CreateSkinnedMesh(mp, rootId);
                if (skinMeshCharacter == null)
                {
                    MessageBox.Show("骨骼动画创建失败！");
                    return;
                }
                skinMeshCharacter.Duration = 1;  //人物的那个骨骼动画播放速度调快一倍
                skinMeshCharacter.Loop = true;
                skinMeshCharacter.Play();
                skinMeshCharacter.MaxVisibleDistance = 1000;
            }
        }

        /// <summary>
        /// 加载动态树骨骼动画
        /// </summary>
        private void LoadXFileTrees()
        {
            {
                string fileName = (strMediaPath + @"\x\Trees\tree.X");
                if (skinMeshTree == null)
                {
                    // 构造ModelPoint
                    IGeometryFactory gf = new GeometryFactory();
                    IModelPoint mp = gf.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                    mp.ModelName = fileName;
                    mp.SpatialCRS = datasetCRS;
                    // 设置位置
                    IMatrix matrix = new Matrix();
                    matrix.MakeIdentity();
                    v3.Set(treeX, treeY, treeZ);
                    matrix.SetTranslate(v3);
                    mp.FromMatrix(matrix);
                    mp.SelfScale(0.75, 0.75, 0.75);
                    // 创建骨骼动画
                    skinMeshTree = CommonUnity.RenderHelper.ObjectManager.CreateSkinnedMesh(mp, rootId);
                    if (skinMeshTree == null)
                    {
                        MessageBox.Show("骨骼动画创建失败！");
                        return;
                    }
                    skinMeshTree.Duration = 4;
                    skinMeshTree.Loop = true;
                    skinMeshTree.Play();
                    skinMeshTree.MaxVisibleDistance = 1000;
                }
            }

            {
                string fileName = (strMediaPath + @"\x\Trees\tree1.X");
                if (skinMeshTree1 == null)
                {
                    // 构造ModelPoint
                    IGeometryFactory gf = new GeometryFactory();
                    IModelPoint mp = gf.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                    mp.ModelName = fileName;
                    mp.SpatialCRS = datasetCRS;
                    // 设置位置
                    IMatrix matrix = new Matrix();
                    matrix.MakeIdentity();
                    v3.Set(tree1X, tree1Y, tree1Z);
                    matrix.SetTranslate(v3);
                    mp.FromMatrix(matrix);
                    mp.SelfScale(0.5, 0.5, 0.5);
                    // 创建骨骼动画
                    skinMeshTree1 = CommonUnity.RenderHelper.ObjectManager.CreateSkinnedMesh(mp, rootId);
                    if (skinMeshTree == null)
                    {
                        MessageBox.Show("骨骼动画创建失败！");
                        return;
                    }
                    skinMeshTree1.Duration = 3;
                    skinMeshTree1.Loop = true;
                    skinMeshTree1.Play();
                    skinMeshTree1.MaxVisibleDistance = 1000;
                }
            }

            {
                string fileName = (strMediaPath + @"\x\Trees\tree2.X");
                if (skinMeshTree2 == null)
                {
                    // 构造ModelPoint
                    IGeometryFactory gf = new GeometryFactory();
                    IModelPoint mp = gf.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                    mp.ModelName = fileName;
                    mp.SpatialCRS = datasetCRS;
                    // 设置位置
                    IMatrix matrix = new Matrix();
                    matrix.MakeIdentity();
                    v3.Set(tree2X, tree2Y, tree2Z);
                    matrix.SetTranslate(v3);
                    mp.FromMatrix(matrix);
                    mp.SelfScale(0.25, 0.25, 0.25);
                    // 创建骨骼动画
                    skinMeshTree2 = CommonUnity.RenderHelper.ObjectManager.CreateSkinnedMesh(mp, rootId);
                    if (skinMeshTree2 == null)
                    {
                        MessageBox.Show("骨骼动画创建失败！");
                        return;
                    }
                    skinMeshTree2.Loop = true;
                    skinMeshTree2.Play();
                    skinMeshTree2.MaxVisibleDistance = 1000;
                }
            }            
        }

        /// <summary>
        /// 加载直升机骨骼动画
        /// </summary>
        private void LoadXFilePlane()
        {
            string fileName = (strMediaPath + @"\x\Vehicles\wrj.X");
            if (skinMeshPlane == null)
            {
                // 构造ModelPoint
                IGeometryFactory gf = new GeometryFactory();
                IModelPoint mp = gf.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                mp.ModelName = fileName;
                mp.SpatialCRS = datasetCRS;
                // 设置位置
                IMatrix matrix = new Matrix();
                matrix.MakeIdentity();
                v3.Set(planeX, planeY, planeZ);
                matrix.SetTranslate(v3);
                mp.FromMatrix(matrix);
                // 创建骨骼动画
                skinMeshPlane = CommonUnity.RenderHelper.ObjectManager.CreateSkinnedMesh(mp, rootId);
                if (skinMeshPlane == null)
                {
                    MessageBox.Show("骨骼动画创建失败！");
                    return;
                }
                skinMeshPlane.Loop = true;
                skinMeshPlane.Play();
                skinMeshPlane.MaxVisibleDistance = 1000;
            }
        }

        /// <summary>
        /// 加载火特效
        /// </summary>
        private void LoadFire()
        {
            fire = new FireEffect(rootId);
            fire.Draw();
            fire.Refresh();
            fire.Start();
            position.SetCoords(fireX, fireY, fireZ, 0, 0);
            fire.SetPosition(position);
        }

        /// <summary>
        /// 水的粒子特效
        /// </summary>
        private void LoadWater()
        {
            water = new WaterEffect(rootId);
            water.Draw();
            water.Refresh();
            water.Start();
            position.SetCoords(waterX, waterY, waterZ, 0, 0);
            water.SetPosition(position);
        }

        /// <summary>
        /// 加载烟特效
        /// </summary>
        private void LoadSmoke()
        {
            smoke = new SmokeEffect(rootId);
            smoke.Draw();
            smoke.Refresh();
            smoke.Start();
            position.SetCoords(smokeX, smokeY, smokeZ, 0, 0);
            smoke.SetPosition(position);
        }

        private ComplexParticleEffect fire0 = null;
        private ComplexParticleEffect smoke0 = null;
        private ComplexParticleEffect explosion0 = null;
        private ComplexParticleEffect rocketTailFlame0 = null;
        /// <summary>
        /// 加载组合粒子特效
        /// </summary>
        private void LoadComplexPaiticleEffect()
        {
            fire0 = new ComplexParticleEffect();
            fire0.Create(gviComplexParticleEffectType.gviComplexParticleEffectFire_0);
            //fire0.Refresh();
            position.SetCoords(fire0X, fire0Y, fire0Z, 0, 0);
            fire0.SetPosition(position);

            smoke0 = new ComplexParticleEffect();
            smoke0.Create(gviComplexParticleEffectType.gviComplexParticleEffectSmoke_0);
            //smoke0.Refresh();
            position.SetCoords(smoke0X, smoke0Y, smoke0Z, 0, 0);
            smoke0.SetPosition(position);

            explosion0 = new ComplexParticleEffect();
            explosion0.Create(gviComplexParticleEffectType.gviComplexParticleEffectExplosion_0);
            //explosion0.Refresh();
            position.SetCoords(explosion0X, explosion0Y, explosion0Z, 0, 0);
            explosion0.SetPosition(position);

            rocketTailFlame0 = new ComplexParticleEffect();
            rocketTailFlame0.Create(gviComplexParticleEffectType.gviComplexParticleEffectRocketTailFlame);
            //rocketTailFlame0.Refresh();
            position.SetCoords(rocketTailFlameX, rocketTailFlameY, rocketTailFlameZ, 0, 0);
            rocketTailFlame0.SetPosition(position);
        }

        /// <summary>
        /// 加载MotionPath
        /// </summary>
        private void LoadMotionPath()
        {
            motionPath = CommonUnity.RenderHelper.ObjectManager.CreateMotionPath(rootId);
            motionPath.CrsWKT = datasetCRS.AsWKT();
            //为MotionPath添加第一个点
            position.SetCoords(firstX, firstY, firstZ, 0, 0);
            angle.Set(firstH, firstP, firstR);
            scale.Set(firstSX, firstSY, firstSZ);
            motionPath.AddWaypoint2(position, angle, scale, firtWhen);
            //为MotionPath添加第二个点
            position.SetCoords(secondX, secondY, secondZ, 0, 0);
            angle.Set(secondH, secondP, secondR);
            scale.Set(secondSX, secondSY, secondSZ);
            motionPath.AddWaypoint2(position, angle, scale, secondWhen);
            //为MotionPath添加第三个点
            position.SetCoords(thirdX, thirdY, thirdZ, 0, 0);
            angle.Set(thirdH, thirdP, thirdR);
            scale.Set(thirdSX, thirdSY, thirdSZ);
            motionPath.AddWaypoint2(position, angle, scale, thirdWhen);
            //为MotionPath添加第四个点
            position.SetCoords(fourthX, fourthY, fourthZ, 0, 0);
            angle.Set(fourthH, fourthP, fourthR);
            scale.Set(fourthSX, fourthSY, fourthSZ);
            motionPath.AddWaypoint2(position, angle, scale, fourthWhen);

        }

        /// <summary>
        /// 加载模型
        /// </summary>
        private void LoadModel()
        {
            string modelName = (strMediaPath + @"\osg\Vehicles\XiaoFangChe\xiaoFangChe3.OSG");
            IGeometryFactory geoFactory = new GeometryFactory();
            IModelPoint modelPoint = (IModelPoint)geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            modelPoint.ModelName = modelName;
            modelPoint.SetCoords(firstX, firstY, firstZ, 0, 0);
            modelPoint.SpatialCRS = datasetCRS;
            renderModelPoint = CommonUnity.RenderHelper.ObjectManager.CreateRenderModelPoint(modelPoint, null, rootId);
            renderModelPoint.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }

        /// <summary>
        /// 绑定运动路径 
        /// </summary>
        private void BindPath()
        {
            if (renderModelPoint != null && water != null && motionPath != null)
            {
                m = renderModelPoint as IMotionable;
                v3.Set(0, 0, 0);
                m.Bind(motionPath, v3, 0, 0, 0);


                m = water.ParticleEffectObject as IMotionable;
                v3.Set(0, 0, 2.5);
                m.Bind(motionPath, v3, 0, 0, 0);

                m = skinMeshCharacter as IMotionable;
                v3.Set(-10, -7, 0);
                m.Bind(motionPath, v3, 0, 0, 0);
            }          
        }

        /// <summary>
        /// 打开属性面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProperty_Click(object sender, EventArgs e)
        {
            PropertyFrm pf = null;
            if (comboBox1.SelectedIndex == 0)
            {
                pf = new PropertyFrm(this.fire);
                pf.Text = "火的属性";
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                pf = new PropertyFrm(this.smoke);
                pf.Text = "烟的属性";
            }
            else
            {
                pf = new PropertyFrm(this.water);
                pf.Text = "水的属性";
            }
            pf.Owner = this;
            pf.Show();
        }

        /// <summary>
        /// 定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocate_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                position.SetCoords(fireX, fireY, fireZ, 0, 0);
                angle.Set(0, -10, 0);
                CommonUnity.RenderHelper.Camera.LookAt2(position, 20, angle);
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                //IEnvelope env = this.smoke.ParticleEffectObject.Envelope;
                //position.SetCoords((env.MaxX + env.MinX) / 2, (env.MaxY + env.MinY) / 2, (env.MaxZ + env.MinZ) / 2, 0, 0);
                //angle.Set(0, -10, 0);
                //CommonUnity.RenderHelper.Camera.LookAt2(position, 30, angle);

                CommonUnity.RenderHelper.Camera.FlyToObject(this.smoke.ParticleEffectObject.Guid, gviActionCode.gviActionFlyTo);
            }
            else
            {
                CommonUnity.RenderHelper.Camera.FlyToObject(this.water.ParticleEffectObject.Guid, gviActionCode.gviActionFlyTo);
            }
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (motionPath != null)
            {
                CommonUnity.RenderHelper.Camera.FlyTime = 0;
                //position.SetCoords(15013.714200397741, 35620.582091043034, 56.87099679938563, 0, 0);
                //angle.Set(60.75, -26.97, 0);
                //CommonUnity.RenderHelper.Camera.SetCamera2(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);

                IVector3 p = new Vector3();
                p.Set(15013.714200397741, 35620.582091043034, 56.87099679938563);
                angle.Set(60.75, -26.97, 0);
                CommonUnity.RenderHelper.Camera.SetCamera(p, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                motionPath.Play();
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (motionPath != null)
            {
                motionPath.Pause();
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (motionPath != null)
            {
                motionPath.Stop();
            }
        }
        /// <summary>
        /// 骨骼动画播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSkinnedMeshPlay_Click(object sender, EventArgs e)
        {
            if (curSkinMesh != null)
            {
                curSkinMesh.Loop = true;
                curSkinMesh.Play();
            }
        }
        /// <summary>
        /// 骨骼动画暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSkinnedMeshPause_Click(object sender, EventArgs e)
        {
            if (curSkinMesh != null)
            {
                curSkinMesh.Pause();
            }
        }
        /// <summary>
        /// 骨骼动画停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSkinnedMeshStop_Click(object sender, EventArgs e)
        {
            if (curSkinMesh != null)
            {
                curSkinMesh.Loop = false;
                curSkinMesh.Stop();
            }
        }

        /// <summary>
        /// 骨骼动画定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocateSkinmesh_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                if (skinMeshCharacter != null)
                {                    
                    CommonUnity.RenderHelper.Camera.FlyToObject(this.skinMeshCharacter.Guid, gviActionCode.gviActionFlyTo);
                    curSkinMesh = skinMeshCharacter;
                }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                if (skinMeshTree != null)
                {
                    position.SetCoords(treeX, treeY, treeZ, 0, 0);
                    angle.Set(0, -45, 0);
                    CommonUnity.RenderHelper.Camera.LookAt2(position, 20, angle);
                    //CommonUnity.RenderHelper.Camera.FlyToObject(this.skinMeshTree.Guid, gviActionCode.gviActionFlyTo);
                    curSkinMesh = skinMeshTree;
                }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                if (skinMeshTree1 != null)
                {
                    position.SetCoords(tree1X, tree1Y, tree1Z, 0, 0);
                    angle.Set(0, -45, 0);
                    CommonUnity.RenderHelper.Camera.LookAt2(position, 20, angle);
                    //CommonUnity.RenderHelper.Camera.FlyToObject(this.skinMeshTree1.Guid, gviActionCode.gviActionFlyTo);
                    curSkinMesh = skinMeshTree1;
                }
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                if (skinMeshTree2 != null)
                {
                    position.SetCoords(tree2X, tree2Y, tree2Z, 0, 0);
                    angle.Set(0, -45, 0);
                    CommonUnity.RenderHelper.Camera.LookAt2(position, 20, angle);
                    //CommonUnity.RenderHelper.Camera.FlyToObject(this.skinMeshTree2.Guid, gviActionCode.gviActionFlyTo);
                    curSkinMesh = skinMeshTree2;
                }
            }
            else if (comboBox2.SelectedIndex == 4)
            {
                if (skinMeshPlane != null)
                {
                    CommonUnity.RenderHelper.Camera.FlyToObject(this.skinMeshPlane.Guid, gviActionCode.gviActionFlyTo);
                    curSkinMesh = skinMeshPlane;
                }
            }
        }

        private void btnComplexProperty_Click(object sender, EventArgs e)
        {
            ComplexPropertyFrm pf = null;
            switch (cbComplexType.SelectedIndex)
            {
                case 0:
                    {
                        pf = new ComplexPropertyFrm(this.fire0);
                        pf.Text = "火的属性";
                    }
                    break;
                case 5:
                    {
                        pf = new ComplexPropertyFrm(this.smoke0);
                        pf.Text = "烟的属性";
                    }
                    break;
                case 8:
                    {
                        pf = new ComplexPropertyFrm(this.explosion0);
                        pf.Text = "水的属性";
                    }
                    break;
                case 17:
                    {
                        pf = new ComplexPropertyFrm(this.rocketTailFlame0);
                        pf.Text = "水的属性";
                    }
                    break;
            } 
            pf.Owner = this;
            pf.Show();
        }

        private void btnComplexLocate_Click(object sender, EventArgs e)
        {
            switch (cbComplexType.SelectedIndex)
            {
                case 0:
                    CommonUnity.RenderHelper.Camera.FlyToObject(this.fire0.ComplexParticleEffectObject.Guid, gviActionCode.gviActionFlyTo);
                    break;
                case 5:
                    CommonUnity.RenderHelper.Camera.FlyToObject(this.smoke0.ComplexParticleEffectObject.Guid, gviActionCode.gviActionFlyTo);
                    this.smoke0.Stop();
                    this.smoke0.Play();
                    break;
                case 8:
                    CommonUnity.RenderHelper.Camera.FlyToObject(this.explosion0.ComplexParticleEffectObject.Guid, gviActionCode.gviActionFlyTo);
                    this.explosion0.Play();
                    break;
                case 17:
                    CommonUnity.RenderHelper.Camera.FlyToObject(this.rocketTailFlame0.ComplexParticleEffectObject.Guid, gviActionCode.gviActionFlyTo);
                    this.rocketTailFlame0.Play();
                    break;   
            }            
        }

    }
}
