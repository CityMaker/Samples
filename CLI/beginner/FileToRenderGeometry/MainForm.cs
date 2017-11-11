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
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;

namespace FileToRenderGeometry
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";       

        private System.Guid rootId = new System.Guid();
        private IRenderModelPoint rmp = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(false, ps);

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

            this.toolStripComboBoxOsgMode.SelectedIndex = 0;

            this.axRenderControl1.RcObjectEditFinish += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "FileToRenderGeometry.html";
            }
        }
 

        void axRenderControl1_RcObjectEditFinish()
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            rmp.VisibleMask = gviViewportMask.gviViewAllNormalView;  //还原物体可拾取性
        }


        private void toolStripButtonSelectOsg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "OSG Files(*.osg)|*.osg";
            dlg.Multiselect = false;
            if(System.IO.Directory.Exists(strMediaPath))
            {
                dlg.InitialDirectory = strMediaPath + @"\osg";
            }
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFile = dlg.FileName;
                ImportModelOsg(strFile);
            }
        }

        public void ImportModelOsg(string osgFile)
        {
            //try
            {
                FileInfo fInfo = new FileInfo(osgFile);
                string strFilePath = fInfo.DirectoryName;
                IObjectManager rm = this.axRenderControl1.ObjectManager;
                IGeometryFactory geoFactory = new GeometryFactory();
                IResourceFactory symbolFac = new ResourceFactory();

                //mc
                string modelName = fInfo.Name.Split('.')[0];
                string osgFilePath = strFilePath + "\\" + modelName + ".osg";
                IPropertySet Images = null;
                IModel model = null;                
                IMatrix matrix = null;
                symbolFac.CreateModelAndImageFromFile(osgFilePath, out Images, out model, out matrix);
                if (model == null || model.GroupCount == 0)
                    return;
                rm.AddModel(modelName, model);

                //tc
                int nCount = Images.Count;
                if (Images != null && nCount > 0)
                {
                    string[] keys = Images.GetAllKeys();
                    foreach (string imgName in keys)
                    {
                        IImage img = Images.GetProperty(imgName) as IImage;

                        if (img == null)
                            continue;
                        if (string.IsNullOrEmpty(imgName))
                            continue;

                        rm.AddImage(imgName, img);
                    }

                  
                }

                //modelpoint
                IModelPoint modelPoint = null;
                modelPoint = (IModelPoint)geoFactory.CreateGeometry(
                                gviGeometryType.gviGeometryModelPoint,
                                gviVertexAttribute.gviVertexAttributeZ);
                modelPoint.FromMatrix(matrix);
                modelPoint.ModelName = modelName;
                modelPoint.SpatialCRS = (new CRSFactory()).CreateFromWKT(this.axRenderControl1.GetCurrentCrsWKT()) as ISpatialCRS;

                
                if (this.toolStripComboBoxOsgMode.SelectedIndex == 0)
                {
                    rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(modelPoint, null, rootId);
                    this.axRenderControl1.Camera.FlyToObject(rmp.Guid, gviActionCode.gviActionFlyTo);                    
                }
                else
                {
                    modelPoint.X = 0.0;
                    modelPoint.Y = 0.0;
                    modelPoint.Z = 0.0;

                    rmp = this.axRenderControl1.ObjectManager.CreateRenderModelPoint(modelPoint, null, rootId);
                    rmp.MouseSelectMask = gviViewportMask.gviViewNone;  //设置物体不可拾取，以免总是拾取到自身影响交互
                    IEulerAngle angle = new EulerAngle();
                    angle.Set(0, -50, 0);
                    this.axRenderControl1.Camera.LookAt2(modelPoint, 200, angle);

                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                    this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(rmp, gviGeoEditType.gviGeoEditCreator);
                }
                
            }
            //catch (System.Exception ex)
            //{
            //    if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
            //        MessageBox.Show("需要标准runtime授权");
            //    else
            //        MessageBox.Show(ex.Message);
            //}
        }

    }
}
