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
    public partial class frmPlacement : Form
    {
        public static frmPlacement fmp;
        public frmPlacement()
        {   
            InitializeComponent();
        }
        int cbegin_week;
        int cbegin_day;
        int cnumclass_week;
        int cnumpeo_max;
        int cnumpeo_min;

        private void checkIsNull()
        {
            DataTable dt = new SqlHelper().getDs("select * from Placement_Data", "Placement_Data").Tables[0];
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != ""
                && textBox5.Text != "" )
            {   if(dt.Rows.Count==0)
            {
                btnPageUp.Enabled = true;
            }
                button3.Enabled = true;
            }
            else
            {
                btnPageUp.Enabled = false;
                button3.Enabled = false;
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
           if(checkType())
           {    
               frmMain.fm.SetStatusText("正在工作中，请耐心等待~~", 1);
               //MessageBox.Show("OK");
              
               PlacementConfig pc = new PlacementConfig(cbegin_week,cbegin_day,cnumclass_week,cnumpeo_max,cnumpeo_min);
               Placement doplacement = new Placement();
               doplacement.MakePlacement(pc);
              
               
             
               
               
           }
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
        {   if(checkType())
        {
            frmMain.fm.SetStatusText("正在工作中，请耐心等待~~", 1);
            //MessageBox.Show("OK");


            PlacementConfig pc = new PlacementConfig(cbegin_week, cbegin_day, cnumclass_week, cnumpeo_max, cnumpeo_min);
            Placement doplacement = new Placement();
            doplacement.RePlacement(pc);
          
        }
        }
    }
}
