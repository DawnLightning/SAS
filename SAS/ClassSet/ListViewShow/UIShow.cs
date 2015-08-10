using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using SAS.ClassSet.FunctionTools;
namespace SAS.ClassSet.ListViewShow
{
    class UIShow
    {
        public void logs_listview_write(DataTable dtLogs, ListView listview, int currpage, int pagesize)
        {

            for (int i = 0; i < dtLogs.Rows.Count; i++)
            {
                string[] Logsrecovery = new string[]
                { 
                  Ordernumber(i+1,currpage,pagesize),
                  
                   dtLogs.Rows[i]["Email_Receiver"].ToString(),
                   dtLogs.Rows[i]["Teacher_Identity"].ToString(),
                  dtLogs.Rows[i]["Email_Theme"].ToString(),
                  dtLogs.Rows[i]["Time_Now"].ToString(),
                 dtLogs.Rows[i]["Email_Type"].ToString(),
                  dtLogs.Rows[i]["File_State"].ToString()
                };
                ListViewItem lvi = new ListViewItem(Logsrecovery);
                listview.Items.Add(lvi);

            }
        }
        public void listview_write_classes_data(DataTable dtClass, ListView listview1)
        {


            for (int i = 0; i < dtClass.Rows.Count; i++)
            {
                string a = addseparator(Convert.ToInt32(dtClass.Rows[i]["Class_Number"]));
                string[] sl = new String[]
                { 
                  //Ordernumber(i+1,currpage,pagesize),//序号
                   dtClass.Rows[i]["Teacher"].ToString(),
                   dtClass.Rows[i]["Class_Content"].ToString(),
                   //time + 
                   "第" +  dtClass.Rows[i]["Class_Week"].ToString() + "周," + "星期" + dtClass.Rows[i]["Class_Day"].ToString()+ "," +a+ "节",//得知具体日期
                    dtClass.Rows[i]["Class_Address"].ToString(),
                   dtClass.Rows[i]["Spcialty"].ToString(),
                   dtClass.Rows[i]["Class_Type"].ToString(),
                };
                ListViewItem lvi = new ListViewItem(sl);
                listview1.Items.Add(lvi);
            }
        }
        public void teachers_listview_write(DataTable dtTeachers, ListView listview)
        {
           
            for (int i = 0; i < dtTeachers.Rows.Count; i++)
            {
                string[] teachers_arrages = new string[]
                { 
                    dtTeachers.Rows[i][0].ToString(),
                    dtTeachers.Rows[i][1].ToString(),
                    dtTeachers.Rows[i][2].ToString(),   
                    dtTeachers.Rows[i][3].ToString(),     
                    dtTeachers.Rows[i][4].ToString(),      
                    Trueflase( Convert.ToBoolean( dtTeachers.Rows[i][5])), 
                    dtTeachers.Rows[i][6].ToString()
                };
                ListViewItem lvi = new ListViewItem(teachers_arrages);
                listview.Items.Add(lvi);

            }
        }
        public void placement_listview_write(DataTable dtPlacement, ListView listview,int currentpage,int pagesize)
        {
           
            for (int i = 0; i < dtPlacement.Rows.Count; i++)
            {
                int nowday = Convert.ToInt32(dtPlacement.Rows[i][4]);
                int nowWeeks = Convert.ToInt32(dtPlacement.Rows[i][3]);
                string time = CalendarTools.getdata(Common.Common.Year, nowWeeks, nowday-CalendarTools.weekdays(CalendarTools.CaculateWeekDay(Common.Common.Year,Common.Common.Month,Common.Common.Day)), Common.Common.Month, Common.Common.Day).ToLongDateString();
                string[] placement_arrages = new string[]
                {
                    Ordernumber(i+1,currentpage,pagesize),//序号
                    dtPlacement.Rows[i][9].ToString(),
                    dtPlacement.Rows[i][8].ToString(),
                    dtPlacement.Rows[i][10].ToString(),
                    dtPlacement.Rows[i][11].ToString(),
                    dtPlacement.Rows[i][7].ToString(),
                    dtPlacement.Rows[i][2].ToString(),
                    dtPlacement.Rows[i][3].ToString(),//周次
                    time + " "+addseparator(Convert.ToInt32(dtPlacement.Rows[i][5])) + "节",//得知具体日期
                    dtPlacement.Rows[i][6].ToString(),
                    dtPlacement.Rows[i][12].ToString()
                };
                ListViewItem lvi = new ListViewItem(placement_arrages);
                listview.Items.Add(lvi);

            }
        }
        //生成序号
        private string Ordernumber(int i, int currentpage, int pagesize)
        {
            string ordernumber = "";
            if (currentpage == 1)
            {
                ordernumber = i.ToString();
                return ordernumber;
            }
            else
            {
                int nextnumber = ((currentpage - 1) * pagesize) + i;
                ordernumber = nextnumber.ToString();
                return ordernumber;
            }
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



        //true false 改为是否
        private string Trueflase(bool B)
        {
            string chiness_tureorflase;
            if (B)
            {
                chiness_tureorflase = "是";
                return chiness_tureorflase;
            }
            else
            {
                chiness_tureorflase = "否";
                return chiness_tureorflase;
            }

        }
    }
}
