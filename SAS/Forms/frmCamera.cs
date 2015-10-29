using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SAS.ClassSet.FunctionTools;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using SAS.ClassSet.Common;
namespace SAS.Forms
{
    public partial class frmCamera : DevComponents.DotNetBar.Office2007Form
    {
        CameraTools camera;
        DeviceCapabilityInfo _DeviceCapabilityInfo;
        DeviceInfo _DeviceInfo;
        string path = Common.strEmailResultPath;
        string name = "me";
        public frmCamera(string filepath, string name)
        {
            this.EnableGlass = false;
            this.name = name;
            InitializeComponent();
            camera = new CameraTools();
            buttonX4.Enabled = false;
            foreach (DeviceInfo info in camera.GetCameras())
            {
                comboBoxEx1.Items.Add(info);
            }
            camera.NewFrameEvent +=new NewFrameEventHandler(camera_NewFrameEvent);
       
            
        }
      
        void camera_NewFrameEvent(object sender, EventArgs e)
        {
           pictureBox1.Image = camera.NewFrame;
        }
        private void frmCamera_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
         
        }

        private void frmCamera_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (camera.DeviceExist)
                camera.CloseVideo();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (_DeviceInfo != null && _DeviceCapabilityInfo != null)
            {
                if (camera.StartVideo(_DeviceInfo, _DeviceCapabilityInfo))
                    buttonX2.Enabled = true;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (camera.DeviceExist)
            {
                if (camera.CloseVideo())
                    buttonX2.Enabled = false;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = camera.NewFrame;
            buttonX4.Enabled = true;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                pictureBox2.Image.Save(path + @name + ".jpg", ImageFormat.Jpeg);
                MessageBox.Show("存储成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(path + name + ".jpg");

            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEx2.Items.Clear();
            _DeviceCapabilityInfo = null;
            _DeviceInfo = (DeviceInfo)comboBoxEx1.SelectedItem;
            foreach (DeviceCapabilityInfo info in camera.GetDeviceCapability(_DeviceInfo))
            {
                comboBoxEx2.Items.Add(info);
            }
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _DeviceCapabilityInfo = (DeviceCapabilityInfo)comboBoxEx2.SelectedItem;
        }
    }
}
