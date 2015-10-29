using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SAS.ClassSet.ListViewShow;
using SAS.ClassSet.FunctionTools;
using SAS.ClassSet.MemberInfo;
using System.Data.OleDb;
using System.Text.RegularExpressions;
namespace SAS.Forms
{
    public partial class frmTeacher : DevComponents.DotNetBar.Office2007Form
    {

        public static frmTeacher frmt;
        public frmTeacher()
        {
            InitializeComponent();
            this.EnableGlass = false;
            frmt = this;
        }

       SqlHelper pageshow;
       OleDbDataAdapter daTeachers;
       DataTable dtTeachers;
      
        int pagesize = 18;
        int currentpage = 1;
        int totalpage;
        int status;
        //int numTurnPage = 0; //翻页后要加的行数

        private void setTbEnable(bool b = false)
        {
            tBID.Enabled = b;
            tBName.Enabled = b;
            tBMail.Enabled = b;
            tBTel.Enabled = b;
            tbTitle.Enabled = b;
            cbIsDUDao.Enabled = b;
            tbBelongs.Enabled = b;
        }
        private void frmTeacher_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            setTbEnable();

         
            NoIdTeacher();//添加对应的教师

         

            pageshow = new SqlHelper();
            totalpage = pageshow.totalpage("select * from Teachers_Data", pagesize, "Teachers_Data");
            labPageAll.Text = totalpage + "";
            textBoxNow.Text = currentpage.ToString();
            DataTable dt = pageshow.ListviewShow("select * from Teachers_Data", currentpage, pagesize, "Teachers_Data");
            UIShow show = new UIShow();
            show.teachers_listview_write(dt, listView1);
          

           
            groupBox1.SendToBack();
        }


        private void btnMore_Click(object sender, EventArgs e)
        {
            
        }

        private bool isTrueOrFalse(string text)
        {
            if (text == "是")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnPageUp_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPageDown_Click(object sender, EventArgs e)
        {
           
        }
        private void clear_listview()
        {
            int count = this.listView1.Items.Count;
            for (int i = count - 1; i >= 0; i--)
            {

                this.listView1.Items.RemoveAt(i);

            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
           
        }



        private void btnNew_Click(object sender, EventArgs e)
        {
           
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
          
        }


        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {

            Point curPos = this.listView1.PointToClient(Control.MousePosition);
            ListViewItem lvwItem = this.listView1.GetItemAt(curPos.X, curPos.Y);
            foreach (ListViewItem s in listView1.Items)
            {
                s.Checked = false;
                s.Selected = false;
            }
            if (lvwItem != null)
            {
                lvwItem.Selected = true;
                if (e.X > 16) lvwItem.Checked = true;
                status = 1;
                setLviText(lvwItem);
            }
            else { setTbEnable(); }
        }

        private void setLviText(ListViewItem lvwItem)
        {
            setTbEnable(true);
            if (lvwItem.SubItems[0].Text != lvwItem.SubItems[1].Text)
                tBID.Text = lvwItem.SubItems[0].Text;
            else { tBID.Text = ""; }
            tBName.Text = lvwItem.SubItems[1].Text;
            tBMail.Text = lvwItem.SubItems[2].Text;
            tBTel.Text = lvwItem.SubItems[3].Text;
            tbTitle.Text = lvwItem.SubItems[4].Text;
            cbIsDUDao.Checked = isTrueOrFalse(lvwItem.SubItems[5].Text);
            tbBelongs.Text = lvwItem.SubItems[6].Text;
            tBName.Enabled = false;
        }



        private void tBName_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tBID.Text) || string.IsNullOrWhiteSpace(tBName.Text))
            {
                if (cbIsDUDao.Enabled || btnsave.Enabled)
                {
                    cbIsDUDao.Enabled = false;
                    btnsave.Enabled = false;
                }
            }
            else
            {
                if (cbIsDUDao.Enabled == false || btnsave.Enabled == false)
                {
                    cbIsDUDao.Enabled = true;
                    btnsave.Enabled = true;
                }
            }
        }


        private void NoIdTeacher()
        {
            SqlHelper help = new SqlHelper();
            DataTable dtClass = help.getDs("select * from Classes_Data","Classes_Data").Tables[0];
            DataRow[] NoIDteacher = dtClass.Select("Teacher_ID='" + "0000000000" + "' ");
            daTeachers = help.adapter("select * from Teachers_Data");
            dtTeachers = new System.Data.DataTable();
            daTeachers.Fill(dtTeachers);
            daTeachers.FillSchema(dtTeachers, SchemaType.Source);
            List<string> teachers = new List<string> { };
            DataTable Idteacher_dt = dtTeachers.Copy();   //  获取Class_Data的架构
            Idteacher_dt.Clear();
            for (int i = 0; i < NoIDteacher.Length; i++)
            {
                teachers.Add(NoIDteacher[i][2].ToString());
            }
            string[] newteachers = teachers.Distinct<string>().ToArray();
            teachers.Clear();
            for (int i = 0; i < dtTeachers.Rows.Count;i++ )
            {
                teachers.Add(dtTeachers.Rows[i][1].ToString());
            }
            string[] oldteachers = teachers.Distinct<string>().ToArray();
            for (int z = 0; z < newteachers.Length; z++)
            {
                for (int i = 0; i < oldteachers.Length;i++ )
                {
                    if (newteachers[z].ToString().Equals(oldteachers[i].ToString()))
                    {
                        newteachers[z] = "";
                    }
                    
                }
                
            }
           
            for (int i = 0; i < newteachers.Length;i++ )
            {   if(newteachers[i]!=""){
                DataRow dr_teacher = Idteacher_dt.NewRow();
                dr_teacher[1] = newteachers[i].ToString();
                dr_teacher[0] = newteachers[i].ToString();
                dr_teacher[7] = 0;
                dr_teacher[8] = 0;
                dr_teacher[9] = 0;
                dr_teacher[10] = 0;
                Idteacher_dt.Rows.Add(dr_teacher);
               }
            }
           
            dtTeachers.Merge(Idteacher_dt, true);
            daTeachers.Update(dtTeachers);
            
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            
        }

      
        private void tBMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem s in listView1.Items)
            {
                s.Checked = false;
                s.Selected = false;
            }
            status = 0;
            string[] arr = new string[7] { "", "", "", "", "", "", "" };
            ListViewItem lvi = new ListViewItem(arr);
            setLviText(lvi);
            tBName.Enabled = true;
            this.tBID.Focus();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count != 0)
            {
                SqlHelper help = new SqlHelper();
                string condition = " where " + "Teacher='" + listView1.CheckedItems[0].SubItems[1].Text + "'";

                if (help.delete("Teachers_Data", condition) > 0)
                {
                    MessageBox.Show("删除成功");
                }
                else
                {
                    MessageBox.Show("删除失败");
                }

                listView1.Items.Clear();
                setTbEnable();
                clear_listview();
                totalpage = pageshow.totalpage("select * from Teachers_Data", pagesize, "Teachers_Data");
                labPageAll.Text = totalpage + "";
                textBoxNow.Text = currentpage.ToString();
                DataTable dt = pageshow.ListviewShow("select * from Teachers_Data", currentpage, pagesize, "Teachers_Data");
                UIShow show = new UIShow();
                show.teachers_listview_write(dt, listView1);

                groupBox1.SendToBack();
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                frmTeacher_Load(sender, e);
            }
            else
            {
                listView1.Items.Clear();
                string fe = "Teacher like '%" + txtSearch.Text.Trim() + "%'";
                string cmdText = "SELECT * FROM Teachers_Data WHERE " + fe;
                pageshow = new SqlHelper();
                totalpage = pageshow.totalpage(cmdText, pagesize, "Teachers_Data");
                labPageAll.Text = totalpage + "";
                textBoxNow.Text = currentpage.ToString();
                DataTable dt = pageshow.ListviewShow(cmdText, currentpage, pagesize, "Teachers_Data");
                UIShow show = new UIShow();
                show.teachers_listview_write(dt, listView1);
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (tBID.Text != "" && tBName.Text != "" && tBMail.Text != "" && tBTel.Text != "" && tbTitle.Text != "" && tbBelongs.Text != "")
            {


                string EmailPattern = @"^([A-Za-z0-9]{1}[A-Za-z0-9_]*)@([A-Za-z0-9_]+)[.]([A-Za-z0-9_]*)$";//E-Mail地址格式的正则表达式
                if (Regex.IsMatch(tBMail.Text.Trim(), EmailPattern))
                {
                    if (status == 1)
                    {

                        TeacherInfo teacher = new TeacherInfo();
                        teacher.TeacherId = tBID.Text;
                        teacher.TeacherName = tBName.Text;
                        teacher.Email = tBMail.Text;
                        teacher.Phone = tBTel.Text;
                        teacher.Title = tbTitle.Text;
                        teacher.IsSupervisor = cbIsDUDao.Checked;
                        teacher.TeachingSection = tbBelongs.Text;


                        SqlHelper help = new SqlHelper();
                        if (help.update("Teachers_Data", teacher) > 0)
                        {
                            MessageBox.Show("修改成功");
                            listView1.Items.Clear();
                            DataTable dt = pageshow.ListviewShow("select * from Teachers_Data", currentpage, pagesize, "Teachers_Data");
                            UIShow show = new UIShow();
                            show.teachers_listview_write(dt, listView1);
                        }

                    }
                    if (status == 0)
                    {

                        TeacherInfo teacher = new TeacherInfo();
                        teacher.TeacherId = tBID.Text;
                        teacher.TeacherName = tBName.Text;
                        teacher.Email = tBMail.Text;
                        teacher.Phone = tBTel.Text;
                        teacher.Title = tbTitle.Text;
                        teacher.IsSupervisor = cbIsDUDao.Checked;
                        teacher.TeachingSection = tbBelongs.Text;
                        SqlHelper help = new SqlHelper();
                        if (help.Insert(teacher, "Teachers_Data") > 0)
                        {
                            MessageBox.Show("添加成功");
                        }
                        else
                        {
                            MessageBox.Show("操作失败");
                        }
                        clear_listview();
                        totalpage = pageshow.totalpage("select * from Teachers_Data", pagesize, "Teachers_Data");
                        labPageAll.Text = totalpage + "";
                        textBoxNow.Text = currentpage.ToString();
                        DataTable dt = pageshow.ListviewShow("select * from Teachers_Data", currentpage, pagesize, "Teachers_Data");
                        UIShow show = new UIShow();
                        show.teachers_listview_write(dt, listView1);
                    }

                    this.btnsave.Enabled = false;
                }
                else
                {
                    MessageBox.Show("请输入正确的邮箱地址");
                }


            }

            else
            {
                MessageBox.Show("请确保数据完整");
            }
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            clear_listview();
            if (currentpage > 1)
            {
                currentpage--;
                //numTurnPage -= 17;
            }
            if (currentpage == 1)
            {
                currentpage = 1;
                //numTurnPage = 0;

            }
            textBoxNow.Text = currentpage.ToString();
            DataTable dt = pageshow.ListviewShow("select * from Teachers_Data", currentpage, pagesize, "Teachers_Data");
            UIShow show = new UIShow();
            show.teachers_listview_write(dt, listView1);

        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            clear_listview();
            if (currentpage < totalpage)
            {
                currentpage++;
                //numTurnPage += 17;
            }
            if (currentpage == totalpage)
            {
                currentpage = totalpage;
                //numTurnPage = (totalpage - 1) * 17;
            }
            textBoxNow.Text = currentpage.ToString();
            // cListview pageshow = new cListview();
            DataTable dt = pageshow.ListviewShow("select * from Teachers_Data", currentpage, pagesize, "Teachers_Data");
            UIShow show = new UIShow();
            show.teachers_listview_write(dt, listView1);
        }
    }

}

