using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using System.Xml;
using Gvitech.CityMaker.FdeGeometry;

namespace Presentation
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private IEulerAngle angle = new EulerAngle();
        private IVector3 position = new Vector3();

        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private IPresentation _presentation = null;
        private DataTable dt = null;
        private int selectPointIndex = -1;
        private int indexInPresentation = -1;

        private string wkt = string.Empty;
        private IPoint point = null;
        private IDynamicObject dynamicObject = null;
        private IPolyline line = null;
        private IRenderPolyline rline = null;
        private ISkinnedMesh skinMeshPlane = null;

        private System.Guid rootId = new System.Guid();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
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
            #endregion

            // 运动路径定位点
            point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);

            // 加载运动路径
            dynamicObject = this.axRenderControl1.ObjectManager.CreateDynamicObject(rootId);

            // 画出运动路径
            line = new GeometryFactory().CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;

            LoadDynamicObjectAndLineFromFile();
            LoadPlane();
            dynamicObject.AutoRepeat = true;
            dynamicObject.Play();

            this.axRenderControl1.RcBeforePresentationItemActivation += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcBeforePresentationItemActivationEventHandler(axRenderControl1_RcBeforePresentationItemActivation);

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "Presentation.html";
            }
        }

        void axRenderControl1_RcBeforePresentationItemActivation(string PresentationID, IPresentationStep Step)
        {
            this.Text = Step.Index + "/" + Step.Type.ToString() + ": Activation";
        }


     
        private void init()
        {
            if (_presentation != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(_presentation.Guid);
                _presentation = null;
            }
        }

        private void btnCreatePresentation_Click(object sender, EventArgs e)
        {
            init();
            _presentation = this.axRenderControl1.ObjectManager.CreatePresentation(rootId);

            //初始化节点表格
            dt = new DataTable();
            DataColumn dc0 = new DataColumn("index", typeof(int));
            DataColumn dc1 = new DataColumn("x", typeof(double));
            DataColumn dc2 = new DataColumn("y", typeof(double));
            DataColumn dc3 = new DataColumn("z", typeof(double));
            DataColumn dc4 = new DataColumn("heading", typeof(double));
            DataColumn dc5 = new DataColumn("tilt", typeof(double));
            DataColumn dc6 = new DataColumn("roll", typeof(double));
            DataColumn dc7 = new DataColumn("type", typeof(string));
            dt.Columns.AddRange(new DataColumn[] { dc0, dc1, dc2, dc3, dc4, dc5, dc6, dc7 });
            this.dataGridView1.DataSource = dt;

            //初始化节点索引
            selectPointIndex = -1;
        }

        private void btnCreateLocationStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                IPosition _pos = new Position();
                _pos.X = position.X;
                _pos.Y = position.Y;
                _pos.Altitude = position.Z;
                _pos.Heading = angle.Heading;
                _pos.Tilt = angle.Tilt;
                _pos.Roll = angle.Roll;
                IPresentationStep _step = _presentation.CreateLocationStep(gviPresentationStepContinue.gviPresentationStepContinueWait, 0, "", _pos, _presentation.Steps.Count);

                //将值写入表格
                DataRow dr = dt.NewRow();
                dr[0] = _presentation.Steps.Count - 1;
                dr[1] = position.X;
                dr[2] = position.Y;
                dr[3] = position.Z;
                dr[4] = angle.Heading;
                dr[5] = angle.Tilt;
                dr[6] = angle.Roll;
                dr[7] = GviPresentationStepType2String(_step.Type);
                dt.Rows.Add(dr);
                this.dataGridView1.Update();
            }
        }

        private void btnDeleteStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null && selectPointIndex != -1)
            {
                _presentation.DeleteStep(indexInPresentation);

                //更新表格
                dt.Rows.RemoveAt(selectPointIndex);
                for (int i = selectPointIndex; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    int oldValue = int.Parse(dr[0].ToString());
                    dr[0] = oldValue - 1;
                }
            }
        }

        private void btnReplaceLocationStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null && selectPointIndex != -1)
            {
                _presentation.DeleteStep(indexInPresentation);

                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                IPosition _pos = new Position();
                _pos.X = position.X;
                _pos.Y = position.Y;
                _pos.Altitude = position.Z;
                _pos.Heading = angle.Heading;
                _pos.Tilt = angle.Tilt;
                _pos.Roll = angle.Roll;
                IPresentationStep _step = _presentation.CreateLocationStep(gviPresentationStepContinue.gviPresentationStepContinueWait, 0, "", _pos, indexInPresentation);

                //更新表格
                DataRow newDr = dt.Rows[selectPointIndex];
                newDr[0] = indexInPresentation;
                newDr[1] = position.X;
                newDr[2] = position.Y;
                newDr[3] = position.Z;
                newDr[4] = angle.Heading;
                newDr[5] = angle.Tilt;
                newDr[6] = angle.Roll;
                newDr[7] = GviPresentationStepType2String(_step.Type);
                this.dataGridView1.Update();
            }
        }


        private void PlayFromStartIndex_Click(object sender, EventArgs e)
        {
            if (_presentation != null && selectPointIndex != -1)
            {
                _presentation.Play(indexInPresentation);
            }
        }

        private void btnPlayStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null && selectPointIndex != -1)
            {
                _presentation.PlayStep(indexInPresentation);
            }
        }

        private void btnPreviousStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                _presentation.PreviousStep();
            }
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                _presentation.NextStep();
            }
        }

        private void btnResetPresentation_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                _presentation.ResetPresentation();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
                _presentation.Pause();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
                _presentation.Continue();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
                _presentation.Stop();
        }

        private void cbLoopRoute_CheckedChanged(object sender, EventArgs e)
        {
            if (_presentation != null)
                _presentation.LoopRoute = cbLoopRoute.Checked;
        }


        #region DataGrid事件
        /// <summary>
        /// 单击行头，设置index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selectPointIndex = e.RowIndex;

            DataRow dr = dt.Rows[selectPointIndex];
            indexInPresentation = int.Parse(dr[0].ToString());
        }

        /// <summary>
        /// 双击行头，相机飞入节点所在位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_presentation != null)
            {
                selectPointIndex = e.RowIndex;
                _presentation.PlayStep(selectPointIndex);
            }
        }
        #endregion


        private string GviPresentationStepType2String(gviPresentationStepType type)
        {
            switch (type)
            {
                case gviPresentationStepType.gviPresentationStepTypeCaption:
                    return "Caption";
                case gviPresentationStepType.gviPresentationStepTypeClearCaption:
                    return "ClearCaption";
                case gviPresentationStepType.gviPresentationStepTypeDynamicObject:
                    return "DynamicObject";
                case gviPresentationStepType.gviPresentationStepTypeFlightSpeedFactor:
                    return "FlightSpeedFactor";
                case gviPresentationStepType.gviPresentationStepTypeGroupOrObject:
                    return "GroupOrObject";
                case gviPresentationStepType.gviPresentationStepTypeLocation:
                    return "Location";
                case gviPresentationStepType.gviPresentationStepTypeMessage:
                    return "Message";
                case gviPresentationStepType.gviPresentationStepTypePlayAnotherPresentation:
                    return "PlayAnotherPresentation";
                case gviPresentationStepType.gviPresentationStepTypePlayTimeAnimation:
                    return "PlayTimeAnimation";
                case gviPresentationStepType.gviPresentationStepTypeRestartDynamicObject:
                    return "RestartDynamicObject";
                case gviPresentationStepType.gviPresentationStepTypeSetTime:
                    return "SetTime";
                case gviPresentationStepType.gviPresentationStepTypeTimeSlider:
                    return "TimeSlider";
                case gviPresentationStepType.gviPresentationStepTypeTool:
                    return "Tool";
                case gviPresentationStepType.gviPresentationStepTypeUnderGroundMode:
                    return "UnderGroundMode";
                default:
                    return "";
            }
        }

        private void btnStartRecord_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                _presentation.StartRecord();
            }
        }

        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                _presentation.StopRecord();
            }
        }

        private void btnShowEditor_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                _presentation.ShowEditor();
            }
        }

        private void cbPlayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                switch(cbPlayMode.SelectedIndex)
                {
                    case 0:
                        _presentation.PlayMode = gviPresentationPlayMode.gviPresentationPlayAutomatic;
                        break;
                    case 1:
                        _presentation.PlayMode = gviPresentationPlayMode.gviPresentationPlayManual;
                        break;
                }
            }
        }

        private void btnCreateCaptionStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                _presentation.CreateCaptionStep(gviPresentationStepContinue.gviPresentationStepContinueWait, 2, "", "当前是第" + _presentation.Steps.Count + "个step，当前时间是:" + DateTime.Now, -1, _presentation.Steps.Count);
            }            
        }

        private void btnCreateFlightSpeedFactorStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null)
            {
                gviPresentationStepFlightSpeed speed;
                switch (cbFlightSpeedFactor.SelectedIndex)
                {
                    case 0:
                        speed = gviPresentationStepFlightSpeed.gviPresentationStepFlightVerySlow;
                        break;
                    case 1:
                        speed = gviPresentationStepFlightSpeed.gviPresentationStepFlightSlow;
                        break;
                    case 2:
                        speed = gviPresentationStepFlightSpeed.gviPresentationStepFlightNormal;
                        break;
                    case 3:
                        speed = gviPresentationStepFlightSpeed.gviPresentationStepFlightFast;
                        break;
                    case 4:
                        speed = gviPresentationStepFlightSpeed.gviPresentationStepFlightVeryFast;
                        break;
                    default:
                        speed = gviPresentationStepFlightSpeed.gviPresentationStepFlightNormal;
                        break;
                }
                _presentation.CreateFlightSpeedFactorStep(gviPresentationStepContinue.gviPresentationStepContinueWait, 0, "", speed, _presentation.Steps.Count);
            }
        }

        private void btnCreateFollowDynamicObjectStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null && dynamicObject != null)
            {
                _presentation.CreateFollowDynamicObjectStep(gviPresentationStepContinue.gviPresentationStepContinueWait, 0, "", dynamicObject.Guid, _presentation.Steps.Count);
            }
        }

        private void LoadDynamicObjectAndLineFromFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + @"\MotionPath.xml");
            wkt = xmlDoc.SelectSingleNode("root/WKT").InnerText;
            // 指定坐标系与xml里的相同
            dynamicObject.CrsWKT = xmlDoc.SelectSingleNode("root/WKT").InnerText;
            point.SpatialCRS = new CRSFactory().CreateFromWKT(dynamicObject.CrsWKT) as ISpatialCRS;
            line.SpatialCRS = new CRSFactory().CreateFromWKT(dynamicObject.CrsWKT) as ISpatialCRS;

            XmlNodeList nodes = xmlDoc.SelectNodes("root/Waypoint");
            int i = 0;
            foreach (XmlNode node in nodes)
            {
                double x = double.Parse(node.SelectSingleNode("X").InnerText);
                double y = double.Parse(node.SelectSingleNode("Y").InnerText);
                double z = double.Parse(node.SelectSingleNode("Z").InnerText);
                position.Set(x, y, z);
                point.Position = position;
                if (line.PointCount == 0)
                {
                    line.StartPoint = point;
                }
                else
                    line.AddPointAfter(i - 1, point);
                i++;
                double when = double.Parse(node.SelectSingleNode("When").InnerText);
                dynamicObject.AddWaypoint2(point, when);
                this.axRenderControl1.ObjectManager.CreateRenderPoint(point, null, rootId);
            }

            ICurveSymbol cur = new CurveSymbol();
            cur.Color = System.Drawing.Color.Red;
            cur.Width = -2;
            rline = this.axRenderControl1.ObjectManager.CreateRenderPolyline(line, cur, rootId);
        }

        private void LoadPlane()
        {
            // 加载直升飞机
            string fileName = (strMediaPath + @"\x\Vehicles\wrj.X");
            if (skinMeshPlane == null)
            {
                // 构造ModelPoint
                IGeometryFactory gf = new GeometryFactory();
                IModelPoint mp = gf.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                mp.ModelName = fileName;
                mp.SpatialCRS = new CRSFactory().CreateFromWKT(wkt) as ISpatialCRS;
                // 设置位置
                IMatrix matrix = new Matrix();
                matrix.MakeIdentity();
                matrix.SetTranslate(line.GetPoint(0).Position);
                mp.FromMatrix(matrix);
                // 创建骨骼动画
                skinMeshPlane = this.axRenderControl1.ObjectManager.CreateSkinnedMesh(mp, rootId);
                if (skinMeshPlane == null)
                {
                    MessageBox.Show("骨骼动画创建失败！");
                    return;
                }
                skinMeshPlane.Loop = true;
                skinMeshPlane.Play();
                skinMeshPlane.MaxVisibleDistance = 1000;
                skinMeshPlane.ViewingDistance = 100;

                // 绑定到运动路径
                IMotionable m = skinMeshPlane as IMotionable;
                position.Set(0, 0, 0);
                m.Bind2(dynamicObject, position, 0, 0, 0);
            }
        }

        private void btnCreateRestartDynamicObjectStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null && dynamicObject != null)
            {
                _presentation.CreateRestartDynamicObjectStep(gviPresentationStepContinue.gviPresentationStepContinueWait, 0, "", dynamicObject.Guid, _presentation.Steps.Count);
            }
        }

        private void btnCreateHideObjectStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null && rline != null)
            {
                _presentation.CreateShowObjectStep(gviPresentationStepContinue.gviPresentationStepContinueWait, 5, "", rline.Guid, false, _presentation.Steps.Count);
            }
        }

        private void btnCreateShowObjectStep_Click(object sender, EventArgs e)
        {
            if (_presentation != null && rline != null)
            {
                _presentation.CreateShowObjectStep(gviPresentationStepContinue.gviPresentationStepContinueWait, 5, "", rline.Guid, true, _presentation.Steps.Count);
            }
        }

    }
}
