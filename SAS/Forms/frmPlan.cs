using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAS.ClassSet.FunctionTools;
using System.Data.OleDb;
using SAS.ClassSet.ListViewShow;

namespace SAS.Forms
{
    public partial class frmPlan : Form
    {


        public static frmPlan frmpr;

        public frmPlan()
        {
            InitializeComponent();
            frmpr = this;
        }


        public int pagesize = 19;
        public int currentpage = 1;
        public int totalpage;
        SqlHelper pageshow;


        #region weijie
        //外部调用，暂时不明作用
        public void NewMethod()
        {
            clear_listview();
        
            DataTable dt = pageshow.ListviewShow("select * from Classes_Data", currentpage, pagesize, "Classes_Data");
            UIShow show = new UIShow();
            show.listview_write_classes_data(dt, listView1);
            pageshow = new SqlHelper();
            totalpage = pageshow.totalpage("select * from Classes_Data", pagesize, "Classes_Data");
            labPageAll.Text = totalpage + "";
            textBoxNow.Text = currentpage.ToString();

        }

        #endregion

       
        private void frmPlan_Load(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();

                pageshow = new SqlHelper();
                totalpage = pageshow.totalpage("select * from Classes_Data", pagesize, "Classes_Data");
                labPageAll.Text = totalpage + "";
                textBoxNow.Text = currentpage.ToString();
                DataTable dt = pageshow.ListviewShow("select * from Classes_Data",currentpage,pagesize,"Classes_Data");
                UIShow show = new UIShow();
                show.listview_write_classes_data(dt,listView1);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clear_listview()
        {
            int count = this.listView1.Items.Count;
            for (int i = count - 1; i >= 0; i--)
            {

                this.listView1.Items.RemoveAt(i);

            }
        }
        //*************************-----------------听课安排翻页功能结束 


        //--------添加“—”分隔符




        //--------------------首页



        private void button1_Click(object sender, EventArgs e)
        {
            frmPlanRead frmp = new frmPlanRead();
            frmp.Show();
        }
       


        private void btnPageUp_Click_1(object sender, EventArgs e)
        {
            clear_listview();
            if (currentpage > 1)
            {
                currentpage--;

            }
            if (currentpage == 1)
            {
                currentpage = 1;


            }
            textBoxNow.Text = currentpage.ToString();
         
            DataTable dt = pageshow.ListviewShow("select * from Classes_Data", currentpage, pagesize, "Classes_Data");
            UIShow show = new UIShow();
            show.listview_write_classes_data(dt, listView1);
        }

        private void btnPageDown_Click_1(object sender, EventArgs e)
        {
            clear_listview();
            if (currentpage < totalpage)
            {
                currentpage++;

            }
            if (currentpage == totalpage)
            {
                currentpage = totalpage;

            }
            textBoxNow.Text = currentpage.ToString();
            DataTable dt = pageshow.ListviewShow("select * from Classes_Data", currentpage, pagesize, "Classes_Data");
            UIShow show = new UIShow();
            show.listview_write_classes_data(dt, listView1);
        }

      

      

        private void btnDelType_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要执行删除操作？这将会删除当前的所有数据！", "警告！！！", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {

                    SqlHelper help = new SqlHelper();
                    help.delete("Classes_Data"," ");
                    frmPlan_Load(sender, e);

                }
                catch (Exception)
                {
                   
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




    }


}

