using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Net.Mail;
using System.Threading;
using SAS.Forms;
using System.Windows.Forms;
using SAS.ClassSet.MemberInfo;
namespace SAS.ClassSet.FunctionTools
{
    class Email
    {
        public static int a = 0, b = 0;
        public int Type { get; set; }
        public EmailRecordInfo ERI { get; set; }

        public EmailInfo EI { get; set; }

        public frmLog FL { get; set; }
        public string str { get; set; }
        public ListView list { get; set; }
        public int id { get; set;}
        public int count { get; set; }
        public void Send(Email myemail)
        {
            Thread t1;
            t1 = new Thread(new ParameterizedThreadStart(Send1));
            t1.IsBackground = true;
            t1.Start(myemail);
        }
        public void Send1(object obj)
        {
            Email E1=obj as Email;
           string updatecommand = "";
            EmailRecordInfo ERecord = null;
            frmLog log1 = null;
            EmailInfo myemail = E1.EI;
            if (E1.Type==0||E1.Type==2)
            {
                ERecord = E1.ERI;
            }
            else if (E1.Type==1)
            {
                log1 = E1.FL;
                updatecommand = E1.str;
            }
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
                        MessageBox.Show("暂时不支持你的邮箱类型");
                        break;
                }
                sc.UseDefaultCredentials = false;
                sc.Credentials = new System.Net.NetworkCredential(myemail.User, myemail.PassWord);
                // sc.Port = 75;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network; //通过网络发送到stmp邮件服务器
                sc.EnableSsl = true; //SMTP 服务器要求安全连接需要设置此属性
                sc.Send(mm);
                mm.Dispose();
                if (ERecord != null)
                {
                    ERecord.File_State = "发送成功";
                    Email.a++;
                    if (E1.Type==2)
                    {
                        MessageBox.Show("发送成功");
                    }
                }
                if (log1 != null)
                {
                    updatecommand = updatecommand.Replace("{flag}", "发送成功");
                    MessageBox.Show("发送成功");
                    //log1.listView1.SelectedItems[0].BackColor = Color.Green;
                }
                
            }
            catch (Exception ex)
            {
                if (ERecord != null)
                {
                    ERecord.File_State = "发送失败";
                    if (E1.Type == 2)
                    {
                        MessageBox.Show("发送失败"+ex.ToString());
                    }
                }
                if (log1 != null)
                {
                    updatecommand = updatecommand.Replace("{flag}", "发送失败");
                    MessageBox.Show("发送失败" + ex.ToString());
                    //log1.listView1.SelectedItems[0].BackColor = Color.Red;

                }
                //MessageBox.Show("邮件发送失败\r\n" + ex);
                //MessageBox.Show("发送失败");//记录错误日志

            }
            finally
            {
                if (ERecord != null)
                {
                    new SqlHelper().Insert(ERecord, "Logs_Data");
                }
                if (log1 != null)
                {
                    if (new SqlHelper().Oledbcommand(updatecommand) > 0)
                    {
                        log1.frmLog_Load_1(null, null);
                    }
                }
                Email.b++;
                if(E1.count==E1.id&&E1.Type==0)
                {
                    if (Email.a==Email.b)
                    {
                        E1.list.SelectedItems[0].BackColor = Color.Green;
                    }
                    else if (Email.a==0)
                    {
                        E1.list.SelectedItems[0].BackColor = Color.Red;
                    }
                    else
                    {
                        E1.list.SelectedItems[0].BackColor = Color.Orange;
                    }
                    Email.a = 0;
                    Email.b = 0;
                }
            }
        }
    }
}
