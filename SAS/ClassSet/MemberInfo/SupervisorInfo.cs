using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAS.ClassSet.MemberInfo
{
    class SupervisorInfo
    {
        private string m_SpareID;

        public string SpareID
        {
            get { return m_SpareID; }
            set { m_SpareID = value; }
        }
        private string m_SupervisorId;

        public string SupervisorId
        {
            get { return m_SupervisorId; }
            set { m_SupervisorId = value; }
        }
        private string m_SupervisorName;

        public string SupervisorName
        {
            get { return m_SupervisorName; }
            set { m_SupervisorName = value; }
        }
        private int m_SpareWeek;

        public int SpareWeek
        {
            get { return m_SpareWeek; }
            set { m_SpareWeek = value; }
        }
        private int m_SpareDay;

        public int SpareDay
        {
            get { return m_SpareDay; }
            set { m_SpareDay = value; }
        }
        private int m_SpareNumber;

        public int SpareNumber
        {
            get { return m_SpareNumber; }
            set { m_SpareNumber = value; }
        }
        private bool m_Isassigned;

        public bool Isassigned
        {
            get { return m_Isassigned; }
            set { m_Isassigned = value; }
        }
        public SupervisorInfo()
        {
        }
        public SupervisorInfo(string spareid,string supervisorid,string supervisorname,int week,int day,int number,bool isassinged)
        {
            this.m_SpareID = spareid;
            this.m_SupervisorId = supervisorid;
            this.m_SupervisorName = supervisorname;
            this.m_SpareWeek = week;
            this.m_SpareDay = day;
            this.m_SpareNumber = number;
            this.m_Isassigned = isassinged;
        }
    }
}
