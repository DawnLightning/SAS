using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAS.ClassSet.MemberInfo
{
    class PlacementInfo
    {
        public PlacementInfo()
        {
        }
        public PlacementInfo(string classid,string teacherid,string teachername,int classweek,int classday,int classnumber,string supervisornsname,string classadress,
            string classcontent,string classname,string classtype,string spcialty,int grade )
        {
            this.m_ClassId=classid;
            this.m_TeacherId=teacherid;
            this.m_TeacherName=teachername;
            this.m_ClassWeek=classweek;
            this.m_ClassDay=classday;
            this.m_ClassNumber=classnumber;
            this.m_SupervisorsName=supervisornsname;
            this.m_ClassAddress=classadress ;
            this.m_ClassContent=classcontent ;
            this.m_ClassName=classname ;
            this.m_ClassType=classtype;
            this.m_Spcialty=spcialty ;
            this.m_Grade=grade;
        }
        private string m_ClassId;

        public string ClassId
        {
            get { return m_ClassId; }
            set { m_ClassId = value; }
        }
        private string m_TeacherId;

        public string TeacherId
        {
            get { return m_TeacherId; }
            set { m_TeacherId = value; }
        }
        private string m_TeacherName;

        public string TeacherName
        {
            get { return m_TeacherName; }
            set { m_TeacherName = value; }
        }
        private int m_ClassWeek;

        public int ClassWeek
        {
            get { return m_ClassWeek; }
            set { m_ClassWeek = value; }
        }
        private int m_ClassDay;

        public int ClassDay
        {
            get { return m_ClassDay; }
            set { m_ClassDay = value; }
        }
        private int m_ClassNumber;

        public int ClassNumber
        {
            get { return m_ClassNumber; }
            set { m_ClassNumber = value; }
        }
        private string m_SupervisorsName;

        public string SupervisorsName
        {
            get { return m_SupervisorsName; }
            set { m_SupervisorsName = value; }
        }
        private string m_ClassAddress;

        public string ClassAddress
        {
            get { return m_ClassAddress; }
            set { m_ClassAddress = value; }
        }
        private string m_ClassContent;

        public string ClassContent
        {
            get { return m_ClassContent; }
            set { m_ClassContent = value; }
        }
        private string m_ClassName;

        public string ClassName
        {
            get { return m_ClassName; }
            set { m_ClassName = value; }
        }
        private string m_ClassType;

        public string ClassType
        {
            get { return m_ClassType; }
            set { m_ClassType = value; }
        }
        private string m_Spcialty;

        public string Spcialty
        {
            get { return m_Spcialty; }
            set { m_Spcialty = value; }
        }
        private int m_Grade;

        public int Grade
        {
            get { return m_Grade; }
            set { m_Grade = value; }
        }




    }
}
