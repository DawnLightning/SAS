using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAS.ClassSet.FunctionTools;
namespace SAS.Forms
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }
        public static DateTime date;
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            date = monthCalendar1.SelectionStart;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            date = monthCalendar1.TodayDate;
            comboBox1.SelectedIndex = 0;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    SqlHelper help = new SqlHelper();
                    DataTable dt = help.getDs("select * from Classes_Data", "Classes_Data").Tables[0];
                    frmClasses frm = new frmClasses(date, dt);
                    frm.Show();
                    this.Close();
                }
                else
                {
                    frmPlanSearch frm = new frmPlanSearch(date);
                    frm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("请选择查找的数据表");
            }
        }
    }
}
