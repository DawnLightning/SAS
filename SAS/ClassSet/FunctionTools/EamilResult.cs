using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAS.ClassSet.Common;
using SAS.ClassSet.FunctionTools;
using SAS.ClassSet.MemberInfo;
using System.Windows.Forms;
namespace SAS.ClassSet.FunctionTools
{
    class EamilResult
    {
        private ListView Listview;
        private string FilePath;
        SqlHelper help = new SqlHelper();
        public EamilResult(ListView listview,string file)
        {
            this.Listview = listview;
            this.FilePath = file;
        }
        private EmailInfo InitializeEmailInfo(){
            EmailInfo Info = new EmailInfo();
            Info.AddFiles = FilePath;
            Info.User = Common.Common.MailAddress;
            Info.PassWord = Common.Common.MailPassword;
            Info.Content = "";
            Info.Title = "听课反馈" + DateTime.Now.ToShortTimeString();
            Info.Receiver = help.getDs("select * from Teachers_Data where Teacher='" + Listview.CheckedItems[0].SubItems[6].Text + "'","Teachers_Data").Tables[0].Rows[0][2].ToString();
            return Info;
        }
        public void SentResult(){
            EmailInfo EInfo = InitializeEmailInfo();
            Email senter = new Email();
            string flag="";
            EmailRecordInfo record = new EmailRecordInfo(Listview.CheckedItems[0].SubItems[6].Text, "受听课教师", EInfo.Title, Listview.CheckedItems[0].SubItems[6].Text + DateTime.Now.ToShortTimeString(), "听课反馈结果", flag, FilePath);
            senter.Send(new Email { Type=0,EI=EInfo,ERI=record});
            
            //help.Insert(record,"Logs_Data");
            //MessageBox.Show(flag);
        }
    }
}
