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
            dtClasses = helper.getDs(strSelect_Class_Data, "Classes_Data").Tables[0];
            dtTeachers = helper.getDs(strSelect_Teachers_Data, "Teachers_Data").Tables[0];
            dtTeachers = dtTeachers.Select("IsSupervisor=False").CopyToDataTable();
            dtSupervisor = helper.getDs(strSelect_Teachers_Data, "Teachers_Data").Tables[0];
            dtSupervisor = dtSupervisor.Select("IsSupervisor=True").CopyToDataTable();
            dtSpareTime = helper.getDs(strSelect_SpareTime_Data, "SpaerTime_Data").Tables[0];
            dtPlacement = helper.getDs(strSelect_Placement_Data,"Placement_Data").Tables[0];
            for (Week = config.Cbegin_week; Week < 19;Week++ )
            {   UpdataWeek(dtSupervisor);
                if(CheckWeekPeo(Week)>=config.Cnumpeo_min){
                    SelectSameDay(config,Week);
                }
                else{
                    continue;
                }
            }
            UpdateALL();
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
                if((dtPlacement.Select("Class_Type='理论'").Length==0)){
                      dr= dtClasses.Select(" Class_Week=" + Week + "and Class_Day=" + Day + " and Class_Number="+spareclass[Index]+"and Class_Type ='理论' ");
                      return dr;
                }else if((dtPlacement.Select("Class_Type='实验'").Length==0)){
                    dr = dtClasses.Select(" Class_Week=" + Week + "and Class_Day=" + Day + " and Class_Number=" + spareclass[Index] + "and Class_Type ='实验' ");
                    return dr;
                }
                else if ((dtPlacement.Select("Class_Type='理论'").Length / dtPlacement.Select("Class_Type='实验'").Length) < Proportion)
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
                 DataTable dt = drSpareTimeClass.CopyToDataTable();
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
            frmMain.fm.SetStatusText("自动生成听课安排成功！", 0);
            frmMain.fm.flashListview();

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
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString());
            daTeacher = helper.adapter(strSelect_Teachers_Data);
            daTeacher.Update(dtTeachers);
            MakePlacement(config);
          
        }
        #endregion
    }
}
