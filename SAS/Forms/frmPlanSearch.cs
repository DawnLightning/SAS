using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAS.ClassSet.FunctionTools;
using SAS.ClassSet.Common;
using System.Diagnostics;
namespace SAS.Forms
{
    public partial class frmPlanSearch : DevComponents.DotNetBar.Office2007Form
    {

        private DataTable dt = null;
        private SqlHelper helper = new SqlHelper();
        private string selectcommand = "";
        private string filepath;
        public frmPlanSearch()
        {
            InitializeComponent();
            //this.EnableGlass = false; 
        }
        private void frmPlanSearch_Load(object sender, EventArgs e)
        {
           string selectcommand="SELECT distinct Teacher,Class_Name,Spcialty FROM Classes_Data";
           dt = helper.getDs(selectcommand, "classes").Tables[0];
           cbname.Items.Add("全部");
           cbclass.Items.Add("全部");
           cbspcial.Items.Add("全部");
           for (int i = 0; i < dt.Rows.Count;i++ )
           {
               cbname.Items.Add(dt.Rows[i][0].ToString());
               if (!cbclass.Items.Contains(dt.Rows[i][1].ToString()))
               {
                   cbclass.Items.Add(dt.Rows[i][1].ToString());
               }
               if (!cbspcial.Items.Contains(dt.Rows[i][2].ToString()))
               {
                    cbspcial.Items.Add(dt.Rows[i][2].ToString());
               }
              
              
           }
           cbname.SelectedIndex = 0;
           cbclass.SelectedIndex = 0;
           cbspcial.SelectedIndex = 0;
           DataRow[] dr = dt.Select();
           listviewshow(dr);

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {  
        }
        private void listviewshow(DataRow [] dr)
        {
            if (dr.Length == 0)
            {
                MessageBox.Show("没有找到指定数据");
            }
            else
            {
                for (int i = 0; i < dr.Length; i++)
                {
                    string[] recovery = new string[]
                { 
                    dr[i][0].ToString(),
                    dr[i][1].ToString(),
                    dr[i][2].ToString()
                };
                    ListViewItem lvi = new ListViewItem(recovery);
                    listView1.Items.Add(lvi);

                }
            }
           
        }
    
        private void buttonX2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            selectcommand = "";
            List<string> condition = new List<string>();
            if (cbname.Text == "全部")
            {
            }
            else
            {
                condition.Add("Teacher='" + cbname.Text + "'");
            }
            if (cbclass.Text == "全部")
            {
            } 
            else
            {
                condition.Add("Class_Name='" + cbclass.Text + "'");
            }
            if (cbspcial.Text=="全部")
            {

            } 
            else
            {
                condition.Add("Spcialty='" + cbspcial.Text + "'");
            }
          
            for (int i=0;i<condition.Count;i++)
            {
                selectcommand = selectcommand +"+"+ condition[i];
            }
            if (selectcommand!="")
            {
                selectcommand = selectcommand.Substring(1).Replace("+", " and ");
            }
        
            DataRow[] dr = dt.Select(selectcommand);
            listviewshow(dr);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ExportClass ex = new ExportClass();
            if (!System.IO.File.Exists(Common.strAddfilesPath + "课程表"))
            {
                System.IO.Directory.CreateDirectory(Common.strAddfilesPath + "课程表");
            }
            string filename = "课程表\\"+ cbspcial.Text + "专业" + cbname.Text + "老师的" + cbclass.Text + "课程表";
            filepath = Common.strAddfilesPath + filename+".docx";
            ex.MakeWordDoc(selectcommand,filename);
            MessageBox.Show("导出成功");
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(Common.strAddfilesPath + "课程表"))
            {
                Process.Start(Common.strAddfilesPath + "课程表");
            }  
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Process.Start(filepath);
        }
    }
}
