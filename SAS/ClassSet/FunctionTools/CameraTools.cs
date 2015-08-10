using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AForge.Video.DirectShow;

namespace SAS.ClassSet.FunctionTools
{
    public delegate void NewFrameEventHandler(object sender, EventArgs e);
   public class CameraTools
    {
            public event NewFrameEventHandler NewFrameEvent;
            private FilterInfoCollection videoDevices;
            private VideoCaptureDevice videoSource = null;
            public bool DeviceExist
            { get; set; }
            private Image newFrame;
            public Image NewFrame
            {
                set
                {
                    newFrame = value;
                }
                get
                {

                    return newFrame;

                }
            }
            public CameraTools()
            {
                DeviceExist = false;
            }
            public List<DeviceCapabilityInfo> GetDeviceCapability(DeviceInfo deviceInfo)
            {
                List<DeviceCapabilityInfo> deviceCapability = new List<DeviceCapabilityInfo>();
                VideoCaptureDevice video = new VideoCaptureDevice(deviceInfo.MonikerString);
                for (int i = 0; i < video.VideoCapabilities.Length; i++)
                {
                    VideoCapabilities cap = video.VideoCapabilities[i];
                    DeviceCapabilityInfo capInfo = new DeviceCapabilityInfo(cap.FrameSize, cap.FrameRate);
                    deviceCapability.Add(capInfo);
                }
                return deviceCapability;
            }
            public List<DeviceInfo> GetCameras()
            {
                List<DeviceInfo> cameraList = new List<DeviceInfo>();
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                int idx = 0;
                foreach (FilterInfo device in videoDevices)
                {
                    cameraList.Add(new DeviceInfo(device.Name, device.MonikerString, idx, FilterCategory.VideoInputDevice));
                    idx++;
                }
                return cameraList;
            }
            public FilterInfoCollection VideoDevices
            {
                get
                {
                    return videoDevices;
                }
            }
            public bool CloseVideo()
            {

                if (!(videoSource == null))
                    if (videoSource.IsRunning)
                    {
                        videoSource.SignalToStop();
                        videoSource.WaitForStop();
                        DeviceExist = false;
                    }
                videoSource = null;
                return true;
            }
            public bool StartVideo(DeviceInfo device, DeviceCapabilityInfo info)
            {
                try
                {
                    Size frameSize = info.FrameSize;
                    int rate = info.MaxFrameRate;
                    videoSource = new VideoCaptureDevice(device.MonikerString);
                    videoSource.DesiredFrameSize = frameSize;
                    videoSource.DesiredFrameRate = rate;
                    videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);
                    videoSource.Start();
                    DeviceExist = true;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
            {

                newFrame = (Image)eventArgs.Frame.Clone();

                if (NewFrameEvent != null)
                {
                    NewFrameEvent(this, new EventArgs());
                }


            }


        }

        public class NewFrameEventArgs : EventArgs
        {
            public NewFrameEventArgs(Bitmap bitmap)
            {
                newFrame = bitmap;
            }
            Bitmap newFrame;
            public Bitmap NewFrame
            {
                get
                {
                    return newFrame;
                }
            }
        }
        //设备信息
        public class DeviceInfo
        {
            public string Name;
            public string MonikerString;
            public int Index;
            Guid Category;

            public DeviceInfo(string name, string monikerString, int index) :
                this(name, monikerString, index, Guid.Empty)
            {
            }

            public DeviceInfo(string name, string monikerString, int index, Guid category)
            {
                Name = name;
                MonikerString = monikerString;
                Index = index;
                Category = category;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        //设备能力
        public class DeviceCapabilityInfo
        {
            public Size FrameSize;
            public int MaxFrameRate;

            public DeviceCapabilityInfo(Size frameSize, int maxFrameRate)
            {
                FrameSize = frameSize;
                MaxFrameRate = maxFrameRate;
            }

            public override string ToString()
            {
                return string.Format("{0}x{1}  {2}fps", FrameSize.Width, FrameSize.Height, MaxFrameRate);
            }
        }
   
}
