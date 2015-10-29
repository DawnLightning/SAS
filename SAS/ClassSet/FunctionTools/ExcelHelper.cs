using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using SAS.ClassSet.FunctionTools;
using System.Windows.Forms;
using System.Collections.Generic;
using SAS.ClassSet.Common;
using System.Data.OleDb;
/// <summary>
/// Excel导入，导出操作类
/// lcl add 2015-02-02
/// </summary>
public class NPOIHelper
{
    OleDbDataAdapter daClass;
    System.Data.DataTable dtClass;
    System.Data.DataTable Excel_dt;
    SqlHelper helper = new SqlHelper();
    string strSelect_Class_Data = "select * from Classes_Data";
    System.Data.DataTable dt = new System.Data.DataTable();
     List<string> ListSupervisor = new List<string>();
     List<string> dcs = new List<string> { "序号", "课程", "授课内容", "授课方式", "专业", "教室", "教师", "周次", "听课时间", "听课人员安排", "分数", "申报" };
    public  void ExportToExecl(string sqlcommand, string tablename)
    {
        System.Windows.Forms.SaveFileDialog sfd = new SaveFileDialog();
        sfd.DefaultExt = "xls";
        sfd.Filter = "Excel文件(*.xls)|*.xls";
        SqlHelper helper = new SqlHelper();
        DataTable dt = helper.getDs(sqlcommand, tablename).Tables[0];
        string strHeaderText = "信息工程学院2014－2015学年第一学期教学检查听课安排";
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            Export(dt, strHeaderText, sfd.FileName);
            MessageBox.Show("导出成功");
        }
    }
    #region DataTable导出到Excel文件 Export(dtSource,strHeaderText,strFileName)
    /// <summary>
    /// DataTable导出到Excel文件 Export()
    /// </summary>
    /// <param Name="dtSource">DataTable数据源</param>
    /// <param Name="strHeaderText">Excel表头文本（例如：车辆列表）</param>
    /// <param Name="strFileName">保存位置</param>
    public  void Export(DataTable dtSource, string strHeaderText, string strFileName)
    {
        using (MemoryStream ms = Export(dtSource, strHeaderText))
        {
            using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
        }
    }
    #endregion

    #region DataTable导出到Excel的MemoryStream Export(dtSource,strHeaderText)
    /// <summary>
    /// DataTable导出到Excel的MemoryStream Export()
    /// </summary>
    /// <param Name="dtSource">DataTable数据源</param>
    /// <param Name="strHeaderText">Excel表头文本（例如：车辆列表）</param>
    public  MemoryStream Export(DataTable dtSource, string strHeaderText)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet();

        #region 右击文件 属性信息
        {
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "文件作者信息"; //填加xls文件作者信息
            si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
            si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
            si.Comments = "作者信息"; //填加xls文件作者信息
            si.Title = "标题信息"; //填加xls文件标题信息
            si.Subject = "主题信息";//填加文件主题信息
            si.CreateDateTime = System.DateTime.Now;
            workbook.SummaryInformation = si;
        }
        #endregion

        ICellStyle dateStyle = workbook.CreateCellStyle();
        IDataFormat format = workbook.CreateDataFormat();
        dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

        int rowIndex = 0;
        int j = 0;
        foreach (DataRow row in dtSource.Rows)
        {
            #region 新建表，填充表头，填充列头，样式
            if (rowIndex == 65535 || rowIndex == 0)
            {
                if (rowIndex != 0)
                {
                    sheet = workbook.CreateSheet();
                }

                #region 表头及样式
                {
                    IRow headerRow = sheet.CreateRow(0);
                    headerRow.HeightInPoints = 25;
                    headerRow.CreateCell(0).SetCellValue(strHeaderText);

                    ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; // ------------------
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints =20;
                    font.Boldweight =800;
                    headStyle.SetFont(font);
                    headerRow.GetCell(0).CellStyle = headStyle;
                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dcs.Count - 1)); // ------------------
                }
                #endregion

                #region 列头及样式
                {
                    IRow headerRow = sheet.CreateRow(1);
                    ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; // ------------------
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 20;
                    font.Boldweight = 800;
                    font.FontName = "宋体";
                    headStyle.SetFont(font);
                    int index = 0;
                    foreach (string column in dcs)
                    {
                        headerRow.CreateCell(index).SetCellValue(column);
                        headerRow.GetCell(index).CellStyle = headStyle;
                        sheet.AutoSizeColumn(index);
                        //设置列宽
                        //sheet.SetColumnWidth(index, (arrColWidth[index] + 1) * 256);
                        //sheet.SetColumnWi
                        index++;
                    }
                }
                #endregion

                rowIndex = 2;
            }
            #endregion

            #region 填充内容
          
            IRow dataRow = sheet.CreateRow(rowIndex);
                
                int columnIndex = 0;
                DistinctSupervisor(dtSource.Rows[j][6].ToString(), ListSupervisor);//去职称
                for (int i = 0; i < dcs.Count; i++)
                {   
                    ICell newCell = dataRow.CreateCell(columnIndex);
                    columnIndex++;

                   
                    if (i != dcs.Count - 1)
                    {

                        //注意这个在导出的时候加了“\t” 的目的就是避免导出的数据显示为科学计数法。可以放在每行的首尾。
                        switch (i)
                        {
                            case 0:

                                newCell.SetCellValue((j + 1).ToString() + "\t");
                                
                                break;
                            case 1:

                                newCell.SetCellValue(dtSource.Rows[j][8].ToString() + "\t");

                                break;
                            case 2:

                                newCell.SetCellValue(dtSource.Rows[j][9].ToString() + "\t");

                                break;
                            case 3:

                                newCell.SetCellValue(dtSource.Rows[j][10].ToString() + "\t");

                                break;
                            case 4:

                                newCell.SetCellValue(dtSource.Rows[j][11].ToString() + "\t");

                                break;
                            case 5:

                                newCell.SetCellValue(dtSource.Rows[j][7].ToString() + "\t");

                                break;
                            case 6:

                                newCell.SetCellValue(dtSource.Rows[j][2].ToString() + "\t");

                                break;
                            case 7:

                                newCell.SetCellValue(dtSource.Rows[j][3].ToString() + "\t");

                                break;
                            case 8:
                                //听课时间

                                newCell.SetCellValue(
                                CalendarTools.getdata(Common.Year, Convert.ToInt32(dtSource.Rows[j][3]), Convert.ToInt32(dtSource.Rows[j][4]) - CalendarTools.weekdays(CalendarTools.CaculateWeekDay(Common.Year, Common.Month, Common.Day)), Common.Month, Common.Day).ToLongDateString()
                                + " " + addseparator(Convert.ToInt32(dtSource.Rows[j][5])) + "节" + "\t");

                                break;
                            case 9:

                                newCell.SetCellValue(FormatSupervisor(ListSupervisor) + "\t");

                                break;
                            case 10:

                                newCell.SetCellValue(dtSource.Rows[j][12].ToString() + "\t");

                                break;
                        }

                    }
                    else
                    {

                        newCell.SetCellValue(" ");

                    }

                }
           
            #endregion
                j++;
            rowIndex++;
        }
        adjustcolum(sheet);//调整列宽
        AddBorder(sheet,workbook);//加边框
        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
           
            return ms;
        }
    }
    #endregion

    #region 读取excel ,默认第一行为标头Import()
    /// <summary>
    /// 读取excel ,默认第一行为标头
    /// </summary>
    /// <param Name="strFileName">excel文档路径</param>
    /// <returns></returns>
    public  int Import(string strFileName)
    {
        try
        {
            Excel_dt = new DataTable();
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
          
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            for (int j = 0; j < cellCount; j++)
            {
               
                Excel_dt.Columns.Add(j.ToString());
            }
            DataRow title = Excel_dt.NewRow();//标题
            title[0] = sheet.GetRow(0).ToString();
            Excel_dt.Rows.Add(title);
            DataRow term = Excel_dt.NewRow();//学期
            term[0] = sheet.GetRow(1).ToString();
            Excel_dt.Rows.Add(term);
            DataRow classinfo = Excel_dt.NewRow();//课程专业
            classinfo[0] = sheet.GetRow(2).GetCell(0).ToString();
            classinfo[1] = sheet.GetRow(2).GetCell(4).ToString();
            Excel_dt.Rows.Add(classinfo);

            DataRow student = Excel_dt.NewRow();//年级和班级
            student[0] = sheet.GetRow(3).GetCell(0).ToString();
            student[1] = sheet.GetRow(3).GetCell(4).ToString();
            Excel_dt.Rows.Add(student);
            
            DataRow book = Excel_dt.NewRow();//教材
            book[0] = sheet.GetRow(4).GetCell(0).ToString();
            Excel_dt.Rows.Add(book);
          
            //headerRow = sheet.GetRow(5);
          

            for (int i = 5; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row!=null)
                {
                    DataRow dataRow = Excel_dt.NewRow();

                    for (int j = 0; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }
                    Excel_dt.Rows.Add(dataRow);
                }
                else
                {
                    break;
                }
               
            }
          
            int[] name = new int[7];//确定Excel的所需字段值所在的列---
            daClass = helper.adapter(strSelect_Class_Data);
            dtClass = new System.Data.DataTable();
            daClass.Fill(dtClass);
            daClass.FillSchema(dtClass, SchemaType.Source);
            string classname = Excel_dt.Rows[2][0].ToString();//课程名称
            string spcialty = Excel_dt.Rows[2][1].ToString().Substring(3);//专业
            string banji = Excel_dt.Rows[3][1].ToString();
            for (int q = 0; q < Excel_dt.Columns.Count; q++)
            {
                switch (Excel_dt.Rows[5][q].ToString())
                {
                    case "周次": name[0] = q; break;
                    case "星期": name[1] = q; break;
                    case "节次": name[2] = q; break;
                    case "上课地点": name[3] = q; break;
                    case "授课教师": name[4] = q; break;
                    case "授课内容": name[5] = q; break;
                    case "授课方式": name[6] = q; break;
                }
            }

            dt = dtClass.Copy();   //  获取Class_Data的架构
            dt.Clear();
            for (int i = 6; i < Excel_dt.Rows.Count; i++)
            {
                // dr = dt.Rows[i];//获取Excel的当前操作行的数据
                if (!(Excel_dt.Rows[i][name[0]].ToString() == "周次" || Excel_dt.Rows[i][name[0]].ToString() == ""))
                {
                    string teachername = Excel_dt.Rows[i][name[4]].ToString();//获取授课老师列的数据
                    int k = Teacher(teachername) + 1;//判断有多少位老师上同一节课
                    for (int m = 1; m <= k; m++)//有几位老师，就循环几次
                    {
                        string teachernamepick;//定义截取的老师名字
                        //以逗号为分界点，把多位老师的名字分成各自的名字
                        if ((k == 1) || (m == k)) teachernamepick = teachername;
                        else
                        {
                            int index2 = teachername.IndexOf(",");
                            teachernamepick = teachername.Substring(0, index2);
                            teachername = teachername.Remove(0, index2 + 1);
                        }


                        int j;
                        //判断星期几，返回对应的数字
                        switch (Excel_dt.Rows[i][name[1]].ToString().Substring(0, 1))
                        {
                            case "一": j = 1; break;
                            case "二": j = 2; break;
                            case "三": j = 3; break;
                            case "四": j = 4; break;
                            case "五": j = 5; break;
                            default: j = 0; break;
                        }
                        if (teachernamepick != null && teachernamepick != "")
                        {
                            DataRow drClass_information = dt.NewRow();
                            drClass_information["Class_Day"] = j;
                            //获取节次          
                            string strclassname = Excel_dt.Rows[i][name[2]].ToString();
                            int classnumindex = strclassname.IndexOf("-");
                            drClass_information["Teacher"] = teachernamepick;
                            drClass_information["Class_ID"] = teachernamepick + Excel_dt.Rows[i][name[0]].ToString() + j.ToString() + strclassname.Substring(0, classnumindex) + strclassname.Substring(classnumindex + 1) + classname.Substring(5) + Excel_dt.Rows[i][name[3]] + banji;
                            drClass_information["Teacher_ID"] = "0000000000";
                            drClass_information["Class_Week"] = Excel_dt.Rows[i][name[0]];
                            drClass_information["Class_Number"] = Convert.ToInt32(strclassname.Substring(0, classnumindex) + strclassname.Substring(classnumindex + 1));
                            drClass_information["Class_Address"] = Excel_dt.Rows[i][name[3]];
                            drClass_information["Class_Name"] = classname.Substring(5);
                            drClass_information["Class_Content"] = Excel_dt.Rows[i][name[5]];
                            drClass_information["Class_Type"] = Excel_dt.Rows[i][name[6]];
                            drClass_information["Spcialty"] = spcialty;

                            dt.Rows.Add(drClass_information);
                        }
                        else
                        {
                            return 0;
                        }

                    }
                }



            }
            dtClass.Merge(dt, true);

            daClass.Update(dtClass);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
       
    }
    private int Teacher(string strTeacher)
    {
        return strTeacher.Length - strTeacher.Replace(",", "").Length;
    }
    #endregion
    //生成间隔符“-”
    private  string addseparator(int classnumber)
    {
        string newclassnumber = "";
        switch (classnumber)
        {
            case 12: newclassnumber = "1-2"; break;
            case 13: newclassnumber = "1-3"; break;
            case 23: newclassnumber = "2-3"; break;
            case 24: newclassnumber = "2-4"; break;
            case 34: newclassnumber = "3-4"; break;
            case 35: newclassnumber = "3-5"; break;
            case 45: newclassnumber = "4-5"; break;
            case 46: newclassnumber = "4-6"; break;
            case 67: newclassnumber = "6-7"; break;
            case 68: newclassnumber = "6-8"; break;
            case 78: newclassnumber = "7-8"; break;
            case 79: newclassnumber = "7-9"; break;
            case 89: newclassnumber = "8-9"; break;
            case 1011: newclassnumber = "10-11"; break;
            case 1112: newclassnumber = "11-12"; break;
            case 1012: newclassnumber = "10-12"; break;
        }
        return newclassnumber;
    }
    //去掉职称
    private  string DistinctSupervisor(string supervisor, List<string> ListSupervisor)
    {
        
        if (supervisor.IndexOf(",") != -1)
        {
            ListSupervisor.Add(supervisor.Substring(0, supervisor.IndexOf(",")));
            return DistinctSupervisor(supervisor.Substring(supervisor.IndexOf(",") + 1), ListSupervisor);
        }
        else
        {
            ListSupervisor.Add(supervisor);
            return supervisor;
        }

    }
    private  string FormatSupervisor(List<string> List)
    {
        string supervisor = "";
      
        foreach (string s in List)
        {
            if (s.IndexOf("(") != -1)
            {
                supervisor = supervisor + "," + s.Substring(0, s.IndexOf("("));
            }
            else
            {
                supervisor = supervisor + "," + s;
            }
        }
        List.Clear();
        return supervisor.Substring(1);

    }
    private  string FormatTeacher(string s)
    {
        string supervisor = "";
     

        if (s.IndexOf("(") != -1)
        {
            supervisor = supervisor + "," + s.Substring(0, s.IndexOf("("));
        }
        else
        {
            supervisor = supervisor + "," + s;
        }

        return supervisor.Substring(1);

    }
    /// <summary>
    /// 加边框
    /// </summary>
    /// <param Name="rowindex">1开始</param>
    /// <param Name="cellIndex">1开始</param>
    public  void AddBorder( ISheet sheet, HSSFWorkbook workbook)

    {
        ICellStyle styel = workbook.CreateCellStyle();
        styel.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; // ------------------
        IFont font1 = workbook.CreateFont();
        font1.FontHeightInPoints = 11;
      
        font1.Boldweight = 600;
        font1.FontName = "宋体";
        styel.SetFont(font1);
        for (int rowindex=1;rowindex<sheet.LastRowNum+1;rowindex++)
        {
            for (int cellIndex =0; cellIndex < dcs.Count;cellIndex++ )
            {
                sheet.GetRow(rowindex).RowStyle = styel;
                ICell cell = sheet.GetRow(rowindex ).GetCell(cellIndex );
                
                HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;

                Style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                Style.VerticalAlignment = VerticalAlignment.Center;
                Style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                Style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                Style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                Style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                Style.DataFormat = 0;
                Style.SetFont(font1);
                cell.CellStyle = Style;
            }
         }
       
    }
    
    private  void adjustcolum(ISheet sheet)
    {
        for (int columnNum = 0; columnNum < dcs.Count; columnNum++)
        {
            int columnWidth = sheet.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
            for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)//在这一列上循环行  
            {
                IRow currentRow = sheet.GetRow(rowNum);
                ICell currentCell = currentRow.GetCell(columnNum);
               
                int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度  
                if (columnWidth < length + 1)
                {
                    columnWidth = length + 1;
                }//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符  
            }
            sheet.SetColumnWidth(columnNum, columnWidth*256);  
        }
    }
}

