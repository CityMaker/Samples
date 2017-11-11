using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;


namespace DomainTree
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private IDataSource ds = null;
        private IFeatureDataSet dataset = null;  // 存储为全局变量，以便获取其对应的逻辑图层树
        private List<IFeatureLayer> layerList = null;  //存储全局变量dataset中已加载的FeatureLayer，以便设置其组可见性
         
        private Hashtable nodekeyMap = new Hashtable();  //text, key存储树节点文本及对应key值

        private System.Guid rootId = new System.Guid();

        private List<string> fieldNamesHasDomain = new List<string>();
        private IFieldInfoCollection fieldinfos = null;

        public MainForm()
        {
            InitializeComponent();            
        }

        private void MainForm_Load(object sender, EventArgs e)
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
                string tmpFDBPath = (strMediaPath + @"\BIM.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                dataset = ds.OpenFeatureDataset(setnames[0]);   //此处把dataset作为全局对象，以便后面调用
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                fcMap = new Hashtable(fcnames.Length);
                foreach (string name in fcnames)
                {
                    IFeatureClass fc = dataset.OpenFeatureClass(name);
                    // 找到空间列字段
                    List<string> geoNames = new List<string>();
                    fieldinfos = fc.GetFields();
                    for (int i = 0; i < fieldinfos.Count; i++)
                    {
                        IFieldInfo fieldinfo = fieldinfos.Get(i);
                        if (null == fieldinfo)
                            continue;

                        //找出设置了域的字段
                        IDomain domain = fieldinfo.Domain;
                        if (domain != null)
                        {
                            if (!fieldNamesHasDomain.Contains(fieldinfo.Name))
                                fieldNamesHasDomain.Add(fieldinfo.Name);
                        }

                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        if (null == geometryDef)
                            continue;
                        geoNames.Add(fieldinfo.Name);
                    }
                    fcMap.Add(fc, geoNames);
                }

                //设置可供选择的字段
                comboBoxDomains.DataSource = fieldNamesHasDomain.ToArray();
                if (fieldNamesHasDomain.Count > 0)
                {
                    comboBoxDomains.SelectedIndex = 0;
                }
                SetDomainTree();
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            // CreateFeautureLayer
            bool hasfly = false;
            layerList = new List<IFeatureLayer>();
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    ISimpleGeometryRender georender = null;
                    if (comboBoxDomains.SelectedIndex >= 0 && fieldNamesHasDomain.Count > 0)
                    {
                        georender = new SimpleGeometryRender();
                        georender.RenderGroupField = fieldNamesHasDomain[comboBoxDomains.SelectedIndex];
                    }
                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(fc, geoName, null, georender, rootId);
                    layerList.Add(featureLayer);

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

            //绑定事件
            this.comboBoxDomains.SelectedIndexChanged += new System.EventHandler(this.comboBoxDomains_SelectedIndexChanged);

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "DomainTree.html";
            }  
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {              
                foreach (IFeatureLayer layer in layerList)
                {
                    layer.SetGroupVisibleMask((int)nodekeyMap[e.Node.Text], e.Node.Checked ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone);
                }
            }
        }

        private void comboBoxDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDomains.SelectedIndex < 0 || fieldNamesHasDomain.Count == 0)
                return;

            for (int l = 0; l < layerList.Count; l++)
            {
                IFeatureLayer layer = layerList[l];
                ISimpleGeometryRender georenderNull = new SimpleGeometryRender();
                layer.SetGeometryRender(georenderNull);
                layer.VisibleMask = gviViewportMask.gviViewAllNormalView;

                ISimpleGeometryRender georender = new SimpleGeometryRender();
                georender.RenderGroupField = fieldNamesHasDomain[comboBoxDomains.SelectedIndex];
                layer.SetGeometryRender(georender);
            }

            SetDomainTree();
        }

        private void SetDomainTree()
        {
            if (comboBoxDomains.SelectedIndex < 0 || fieldNamesHasDomain.Count == 0)
                return;

            string fieldName = fieldNamesHasDomain[comboBoxDomains.SelectedIndex];
            int index = fieldinfos.IndexOf(fieldName);
            IFieldInfo field = fieldinfos.Get(index);
            IDomain domainSelect = field.Domain;
            if (domainSelect == null)
                return;

            nodekeyMap.Clear();
            this.treeViewDomain.Nodes.Clear();
            if (domainSelect.DomainType == gviDomainType.gviDomainCodedValue)
            {                
                ICodedValueDomain codedomain = domainSelect as ICodedValueDomain;
                int nCodes = codedomain.CodeCount;
                for (int c = 0; c < nCodes; c++)
                {
                    string codename = codedomain.GetCodeName(c);
                    object codevalue = codedomain.GetCodeValue(c);
                    this.treeViewDomain.Nodes.Add(codevalue.ToString(), codename, 2, 2);
                    nodekeyMap.Add(codename, c);

                    TreeNode pNode = this.treeViewDomain.Nodes.Find(codevalue.ToString(), true)[0];
                    pNode.Checked = true;
                }

                //其它值
                {
                    this.treeViewDomain.Nodes.Add("other", "other", 2, 2);
                    nodekeyMap.Add("other", nCodes);

                    TreeNode pNode = this.treeViewDomain.Nodes.Find("other", true)[0];
                    pNode.Checked = true;
                }
            }
            else
            {
                IRangeDomain rangedomain = domainSelect as IRangeDomain;
                object minValue = rangedomain.MinValue;
                object maxValue = rangedomain.MaxValue;
            }
        }


    }

}
