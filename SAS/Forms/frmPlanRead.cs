using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using SAS.ClassSet.FunctionTools;

namespace SAS.Forms
{
    public partial class frmPlanRead : Form
    {
        public frmPlanRead()
        {
            InitializeComponent();
        }
        string path2 = Environment.CurrentDirectory + "/" + "DataBase.mdb";//数据库路径
        List<string> al = new List<string> { };//有几张表，数组就有多大
        List<string> history = new List<string> { };
        List<string> newfilepath = new List<string> { };
        List<string> oldfilepath = new List<string> { };
        int numexcel = -1;//导入的Excel表的索引
        int NumItems = 0;//listview1的item数目
        int successflag = 0;//Excel表导入情况
        int numlistview = 0;
        private void btnDel_Click(object sender, EventArgs e)
        {
            int index2 = 0;
            if (this.listView1.SelectedItems.Count > 0)
            {
                index2 = this.listView1.SelectedItems[0].Index;
                al.RemoveAt(index2);
                numexcel--;
                NumItems--;
                listView1.Items[index2].Remove();
            }
            else MessageBox.Show("没有选择有效的文件,无法移除");
        }

        //-------------添加文件
        private void btnAdd_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            numexcel = -1;
            NumItems = 0;
            numlistview = 0;
            al.Clear();
            if (ofdAdd.ShowDialog() == DialogResult.OK)
            {
                int excelindex = 0;
                foreach (string a in ofdAdd.FileNames)//批量导入
                {


                    NumItems++;
                    numexcel++;
                    al.Add(a);

                    int index = a.LastIndexOf(@"\");
                    listView1.Items.Add(new ListViewItem(new string[] { a.Substring(index + 1), "未导入", a }));
                    excelindex++;


                }


                btnRead.Enabled = true;
            }

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (NumItems > 0)
            {   if(numlistview>=al.Count)
            {
                numlistview = 0;
            }
                progressBarRead.Maximum = listView1.Items.Count;
                progressBarRead.Step = 1;
                if(!backgroundWorker1.IsBusy)
                {
                backgroundWorker1.RunWorkerAsync();
                   
                }
                
                //cReadData rd = new cReadData();
               
                btnAdd.Enabled = false;
                btnDel.Enabled = false;
                btnRead.Enabled = false;
               
                //初始化数据及提示导入成功

              
            }
            else MessageBox.Show("无文件可导入");
        }



        private void frmPlanRead_Load(object sender, EventArgs e)
        {
            //TxtToFrom();

        }

        private void frmPlanRead_Load_1(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
         
            ExcelTools rd = new ExcelTools();
            for (int n = 0; n < al.Count; n++)
            {
              successflag=rd.ReadExcel(al[n].ToString());
             
                backgroundWorker1.ReportProgress(1,progressBarRead);
              
            }
        }
     
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarRead.PerformStep();
            if(successflag==1){
            listView1.Items[numlistview].SubItems[1].Text = "已导入";
            }else{
                listView1.Items[numlistview].SubItems[1].Text = "未导入";
            }
            numlistview++;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarRead.Value = progressBarRead.Maximum;

           
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            frmPlan.frmpr.NewMethod();
           
            if (MessageBox.Show("成功导入，是否马上补充填写对应教师信息", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frmTeacher frmt = new frmTeacher();
                frmt.Show();
                this.Close();
            }
            progressBarRead.Value = 0;
        }

        private void ofdAdd_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
