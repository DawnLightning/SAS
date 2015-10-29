using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAS.ClassSet.FunctionTools;
using SAS.ClassSet.Common;
namespace SAS.Forms
{
    public partial class frmClasses : DevComponents.DotNetBar.Office2007Form
    {
        public DateTime CurrentTime;
        public DataTable dtClasses;
        public DataTable dtSpareTime;

        int thisday;//星期
        int thisweek;//周
        public frmClasses(DateTime dt,DataTable dtClasses)
        {
            this.EnableGlass = false;
            this.dtClasses = dtClasses;
            this.CurrentTime = dt;
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //生成间隔符“-”
        private string addseparator(int classnumber)
        {
            string newclassnumber = "";
            switch (classnumber)
            {
                case 12: newclassnumber = "1-2"; break;
                case 13: newclassnumber = "1-3"; break;
                case 23: newclassnumber = "2-3"; break;
                case 24: newclassnumber = "2-4"; break;
                case 34: newclassnumber = "3-4"; break;
                case 35: newclassnumber = "3-5"; break;
                case 45: newclassnumber = "4-5"; break;
                case 46: newclassnumber = "4-6"; break;
                case 67: newclassnumber = "6-7"; break;
                case 68: newclassnumber = "6-8"; break;
                case 78: newclassnumber = "7-8"; break;
                case 79: newclassnumber = "7-9"; break;
                case 89: newclassnumber = "8-9"; break;
                case 1011: newclassnumber = "10-11"; break;
                case 1112: newclassnumber = "11-12"; break;
                case 1012: newclassnumber = "10-12"; break;
            }
            return newclassnumber;
        }
        private int numbersupervisor(int week,int day,int classnumber){
            int count = dtSpareTime.Select("Spare_Week=" + week + " and Spare_Day=" + day + " and Spare_Number=" + classnumber + "").Length;
            return count;

        }
        private void frmClasses_Load(object sender, EventArgs e)
        {
            dtSpareTime = new SqlHelper().getDs("select * from SpareTime_Data","SpareTime").Tables[0];
           thisday= CalendarTools.weekdays(CalendarTools.CaculateWeekDay(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day));
           thisweek = CalendarTools.WeekOfYear(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day) - CalendarTools.WeekOfYear(Common.Year, Common.Month, Common.Day)+1;
           DataRow[] dr = dtClasses.Select("Class_Week=" + thisweek + " and Class_Day=" + thisday + "", "Class_Number asc");
          if(dr.Length!=0){
             
              for (int i = 0; i < dr.Length;i++ )
              {
                  string[] strclass = new string[]{
                      (i+1).ToString(),
                     CurrentTime.ToLongDateString()+" "+addseparator(Convert.ToInt32(dr[i][5]))+"节"+" (第"+(thisweek.ToString()+"周 第"+thisday.ToString()+"天")+")",
                     dr[i][2].ToString(),
                     dr[i][8].ToString(),
                     dr[i][10].ToString(),
                     dr[i][9].ToString(),
                     numbersupervisor(thisweek,thisday,Convert.ToInt32(dr[i][5])).ToString()
                  };
                  ListViewItem lvi = new ListViewItem(strclass);
                  listView1.Items.Add(lvi);
              }
          }
          
        }
        
    }
}
