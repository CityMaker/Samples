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
//author	yuanying
//date	2013/05/28
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
using Gvitech;
using System.Drawing;

namespace ShadowMapApplication
{
    public partial class MainForm : Form
    {        
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private System.Guid rootId = System.Guid.Empty;

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            //this.axRenderControl1.Initialize2("PROJCS[\"MacauProj\",GEOGCS[\"MacauGrid\",DATUM[\"D_International_1924\",SPHEROID[\"International_1924\",6378388.0,297.0]],PRIMEM[\"<custom>\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",20000.0],PARAMETER[\"False_Northing\",20000.0],PARAMETER[\"Central_Meridian\",113.5364694444],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",22.2123972222],UNIT[\"Meter\",1.0]]", ps);
            this.axRenderControl1.Initialize(false, ps);
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


            // 加载瓦片图层
            string tilePath = (strMediaPath + @"\sdk.tdb");
            I3DTileLayer layer = this.axRenderControl1.ObjectManager.Create3DTileLayer(tilePath, "", rootId);
            this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);
            
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "ShadowMapApplication.html";
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

            init();

            this.colorBox.Text = axRenderControl1.SunConfig.ShadowColor.ToArgb().ToString("X");
            this.trackBarOpacity.Value = axRenderControl1.SunConfig.ShadowColor.A;
            this.comboBoxSunMode.SelectedIndex = 0;
            this.numHeading.Value = 0;
            this.numTilt.Value = 0;
            this.numYear.Value = (decimal)DateTime.UtcNow.Year;
            this.numMonth.Value = (decimal)DateTime.UtcNow.Month;
            this.numDay.Value = (decimal)DateTime.UtcNow.Day;
            this.numHour.Value = (decimal)DateTime.UtcNow.Hour;
            this.numMinute.Value = (decimal)DateTime.UtcNow.Minute;
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = Color.FromArgb(Convert.ToInt32(colorBox.Text, 16));
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.colorBox.Text = this.colorDialog1.Color.ToArgb().ToString("X");
                axRenderControl1.SunConfig.ShadowColor = this.colorDialog1.Color;
            }
        }

        /// <summary>
        /// 设置颜色透明度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarOpacity_Scroll(object sender, EventArgs e)
        {
            int a = this.trackBarOpacity.Value;
            Color oldColor = axRenderControl1.SunConfig.ShadowColor;
            Color newColor = Color.FromArgb(a, oldColor.R, oldColor.G, oldColor.B);
            axRenderControl1.SunConfig.ShadowColor = newColor;
            if (axRenderControl1.SunConfig.ShadowColor == Color.Empty)
                this.colorBox.Text = "00000000";
            else
                this.colorBox.Text = axRenderControl1.SunConfig.ShadowColor.ToArgb().ToString("X");
        }

        private void comboBoxSunMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSunMode.SelectedIndex == 0)
            {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
            }
            else if (this.comboBoxSunMode.SelectedIndex == 1)
            {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = true;
                this.btnPlay.Enabled = true;
                this.btnStop.Enabled = true;
                this.btnNow.Enabled = true;
            }
            else
            {
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = false;
            }
        }

        /// <summary>
        /// 开始分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartAnalyse_Click(object sender, EventArgs e)
        {            
            switch (this.comboBoxSunMode.SelectedIndex)
            {
                case 0:
                    {
                        axRenderControl1.SunConfig.SunCalculateMode = gviSunCalculateMode.gviSunModeFollowCamera;
                    }
                    break;
                case 1:
                    {
                        axRenderControl1.SunConfig.SunCalculateMode = gviSunCalculateMode.gviSunModeAccordingToGMT;
                        DateTime time = new DateTime(int.Parse(this.numYear.Value.ToString()),
                            int.Parse(this.numMonth.Value.ToString()),
                            int.Parse(this.numDay.Value.ToString()),
                            int.Parse(this.numHour.Value.ToString()),
                            int.Parse(this.numMinute.Value.ToString()),
                            0);
                        axRenderControl1.SunConfig.SetGMT(time);
                        this.btnPlay.Enabled = true;
                        this.btnStop.Enabled = true;
                        this.btnNow.Enabled = true;
                    }
                    break;
                case 2:
                    {
                        axRenderControl1.SunConfig.SunCalculateMode = gviSunCalculateMode.gviSunModeUserDefined;
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(double.Parse(numHeading.Value.ToString()), double.Parse(numTilt.Value.ToString()), 0);
                        axRenderControl1.SunConfig.SetSunEuler(angle);
                    }
                    break;
            }

            axRenderControl1.SunConfig.EnableShadow(0, true);
        }

        /// <summary>
        /// 停止分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopAnalyse_Click(object sender, EventArgs e)
        {
            axRenderControl1.SunConfig.EnableShadow(0, false);
        }


        #region GMT
        private void numHour_ValueChanged(object sender, EventArgs e)
        {
            DateTime time = new DateTime(int.Parse(this.numYear.Value.ToString()),
                                     int.Parse(this.numMonth.Value.ToString()),
                                     int.Parse(this.numDay.Value.ToString()),
                                     int.Parse(this.numHour.Value.ToString()),
                                     int.Parse(this.numMinute.Value.ToString()),
                                     0);
            axRenderControl1.SunConfig.SetGMT(time);
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
            this.numYear.Value = (decimal)DateTime.UtcNow.Year;
            this.numMonth.Value = (decimal)DateTime.UtcNow.Month;
            this.numDay.Value = (decimal)DateTime.UtcNow.Day;
            this.numHour.Value = (decimal)DateTime.UtcNow.Hour;
            this.numMinute.Value = (decimal)DateTime.UtcNow.Minute;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.numMinute.Value == 59)
            {
                this.numMinute.Value = 0;
                if (this.numHour.Value == 23)
                    this.numHour.Value = 0;
                else
                    this.numHour.Value = int.Parse(this.numHour.Value.ToString()) + 1;
            }
            else
                this.numMinute.Value++;
        }
        #endregion


        private void numSunDirection_ValueChanged(object sender, EventArgs e)
        {
            IEulerAngle angle = new EulerAngle();
            angle.Set(double.Parse(numHeading.Value.ToString()), double.Parse(numTilt.Value.ToString()), 0);
            axRenderControl1.SunConfig.SetSunEuler(angle);
        }

    }
}
