namespace SAS.Forms
{
    partial class frmPlanSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cbname = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbclass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbspcial = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Font = new System.Drawing.Font("宋体", 10F);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(2, 74);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(696, 254);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "教师";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "课程";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "专业";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 150;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(621, 26);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 3;
            this.buttonX1.Text = "导出";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(10, 26);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(65, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "教师姓名";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(186, 26);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(33, 23);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "课程";
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(345, 26);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(36, 23);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "专业";
            // 
            // cbname
            // 
            this.cbname.DisplayMember = "Text";
            this.cbname.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbname.FormattingEnabled = true;
            this.cbname.ItemHeight = 15;
            this.cbname.Location = new System.Drawing.Point(81, 26);
            this.cbname.Name = "cbname";
            this.cbname.Size = new System.Drawing.Size(99, 21);
            this.cbname.TabIndex = 98;
            this.cbname.SelectedIndexChanged += new System.EventHandler(this.comboBox7_SelectedIndexChanged);
            // 
            // cbclass
            // 
            this.cbclass.DisplayMember = "Text";
            this.cbclass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbclass.FormattingEnabled = true;
            this.cbclass.ItemHeight = 15;
            this.cbclass.Location = new System.Drawing.Point(225, 26);
            this.cbclass.Name = "cbclass";
            this.cbclass.Size = new System.Drawing.Size(114, 21);
            this.cbclass.TabIndex = 99;
            // 
            // cbspcial
            // 
            this.cbspcial.DisplayMember = "Text";
            this.cbspcial.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbspcial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbspcial.FormattingEnabled = true;
            this.cbspcial.ItemHeight = 15;
            this.cbspcial.Location = new System.Drawing.Point(387, 26);
            this.cbspcial.Name = "cbspcial";
            this.cbspcial.Size = new System.Drawing.Size(135, 21);
            this.cbspcial.TabIndex = 100;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonX2);
            this.groupBox1.Controls.Add(this.cbclass);
            this.groupBox1.Controls.Add(this.cbspcial);
            this.groupBox1.Controls.Add(this.buttonX1);
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Controls.Add(this.cbname);
            this.groupBox1.Controls.Add(this.labelX3);
            this.groupBox1.Controls.Add(this.labelX4);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(702, 55);
            this.groupBox1.TabIndex = 101;
            this.groupBox1.TabStop = false;
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(540, 26);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.TabIndex = 101;
            this.buttonX2.Text = "数据预览";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // frmPlanSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 340);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.DoubleBuffered = true;
            this.Name = "frmPlanSearch";
            this.ShowIcon = false;
            this.Text = "frmPlanSearch";
            this.Load += new System.EventHandler(this.frmPlanSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbname;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbclass;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbspcial;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
    }
}