using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAS.ClassSet.MemberInfo
{
    class WordTableInfo
    {
        private string m_Supervisor;

        public string Supervisor
        {
            get { return m_Supervisor; }
            set { m_Supervisor = value; }
        }
        private string m_Time;

        public string Time
        {
            get { return m_Time; }
            set { m_Time = value; }
        }
        private string m_Classroom;

        public string Classroom
        {
            get { return m_Classroom; }
            set { m_Classroom = value; }
        }
        private string m_Perfession;

        public string Perfession
        {
            get { return m_Perfession; }
            set { m_Perfession = value; }
        }
        private string m_Teacher;

        public string Teacher
        {
            get { return m_Teacher; }
            set { m_Teacher = value; }
        }
        private string m_Class;

        public string Class
        {
            get { return m_Class; }
            set { m_Class = value; }
        }
        private string m_Subject;

        public string Subject
        {
            get { return m_Subject; }
            set { m_Subject = value; }
        }
        private string m_teachingtype;

        public string Teachingtype
        {
            get { return m_teachingtype; }
            set { m_teachingtype = value; }
        }
        public WordTableInfo()
        {
        }
        public WordTableInfo(string Supervisor, string Time, string Classroom, string Perfession,
                                                    string Teacher, string Class, string Subject, string Teachingtype)
        {
            this.m_Supervisor = Supervisor;
            this.m_Time = Time;
            this.m_Classroom = Classroom;
            this.m_Perfession = Perfession;
            this.m_Teacher = Teacher;
            this.m_Class = Class;
            this.m_Subject = Subject;
            this.m_teachingtype = Teachingtype;
        }
    }
}
