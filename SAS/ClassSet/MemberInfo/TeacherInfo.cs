using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAS.ClassSet.MemberInfo
{
    class TeacherInfo
    {
        private string m_TeacherId;//教师编号

        public string TeacherId
        {
            get { return m_TeacherId; }
            set { m_TeacherId = value; }
        }
        private string m_TeacherName;//教师姓名

        public string TeacherName
        {
            get { return m_TeacherName; }
            set { m_TeacherName = value; }
        }
        private string m_Email;//邮箱地址

        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        private string m_phone;//电话

        public string Phone
        {
            get { return m_phone; }
            set { m_phone = value; }
        }
        private string m_Title;//职称

        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }
        private bool m_IsSupervisor;//是否为督导

        public bool IsSupervisor
        {
            get { return m_IsSupervisor; }
            set { m_IsSupervisor = value; }
        }
        private string m_TeachingSection;//教研室

        public string TeachingSection
        {
            get { return m_TeachingSection; }
            set { m_TeachingSection = value; }
        }
        private int m_AcceptClassNumber;//受听课次数

        public int AcceptClassNumber
        {
            get { return m_AcceptClassNumber; }
            set { m_AcceptClassNumber = value; }
        }
        private int m_ClassTotality;//总听课次数

        public int ClassTotality
        {
            get { return m_ClassTotality; }
            set { m_ClassTotality = value; }
        }
        private int m_ClassWeekNumber;//周听课次数

        public int ClassWeekNumber
        {
            get { return m_ClassWeekNumber; }
            set { m_ClassWeekNumber = value; }
        }
        private int m_ClassDayNumber;//日听课次数

        public int ClassDayNumber
        {
            get { return m_ClassDayNumber; }
            set { m_ClassDayNumber = value; }
        }
        public TeacherInfo()
        {
        }
        public TeacherInfo(string teacherid,string teachername,string email,string phone,string title,bool issupervisor,string teachingsection,int acceptclassnumber,int classtotality,int classweeknumber,int classdaynumber)
        {
            this.m_TeacherId = teacherid;
            this.m_TeacherName = teachername;
            this.m_Email = email;
            this.m_phone = phone;
            this.m_Title = title;
            this.m_IsSupervisor = issupervisor;
            this.m_TeachingSection = teachingsection;
            this.m_AcceptClassNumber = acceptclassnumber;
            this.m_ClassTotality = classtotality;
            this.m_ClassWeekNumber = classweeknumber;
            this.ClassDayNumber = classdaynumber;

        }
    }
}
