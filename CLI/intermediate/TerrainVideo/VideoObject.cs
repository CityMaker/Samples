using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Gvitech.CityMaker.RenderControl;
using System.Runtime.InteropServices;
using System.Drawing;
using Gvitech.CityMaker.Math;
using System.IO;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeGeometry;

namespace TerrainVideo
{
    public class VideoObject
    {
        public delegate void PropertyChanged(string changeType);
        public event PropertyChanged NotifyPropertyChangeEvent;
        
        private ITerrainVideo tv = null;
        public System.Guid _Guid = CommonUnity.RenderHelper.ObjectManager.GetProjectTree().RootID;
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        [Browsable(false)]
        public ITerrainVideo TerrainVideoObject
        {
            get
            {
                return this.tv;
            }
            set
            {
                this.tv = value;
            }
        }

        #region 参数
        private Guid guidstr;
        [Category("参数"), Description("GUID")]
        public Guid GUIDString
        {
            get { return guidstr; }
            set
            {
                if (value != guidstr)
                {
                    guidstr = value;
                    NotifyPropertyChange("GUIDString");
                }
            }
        }

        private string angle;
        [Category("参数"), Description("投影相机的方向")]
        public string Angle
        {
            get { return angle; }
            set
            {
                if (value != angle)
                {
                    angle = value;
                    NotifyPropertyChange("Angle");
                }
            }
        }

        private double aspectRatio;
        [Category("参数"), Description("拍摄范围的纵横比")]
        public double AspectRatio
        {
            get { return aspectRatio; }
            set
            {
                if (value != aspectRatio)
                {
                    aspectRatio = value;
                    NotifyPropertyChange("AspectRatio");
                }
            }
        }

        private bool canSeek;
        [Category("参数"), Description("是否可以定位视频的播放位置")]
        public bool CanSeek
        {
            get { return canSeek; }
            set
            {
                if (value != canSeek)
                {
                    canSeek = value;
                    NotifyPropertyChange("CanSeek");
                }
            }
        }

        private double farClip;
        [Category("参数"), Description("投影相机的远裁距离")]
        public double FarClip
        {
            get { return farClip; }
            set
            {
                if (value != farClip)
                {
                    farClip = value;
                    NotifyPropertyChange("FarClip");
                }
            }
        }

        private double fieldOfView;
        [Category("参数"), Description("投影相机的纵向广角")]
        public double FieldOfView
        {
            get { return fieldOfView; }
            set
            {
                if (value != fieldOfView)
                {
                    fieldOfView = value;
                    NotifyPropertyChange("FieldOfView");
                }
            }
        }

        private string icon;
        [Category("参数"), Description("视频图标")]
        public string Icon
        {
            get { return icon; }
            set
            {
                if (value != icon)
                {
                    icon = value;
                    NotifyPropertyChange("Icon");
                }
            }
        }

        private double playbackRate;
        [Category("参数"), Description("视频的播放速度")]
        public double PlaybackRate
        {
            get { return playbackRate; }
            set
            {
                if (value != playbackRate)
                {
                    playbackRate = value;
                    NotifyPropertyChange("PlaybackRate");
                }
            }
        }

        private bool playLoop;
        [Category("参数"), Description("视频是否循环播放")]
        public bool PlayLoop
        {
            get
            {
                return playLoop;
            }
            set
            {
                if (value != playLoop)
                {
                    playLoop = value;
                    NotifyPropertyChange("PlayLoop");
                }
            }
        }

        private int playStatus;
        [Category("参数"), Description("视频的播放状态")]
        public int PlayStatus
        {
            get
            {
                return playStatus;
            }
            set
            {
                if (value != playStatus)
                {
                    playStatus = value;
                    NotifyPropertyChange("PlayStatus");
                }
            }
        }

        private bool playVideoOnStartup;
        [Category("参数"), Description("视频是否自动播放")]
        public bool PlayVideoOnStartup
        {
            get
            {
                return playVideoOnStartup;
            }
            set
            {
                if (value != playVideoOnStartup)
                {
                    playVideoOnStartup = value;
                    NotifyPropertyChange("PlayVideoOnStartup");
                }


            }
        }
 
        private string videoFileName;
        [Category("参数"), Description("视频文件路径")]
        public string VideoFileName
        {
            get
            {
                return videoFileName;
            }
            set
            {
                if (value != videoFileName)
                {
                    videoFileName = value;
                    NotifyPropertyChange("VideoFileName");
                }
            }
        }

        private double videoLength;
        [Category("参数"), Description("视频播放总时长")]
        public double VideoLength
        {
            get
            {
                return videoLength;
            }
            set
            {
                if (value != videoLength)
                {
                    videoLength = value;
                    NotifyPropertyChange("VideoLength");
                }
            }
        }

        private double videoOpacity;
        [Category("参数"), Description("视频的透明度")]
        public double VideoOpacity
        {
            get
            {
                return videoOpacity;
            }
            set
            {
                if (value != videoOpacity)
                {
                    videoOpacity = value;
                    NotifyPropertyChange("VideoOpacity");
                }
            }
        }

        private double videoPosition;
        [Category("参数"), Description("视频的播放位置")]
        public double VideoPosition
        {
            get
            {
                return videoPosition;
            }
            set
            {
                if (value != videoPosition)
                {
                    videoPosition = value;
                    NotifyPropertyChange("VideoPosition");
                }
            }
        }

        #endregion

        protected IEulerAngle v3 = new EulerAngle();
        public VideoObject()
        {
            v3.Set(0, -90, 0);
            Angle = v3.Heading.ToString() + "," + v3.Tilt.ToString() + "," + v3.Roll.ToString();

            AspectRatio = 1.36777;
            CanSeek = false;
            FarClip = 200.0;
            FieldOfView = 45.0;            

            Icon = (strMediaPath + @"\png\camera.png");

            PlaybackRate = 1.0;
            PlayLoop = true;
            PlayStatus = 1;
            PlayVideoOnStartup = true;
            VideoFileName = (strMediaPath + @"\video\ambulance.AVI");
            VideoLength = 0;
            VideoOpacity = 1.0;
            VideoPosition = 0;
        }

        private void NotifyPropertyChange(string type)
        {
            if (NotifyPropertyChangeEvent != null)
            {
                NotifyPropertyChangeEvent(type);
            }
        }

        public void Create()
        {
            try
            {
                if (this.tv == null)
                {
                    IPoint tmpPos = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                    tmpPos.SetCoords(0, 0, 0, 0, 0);
                    tmpPos.SpatialCRS = new CRSFactory().CreateFromWKT(CommonUnity.RenderHelper.GetCurrentCrsWKT()) as ISpatialCRS;
                    this.tv = CommonUnity.RenderHelper.ObjectManager.CreateTerrainVideo(tmpPos, _Guid);
                    if(this.tv != null)
                        this.guidstr = this.tv.Guid;
                }

            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
        }

        public void Update()
        {
            try
            {
                if (this.angle != null)
                {
                    string[] arr = this.angle.Split(',');
                    if (arr.Length == 3)
                    {
                        v3.Set(double.Parse(arr[0]), double.Parse(arr[1]), double.Parse(arr[2]));
                        tv.Angle = v3;
                    }
                }

                tv.AspectRatio = this.aspectRatio;
                //tv.CanSeek = this.canSeek;
                tv.FarClip = this.farClip;
                tv.FieldOfView = this.fieldOfView;
                tv.Icon = this.icon;
                tv.PlaybackRate = this.playbackRate;
                tv.PlayLoop = this.playLoop;
                //tv.PlayStatus = this.playStatus;
                tv.PlayVideoOnStartup = this.playVideoOnStartup;
                tv.VideoFileName = this.videoFileName;
                //tv.VideoLength = this.videoLength;
                tv.VideoOpacity = this.videoOpacity;
                tv.VideoPosition = this.videoPosition;

            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
        }


        public void SetPosition(IPoint v3)
        {
            tv.Position = v3;
        }

        public void SetAngle(IEulerAngle g)
        {
            tv.Angle = g;
            Angle = g.Heading.ToString() + "," + g.Tilt.ToString() + "," + g.Roll.ToString();
        }
    }
}
