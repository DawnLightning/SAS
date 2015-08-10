
using System;
using System.Collections;
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
    public partial class frmSupervisor : Form
    {
        public frmSupervisor(string name,string id)
        {
            this.DDName = name;
            this.DDID = id;
            InitializeComponent();
        }
        SqlHelper help = new SqlHelper();
        ArrayList A1 = new ArrayList(), InsertArray = new ArrayList();
        int[] intcheck = new int[7];
        string[] sAWeek = new string[7];
        ArrayList[] aAWeek = new ArrayList[7];
        string DDID;
        string DDName;
        string strID;
        int iyanzheng = 0;//, ibaocun;
        string[] strBClass2 = { "0102", "0203", "0304", "0405", "0607", "0708", "0809", "1011", "1112" }, strBClass3 = { "0103", "0204", "0305", "0608", "0709", "1012" };
        ArrayList Ctoclass(string s)
        {
            ArrayList a1 = new ArrayList();
            string s1;
            while (s.IndexOf("10") > 0)
            {
                s1 = s.Substring(s.IndexOf("10"), 4);
                if (!a1.Contains(s1.Substring(0, 2)))
                {
                    a1.Add(s1.Substring(0, 2));
                }
                if (!a1.Contains(s1.Substring(2)))
                {
                    a1.Add(s1.Substring(2));
                }
                s = s.Substring(0, s.IndexOf("10")) + s.Substring(s.IndexOf("10") + 4);
            }
            if (s.IndexOf("11") > 0)
            {
                s1 = s.Substring(s.IndexOf("11"), 4);
                if (!a1.Contains(s1.Substring(0, 2)))
                {
                    a1.Add(s1.Substring(0, 2));
                }
                if (!a1.Contains(s1.Substring(2)))
                {
                    a1.Add(s1.Substring(2));
                }
                s = s.Substring(0, s.IndexOf("11")) + s.Substring(s.IndexOf("11") + 4);
            }
            for (int i = 0; i < s.Count(); i++)
            {
                if (!a1.Contains(s.Substring(i, 1)))
                {
                    a1.Add(s.Substring(i, 1));
                }
            }
            return a1;
        }
        string ConvertToChinese(int i)
        {
            string s = "";
            string si = i.ToString();
            for (int j = 0; j < si.Length; j++)
            {
                switch (si[j])
                {
                    case '1': s += "一";
                        break;
                    case '2': s += "二";
                        break;
                    case '3': s += "三";
                        break;
                    case '4': s += "四";
                        break;
                    case '5': s += "五";
                        break;
                    case '6': s += "六";
                        break;
                    case '7': s += "日";
                        break;
                }
            }
            return s;
        }
        ArrayList ConvertToBClass(string s)
        {
            ArrayList sA1 = new ArrayList();
            for (int i = 0; i < s.Length - 3; i++)
            {
                if (strBClass2.Contains(s.Substring(i, 4)))
                {
                    if (s.Substring(i, 1) == "0" && s.Substring(i + 2, 1) == "0")
                    {
                        sA1.Add(s.Substring(i + 1, 1) + s.Substring(i + 3, 1));
                    }
                    else
                    {
                        sA1.Add(s.Substring(i, 4));
                    }
                }
            }
            for (int i = 0; i < s.Length - 5; i++)
            {
                if (strBClass3.Contains(s.Substring(i, 2) + s.Substring(i + 4, 2)))
                {
                    if (s.Substring(i, 1) == "0" && s.Substring(i + 2, 1) == "0" && s.Substring(i + 4, 1) == "0")
                    {
                        sA1.Add(s.Substring(i + 1, 1) + s.Substring(i + 5, 1));
                    }
                    else
                    {
                        sA1.Add(s.Substring(i, 2) + s.Substring(i + 4, 2));
                    }
                }
            }
            return sA1;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (chkBatchAdd.Checked)
            {
                if (tBStart.Text == "" || tBStart.Text == "00" || tBStart.Text == "0" || tBEnd.Text == "" || tBEnd.Text == "00" || tBEnd.Text == "0")
                {
                    MessageBox.Show("周次不能为空或0！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            else
            {
                if (tBWeek.Text == "" || tBWeek.Text == "00" || tBWeek.Text == "0")
                {
                    MessageBox.Show("周次不能为空或0！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            foreach (ListViewItem LVI in listView3.Items)
            {
                LVI.Checked = true;
            }
            string[] strWeek = new string[7];
            foreach (object obj in A1)
            {
                string S1 = obj.ToString();
                switch (S1[0])
                {
                    case '1': strWeek[0] += S1.Substring(S1.IndexOf("第") + 1, 2);
                        break;
                    case '2': strWeek[1] += S1.Substring(S1.IndexOf("第") + 1, 2);
                        break;
                    case '3': strWeek[2] += S1.Substring(S1.IndexOf("第") + 1, 2);
                        break;
                    case '4': strWeek[3] += S1.Substring(S1.IndexOf("第") + 1, 2);
                        break;
                    case '5': strWeek[4] += S1.Substring(S1.IndexOf("第") + 1, 2);
                        break;
                    case '6': strWeek[5] += S1.Substring(S1.IndexOf("第") + 1, 2);
                        break;
                    case '7': strWeek[6] += S1.Substring(S1.IndexOf("第") + 1, 2);
                        break;
                }
            }
            ArrayList[] ALWeek = new ArrayList[7];
            for (int i = 0; i < 7; i++)
            {
                ALWeek[i] = new ArrayList();
                if (strWeek[i] != null)
                {
                    ALWeek[i] = ConvertToBClass(strWeek[i]);
                }
            }
            help.Oledbcommand("delete from SpareTime_Data where Supervisor_ID='" + DDID + "' and Spare_Week=" + tBWeek.Text + ";");
        
            if (chkBatchAdd.Checked == false)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (ALWeek[i].Count > 0)
                    {
                        for (int j = 0; j < ALWeek[i].Count; j++)
                        {
                            try
                            {
                                if (tBWeek.Text.Count() == 1)
                                { strID = DDID + "0" + tBWeek.Text + (i + 1).ToString() + ALWeek[i][j]; }
                                else
                                { strID = DDID + tBWeek.Text + (i + 1).ToString() + ALWeek[i][j]; }
                                InsertArray.Add("insert into SpareTime_Data(Spare_ID,Supervisor_ID,Supervisor,Spare_Week,Spare_Day,Spare_Number) values('" + strID + "','" + DDID + "','" + DDName + "','" + tBWeek.Text + "','" + (i + 1).ToString() + "','" + ALWeek[i][j] + "')");
                            }
                            catch {  }
                        }
                    }
                }
              
                for (int i = 0; i < InsertArray.Count; i++)
                {
                    try
                    {

                        help.Oledbcommand(InsertArray[i].ToString());
                    }
                    catch { }
                }
                InsertArray.Clear();
                iyanzheng = 1;
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                if (int.Parse(tBEnd.Text) <= int.Parse(tBStart.Text))
                {
                    MessageBox.Show("终止周必须大于起始周!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    tBEnd.Select();
                    return;
                }
                for (int i1 = int.Parse(tBStart.Text); i1 <= int.Parse(tBEnd.Text); i1++)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (ALWeek[i].Count > 0)
                        {
                            for (int j = 0; j < ALWeek[i].Count; j++)
                            {
                                try
                                {
                                    if (i1.ToString().Count() == 1)
                                    { strID = DDID + "0" + i1.ToString() + (i + 1).ToString() + ALWeek[i][j]; }
                                    else
                                    { strID = DDID + i1.ToString() + (i + 1).ToString() + ALWeek[i][j]; }

                                    InsertArray.Add("insert into SpareTime_Data(Spare_ID,Supervisor_ID,Supervisor,Spare_Week,Spare_Day,Spare_Number) values('" + strID + "','" + DDID + "','" + DDName + "','" + i1.ToString() + "','" + (i + 1).ToString() + "','" + ALWeek[i][j] + "')");
                                }
                                catch
                                {
                                    
                                }
                            }
                        }
                    }

                }
                for (int i = 0; i < InsertArray.Count; i++)
                {
                    try
                    {

                        help.Oledbcommand(InsertArray[i].ToString());
                    }
                    catch { }
                }
                InsertArray.Clear();
                iyanzheng = 1;
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void frmSupervisor_Load(object sender, EventArgs e)
        {
            sAWeek = select(DDName, tBWeek.Text);
            for (int i = 0; i < 7; i++)
            {
                aAWeek[i] = new ArrayList();
                if (sAWeek[i] != null) aAWeek[i] = Ctoclass(sAWeek[i]);
            }
            foreach (Control Cl in panelFree.Controls)
            {
                if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                {
                    CheckBox CB = (CheckBox)Cl;
                    if (CB.ThreeState == false)
                    {
                        int iday = int.Parse(Cl.Name.Substring(8, 1));
                        int inum = int.Parse(Cl.Name.Substring(10));
                        if (aAWeek[iday - 1].Contains(inum.ToString()))
                        {

                            CB.Checked = true;

                        }
                    }
                }
            }
        }
        private void checkBox1_1_CheckedChanged(object sender, EventArgs e)
        {
            for (int j1 = 0; j1 < 7;j1++ )
            {
                intcheck[j1] = 0;
            }
            foreach (Control Cl in panelFree.Controls)
            {
                if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                {
                    CheckBox CB1 = (CheckBox)Cl;
                    if (CB1.ThreeState == false && CB1.Checked == true)
                    {
                        int iday = int.Parse(Cl.Name.Substring(8, 1));
                        int inum = int.Parse(Cl.Name.Substring(10));
                        intcheck[iday - 1]++;
                    }
                }
            }
            int intck = 0;
            for (int j1 = 0; j1 < 7; j1++)
            {
                if(intcheck[j1]<1)
                {
                    foreach (Control Cl in panelFree.Controls)
                    {
                        if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                        {
                            CheckBox CB1 = (CheckBox)Cl;
                            if (CB1.ThreeState == true && CB1.Name == "checkBox"+(j1+1).ToString())
                            {
                                CB1.CheckState = CheckState.Unchecked;
                            }
                        }
                    }
                }
                else if(intcheck[j1]<12)
                {
                    foreach (Control Cl in panelFree.Controls)
                    {
                        if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                        {
                            CheckBox CB1 = (CheckBox)Cl;
                            if (CB1.ThreeState == true && CB1.Name == "checkBox" + (j1 + 1).ToString())
                            {
                                CB1.CheckState = CheckState.Indeterminate;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Control Cl in panelFree.Controls)
                    {
                        if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                        {
                            CheckBox CB1 = (CheckBox)Cl;
                            if (CB1.ThreeState == true && CB1.Name == "checkBox" + (j1 + 1).ToString())
                            {
                                CB1.CheckState = CheckState.Checked;
                            }
                        }
                    }
                }
                intck += intcheck[j1];
            }
            if(intck<1)
            {
                checkBox0.CheckState = CheckState.Unchecked;
            }
            else if(intck<84)
            {
                checkBox0.CheckState = CheckState.Indeterminate;
            }
            else
            {
                checkBox0.CheckState = CheckState.Checked;
            }
            CheckBox CB = sender as CheckBox;
            string strInput = CB.Name.Substring(8) + " " + "星期" + ConvertToChinese(int.Parse(CB.Name.Substring(8, CB.Name.IndexOf('_') - 8))) + ",第" + CB.Name.Substring(CB.Name.IndexOf('_') + 1) + "节";
            if (CB.Name.Length == 11)
            {
                strInput = CB.Name.Substring(8) + " " + "星期" + ConvertToChinese(int.Parse(CB.Name.Substring(8, CB.Name.IndexOf('_') - 8))) + ",第0" + CB.Name.Substring(CB.Name.IndexOf('_') + 1) + "节";
            }
            if (CB.Checked)
            {
                if (A1.Count == 0)
                {
                    A1.Add(strInput);
                    if (chkBatchAdd.Checked)
                    {
                        ListViewItem LVI = listView3.Items.Add(DDName);
                        LVI.SubItems.Add("第" + tBStart.Text + "周-" + "第" + tBEnd.Text + "周" + " " + strInput.Substring(strInput.IndexOf(" ") + 1, strInput.IndexOf((',')) - strInput.IndexOf(" ") - 1) + " " + strInput.Substring(strInput.IndexOf(',') + 1));
                    }
                    else
                    {
                        ListViewItem LVI = listView3.Items.Add(DDName);
                        LVI.SubItems.Add("第" + tBWeek.Text + "周" + " " + strInput.Substring(strInput.IndexOf(" ") + 1, strInput.IndexOf((',')) - strInput.IndexOf(" ") - 1) + " " + strInput.Substring(strInput.IndexOf(',') + 1));
                    }
                }
                else
                {

                    int intA1ct = A1.Count;
                    foreach (object obj in A1)
                    {
                        if (obj.ToString().Substring(0, obj.ToString().IndexOf('_')) == strInput.Substring(0, strInput.IndexOf('_')) && (int.Parse(obj.ToString().Substring(obj.ToString().IndexOf('_') + 1, obj.ToString().IndexOf(" ") - obj.ToString().IndexOf('_') - 1)) > int.Parse(strInput.Substring(strInput.IndexOf('_') + 1, strInput.IndexOf(" ") - strInput.IndexOf('_') - 1))))
                        {
                            int insertadress = A1.IndexOf(obj);
                            A1.Insert(insertadress, strInput);
                            if (chkBatchAdd.Checked)
                            {
                                ListViewItem LVI = listView3.Items.Insert(insertadress, DDName);
                                LVI.SubItems.Add("第" + tBStart.Text + "周-" + "第" + tBEnd.Text + "周" + " " + strInput.Substring(strInput.IndexOf(" ") + 1, strInput.IndexOf((',')) - strInput.IndexOf(" ") - 1) + " " + strInput.Substring(strInput.IndexOf(',') + 1));
                            }
                            else
                            {
                                ListViewItem LVI = listView3.Items.Insert(insertadress, DDName);
                                LVI.SubItems.Add("第" + tBWeek.Text + "周" + " " + strInput.Substring(strInput.IndexOf(" ") + 1, strInput.IndexOf((',')) - strInput.IndexOf(" ") - 1) + " " + strInput.Substring(strInput.IndexOf(',') + 1));
                            }
                            break;
                        }
                        else if ((int.Parse(obj.ToString().Substring(0, obj.ToString().IndexOf('_'))) > int.Parse(strInput.Substring(0, strInput.IndexOf('_')))))
                        {
                            int insertadress = A1.IndexOf(obj);
                            A1.Insert(insertadress, strInput);
                            if (chkBatchAdd.Checked)
                            {
                                ListViewItem LVI = listView3.Items.Insert(insertadress, DDName);
                                LVI.SubItems.Add("第" + tBStart.Text + "周-" + "第" + tBEnd.Text + "周" + " " + strInput.Substring(strInput.IndexOf(" ") + 1, strInput.IndexOf((',')) - strInput.IndexOf(" ") - 1) + " " + strInput.Substring(strInput.IndexOf(',') + 1));
                            }
                            else
                            {
                                ListViewItem LVI = listView3.Items.Insert(insertadress, DDName);
                                LVI.SubItems.Add("第" + tBWeek.Text + "周" + " " + strInput.Substring(strInput.IndexOf(" ") + 1, strInput.IndexOf((',')) - strInput.IndexOf(" ") - 1) + " " + strInput.Substring(strInput.IndexOf(',') + 1));
                            }
                            break;
                        }
                    }
                    if (A1.Count == intA1ct)
                    {
                        A1.Add(strInput);
                        if (chkBatchAdd.Checked)
                        {
                            ListViewItem LVI = listView3.Items.Add(DDName);
                            LVI.SubItems.Add("第" + tBStart.Text + "周-" + "第" + tBEnd.Text + "周" + " " + strInput.Substring(strInput.IndexOf(" ") + 1, strInput.IndexOf((',')) - strInput.IndexOf(" ") - 1) + " " + strInput.Substring(strInput.IndexOf(',') + 1));
                        }
                        else
                        {
                            ListViewItem LVI = listView3.Items.Add(DDName);
                            LVI.SubItems.Add("第" + tBWeek.Text + "周" + " " + strInput.Substring(strInput.IndexOf(" ") + 1, strInput.IndexOf((',')) - strInput.IndexOf(" ") - 1) + " " + strInput.Substring(strInput.IndexOf(',') + 1));
                        }
                    }
                }
           

            }
            else if (A1.Count < 1)
            {
            }
            else
            {
                int i2 = A1.IndexOf(strInput);
                A1.Remove(A1[i2]);
                listView3.Items[i2].Remove();
            }
        }

        private void listView3_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            foreach (ListViewItem s in listView3.Items)
            {
                if (listView3.CheckedItems.Contains(s))
                {
                    s.Selected = true;
                }
                else
                {
                    s.Selected = false;
                }

            }
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem s in listView3.Items)
            {
                if (listView3.SelectedItems.Contains(s))
                {
                    s.Checked = true;
                }
                else
                {
                    s.Checked = false;
                }

            }
        }

        private void chkBatchAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBatchAdd.Checked == true)
            {
                groupBox2.Enabled = true;
                tBWeek.Enabled = false;
                lblWeek.Enabled = false;
                btnLastWeek.Enabled = false;
                btnNextWeek.Enabled = false;
                listView3.Items.Clear();
                for (int i = 0; i < A1.Count; i++)
                {
                    ListViewItem LVI = listView3.Items.Add(DDName);
                    LVI.SubItems.Add("第" + tBStart.Text + "周-" + "第" + tBEnd.Text + "周" + " " + A1[i].ToString().Substring(A1[i].ToString().IndexOf(" ") + 1, A1[i].ToString().IndexOf((',')) - A1[i].ToString().IndexOf(" ") - 1) + " " + A1[i].ToString().Substring(A1[i].ToString().IndexOf(',') + 1));
                }
            }
            else
            {
                groupBox2.Enabled = false;
                tBWeek.Enabled = true;
                lblWeek.Enabled = true;
                btnLastWeek.Enabled = true;
                btnNextWeek.Enabled = true;
                listView3.Items.Clear();
                for (int i = 0; i < A1.Count; i++)
                {
                    ListViewItem LVI = listView3.Items.Add(DDName);
                    LVI.SubItems.Add("第" + tBWeek.Text + "周" + " " + A1[i].ToString().Substring(A1[i].ToString().IndexOf(" ") + 1, A1[i].ToString().IndexOf((',')) - A1[i].ToString().IndexOf(" ") - 1) + " " + A1[i].ToString().Substring(A1[i].ToString().IndexOf(',') + 1));
                }
            }
        }


        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            A1.Clear();
            foreach (Control Cl in panelFree.Controls)
            {
                if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                {
                    CheckBox CB = new CheckBox();
                    CB = (CheckBox)Cl;
                    CB.Checked = false;
                }
            }
        }

        private void btnDeleteSelect_Click(object sender, EventArgs e)
        {
            while (listView3.CheckedIndices.Count > 0)
            {
                int i = listView3.CheckedIndices[0];
                string S11 = A1[i].ToString().Substring(0, A1[i].ToString().IndexOf(" "));
                foreach (Control Cl in panelFree.Controls)
                {
                    if (Cl is CheckBox && Cl.Name == "checkBox" + S11)
                    {
                        CheckBox CB = new CheckBox();
                        CB = (CheckBox)Cl;
                        CB.Checked = false;
                    }
                }
            }
        }

        private void tBWeek_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
            if (tBWeek.Text.Length == 2)
            {
                e.Handled = true;
            }
            if (tBWeek.Text.Length < 1 && e.KeyChar == '0')
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void tBWeek_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox TB = sender as TextBox;
            try
            {
                if (int.Parse(TB.Text) < 1)
                {
                    TB.Text = "1";
                    TB.SelectAll();
                }
                if (int.Parse(TB.Text) > 20)
                {
                    TB.Text = "20";
                    TB.SelectAll();
                }
            }
            catch
            {

            }
        }

        private void tBWeek_TextChanged(object sender, EventArgs e)
        {
            if (tBWeek.Text == "1")
            {
                btnLastWeek.Enabled = false;
                btnNextWeek.Enabled = true;
            }
            else if (tBWeek.Text == "20")
            {
                btnLastWeek.Enabled = false;
                btnNextWeek.Enabled = true;
            }
            else
            {
                btnLastWeek.Enabled = true;
                btnNextWeek.Enabled = true;
            }
            listView3.Items.Clear();
            for (int i = 0; i < A1.Count; i++)
            {
                ListViewItem LVI = listView3.Items.Add(DDName);
                LVI.SubItems.Add("第" + tBWeek.Text + "周" + " " + A1[i].ToString().Substring(A1[i].ToString().IndexOf(" ") + 1, A1[i].ToString().IndexOf((',')) - A1[i].ToString().IndexOf(" ") - 1) + " " + A1[i].ToString().Substring(A1[i].ToString().IndexOf(',') + 1));
            }
        }

        private void tBEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox TB = sender as TextBox;
            if (TB.Text.Count() >= 2)
            {
                if (TB.SelectedText.Length == 0)
                    e.Handled = true;
            }
            if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void tBEnd_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox TB = sender as TextBox;
            try
            {
                if (int.Parse(TB.Text) < 1)
                {
                    TB.Text = "1";
                    TB.SelectAll();
                }
                if (int.Parse(TB.Text) > 20)
                {
                    TB.Text = "20";
                    TB.SelectAll();
                }
            }
            catch
            {

            }
        }

        private void tBEnd_TextChanged(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            for (int i = 0; i < A1.Count; i++)
            {
                ListViewItem LVI = listView3.Items.Add(DDName);
                LVI.SubItems.Add("第" + tBStart.Text + "周-" + "第" + tBEnd.Text + "周" + " " + A1[i].ToString().Substring(A1[i].ToString().IndexOf(" ") + 1, A1[i].ToString().IndexOf((',')) - A1[i].ToString().IndexOf(" ") - 1) + " " + A1[i].ToString().Substring(A1[i].ToString().IndexOf(',') + 1));
            }
        }

        private void btnLastWeek_Click(object sender, EventArgs e)
        {
            if (iyanzheng == 0)
            {
                DialogResult DR = MessageBox.Show("本周空课表还未保存,是否保存?", "提醒", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DR == DialogResult.Yes)
                {
                    btnsave_Click(sender, e);
                }
            }
            if (tBWeek.Text == "0" || tBWeek.Text == "" || tBWeek.Text == "00" || tBWeek.Text == "1" || tBWeek.Text == "01")
            {
                tBWeek.Text = "1";
                tBWeek.SelectAll();
                return;
            }
            tBWeek.Text = (int.Parse(tBWeek.Text) - 1).ToString();
            sAWeek = select(DDName, tBWeek.Text);
            for (int i = 0; i < 7; i++)
            {
                aAWeek[i] = new ArrayList();
                if (sAWeek[i] != null) aAWeek[i] = Ctoclass(sAWeek[i]);
            }
            foreach (Control Cl in panelFree.Controls)
            {
                if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                {
                    CheckBox CB = (CheckBox)Cl;
                    if (CB.ThreeState == false)
                    {
                        int iday = int.Parse(Cl.Name.Substring(8, 1));
                        int inum = int.Parse(Cl.Name.Substring(10));
                        if (aAWeek[iday - 1].Contains(inum.ToString()))
                        {

                            CB.Checked = true;

                        }
                    }
                }
            }
        }
        private string[] select(string Name,string Week)
        {
            string[] sArrayWeek = new string[7];

            DataTable dt = help.getDs("select * from " + "SpareTime_Data" + " where " + "Supervisor='" + Name + "'and Spare_Week=" + Week,"SpareTime_Data").Tables[0];
            if (dt.Rows.Count > 0)
            {
                int idtcount = dt.Rows.Count;
                for (int i = 0; i < idtcount; i++)
                {
                    int iweekday = int.Parse(dt.Rows[i]["Spare_Day"].ToString());
                    sArrayWeek[iweekday - 1] += dt.Rows[i]["Spare_Number"].ToString();
                }
            }
           
            return sArrayWeek;
        }
        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            if (iyanzheng == 0)
            {
                DialogResult DR = MessageBox.Show("本周空课表还未保存,是否保存?", "提醒", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DR == DialogResult.Yes)
                {
                    btnsave_Click(sender, e);
                }
            }
            if (tBWeek.Text == "0" || tBWeek.Text == "" || tBWeek.Text == "00")
            {
                tBWeek.Text = "1";
                tBWeek.SelectAll();
                return;
            }
            tBWeek.Text = (int.Parse(tBWeek.Text) + 1).ToString();
            sAWeek = select(DDName, tBWeek.Text);
            for (int i = 0; i < 7; i++)
            {
                aAWeek[i] = new ArrayList();
                if (sAWeek[i] != null) aAWeek[i] = Ctoclass(sAWeek[i]);
            }
            foreach (Control Cl in panelFree.Controls)
            {
                if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                {
                    CheckBox CB = (CheckBox)Cl;
                    if (CB.ThreeState == false)
                    {
                        int iday = int.Parse(Cl.Name.Substring(8, 1));
                        int inum = int.Parse(Cl.Name.Substring(10));
                        if (aAWeek[iday - 1].Contains(inum.ToString()))
                        {

                            CB.Checked = true;

                        }
                    }
                }
            }
        }

        private void checkBox0_Click(object sender, EventArgs e)
        {
            CheckBox CB = sender as CheckBox;
            int iday0 = int.Parse(CB.Name.Substring(8, 1));
            if(CB.CheckState== CheckState.Checked)
            {
                CB.Checked = false;
                foreach (Control Cl in panelFree.Controls)
                {
                    if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                    {
                        CheckBox CB1 = (CheckBox)Cl;
                        int iday = int.Parse(Cl.Name.Substring(8, 1));
                        if (CB1.ThreeState == false && iday == iday0)
                        {
                            int inum = int.Parse(Cl.Name.Substring(10));
                            CB1.Checked = false;
                        }
                    }
                }
            }
            else
            {
                CB.Checked = true;
                foreach (Control Cl in panelFree.Controls)
                {
                    if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                    {
                        CheckBox CB1 = (CheckBox)Cl;
                        int iday = int.Parse(Cl.Name.Substring(8, 1));
                        if (CB1.ThreeState == false && iday == iday0)
                        {
                            int inum = int.Parse(Cl.Name.Substring(10));
                            CB1.Checked = true;
                        }
                    }
                }
            }
        }

        private void checkBox0_Click_1(object sender, EventArgs e)
        {
            if(checkBox0.CheckState== CheckState.Checked)
            {
                checkBox0.CheckState = CheckState.Unchecked;
                foreach (Control Cl in panelFree.Controls)
                {
                    if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                    {
                        CheckBox CB1 = (CheckBox)Cl;
                        int iday = int.Parse(Cl.Name.Substring(8, 1));
                        if (CB1.ThreeState == false)
                        {
                            int inum = int.Parse(Cl.Name.Substring(10));
                            CB1.Checked = false;
                        }
                    }
                }
            }
            else
            {
                checkBox0.CheckState = CheckState.Checked;
                foreach (Control Cl in panelFree.Controls)
                {
                    if (Cl is CheckBox && Cl.Name != "chkBatchAdd")
                    {
                        CheckBox CB1 = (CheckBox)Cl;
                        int iday = int.Parse(Cl.Name.Substring(8, 1));
                        if (CB1.ThreeState == false)
                        {
                            int inum = int.Parse(Cl.Name.Substring(10));
                            CB1.Checked = true;
                        }
                    }
                }
            }
        }

    }
}
