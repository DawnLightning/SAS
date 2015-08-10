
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAS.ClassSet.FunctionTools;
using SAS.ClassSet.ListViewShow;
using SAS.ClassSet.MemberInfo;
using SAS.ClassSet.Common;
namespace SAS.Forms
{
    public partial class frmLog : Form
    {
        public frmLog()
        {
            InitializeComponent();
        }
        int pagesize = 18;
        int currentpage = 1;
        int totalpage;
        SqlHelper help = new SqlHelper();
        DataTable dt = new DataTable(),dt1 = new DataTable(),dt2 = new DataTable();
      
        
        public void frmLog_Load_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            CheckForIllegalCrossThreadCalls = false;
            totalpage = help.totalpage("select * from Logs_Data", pagesize, "Logs_Data");
            labPageAll.Text = totalpage.ToString();
            DataTable dt = help.getDs("select * from Logs_Data","Logs_Data").Tables[0];
            UIShow show = new UIShow();
            show.logs_listview_write(dt, listView1, currentpage, pagesize);
            
        }
        private void btnPageDown_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (currentpage < totalpage)
            {
                currentpage++;
            
            }
            if (currentpage == totalpage)
            {
                currentpage = totalpage;
              
            }
            textBoxNow.Text = currentpage.ToString();
            DataTable dt = help.getDs("select * from Logs_Data", "Logs_Data").Tables[0];
            UIShow show = new UIShow();
            show.logs_listview_write(dt, listView1, currentpage, pagesize);
        }
        private void btnPageUp_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            if (currentpage > 1)
            {
                currentpage--;

            }
            if (currentpage == 1)
            {
                currentpage = 1;


            }
            textBoxNow.Text = currentpage.ToString();
            DataTable dt = help.getDs("select * from Logs_Data", "Logs_Data").Tables[0];
            UIShow show = new UIShow();
            show.logs_listview_write(dt, listView1, currentpage, pagesize);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认删除?注意!删除后将无法恢复!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            if (listView1.SelectedItems.Count > 0)
            {
                foreach (ListViewItem LVI in listView1.SelectedItems)
                {
                    string strCMD = "delete from Logs_Data where Time_Now = '" + LVI.SubItems[4].Text + "'";
                    help.Oledbcommand(strCMD);
                }
            }
            listView1.Items.Clear();
            frmLog_Load_1(sender,e);
          
        }
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            foreach (ListViewItem s in listView1.Items)
            {
                if (listView1.CheckedItems.Contains(s))
                {
                    s.Selected = true;
                }
                else
                {
                    s.Selected = false;
                }

            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem s in listView1.Items)
            {
                if (listView1.SelectedItems.Contains(s))
                {
                    s.Checked = true;
                }
                else
                {
                    s.Checked = false;
                }

            }
        }
        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem s in listView1.Items)
            {
                s.Checked = true;
            }
        }
        private EmailInfo InitializeEmailInfo()
        {
            EmailInfo EInfo = new EmailInfo();
            EInfo.User = Common.MailAddress;
            EInfo.PassWord = Common.MailPassword;
            EInfo.Title = listView1.CheckedItems[0].SubItems[3].Text;
            EInfo.Content = "";
            EInfo.AddFiles = help.getDs("select * from Logs_Data where Time_Now='" + listView1.CheckedItems[0].SubItems[4].Text + "'", "Logs_Data").Tables[0].Rows[0][6].ToString();
            EInfo.Receiver = help.getDs("select * from Teachers_Data where Teacher='" + listView1.CheckedItems[0].SubItems[1].Text + "'", "Teachers_Data").Tables[0].Rows[0][2].ToString();
            return EInfo;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(listView1.CheckedItems.Count==1)
            {
                EmailInfo EInfo = InitializeEmailInfo();
                Email senter = new Email();
                string updatecommand = "update Logs_Data set File_State='{flag}'" + "where Time_Now='" + listView1.CheckedItems[0].SubItems[4].Text + "'";
                frmLog log1 = this;
                senter.Send(new Email {Type=1, EI= EInfo,FL=log1,str=updatecommand });
                
                if(help.Oledbcommand(updatecommand)>0){
                    frmLog_Load_1(sender, e);
                }
            
            }
        }

        private void textBoxNow_TextChanged(object sender, EventArgs e)
        {
            if(int.Parse(textBoxNow.Text)>int.Parse(labPageAll.Text))
            {
                textBoxNow.Text = labPageAll.Text;
            }
            if (int.Parse(textBoxNow.Text) <1)
            {
                textBoxNow.Text = "1";
            }
        }
    }
}
