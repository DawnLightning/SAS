using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Threading;
using System.ComponentModel;
using SAS.ClassSet.MemberInfo;
namespace SAS.ClassSet.FunctionTools
{
    class Placement
    {  
        #region 变量
        List<int> spareclass = new List<int> { 12, 13, 23, 24, 34, 35, 45, 67, 68, 78, 79, 89, 1011, 1112, 1012 };//枚举所有的连续节次
        private int Week;//周数
        private int Day;//上课天
        private int Index = 0;//数组spareclass的索引
        DataTable dtSpareTime;//空闲时间表
        DataTable dtSpareTimeWeek;//周空闲时间表
        DataTable dtSpareTimeDay;//天空闲时间表
        DataTable dtPlacement;//听课安排表
        DataTable dtTeachers;//教师表
        DataTable dtSupervisor;//督导表
        DataTable dtClasses;//课程表
        DataRow[] drSpareTimeClass;//督导能听的课
        DataRow[] drSupervisor;//督导小组
        DataRow[] drTeacher;//能听的课的老师
        DataRow[] drClasses;//总的课
        List<string> supervisor=new List<string>();//最终确定的督导
        SqlHelper helper=new SqlHelper();
        
        OleDbDataAdapter daSpareTime;
        OleDbDataAdapter daPlacement;
        OleDbDataAdapter daTeacher;
        string strSelect_SpareTime_Data = "select * from SpareTime_Data";
        string strSelect_Placement_Data = "select * from Placement_Data";
        string strSelect_Class_Data = "select * from Classes_Data";
        string strSelect_Teachers_Data = "select * from Teachers_Data";
        string Supervisors;
       #endregion
        #region 主方法
        public void MakePlacement(PlacementConfig config)
        {
            try
            {
                dtClasses = helper.getDs(strSelect_Class_Data, "Classes_Data").Tables[0];
                dtTeachers = helper.getDs(strSelect_Teachers_Data, "Teachers_Data").Tables[0];
                dtTeachers = dtTeachers.Select("IsSupervisor=False").CopyToDataTable();
                dtSupervisor = helper.getDs(strSelect_Teachers_Data, "Teachers_Data").Tables[0];
                dtSupervisor = dtSupervisor.Select("IsSupervisor=True").CopyToDataTable();
                dtSpareTime = helper.getDs(strSelect_SpareTime_Data, "SpaerTime_Data").Tables[0];
                dtPlacement = helper.getDs(strSelect_Placement_Data, "Placement_Data").Tables[0];
                for (Week = config.Cbegin_week; Week < 19; Week++)
                {
                    UpdataWeek(dtSupervisor);
                    if (CheckWeekPeo(Week) >= config.Cnumpeo_min)
                    {
                        SelectSameDay(config, Week);
                    }
                    else
                    {
                        continue;
                    }
                }
                UpdateALL();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("排课失败" + ex.ToString());
            }
               
           
        }
        #endregion
        #region 辅助方法
        public int CheckWeekPeo(int week)
        {
            if (dtSpareTime.Select("Spare_Week=" + week + "").Length != 0)
            {
                dtSpareTimeWeek = dtSpareTime.Select("Spare_Week=" + week + "").CopyToDataTable();
                return dtSpareTimeWeek.Rows.Count;
            }
            else
            {
                return 0;
            }
           
        }
        public int CheckDayPeo(int day)
        {  
            if(dtSpareTimeWeek.Select("Spare_Day=" + day + "").Length!=0){
                     dtSpareTimeDay = dtSpareTimeWeek.Select("Spare_Day=" + day + "").CopyToDataTable();
                        return dtSpareTimeDay.Rows.Count;
             }
            else
             {
                 return 0;
             }
           
        }
        private void SelectSameDay(PlacementConfig config,int week)
        {
          
            for (Day =config.Cbegin_day ; Day < 6; Day++)
            {
                int weekplacement = dtPlacement.Select("Class_week= '" + week + "'").Length;
                if(CheckDayPeo(Day)>=config.Cnumpeo_min &&weekplacement<=config.Cnumclass_week-1){
                    UpdataDay(dtSupervisor);
                    SelectSameClass(config);

                }else{
                    continue;
                }
            }
        }
        private DataRow[] ContorlProportion(int Proportion)//控制理论课和实验课的比例
        {
                 DataRow[] dr = null;
                 //dr = dtClasses.Select(" Class_Week=" + Week + "and Class_Day=" + Day + " and Class_Number=" + spareclass[Index] + "and Class_Type ='理论' ");
                 //   dr = dtClasses.Select(" Class_Week=" + Week + "and Class_Day=" + Day + " and Class_Number=" + spareclass[Index] + "and Class_Type ='实验' ");
                 Random rd = new Random();
                 int i = rd.Next(1, 100);
                 System.Diagnostics.Debug.WriteLine(i.ToString());
                 if (i > 0 && i <= Proportion)
                 {
                     dr = dtClasses.Select(" Class_Week=" + Week + "and Class_Day=" + Day + " and Class_Number=" + spareclass[Index] + "and Class_Type ='理论' ");
                     return dr;
                 }
                 else
                 {
                     dr = dtClasses.Select(" Class_Week=" + Week + "and Class_Day=" + Day + " and Class_Number=" + spareclass[Index] + "and Class_Type ='实验' ");
                     return dr;
                 }

           
        }
        private void SelectSameClass(PlacementConfig config)
        {
            for (Index = 0; Index < 15; Index++)
            {
                supervisor.Clear();
                Supervisors = "";
                drSpareTimeClass = dtSpareTimeDay.Select("Spare_Number= '" + spareclass[Index] + "'");
              
                 drClasses = ContorlProportion(config.Proportion);
                 drSupervisor = dtSupervisor.Select("Class_WeekNumber<2 and Class_DayNumber = 0", "Class_Totality asc");
                 drTeacher = dtTeachers.Select("Accept_ClassNumber=0");
                int count = Supervisor(drSpareTimeClass,drSupervisor).Count;
                if(count<config.Cnumpeo_min){
                    break;
                }
                else if (count >= config.Cnumpeo_min && count <= config.Cnumpeo_max)
                {
                    foreach (string dr in supervisor)
                    {
                        Supervisors = Supervisors + "," + dr;
                       
                    }
                     WritePlacement(Supervisors,Teacher(drClasses,drTeacher));
                }
                else if (count > config.Cnumpeo_max)
                {
                    for (int i = 0; i < config.Cnumpeo_max; i++)
                    {
                        Supervisors = Supervisors + "," + supervisor[i];
                    }
                    WritePlacement(Supervisors, Teacher(drClasses, drTeacher));
                }
                else
                {
                    break;
                }

            }
                            
        }
        private List<string> Supervisor(DataRow[] drSpareTimeClass, DataRow[] drSupervisor)
        {
            supervisor=new List<string>(); 
            for (int i = 0; i < drSupervisor.Length;i++ )
            {
                for (int j = 0; j < drSpareTimeClass.Length;j++ )
                {
                    if (drSupervisor[i]["Teacher"].Equals(drSpareTimeClass[j]["Supervisor"]))
                    {
                        supervisor.Add(drSupervisor[i]["Teacher"].ToString());
                        break;
                    }
                }
            }
            return supervisor;
        }
        private void WritePlacement(string supervisors,DataRow teacher)
        {
                    if(teacher!=null){
                    DataRow dr =dtPlacement.NewRow();
                    dr["Class_ID"] = teacher[0].ToString();
                    dr["Teacher_ID"] = teacher[1].ToString();
                    dr["Teacher"] = teacher[2].ToString();
                    dr["Class_week"] = Convert.ToInt32(teacher[3]);
                    dr["Class_Day"] = Convert.ToInt32(teacher[4]);
                    dr["Class_Number"] = Convert.ToInt32(teacher[5]);
                    dr["Class_Name"] = teacher[6].ToString();
                    dr["Class_Content"] = teacher[7].ToString();
                    dr["Class_Address"] = teacher[8].ToString();
                    dr["Class_Type"] = teacher[9].ToString();
                    dr["Spcialty"] = teacher[10].ToString();
                    dr["Supervisor_Name"] = supervisors.Substring(1);
                    dr["Grade"] = 0;
                    
                    dtPlacement.Rows.Add(dr);


                    UpdataSupervisor(supervisors.Substring(1));
                    UpdataTeacher(teacher);
                    }
        }
        private void UpdataWeek(DataTable dt){
           for(int i=0;i<dt.Rows.Count;i++ ){
               dt.Rows[i]["Class_WeekNumber"]=0;
           }
        }
         private void UpdataDay(DataTable dt){
           for(int i=0;i<dt.Rows.Count;i++ ){
               dt.Rows[i]["Class_DayNumber"]=0;
           }
        }
        private void UpdateALL(){
            daPlacement = helper.adapter(strSelect_Placement_Data);
            daPlacement.Update(dtPlacement);
            daTeacher = helper.adapter(strSelect_Teachers_Data);
            daTeacher.Update(dtTeachers);
            daTeacher.Update(dtSupervisor);
            daSpareTime = helper.adapter(strSelect_SpareTime_Data);
            daSpareTime.Update(dtSpareTime);
            Main.fm.SetStatusText("自动生成听课安排成功！", 0);
            Main.fm.flashListview();

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
        private void UpdataSupervisor(string s){
            List<string> lastsupervisor = new List<string>();
            DistinctSupervisor(s,lastsupervisor);
            for (int i = 0; i < lastsupervisor.Count; i++)
            {
                DataRow[] dr = dtSupervisor.Select("Teacher='" + lastsupervisor[i].ToString() + "'");
                dr[0]["Class_Totality"] = Convert.ToInt32(dr[0]["Class_Totality"]) + 1;
                dr[0]["Class_WeekNumber"] = Convert.ToInt32(dr[0]["Class_WeekNumber"]) + 1;
                dr[0]["Class_DayNumber"] = Convert.ToInt32(dr[0]["Class_DayNumber"]) + 1;

                dtSpareTime.Select("Supervisor='" + lastsupervisor[i].ToString() + "'" + "and Spare_Number= '" + spareclass[Index] + "'" + "and Spare_Day=" + Day + "" + "and Spare_Week=" + Week + "")[0]["IsAssigned"] = true;
                System.Diagnostics.Debug.WriteLine(drSpareTimeClass.Length);
            }
           
        }
    
        private void UpdataTeacher(DataRow dr){
            DataRow[] dr1=dtTeachers.Select("Teacher='"+dr["Teacher"].ToString()+"'");
               int a=(int)dr1[0]["Accept_ClassNumber"];
               dr1[0]["Accept_ClassNumber"]=a+1;
        }
        private DataRow Teacher(DataRow[] drClasses,DataRow[] drTeacher)
        {
            DataRow dr = null;
            if (drTeacher.Length == 0||drClasses.Length==0)
            {
                return dr;
            }
            else
            {
                
                    for (int j = 0; j < drTeacher.Length;j++ )
                    {
                        if (drTeacher[j]["Teacher"].Equals(drClasses[0]["Teacher"]))
                        {
                            dr = drClasses[0];
                            break;
                        }
                       

                    }
               
            }
            return dr;
        }
        #endregion
        #region 重新安排 
        public void RePlacement(PlacementConfig config){
            string deletecommand = "delete * from Placement_Data";
            helper.Oledbcommand(deletecommand);
            dtTeachers = helper.getDs(strSelect_Teachers_Data, "Teachers_Data").Tables[0];
            for (int i = 0; i < dtTeachers.Rows.Count;i++ )
            {
                dtTeachers.Rows[i][7] = 0;
                dtTeachers.Rows[i][8] = 0;
                dtTeachers.Rows[i][9] = 0;
                dtTeachers.Rows[i][10] = 0;
                
            }
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString());
            dtSpareTime = helper.getDs(strSelect_SpareTime_Data, "SpareTime").Tables[0];
            DataRow[] DrIsAssigned = dtSpareTime.Select("IsAssigned='"+"true"+"'");
            for (int i = 0; i < DrIsAssigned.Length; i++)
            {
                DrIsAssigned[i]["IsAssigned"] = false;
            }
            daSpareTime = helper.adapter(strSelect_SpareTime_Data);
            daSpareTime.Update(dtSpareTime);
            daTeacher = helper.adapter(strSelect_Teachers_Data);
            daTeacher.Update(dtTeachers);
            MakePlacement(config);
          
        }
        #endregion
        #region 自动筛选督导空闲时间
        public void AutoSelectSpareTime(ProgressBar ProgressBar1)
        {  
            List<SupervisorInfo> supervisorname = new List<SupervisorInfo>();//存储督导信息
            List<SupervisorInfo> listsparetime = new List<SupervisorInfo>();//用于存储经过对比课程表后的督导的上课时间
            List<string> ListInsertCommand = new List<string>();
            dtClasses = helper.getDs(strSelect_Class_Data, "Classes_Data").Tables[0];
            dtSupervisor = helper.getDs(strSelect_Teachers_Data, "Teachers_Data").Tables[0];
            dtSupervisor = dtSupervisor.Select("IsSupervisor=True").CopyToDataTable();
            List<DataRow[]> DrClassesArray = new List<DataRow[]>();//一个Datarow数组代表每一周每一天每一节次所有的上课记录
            for (int i = 0; i < dtSupervisor.Rows.Count;i++ )
            {

                SupervisorInfo sp = new SupervisorInfo();
                sp.SupervisorName = dtSupervisor.Rows[i][1].ToString();
                sp.SupervisorId = dtSupervisor.Rows[i][0].ToString();
                supervisorname.Add(sp);
               
            }
            //分别把每一周每一天每一节的记录分开，存储在DrClassArray数组中
            for (int i = 0; i < supervisorname.Count; i++)
            {
                for (int week = 1; week < 20; week++)
                {
                    for (int day = 1; day < 6; day++)
                    {
                        for (int index = 0; index < spareclass.Count; index++)
                        {
                            DataRow[] dr = dtClasses.Select(" Class_Week=" + week + "and Class_Day=" + day + " and Class_Number=" + spareclass[index] + "and Teacher='" + supervisorname[i].SupervisorName + "'");
                            if (dr.Length==0)
                            {
                                SupervisorInfo info = new SupervisorInfo();
                                info.SupervisorName = supervisorname[i].SupervisorName.ToString();
                                info.Isassigned = false;
                                foreach(SupervisorInfo s in supervisorname)
                                {
                               if (supervisorname[i].SupervisorName.Equals(s.SupervisorName))
                                 {
                                     info.SupervisorId = s.SupervisorId;
                                     info.SpareID = info.SupervisorId + week.ToString() + day.ToString() + spareclass[index].ToString();
                                        break;

                                 }
                             
                                }
                                if (info.SupervisorId==""||info.SpareID=="")
                                {

                                    info.SupervisorId = supervisorname[i].SupervisorName;
                                     info.SpareID = info.SupervisorId + week.ToString() + day.ToString() + spareclass[index].ToString();
                                   
                              
                                }
                                info.SpareWeek = week;
                                info.SpareDay = day;
                                info.SpareNumber = spareclass[index];
                                listsparetime.Add(info);
                            }
                            else
                            {
                                int[] array;
                                switch (spareclass[index])
                                {
                                    case 12:
                                        index += 3;
                                        break;
                                    case 13:
                                         array=new int []{12};
                                        DeleteError(array, week, day, listsparetime);
                                        index += 4;
                                        break;
                                    case 23:
                                        array=new int []{12,13};
                                        DeleteError(array, week, day, listsparetime);
                                        index += 3;
                                        break;
                                    case 24:
                                          array=new int []{12,13,23};
                                        DeleteError(array, week, day, listsparetime);
                                        index += 3;
                                        break;
                                    case 34:
                                          array=new int []{13,23,24};
                                        DeleteError(array, week, day, listsparetime);
                                        index += 2;
                                        break;
                                    case 35:
                                          array=new int []{13,24,23,34};
                                        DeleteError(array, week, day, listsparetime);
                                        index +=1;
                                        break;
                                    case 45:
                                          array=new int []{24,34,35};
                                        DeleteError(array, week, day, listsparetime);
                                        break;
                                    case 67:
                                        index += 3;
                                        break;
                                    case 68:
                                         array=new int []{67};
                                        DeleteError(array, week, day, listsparetime);
                                        index += 3;
                                        break;
                                    case 78:
                                        array=new int []{67,68};
                                        DeleteError(array, week, day, listsparetime);
                                        index += 2;
                                        break;
                                    case 79:
                                        array=new int []{67,78,68};
                                        DeleteError(array, week, day, listsparetime);
                                        index +=1;
                                        break;
                                    case 89:
                                         array=new int []{68,79,78};
                                        DeleteError(array, week, day, listsparetime);
                                        break;
                                    case 1011:
                                        index +=2;
                                        break;
                                    case 1112:
                                         array=new int []{1011,1012};
                                        DeleteError(array, week, day, listsparetime);
                                        index +=1;
                                        break;
                                    case 1012:
                                         array=new int []{1011,1112};
                                        DeleteError(array, week, day, listsparetime);
                                        break;
                                        
                                }
                            }
                         
                        }
                    }
                }
            }

           List<SupervisorInfo> newsupervisorlist= listsparetime.Distinct<SupervisorInfo>().ToList();
           
            //编写数据库插入语句
           foreach (SupervisorInfo supervisor in newsupervisorlist)
            {
             string insertcommand = string.Format(@"insert into SpareTime_Data values('{0}','{1}','{2}',{3},{4},{5},{6})", supervisor.SpareID,
                       supervisor.SupervisorId, supervisor.SupervisorName, supervisor.SpareWeek, supervisor.SpareDay, supervisor.SpareNumber, supervisor.Isassigned);
             ListInsertCommand.Add(insertcommand);
            }
               
            
          
            //使用数据库事务条件插入请求
            helper.insertToStockDataByBatch(ListInsertCommand, ProgressBar1);
        }
        #endregion
        #region 去除枚举情况造成的空闲时间错误，如2-3节上课，12节有空，13节有课这些情况
        private void DeleteError(int[] num,int thisweek,int thisday,List<SupervisorInfo> list)
        {
            List<SupervisorInfo> newlist = new List<SupervisorInfo>();
            foreach (SupervisorInfo i in list)
            {
                newlist.Add(i);
            }
            for (int j = 0; j < num.Length;j++ )
            {
                for (int i = 0; i < newlist.Count; i++)
                {
                    if (newlist[i].SpareNumber.Equals(num[j]) && newlist[i].SpareWeek.Equals(thisweek) && newlist[i].SpareDay.Equals(thisday))
                    {
                        list.Remove(newlist[i]);
                    }
                }
            }
            newlist.Clear();
        }
#endregion
      
       
    }
}
