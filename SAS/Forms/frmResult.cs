using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using SAS.ClassSet.Common;
using SAS.ClassSet.FunctionTools;
namespace SAS.Forms
{
    public partial class frmResult : Form
    {
        ListView listview;
        string path;
        SqlHelper help = new SqlHelper();
        public frmResult(ListView listview)
        {
            this.listview = listview;
            InitializeComponent();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
              OpenFileDialog ofd = new OpenFileDialog();
              ofd.InitialDirectory = Common.strEmailResultPath;
              if (ofd.ShowDialog() == DialogResult.OK)
              {
                  textBox2.Text = ofd.FileName.Trim();
                  path = ofd.FileName;
              }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            EamilResult ER = new EamilResult(listview,path);
            ER.SentResult();
            string updatecommand = string.Format("update Placement_Data set Grade={0} where Teacher='{1}' and Class_Address='{2}'", textBox1.Text.Trim(), listview.CheckedItems[0].SubItems[6].Text, listview.CheckedItems[0].SubItems[5].Text);
            if (help.Oledbcommand(updatecommand) > 0)
            {
                frmMain.fm.flashListview();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmResult_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            if (!System.IO.File.Exists(Common.strEmailResultPath))
            {
                Directory.CreateDirectory(Common.strEmailResultPath);
            }
            label3.Text = label3.Text + Common.strEmailResultPath.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmCamera frm = new frmCamera(Common.strEmailResultPath.ToString(), listview.CheckedItems[0].SubItems[6].Text + listview.CheckedItems[0].SubItems[8].Text);
            frm.Show();
        }
    }
}
