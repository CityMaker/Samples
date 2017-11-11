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

namespace TerrainRoute
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private IEulerAngle angle = new EulerAngle();
        private IVector3 position = new Vector3();

        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private ITerrainRoute _route = null;
        private DataTable dt = null;
        private int selectPointIndex = -1;

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

            // 加载FDB场景
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

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "TerrainRoute.html";
            }
        }

        private void init()
        {
            if (_route != null)
            {
                this.axRenderControl1.ObjectManager.DeleteObject(_route.Guid);
                _route = null;
            }
        }

        private void btnCreateRoute_Click(object sender, EventArgs e)
        {
            init();
            _route = this.axRenderControl1.ObjectManager.CreateTerrainRoute(rootId);

            //初始化节点表格
            dt = new DataTable();
            DataColumn dc0 = new DataColumn("index", typeof(int));
            DataColumn dc1 = new DataColumn("x", typeof(double));
            DataColumn dc2 = new DataColumn("y", typeof(double));
            DataColumn dc3 = new DataColumn("z", typeof(double));
            DataColumn dc4 = new DataColumn("heading", typeof(double));
            DataColumn dc5 = new DataColumn("tilt", typeof(double));
            DataColumn dc6 = new DataColumn("roll", typeof(double));
            DataColumn dc7 = new DataColumn("speed", typeof(double));
            dt.Columns.AddRange(new DataColumn[] { dc0, dc1, dc2, dc3, dc4, dc5, dc6, dc7 });
            this.dataGridView1.DataSource = dt;

            //初始化节点索引
            selectPointIndex = -1;
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            if (_route != null)
            {             
                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                _route.AddWaypoint(position, angle, Convert.ToDouble(this.numSpeed.Text));

                //将值写入表格
                DataRow dr = dt.NewRow();
                dr[0] = _route.WaypointsNumber - 1;
                dr[1] = position.X;
                dr[2] = position.Y;
                dr[3] = position.Z;
                dr[4] = angle.Heading;
                dr[5] = angle.Tilt;
                dr[6] = angle.Roll;
                dr[7] = Convert.ToDouble(this.numSpeed.Text);
                dt.Rows.Add(dr);
                this.dataGridView1.Update();
            }
        }

        private void btnDelPoint_Click(object sender, EventArgs e)
        {
            if (_route != null && selectPointIndex != -1)
            {
                _route.DeleteWaypoint(selectPointIndex);

                //更新表格
                dt.Rows.RemoveAt(selectPointIndex);
                for (int i = selectPointIndex; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dr[0] = i;
                }
            }
        }

        private void btnReplacePoint_Click(object sender, EventArgs e)
        {
            if (_route != null && selectPointIndex != -1)
            {
                this.axRenderControl1.Camera.GetCamera(out position, out angle);
                _route.ModifyWaypoint(selectPointIndex, position, angle, Convert.ToDouble(this.numSpeed.Text));

                //更新表格
                DataRow newDr = dt.NewRow();
                newDr[0] = selectPointIndex;
                newDr[1] = position.X;
                newDr[2] = position.Y;
                newDr[3] = position.Z;
                newDr[4] = angle.Heading;
                newDr[5] = angle.Tilt;
                newDr[6] = angle.Roll;
                newDr[7] = Convert.ToDouble(this.numSpeed.Text);
                dt.Rows.InsertAt(newDr, selectPointIndex);
                dt.Rows.RemoveAt(selectPointIndex + 1);
                this.dataGridView1.Update();
                selectPointIndex = -1;
            }
        }

        private void btnPlayRoute_Click(object sender, EventArgs e)
        {
            if (_route != null)
                _route.Play();
        }

        private void btnPauseRoute_Click(object sender, EventArgs e)
        {
            if (_route != null)
                _route.Pause();
        }

        private void btnStopRoute_Click(object sender, EventArgs e)
        {
            if (_route != null)
                _route.Stop();
        }

        private void cbAutoRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (_route != null)
            {
                _route.AutoRepeat = this.cbAutoRepeat.Checked;
            }
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
        }

        /// <summary>
        /// 双击行头，相机飞入节点所在位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_route != null)
            {
                selectPointIndex = e.RowIndex;
                double speed;
                _route.GetWaypoint(selectPointIndex, out position, out angle, out speed);
                this.axRenderControl1.Camera.SetCamera(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);
            }
        }
        #endregion

        private void btnImportFromXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XML文件|*.xml";
            if(System.IO.Directory.Exists(strMediaPath))
            {
                od.InitialDirectory = strMediaPath + @"\xml";
            }
            od.RestoreDirectory = true;
            if (od.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(od.FileName))
                {
                    StreamReader sr = new StreamReader(od.FileName);
                    string xmlstring = sr.ReadToEnd();
                    sr.Close();
                    _route = null;
                    _route = this.axRenderControl1.ObjectManager.CreateTerrainRoute(rootId);
                    _route.FromXml(xmlstring);

                    if (_route.WaypointsNumber > 0)
                    {
                        //初始化节点表格
                        dt = new DataTable();
                        DataColumn dc0 = new DataColumn("index", typeof(int));
                        DataColumn dc1 = new DataColumn("x", typeof(double));
                        DataColumn dc2 = new DataColumn("y", typeof(double));
                        DataColumn dc3 = new DataColumn("z", typeof(double));
                        DataColumn dc4 = new DataColumn("heading", typeof(double));
                        DataColumn dc5 = new DataColumn("tilt", typeof(double));
                        DataColumn dc6 = new DataColumn("roll", typeof(double));
                        DataColumn dc7 = new DataColumn("speed", typeof(double));
                        dt.Columns.AddRange(new DataColumn[] { dc0, dc1, dc2, dc3, dc4, dc5, dc6, dc7 });
                        this.dataGridView1.DataSource = dt;

                        for (int i = 0; i < _route.WaypointsNumber; i++)
                        {
                            double speed;
                            _route.GetWaypoint(i, out position, out angle, out speed);

                            //将值写入表格
                            DataRow dr = dt.NewRow();
                            dr[0] = i;
                            dr[1] = position.X;
                            dr[2] = position.Y;
                            dr[3] = position.Z;
                            dr[4] = angle.Heading;
                            dr[5] = angle.Tilt;
                            dr[6] = angle.Roll;
                            dr[7] = speed;
                            dt.Rows.Add(dr);
                        }
                        this.dataGridView1.Update();
                    }
                }
            }
        }

        private void btnExportAsXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.AddExtension = true;
            sd.DefaultExt = "xml";
            sd.Filter = "XML文件|*.xml";
            if(System.IO.Directory.Exists(strMediaPath))
            {
                sd.InitialDirectory = strMediaPath + @"\xml";
            }
            sd.RestoreDirectory = true;
            if (sd.ShowDialog() == DialogResult.OK)
            {
                String final = sd.FileName;
                if (sd.FileName.LastIndexOf(".xml") == -1)
                    final = String.Format("{0}.xml", sd.FileName);

                String xmlstring = null;
                if (_route != null)
                    xmlstring = _route.AsXml();
                System.IO.File.WriteAllText(final, xmlstring);
                MessageBox.Show("导出成功");
            }
        }

    }
}
