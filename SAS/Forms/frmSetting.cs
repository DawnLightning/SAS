using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAS.ClassSet.Common;
using System.IO;
using SAS.ClassSet.FunctionTools;
namespace SAS.Forms
{
    public partial class frmSetting : Form
    {

        public frmSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int a1, b1, c1;
                if (Int32.Parse(txtYear.Text) > Int32.Parse(DateTime.Now.AddYears(-5).ToString("yyyy")) &&
                    Int32.Parse(txtYear.Text) < Int32.Parse(DateTime.Now.AddYears(10).ToString("yyyy")))
                {
                    //判断文本框输入的内容是否符合要求
                    if (txtYear.Text == "" || txtMonth.Text == "" || txtDay.Text == "")
                    {
                        MessageBox.Show("文本框不能为空！"); return;
                    }
                    else
                    {
                        a1 = Convert.ToInt32(txtYear.Text);
                        b1 = Convert.ToInt32(txtMonth.Text);
                        c1 = Convert.ToInt32(txtDay.Text);
                        if (a1 < 1 || b1 < 1 || b1 > 12 || c1 < 1)
                        {
                            MessageBox.Show("文本框输入的内容不符合要求"); return;
                        }
                        else
                        {
                            if (b1 == 2)
                            {
                                if (a1 % 400 == 0 || (a1 % 4 == 0 && a1 % 100 != 0))
                                {
                                    if (c1 > 29)
                                    {
                                        MessageBox.Show("文本框输入的内容不符合要求"); return;
                                    }
                                }

                                else if (c1 > 28)
                                {
                                    MessageBox.Show("文本框输入的内容不符合要求");
                                    return;
                                }
                            }
                            else if (b1 == 1 || b1 == 3 || b1 == 5 || b1 == 7 || b1 == 8 || b1 == 10 || b1 == 12)
                            {
                                if (c1 > 31)
                                {
                                    MessageBox.Show("文本框输入的内容不符合要求");
                                    return;
                                }
                            }
                            else
                            {
                                if (c1 > 30)
                                {
                                    MessageBox.Show("文本框输入的内容不符合要求");
                                    return;
                                }
                                if (string.IsNullOrWhiteSpace(txbMailAddress.Text) || string.IsNullOrWhiteSpace(txbMailPassword.Text) || string.IsNullOrWhiteSpace(comboBox1.Text))
                                {
                                    MessageBox.Show("请输入邮箱信息");
                                    return;
                                }

                            }
                            Common.Year = Convert.ToInt32(txtYear.Text.Trim());
                            Common.Month = Convert.ToInt32(txtMonth.Text.Trim());
                            Common.Day = Convert.ToInt32(txtDay.Text.Trim());
                            Common.MailAddress = txbMailAddress.Text.Trim() + "@" + comboBox1.Text.Trim();
                            Common.MailPassword = txbMailPassword.Text;
                            Common.xmlSave();

                            if (MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                this.Close();
                            }
                        }
                    }
                }
                else
                {

                    MessageBox.Show("请输入合适的年份", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtYear.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            if (Common.Year >= 0 && Common.Month > 0 && Common.Day > 0)
            {
                txtYear.Text = Common.Year.ToString();
                txtMonth.Text = Common.Month.ToString();
                txtDay.Text = Common.Day.ToString();
                if (!(string.IsNullOrWhiteSpace(Common.MailAddress) || string.IsNullOrWhiteSpace(Common.MailPassword)) && Common.MailAddress.IndexOf('@') != -1)
                {
                    string[] arr = Common.MailAddress.Split('@');
                    txbMailAddress.Text = arr[0];
                    comboBox1.Text = arr[1];
                    txbMailPassword.Text = Common.MailPassword;
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要执行重置操作？这将会删除除“教师信息”外的所有数据！", "警告！！！", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    SqlHelper help= new SqlHelper();
                    help.delete("Logs_Data","");
                    help.delete("Placement_Data", "");
                    help.delete("SpareTime_Data", "");
                    help.delete("Classes_Data", "");
                    help.delete("Conf", "");
                    
                    frmMain.fm.flashListview();
                    MessageBox.Show("操作成功");
                }
                catch { MessageBox.Show("操作失败"); }
            }
        }

        private void txtMonth_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtMonth.Text.Length == 2)
            {
                this.txtDay.Focus();
            }
        }

        private void txtYear_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtYear.Text.Length == 4)
            {
                this.txtMonth.Focus();
            }


        }

        private void txtDay_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



    }
}
