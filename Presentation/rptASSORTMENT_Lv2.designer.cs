
namespace Report_Center.Presentation
{
    partial class rptASSORTMENT_Lv2
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.theo_ln = new System.Windows.Forms.RadioButton();
            this.theo_dt = new System.Windows.Forms.RadioButton();
            this.theo_slb = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.check_minimart_sg = new System.Windows.Forms.RadioButton();
            this.check_minimart_hn = new System.Windows.Forms.RadioButton();
            this.Exp2Excl = new System.Windows.Forms.Button();
            this.check_all = new System.Windows.Forms.RadioButton();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.frdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.check_minimart = new System.Windows.Forms.RadioButton();
            this.check_mart = new System.Windows.Forms.RadioButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Grp_id = new System.Windows.Forms.TextBox();
            this.Stk_Id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.export_full = new System.Windows.Forms.Button();
            this.tk_Full = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView_full = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_full)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.check_minimart_sg);
            this.panel1.Controls.Add(this.check_minimart_hn);
            this.panel1.Controls.Add(this.Exp2Excl);
            this.panel1.Controls.Add(this.check_all);
            this.panel1.Controls.Add(this.todate);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.frdate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.check_minimart);
            this.panel1.Controls.Add(this.check_mart);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Grp_id);
            this.panel1.Controls.Add(this.Stk_Id);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Exit);
            this.panel1.Controls.Add(this.export_full);
            this.panel1.Controls.Add(this.tk_Full);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1380, 136);
            this.panel1.TabIndex = 35;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.theo_ln);
            this.groupBox1.Controls.Add(this.theo_dt);
            this.groupBox1.Controls.Add(this.theo_slb);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(674, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 117);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sắp Xếp";
            // 
            // theo_ln
            // 
            this.theo_ln.AutoSize = true;
            this.theo_ln.Location = new System.Drawing.Point(32, 86);
            this.theo_ln.Name = "theo_ln";
            this.theo_ln.Size = new System.Drawing.Size(140, 24);
            this.theo_ln.TabIndex = 2;
            this.theo_ln.Text = "theo Lợi nhuận";
            this.theo_ln.UseVisualStyleBackColor = true;
            // 
            // theo_dt
            // 
            this.theo_dt.AutoSize = true;
            this.theo_dt.Location = new System.Drawing.Point(32, 56);
            this.theo_dt.Name = "theo_dt";
            this.theo_dt.Size = new System.Drawing.Size(149, 24);
            this.theo_dt.TabIndex = 1;
            this.theo_dt.Text = "theo Doanh Thu";
            this.theo_dt.UseVisualStyleBackColor = true;
            // 
            // theo_slb
            // 
            this.theo_slb.AutoSize = true;
            this.theo_slb.Checked = true;
            this.theo_slb.Location = new System.Drawing.Point(32, 26);
            this.theo_slb.Name = "theo_slb";
            this.theo_slb.Size = new System.Drawing.Size(164, 24);
            this.theo_slb.TabIndex = 0;
            this.theo_slb.TabStop = true;
            this.theo_slb.Text = "theo Số lượng bán";
            this.theo_slb.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(266, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(402, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "* Chú ý: Điều kiện tạo BC có dạng : 10011,10021,10031";
            // 
            // check_minimart_sg
            // 
            this.check_minimart_sg.AutoSize = true;
            this.check_minimart_sg.Location = new System.Drawing.Point(492, 39);
            this.check_minimart_sg.Name = "check_minimart_sg";
            this.check_minimart_sg.Size = new System.Drawing.Size(123, 24);
            this.check_minimart_sg.TabIndex = 21;
            this.check_minimart_sg.Text = "MiniMart-SG";
            this.check_minimart_sg.UseVisualStyleBackColor = true;
            this.check_minimart_sg.CheckedChanged += new System.EventHandler(this.check_minimart_sg_CheckedChanged);
            // 
            // check_minimart_hn
            // 
            this.check_minimart_hn.AutoSize = true;
            this.check_minimart_hn.Location = new System.Drawing.Point(354, 39);
            this.check_minimart_hn.Name = "check_minimart_hn";
            this.check_minimart_hn.Size = new System.Drawing.Size(122, 24);
            this.check_minimart_hn.TabIndex = 20;
            this.check_minimart_hn.Text = "MiniMart-HN";
            this.check_minimart_hn.UseVisualStyleBackColor = true;
            this.check_minimart_hn.CheckedChanged += new System.EventHandler(this.check_minimart_hn_CheckedChanged);
            // 
            // Exp2Excl
            // 
            this.Exp2Excl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exp2Excl.Location = new System.Drawing.Point(1154, 19);
            this.Exp2Excl.Name = "Exp2Excl";
            this.Exp2Excl.Size = new System.Drawing.Size(96, 38);
            this.Exp2Excl.TabIndex = 19;
            this.Exp2Excl.Text = "Export";
            this.Exp2Excl.UseVisualStyleBackColor = true;
            this.Exp2Excl.Click += new System.EventHandler(this.Exp2Excl_Click);
            // 
            // check_all
            // 
            this.check_all.AutoSize = true;
            this.check_all.Checked = true;
            this.check_all.Location = new System.Drawing.Point(24, 39);
            this.check_all.Name = "check_all";
            this.check_all.Size = new System.Drawing.Size(122, 24);
            this.check_all.TabIndex = 18;
            this.check_all.TabStop = true;
            this.check_all.Text = "All / Options ";
            this.check_all.UseVisualStyleBackColor = true;
            this.check_all.CheckedChanged += new System.EventHandler(this.check_all_CheckedChanged);
            // 
            // todate
            // 
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.todate.Location = new System.Drawing.Point(406, 8);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(142, 26);
            this.todate.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(979, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 22);
            this.label6.TabIndex = 16;
            this.label6.Text = " 0 : 0 : 0 : 0";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // frdate
            // 
            this.frdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.frdate.Location = new System.Drawing.Point(93, 8);
            this.frdate.Name = "frdate";
            this.frdate.Size = new System.Drawing.Size(142, 26);
            this.frdate.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(305, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Đến ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(15, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Từ ngày";
            // 
            // check_minimart
            // 
            this.check_minimart.AutoSize = true;
            this.check_minimart.Location = new System.Drawing.Point(244, 39);
            this.check_minimart.Name = "check_minimart";
            this.check_minimart.Size = new System.Drawing.Size(94, 24);
            this.check_minimart.TabIndex = 11;
            this.check_minimart.Text = "MiniMart";
            this.check_minimart.UseVisualStyleBackColor = true;
            this.check_minimart.CheckedChanged += new System.EventHandler(this.check_minimart_CheckedChanged);
            // 
            // check_mart
            // 
            this.check_mart.AutoSize = true;
            this.check_mart.Location = new System.Drawing.Point(162, 39);
            this.check_mart.Name = "check_mart";
            this.check_mart.Size = new System.Drawing.Size(66, 24);
            this.check_mart.TabIndex = 10;
            this.check_mart.Text = "Mart";
            this.check_mart.UseVisualStyleBackColor = true;
            this.check_mart.CheckedChanged += new System.EventHandler(this.check_mart_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(979, 94);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(385, 29);
            this.progressBar1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(938, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 38);
            this.button2.TabIndex = 9;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Blue;
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(1235, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "Export Data";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Grp_id
            // 
            this.Grp_id.Location = new System.Drawing.Point(86, 101);
            this.Grp_id.Name = "Grp_id";
            this.Grp_id.Size = new System.Drawing.Size(162, 26);
            this.Grp_id.TabIndex = 1;
            this.Grp_id.Text = "*";
            // 
            // Stk_Id
            // 
            this.Stk_Id.ForeColor = System.Drawing.Color.DarkOrange;
            this.Stk_Id.Location = new System.Drawing.Point(20, 70);
            this.Stk_Id.Name = "Stk_Id";
            this.Stk_Id.Size = new System.Drawing.Size(598, 26);
            this.Stk_Id.TabIndex = 0;
            this.Stk_Id.Text = "*";
            this.Stk_Id.TextChanged += new System.EventHandler(this.Ma_NCC_TextChanged);
            this.Stk_Id.DoubleClick += new System.EventHandler(this.Stk_Id_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(15, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhóm";
            // 
            // Exit
            // 
            this.Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit.Location = new System.Drawing.Point(1270, 19);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(96, 38);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // export_full
            // 
            this.export_full.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.export_full.BackColor = System.Drawing.Color.Red;
            this.export_full.Location = new System.Drawing.Point(1116, 62);
            this.export_full.Name = "export_full";
            this.export_full.Size = new System.Drawing.Size(118, 31);
            this.export_full.TabIndex = 4;
            this.export_full.Text = "Export 1";
            this.export_full.UseVisualStyleBackColor = false;
            this.export_full.Click += new System.EventHandler(this.export_full_Click);
            // 
            // tk_Full
            // 
            this.tk_Full.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tk_Full.Location = new System.Drawing.Point(1040, 19);
            this.tk_Full.Name = "tk_Full";
            this.tk_Full.Size = new System.Drawing.Size(96, 38);
            this.tk_Full.TabIndex = 3;
            this.tk_Full.Text = "Báo Cáo";
            this.tk_Full.UseVisualStyleBackColor = true;
            this.tk_Full.Click += new System.EventHandler(this.tk_Full_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.dataGridView_full);
            this.panel3.Location = new System.Drawing.Point(12, 150);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1358, 393);
            this.panel3.TabIndex = 37;
            // 
            // dataGridView_full
            // 
            this.dataGridView_full.AllowUserToAddRows = false;
            this.dataGridView_full.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView_full.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_full.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_full.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_full.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_full.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_full.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_full.Name = "dataGridView_full";
            this.dataGridView_full.ReadOnly = true;
            this.dataGridView_full.RowHeadersWidth = 62;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView_full.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_full.RowTemplate.Height = 28;
            this.dataGridView_full.Size = new System.Drawing.Size(1358, 393);
            this.dataGridView_full.TabIndex = 0;
            this.dataGridView_full.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView_full.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_full_CellContentClick_2);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rptASSORTMENT_Lv2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 555);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "rptASSORTMENT_Lv2";
            this.Text = "AsSortMent";
            this.Load += new System.EventHandler(this.rptASSORTMENT_Lv2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_full)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button export_full;
        private System.Windows.Forms.Button tk_Full;
        private System.Windows.Forms.TextBox Grp_id;
        private System.Windows.Forms.TextBox Stk_Id;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView_full;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RadioButton check_minimart;
        private System.Windows.Forms.RadioButton check_mart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton check_all;
        private System.Windows.Forms.DateTimePicker todate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker frdate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Exp2Excl;
        private System.Windows.Forms.RadioButton check_minimart_sg;
        private System.Windows.Forms.RadioButton check_minimart_hn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton theo_ln;
        private System.Windows.Forms.RadioButton theo_dt;
        private System.Windows.Forms.RadioButton theo_slb;
    }
}