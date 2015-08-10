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
using SAS.ClassSet.MemberInfo;
using SAS.ClassSet.Common;
namespace SAS.ClassSet.FunctionTools
{
    class WordTools
    {
        private void closefile()//删除word文档线程
        {
            try
            {
                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
                {
                    p.Kill();
                }
            }
            catch (Exception)
            {
            }
        }
        //首席
        public string  Addchiefsupervisordata(WordTableInfo Info)
        {
            Common.Common.load_cheif_supervisor();
            object missingValue = System.Reflection.Missing.Value;
            object myTrue = false;                  //不显示Word窗口
            object fileName = Environment.CurrentDirectory + "\\" + "chief_supervisor.doc";//WORD文档所在路径
            string newfile =  Common.Common.strAddfilesPath + Info.Teacher + Info.Time.Trim() + Info.Supervisor + ".doc";//存储路径名称
            // object fileName1 = Environment.CurrentDirectory + "\\" + "supervisor.doc";
            Microsoft.Office.Interop.Word._Application oWord = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word._Document oDoc;
            oDoc = oWord.Documents.Open(ref fileName, ref missingValue,
               ref myTrue, ref missingValue, ref missingValue, ref missingValue,
               ref missingValue, ref missingValue, ref missingValue,
               ref missingValue, ref missingValue, ref missingValue,
               ref missingValue, ref missingValue, ref missingValue,
               ref missingValue);
            Microsoft.Office.Interop.Word.Table newtable = oDoc.Tables[1];//获取word文档中的表格
            newtable.Cell(1, 2).Range.Text = Info.Teacher;
            newtable.Cell(1, 6).Range.Text = Info.Time.Substring(0, Info.Time.IndexOf(" "));
            newtable.Cell(2, 6).Range.Text = Info.Classroom + Info.Time.Substring(Info.Time.IndexOf(" ") + 1);
            newtable.Cell(4, 2).Range.Text = Info.Class;
            newtable.Cell(5, 2).Range.Text = Info.Subject;
            object bSaveChange = true;
            oDoc.Close(ref bSaveChange, ref missingValue, ref missingValue);
            oDoc = null;
            oWord = null;

            closefile();
            if (!System.IO.File.Exists(Common.Common.strAddfilesPath))
            {
                Directory.CreateDirectory(Common.Common.strAddfilesPath);
            }
          
            System.IO.File.Copy(fileName.ToString(), newfile, true);

            File.Delete(fileName.ToString());
            //sent_email(Supervisor, Time, Subject, newfile);
            //movetofile(newfile);
            return newfile; 
        }
        //一般
        public string  Addsupervisordata(WordTableInfo Info)
        {
            Common.Common.load_supervisor();
            object missingValue = System.Reflection.Missing.Value;
            object myTrue = false;                  //不显示Word窗口
            //object fileName = Environment.CurrentDirectory + "\\" + "chief_supervisor.doc";
            object fileName1 = Environment.CurrentDirectory + "\\" + "supervisor.doc";
            string newfile = Common.Common.strAddfilesPath+ "\\" + Info.Teacher + Info.Time.Trim() + Info.Supervisor + ".doc";
            Microsoft.Office.Interop.Word._Application oWord1 = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word._Document oDoc1;
            oDoc1 = oWord1.Documents.Open(ref fileName1, ref missingValue,
               ref myTrue, ref missingValue, ref missingValue, ref missingValue,
               ref missingValue, ref missingValue, ref missingValue,
               ref missingValue, ref missingValue, ref missingValue,
               ref missingValue, ref missingValue, ref missingValue,
               ref missingValue);
            Microsoft.Office.Interop.Word.Table newtable1 = oDoc1.Tables[1];
            oWord1.Selection.TypeText("广东医学院教师课堂教学质量评价表" + "(" + Info.Teachingtype + ")");
            newtable1.Cell(1, 2).Range.Text = Info.Teacher;
            newtable1.Cell(1, 4).Range.Text = Info.Perfession;
            newtable1.Cell(1, 6).Range.Text = Info.Time.Substring(0, Info.Time.IndexOf(" "));
            newtable1.Cell(2, 2).Range.Text = Info.Class;
            newtable1.Cell(2, 4).Range.Text = Info.Classroom + Info.Time.Substring(Info.Time.IndexOf(" ") + 1);
            newtable1.Cell(3, 2).Range.Text = Info.Subject;
            object bSaveChange = true;
            oDoc1.Close(ref bSaveChange, ref missingValue, ref missingValue);
            oDoc1 = null;
            oWord1 = null;



            closefile();
            if (!System.IO.File.Exists(Common.Common.strAddfilesPath))
            {
                Directory.CreateDirectory(Common.Common.strAddfilesPath);
            }
            System.IO.File.Copy(fileName1.ToString(), newfile, true);

            File.Delete(fileName1.ToString());
            return newfile;
           // sent_email(Supervisor, Time, Subject, newfile);
            // movetofile(newfile);
            //File.Move(newfile, cCommon.strAddfilesPath);
        }
    }
}
