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
        private int recnuum =0;
        private int sentnum = 1;
        private int successnum = 0;
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
            Info.Title = DateTime.Now.ToLongTimeString() + "听课反馈";
          
            Info.Receiver = help.getDs("select * from Teachers_Data where Teacher like '%" + Listview.CheckedItems[0].SubItems[6].Text + "%'", "Teachers_Data").Tables[0].Rows[0][2].ToString();
            return Info;
        }
        public void SentResult(){
            EmailInfo EInfo = InitializeEmailInfo();
           
            string flag="";
            EmailRecordInfo record = new EmailRecordInfo(Listview.CheckedItems[0].SubItems[6].Text, "受听课教师", EInfo.Title, Listview.CheckedItems[0].SubItems[6].Text + DateTime.Now.ToShortTimeString(), "听课反馈结果", flag, FilePath);

            AsynEmail EmailSendPoccess = new AsynEmail(EInfo, record, this.EmailResultCallBack);
            EmailSendPoccess.ThreadSend();
            Main.fm.SetStatusText("正在发送邮件", 0);
            //help.Insert(record,"Logs_Data");
            //MessageBox.Show(flag);
        }
        private void EmailResultCallBack(EmailRecordInfo info, string message)
        {
            recnuum++;
            Main.fm.SetStatusText(string.Format("已发送{0}封", recnuum), 0);
            SqlHelper help = new SqlHelper();
            help.Insert(info, "Logs_Data");
            if (message == "发送成功")
            {
                successnum++;
            }
            if (recnuum == sentnum)
            {

                MessageBox.Show(string.Format("共发送{0}邮件,成功{1}封，失败{2}封，请查看记录", sentnum, successnum, sentnum - successnum));
                Main.fm.SetStatusText("发送完成", 0);
            }


        }
    }
}
