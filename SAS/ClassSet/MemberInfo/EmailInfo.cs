using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAS.ClassSet.MemberInfo
{
    class EmailInfo
    {
        private string m_User;

        public string User
        {
            get { return m_User; }
            set { m_User = value; }
        }
        private string m_PassWord;

        public string PassWord
        {
            get { return m_PassWord; }
            set { m_PassWord = value; }
        }
        private string m_AddFiles;

        public string AddFiles
        {
            get { return m_AddFiles; }
            set { m_AddFiles = value; }
        }
        private string m_Content;

        public string Content
        {
            get { return m_Content; }
            set { m_Content = value; }
        }
        private string m_Receiver;

        public string Receiver
        {
            get { return m_Receiver; }
            set { m_Receiver = value; }
        }
        private string m_Title;

        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }
        public EmailInfo()
        {
        }
        public EmailInfo(string user,string password,string addfile,string content,string receiver,string title)
        {
            this.m_User = user;
            this.m_PassWord = password;
            this.m_AddFiles = addfile;
            this.m_Content = content;
            this.m_Receiver = receiver;
            this.m_Title = title;

        }
    }
}
