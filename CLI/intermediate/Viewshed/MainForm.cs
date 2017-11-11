using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml;
using System;
using System.Data;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;


namespace Viewshed
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        private DataTable dt = null;
        private string locationName = "";
        private CameraProperty cp = null;
        private IVector3 vector = new Vector3();
        private IEulerAngle angle = new EulerAngle();
        private IPoint positionPoint = null;

        private List<IViewshed> videoList = new List<IViewshed>();
        private int curVideoIndex = -1;
        private IViewshed curVideo = null;
        private int curRowIndex = -1;
        private StreamWriter streamWriter = null;
        private double factor = 10.0;

        private System.Guid rootId = new System.Guid();

        private VideoObject tmpTV = null;
        private PropertyFrm pf = null;

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
            this.axRenderControl1.Camera.FlyTime = 1;

            CommonUnity.RenderHelper = this.axRenderControl1;

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

            this.axRenderControl1.Camera.VerticalFieldOfView = 60;

            // 加载瓦片图层
            string tilePath = (strMediaPath + @"\sdk.tdb");
            I3DTileLayer layer = this.axRenderControl1.ObjectManager.Create3DTileLayer(tilePath, "", rootId);
            this.axRenderControl1.Camera.FlyToObject(layer.Guid, gviActionCode.gviActionFlyTo);

            IGeometryFactory fac = new GeometryFactory();
            positionPoint = fac.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);

            ConstructTable();

            // 绑定键盘事件
            this.axRenderControl1.RcKeyUp += new _IRenderControlEvents_RcKeyUpEventHandler(axRenderControl1_RcKeyUp);
            
            string filePath = Application.StartupPath + @"\adjust.txt";
            if (File.Exists(filePath))
            {
                streamWriter = new StreamWriter(filePath, true);
            }

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "Viewshed.html";
            }
        }

        bool axRenderControl1_RcKeyUp(uint Flags, uint Ch)
        {
            

            if (curVideoIndex < 0)
                return false;

            switch (Ch)
            {
            #region 按键调整
                case (uint)Keys.Q:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.X += factor;
                        vector.Set(cp.X, cp.Y, cp.Z);
                        positionPoint.Position = vector;
                        curVideo.Position = positionPoint;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);

                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.W:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.X -= factor;
                        vector.Set(cp.X, cp.Y, cp.Z);
                        positionPoint.Position = vector;
                        curVideo.Position = positionPoint;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);

                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.E:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Y += factor;
                        vector.Set(cp.X, cp.Y, cp.Z);
                        positionPoint.Position = vector;
                        curVideo.Position = positionPoint;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.R:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Y -= factor;
                        vector.Set(cp.X, cp.Y, cp.Z);
                        positionPoint.Position = vector;
                        curVideo.Position = positionPoint;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.T:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Z += factor;
                        vector.Set(cp.X, cp.Y, cp.Z);
                        positionPoint.Position = vector;
                        curVideo.Position = positionPoint;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.Y:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Z -= factor;
                        vector.Set(cp.X, cp.Y, cp.Z);
                        positionPoint.Position = vector;
                        curVideo.Position = positionPoint;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.U:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Heading += factor;
                        angle.Set(cp.Heading, cp.Tilt, cp.Roll);
                        curVideo.Angle = angle;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.I:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Heading -= factor;
                        angle.Set(cp.Heading, cp.Tilt, cp.Roll);
                        curVideo.Angle = angle;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.O:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Tilt += factor;
                        vector.Set(cp.X, cp.Y, cp.Z);
                        angle.Set(cp.Heading, cp.Tilt, cp.Roll);
                        curVideo.Angle = angle;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.P:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Tilt -= factor;
                        angle.Set(cp.Heading, cp.Tilt, cp.Roll);
                        curVideo.Angle = angle;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.D:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Roll += factor;
                        angle.Set(cp.Heading, cp.Tilt, cp.Roll);
                        curVideo.Angle = angle;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.F:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.Roll -= factor;
                        angle.Set(cp.Heading, cp.Tilt, cp.Roll);
                        curVideo.Angle = angle;
                        //this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.G:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.AspectRatio += factor;
                        curVideo.AspectRatio = cp.AspectRatio;
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.H:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.AspectRatio -= factor;
                        curVideo.AspectRatio = cp.AspectRatio;
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.J:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.FieldOfView += factor;
                        curVideo.FieldOfView = cp.FieldOfView;
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;
                case (uint)Keys.K:
                    {
                        curVideo = videoList[curVideoIndex];
                        cp.FieldOfView -= factor;
                        curVideo.FieldOfView = cp.FieldOfView;
                        DataRow dr = dt.Rows[curRowIndex];
                        dr["Location"] = cp;
                    }
                    break;

                case (uint)Keys.D1:
                    {
                        factor = 10;
                    }
                    break;
                case (uint)Keys.D2:
                    {
                        factor = 1;
                    }
                    break;
                case (uint)Keys.D3:
                    {
                        factor = 0.1;
                    }
                    break;
                case (uint)Keys.D4:
                    {
                        factor = 0.01;
                    }
                    break;
            #endregion
                case (uint)Keys.S:
                    {
                        String str = cp.PropertyStrings();
                        streamWriter.WriteLine(str);
                        streamWriter.Flush();
                    }
                    break;
                case (uint)Keys.C:
                    {
                        curVideo = videoList[curVideoIndex];
                        curVideo.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    }
                    break;
                case (uint)Keys.V:
                    {
                        curVideo = videoList[curVideoIndex];
                        curVideo.VisibleMask = gviViewportMask.gviViewNone;
                    }
                    break;
            }

            return true;
        }

        // 构造picture控件
        private void ConstructTable()
        {
            dt = new DataTable();
            DataColumn col_Name = new DataColumn("Name", typeof(System.String));
            DataColumn col_Location = new DataColumn("Location", typeof(System.Object));
            DataColumn col_Object = new DataColumn("Object", typeof(System.Object));
            dt.Columns.AddRange(new DataColumn[] { col_Name, col_Location, col_Object });
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns[1].Visible = false;
            this.dataGridView1.Columns[2].Visible = false;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            curRowIndex = (sender as DataGridView).CurrentRow.Index;
            cp = (sender as DataGridView).CurrentRow.Cells[1].Value as CameraProperty;
            vector.Set(cp.X, cp.Y, cp.Z);
            angle.Set(cp.Heading, cp.Tilt, cp.Roll);
            this.axRenderControl1.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
            curVideoIndex = cp.index;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((sender as DataGridView).CurrentRow == null)
                return;

            curRowIndex = (sender as DataGridView).CurrentRow.Index;
            tmpTV = (sender as DataGridView).CurrentRow.Cells[2].Value as VideoObject;

            if (pf.hasClosed())
            {
                pf = new PropertyFrm(tmpTV);
                pf.Text = tmpTV.GUIDString + "的属性";
                pf.Owner = this;
                pf.Show();
            }
            else
            {
                pf.Text = tmpTV.GUIDString + "的属性";
                pf.SetSource(tmpTV);
            }

            cp = (sender as DataGridView).CurrentRow.Cells[1].Value as CameraProperty;
            curVideoIndex = cp.index;
        }

        private void toolStripButtonCreateVideo_Click(object sender, EventArgs e)
        {
            tmpTV = new VideoObject();
            tmpTV.Create();
            if (tmpTV.ViewshedObject == null)
            {
                MessageBox.Show("Viewshed create failed!");
                return;
            }
            tmpTV.Update();

            pf = new PropertyFrm(tmpTV);
            pf.Text = tmpTV.GUIDString + "的属性";
            pf.Owner = this;        
            pf.Show();

            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectTileLayer;
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (IntersectPoint == null)
                return;

            if (EventSender.Equals(gviMouseSelectMode.gviMouseSelectClick))
            {
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
                
                
                //把视频加入list，同时更新界面
                locationName = tmpTV.ViewshedObject.Guid.ToString();
                cp = new CameraProperty();
                cp.name = locationName;
                cp.X = tmpTV.ViewshedObject.Position.X;
                cp.Y = tmpTV.ViewshedObject.Position.Y;
                cp.Z = tmpTV.ViewshedObject.Position.Z;
                cp.Heading = tmpTV.ViewshedObject.Angle.Heading;
                cp.Roll = tmpTV.ViewshedObject.Angle.Roll;
                cp.Tilt = tmpTV.ViewshedObject.Angle.Tilt;
                cp.AspectRatio = tmpTV.ViewshedObject.AspectRatio;
                cp.FieldOfView = tmpTV.ViewshedObject.FieldOfView;

                videoList.Add(tmpTV.ViewshedObject);
                cp.index = videoList.Count - 1;

                DataRow dr = dt.NewRow();
                dr["Name"] = locationName;
                dr["Location"] = cp;
                dr["Object"] = tmpTV;
                dt.Rows.Add(dr);

                this.dataGridView1.Rows[dt.Rows.Count - 1].Selected = true;

                pf.SetSource(tmpTV);
            }
            else if (EventSender.Equals(gviMouseSelectMode.gviMouseSelectMove))
            {
                this.axRenderControl1.Camera.GetCamera(out vector, out angle);
                tmpTV.SetAngle(angle);

                positionPoint = IntersectPoint;
                positionPoint.Z += 10;
                tmpTV.SetPosition(positionPoint);
            }
        }

        private void toolStripButtonHideVideo_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripButton).Text.Equals("隐藏视频"))
            {
                for (int i = 0; i < videoList.Count; i++)
                {
                    IViewshed v = videoList[i];
                    v.VisibleMask = gviViewportMask.gviViewNone;
                }
            }
            else
            {
                for (int i = 0; i < videoList.Count; i++)
                {
                    IViewshed v = videoList[i];
                    v.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }

            if ((sender as ToolStripButton).Text.Equals("隐藏视频"))
                (sender as ToolStripButton).Text = "显示视频";
            else
                (sender as ToolStripButton).Text = "隐藏视频";
        }

        private void toolStripButtonHideProjectionLines_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripButton).Text.Equals("隐藏投影线"))
            {
                for (int i = 0; i < videoList.Count; i++)
                {
                    IViewshed v = videoList[i];
                    v.DisplayMode = gviTVDisplayMode.gviTVShowPicture;
                }
            }
            else
            {
                for (int i = 0; i < videoList.Count; i++)
                {
                    IViewshed v = videoList[i];
                    v.DisplayMode = gviTVDisplayMode.gviTVShowAll;
                }
            }

            if ((sender as ToolStripButton).Text.Equals("隐藏投影线"))
                (sender as ToolStripButton).Text = "显示投影线";
            else
                (sender as ToolStripButton).Text = "隐藏投影线";
        }

        private void toolStripButtonCreatePoint_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect2);
            
        }

        void axRenderControl1_RcMouseClickSelect2(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (IntersectPoint == null)
                return;

            if (EventSender.Equals(gviMouseSelectMode.gviMouseSelectClick))
            {
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect2);
                

                ISimplePointSymbol ps = new SimplePointSymbol();
                ps.Size = 20;
                ps.FillColor = System.Drawing.Color.Red;
                this.axRenderControl1.ObjectManager.CreateRenderPoint(IntersectPoint, ps, rootId);

                for (int i = 0; i < videoList.Count; i++)
                {
                    IViewshed v = videoList[i];
                    v.Unhighlight();
                    double wx, wy;
                    bool isIn = v.WorldToScreen(IntersectPoint, 1, out wx, out wy);
                    if (isIn)
                    {
                        v.Highlight(System.Drawing.Color.Red);

                        IPoint poiInWorld = v.ScreenToWorld(wx, wy);
                        if (poiInWorld != null)
                        {
                            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractFocus;
                            bool bFocus = this.axRenderControl1.Camera.SetFocus(poiInWorld);
                            if (bFocus)
                                MessageBox.Show("set focus success!");
                        }
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect3);
        }


        void axRenderControl1_RcMouseClickSelect3(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (IntersectPoint == null)
                return;

            if (EventSender.Equals(gviMouseSelectMode.gviMouseSelectClick))
            {
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                this.axRenderControl1.RcMouseClickSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect3);
                

                IViewshedPickResult pr = PickResult as IViewshedPickResult;
                if (pr == null)
                    return;

                IViewshed v = pr.Viewshed;
                if (v == null)
                    return;
                v.Highlight(System.Drawing.Color.Red);
            }
        }


    }

    public class CameraProperty
    {
        public double X = 0.0;
        public double Y = 0.0;
        public double Z = 0.0;
        public double Heading = 0.0;
        public double Tilt = 0.0;
        public double Roll = 0.0;
        public int index = -1;
        public double AspectRatio = 0.0;
        public double FieldOfView = 0.0;
        public double VideoOpacity = 0.0;
        public string name = "";

        public CameraProperty()
        {

        }
        public bool SetCameraProperty(String str)
        {
            try
            {
                String[] buf = str.Split(';');
                Double.TryParse(buf[0], out X);
                Double.TryParse(buf[1], out Y);
                Double.TryParse(buf[2], out Z);
                Double.TryParse(buf[3], out Heading);
                Double.TryParse(buf[4], out Tilt);
                Double.TryParse(buf[5], out Roll);
                int.TryParse(buf[6], out index);
                Double.TryParse(buf[7], out AspectRatio);
                Double.TryParse(buf[8], out FieldOfView);
                Double.TryParse(buf[9], out VideoOpacity);
                name = buf[10].ToString();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public String PropertyStrings()
        {
            String s = String.Format("Name=\"{0}\" x=\"{1}\" y=\"{2}\" z=\"{3}\" heading=\"{4}\" tilt=\"{5}\" roll=\"{6}\" AspectRatio=\"{7}\" FieldOfView=\"{8}\" VideoOpacity=\"{9}\"", name, X, Y, Z, Heading, Tilt, Roll, AspectRatio, FieldOfView, VideoOpacity);
            return s;
        }
    }
}
