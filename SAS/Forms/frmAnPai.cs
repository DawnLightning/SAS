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
using SAS.ClassSet.MemberInfo;
using SAS.ClassSet.Common;

namespace SAS.Forms
{
    public partial class frmAnPai : Form
    {
        public frmAnPai()
        {
            InitializeComponent();
            this.Text = "添加安排";
        }
        string id;
        List<string> ListSupervisor = new List<string>();//存放督导成员
        List<string> oldsupervisors = new List<string>();//修改前的督导
        string[] Supervisor;
        string olds;
        int week;
        int day;
        int intclassnumber;
        public frmAnPai(string _ID)
        {
            InitializeComponent();
            this.Text = "修改安排";
            id = _ID;
        }
        const string newTeacher = "（新增教师）";
        public static string[] selectTeacher()
        {
            SqlHelper help = new SqlHelper();
            string cmdText = "SELECT Teacher FROM Teachers_Data"+" where IsSupervisor=false";
           
            List<string> ls = new List<string>();



          OleDbDataReader  DtReader = help.getDataReader(cmdText);
            while (DtReader.Read())
            {
                ls.Add(DtReader.GetValue(0).ToString());
            }
          
            return ls.ToArray();
        }
        private void frmAnPai_Load(object sender, EventArgs e)
        {
            cmbName.Items.Clear();
            foreach (var s in selectTeacher())
            {
                cmbName.Items.Add(s);
            }
            if (!string.IsNullOrEmpty(id))
            {
                int a = frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.IndexOf("年");
                int b = frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.IndexOf("月");
                int c = frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.IndexOf("日");
                int d = frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.IndexOf("-");
                int f = frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.IndexOf(" ");
                olds = frmMain.fm.listView1.CheckedItems[0].SubItems[9].Text;
                int year = Convert.ToInt32(frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.Substring(0, a));
                int month = Convert.ToInt32(frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.Substring(5).Substring(0, frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.Substring(5).IndexOf("月")));
                 day = Convert.ToInt32(frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.Substring(b + 1).Substring(0, frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.Substring(b + 1).IndexOf("日")));
                int thisweekday = CalendarTools.weekdays(CalendarTools.CaculateWeekDay(year, month, day));
                day = thisweekday;
                week = Convert.ToInt32(frmMain.fm.listView1.CheckedItems[0].SubItems[7].Text);
                string classnumber = frmMain.fm.listView1.CheckedItems[0].SubItems[8].Text.Substring(f + 1).Replace("-","").Replace("节","").ToString();
                intclassnumber = Convert.ToInt32(classnumber);
                textBox1.Text = week.ToString(); ;
                textBox2.Text = thisweekday.ToString();
                comboBox1.Text = frmMain.fm.listView1.CheckedItems[0].SubItems[3].Text;
                comboBox7.Text = classnumber;
                cmbName.Text = frmMain.fm.listView1.CheckedItems[0].SubItems[6].Text;

                comboBox1.Enabled = false;
                button1.Text = "确认修改";
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                comboBox7.Enabled = false;
                cmbName.Enabled = false;
               
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            
            PlacementInfo placement = new PlacementInfo(listView2.CheckedItems[0].SubItems[5].Text,label1.Text,cmbName.SelectedItem.ToString(),
                Convert.ToInt32(textBox1.Text.Trim()), Convert.ToInt32(textBox2.Text.Trim()),Convert.ToInt32(comboBox7.SelectedItem.ToString()),
                label13.Text,listView2.CheckedItems[0].SubItems[0].Text,listView2.CheckedItems[0].SubItems[1].Text,listView2.CheckedItems[0].SubItems[4].Text
                ,listView2.CheckedItems[0].SubItems[2].Text,listView2.CheckedItems[0].SubItems[3].Text,0);
            SqlHelper help = new SqlHelper();
            DataTable dtPlacement = help.getDs("select * from Placement_Data","Placement_Data").Tables[0];
            if (string.IsNullOrEmpty(id))
            {
                if (dtPlacement.Select("Class_ID='" + listView2.CheckedItems[0].SubItems[5].Text + "'").Length == 0)
                {
                    if (help.Insert(placement, "Placement_Data") > 0)
                    {
                        frmMain.fm.flashListview();
                        foreach (string supervisor in Supervisor)
                        {
                            string SpareTimeCommand = "update SpareTime_Data set IsAssigned=true where Supervisor='" + supervisor + "'"+ " and Spare_Number= '" + intclassnumber + "'" + " and Spare_Day=" + day + "" + " and Spare_Week=" + week + "";
                            string TeacherCommand = "update Teachers_Data set Class_Totality=Class_Totality+1 where Teacher='" + supervisor + "'";
                            help.Oledbcommand(SpareTimeCommand);
                            help.Oledbcommand(TeacherCommand);
                        }
                        MessageBox.Show("添加成功");
                    }
                    else
                    {
                        MessageBox.Show("添加失败");

                    }
                }
                else
                {
                    MessageBox.Show("请勿重复添加");
                }
            }
            else {

                if (help.update("Placement_Data", placement) >= 0)
                {
                  
                    DistinctSupervisor(olds,oldsupervisors);
                    List<string> s = new List<string>();
                    List<string> j = new List<string>();
                    foreach(string a in oldsupervisors){
                        s.Add(a);
                    }
                    foreach(string b in Supervisor){
                        j.Add(b);
                    }
                   
                    foreach(string old in oldsupervisors){

                        foreach(string news in Supervisor){
                            if(old.IndexOf(news)!=-1||news.IndexOf(old)!=-1){
                                s.Remove(old);
                                j.Remove(news);
                            }
                        }
                    }
                    foreach (string supervisor in j)
                    {
                        
                            string SpareTimeCommand = "update SpareTime_Data set IsAssigned=true where Supervisor='" + supervisor + "'" + " and Spare_Number= " + intclassnumber + "" + " and Spare_Day=" + day + "" + " and Spare_Week=" + week + "";
                            string TeacherCommand = "update Teachers_Data set Class_Totality=Class_Totality+1 where Teacher='" + supervisor + "'";
                            help.Oledbcommand(SpareTimeCommand);
                            help.Oledbcommand(TeacherCommand);
                       
                   
                    }
                    //对应的督导安排情况和总听课次数做相应的修改。     
                    foreach(string old in s){
                        string SpareTimeCommand = "update SpareTime_Data set IsAssigned=false where Supervisor like '" +"%"+old +"%"+"'"+ " and Spare_Number= " + intclassnumber + "" + " and Spare_Day=" + day + "" + " and Spare_Week=" + week + "";
                        string TeacherCommand = "update Teachers_Data set Class_Totality=Class_Totality-1 where Teacher like '" + "%"+ old + "%" +"'";
                        help.Oledbcommand(SpareTimeCommand);
                        help.Oledbcommand(TeacherCommand);
                    }
                    frmMain.fm.flashListview();
                    MessageBox.Show("修改成功！");
                    oldsupervisors.Clear();
                    olds = label13.Text;
                    flashui();
                   
                  

                }
                else {
                    MessageBox.Show("修改失败！");
                }
            
            }
        }
        private void flashui(){
            SqlHelper help = new SqlHelper();
            listView1.Items.Clear();
            listView3.Items.Clear();
           DataTable dtTeacher = help.getDs("select * from TeacherS_Data where IsSupervisor=true","Teachers_Data").Tables[0];
         DataTable dtSpareTime = help.getDs(string.Format("select * from SpareTime_Data where Spare_Week={0} and Spare_Day={1} and Spare_Number={2} order by IsAssigned ", Convert.ToInt32(textBox1.Text.Trim()), Convert.ToInt32(textBox2.Text.Trim()), Convert.ToInt32(comboBox7.SelectedItem.ToString().Trim()), cmbName.SelectedItem.ToString()), "SpareTime_Data").Tables[0];
         for (int i = 0; i < dtSpareTime.Rows.Count;i++ )
         {
            
                 DataRow[] dr = dtTeacher.Select("Teacher='" + dtSpareTime.Rows[i][2].ToString() + "'");
                 string[] supervisor = new string[] { 
             dtSpareTime.Rows[i][2].ToString(),
         
            Trueflase(Convert.ToBoolean(dtSpareTime.Rows[i][6]))
           
             };
                 ListViewItem lvi = new ListViewItem(supervisor);
                 listView1.Items.Add(lvi);
            
         }
         for (int j = 0; j < dtTeacher.Rows.Count;j++ )
         {
             
                 string[] supervisor = new string[] { 
                     dtTeacher.Rows[j][1].ToString(),
                 dtTeacher.Rows[j][8].ToString()
             };
                   
                 ListViewItem lvi = new ListViewItem(supervisor);
                    
                 listView3.Items.Add(lvi);
            
             
         }
            
            
            

        }
        private string DistinctSupervisor(string supervisor, List<string> ListSupervisor)
        {
            if (supervisor.IndexOf(",") != -1)
            {
                ListSupervisor.Add(supervisor.Substring(0, supervisor.IndexOf(",")));
                return DistinctSupervisor(supervisor.Substring(supervisor.IndexOf(",") + 1), ListSupervisor);
            }
            else
            {
                ListSupervisor.Add(supervisor);
                return supervisor;
            }

        }
        private void tb_KeyUp(object sender, KeyEventArgs e)
        {
          
        }


        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            listView1.Items.Clear();
            listView3.Items.Clear();
            if(textBox1.Text!=""&&textBox2.Text!=""){
          SqlHelper help=new SqlHelper();
         label1.Text = help.getDs( string.Format("select * from Teachers_Data where Teacher='{0}'", cmbName.SelectedItem.ToString()), "Teachers_Data").Tables[0].Rows[0][0].ToString();
         DataTable dt = help.getDs(string.Format("select * from Classes_Data where Class_Week={0} and Class_Day={1} and Class_Number={2} and Teacher='{3}'",Convert.ToInt32(textBox1.Text.Trim()), Convert.ToInt32(textBox2.Text.Trim()), Convert.ToInt32(comboBox7.SelectedItem.ToString().Trim()),cmbName.SelectedItem.ToString()),"Classes_Data").Tables[0];
         for (int i = 0; i < dt.Rows.Count; i++)
         {
             string[] classes = new string[] { 
             dt.Rows[i][8].ToString(),
             dt.Rows[i][7].ToString(),
             dt.Rows[i][9].ToString(),
             dt.Rows[i][10].ToString(),
             dt.Rows[i][6].ToString(),
             dt.Rows[i][0].ToString()
             };
             ListViewItem lvi = new ListViewItem(classes);
             listView2.Items.Add(lvi);
             
         }
         DataTable dtTeacher = help.getDs("select * from TeacherS_Data where IsSupervisor=true","Teachers_Data").Tables[0];
         DataTable dtSpareTime = help.getDs(string.Format("select * from SpareTime_Data where Spare_Week={0} and Spare_Day={1} and Spare_Number={2} order by IsAssigned ", Convert.ToInt32(textBox1.Text.Trim()), Convert.ToInt32(textBox2.Text.Trim()), Convert.ToInt32(comboBox7.SelectedItem.ToString().Trim()), cmbName.SelectedItem.ToString()), "SpareTime_Data").Tables[0];
         for (int i = 0; i < dtSpareTime.Rows.Count;i++ )
         {
            
                 DataRow[] dr = dtTeacher.Select("Teacher='" + dtSpareTime.Rows[i][2].ToString() + "'");
                 string[] supervisor = new string[] { 
             dtSpareTime.Rows[i][2].ToString(),
         
            Trueflase(Convert.ToBoolean(dtSpareTime.Rows[i][6]))
           
             };
                 ListViewItem lvi = new ListViewItem(supervisor);
                 listView1.Items.Add(lvi);
            
         }
         for (int j = 0; j < dtTeacher.Rows.Count;j++ )
         {
             
                 string[] supervisor = new string[] { 
                     dtTeacher.Rows[j][1].ToString(),
                 dtTeacher.Rows[j][8].ToString()
             };
                   
                 ListViewItem lvi = new ListViewItem(supervisor);
                    
                 listView3.Items.Add(lvi);
            
             
         }
            }
            else{
            }
          
        }
        private string Trueflase(bool B)
        {
            string chiness_tureorflase;
            if (B)
            {
                chiness_tureorflase = "已安排";
                return chiness_tureorflase;
            }
            else
            {
                chiness_tureorflase = "未安排";
                return chiness_tureorflase;
            }

        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            listView1.Items.Clear();
            listView3.Items.Clear();
            if (textBox1.Text != "" && textBox2.Text != ""&&comboBox7.SelectedItem!=null)
            {
                try
                {
                    cmbName.Items.Clear();
                    SqlHelper help = new SqlHelper();
                    string selectcommand = string.Format("select * from Classes_Data where Class_Week={0} and Class_Day={1} and Class_Number={2} and Class_Type ='{3}'", Convert.ToInt32(textBox1.Text.Trim()), Convert.ToInt32(textBox2.Text.Trim()), Convert.ToInt32(comboBox7.SelectedItem.ToString().Trim()),comboBox1.SelectedItem.ToString());
                    DataTable dtClass = help.getDs(selectcommand, "Classes_Data").Tables[0];
                    if (dtClass.Rows.Count != 0)
                    {
                        List<string> teachers = new List<string> { };
                        for (int i = 0; i < dtClass.Rows.Count; i++)
                        {
                            teachers.Add(dtClass.Rows[i][2].ToString());
                        }
                        string[] newteachers = teachers.Distinct<string>().ToArray();
                        cmbName.Items.AddRange(newteachers);
                        cmbName.SelectedIndex = 0;
                    }
                    else
                    {

                        MessageBox.Show("当前时间没有上课老师");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("请输入整数");
                }catch(NullReferenceException){
                    MessageBox.Show("请先选择授课方式");
                }
            }
            else
            {
               
                MessageBox.Show("请先填写周，星期，节次");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            comboBox7.SelectedItem = null;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label13.Text = "";
            ListSupervisor.Clear();
            for (int i = 0; i < listView1.CheckedItems.Count;i++ )
            {
                ListSupervisor.Add(listView1.CheckedItems[i].SubItems[0].Text);
            }
            for (int j = 0; j < listView3.CheckedItems.Count;j++ )
            {
                ListSupervisor.Add(listView3.CheckedItems[j].SubItems[0].Text);
            }
           
            Supervisor = ListSupervisor.Distinct<string>().ToArray();
            string b = "";
                foreach(string a in Supervisor){
                   if(a!=cmbName.Text){
                    b = b + "," + a;
                   }
                    
                }
                if(b!=""){
                label13.Text = b.Substring(1);
                }else{
                    MessageBox.Show("请选择对应的督导");
                }
           
        }

        private void label4_TextChanged(object sender, EventArgs e)
        {   if(listView2.CheckedItems.Count==1&&label4.Text!=""){
              button1.Enabled = true;
           }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string temp = Supervisor[0].ToString();
            for (int i = 0; i < Supervisor.Length-1;i++ )
            {
                Supervisor[i] = Supervisor[i + 1];
            }
            Supervisor[Supervisor.Length - 1] = temp;
            string b = "";
            foreach (string a in Supervisor)
            {
                if (a != cmbName.Text)
                {
                    b = b + "," + a;
                }

            }
            label13.Text = b.Substring(1);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label13_TextChanged(object sender, EventArgs e)
        {
            if (listView2.CheckedItems.Count == 1 && label13.Text != "")
            {
                button1.Enabled = true;
            }
        }

       

      


    }
}
