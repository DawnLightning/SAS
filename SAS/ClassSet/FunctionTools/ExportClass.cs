using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using SAS.ClassSet.MemberInfo;
using System.Windows.Forms;
namespace SAS.ClassSet.FunctionTools
{   
    class ExportClass
    {   
        /// <summary>
        ///  使用说明
        ///  这里导出的数据默认为全部教师的所有上课情况，可以根据select语句筛选返回不同的DataTable
        ///  这样就可以实现：
        ///  1.导出一个教师的这个学期的上课课表
        ///  2.导出一门课程的所有上课情况
        ///  3.等等
        ///  ！所有节次在数据库中的存储都是数字，例如1-2节，那么就是12
        /// </summary>
        private DataTable dtclass;//进度表
        private List<ExportClassInfo> Info = new List<ExportClassInfo>();//对应教师的上课信息
        private SqlHelper help = new SqlHelper();
        /// <summary>
        /// 从数据库中选择要导出的教学进度
        /// </summary>
        public bool InitData(string condition)
        {    string selectcommand="";
            if (condition!="")
            {
                selectcommand = "select Teacher,Class_Type,Class_Week,Class_Day,Class_Number,Class_Name from Classes_Data" + " where " + condition;
            }
            else
            {
                selectcommand = "select Teacher,Class_Type,Class_Week,Class_Day,Class_Number,Class_Name from Classes_Data";
            }
         
            dtclass = help.getDs(selectcommand, "Classes_Data").Tables[0];
            if (dtclass.Rows.Count==0)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
        /// <summary>
        /// //将数据库中的记录导入到对象数组中
        /// </summary>
        public void InitInfo()
        {   
            for (int i = 0; i < dtclass.Rows.Count;i++ )
            {
                ExportClassInfo info = new ExportClassInfo();
                info.Teachername = ClearTechnicalTitle(dtclass.Rows[i][0].ToString());
                info.Classtype = dtclass.Rows[i][1].ToString();
                info.Week = Convert.ToInt32(dtclass.Rows[i][2]);
                info.Day = Convert.ToInt32(dtclass.Rows[i][3]);
                info.Classname = dtclass.Rows[i][5].ToString();
                if ( Convert.ToInt32(dtclass.Rows[i][4])<100&& Convert.ToInt32(dtclass.Rows[i][4])>0)
                {
                    info.Start = ( Convert.ToInt32(dtclass.Rows[i][4])) / 10;//例如89节，那/10就是8，%10就是9
                    info.End = ( Convert.ToInt32(dtclass.Rows[i][4]))%10;
                    if (info.End-info.Start>1)
                    {
                        info.IsOverTop = true;
                    } 
                    else
                    {
                        info.IsOverTop = false;
                    }
                } 
                else
                {
                    info.Start = (Convert.ToInt32(dtclass.Rows[i][4])) / 100;
                    info.End = (Convert.ToInt32(dtclass.Rows[i][4])) % 100;
                    if (info.End - info.Start > 1)
                    {
                        info.IsOverTop = true;
                    }
                    else
                    {
                        info.IsOverTop = false;
                    }
                }
                Info.Add(info);
            }
        }
        /// <summary>
        /// 输出word文档
        /// </summary>
        public void MakeWordDoc(string selectcommand,string filename)
        {
            if (InitData(selectcommand)) //从数据库中选择要导出的教学进度
           {
               InitInfo();//将数据库中的记录导入到对象数组中
               WordTools tools = new WordTools();
               tools.fullclasses(Info, filename);//将对象数组写进word文档
           }else{
               MessageBox.Show("没有找到指定数据");
           }
         
           
        }
        /// <summary>
        /// 去掉职称
        /// </summary>
        /// <param name="s">教师姓名</param>
        /// <returns></returns>
        private string ClearTechnicalTitle(string s)
        {
            if (s.IndexOf("(") != -1)
            {
                 return s.Substring(0, s.IndexOf("("));
            }
            else
            {
                return s;
            }
        }
    }
}
