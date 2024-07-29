
namespace Report_Center.Presentation
{
    partial class rpt_Sku_UseByDays
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
            this.New_Export = new System.Windows.Forms.Button();
            this.Show_all = new System.Windows.Forms.Button();
            this.btm_Fillter = new System.Windows.Forms.Button();
            this.txt_Fillter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Exit = new System.Windows.Forms.Button();
            this.tk_Full = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView_full = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Details = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_full)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Details.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.New_Export);
            this.panel1.Controls.Add(this.Show_all);
            this.panel1.Controls.Add(this.btm_Fillter);
            this.panel1.Controls.Add(this.txt_Fillter);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.Exit);
            this.panel1.Controls.Add(this.tk_Full);
            this.panel1.Location = new System.Drawing.Point(12, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1247, 114);
            this.panel1.TabIndex = 35;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // New_Export
            // 
            this.New_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.New_Export.Location = new System.Drawing.Point(852, 14);
            this.New_Export.Name = "New_Export";
            this.New_Export.Size = new System.Drawing.Size(126, 43);
            this.New_Export.TabIndex = 27;
            this.New_Export.Text = "New Export";
            this.New_Export.UseVisualStyleBackColor = true;
            this.New_Export.Click += new System.EventHandler(this.New_Export_Click);
            // 
            // Show_all
            // 
            this.Show_all.BackColor = System.Drawing.Color.PeachPuff;
            this.Show_all.Location = new System.Drawing.Point(479, 19);
            this.Show_all.Name = "Show_all";
            this.Show_all.Size = new System.Drawing.Size(92, 46);
            this.Show_all.TabIndex = 25;
            this.Show_all.Text = "Show All";
            this.Show_all.UseVisualStyleBackColor = false;
            this.Show_all.Click += new System.EventHandler(this.Show_all_Click);
            // 
            // btm_Fillter
            // 
            this.btm_Fillter.BackColor = System.Drawing.Color.LightYellow;
            this.btm_Fillter.Location = new System.Drawing.Point(380, 19);
            this.btm_Fillter.Name = "btm_Fillter";
            this.btm_Fillter.Size = new System.Drawing.Size(86, 46);
            this.btm_Fillter.TabIndex = 24;
            this.btm_Fillter.Text = "Fillter";
            this.btm_Fillter.UseVisualStyleBackColor = false;
            this.btm_Fillter.Click += new System.EventHandler(this.btm_Fillter_Click);
            // 
            // txt_Fillter
            // 
            this.txt_Fillter.Location = new System.Drawing.Point(17, 21);
            this.txt_Fillter.Multiline = true;
            this.txt_Fillter.Name = "txt_Fillter";
            this.txt_Fillter.Size = new System.Drawing.Size(357, 49);
            this.txt_Fillter.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(1120, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 22);
            this.label6.TabIndex = 16;
            this.label6.Text = " 0 : 0 : 0 : 0";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(644, 68);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(571, 25);
            this.progressBar1.TabIndex = 1;
            // 
            // Exit
            // 
            this.Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit.Location = new System.Drawing.Point(985, 14);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(103, 43);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // tk_Full
            // 
            this.tk_Full.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tk_Full.Location = new System.Drawing.Point(743, 14);
            this.tk_Full.Name = "tk_Full";
            this.tk_Full.Size = new System.Drawing.Size(103, 43);
            this.tk_Full.TabIndex = 3;
            this.tk_Full.Text = "Báo Cáo";
            this.tk_Full.UseVisualStyleBackColor = true;
            this.tk_Full.Click += new System.EventHandler(this.tk_Full_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView_full
            // 
            this.dataGridView_full.AllowUserToAddRows = false;
            this.dataGridView_full.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView_full.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_full.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Firebrick;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_full.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_full.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_full.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_full.EnableHeadersVisualStyles = false;
            this.dataGridView_full.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_full.Name = "dataGridView_full";
            this.dataGridView_full.ReadOnly = true;
            this.dataGridView_full.RowHeadersWidth = 62;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView_full.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_full.RowTemplate.Height = 28;
            this.dataGridView_full.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_full.Size = new System.Drawing.Size(1233, 376);
            this.dataGridView_full.TabIndex = 0;
            this.dataGridView_full.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView_full.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_full_CellContentClick_2);
            this.dataGridView_full.ColumnSortModeChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView_full_ColumnSortModeChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Details);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 128);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1247, 415);
            this.tabControl1.TabIndex = 36;
            // 
            // Details
            // 
            this.Details.Controls.Add(this.dataGridView_full);
            this.Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Details.ForeColor = System.Drawing.Color.Black;
            this.Details.Location = new System.Drawing.Point(4, 29);
            this.Details.Name = "Details";
            this.Details.Padding = new System.Windows.Forms.Padding(3);
            this.Details.Size = new System.Drawing.Size(1239, 382);
            this.Details.TabIndex = 0;
            this.Details.Text = "Details";
            this.Details.UseVisualStyleBackColor = true;
            // 
            // rpt_Sku_UseByDays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 555);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "rpt_Sku_UseByDays";
            this.Text = "Use By Days";
            this.Load += new System.EventHandler(this.rpt_Sku_UseByDays_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_full)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.Details.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button tk_Full;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btm_Fillter;
        private System.Windows.Forms.TextBox txt_Fillter;
        private System.Windows.Forms.Button Show_all;
        private System.Windows.Forms.DataGridView dataGridView_full;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Details;
        private System.Windows.Forms.Button New_Export;
    }
}