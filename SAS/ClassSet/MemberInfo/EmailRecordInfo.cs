using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAS.ClassSet.MemberInfo
{
    class EmailRecordInfo
    {
        private string m_Email_Receiver;

        public string Email_Receiver
        {
            get { return m_Email_Receiver; }
            set { m_Email_Receiver = value; }
        }
        private string m_Teacher_Identity;

        public string Teacher_Identity
        {
            get { return m_Teacher_Identity; }
            set { m_Teacher_Identity = value; }
        }
        private string m_Email_Theme;

        public string Email_Theme
        {
            get { return m_Email_Theme; }
            set { m_Email_Theme = value; }
        }
        private string m_Time_Now;

        public string Time_Now
        {
            get { return m_Time_Now; }
            set { m_Time_Now = value; }
        }
        private string m_Email_Type;

        public string Email_Type
        {
            get { return m_Email_Type; }
            set { m_Email_Type = value; }
        }
        private string m_File_State;

        public string File_State
        {
            get { return m_File_State; }
            set { m_File_State = value; }
        }
        private string m_Enclosure_Path;

        public string Enclosure_Path
        {
            get { return m_Enclosure_Path; }
            set { m_Enclosure_Path = value; }
        }
        public EmailRecordInfo()
        {
        }
        //
        public EmailRecordInfo(string Email_Receiver, string Teacher_Identity, string Email_Theme, string Time_Now, string Email_Type, string File_State, string Enclosure_Path)
        {
            this.m_Email_Receiver = Email_Receiver;
            this.m_Teacher_Identity = Teacher_Identity;
            this.m_Email_Theme = Email_Theme;
            this.m_Time_Now = Time_Now;
            this.m_Email_Type = Email_Type;
            this.m_File_State = File_State; ;
            this.m_Enclosure_Path = Enclosure_Path;

        }
    }
}
