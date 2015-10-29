using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SAS.ClassSet.MemberInfo;
using SAS.ClassSet.FunctionTools;
namespace SAS.Forms
{
    public partial class frmPlacement : DevComponents.DotNetBar.Office2007Form
    {
        public static frmPlacement fmp;
        public frmPlacement()
        {   
            InitializeComponent();
            this.EnableGlass = false;
        }
        int cbegin_week;
        int cbegin_day;
        int cnumclass_week;
        int cnumpeo_max;
        int cnumpeo_min;
        int proportion;
        private void checkIsNull()
        {
            DataTable dt = new SqlHelper().getDs("select * from Placement_Data", "Placement_Data").Tables[0];
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != ""
                && textBox5.Text != "" )
            {   if(dt.Rows.Count==0)
            {
                buttonX1.Enabled = true;
            }
                 buttonX2.Enabled = true;
            }
            else
            {
                buttonX1.Enabled = false;
                buttonX2.Enabled = false;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            checkIsNull();
        }
        public bool checkType()
        {
            bool result;
          
            try
            {
              
                cbegin_week = Convert.ToInt32(textBox3.Text);
                cbegin_day = Convert.ToInt32(textBox5.Text);
                cnumclass_week = Convert.ToInt32(textBox4.Text);
                cnumpeo_max = Convert.ToInt32(textBox2.Text);
               cnumpeo_min= Convert.ToInt32(textBox1.Text);
               proportion = Convert.ToInt32(textBox6.Text);
               if (cbegin_week > 0 && cbegin_day > 0 && cnumclass_week > 0 && cnumpeo_max > 0 && cnumpeo_min > 0)
               {
                   if (cbegin_day < 6)
                   {
                       result = true;
                   }
                   else {
                       MessageBox.Show("请输入1-5的整数");
                       result = false;
                   }
               }
               else
               {
                   result = false;
               }
              
                return result;
              
            }catch(Exception)
            {
                MessageBox.Show("请输入整数");
                result = false;
                return result;
            }
           
           

            
        }
        private void btnPageUp_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkIsNull();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            checkIsNull();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            checkIsNull();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            checkIsNull();
        }

        private void frmPlacement_Load(object sender, EventArgs e)
        {
           
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            SqlHelper help = new SqlHelper();
            DataTable dtsparetime = help.getDs("select * from SpareTime_Data", "SpareTime_Data").Tables[0];
            DataTable dtclass = help.getDs("select * from Classes_Data", "Classes_Data").Tables[0];
            DataTable dtteacher = help.getDs("select * from Teachers_Data", "Teachers_Data").Tables[0];


            if (checkType())
            {
            if (cnumpeo_max >= cnumpeo_min)
            {
            if (dtsparetime.Rows.Count != 0 && dtclass.Rows.Count != 0 && dtteacher.Rows.Count != 0)
            {
                Main.fm.SetStatusText("正在工作中，请耐心等待~~", 1);
                //MessageBox.Show("OK");

                PlacementConfig pc = new PlacementConfig(cbegin_week, cbegin_day, cnumclass_week, cnumpeo_max, cnumpeo_min, proportion);
                Placement doplacement = new Placement();
                doplacement.MakePlacement(pc);
            }
            else
            {
                MessageBox.Show("请导入数据后重试");
            }

            }
            else
            {
            MessageBox.Show("最大人数不能小于最小人数");
            }





            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            SqlHelper help = new SqlHelper();
            DataTable dtsparetime = help.getDs("select * from SpareTime_Data", "SpareTime_Data").Tables[0];
            DataTable dtclass = help.getDs("select * from Classes_Data", "Classes_Data").Tables[0];
            DataTable dtteacher = help.getDs("select * from Teachers_Data", "Teachers_Data").Tables[0];
            if (checkType())
            {
                if (cnumpeo_max >= cnumpeo_min)
                {
                    if (dtsparetime.Rows.Count != 0 && dtclass.Rows.Count != 0 && dtteacher.Rows.Count != 0)
                    {
                        Main.fm.SetStatusText("正在工作中，请耐心等待~~", 1);
                        //MessageBox.Show("OK");


                        PlacementConfig pc = new PlacementConfig(cbegin_week, cbegin_day, cnumclass_week, cnumpeo_max, cnumpeo_min, proportion);
                        Placement doplacement = new Placement();
                        doplacement.RePlacement(pc);
                    }
                    else
                    {
                        MessageBox.Show("请导入数据后重试");
                    }
                }
                else
                {
                    MessageBox.Show("最大人数不能小于最小人数");
                }

            }
        }
    }
}
