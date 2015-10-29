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
namespace SAS.Forms
{
    public partial class frmSpareTime : DevComponents.DotNetBar.Office2007Form
    {
        public frmSpareTime()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        List<string> ListSupervisor = new List<string>();
        List<int> week = new List<int>();
        List<string> strweek = new List<string>();
        string b = "";//暂存形如 1，2，3的字符串，用于描述已填的周
        SqlHelper help = new SqlHelper();
        private void frmSpareTime_Load(object sender, EventArgs e)
        {

            string selectcommand = "Select * from Teachers_Data where IsSupervisor = true ";
            DataTable dt = help.getDs(selectcommand, "Teachers_Data").Tables[0];
            DataTable dtSpareTime = help.getDs("select * from SpareTime_Data", "SpareTime_Data").Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListSupervisor.Add(dt.Rows[i][1].ToString());
                for (int j = 1; j < 20; j++)
                {
                    string condition = string.Format("Supervisor='{0}'and Spare_Week={1}", dt.Rows[i][1].ToString(), j);
                    DataRow[] dr = dtSpareTime.Select(condition);
                    if (dr.Length != 0)
                    {
                        week.Add(j);
                    }
                }
                foreach (int a in week)
                {

                    b = b + a.ToString() + " ";
                }
                strweek.Add(b);
                b = "";
                week.Clear();
            }

            for (int i = 0; i < ListSupervisor.Count; i++)
            {
                string[] teachers_arrages = new string[]
                { 
                 ListSupervisor[i].ToString(),
                  strweek[i].ToString()
               
                  
                };
                ListViewItem lvi = new ListViewItem(teachers_arrages);
                listView1.Items.Add(lvi);

            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                foreach (ListViewItem item in this.listView1.CheckedItems)
                {
                    if (item != e.Item)
                        item.Checked = false;
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
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

        private void button3_Click(object sender, EventArgs e)
        {

           
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count != 0)
            {
                string deletecommand = string.Format("delete * from Teachers_Data where Teacher ='{0}'", listView1.CheckedItems[0].SubItems[0].Text);
                if (help.Oledbcommand(deletecommand) > 0)
                {
                    MessageBox.Show("移除成功");

                }
                listView1.Items.Clear();
                ListSupervisor.Clear();
                week.Clear();
                strweek.Clear();
                frmSpareTime_Load(sender, e);
            }
            else
            {
                MessageBox.Show("请勾选相应督导");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count != 0)
            {
                string selectcommand = string.Format(@"Select * from Teachers_Data where Teacher='{0}'", listView1.CheckedItems[0].SubItems[0].Text.Trim());
                DataTable dt = help.getDs(selectcommand, "Teacher_Data").Tables[0];
                if (listView1.CheckedItems[0].SubItems[1].Text != "")
                {

                    string startweek = listView1.CheckedItems[0].SubItems[1].Text.Substring(0, listView1.CheckedItems[0].SubItems[1].Text.IndexOf(" "));
                    frmSupervisor sp = new frmSupervisor(dt.Rows[0][1].ToString(), dt.Rows[0][0].ToString(), startweek);
                    sp.Show();
                }
                else
                {
                    string startweek = "1";
                    frmSupervisor sp = new frmSupervisor(dt.Rows[0][1].ToString(), dt.Rows[0][0].ToString(), startweek);
                    sp.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("请勾选相应的督导");

            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            string deletecommand = "delete * from SpareTime_Data";
            string selectcommand = "select * from SpareTime_Data";
            DataTable dt = help.getDs(selectcommand, "SpareTime_Data").Tables[0];
            if (dt.Rows.Count != 0)
            {
                if (help.Oledbcommand(deletecommand) > 0)
                {
                    try
                    {
                        Placement selecttime = new Placement();
                        selecttime.AutoSelectSpareTime(progressBar1);
                        if (progressBar1.Value == progressBar1.Maximum)
                        {
                            MessageBox.Show("自动填补成功");
                            progressBar1.Value = 0;
                            listView1.Items.Clear();
                            ListSupervisor.Clear();
                            week.Clear();
                            strweek.Clear();
                            frmSpareTime_Load(sender, e);
                        }
                        else
                        {

                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());


                    }
                }
                else
                {
                    MessageBox.Show("请手动删除当前数据表数据");
                }
            }
            else
            {
                try
                {
                    Placement selecttime = new Placement();
                    selecttime.AutoSelectSpareTime(progressBar1);
                    if (progressBar1.Value == progressBar1.Maximum)
                    {
                        MessageBox.Show("自动填补成功");
                        progressBar1.Value = 0;
                        listView1.Items.Clear();
                        ListSupervisor.Clear();
                        week.Clear();
                        strweek.Clear();
                        frmSpareTime_Load(sender, e);
                    }
                    else
                    {

                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());


                }
            }
        }
    }
}
            
          
           
         
           
        
    

