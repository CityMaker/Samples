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
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Controls;

namespace WalkGround
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        private System.Guid rootId = new System.Guid();

        // 线程转发
        public int MainThreadId = 0;
        

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             
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

            string tmpTdbPath = (strMediaPath + @"\sdk.tdb");
            I3DTileLayer tilelayer = this.axRenderControl1.ObjectManager.Create3DTileLayer(tmpTdbPath, "", rootId);
            if (tilelayer != null)
            {
                IVector3 pos = new Vector3();
                pos.Set(15404.97, 35575.28, 59);
                IEulerAngle ang = new EulerAngle();
                ang.Set(0, -30, 0);
                this.axRenderControl1.Camera.LookAt(pos, 10, ang);
            }
            else
                this.Text = "tilelayer create failed!";

            this.comboBox1.SelectedIndex = 2;

            this.axRenderControl1.RcCameraFlyFinished += new _IRenderControlEvents_RcCameraFlyFinishedEventHandler(axRenderControl1_RcCameraFlyFinished);
            
            

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "WalkGround.html";
            } 
        }

        void axRenderControl1_RcCameraFlyFinished(byte Type)
        {
            

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractWalk;            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    this.axRenderControl1.Camera.WalkMode = gviWalkMode.gviWalkDisable;
                    break;
                case 1:
                    this.axRenderControl1.Camera.WalkMode = gviWalkMode.gviWalkOnWalkGround;
                    break;
                case 2:
                    this.axRenderControl1.Camera.WalkMode = gviWalkMode.gviWalkOnAll;
                    break;
            }
        }



    }
}
