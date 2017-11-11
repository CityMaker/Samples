using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeDataInterop;
using System.Threading;
using Gvitech.CityMaker.FdeGeometry;

namespace DataImport
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = new Hashtable();  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名  
        private Hashtable fcuidMap = new Hashtable(); //FeatureClassUID,FeatureClass

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
                string tmpFDBPath = (strMediaPath + @"\empty.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                //设置资源目录树DataSource节点
                myTreeNode catalogDsNode = new myTreeNode(System.IO.Path.GetFileNameWithoutExtension(tmpFDBPath), NodeObjectType.DataSource, ds);
                this.tv_CatalogTree.Nodes.Add(catalogDsNode);

                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);

                //设置资源目录树DataSet节点
                myTreeNode catalogFdsNode = new myTreeNode(setnames[0], NodeObjectType.DataSet, dataset);
                catalogDsNode.Nodes.Add(catalogFdsNode);

                //遍历FeatureClass
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                //fcMap = new Hashtable(fcnames.Length);
                //fcuidMap = new Hashtable(fcnames.Length);
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
                    fcuidMap.Add(fc.Guid, fc);
                    //设置资源目录FeatureClass节点
                    myTreeNode catalogFcNode = new myTreeNode(name, NodeObjectType.FeatureClass, fc);
                    catalogFdsNode.Nodes.Add(catalogFcNode);
                    this.tv_CatalogTree.ExpandAll();
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
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }

            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DataImport.html";
            }

        }

        private void tv_CatalogTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                myTreeNode selectNode = this.tv_CatalogTree.GetNodeAt(e.X,e.Y) as myTreeNode;
                this.tv_CatalogTree.SelectedNode = selectNode;
                if (selectNode.nodeType == NodeObjectType.DataSet)
                {
                    cMenuCatalog.Items.Clear();
                    ToolStripMenuItem menuImport = new ToolStripMenuItem("外部数据导入");
                    menuImport.Click +=new EventHandler(menuImport_Click);
                    cMenuCatalog.Items.Add(menuImport);

                    ToolStripMenuItem menuGdbImport = new ToolStripMenuItem("GDB导入");
                    menuGdbImport.Click +=new EventHandler(menuGdbImport_Click);
                    cMenuCatalog.Items.Add(menuGdbImport);
                    cMenuCatalog.Show(Cursor.Position);
                }
            }
        }

        private DataImportProgressDlg bar = null;
        public delegate void IsFinishDelegate(bool b);
        private IsFinishDelegate _IsFinishDelegate = null;
        private IFuncCallBack callback = new IFuncCallBack();
        private ReplicatingHandler replicatingHandler = null;

        private void menuGdbImport_Click(object sender, EventArgs e)
        {
            myTreeNode curNode = this.tv_CatalogTree.SelectedNode as myTreeNode;
            IFeatureDataSet fds = curNode.Tag as IFeatureDataSet;
            bool b = false;
            Thread importdataThread = null;
            IDataInteropFactory dintf = new DataInteropFactory();
            IDataInterop op = null;
            try
            {
                if (fds.IsCheckOut)
                {
                    MessageBox.Show("数据已被签出，导入终止！");
                    return;
                }
                replicatingHandler = new ReplicatingHandler(UpdateProgress);
                callback.Replicating += UpdateProgressInvoking;
                _IsFinishDelegate = new IsFinishDelegate(IsFinish);

                string sGdbFilePath = "";
                if(System.IO.Directory.Exists(strMediaPath))
                    sGdbFilePath = (strMediaPath + @"\gdb\gdbtest.gdb");
                else
                {
                    MessageBox.Show("请不要随意更改SDK目录名");
                    return;
                }
                int index = sGdbFilePath.LastIndexOf('.');
                string ext = sGdbFilePath.Substring(index + 1, sGdbFilePath.Length - index - 1);
                string fn = "Shapestwst";

                if (ext.ToLower().Equals("gdb"))
                {
                    if (fn.Length >= 128)
                    {
                        MessageBox.Show("文件{0}名称长度超长，不得超过128个字节", fn);
                        return;
                    }
                    IPropertySet ps = new PropertySet();
                    ps.SetProperty("FILENAME", sGdbFilePath);
                    ps.SetProperty("LAYER", "\\Shapestwst");
                    op = dintf.CreateDataInterop(
                                gviDataConnectionType.gviOgrConnectionFileGDB,
                                 ps);
                    if (op != null)
                    {
                        if (op.LayersInfo.Count != 0)
                        {
                            ICRSFactory coorFactory = new CRSFactory();
                            ISpatialCRS coorSysProject = coorFactory.CreateFromWKT(op.LayersInfo.Get(0).CrsWKT) as ISpatialCRS;
                            if (fds.SpatialReference.Name.Equals("\\unnamed\\") || fds.SpatialReference.IsSame(coorSysProject))
                            {
                                if (fn.LastIndexOf('.') != -1)
                                    fn = fn.Replace('.', '-');
                                //判断要名称为fn的FeatureClass是否已存在，如果存在，fn名称添加后缀
                                String[] collection = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                                if (collection != null)
                                {
                                    while (CheckImportFCName(fn, collection))
                                    {
                                        int i = GetPostfix(fn);
                                        string tem = String.Format("({0})", i - 1);
                                        if (fn.LastIndexOf(tem) == -1)
                                            fn = String.Format("{0}({1})", fn, i);
                                        else
                                            fn = fn.Replace(tem, String.Format("({0})", i));
                                    }
                                }
                                importdataThread = new Thread(new ParameterizedThreadStart(ImportGDB));
                                importdataThread.Name = "ImportDataThread";
                                importdataThread.Start(new object[] { fds.DataSource.ConnectionInfo.ToConnectionString(), fds.Name, sGdbFilePath, "\\Shapestwst",fn });
                                bar = new DataImportProgressDlg();
                                if (bar.ShowDialog() == DialogResult.Cancel)
                                {
                                    b = false;
                                    //fds.DeleteByName(fn);
                                }
                                else
                                {
                                    b = true;
                                }
                                if (b)
                                {
                                    //添加新节点到当前节点下
                                    IFeatureClass newFc = fds.OpenFeatureClass(fn);
                                    // 找到空间列字段
                                    List<string> newGeoNames = new List<string>();
                                    IFieldInfoCollection newFieldinfos = newFc.GetFields();
                                    for (int i = 0; i < newFieldinfos.Count; i++)
                                    {
                                        IFieldInfo fieldinfo = newFieldinfos.Get(i);
                                        if (null == fieldinfo)
                                            continue;
                                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                        if (null == geometryDef)
                                            continue;
                                        newGeoNames.Add(fieldinfo.Name);
                                    }
                                    fcMap.Add(newFc, newGeoNames);
                                    fcuidMap.Add(newFc.Guid, newFc);
                                    myTreeNode newNode = new myTreeNode(fn, NodeObjectType.FeatureClass, null);
                                    curNode.Nodes.Add(newNode);
                                    //加入RenderControl
                                    bool hasfly = false;
                                    foreach (string geoName in newGeoNames)
                                    {
                                        IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                                        newFc, geoName, null, null, rootId);

                                        if (!hasfly)
                                        {
                                            IFieldInfoCollection fieldinfos = newFc.GetFields();
                                            IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                            IEnvelope env = geometryDef.Envelope;
                                            if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                                                env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                                                continue;
                                            IEulerAngle angle = new EulerAngle();
                                            angle.Set(0, -20, 0);
                                            this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                                        }
                                        hasfly = true;
                                    }
                                    MessageBox.Show("导入成功！");
                                }
                                else
                                {
                                    MessageBox.Show("导入失败！");
                                }
                            }
                            else
                            {
                                MessageBox.Show("坐标系冲突！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("导入失败，请检查数据完整性！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("导入失败！");
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
                    MessageBox.Show("需要标准runtime授权");
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                if (callback != null)
                    callback.Replicating -= UpdateProgressInvoking;
                if (op != null)
                {
                    //Marshal.ReleaseComObject(op);
                    op = null;
                }
            }
        }

        private void menuImport_Click(object sender, EventArgs e)
        {
            myTreeNode curNode = this.tv_CatalogTree.SelectedNode as myTreeNode;
            IFeatureDataSet fds = curNode.Tag as IFeatureDataSet;
            bool b = false;
            Thread importdataThread = null;
            IDataInteropFactory dintf = new DataInteropFactory();
            //IFeatureClass fc = null;
            IDataInterop op = null;
            try
            {
                if (fds.IsCheckOut)
                {
                    MessageBox.Show("数据已被签出，导入终止！");
                    return;
                }
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.CheckPathExists = true;
                dlg.Multiselect = false;
                dlg.Filter = "shp文件|*.shp|dwg文件|*.dwg|las文件|*.las|skp文件|*.skp|ifc文件|*.ifc";
                if(System.IO.Directory.Exists(strMediaPath))
                {
                    dlg.InitialDirectory = strMediaPath;
                }
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    replicatingHandler = new ReplicatingHandler(UpdateProgress);
                    callback.Replicating += UpdateProgressInvoking;
                    _IsFinishDelegate = new IsFinishDelegate(IsFinish);
                    int index = dlg.SafeFileName.LastIndexOf('.');
                    string ext = dlg.SafeFileName.Substring(index + 1, dlg.SafeFileName.Length - index - 1);
                    string fn = dlg.SafeFileName.Substring(0, index);
                    //导入ShapeFile
                    if (ext.ToLower().Equals("shp"))
                    {
                        if (fn.Length >= 128)
                        {
                            MessageBox.Show("文件{0}名称长度超长，不得超过128个字节",fn);
                            return;
                        }
                        IPropertySet ps = new PropertySet();
                        ps.SetProperty("FILENAME", dlg.FileName);
                        op = dintf.CreateDataInterop(
                                    gviDataConnectionType.gviOgrConnectionShp,
                                     ps);
                        if (op != null)
                        {
                            if (op.LayersInfo.Count != 0)
                            {
                                ICRSFactory coorFactory = new CRSFactory();
                                ISpatialCRS coorSysProject = coorFactory.CreateFromWKT(op.LayersInfo.Get(0).CrsWKT) as ISpatialCRS;
                                if (fds.SpatialReference.Name.Equals("\\unnamed\\") || fds.SpatialReference.IsSame(coorSysProject))
                                {
                                    if (fn.LastIndexOf('.') != -1)
                                        fn = fn.Replace('.', '-');
                                    //判断要名称为fn的FeatureClass是否已存在，如果存在，fn名称添加后缀
                                    String[] collection = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                                    if (collection != null)
                                    {
                                        while (CheckImportFCName(fn, collection))
                                        {
                                            int i = GetPostfix(fn);
                                            string tem = String.Format("({0})", i - 1);
                                            if (fn.LastIndexOf(tem) == -1)
                                                fn = String.Format("{0}({1})", fn, i);
                                            else
                                                fn = fn.Replace(tem, String.Format("({0})", i));
                                        }
                                    }
                                    importdataThread = new Thread(new ParameterizedThreadStart(ImportSHP));
                                    importdataThread.Name = "ImportDataThread";
                                    importdataThread.Start(new object[] { fds.DataSource.ConnectionInfo.ToConnectionString(), fds.Name, dlg.FileName, fn });
                                    bar = new DataImportProgressDlg();
                                    if (bar.ShowDialog() == DialogResult.Cancel)
                                    {
                                        b = false;
                                        //fds.DeleteByName(fn);
                                    }
                                    else
                                    {
                                        b = true;
                                    }
                                    if (b)
                                    {
                                        //添加新节点到当前节点下
                                        IFeatureClass newFc = fds.OpenFeatureClass(fn);
                                        // 找到空间列字段
                                        List<string> newGeoNames = new List<string>();
                                        IFieldInfoCollection newFieldinfos = newFc.GetFields();
                                        for (int i = 0; i < newFieldinfos.Count; i++)
                                        {
                                            IFieldInfo fieldinfo = newFieldinfos.Get(i);
                                            if (null == fieldinfo)
                                                continue;
                                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                            if (null == geometryDef)
                                                continue;
                                            newGeoNames.Add(fieldinfo.Name);
                                        }
                                        fcMap.Add(newFc, newGeoNames);
                                        fcuidMap.Add(newFc.Guid, newFc);
                                        myTreeNode newNode = new myTreeNode(fn, NodeObjectType.FeatureClass, null);
                                        curNode.Nodes.Add(newNode);
                                        //加入RenderControl
                                        bool hasfly = false;
                                        foreach (string geoName in newGeoNames)
                                        {
                                            IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                                            newFc, geoName, null, null, rootId);

                                            if (!hasfly)
                                            {
                                                IFieldInfoCollection fieldinfos = newFc.GetFields();
                                                IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                                                IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                                IEnvelope env = geometryDef.Envelope;
                                                if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                                                    env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                                                    continue;
                                                IEulerAngle angle = new EulerAngle();
                                                angle.Set(0, -20, 0);
                                                this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                                            }
                                            hasfly = true;
                                        }
                                        MessageBox.Show("导入成功！");
                                    }
                                    else
                                    {
                                        MessageBox.Show("导入失败！");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("坐标系冲突！");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("导入失败，请检查数据完整性！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("导入失败！");
                        }
                    }
                    //导入DWG
                    else if (ext.ToLower().Equals("dwg"))
                    {
                        string prefix = TrimPath(dlg.FileName);
                        Hashtable list = new Hashtable();
                        IDataInteropFactory dint1f = new DataInteropFactory();
                        IPropertySet ps = new PropertySet();
                        ps.SetProperty("FILENAME", dlg.FileName);
                        ps.SetProperty("TOLERANCE", 0.02);
                        IDataInterop op1 = dint1f.CreateDataInterop(gviDataConnectionType.gviOgrConnectionDWG, ps);
                        String[] collection = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                        for (int i = 0; i < op1.LayersInfo.Count; i++)
                        {
                            string s = string.Format("{0}-{1}", prefix.Replace('.', '-'), op1.LayersInfo.Get(i).Name);
                            //判断要名称为fn的FeatureClass是否已存在，如果存在，fn名称添加后缀
                            if (collection != null)
                            {
                                while (CheckImportFCName(s, collection))
                                {
                                    int l = GetPostfix(s);
                                    string tem = String.Format("({0})", l - 1);
                                    if (s.LastIndexOf(tem) == -1)
                                        s = String.Format("{0}({1})", s, l);
                                    else
                                        s = s.Replace(tem, String.Format("({0})", l));
                                }
                            }
                            list.Add(op1.LayersInfo.Get(i).Name, s);
                        }
                        //Marshal.ReleaseComObject(op1);
                        op1 = null;

                        bar = new DataImportProgressDlg();
                        importdataThread = new Thread(new ParameterizedThreadStart(ImportDwg));
                        importdataThread.Name = "ImportDataThread";
                        importdataThread.Start(new object[] { fds.DataSource.ConnectionInfo.ToConnectionString(), fds.Name, dlg.FileName, list });
                        if (bar.ShowDialog() == DialogResult.Cancel)
                        {
                            MessageBox.Show("导入失败！");
                        }
                        else
                        {
                            //添加新节点到当前节点下
                            bool hasfly = false;
                            foreach (DictionaryEntry de in list)
                            {
                                //添加新节点到当前节点下
                                IFeatureClass newFc = fds.OpenFeatureClass(de.Value.ToString());
                                // 找到空间列字段
                                List<string> newGeoNames = new List<string>();
                                IFieldInfoCollection newFieldinfos = newFc.GetFields();
                                for (int i = 0; i < newFieldinfos.Count; i++)
                                {
                                    IFieldInfo fieldinfo = newFieldinfos.Get(i);
                                    if (null == fieldinfo)
                                        continue;
                                    IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                    if (null == geometryDef)
                                        continue;
                                    newGeoNames.Add(fieldinfo.Name);
                                }
                                fcMap.Add(newFc, newGeoNames);
                                fcuidMap.Add(newFc.Guid, newFc);
                                myTreeNode newNode = new myTreeNode(de.Value.ToString(), NodeObjectType.FeatureClass, null);
                                curNode.Nodes.Add(newNode);
                                //加入RenderControl
                                
                                foreach (string geoName in newGeoNames)
                                {
                                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                                    newFc, geoName, null, null, rootId);

                                    if (!hasfly)
                                    {
                                        IFieldInfoCollection fieldinfos = newFc.GetFields();
                                        IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                        IEnvelope env = geometryDef.Envelope;
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
                            MessageBox.Show("导入成功！");
                        }
                    }
                    else if (ext.ToLower().Equals("las"))
                    {
                        if (fn.Length >= 128)
                        {
                            MessageBox.Show("文件{0}名称长度超长，不得超过128个字节", fn);
                            return;
                        }
                        IPropertySet ps = new PropertySet();
                        ps.SetProperty("FILENAME", dlg.FileName);
                        op = dintf.CreateDataInterop(
                                    gviDataConnectionType.gviOgrConnectionLAS,
                                     ps);
                        if (op != null)
                        {
                            if (op.LayersInfo.Count != 0)
                            {
                                ICRSFactory coorFactory = new CRSFactory();
                                ISpatialCRS coorSysProject = coorFactory.CreateFromWKT(op.LayersInfo.Get(0).CrsWKT) as ISpatialCRS;
                                if (fds.SpatialReference.Name.Equals("\\unnamed\\") || fds.SpatialReference.IsSame(coorSysProject))
                                {
                                    if (fn.LastIndexOf('.') != -1)
                                        fn = fn.Replace('.', '-');
                                    //判断要名称为fn的FeatureClass是否已存在，如果存在，fn名称添加后缀
                                    String[] collection = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                                    if (collection != null)
                                    {
                                        while (CheckImportFCName(fn, collection))
                                        {
                                            int i = GetPostfix(fn);
                                            string tem = String.Format("({0})", i - 1);
                                            if (fn.LastIndexOf(tem) == -1)
                                                fn = String.Format("{0}({1})", fn, i);
                                            else
                                                fn = fn.Replace(tem, String.Format("({0})", i));
                                        }
                                    }
                                    importdataThread = new Thread(new ParameterizedThreadStart(ImportLas));
                                    importdataThread.Name = "ImportDataThread";
                                    importdataThread.Start(new object[] { fds.DataSource.ConnectionInfo.ToConnectionString(), fds.Name, dlg.FileName, fn });
                                    bar = new DataImportProgressDlg();
                                    if (bar.ShowDialog() == DialogResult.Cancel)
                                    {
                                        b = false;
                                        //fds.DeleteByName(fn);
                                    }
                                    else
                                    {
                                        b = true;
                                    }
                                    if (b)
                                    {
                                        //添加新节点到当前节点下
                                        IFeatureClass newFc = fds.OpenFeatureClass(fn);
                                        // 找到空间列字段
                                        List<string> newGeoNames = new List<string>();
                                        IFieldInfoCollection newFieldinfos = newFc.GetFields();
                                        for (int i = 0; i < newFieldinfos.Count; i++)
                                        {
                                            IFieldInfo fieldinfo = newFieldinfos.Get(i);
                                            if (null == fieldinfo)
                                                continue;
                                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                            if (null == geometryDef)
                                                continue;
                                            newGeoNames.Add(fieldinfo.Name);
                                        }
                                        fcMap.Add(newFc, newGeoNames);
                                        fcuidMap.Add(newFc.Guid, newFc);
                                        myTreeNode newNode = new myTreeNode(fn, NodeObjectType.FeatureClass, null);
                                        curNode.Nodes.Add(newNode);
                                        //加入RenderControl
                                        bool hasfly = false;
                                        foreach (string geoName in newGeoNames)
                                        {
                                            IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                                            newFc, geoName, null, null, rootId);

                                            if (!hasfly)
                                            {
                                                IFieldInfoCollection fieldinfos = newFc.GetFields();
                                                IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                                                IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                                IEnvelope env = geometryDef.Envelope;
                                                if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                                                    env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                                                    continue;
                                                IEulerAngle angle = new EulerAngle();
                                                angle.Set(0, -20, 0);
                                                this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                                            }
                                            hasfly = true;
                                        }
                                        MessageBox.Show("导入成功！");
                                    }
                                    else
                                    {
                                        MessageBox.Show("导入失败！");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("坐标系冲突！");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("导入失败，请检查数据完整性！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("导入失败！");
                        }
                    }
                    else if (ext.ToLower().Equals("skp"))
                    {
                        if (fn.Length >= 128)
                        {
                            MessageBox.Show("文件{0}名称长度超长，不得超过128个字节", fn);
                            return;
                        }
                        IPropertySet ps = new PropertySet();
                        ps.SetProperty("FILENAME", dlg.FileName);
                        op = dintf.CreateDataInterop(
                                    gviDataConnectionType.gviOgrConnectionSKP,
                                     ps);
                        if (op != null)
                        {
                            if (op.LayersInfo.Count != 0)
                            {
                                if (fn.LastIndexOf('.') != -1)
                                    fn = fn.Replace('.', '-');
                                //判断要名称为fn的FeatureClass是否已存在，如果存在，fn名称添加后缀
                                String[] collection = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                                if (collection != null)
                                {
                                    while (CheckImportFCName(fn, collection))
                                    {
                                        int i = GetPostfix(fn);
                                        string tem = String.Format("({0})", i - 1);
                                        if (fn.LastIndexOf(tem) == -1)
                                            fn = String.Format("{0}({1})", fn, i);
                                        else
                                            fn = fn.Replace(tem, String.Format("({0})", i));
                                    }
                                }
                                importdataThread = new Thread(new ParameterizedThreadStart(ImportSKP));
                                importdataThread.Name = "ImportDataThread";
                                importdataThread.Start(new object[] { fds.DataSource.ConnectionInfo.ToConnectionString(), fds.Name, dlg.FileName, fn });
                                bar = new DataImportProgressDlg();
                                if (bar.ShowDialog() == DialogResult.Cancel)
                                {
                                    b = false;
                                    //fds.DeleteByName(fn);
                                }
                                else
                                {
                                    b = true;
                                }
                                if (b)
                                {
                                    //添加新节点到当前节点下
                                    IFeatureClass newFc = fds.OpenFeatureClass(fn);
                                    // 找到空间列字段
                                    List<string> newGeoNames = new List<string>();
                                    IFieldInfoCollection newFieldinfos = newFc.GetFields();
                                    for (int i = 0; i < newFieldinfos.Count; i++)
                                    {
                                        IFieldInfo fieldinfo = newFieldinfos.Get(i);
                                        if (null == fieldinfo)
                                            continue;
                                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                        if (null == geometryDef)
                                            continue;
                                        newGeoNames.Add(fieldinfo.Name);
                                    }
                                    fcMap.Add(newFc, newGeoNames);
                                    fcuidMap.Add(newFc.Guid, newFc);
                                    myTreeNode newNode = new myTreeNode(fn, NodeObjectType.FeatureClass, null);
                                    curNode.Nodes.Add(newNode);
                                    //加入RenderControl
                                    bool hasfly = false;
                                    foreach (string geoName in newGeoNames)
                                    {
                                        IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                                        newFc, geoName, null, null, rootId);

                                        if (!hasfly)
                                        {
                                            IFieldInfoCollection fieldinfos = newFc.GetFields();
                                            IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                            IEnvelope env = geometryDef.Envelope;
                                            if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                                                env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                                                continue;
                                            IEulerAngle angle = new EulerAngle();
                                            angle.Set(0, -20, 0);
                                            this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                                        }
                                        hasfly = true;
                                    }
                                    MessageBox.Show("导入成功！");
                                }
                                else
                                {
                                    MessageBox.Show("导入失败！");
                                }
                            }
                            else
                            {
                                MessageBox.Show("导入失败，请检查数据完整性！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("导入失败，请检查计算机上是否已安装vs2005运行时！");
                        }
                    }
                    else if (ext.ToLower().Equals("ifc"))
                    {
                        if (fn.Length >= 128)
                        {
                            MessageBox.Show("文件{0}名称长度超长，不得超过128个字节", fn);
                            return;
                        }
                        IPropertySet ps = new PropertySet();
                        ps.SetProperty("FILENAME", dlg.FileName);
                        op = dintf.CreateDataInterop(
                                    gviDataConnectionType.gviOgrConnectionIFC,
                                     ps);
                        if (op != null)
                        {
                            if (op.LayersInfo.Count != 0)
                            {
                                ICRSFactory coorFactory = new CRSFactory();
                                ISpatialCRS coorSysProject = coorFactory.CreateFromWKT(op.LayersInfo.Get(0).CrsWKT) as ISpatialCRS;
                                if (fds.SpatialReference.Name.Equals("\\unnamed\\") || fds.SpatialReference.IsSame(coorSysProject))
                                {
                                    if (fn.LastIndexOf('.') != -1)
                                        fn = fn.Replace('.', '-');
                                    //判断要名称为fn的FeatureClass是否已存在，如果存在，fn名称添加后缀
                                    String[] collection = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                                    if (collection != null)
                                    {
                                        while (CheckImportFCName(fn, collection))
                                        {
                                            int i = GetPostfix(fn);
                                            string tem = String.Format("({0})", i - 1);
                                            if (fn.LastIndexOf(tem) == -1)
                                                fn = String.Format("{0}({1})", fn, i);
                                            else
                                                fn = fn.Replace(tem, String.Format("({0})", i));
                                        }
                                    }
                                    importdataThread = new Thread(new ParameterizedThreadStart(ImportIFC));
                                    importdataThread.Name = "ImportDataThread";
                                    importdataThread.Start(new object[] { fds.DataSource.ConnectionInfo.ToConnectionString(), fds.Name, dlg.FileName, fn });
                                    bar = new DataImportProgressDlg();
                                    if (bar.ShowDialog() == DialogResult.Cancel)
                                    {
                                        b = false;
                                        //fds.DeleteByName(fn);
                                    }
                                    else
                                    {
                                        b = true;
                                    }
                                    if (b)
                                    {
                                        //添加新节点到当前节点下
                                        IFeatureClass newFc = fds.OpenFeatureClass(fn);
                                        // 找到空间列字段
                                        List<string> newGeoNames = new List<string>();
                                        IFieldInfoCollection newFieldinfos = newFc.GetFields();
                                        for (int i = 0; i < newFieldinfos.Count; i++)
                                        {
                                            IFieldInfo fieldinfo = newFieldinfos.Get(i);
                                            if (null == fieldinfo)
                                                continue;
                                            IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                            if (null == geometryDef)
                                                continue;
                                            newGeoNames.Add(fieldinfo.Name);
                                        }
                                        fcMap.Add(newFc, newGeoNames);
                                        fcuidMap.Add(newFc.Guid, newFc);
                                        myTreeNode newNode = new myTreeNode(fn, NodeObjectType.FeatureClass, null);
                                        curNode.Nodes.Add(newNode);
                                        //加入RenderControl
                                        bool hasfly = false;
                                        foreach (string geoName in newGeoNames)
                                        {
                                            IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                                            newFc, geoName, null, null, rootId);

                                            if (!hasfly)
                                            {
                                                IFieldInfoCollection fieldinfos = newFc.GetFields();
                                                IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                                                IGeometryDef geometryDef = fieldinfo.GeometryDef;
                                                IEnvelope env = geometryDef.Envelope;
                                                if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                                                    env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                                                    continue;
                                                IEulerAngle angle = new EulerAngle();
                                                angle.Set(0, -20, 0);
                                                this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                                            }
                                            hasfly = true;
                                        }
                                        MessageBox.Show("导入成功！");
                                    }
                                    else
                                    {
                                        MessageBox.Show("导入失败！");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("坐标系冲突！");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("导入失败，请检查数据完整性！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("导入失败！");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
                    MessageBox.Show("需要标准runtime授权");
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                if (callback != null)
                    callback.Replicating -= UpdateProgressInvoking;
                if (op != null)
                {
                    //Marshal.ReleaseComObject(op);
                    op = null;
                }
            }
        }

        private bool UpdateProgress(IFeatureProgress preg)
        {
            bar.lbl_Info.Text = GetReplicateOperationName(preg.CurrentOperation);
            if (preg.CurrentOperation == gviReplicateOperation.gviReplicateReplicateData)
            {
                if (preg.TotalFeatureCount > 0)
                {
                    bar.lbl_ImportCount.Visible = false;
                    bar.progressBar.Visible = true;
                    int p = preg.CurrentFeatureCount * 100 / preg.TotalFeatureCount;
                    bar.progressBar.Value = p;
                    bar.progressBar.Maximum = 101;
                }
                else
                {
                    bar.lbl_ImportCount.Visible = true;
                    bar.progressBar.Visible = false;
                    bar.lbl_Info.Text = string.Format("正在执行第{0}个要素导入。", preg.CurrentFeatureCount.ToString());
                }
              
            }
            if (bar.CallbackCancel)
            {
                preg.Cancel();
            }
            return true;
        }

        private bool UpdateProgressInvoking(IFeatureProgress preg)
        {
            IAsyncResult result = bar.BeginInvoke(replicatingHandler, preg);
            bar.EndInvoke(result);
            return true;
        }

        private void IsFinish(bool sender)
        {
            if (sender)
                bar.DialogResult = DialogResult.OK;
            else
                bar.DialogResult = DialogResult.Cancel;
        }

        private bool CheckImportFCName(String fcname, String[] collection)
        {
            foreach (String s in collection)
            {
                if (fcname.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetPostfix(String fcname)
        {
            int index1 = fcname.LastIndexOf('(');
            int index2 = fcname.LastIndexOf(')');
            if (index1 == -1 || index2 == -1)
                return 1;
            String inter = fcname.Substring(index1 + 1, index2 - index1 - 1);
            return int.Parse(inter) + 1;
        }

        String GetReplicateOperationName(gviReplicateOperation gviRO)
        {
            switch (gviRO)
            {
                case gviReplicateOperation.gviReplicateInitialize:
                    return "数据准备";

                case gviReplicateOperation.gviReplicateFinished:
                    return "完成";

                case gviReplicateOperation.gviReplicateExtractSchema:
                    return "ExtractSchema";

                case gviReplicateOperation.gviReplicateExtractData:
                    return "ExtractData";

                case gviReplicateOperation.gviReplicateCreateSchema:
                    return "CreateSchema";

                case gviReplicateOperation.gviReplicateReplicateData:
                    return "正在导入数据";

                case gviReplicateOperation.gviReplicateCreateSpatialIndex:
                    return "CreatSpatialIndex";

                case gviReplicateOperation.gviReplicateCreateRenderIndex:
                    return "CreateRenderIndex";

                case gviReplicateOperation.gviReplicateCommitTransaction:
                    return "CommitTransaction";

                case gviReplicateOperation.gviReplicateTruncateDelta:
                    return "TruncateDelta";

                case gviReplicateOperation.gviReplicateReleaseLock:
                    return "ReleaseLock";
                default:
                    return null;
            }
        }

        private void toolStripPan_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        private void toolStripSelect_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
        }

        private String TrimPath(String path)
        {
            int len1 = path.LastIndexOf(".");
            int len2 = path.LastIndexOf("\\");
            if (len1 == -1 || len2 == -1)
                return "";
            else
                return path.Substring(len2 + 1, len1 - len2 - 1);
        }

        private void ImportSHP(object param)
        {
            IDataInteropFactory dintf = new DataInteropFactory();
            IDataInterop op = null;
            IDataSource ds = null;
            IFeatureDataSet fds = null;
            try
            {
                object[] obj = param as object[];
                string cnnStr = obj[0] as string;
                string fdsname = obj[1] as string;
                string file = obj[2] as string;
                string fn = obj[3] as string;
                IPropertySet ps = new PropertySet();
                ps.SetProperty("FILENAME", file);
                op = dintf.CreateDataInterop(
                            gviDataConnectionType.gviOgrConnectionShp,
                             ps);
                IFieldInfoCollection fields = op.LayersInfo.Get(0).FieldInfos;
                for (int l = 0; l < fields.Count; l++)
                {
                    if (fields.Get(l).FieldType == gviFieldType.gviFieldFID)
                    {
                        fields.RemoveAt(l);
                        break;
                    }
                }
                ds = new DataSourceFactory().OpenDataSourceByString(cnnStr);
                if (ds != null)
                {
                    fds = ds.OpenFeatureDataset(fdsname);
                    if (fds != null)
                    {
                        //op.OnProcessing = callback;
                        op.StepValue = 100;
                        IPropertySet strMap = new PropertySet();
                        strMap.SetProperty(op.LayersInfo.Get(0).Name, fn);
                        int count = op.ImportLayers(fds, strMap);
                        if (count == 1)
                            bar.Invoke(_IsFinishDelegate, true);
                        else
                            bar.Invoke(_IsFinishDelegate, false);
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void ImportLas(object param)
        {
            IDataInteropFactory dintf = new DataInteropFactory();
            IDataInterop op = null;
            IDataSource ds = null;
            IFeatureDataSet fds = null;
            try
            {
                object[] obj = param as object[];
                string cnnStr = obj[0] as string;
                string fdsname = obj[1] as string;
                string file = obj[2] as string;
                string fn = obj[3] as string;
                IPropertySet ps = new PropertySet();
                ps.SetProperty("FILENAME", file);
                op = dintf.CreateDataInterop(
                            gviDataConnectionType.gviOgrConnectionLAS,
                             ps);
                IFieldInfoCollection fields = op.LayersInfo.Get(0).FieldInfos;
                for (int l = 0; l < fields.Count; l++)
                {
                    if (fields.Get(l).FieldType == gviFieldType.gviFieldFID)
                    {
                        fields.RemoveAt(l);
                        break;
                    }
                }
                ds = new DataSourceFactory().OpenDataSourceByString(cnnStr);
                if (ds != null)
                {
                    fds = ds.OpenFeatureDataset(fdsname);
                    if (fds != null)
                    {
                        //op.OnProcessing = callback;
                        op.StepValue = 10000;
                        IPropertySet strMap = new PropertySet();
                        strMap.SetProperty(op.LayersInfo.Get(0).Name, fn);
                        int count = op.ImportLayers(fds, strMap);
                        if (count == 1)
                            bar.Invoke(_IsFinishDelegate, true);
                        else
                            bar.Invoke(_IsFinishDelegate, false);
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void ImportDwg(object param)
        {
            string cnnStr, fdsname, file;
            bool b = true;
            IDataInteropFactory dintf = new DataInteropFactory();
            IDataInterop op = null;
            IDataSource ds = null;
            IFeatureDataSet fds = null;
            try
            {
                object[] obj = param as object[];
                cnnStr = obj[0] as string;
                fdsname = obj[1] as string;
                file = obj[2] as string;
                Hashtable list = obj[3] as Hashtable;
                ds = new DataSourceFactory().OpenDataSourceByString(cnnStr);
                if (ds != null)
                {
                    fds = ds.OpenFeatureDataset(fdsname);
                    if (fds != null)
                    {
                        IPropertySet ps = new PropertySet();
                        ps.SetProperty("FILENAME", file);
                        ps.SetProperty("TOLERANCE", 0.02);
                        op = dintf.CreateDataInterop(
                                    gviDataConnectionType.gviOgrConnectionDWG,
                                     ps);

                        if (op != null)
                        {
                            //op.OnProcessing = callback;
                            op.StepValue = 100;
                            IPropertySet strMap = new PropertySet();
                            foreach (DictionaryEntry de in list)
                            {
                                strMap.SetProperty(de.Key.ToString(), de.Value.ToString());
                            }
                            try
                            {
                                int count = op.ImportLayers(fds, strMap);
                            }
                            catch (System.Exception ex)
                            {
                                b = false;
                            }
                        }
                        else
                        {
                            b = false;
                        }

                    }
                }
            }
            catch (System.Exception ex)
            {
                b = false;
                if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
                    MessageBox.Show("需要标准runtime授权");
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fds != null)
                {
                    //Marshal.ReleaseComObject(fds);
                    fds = null;
                }
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
                if (op != null)
                {
                    //Marshal.ReleaseComObject(op);
                    op = null;
                }
                bar.Invoke(_IsFinishDelegate, b);
            }
        }

        private void ImportIFC(object param)
        {
            IDataInteropFactory dintf = new DataInteropFactory();
            IDataInterop op = null;
            IDataSource ds = null;
            IFeatureDataSet fds = null;
            try
            {
                object[] obj = param as object[];
                string cnnStr = obj[0] as string;
                string fdsname = obj[1] as string;
                string file = obj[2] as string;
                string fn = obj[3] as string;
                IPropertySet ps = new PropertySet();
                ps.SetProperty("FILENAME", file);
                op = dintf.CreateDataInterop(
                            gviDataConnectionType.gviOgrConnectionIFC,
                             ps);
                IFieldInfoCollection fields = op.LayersInfo.Get(0).FieldInfos;
                for (int l = 0; l < fields.Count; l++)
                {
                    if (fields.Get(l).FieldType == gviFieldType.gviFieldFID)
                    {
                        fields.RemoveAt(l);
                        break;
                    }
                }
                ds = new DataSourceFactory().OpenDataSourceByString(cnnStr);
                if (ds != null)
                {
                    fds = ds.OpenFeatureDataset(fdsname);
                    if (fds != null)
                    {
                        //op.OnProcessing = callback;
                        op.StepValue = 100;
                        IPropertySet strMap = new PropertySet();
                        strMap.SetProperty(op.LayersInfo.Get(0).Name, fn);
                        int count = op.ImportLayers(fds, strMap);
                        if (count == 1)
                            bar.Invoke(_IsFinishDelegate, true);
                        else
                            bar.Invoke(_IsFinishDelegate, false);
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void ImportGDB(object param)
        {
            IDataInteropFactory dintf = new DataInteropFactory();
            IDataInterop op = null;
            IDataSource ds = null;
            IFeatureDataSet fds = null;
            try
            {
                object[] obj = param as object[];
                string cnnStr = obj[0] as string;
                string fdsname = obj[1] as string;
                string file = obj[2] as string;
                string srcFcn = obj[3] as string;
                string dstFcn = obj[4] as string;
                IPropertySet ps = new PropertySet();
                ps.SetProperty("FILENAME", file);
                ps.SetProperty("LAYER", srcFcn);
                op = dintf.CreateDataInterop(
                            gviDataConnectionType.gviOgrConnectionFileGDB,
                             ps);
                IFieldInfoCollection fields = op.LayersInfo.Get(0).FieldInfos;
                for (int l = 0; l < fields.Count; l++)
                {
                    if (fields.Get(l).FieldType == gviFieldType.gviFieldFID)
                    {
                        fields.RemoveAt(l);
                        break;
                    }
                }
                ds = new DataSourceFactory().OpenDataSourceByString(cnnStr);
                if (ds != null)
                {
                    fds = ds.OpenFeatureDataset(fdsname);
                    if (fds != null)
                    {
                        //op.OnProcessing = callback;
                        op.StepValue = 1;
                        IPropertySet strMap = new PropertySet();
                        strMap.SetProperty(op.LayersInfo.Get(0).Name, dstFcn);
                        int count = op.ImportLayers(fds, strMap);
                        if (count == 1)
                            bar.Invoke(_IsFinishDelegate, true);
                        else
                            bar.Invoke(_IsFinishDelegate, false);
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
                    MessageBox.Show("需要标准runtime授权");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void ImportSKP(object param)
        {
            IDataInteropFactory dintf = new DataInteropFactory();
            IDataInterop op = null;
            IDataSource ds = null;
            IFeatureDataSet fds = null;
            try
            {
                object[] obj = param as object[];
                string cnnStr = obj[0] as string;
                string fdsname = obj[1] as string;
                string file = obj[2] as string;
                string fn = obj[3] as string;
                IPropertySet ps = new PropertySet();
                ps.SetProperty("FILENAME", file);
                op = dintf.CreateDataInterop(
                            gviDataConnectionType.gviOgrConnectionSKP,
                             ps);
                IFieldInfoCollection fields = op.LayersInfo.Get(0).FieldInfos;
                for (int l = 0; l < fields.Count; l++)
                {
                    if (fields.Get(l).FieldType == gviFieldType.gviFieldFID)
                    {
                        fields.RemoveAt(l);
                        break;
                    }
                }
                ds = new DataSourceFactory().OpenDataSourceByString(cnnStr);
                if (ds != null)
                {
                    fds = ds.OpenFeatureDataset(fdsname);
                    if (fds != null)
                    {
                        //op.OnProcessing = callback;
                        op.StepValue = 100;
                        IPropertySet strMap = new PropertySet();
                        strMap.SetProperty(op.LayersInfo.Get(0).Name, fn);
                        int count = op.ImportLayers(fds, strMap);
                        if (count == 1)
                            bar.Invoke(_IsFinishDelegate, true);
                        else
                            bar.Invoke(_IsFinishDelegate, false);
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
                    MessageBox.Show("需要标准runtime授权");
                else
                    MessageBox.Show(ex.Message);
            }
        }
    }



    public enum NodeObjectType
    {
        Unknown = 0,
        DataSource = 1,
        DataSet = 2,
        FeatureClass = 3,
        FeatureLayer = 4
    }

    public class myTreeNode : TreeNode
    {
        public NodeObjectType nodeType = NodeObjectType.Unknown;


        public myTreeNode(string sName,NodeObjectType type,object oTag)
        {
            this.Name = sName;
            nodeType = type;
            this.Text = sName;
            this.Tag = oTag;
        }
    }
}
