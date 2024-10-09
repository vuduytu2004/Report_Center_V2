
namespace Report_Center.Presentation
{
    partial class fr_StockBLK//fr_StockBLK
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Ma_stk_id = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Ma_hang = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Ma_nhom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.export_full = new System.Windows.Forms.Button();
            this.tk_Full = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView_full = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_full)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Ma_stk_id);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.Ma_hang);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Ma_nhom);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Exit);
            this.panel1.Controls.Add(this.export_full);
            this.panel1.Controls.Add(this.tk_Full);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Location = new System.Drawing.Point(8, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 31);
            this.panel1.TabIndex = 35;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(156, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nhóm";
            // 
            // Ma_stk_id
            // 
            this.Ma_stk_id.Location = new System.Drawing.Point(62, 8);
            this.Ma_stk_id.Margin = new System.Windows.Forms.Padding(2);
            this.Ma_stk_id.Name = "Ma_stk_id";
            this.Ma_stk_id.Size = new System.Drawing.Size(90, 20);
            this.Ma_stk_id.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(465, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 20);
            this.button2.TabIndex = 9;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Ma_hang
            // 
            this.Ma_hang.Location = new System.Drawing.Point(355, 7);
            this.Ma_hang.Margin = new System.Windows.Forms.Padding(2);
            this.Ma_hang.Name = "Ma_hang";
            this.Ma_hang.Size = new System.Drawing.Size(90, 20);
            this.Ma_hang.TabIndex = 2;
            this.Ma_hang.TextChanged += new System.EventHandler(this.Ma_hang_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(292, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Hàng hóa";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Ma_nhom
            // 
            this.Ma_nhom.Location = new System.Drawing.Point(198, 8);
            this.Ma_nhom.Margin = new System.Windows.Forms.Padding(2);
            this.Ma_nhom.Name = "Ma_nhom";
            this.Ma_nhom.Size = new System.Drawing.Size(90, 20);
            this.Ma_nhom.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Siêu thị";
            // 
            // Exit
            // 
            this.Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit.Location = new System.Drawing.Point(747, 6);
            this.Exit.Margin = new System.Windows.Forms.Padding(2);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(64, 20);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // export_full
            // 
            this.export_full.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.export_full.Location = new System.Drawing.Point(653, 6);
            this.export_full.Margin = new System.Windows.Forms.Padding(2);
            this.export_full.Name = "export_full";
            this.export_full.Size = new System.Drawing.Size(64, 20);
            this.export_full.TabIndex = 4;
            this.export_full.Text = "Export";
            this.export_full.UseVisualStyleBackColor = true;
            this.export_full.Click += new System.EventHandler(this.export_full_Click);
            // 
            // tk_Full
            // 
            this.tk_Full.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tk_Full.Location = new System.Drawing.Point(559, 6);
            this.tk_Full.Margin = new System.Windows.Forms.Padding(2);
            this.tk_Full.Name = "tk_Full";
            this.tk_Full.Size = new System.Drawing.Size(64, 20);
            this.tk_Full.TabIndex = 3;
            this.tk_Full.Text = "Tìm kiếm";
            this.tk_Full.UseVisualStyleBackColor = true;
            this.tk_Full.Click += new System.EventHandler(this.tk_Full_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(451, 2);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(374, 27);
            this.progressBar1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.dataGridView_full);
            this.panel3.Location = new System.Drawing.Point(8, 40);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(831, 313);
            this.panel3.TabIndex = 37;
            // 
            // dataGridView_full
            // 
            this.dataGridView_full.AllowUserToAddRows = false;
            this.dataGridView_full.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView_full.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_full.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_full.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_full.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_full.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_full.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_full.Name = "dataGridView_full";
            this.dataGridView_full.ReadOnly = true;
            this.dataGridView_full.RowHeadersWidth = 62;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.dataGridView_full.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_full.RowTemplate.Height = 28;
            this.dataGridView_full.Size = new System.Drawing.Size(831, 313);
            this.dataGridView_full.TabIndex = 0;
            this.dataGridView_full.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView_full.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_full_CellContentClick_2);
            // 
            // fr_StockBLK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 361);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fr_StockBLK";
            this.Text = "Danh sách Hàng Hóa đã bị khóa theo Kho";
            this.Load += new System.EventHandler(this.fr_StockBLK_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_full)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button export_full;
        private System.Windows.Forms.Button tk_Full;
        private System.Windows.Forms.TextBox Ma_nhom;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView_full;
        private System.Windows.Forms.TextBox Ma_hang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Ma_stk_id;
    }
}