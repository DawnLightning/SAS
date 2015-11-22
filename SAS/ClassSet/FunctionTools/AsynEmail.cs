using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAS.ClassSet.MemberInfo;
using System.Net.Mail;
using System.Threading;
namespace SAS.ClassSet.FunctionTools
{  
    class AsynEmail
    {   

        public delegate void ResultCallBack(EmailRecordInfo info, string message);//声明一个委托
        private ResultCallBack resultcallback;//实例化回调方法
        private EmailInfo Info;//邮件信息对象
        private EmailRecordInfo ReInfo;//邮件记录对象
        private static string  SEND_SUCCESS ="发送成功";
        private static string SEND_FAILED = "发送失败";
        private static string NOT_SUPPOET_EMAIL_TYPE = "不支持当前邮箱类型";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="reinfo">邮件记录</param>
        /// <param name="resultcallback">回调函数</param>
        public AsynEmail(EmailInfo info,EmailRecordInfo reinfo,ResultCallBack resultcallback)
        {
            this.Info = info;
            this.resultcallback = resultcallback;
            this.ReInfo = reinfo;
        }
        /// <summary>
        /// 发送邮件的线程方法
        /// </summary>
        public void ThreadSend()
        {
            Thread EamilSendThread = new Thread(send);
            EamilSendThread.IsBackground = true;
            EamilSendThread.Start();
        }
        /// <summary>
        /// 发送方法
        /// </summary>
        public void send()
        {
            EmailInfo myemail = this.Info;
           
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(myemail.User, myemail.User); //发送者
                mm.To.Add(myemail.Receiver); //接收者
                mm.Subject = myemail.Title; //标题
                mm.BodyEncoding = Encoding.UTF8; //内容编码
                mm.Priority = MailPriority.High; //发送优先级
                mm.Body = myemail.Content; //内容
                mm.Attachments.Add(new Attachment(myemail.AddFiles));
                mm.IsBodyHtml = true; //html形式发送
                SmtpClient sc = new SmtpClient();
                string a = myemail.User.Substring(myemail.User.LastIndexOf("@") + 1);
                string b = a.Substring(0, a.LastIndexOf("."));
                switch (b)
                {
                    case "qq":
                        sc.Host = "smtp.qq.com";
                        break;
                    case "163":
                        sc.Host = "smtp.163.com";
                        break;
                    case "126":
                        sc.Host = "smtp.126.com";
                        break;
                    case "sina":
                        sc.Host = "smtp.sina.com.cn";
                        break;
                    case "gmail":
                        sc.Host = "smtp.gmail.com";
                        break;
                    default:
                        resultcallback(ReInfo, NOT_SUPPOET_EMAIL_TYPE);
                        break;
                }
                sc.UseDefaultCredentials = false;
                sc.Credentials = new System.Net.NetworkCredential(myemail.User, myemail.PassWord);
                sc.DeliveryMethod = SmtpDeliveryMethod.Network; //通过网络发送到stmp邮件服务器
                sc.EnableSsl = true; //SMTP 服务器要求安全连接需要设置此属性
                sc.Send(mm);
                mm.Dispose();
                ReInfo.File_State=SEND_SUCCESS;
                ReInfo.Time_Now=DateTime.Now.ToLongTimeString();
                resultcallback(ReInfo, SEND_SUCCESS);

            }
            catch (Exception ex)
            {    
                 ReInfo.File_State=SEND_FAILED;
                 ReInfo.Time_Now=DateTime.Now.ToLongTimeString();
                 resultcallback(ReInfo, ex.Message);
               

            }
        }
    }
}
