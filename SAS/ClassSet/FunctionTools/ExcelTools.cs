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
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.DataTable;
using SAS.ClassSet.MemberInfo;
namespace SAS.ClassSet.FunctionTools
{
    class ExcelTools
    {    OleDbConnection con;
         OleDbDataAdapter Excel_da;
         OleDbDataAdapter daClass;
         System.Data.DataTable dtClass;
         System.Data.DataTable Excel_dt;
         SqlHelper helper=new SqlHelper();
         string strSelect_Class_Data = "select * from Classes_Data";
         string strSelect_Teachers_Data = "select * from Teachers_Data";
         System.Data.DataTable dt = new System.Data.DataTable();
         int rowIndex = 2;
         int columnIndex = 0;
         List<string>  ListSupervisor=new List<string>();
         List<string> dcs = new List<string> { "序号", "课程", "授课内容", "授课方式", "专业", "教室", "教师", "周次", "听课时间", "听课人员安排", "分数", "申报" };
         #region Excel链接
         private  void ExcelConnection(string ExcelFiles)
        {
             con= new OleDbConnection("provider=Microsoft.Jet.OleDb.4.0;data source=" +
                                                  ExcelFiles + ";" + "extended properties=excel 8.0;");
             Excel_da = new OleDbDataAdapter("select * from [sheet1$]", con);
             Excel_dt = new System.Data.DataTable();
             OleDbCommandBuilder cb = new OleDbCommandBuilder(Excel_da);
             Excel_da.Fill(Excel_dt);
        }
         #endregion
         #region 导入Excel
         public int ReadExcel(string ExcelPath)
         {
             try
             {
                 int[] name = new int[7];//确定Excel的所需字段值所在的列---
                 daClass = helper.adapter(strSelect_Class_Data);
                 dtClass = new System.Data.DataTable();
                 daClass.Fill(dtClass);
                 daClass.FillSchema(dtClass, SchemaType.Source);
                 ExcelConnection(ExcelPath);
                 //MessageBox.Show(Excel_dt.Columns[0].ToString());
              
                 string classname = Excel_dt.Rows[1][0].ToString();//课程名称
                 string spcialty = Excel_dt.Rows[1][4].ToString().Substring(3);//专业
                 string banji = Excel_dt.Rows[2][4].ToString();
                 for (int q = 0; q < Excel_dt.Columns.Count; q++)
                 {
                     switch (Excel_dt.Rows[4][q].ToString())
                     {
                         case "周次": name[0] = q; break;
                         case "星期": name[1] = q; break;
                         case "节次": name[2] = q; break;
                         case "上课地点": name[3] = q; break;
                         case "授课教师": name[4] = q; break;
                         case "授课内容": name[5] = q; break;
                         case "授课方式": name[6] = q; break;
                     }
                 }

                 dt = dtClass.Copy();   //  获取Class_Data的架构
                 dt.Clear();
                 for (int i = 5; i < Excel_dt.Rows.Count; i++)
                 {
                     // dr = dt.Rows[i];//获取Excel的当前操作行的数据
                     if (!(Excel_dt.Rows[i][name[0]].ToString() == "周次" || Excel_dt.Rows[i][name[0]].ToString() == ""))
                     {
                         string teachername = Excel_dt.Rows[i][name[4]].ToString();//获取授课老师列的数据
                         int k = Teacher(teachername) + 1;//判断有多少位老师上同一节课
                         for (int m = 1; m <= k; m++)//有几位老师，就循环几次
                         {
                             string teachernamepick;//定义截取的老师名字
                             //以逗号为分界点，把多位老师的名字分成各自的名字
                             if ((k == 1) || (m == k)) teachernamepick = teachername;
                             else
                             {
                                 int index2 = teachername.IndexOf(",");
                                 teachernamepick = teachername.Substring(0, index2);
                                 teachername = teachername.Remove(0, index2 + 1);
                             }


                             int j;
                             //判断星期几，返回对应的数字
                             switch (Excel_dt.Rows[i][name[1]].ToString().Substring(0, 1))
                             {
                                 case "一": j = 1; break;
                                 case "二": j = 2; break;
                                 case "三": j = 3; break;
                                 case "四": j = 4; break;
                                 case "五": j = 5; break;
                                 default: j = 0; break;
                             }
                             if (teachernamepick != null && teachernamepick != "")
                             {
                                 DataRow drClass_information = dt.NewRow();
                                 drClass_information["Class_Day"] = j;
                                 //获取节次          
                                 string strclassname = Excel_dt.Rows[i][name[2]].ToString();
                                 int classnumindex = strclassname.IndexOf("-");
                                 drClass_information["Teacher"] = teachernamepick;
                                 drClass_information["Class_ID"] = teachernamepick + Excel_dt.Rows[i][name[0]].ToString() + j.ToString() + strclassname.Substring(0, classnumindex) + strclassname.Substring(classnumindex + 1) + classname.Substring(5) + Excel_dt.Rows[i][name[3]] + banji;
                                 drClass_information["Teacher_ID"] = "0000000000";
                                 drClass_information["Class_Week"] = Excel_dt.Rows[i][name[0]];
                                 drClass_information["Class_Number"] = Convert.ToInt32(strclassname.Substring(0, classnumindex) + strclassname.Substring(classnumindex + 1));
                                 drClass_information["Class_Address"] = Excel_dt.Rows[i][name[3]];
                                 drClass_information["Class_Name"] = classname.Substring(5);
                                 drClass_information["Class_Content"] = Excel_dt.Rows[i][name[5]];
                                 drClass_information["Class_Type"] = Excel_dt.Rows[i][name[6]];
                                 drClass_information["Spcialty"] = spcialty;

                                 dt.Rows.Add(drClass_information);
                             }
                             else
                             {
                                 return 0;
                             }
                           
                         }
                     }
                   


                 }
                 dtClass.Merge(dt, true);

                 daClass.Update(dtClass);
                 return 1;
             }catch(Exception){
                 return 0;
             }
        }
         
         private string classnumber(string teachername)
        {
            System.Data.DataTable dtTeachers = helper.getDs(strSelect_Teachers_Data, "Teachers_Data").Tables[0];
            DataRow[] teacher = dtTeachers.Select("Teacher='" + teachername + "'");
            if (teacher.Length != 0)
            {
                string teachernumber = teacher[0][0].ToString();
                return teachernumber;
            }
            else
            {
                string classnumber = "0000000000";

                return classnumber;
            }
        }
        private int Teacher(string strTeacher)
        {
            return strTeacher.Length - strTeacher.Replace(",", "").Length;
        }
         #endregion
         #region 导出Excel
        public void ExportToExecl(string sqlcommand, string tablename, ListView listview)
        {
            System.Windows.Forms.SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xls";
            sfd.Filter = "Excel文件(*.xls)|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DoExport(sfd.FileName,sqlcommand,tablename,listview);
            }
        }
        private void DoExport(string strFileName, string sqlcommand, string tablename, ListView listview)
        {
            // try
            //{
            System.Data.DataTable dt = helper.getDs(sqlcommand, tablename).Tables[0];
            int rowNum = dt.Rows.Count;
            int columnNum = dcs.Count;

            if (rowNum == 0 || string.IsNullOrEmpty(strFileName))
            {
                return;
            }
            if (rowNum > 0)
            {

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建excel对象，可能您的系统没有安装excel");
                    return;
                }
                xlApp.DefaultFilePath = "";
                xlApp.DisplayAlerts = true;
                xlApp.SheetsInNewWorkbook = 1;
                Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Sheets[1];


                //将ListView的列名导入Excel表第一行
                foreach (string dc in dcs)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = dc;
                    Range rg = xlApp.Cells[rowIndex, columnIndex];
                    rg.Font.Bold = true;
                    rg.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                }

                Range rg3 = sheet.get_Range("A1", "K1");
                rg3.MergeCells = true;
                rg3.set_Value(Type.Missing, "信息工程学院2014－2015学年第一学期教学检查听课安排");
                rg3.Font.Bold = true;
                rg3.Font.Size = 18;
                rg3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //将ListView中的数据导入Excel中
                for (int i = 0; i < rowNum; i++)
                {
                    rowIndex++;
                    columnIndex = 0;
                    DistinctSupervisor(dt.Rows[i][6].ToString(), ListSupervisor);//去职称
                    for (int j = 0; j < dcs.Count; j++)
                    {
                        columnIndex++;
                        if (j != dcs.Count-1)
                        {

                            //注意这个在导出的时候加了“\t” 的目的就是避免导出的数据显示为科学计数法。可以放在每行的首尾。
                            switch(j){
                                case 0:
                                    xlApp.Cells[rowIndex, columnIndex] = (i + 1).ToString() + "\t"; 
                                    break;
                                case 1:
                                    xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][8].ToString() + "\t";
                                    break;
                                case 2:
                                    xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][9].ToString() + "\t";
                                    break;
                                case 3:
                                    xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][10].ToString() + "\t";
                                    break;
                                case 4:
                                    xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][11].ToString() + "\t";
                                    break;
                                case 5:
                                    xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][7].ToString() + "\t";
                                    break;
                                case 6:
                                    xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][2].ToString() + "\t";
                                    break;
                                case 7:
                                    xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][3].ToString() + "\t";
                                    break;
                                case 8:
                                    xlApp.Cells[rowIndex, columnIndex] = //听课时间
                                    CalendarTools.getdata(Common.Common.Year, Convert.ToInt32(dt.Rows[i][3]), Convert.ToInt32(dt.Rows[i][4]) - CalendarTools.weekdays(CalendarTools.CaculateWeekDay(Common.Common.Year, Common.Common.Month, Common.Common.Day)), Common.Common.Month, Common.Common.Day).ToLongDateString()
                                    + " " + addseparator(Convert.ToInt32(dt.Rows[i][5])) + "节" + "\t";                               
                                    break;
                                case 9:
                                    xlApp.Cells[rowIndex, columnIndex] =  FormatSupervisor(ListSupervisor)+ "\t";
                                    break;
                                case 10:
                                    xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][12].ToString() + "\t";
                                    break;
                            }
                          
                            Range rg2 = xlApp.Cells[rowIndex, columnIndex];
                            rg2.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        }
                        else
                        {
                            xlApp.Cells[rowIndex, columnIndex] = " ";
                            Range rg2 = xlApp.Cells[rowIndex, columnIndex];
                            rg2.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        }
                    }
                }
                Range range = sheet.Columns;
                range.Columns.AutoFit();
                //Range chd = sheet.Columns[0];
                //chd.Font.Bold = true;

                //var RowAll = sheet.get_Range("A1", "K"+columnIndex.ToString());

                //RowAll.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //例外需要说明的是用strFileName,Excel.XlFileFormat.xlExcel9795保存方式时 当你的Excel版本不是95、97 而是2003、2007 时导出的时候会报一个错误：异常来自 HRESULT:0x800A03EC。 解决办法就是换成strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal。
                xlBook.SaveAs(strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlApp = null;
                xlBook = null;
                MessageBox.Show("OK");
                closefile();
                //  }
                // }
                // catch (Exception)
                // {
                // }
            }
        }
        private void closefile()//删除word文档线程
        {
            try
            {
                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                {
                    p.Kill();
                }
            }
            catch (Exception)
            {
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
         //去掉职称
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
        private string FormatSupervisor( List<string> List)
        {
            string supervisor = "";
          
            foreach(string s in List){
                if (s.IndexOf("(") != -1)
                {
                    supervisor = supervisor +","+ s.Substring(0, s.IndexOf("("));
                }
                else
                {
                    supervisor = supervisor+"," + s;
                }
            }
            List.Clear();
            return supervisor.Substring(1);

        }
        private string FormatTeacher(string s)
        {
            string supervisor = "";

            
                if (s.IndexOf("(") != -1)
                {
                    supervisor = supervisor + "," + s.Substring(0, s.IndexOf("("));
                }
                else
                {
                    supervisor = supervisor + "," + s;
                }
           
            return supervisor.Substring(1);

        }

    }
        #endregion
    }


