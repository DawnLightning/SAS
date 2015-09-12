namespace SAS.Forms
{
    partial class frmTeacher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.tBID = new System.Windows.Forms.TextBox();
            this.tBTel = new System.Windows.Forms.TextBox();
            this.tBMail = new System.Windows.Forms.TextBox();
            this.tBName = new System.Windows.Forms.TextBox();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.cbIsDUDao = new System.Windows.Forms.CheckBox();
            this.btnAddType = new System.Windows.Forms.Button();
            this.btnDelType = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnPageUp = new System.Windows.Forms.Button();
            this.textBoxNow = new System.Windows.Forms.TextBox();
            this.labPageAll = new System.Windows.Forms.Label();
            this.labPage = new System.Windows.Forms.Label();
            this.btnPageDown = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBelongs = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tBID
            // 
            this.tBID.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.tBID.Location = new System.Drawing.Point(61, 20);
            this.tBID.MaxLength = 20;
            this.tBID.Name = "tBID";
            this.tBID.Size = new System.Drawing.Size(130, 21);
            this.tBID.TabIndex = 8;
            this.tBID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBName_KeyUp);
            // 
            // tBTel
            // 
            this.tBTel.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.tBTel.Location = new System.Drawing.Point(326, 47);
            this.tBTel.MaxLength = 11;
            this.tBTel.Name = "tBTel";
            this.tBTel.Size = new System.Drawing.Size(130, 21);
            this.tBTel.TabIndex = 12;
            this.tBTel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBName_KeyUp);
            // 
            // tBMail
            // 
            this.tBMail.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.tBMail.Location = new System.Drawing.Point(326, 74);
            this.tBMail.Name = "tBMail";
            this.tBMail.Size = new System.Drawing.Size(130, 21);
            this.tBMail.TabIndex = 13;
            this.tBMail.TextChanged += new System.EventHandler(this.tBMail_TextChanged);
      
            this.tBMail.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBName_KeyUp);
            // 
            // tBName
            // 
            this.tBName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tBName.Location = new System.Drawing.Point(61, 47);
            this.tBName.Name = "tBName";
            this.tBName.Size = new System.Drawing.Size(130, 21);
            this.tBName.TabIndex = 9;
            this.tBName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBName_KeyUp);
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.BackColor = System.Drawing.SystemColors.Control;
            this.lblTel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTel.Location = new System.Drawing.Point(243, 48);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(77, 14);
            this.lblTel.TabIndex = 183;
            this.lblTel.Text = "联系电话：";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.BackColor = System.Drawing.SystemColors.Control;
            this.lblID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblID.Location = new System.Drawing.Point(20, 21);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(35, 14);
            this.lblID.TabIndex = 181;
            this.lblID.Text = "ID：";
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.BackColor = System.Drawing.SystemColors.Control;
            this.lblMail.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMail.Location = new System.Drawing.Point(271, 75);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(49, 14);
            this.lblMail.TabIndex = 182;
            this.lblMail.Text = "邮箱：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.SystemColors.Control;
            this.lblName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(6, 48);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 14);
            this.lblName.TabIndex = 180;
            this.lblName.Text = "姓名：";
            // 
            // cbIsDUDao
            // 
            this.cbIsDUDao.AutoSize = true;
            this.cbIsDUDao.BackColor = System.Drawing.SystemColors.Control;
            this.cbIsDUDao.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbIsDUDao.Location = new System.Drawing.Point(468, 20);
            this.cbIsDUDao.Name = "cbIsDUDao";
            this.cbIsDUDao.Size = new System.Drawing.Size(82, 18);
            this.cbIsDUDao.TabIndex = 14;
            this.cbIsDUDao.Text = "是否督导";
            this.cbIsDUDao.UseVisualStyleBackColor = false;
            // 
            // btnAddType
            // 
            this.btnAddType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddType.FlatAppearance.BorderSize = 0;
            this.btnAddType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddType.Location = new System.Drawing.Point(9, 456);
            this.btnAddType.Name = "btnAddType";
            this.btnAddType.Size = new System.Drawing.Size(25, 25);
            this.btnAddType.TabIndex = 190;
            this.btnAddType.UseVisualStyleBackColor = true;
            // 
            // btnDelType
            // 
            this.btnDelType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelType.FlatAppearance.BorderSize = 0;
            this.btnDelType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelType.Location = new System.Drawing.Point(-5, 475);
            this.btnDelType.Name = "btnDelType";
            this.btnDelType.Size = new System.Drawing.Size(25, 25);
            this.btnDelType.TabIndex = 191;
            this.btnDelType.UseVisualStyleBackColor = true;
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.SystemColors.Window;
            this.btnsave.Enabled = false;
            this.btnsave.FlatAppearance.BorderSize = 0;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnsave.Location = new System.Drawing.Point(642, 70);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(68, 30);
            this.btnsave.TabIndex = 16;
            this.btnsave.Text = "保存";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 194;
            this.label2.Text = "职称：";
            // 
            // tbTitle
            // 
            this.tbTitle.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.tbTitle.Location = new System.Drawing.Point(61, 74);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(130, 21);
            this.tbTitle.TabIndex = 10;
            this.tbTitle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBName_KeyUp);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(9, 155);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(719, 316);
            this.listView1.TabIndex = 194;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "教师号";
            this.columnHeader1.Width = 106;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "教师姓名";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 75;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Email";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 128;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "phone";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 134;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "职称";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "是否为督导";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "教研室";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 120;
            // 
            // btnPageUp
            // 
            this.btnPageUp.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPageUp.BackColor = System.Drawing.SystemColors.Window;
            this.btnPageUp.FlatAppearance.BorderSize = 0;
            this.btnPageUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPageUp.Location = new System.Drawing.Point(151, 479);
            this.btnPageUp.Name = "btnPageUp";
            this.btnPageUp.Size = new System.Drawing.Size(68, 30);
            this.btnPageUp.TabIndex = 6;
            this.btnPageUp.Text = "上一页";
            this.btnPageUp.UseVisualStyleBackColor = false;
            this.btnPageUp.Click += new System.EventHandler(this.btnPageUp_Click);
            // 
            // textBoxNow
            // 
            this.textBoxNow.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxNow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNow.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxNow.Location = new System.Drawing.Point(308, 486);
            this.textBoxNow.Name = "textBoxNow";
            this.textBoxNow.Size = new System.Drawing.Size(40, 16);
            this.textBoxNow.TabIndex = 8;
            this.textBoxNow.Text = "1";
            this.textBoxNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labPageAll
            // 
            this.labPageAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labPageAll.BackColor = System.Drawing.Color.Transparent;
            this.labPageAll.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPageAll.Location = new System.Drawing.Point(413, 483);
            this.labPageAll.Name = "labPageAll";
            this.labPageAll.Size = new System.Drawing.Size(43, 21);
            this.labPageAll.TabIndex = 202;
            this.labPageAll.Text = "1";
            this.labPageAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPage
            // 
            this.labPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labPage.BackColor = System.Drawing.Color.Transparent;
            this.labPage.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPage.Location = new System.Drawing.Point(242, 483);
            this.labPage.Name = "labPage";
            this.labPage.Size = new System.Drawing.Size(246, 21);
            this.labPage.TabIndex = 199;
            this.labPage.Text = "当前是第        页   共        页";
            this.labPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPageDown
            // 
            this.btnPageDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPageDown.BackColor = System.Drawing.SystemColors.Window;
            this.btnPageDown.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPageDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPageDown.Location = new System.Drawing.Point(494, 477);
            this.btnPageDown.Name = "btnPageDown";
            this.btnPageDown.Size = new System.Drawing.Size(68, 30);
            this.btnPageDown.TabIndex = 7;
            this.btnPageDown.Text = "下一页";
            this.btnPageDown.UseVisualStyleBackColor = false;
            this.btnPageDown.Click += new System.EventHandler(this.btnPageDown_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(452, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 197;
            this.label4.Text = "教师姓名：";
            // 
            // txtSearch
            // 
            this.txtSearch.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSearch.Location = new System.Drawing.Point(535, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(113, 21);
            this.txtSearch.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(654, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(68, 24);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(257, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 204;
            this.label3.Text = "教研室：";
            // 
            // tbBelongs
            // 
            this.tbBelongs.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.tbBelongs.Location = new System.Drawing.Point(326, 20);
            this.tbBelongs.Name = "tbBelongs";
            this.tbBelongs.Size = new System.Drawing.Size(130, 21);
            this.tbBelongs.TabIndex = 11;
            this.tbBelongs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBName_KeyUp);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(12, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(68, 25);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(104, 12);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(68, 25);
            this.btnDel.TabIndex = 3;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tBName);
            this.groupBox1.Controls.Add(this.tBMail);
            this.groupBox1.Controls.Add(this.tBID);
            this.groupBox1.Controls.Add(this.tBTel);
            this.groupBox1.Controls.Add(this.lblMail);
            this.groupBox1.Controls.Add(this.btnsave);
            this.groupBox1.Controls.Add(this.lblTel);
            this.groupBox1.Controls.Add(this.lblID);
            this.groupBox1.Controls.Add(this.tbBelongs);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.cbIsDUDao);
            this.groupBox1.Controls.Add(this.tbTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(716, 106);
            this.groupBox1.TabIndex = 210;
            this.groupBox1.TabStop = false;
            // 
            // frmTeacher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 511);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnPageUp);
            this.Controls.Add(this.textBoxNow);
            this.Controls.Add(this.labPageAll);
            this.Controls.Add(this.labPage);
            this.Controls.Add(this.btnPageDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAddType);
            this.Controls.Add(this.btnDelType);
            this.MaximumSize = new System.Drawing.Size(751, 550);
            this.Name = "frmTeacher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "教师信息";
            this.Load += new System.EventHandler(this.frmTeacher_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBTel;
        private System.Windows.Forms.TextBox tBMail;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox cbIsDUDao;
        private System.Windows.Forms.Button btnAddType;
        private System.Windows.Forms.Button btnDelType;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnPageUp;
        private System.Windows.Forms.TextBox textBoxNow;
        private System.Windows.Forms.Label labPageAll;
        private System.Windows.Forms.Label labPage;
        private System.Windows.Forms.Button btnPageDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBelongs;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox tBID;
        public System.Windows.Forms.TextBox tBName;
    }
}