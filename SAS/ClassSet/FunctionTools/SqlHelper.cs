using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlClient;
using SAS.ClassSet.MemberInfo;
namespace SAS.ClassSet.FunctionTools
{
    class SqlHelper
    {
        OleDbConnection conn=new OleDbConnection(connString);  
        ClassInfo classes;
        SupervisorInfo supervisor;
        TeacherInfo teacher;
        EmailRecordInfo record;
        PlacementInfo placement;
        DataSet tableset;
        OleDbDataAdapter oledbda;
       
        static string strDatabasePath
        {
            get
            {
                return Common.Common.strDatabasePath;
            }
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string connString
        {
            get { return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDatabasePath; }
        }
       
        #region  建立数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
        public  OleDbConnection getcon()
        {
            conn = new OleDbConnection(connString);   //用SqlConnection对象与指定的数据库相连接
            this.conn.Open();  //打开数据库连接
            return conn;  //返回SqlConnection对象的信息
        }
        #endregion
        #region  关闭数据库连接
        /// <summary>
        /// 关闭于数据库的连接.
        /// </summary>
        public void con_close()
        {
            if (conn.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                conn.Close();   //关闭数据库的连接
                conn.Dispose();   //释放My_con变量的所有空间
            }
        }
        #endregion
        #region  增加记录
        public int Insert(Object ObjMember,string Flag)
        {
            int SuccessFlag;
            switch (Flag)
            {
                case "Classes_Data":
                    classes = (ClassInfo)ObjMember;
                    string dtClass = string.Format(@"insert into " + Flag +  " values('{0}','{1}','{2}' ,{3},{4},{5},'{6}','{7}','{8}','{9}','{10}')"
                    ,classes.ClassId,classes.TeacherId,classes.TeacherName,classes.ClassWeek,classes.ClassDay,classes.ClassNumber,classes.ClassName,
                    classes.ClassContent,classes.ClassType,classes.Spcialty);
                    SuccessFlag=Oledbcommand(dtClass);
                    return SuccessFlag;
                   
                case "SpareTime_Data":
                    supervisor = (SupervisorInfo)ObjMember;
                    string dtSpareTime = string.Format(@"insert into " + Flag + " values('{0}','{1}','{2}',{3},{4},{5},{6})",supervisor.SpareID,
                        supervisor.SupervisorId,supervisor.SupervisorName,supervisor.SpareWeek,supervisor.SpareDay,supervisor.SpareNumber,supervisor.Isassigned);
              
                    SuccessFlag = Oledbcommand(dtSpareTime);
                    return SuccessFlag;
                case "Teachers_Data":
                    teacher = (TeacherInfo)ObjMember;
                    string dtTeacher = string.Format(@"insert into " + Flag + " values('{0}','{1}','{2}' ,'{3}','{4}',{5},'{6}',{7},{8},{9},{10})"
                        ,teacher.TeacherId.ToString(),teacher.TeacherName.ToString(),teacher.Email.ToString(),teacher.Phone.ToString(),teacher.Title.ToString(),teacher.IsSupervisor,teacher.TeachingSection.ToString(),
                        teacher.AcceptClassNumber,teacher.ClassTotality,teacher.ClassWeekNumber,teacher.ClassDayNumber
                        );
               
                    SuccessFlag = Oledbcommand(dtTeacher);
                    return SuccessFlag;
                case "Placement_Data":
                    placement = (PlacementInfo)ObjMember;
                    string dtPlacement = string.Format(@"insert into " + Flag + " values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12})"
                   ,placement.ClassId,placement.TeacherId,placement.TeacherName,placement.ClassWeek,placement.ClassDay,placement.ClassNumber,placement.SupervisorsName,
                   placement.ClassAddress,placement.ClassContent,placement.ClassName,placement.ClassType,placement.Spcialty,placement.Grade
                  );
                    SuccessFlag = Oledbcommand(dtPlacement);
                    return SuccessFlag;
                case "Logs_Data":
                    record = (EmailRecordInfo)ObjMember;
                    string dtLog = string.Format(@"insert into "+Flag+" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",record.Email_Receiver,record.Teacher_Identity,record.Email_Theme
                        ,record.Time_Now,record.Email_Type,record.File_State,record.Enclosure_Path);
                    SuccessFlag = Oledbcommand(dtLog);
                    return SuccessFlag;

                    
            }
            return 0;
        }
      
        #endregion
        #region 删除记录
        public int delete(string tablename,string condition)
        {
            string deletecommand = "delete" +"*"+ "from"+" "+ tablename + condition;
           return Oledbcommand(deletecommand);
        }
        #endregion
        #region 更新记录
        public int update(string tablename,Object ObjMember)
        {
            int SuccessFlag;
            switch (tablename)
            {
                case "Logs_Data":
                    record = (EmailRecordInfo)ObjMember;
                    string dtLog = string.Format(@"UPDATE " + tablename + " SET  Email_Receiver='{0}', Teacher_Identity='{1}',Email_Theme='{2}',Time_Now='{3}',Email_Type='{4}', File_State'{5}',Enclosure_Path='{6}' WHERE Time_Now='{7}'"
                   ,record.Email_Receiver,record.Teacher_Identity,record.Email_Receiver,record.Time_Now,record.Email_Type,record.File_State,record.Enclosure_Path,record.Time_Now);
                    SuccessFlag = Oledbcommand(dtLog);
                    return SuccessFlag;

                case "SpareTime_Data":
                    supervisor = (SupervisorInfo)ObjMember;
                    string dtSpareTime = string.Format(@"insert into " + tablename + " values('{0}','{1}','{2}' ,{3},{4},{5},{6})", supervisor.SpareID,
                        supervisor.SupervisorId, supervisor.SupervisorName, supervisor.SpareWeek, supervisor.SpareDay, supervisor.SpareNumber, supervisor.Isassigned);

                    SuccessFlag = Oledbcommand(dtSpareTime);
                    return SuccessFlag;
                case "Teachers_Data":
                    teacher = (TeacherInfo)ObjMember;
                    string dtTeacher = string.Format(@"UPDATE "+tablename+" SET Teacher_ID ='{0}', Teacher ='{1}', Email ='{2}', Phone ='{3}', Title ='{4}', IsSupervisor ={5}, Teaching_Section ='{6}', Accept_ClassNumber ={7}, Class_Totality ={8}, Class_WeekNumber ={9}, Class_DayNumber ={10} WHERE  Teacher ='{11}'"
                        , teacher.TeacherId.ToString(), teacher.TeacherName.ToString(), teacher.Email.ToString(), teacher.Phone.ToString(), teacher.Title.ToString(), teacher.IsSupervisor, teacher.TeachingSection.ToString(),
                        teacher.AcceptClassNumber, teacher.ClassTotality, teacher.ClassWeekNumber, teacher.ClassDayNumber,teacher.TeacherName
                        );

                    SuccessFlag = Oledbcommand(dtTeacher);
                    return SuccessFlag;
                case "Placement_Data":
                    placement = (PlacementInfo)ObjMember;
                    string dtPlacement = string.Format(@"UPDATE " + tablename + " SET Class_ID='{0}',Teacher_ID='{1}',Teacher='{2}',Class_week={3},Class_Day={4},Class_Number={5},Supervisor_Name='{6}',Class_Address='{7}',Class_Content='{8}',Class_Name='{9}',Class_Type='{10}',Spcialty='{11}',Grade={12} WHERE  Class_ID='{13}'"
                   , placement.ClassId, placement.TeacherId, placement.TeacherName, placement.ClassWeek, placement.ClassDay, placement.ClassNumber, placement.SupervisorsName,
                   placement.ClassAddress, placement.ClassContent, placement.ClassName, placement.ClassType, placement.Spcialty, placement.Grade,placement.ClassId
                  );
                    SuccessFlag = Oledbcommand(dtPlacement);
                    return SuccessFlag;

        }
            return 0;
        }
       // string UpdateTeachers = string.Format("UPDATE Teachers_Data SET Teacher_ID ='{0}', Teacher ='{1}', Email ='{2}', Phone ='{3}', Title =‘{4}’, IsSupervisor ={5}, Teaching_Section ={6}, Accept_ClassNumber ={7}, Class_Totality ={8}, Class_WeekNumber ={9}, Class_DayNumber ={10} WHERE  Teacher ='{11}'");

        #endregion
        #region 选择记录
        public OleDbDataReader select(string table,string condition)
        {   
            string selectcommand = "select"+"*"+ "from" + table + condition;
            return getDataReader(selectcommand);
        }
        #endregion
        #region 分页显示
        public int totalpage(string sqlcommand,int pagesize,string tablename)//返回总的页数
        {
            DataTable dt = getDs(sqlcommand,tablename).Tables[0];
            int Linenumber = dt.Rows.Count;
            if ((Linenumber % pagesize) > 0)
            {
                int allpage = (Linenumber / pagesize) + 1;
                return allpage;
            }
            else
            {
                int allpage = Linenumber / pagesize;
                return allpage;
            }
           
        }
        private  DataTable datapageview(DataTable dt,int currentpage,int pagesize)//返回指定页数的记录
        {
            if (currentpage == 0)
                return dt;
            DataTable newdt = dt.Clone();
            //newdt.Clear();
            int rowbegin = (currentpage - 1) * pagesize;
            int rowend = currentpage * pagesize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }
        public DataTable ListviewShow(string selectcommand,int currentpage,int pagesize,string tablename)//Listview显示的内容
        {
            DataTable dt = getDs(selectcommand, tablename).Tables[0];
            DataTable Target = datapageview(dt,currentpage,pagesize);
            return Target;
        }
        #endregion
        #region 执行SQL语句返回DataReader
        public OleDbDataReader getDataReader(string commandtext)
        {

            OleDbCommand cmd = new OleDbCommand(commandtext, conn);

            if (conn.State != ConnectionState.Open) conn.Open();
           OleDbDataReader DtReader = cmd.ExecuteReader();
           return DtReader;

           
            
        }
        #endregion
        #region 执行SQL语句返回受影响行数
        public int Oledbcommand(string command)
        {
            try
            {
                int SuccessFlag;
                conn = new OleDbConnection(connString);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(command, conn);
                    SuccessFlag = cmd.ExecuteNonQuery();
                    conn.Dispose();
                    return SuccessFlag;
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand(command, conn);
                    SuccessFlag = cmd.ExecuteNonQuery();
                    conn.Dispose();
                    return SuccessFlag;
                }
            }
            catch (OleDbException e)
            {
                MessageBox.Show("ID重复输入");
                return 0;
            }
        }
        #endregion
        #region 返回dataset
        public DataSet getDs(string strCon, string tbname)
        {

            OleDbConnection conn1 = new OleDbConnection(connString);
            oledbda = new OleDbDataAdapter(strCon, conn1);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(oledbda);
            tableset = new DataSet();
            oledbda.Fill(tableset, tbname);
          
            return tableset;


        }
        #endregion
        #region 合并表格
        public void mergetable(DataTable dt,string sqlcommand)
        {
            OleDbConnection conn1 = new OleDbConnection(connString);
           
            OleDbDataAdapter mergetadapter = new OleDbDataAdapter(sqlcommand,conn1);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(mergetadapter);
            DataTable oldtable = new DataTable();
            mergetadapter.Fill(oldtable);
        
            oldtable.Merge(dt,true);
            mergetadapter.Update(dt);
        }
        #endregion
        public OleDbDataAdapter adapter(string strCon)
        {
            OleDbConnection conn1 = new OleDbConnection(connString);
            OleDbDataAdapter oledbda = new OleDbDataAdapter(strCon, conn1);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(oledbda);
            return oledbda; 
        }
        public void insertToStockDataByBatch(List<string> sqlArray,ProgressBar pb)
        {
            try
            {
                OleDbConnection aConnection = new OleDbConnection(connString);
               aConnection.Open();
                OleDbTransaction transaction = aConnection.BeginTransaction();
              

                OleDbCommand aCommand = new OleDbCommand();
                aCommand.Connection = aConnection;
                aCommand.Transaction = transaction;
                pb.Maximum = sqlArray.Count;
                pb.Step = 1;
                for (int i = 0; i < sqlArray.Count; i++)
                {
                    pb.PerformStep();
                    aCommand.CommandText = sqlArray[i].ToString();
                    aCommand.ExecuteNonQuery();
                    
                }
             
                transaction.Commit();
                aConnection.Close();
              
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
               
            }
        }
        public static object ExcuteScalar(string SQl, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = SQl;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();//返回一行object
                }
            }
        }
        public static DataTable ExecuteDataTable(string SQl, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = SQl;
                    cmd.Parameters.AddRange(parameters);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }//用来执行查询结果较少的sql
        public static int ExcuteNonQuery(string SQL, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();//返回值为影响的数据行数
                }
            }
        }

    }
    
}

        