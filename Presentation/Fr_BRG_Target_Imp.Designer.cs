namespace Report_Center.Presentation
{
    partial class Fr_BRG_Target_Imp
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
            this.import_data = new System.Windows.Forms.Button();
            this.Exp_data = new System.Windows.Forms.Button();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.exp_target_BRG = new System.Windows.Forms.Button();
            this.MONTH_Target_BRG = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // import_data
            // 
            this.import_data.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.import_data.Location = new System.Drawing.Point(92, 55);
            this.import_data.Margin = new System.Windows.Forms.Padding(2);
            this.import_data.Name = "import_data";
            this.import_data.Size = new System.Drawing.Size(225, 48);
            this.import_data.TabIndex = 2;
            this.import_data.Text = "Import";
            this.import_data.UseVisualStyleBackColor = true;
            this.import_data.Click += new System.EventHandler(this.import_data_Click);
            // 
            // Exp_data
            // 
            this.Exp_data.Location = new System.Drawing.Point(460, 62);
            this.Exp_data.Margin = new System.Windows.Forms.Padding(2);
            this.Exp_data.Name = "Exp_data";
            this.Exp_data.Size = new System.Drawing.Size(121, 35);
            this.Exp_data.TabIndex = 3;
            this.Exp_data.Text = "&Xuất BC";
            this.Exp_data.UseVisualStyleBackColor = true;
            this.Exp_data.Visible = false;
            this.Exp_data.Click += new System.EventHandler(this.Exp_data_Click);
            // 
            // bt_Exit
            // 
            this.bt_Exit.Location = new System.Drawing.Point(640, 62);
            this.bt_Exit.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(121, 35);
            this.bt_Exit.TabIndex = 5;
            this.bt_Exit.Text = "&Exit";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(419, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Lùi ngày";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(113, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "Link down Template (nếu cần)";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 123);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(408, 23);
            this.progressBar1.TabIndex = 27;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(481, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 28;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // exp_target_BRG
            // 
            this.exp_target_BRG.Location = new System.Drawing.Point(616, 116);
            this.exp_target_BRG.Margin = new System.Windows.Forms.Padding(2);
            this.exp_target_BRG.Name = "exp_target_BRG";
            this.exp_target_BRG.Size = new System.Drawing.Size(121, 32);
            this.exp_target_BRG.TabIndex = 29;
            this.exp_target_BRG.Text = "Xuất &Target BRG";
            this.exp_target_BRG.UseVisualStyleBackColor = true;
            this.exp_target_BRG.Click += new System.EventHandler(this.exp_target_BRG_Click);
            // 
            // MONTH_Target_BRG
            // 
            this.MONTH_Target_BRG.CustomFormat = "MM/yyyy";
            this.MONTH_Target_BRG.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.MONTH_Target_BRG.Location = new System.Drawing.Point(501, 120);
            this.MONTH_Target_BRG.Name = "MONTH_Target_BRG";
            this.MONTH_Target_BRG.Size = new System.Drawing.Size(79, 20);
            this.MONTH_Target_BRG.TabIndex = 30;
            // 
            // Fr_BRG_Target_Imp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 159);
            this.Controls.Add(this.MONTH_Target_BRG);
            this.Controls.Add(this.exp_target_BRG);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.Exp_data);
            this.Controls.Add(this.import_data);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Fr_BRG_Target_Imp";
            this.Text = "Cập nhật Target BRG";
            this.Load += new System.EventHandler(this.Fr_BRG_Target_Imp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button import_data;
        private System.Windows.Forms.Button Exp_data;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button exp_target_BRG;
        private System.Windows.Forms.DateTimePicker MONTH_Target_BRG;
    }
}