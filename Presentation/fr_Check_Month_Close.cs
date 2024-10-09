using Report_Center.DataAccess;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using CheckBox = System.Windows.Forms.CheckBox;
using DataTable = System.Data.DataTable;
using Point = System.Drawing.Point;

namespace Report_Center.Presentation
{
    public partial class fr_Check_Month_Close : Form
    {
        public fr_Check_Month_Close()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            //Shown += new EventHandler(fr_Zone_Prices_Shown);
            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            frdate.CustomFormat = "dd/MM/yyyy";
            //frdate.Value = DateTime.Today.AddDays(-1);
        }
        ConnectDB cn = new ConnectDB();
        DateTime da;

        private int thang_tren_data;
        //public object AutoCompleteSuggestMode { get; private set; }
        //frdate.CustomFormat = "MMM-dd-yyyy";
        //frdate.Format = DateTimePickerFormat.Custom;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                SendKeys.Send("{TAB}");
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void changecorlor111(object sender, EventArgs e)
        {

            //-----------------------------------------------------

            ////-------------------------------------------------
            //foreach (DataGridViewRow row in this.List_Connected.RowCount)
            //{
            //    if ((row.Cells["TT"].Value.to / 2) == "0")
            //    //List_Connected.Rows/2==0
            //    //&& row.Cells["TagStatus"].Value.ToString() == "Lost"
            //    //|| row.Cells["TagStatus"].Value != null
            //    //&& row.Cells["TagStatus"].Value.ToString() == "Damaged"
            //    //|| row.Cells["TagStatus"].Value != null
            //    //&& row.Cells["TagStatus"].Value.ToString() == "Discarded")
            //    {
            //        row.DefaultCellStyle.BackColor = Color.LightGray;
            //        row.DefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //    }
            //    else
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Ivory;
            //    }
            //}
        }

        private void comboBox1_LostFocus(object sender, System.EventArgs e)
        {
            //string message = "Name22222: " + comboBox1.Text;
            ////message += Environment.NewLine;
            ////message += "CustomerId: " + cbCustomers.SelectedValue;
            //MessageBox.Show(message);
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            //progressBar1.Visible = false;

            //List_Connected.View = View.Details;
            //List_Connected.Columns.Add("Mã ST", 50, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Tên ST/CH", 300, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Check Online", 100, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Check Lệch ngày", 90, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Địa chỉ IP", 100, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("IT Phụ Trách", 100, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Điện Thoại", 100, HorizontalAlignment.Center);
            //List_Connected.GridLines = true;
            //List_Connected.FullRowSelect = true;

            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns[1].HeaderText = "Mã ST";
            //dataGridView1.Columns[1].Frozen = true;
            //dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns[1].Width = 50;
            //dataGridView1.Columns[2].HeaderText = "Tên ST/CH";
            //dataGridView1.Columns[2].Width = 300;
            //dataGridView1.Columns[3].HeaderText = "Check Online";
            //dataGridView1.Columns[3].Width = 100;
            //dataGridView1.Columns[4].HeaderText = "Check Lệch ngày";
            //dataGridView1.Columns[4].Width = 90;
            //dataGridView1.Columns[5].HeaderText = "Địa chỉ IP";
            //dataGridView1.Columns[5].Width = 100;
            //dataGridView1.Columns[6].HeaderText = "IT Phụ Trách";
            //dataGridView1.Columns[6].Width = 100;
            //dataGridView1.Columns[7].HeaderText = "Điện Thoại";
            //dataGridView1.Columns[7].Width = 100;

            //comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            //string sql = "SELECT SKU_ID, full_name  from SKU_DEF";
            //cn.getData(combData,sql);
            //comboBox1.AutoCompleteCustomSource = combData;
            ////comboBox1.AutoCompleteSuggestMode = AutoCompleteSuggestMode.Contains;
            ////---------------------------------------------------------------
            //comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //AutoCompleteStringCollection combData1 = new AutoCompleteStringCollection();
            //cn.getData(combData1,sql);
            //comboBox2.AutoCompleteCustomSource = combData1;
            //-------------------------------------------------------------


            //comboBox1.Text() = '';
            //comboBox1.DisplayMember = "full_name";
            //comboBox1.ValueMember = "full_name";
            //comboBox1.Refresh();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string message = "Name: " + comboBox1.Text;
            ////message += Environment.NewLine;
            ////message += "CustomerId: " + cbCustomers.SelectedValue;
            //MessageBox.Show(message);
        }
        private void comboBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //string message = "Name11111: " + comboBox1.Text;
            ////message += Environment.NewLine;
            ////message += "CustomerId: " + cbCustomers.SelectedValue;
            //MessageBox.Show(message);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            da = DateTime.Now;
            timer1.Start();
            //button1.Text = "Stop";
            // Tạo cột STT, add cột vào tb trước khi đổ dữ liệu vào table >> STT cột đầu tiên
            //DataColumn Col = new DataColumn("STT", typeof(int));
            //List_Connected.Columns.Add(Col);
            check_all.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();

            //if (MessageBox.Show =true)


            //check_not_connect.Enabled = false;

            //////////////////string sql = @"rptCheckCurrentDate_new1";
            ////////////////////dt = cn.taobang_from_Procedure(sql);
            //////////////////List_Connected.DataSource = cn.taobang_from_Procedure(sql);
            ////////////////////--------------------------------------------------------------------------------
            //////////////////sql = @"select b.STK_ID , (c.bu_desc) as 'ST/CH' 	,CONVERT(VARCHAR(10), a.CurDate, 103) as 'Check_Online'
            //////////////////         ,a.inventory as 'Lệch Ngày',c.SRV_IP,rtrim(LTRIM(RIGHT(+it_name, CHARINDEX(' ', REVERSE(+it_name))))) AS 'IT',d.IT_PHONE
            //////////////////            ,DS_TT,ds_tv,(DS_TT-ds_tv) as'Lệch DT', bills, Bills_LW,(bills- Bills_LW) as 'Lệch Bill' 
            //////////////////            FROM doanhso a
            //////////////////            inner join[172.16.70.20].dsmart12.dbo.stock as b on a.Stk_id = b.STK_ID
            //////////////////            inner join  BRG_Info as c on a.Stk_id = c.STK_ID
            //////////////////            inner join  IT_BRG_INFO as d on a.Stk_id = d.STK_ID
            //////////////////            where b.DIMENSION <> 0 and(a.CurDate) is null or a.inventory <> 0 
            //////////////////            or (DS_TT-ds_tv)is null or (bills- Bills_LW)<>0
            //////////////////         or (bills- Bills_LW)is null or (DS_TT-ds_tv)>=1000";
            //////////////////List_Not_Connect.DataSource = cn.taobang(sql);
            //////////////////changecorlor();
            ////////////////////dosomething();
            ////////////////////dgv_CellFormatting(List_Connected);


            ////////////////////List_Connected.Refresh();
            //////////////////List_Connected.Columns["DS_TT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //////////////////List_Connected.Columns["DS_TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////////////////List_Connected.Columns["ds_tv"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //////////////////List_Connected.Columns["ds_tv"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////////////////List_Connected.Columns["DS_TT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //////////////////List_Connected.Columns["DS_TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////////////////List_Connected.Columns["Lệch DT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //////////////////List_Connected.Columns["Lệch DT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////////////////List_Connected.Columns["bills"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //////////////////List_Connected.Columns["bills"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////////////////List_Connected.Columns["Bills_LW"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //////////////////List_Connected.Columns["Bills_LW"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////////////////List_Connected.Columns["Lệch Bill"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //////////////////List_Connected.Columns["Lệch Bill"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////////////////List_Connected.Columns[1].Frozen = true;
            //////////////////List_Not_Connect.Columns[1].Frozen = true;
            ////////////////////List_Not_Connect.RightToLeft = Enabled;
            //////////////////List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //////////////////List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //////////////////List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //////////////////List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            //////////////////check_not_connect.Enabled = true;
            //////////////////check_all.Enabled = true;
            //tsk.Dispose();
            //var st = tsk.Status.ToString();

            //SendKeys.Send("{ENTER}");

            //Task.Factory.StartNew(() =>
            //{
            //    MessageBox.Show("Xong rồi đấy ...");

            //});

            //progressBar1.Visible = false;
            //check_all.Enabled = false;
            ////backgroundWorker1.RunWorkerAsync();
            //DataTable ThongtinConnect;            
            //string sql = @"rptCheckCurrentDate_new1";
            //ThongtinConnect = cn.taobang_from_Procedure(sql);
            //List_Connected.Items.Clear();
            //int i = 0;
            //foreach (DataRow row in ThongtinConnect.Rows)
            //{
            //    List_Connected.Items.Add(row["STK_ID"].ToString());
            //    List_Connected.Items[i].SubItems.Add(row["ST/CH"].ToString());
            //    List_Connected.Items[i].SubItems.Add(row["Check_Online"].ToString());
            //    List_Connected.Items[i].SubItems.Add(row["Lệch Ngày"].ToString());
            //    List_Connected.Items[i].SubItems.Add(row["SRV_IP"].ToString());
            //    List_Connected.Items[i].SubItems.Add(row["IT"].ToString());
            //    List_Connected.Items[i].SubItems.Add(row["IT_PHONE"].ToString());
            //    i++;
            //}
            //check_all.Enabled = true;
        }
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //button2.Enabled = false;
            //progressBar11.Visible = true;
            //progressBar11.Style = ProgressBarStyle.Marquee;
            DataTable table = new DataTable();
            /// -----------------------------test//----------------------------------------------------------------------------------

            //----------------------------------------------------
            string sql1 = @"Check_Month_Close";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            table = cn.taobang_from_Procedure2(sql1);
            setDataSource(table);

            //--------------------------------------------------------------------------------


            changecorlor();

            //dosomething();
            //dgv_CellFormatting(List_Connected);


            //List_Connected.Refresh();
            //List_Connected.Columns["DS_TT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //List_Connected.Columns["DS_TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Connected.Columns["ds_tv"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //List_Connected.Columns["ds_tv"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Connected.Columns["DS_TT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //List_Connected.Columns["DS_TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Connected.Columns["Lệch DT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //List_Connected.Columns["Lệch DT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Connected.Columns["bills"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //List_Connected.Columns["bills"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Connected.Columns["Bills_LW"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //List_Connected.Columns["Bills_LW"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Connected.Columns["Lệch Bill"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //List_Connected.Columns["Lệch Bill"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Connected.Columns[1].Frozen = true;
            //List_Not_Connect.Columns[1].Frozen = true;
            //List_Not_Connect.RightToLeft = Enabled;
            //////List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //////List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //////List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //////List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            ////////////////check_not_connect.Enabled = true;
            ////////////////check_all.Enabled = true;
            //---------------------------------------------------------------------------
            ////////////      string sql = @"select  a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            ////////////              ,a.SKU_ID as 'Mã Hàng', c.BARCODE 
            ////////////              ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            ////////////              ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm' 
            ////////////              ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            ////////////              , c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            ////////////              ,PCPR_CODE as 'Vùng Giá' 
            ////////////              ,c.STATUS as 'Trạng Thái'
            ////////////              ,c.ITEM_TYPE as 'Loại hàng'
            ////////////              ,e.OPEN_DATE ,e.MODI_DATE 
            ////////////              from SPPRICE a
            ////////////              left join SUPPLIER as b on a.supp_id=b.supp_id
            ////////////              left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            ////////////left join GOODS as e on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            ////////////              where c.status <> '02' order by a.SKU_ID";

            ////////////      table = cn.taobang1(sql);
            ////////////      setDataSource(table);

            ////////////      for (int i = 0; i < List_Connected.Rows.Count; i++)
            ////////////      {
            ////////////          List_Connected.Rows[i].Cells[0].Value = i + 1;
            ////////////      }

            //if (progressBar11.InvokeRequired)

            //    progressBar11.Visible = false;

            //dataGridView_full.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //dataGridView_full.ColumnHeadersVisible = false;

            //this.dataGridView_full.DataSource = cn.taobang1(sql);

            //for (int i = 0; i < dataGridView_full.Rows.Count; i++)
            //{
            //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //}

            //dataGridView_full.Columns[4].Frozen = true;
            ////----------------------------------------------------------------------------------
            //// Your background task goes here
            //for (int i = 0; i <= 100; i++)
            //{
            //    // Report progress to 'UI' thread
            //    backgroundWorker1.ReportProgress(i);

            //    // Simulate long task
            //    System.Threading.Thread.Sleep(100);
            //}
        }
        void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //button2.Enabled = false;
            //progressBar11.Visible = true;
            //progressBar11.Style = ProgressBarStyle.Marquee;
            DataTable table = new DataTable();
            /// -----------------------------test//----------------------------------------------------------------------------------
            string sql1 = @"rptCheckCurrentDate_old";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            //table = cn.taobang_from_Procedure(sql1);
            table = cn.taobang_from_Procedure_Parameter(sql1, frdate);
            setDataSource1(table);

            //--------------------------------------------------------------------------------


            changecorlor();


        }

        internal delegate void SetDataSourceDelegate(DataTable table);
        private void setDataSource(DataTable table)
        {
            // Invoke method if required:
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource), table);
            }
            else
            {

                List_Connected.DataSource = null;
                List_Connected.Rows.Clear();
                List_Connected.Columns.Clear();

                //// Add check box colums
                //DataGridViewCheckBoxColumn dgvcCheckBox = new DataGridViewCheckBoxColumn();
                //dgvcCheckBox.HeaderText = "Select";
                //List_Connected.Columns.Add(dgvcCheckBox);

                List_Connected.DataSource = table;
                progressBar1.Visible = false;

                BindGrid();
                //List_Connected.Columns[4].Frozen = true;
                //List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //check_not_connect.Enabled = true;
                check_all.Enabled = true;
                //string sql = @"select b.STK_ID , (c.bu_desc) as 'ST/CH' 	,CONVERT(VARCHAR(10), a.CurDate, 103) as 'Check_Online'
                //     ,a.inventory as 'Lệch Ngày',c.SRV_IP,rtrim(LTRIM(RIGHT(+it_name, CHARINDEX(' ', REVERSE(+it_name))))) AS 'IT',d.IT_PHONE
                //        ,DS_TT,ds_tv,(DS_TT-ds_tv) as'Lệch DT', bills, Bills_LW,(bills- Bills_LW) as 'Lệch Bill' 
                //        FROM doanhso a
                //        inner join[172.16.70.20].dsmart12.dbo.stock as b on a.Stk_id = b.STK_ID
                //        inner join  BRG_Info as c on a.Stk_id = c.STK_ID
                //        inner join  IT_BRG_INFO as d on a.Stk_id = d.STK_ID
                //        where b.DIMENSION <> 0 and(a.CurDate) is null or a.inventory <> 0 
                //        or (DS_TT-ds_tv)is null or (bills- Bills_LW)<>0
                //     or (bills- Bills_LW)is null or (DS_TT-ds_tv)>=1000";
                //List_Not_Connect.DataSource = cn.taobang(sql);
                List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                List_Connected.Columns[5].Frozen = true;
                //List_Connected.Columns[0].ReadOnly = false;
                List_Connected.Columns[1].ReadOnly = true;
                List_Connected.Columns[2].ReadOnly = true;
                List_Connected.Columns[3].ReadOnly = true;
                List_Connected.Columns[4].ReadOnly = true;
                List_Connected.Columns[5].ReadOnly = true;
                List_Connected.Columns[6].ReadOnly = true;
                //List_Connected.Columns[7].ReadOnly = true;

                for (int i = 0; i < List_Connected.Rows.Count - 1; i++)
                {
                    List_Connected.Rows[i].Cells[1].Value = i + 1;
                }



                //---------------------------------------------------
                foreach (DataGridViewRow row in List_Connected.Rows)
                {
                    string somestring = (Convert.ToString(row.Cells["Tháng Khóa Sổ"].Value));
                    if (somestring.Length > 0)
                    { thang_tren_data = Convert.ToInt32(somestring.Substring(somestring.Length - 2, 2)); }
                    else
                    { thang_tren_data = 0; }
                    int thang_hien_tai = da.Month;
                    if ((row.Cells["Tháng Khóa Sổ"].Value is DBNull) || (Convert.ToInt32(row.Cells["Tháng Khóa Sổ"].Value) == 0) || ((thang_hien_tai - thang_tren_data) >= 2))
                    {
                        row.Cells[0].Value = true;
                    }

                }
                //--------------------------------------------------------
                //List_Not_Connect.Columns[4].Frozen = true;
                //setDataSource(table);
                timer1.Stop();
            }
        }

        private void setDataSource1(DataTable table)
        {
            // Invoke method if required:
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource1), table);
            }
            else
            {
                //List_Not_Connect.DataSource = table;
                progressBar1.Visible = false;
                //List_Connected.Columns[4].Frozen = true;
                //List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //check_not_connect.Enabled = true;
                check_all.Enabled = true;
                string sql = @"select b.STK_ID , (c.bu_desc) as 'ST/CH' 	,CONVERT(VARCHAR(10), a.CurDate, 103) as 'Check_Online'
                         ,a.inventory as 'Lệch Ngày',c.SRV_IP,rtrim(LTRIM(RIGHT(+it_name, CHARINDEX(' ', REVERSE(+it_name))))) AS 'IT',d.IT_PHONE
                            ,DS_TT,ds_tv,(DS_TT-ds_tv) as'Lệch DT', bills, Bills_LW,(bills- Bills_LW) as 'Lệch Bill' 
                            FROM doanhso a with(nolock) 
                            inner join[172.16.70.20].dsmart12.dbo.stock as b with(nolock) on a.Stk_id = b.STK_ID
                            inner join  BRG_Info as c with(nolock) on a.Stk_id = c.STK_ID
                            inner join  IT_BRG_INFO as d with(nolock) on a.Stk_id = d.STK_ID
                            where b.DIMENSION <> 0";
                List_Connected.DataSource = cn.taobang(sql);
                //List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Connected.Columns[4].Frozen = true;
                //List_Not_Connect.Columns[4].Frozen = true;
                //setDataSource(table);
                timer1.Stop();
            }
        }
        private void changecorlor()
        {
            foreach (DataGridViewRow row in List_Connected.Rows)
            {
                string somestring = (Convert.ToString(row.Cells["Tháng Khóa Sổ"].Value));
                if (somestring.Length > 0)
                { thang_tren_data = Convert.ToInt32(somestring.Substring(somestring.Length - 2, 2)); }
                else
                { thang_tren_data = 0; }
                int thang_hien_tai = da.Month;

                if ((row.Cells["Tháng Khóa Sổ"].Value is DBNull) || (Convert.ToInt32(row.Cells["Tháng Khóa Sổ"].Value) == 0) || ((thang_hien_tai - thang_tren_data) >= 2))
                //if ((row.Cells["Tháng Khóa Sổ"].Value is DBNull) || (Convert.ToInt32(row.Cells["Tháng Khóa Sổ"].Value) != 0) || ((thang_hien_tai - thang_tren_data) <= 1))
                //Convert.ToInt32(row.Cells["Lệch Ngày"].Value) != 0 ||
                {
                    row.Cells["Tháng Khóa Sổ"].Style.BackColor = Color.Red;
                    //int tam = thang_hien_tai - thang_tren_data;
                    //MessageBox.Show(Convert.ToString(tam));
                    //MessageBox.Show(thang_tren_data.ToString());
                }
                else
                {
                    row.Cells["Tháng Khóa Sổ"].Style.BackColor = Color.Blue;
                }

                //string somestring = (Convert.ToString(row.Cells["Tháng Khóa Sổ"].Value));
                //int thang_tren_data = Convert.ToInt32(somestring.Substring(somestring.Length - 3, 3));
                ////DateTime Thang = DateTime.Now;
                //int thang_hien_tai= da.Month;
                /*((thang_hien_tai - thang_tren_data) <= 1)*/;
                //if ((row.Cells["Lệch DT"].Value is DBNull) || (Convert.ToInt32(row.Cells["Lệch DT"].Value) >= 1000))
                //{
                //    row.Cells["Lệch DT"].Style.BackColor = Color.Red;
                //}
                //else
                //{
                //    row.Cells["Lệch DT"].Style.BackColor = Color.Blue;
                //}

                //if ((row.Cells["Lệch Bill"].Value is DBNull) || (Convert.ToInt32(row.Cells["Lệch Bill"].Value) != 0))
                //{
                //    row.Cells["Lệch Bill"].Style.BackColor = Color.Red;
                //}
                //else
                //{
                //    row.Cells["Lệch Bill"].Style.BackColor = Color.Blue;
                //}
                //List_Connected.Columns["ColumnName"].DefaultCellStyle.Format = "+#,##0;-#,##0;0";
            }
            //List_Connected.Refresh();
            //this.List_Connected.Columns[1].Visible = false;
            //for (int i = 0; i < List_Connected.RowCount; i++)
            //{
            //    if (i / 2 == 0)
            //    {
            //        //this is where your LAST LINE code goes
            //        //row.DefaultCellStyle.BackColor = Color.Yellow;
            //        List_Connected.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        //this is your normal code NOT LAST LINE
            //        //row.DefaultCellStyle.BackColor = Color.Red;
            //        List_Connected.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //    }
            //}
        }

        //private async Task LoadData()
        //{
        //    // show progress bar
        //    progressBar1.Visible = true;
        //    await LoadDataAsync();
        //    // hide progress bar
        //    progressBar1.Visible = false;
        //}

        private void dosomething()
        {
            foreach (DataGridViewRow row in List_Connected.Rows)
            {
                if (Convert.ToInt32(row.Cells["TT"].Value) / 2 == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void List_Connected_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // once per format
            if (e.ColumnIndex == 0 && e.RowIndex == 0)
            {
                foreach (DataGridViewRow row in List_Connected.Rows)
                    if (row != null)
                        row.DefaultCellStyle.BackColor = Color.Red;
            }
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }



        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    progressBar1.Value = e.ProgressPercentage;
        //    System.Windows.Forms.Application.DoEvents();
        //    //textBox1.Text = a + " %";
        //}

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            check_all.Enabled = true;
            //check_not_connect.Enabled = true;
            //MessageBox.Show("Hoàn thành tiến trình", "tiến trình kết thức", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void check_not_connect_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            // Start the background worker
            backgroundWorker2.RunWorkerAsync();
            da = DateTime.Now;
            timer1.Start();
            check_all.Enabled = false;
            //check_not_connect.Enabled = false;
            //if (MessageBox.Show =true)

            ////////////////////check_all.Enabled = false;
            ////////////////////check_not_connect.Enabled = false;
            ////////////////////check_all.Enabled = false;
            ////////////////////check_not_connect.Enabled = false;
            ////////////////////string sql = @"rptCheckCurrentDate_old";
            //////////////////////dt = cn.taobang_from_Procedure(sql);
            ////////////////////List_Not_Connect.DataSource = cn.taobang_from_Procedure(sql);
            //////////////////////--------------------------------------------------------------------------------
            ////////////////////sql = @"select b.STK_ID , (c.bu_desc) as 'ST/CH' 	,CONVERT(VARCHAR(10), a.CurDate, 103) as 'Check_Online'
            ////////////////////         ,a.inventory as 'Lệch Ngày',c.SRV_IP,rtrim(LTRIM(RIGHT(+it_name, CHARINDEX(' ', REVERSE(+it_name))))) AS 'IT',d.IT_PHONE
            ////////////////////            ,DS_TT,ds_tv,(DS_TT-ds_tv) as'Lệch DT', bills, Bills_LW,(bills- Bills_LW) as 'Lệch Bill' 
            ////////////////////            FROM doanhso a
            ////////////////////            inner join[172.16.70.20].dsmart12.dbo.stock as b on a.Stk_id = b.STK_ID
            ////////////////////            inner join  BRG_Info as c on a.Stk_id = c.STK_ID
            ////////////////////            inner join  IT_BRG_INFO as d on a.Stk_id = d.STK_ID
            ////////////////////            where b.DIMENSION <> 0";
            ////////////////////List_Connected.DataSource = cn.taobang(sql);
            ////////////////////changecorlor();
            //////////////////////dosomething();
            //////////////////////dgv_CellFormatting(List_Connected);

            ////////////////////List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            ////////////////////List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            ////////////////////List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            ////////////////////List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //////////////////////List_Connected.Refresh();
            ////////////////////check_not_connect.Enabled = true;
            ////////////////////check_all.Enabled = true;
        }

        private void dgvMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void export_all_Click(object sender, EventArgs e)
        {

        }

        private void export_not_connect_Click(object sender, EventArgs e)
        {

        }

        private void List_Connected_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void List_Connected_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)//set your checkbox column index instead of 2
            {
                if (Convert.ToBoolean(List_Connected.Rows[e.RowIndex].Cells[0].EditedFormattedValue) == true)
                {
                    //Paste your code here
                    stk_id.Text += "," + List_Connected.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                }
                if (Convert.ToBoolean(List_Connected.Rows[e.RowIndex].Cells[0].EditedFormattedValue) == false)
                {
                    string tim = "," + List_Connected.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                    string nguon = stk_id.Text;
                    //nguon=nguon.Replace(tim, "");
                    stk_id.Text = nguon.Replace(tim, "");
                }
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            changecorlor();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                fr_Check_Month_Close f3 = (fr_Check_Month_Close)Application.OpenForms["fr_Check_Month_Close"];
                f3.Close();
            }
            catch (NullReferenceException ne)
            {
                //One of the forms is not opened
            }
        }

        private void List_Connected_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (timer1.Enabled)
        //    {
        //        timer1.Stop();
        //        button1.Text = "Start";
        //    }
        //    else
        //    {
        //        da = DateTime.Now;
        //        timer1.Start();
        //        button1.Text = "Stop";
        //    }
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now.Subtract(da);
            label1.Text = span.Hours.ToString() + " : " + span.Minutes.ToString() + " : " + span.Seconds.ToString() + " : "
                + span.Milliseconds.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            da = DateTime.Now;
            timer1.Start();
            //button1.Text = "Stop";

            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();

            //if (MessageBox.Show =true)

            check_all.Enabled = false;
            //check_not_connect.Enabled = false;
            //SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
        }

        private void List_Not_Connect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void List_Connected_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //////bool checkedCell = (bool)List_Connected.Rows[1].Cells[0].Value;
            //////foreach (DataGridViewRow row in List_Connected.Rows)
            //////{
            //////    if (checkedCell == true)
            //////    {
            //////        row.Cells[0].Value = true;
            //////    }
            //////    else
            //////    {
            //////        row.Cells[0].Value = false;
            //////    }    
            //////}
            changecorlor();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //bool checkedCell = (bool)List_Connected.Rows[1].Cells[0].Value;
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow row in List_Connected.Rows)
                {
                    row.Cells[0].Value = true;
                }
                stk_id.Text = "%";
            }
            else
            {
                foreach (DataGridViewRow row in List_Connected.Rows)
                {
                    row.Cells[0].Value = false;
                }
                stk_id.Text = "%,";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in List_Connected.Rows)
            {
                if (checkBox2.Checked == true)
                {
                    string somestring = (Convert.ToString(row.Cells["Tháng Khóa Sổ"].Value));
                    if (somestring.Length > 0)
                    { thang_tren_data = Convert.ToInt32(somestring.Substring(somestring.Length - 2, 2)); }
                    else
                    { thang_tren_data = 0; }
                    int thang_hien_tai = da.Month;
                    if ((row.Cells["Tháng Khóa Sổ"].Value is DBNull) || (Convert.ToInt32(row.Cells["Tháng Khóa Sổ"].Value) == 0) || ((thang_hien_tai - thang_tren_data) >= 2))
                    {
                        row.Cells[0].Value = true;
                    }
                    if (!row.IsNewRow)
                    {
                        stk_id.Text += "," + row.Cells[2].Value.ToString();
                        //MessageBox.Show(stk_id.Text);
                    }

                }
                else
                {
                    string somestring = (Convert.ToString(row.Cells["Tháng Khóa Sổ"].Value));
                    if (somestring.Length > 0)
                    { thang_tren_data = Convert.ToInt32(somestring.Substring(somestring.Length - 2, 2)); }
                    else
                    { thang_tren_data = 0; }
                    int thang_hien_tai = da.Month;
                    if ((row.Cells["Tháng Khóa Sổ"].Value is DBNull) || (Convert.ToInt32(row.Cells["Tháng Khóa Sổ"].Value) == 0) || ((thang_hien_tai - thang_tren_data) >= 2))
                    {
                        row.Cells[0].Value = false;
                    }
                    if (!row.IsNewRow)
                    {
                        string tim = "," + row.Cells[2].Value.ToString();
                        string nguon = stk_id.Text;
                        stk_id.Text = nguon.Replace(tim, "");
                    }
                }
                if (stk_id.Text == "")
                {
                    stk_id.Text = "%";
                }
            }
        }

        private void List_Connected_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    //string value =
            //    if (e.ColumnIndex == 0)
            //    {
            //        if ((bool)(List_Connected.Rows[e.RowIndex].Cells[0].Value = true))
            //        {
            //            stk_id.Text += "," + List_Connected.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            //        }                                        
            //    }                
            //}
        }

        ///---------------------- add check box colums
        CheckBox headerCheckBox = new CheckBox();
        private void BindGrid()
        {


            //Add a CheckBox Column to the DataGridView Header Cell.

            //Find the Location of Header Cell.
            Point headerCellLocation = this.List_Connected.GetCellDisplayRectangle(0, -1, true).Location;

            //Place the Header CheckBox in the Location of the Header Cell.
            headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
            headerCheckBox.BackColor = Color.White;
            headerCheckBox.Size = new Size(18, 18);

            //Assign Click event to the Header CheckBox.
            headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
            List_Connected.Controls.Add(headerCheckBox);

            //Add a CheckBox Column to the DataGridView at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "checkBoxColumn";
            List_Connected.Columns.Insert(0, checkBoxColumn);

            //Assign Click event to the DataGridView Cell.
            List_Connected.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
        }
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            List_Connected.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in List_Connected.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check to ensure that the row CheckBox is clicked.
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                bool isChecked = true;
                foreach (DataGridViewRow row in List_Connected.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["checkBoxColumn"].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = isChecked;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            changecorlor();
        }

        ///


    }

}
