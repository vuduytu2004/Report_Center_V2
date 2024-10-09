//using System.Collections;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using Report_Center.DataAccess;
using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using COMExcel = Microsoft.Office.Interop.Excel;
//using System.Threading.Tasks;
//using Microsoft.Office.Interop;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
//using System.Threading;
//using Font = System.Drawing.Font;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;
//using Action = System.Action;
//using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;

namespace Report_Center.Presentation
{
    public partial class rptASSORTMENT_Lv2 : Form
    {

        public rptASSORTMENT_Lv2()
        {
            InitializeComponent();
            //Shown += new EventHandler(rptASSORTMENT_Lv2_Shown);
            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //// This event will be raised when we call ReportProgress
            //backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            //this.dataGridView_full.ColumnWidthChanged += new
            //DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);
            //this.dataGridView_full.CellValueChanged += new
            //DataGridViewCellEventHandler(dataGridView1_CellValueChanged);

            frdate.CustomFormat = "dd/MM/yyyy";
            todate.CustomFormat = "dd/MM/yyyy";
            //frdate.Value = DateTime.Today.AddDays(-1);
            //Process currentProcess = Process.GetCurrentProcess();
            //long usedMemory = currentProcess.PrivateMemorySize64;
        }
        ConnectDB cn = new ConnectDB();
        DateTime da;
        //this.Ma_NCC.GotFocus += Ma_NCC_GotFocus;
        //this.Ma_NCC.Click += Ma_NCC_Click;
        DataTable table = new DataTable();
        DataTable table1 = new DataTable();
        //ConnectDB cn = new ConnectDB();
        string dk;
        //public object AutoCompleteSuggestMode { get; private set; }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                SendKeys.Send("{TAB}");
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                return;
            }
            try
            {
                rptASSORTMENT_Lv2 f3 = (rptASSORTMENT_Lv2)Application.OpenForms["rptASSORTMENT_Lv2"];
                f3.Close();
            }
            catch (NullReferenceException ne)
            {
                if (backgroundWorker1.IsBusy)
                {
                    //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                    return;
                }
            }
        }
        void rptASSORTMENT_Lv2_Shown(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();

        }
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //button2.Enabled = false;
            //progressBar11.Visible = true;
            //progressBar11.Style = ProgressBarStyle.Marquee;
            //DataTable table1 = new DataTable();
            //DataTable table1 = new DataTable();
            int sapxep;
            if (theo_dt.Checked == true) { sapxep = 2; }
            if (theo_ln.Checked == true) { sapxep = 2; }
            else { sapxep = 1; }
            string sql1 = "rptASSORTMENT_Chaytay_Lv2";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            //table1 = cn.taobang_from_Procedure_Parameter2(sql1, frdate, todate, Stk_Id.Text.ToString(), Grp_id.Text.ToString(), int.Parse(SapXep.Text));
            table1 = cn.taobang_from_Procedure_Parameter3(sql1, frdate, todate, Stk_Id.Text.ToString(), Grp_id.Text.ToString(), sapxep);


            //Response.clear()
            //    Creating DataTable.
            //DataTable dt_grid_nhap = new DataTable();

            foreach (DataRow dong_row in table1.Rows)
            {
                dong_row[3] = Converter.TCVN3ToUnicode(dong_row[3].ToString());
                dong_row[5] = Converter.TCVN3ToUnicode(dong_row[5].ToString());
                dong_row[7] = Converter.TCVN3ToUnicode(dong_row[7].ToString());
                dong_row[9] = Converter.TCVN3ToUnicode(dong_row[9].ToString());
            }
            ////////////////////foreach (DataColumn cot_cot in table1.Columns)
            ////////////////////{
            ////////////////////    cot_cot.ColumnName = Converter.TCVN3ToUnicode(cot_cot.ToString());               
            ////////////////////}
            //System.GC.Collect();
            //progressBar1.Value = dong + 1;
            //dong++;

            //setDataSource(table);
            //For example, for the DataTable  provided as Datasource 
            //DataTable dtReturn = Pivot_table.GetInversedDataTable(table, "ten_cot", "sku_id", "value", "-", true);



            //DataTable dtReturn = Pivot_table.Pivot(table,  "ten_cot" , "value");

            //For example, for the DataTable  provided as Datasource 
            //DataTable dtReturn = cn.GetInversedDataTable(table, "Date", "EmployeeID",
            //                                          "Cost", "-", true);

            setDataSource(table1);
            //ClearTable(table);
            //ClearTable(table1);
            GC.Collect();
            //Clipboard.Clear();
            //////////////////////////for (int i = 0; i < dataGridView_full.Rows.Count; i++)
            //////////////////////////{
            //////////////////////////    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //////////////////////////}

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
                dataGridView_full.DataSource = table;
                progressBar1.Visible = false;
                dataGridView_full.Columns[5].Frozen = true;
                timer1.Stop();
                tk_Full.Enabled = true;
                export_full.Enabled = true;

                //for (int i = 0; i < dataGridView_full.Rows.Count; i++)
                //{
                //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
                //}

            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void rptASSORTMENT_Lv2_Load(object sender, EventArgs e)
        {

            //backgroundWorker1.RunWorkerAsync();
            //////      //Task task = Task.Run((Action) MyFunction);
            //////      string sql = @"select top (2) a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            //////              ,a.SKU_ID as 'Mã Hàng', c.BARCODE 
            //////              ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            //////              ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm' 
            //////              ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            //////              , c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            //////              ,PCPR_CODE as 'Vùng Giá' 
            //////              ,c.STATUS as 'Trạng Thái'
            //////              ,c.ITEM_TYPE as 'Loại hàng'
            //////              ,e.OPEN_DATE ,e.MODI_DATE 
            //////              from SPPRICE a
            //////              left join SUPPLIER as b on a.supp_id=b.supp_id
            //////              left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            //////left join GOODS as e on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            //////              where c.status <> '02' order by a.SKU_ID";

            //////      //dataGridView_full.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //////      //dataGridView_full.ColumnHeadersVisible = false;

            //////      dataGridView_full.DataSource = cn.taobang1(sql);

            //////      for (int i = 0; i < dataGridView_full.Rows.Count; i++)
            //////      {
            //////          dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //////      }

            //////      dataGridView_full.Columns[4].Frozen = true;

            //FastAutoSizeColumns(dataGridView_full);

            //_worker1.RunWorkerAsync();

            //dataGridView_full.DataSource = _worker1.RunWorkerAsync();


            //dataGridView_full.Columns["DS_TT"].DefaultCellStyle.Format = Converter.TCVN3ToUnicode(dataGridView_full);


            //_worker1_DoWork();
            //dataGridView_full.AutoResizeColumns();

            //dataGridView_full.ColumnHeadersVisible = true;
            //dataGridView_full.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //changecorlor();
            //dosomething();
            //dgv_CellFormatting(List_Connected);


            //List_Connected.Refresh();
            //dataGridView_full.Columns["DS_TT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //dataGridView_full.Columns["DS_TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Not_Connect.RightToLeft = Enabled;


            //dataGridView_full.DataSource = DS.Tables("LV1");

            //FastAutoSizeColumns(dataGridView_full);
            //dataGridView_full.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //dataGridView_full.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;


        }


        //----------------------------------------
        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.dataGridView_full.AutoSizeColumnsMode ==
            //DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader &&
            //this.dataGridView_full.Columns[e.ColumnIndex].AutoSizeMode ==
            //DataGridViewAutoSizeColumnMode.None)
            //{
            //    this.dataGridView_full.Columns[e.ColumnIndex].AutoSizeMode =
            //    DataGridViewAutoSizeColumnMode.NotSet;
            //}
        }

        void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //if (e.Column.Width > 200 &&
            //this.dataGridView_full.AutoSizeColumnsMode ==
            //DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader &&
            //e.Column.AutoSizeMode == DataGridViewAutoSizeColumnMode.NotSet)
            //{
            //    e.Column.AutoSizeMode =
            //    DataGridViewAutoSizeColumnMode.None;
            //    this.dataGridView_full.ColumnWidthChanged -= new
            //    DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);
            //    e.Column.Width = 200;
            //    this.dataGridView_full.ColumnWidthChanged += new
            //    DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);
            //}
        }
        //----------------------------------------
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now.Subtract(da);
            label6.Text = span.Hours.ToString() + " : " + span.Minutes.ToString() + " : " + span.Seconds.ToString() + " : "
                + span.Milliseconds.ToString();
        }

        private void SQLtoExcel(DataGridView grView, string Output)
        {
            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".csv";


            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
            {
                int dong = 0;
                // Loop through the fields and add headers
                //Adding the Columns.
                foreach (DataGridViewColumn column in grView.Columns)
                {
                    //dt_grid_nhap.Columns.Add(column.HeaderText, column.ValueType);
                    //dt_grid_nhap.Columns.Add(Converter.TCVN3ToUnicode(column.HeaderText), column.ValueType);
                    string name = column.HeaderText;
                    if (name.Contains(","))
                        name = "\"" + name + "\"";
                    fs.Write(name + ",");

                }
                fs.WriteLine();
                //Adding the Rows.
                //int dong = 0;
                foreach (DataGridViewRow row in grView.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string value = cell.Value.ToString();
                        if (value.Contains(","))
                            value = "\"" + value + "\"";

                        fs.Write(value + ",");
                    }
                    fs.WriteLine();
                    progressBar1.Value = dong + 1;
                    dong++;
                }
                fs.Close();





                ////////////-----------------------------------------------------------------------
                ////////////for (int i = 0; i < grView.ColumnCount; i++)
                ////////////{
                ////////////    //Converter.TCVN3ToUnicode (dr.GetName(i));
                ////////////    string name = grView.Columns.ToString();
                ////////////    if (name.Contains(","))
                ////////////        name = "\"" + name + "\"";
                ////////////    fs.Write(name + ",");

                ////////////}
                ////////////fs.WriteLine();

                ////////////// Loop through the rows and output the data
                ////////////int dong = 0;
                ////////////for (int i = 0; i < grView.Rows.Count; i++)
                ////////////{
                ////////////    for (int j = 1; j < grView.Columns.Count; j++)
                ////////////    {
                ////////////        string value = Converter.TCVN3ToUnicode(grView.Rows[i].Cells[j].Value.ToString());
                ////////////        if (value.Contains(","))
                ////////////            value = "\"" + value + "\"";

                ////////////        fs.Write(value + ",");
                ////////////    }
                ////////////    fs.WriteLine();
                ////////////    progressBar1.Value = dong + 1;
                ////////////    dong++;
                ////////////}

                ////////////while (dr.Read())
                ////////////{

                ////////////    for (int i = 0; i < dr.FieldCount; i++)
                ////////////    {
                ////////////        string value = Converter.TCVN3ToUnicode(dr[i].ToString());
                ////////////        //if (i == 19 )
                ////////////        //{
                ////////////        //    //CultureInfo frC = new CultureInfo("fr-FR");
                ////////////        //    value = string.Format("{0:MM/dd/yyyy}", value);
                ////////////        //    //value = String.Format("Number {0, 0:D5}", value);
                ////////////        //}


                ////////////        if (value.Contains(","))
                ////////////            value = "\"" + value + "\"";

                ////////////        fs.Write(value + ",");

                ////////////    }
                ////////////    fs.WriteLine();
                ////////////    progressBar1.Value = dong + 1;
                ////////////    dong++;
                ////////////}

                //////////////fs.Close();
            }

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = app.Workbooks.Open(Filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //wb.TableStyles.

            wb.SaveAs(Output, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wb.Close();
            app.Quit();
            File.Delete(Filename);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Blocks;
            int sttmax = dataGridView_full.Rows.Count + 1;
            progressBar1.Maximum = sttmax;
            //////////string sql;
            //////////sql = @"select count(*) from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02')";
            ////////////p_max = cn.Gan_max_progressbar(sql);
            ////////////            sql = @"select a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định',dbo.tcvn2unicode(b.SUPP_NAME) as 'Tên NCC' ,a.SKU_ID as 'Mã Hàng',c.SKU_CODE,dbo.tcvn2unicode(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT', c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            ////////////,PCPR_CODE as 'Vùng Giá' from SPPRICE a
            ////////////left join SUPPLIER as b on a.supp_id=b.supp_id
            ////////////left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            ////////////where a.sku_id in (select SKU_ID from SKU_DEF where status <> '02') order by a.SKU_ID";
            //////////progressBar1.Maximum = cn.Gan_max_progressbar(sql);

            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            //////////      //sql = @"select SUPP_ID as 'Mã NCC', SKU_ID as 'Mã Hàng', SPPRICE as 'Giá Nhập chỉ định', PCPR_CODE as 'Vùng Giá' from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02') order by SKU_ID";
            //////////      //select* from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02')
            //////////      sql = @"select a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            //////////              ,a.SKU_ID as 'Mã Hàng', c.BARCODE 
            //////////              ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            //////////              ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm' 
            //////////              ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            //////////              , c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            //////////              ,PCPR_CODE as 'Vùng Giá' 
            //////////              ,c.STATUS as 'Trạng Thái'
            //////////              ,c.ITEM_TYPE as 'Loại hàng'
            //////////              ,e.OPEN_DATE ,e.MODI_DATE 
            //////////              from SPPRICE a
            //////////              left join SUPPLIER as b on a.supp_id=b.supp_id
            //////////              left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            //////////left join GOODS as e on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            //////////              where c.status <> '02' order by a.SKU_ID";


            //////////      //  Ngày tạo, loại hàng, trạng thái ----Giá bán,Barcode, Mã nhóm, tên nhóm, 


            SQLtoExcel(dataGridView_full, "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
            System.Diagnostics.Process.Start("D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
            GC.Collect();
            progressBar1.Visible = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
        }


        private void dataGridView_full_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //--------------------------------------------------------
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////////if (e.RowIndex >= 0)
            ////////{
            ////////    //string value =
            ////////    if (e.ColumnIndex == 1)
            ////////    {
            ////////        Ma_NCC.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            ////////    }
            ////////    if (e.ColumnIndex == 3)
            ////////    {
            ////////        Ma_NCC.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
            ////////    }
            ////////    if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            ////////    {
            ////////        Ma_hang.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            ////////    }
            ////////    if (e.ColumnIndex == 9)
            ////////    {
            ////////        Ma_nhom.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            ////////    }
            ////////    if (e.ColumnIndex == 10)
            ////////    {
            ////////        Ma_nhom.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
            ////////    }
            ////////    if (e.ColumnIndex == 7)
            ////////    {
            ////////        Ma_hang.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
            ////////    }
            ////////    ////Lưu lại dòng dữ liệu vừa kích chọn
            ////////    //DataGridViewRow rowss = this.dataGridView_full.Rows[e.RowIndex];
            ////////    ////DataGridViewCell cell = this.dataGridView_full.CellClick();
            ////////    //DataGridViewColumn columnss =this.dataGridView_full.Columns[e.ColumnIndex];
            ////////    ////Đưa dữ liệu vào textbox
            ////////    //Ma_NCC.Text = rowss.Cells[columnss.ValueType].Value.ToString();
            ////////    //string value =
            ////////    //dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            ////////    //txtHoVaTen.Text = row.Cells[1].Value.ToString();
            ////////    //txtQueQuan.Text = row.Cells[2].Value.ToString();

            ////////    //Không cho phép sửa trường STT
            ////////    //txtSTT.Enabled = false;
            ////////}
        }
        //----------------------------------------------------------
        private void dataGridView_full_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void Ma_NCC_GotFocus(object sender, RoutedEventArgs e)
        //private void Ma_NCC_Click(object sender, EventArgs e)
        //{
        //    this.Ma_NCC.SelectAll();
        //    this.Ma_NCC.Click -= Ma_NCC_Click;
        //}

        //private void Ma_NCC_GotFocus(object sender, EventArgs e)
        //{
        //    this.Ma_NCC.Select(this.Ma_NCC.Text.Length, 0);
        //}

        private void Ma_NCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tk_Full_Click(object sender, EventArgs e)
        {
            // Kiểm tra điều kiện
            string phrase = Stk_Id.Text;
            phrase = phrase.Replace(" ", "");
            phrase = phrase.Replace("*", "");
            string[] words = phrase.Split(',');
            if (phrase.Length != 0)
            {
                int i = 0;
                foreach (var word in words)
                {
                    if (i == 0)
                    { dk = $"'{word}'"; }

                    else
                    { dk += "," + $"'{word}'"; }
                    i++;
                    //System.Console.WriteLine($"<{word}>");
                }
                string sql1 = @"select count(*)  from STK_INFO with(nolock)  where stk_id in (" + dk + ")";
                i = cn.Gan_max_progressbar(sql1);
                if (i == 0)
                {
                    MessageBox.Show("Điều kiện Siêu Thị/Vùng.. không có." + "\n" + "Hãy kiểm tra lại .", "Chú ý !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            ClearTable(table1);
            ClearTable(table);
            dataGridView_full.Columns.Clear();
            dataGridView_full.Refresh();
            GC.Collect();
            da = DateTime.Now;
            timer1.Start();
            // Tạo cột STT, add cột vào tb trước khi đổ dữ liệu vào table >> STT cột đầu tiên
            //DataColumn Col = new DataColumn("STT", typeof(int));
            //dataGridView_full.Columns.Add(Col);

            //......DataGridView1.DataSource = dt;
            if (backgroundWorker1.IsBusy)
            {
                //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                return;
            }
            backgroundWorker1.RunWorkerAsync();

            tk_Full.Enabled = false;
            export_full.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            //       string sql = @"select a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            //               ,a.SKU_ID as 'Mã Hàng', c.BARCODE 
            //               ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            //               ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm' 
            //               ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            //               , c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            //               ,PCPR_CODE as 'Vùng Giá' 
            //               ,c.STATUS as 'Trạng Thái'
            //               ,c.ITEM_TYPE as 'Loại hàng'
            //               ,e.OPEN_DATE ,e.MODI_DATE 
            //               from SPPRICE a
            //               left join SUPPLIER as b on a.supp_id=b.supp_id
            //               left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            //left join GOODS as e on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            //               where c.status <> '02' ";
            //       if ((Ma_NCC.Text == "") && (Ma_nhom.Text == "") && (Ma_hang.Text == ""))
            //               { progressBar1.Visible = false;
            //                   return; }
            //       if ((Ma_NCC.Text != ""))
            //               { sql += " and a.SUPP_ID= N'" + Ma_NCC.Text + "' or b.SUPP_NAME like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_NCC.Text) + "%'"; }
            //       if ((Ma_hang.Text != ""))
            //               { sql += " and (a.SKU_ID like N'%" + Ma_hang.Text + "%' or c.BARCODE like N'%" + Ma_hang.Text + "%' or c.SKU_CODE like N'%" + Ma_hang.Text + "%' or c.FULL_NAME like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_hang.Text) + "%')"; }
            //       if ((Ma_nhom.Text != ""))
            //               { sql += " and c.GRP_ID like N'%" + Ma_nhom.Text + "%' or c.grp_name like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_nhom.Text) + "%'"; }


            //dataGridView_full.DataSource = cn.taobang1(sql);

            //for (int i = 0; i < dataGridView_full.Rows.Count ; i++)
            //{
            //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //}
            //progressBar1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////if (backgroundWorker1.IsBusy)
            ////{
            ////    //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
            ////    return;
            ////}
            ////else
            ////{

            ////    progressBar1.Visible = true;
            ////    progressBar1.Style = ProgressBarStyle.Marquee;
            ////    backgroundWorker1.RunWorkerAsync();
            ////}
            ///////////////////////////////////////////////////////////////////
            //string sql = @"select * from table_test ";


            //dataGridView_full.DataSource = cn.taobang(sql);


        }

        private void ClearTable(DataTable table)
        {
            try
            {
                table.Clear();
            }
            catch (DataException e)
            {
                // Process exception and return.
                Console.WriteLine("Exception of type {0} occurred.",
                    e.GetType());
            }
        }

        private void Exprot_Table2Excl()
        {
            //Response.clear()
            //Creating DataTable.
            //--------------------------------------------------------------
            //////////////////////DataTable dt_grid_nhap = new DataTable();

            ////////////////////////Adding the Columns.
            //////////////////////foreach (DataGridViewColumn column in dataGridView_full.Columns)
            //////////////////////{
            //////////////////////    //dt_grid_nhap.Columns.Add(column.HeaderText, column.ValueType);
            //////////////////////    //dt_grid_nhap.Columns.Add(Converter.TCVN3ToUnicode(column.HeaderText), column.ValueType);
            //////////////////////    dt_grid_nhap.Columns.Add((column.HeaderText), column.ValueType);

            //////////////////////}

            ////////////////////////Adding the Rows.
            ////////////////////////int dong = 0;
            //////////////////////foreach (DataGridViewRow row in dataGridView_full.Rows)
            //////////////////////{
            //////////////////////    dt_grid_nhap.Rows.Add();
            //////////////////////    foreach (DataGridViewCell cell in row.Cells)
            //////////////////////    {
            //////////////////////        //if (cell.ColumnIndex == 3 || cell.ColumnIndex == 4)// || cell.ColumnIndex == 10)
            //////////////////////        //{
            //////////////////////        //    dt_grid_nhap.Rows[dt_grid_nhap.Rows.Count - 1][cell.ColumnIndex] = Converter.TCVN3ToUnicode(cell.Value.ToString());
            //////////////////////        //}
            //////////////////////        ////else if (cell.ColumnIndex == "")
            //////////////////////        ////{

            //////////////////////        ////}    
            //////////////////////        //else
            //////////////////////        //{
            //////////////////////        //    dt_grid_nhap.Rows[dt_grid_nhap.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
            //////////////////////        //}
            //////////////////////        dt_grid_nhap.Rows[dt_grid_nhap.Rows.Count - 1][cell.ColumnIndex] = cell.Value;

            //////////////////////    }
            //////////////////////    //System.GC.Collect();
            //////////////////////    //progressBar1.Value = dong + 1;
            //////////////////////    //dong++;
            //////////////////////}
            /////--------------------------------------------------------------------------------------------
            //Exporting to Excel.
            //string folderPath = "D:\\Fuji\\";
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            System.GC.Collect();
            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(table1, "Tồn_Bán");


                //Adjust widths of Columns.
                wb.Worksheet(1).Columns().AdjustToContents();

                //Lấy số ngẫu nhiên
                Random _r = new Random();
                string n = _r.Next(1, 10000).ToString();

                string Filename = "D:\\Ton_ban_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx";

                //dt_grid_nhap.Clear();
                //ClearTable(dt_grid_nhap);
                //Save the Excel file.
                wb.SaveAs(Filename);

                //System.Diagnostics.Process.Start("D:\\Zone_Price_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
                System.Diagnostics.Process.Start(Filename);
            }
            System.GC.Collect();

            //progressBar1.Visible = false;
        }
        private void export_full_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            progressBar1.Visible = true;
            //fr_Main. .statusStrip1.toolStripProgressBar1.
            //progressBar1.Style = ProgressBarStyle.Blocks;
            //int sttmax = dataGridView_full.Rows.Count + 1;
            //progressBar1.Maximum = sttmax;
            System.GC.Collect();
            System.Threading.Thread thread =
           new System.Threading.Thread(new System.Threading.ThreadStart(Exprot_Table2Excl));
            thread.Start();

            //progressBar1.Visible = false;
            //progressBar1.Style = ProgressBarStyle.Marquee;

        }

        private void dataGridView_full_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void copyAlltoClipboard()
        {
            dataGridView_full.SelectAll();
            DataObject dataObj = dataGridView_full.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void Exp2Excl_Click(object sender, EventArgs e)
        {
            ClearTable(table);
            GC.Collect();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "ASSORTMENT_Lv2.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Copy DataGridView results to clipboard
                    copyAlltoClipboard();

                    object misValue = System.Reflection.Missing.Value;  // sau khi copy vào Clipboard bộ nhớ chiếm : 787 MB
                    Excel.Application xlexcel = new Excel.Application();

                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                    Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    // Format column D as text before pasting results, this was required for my data
                    Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                    rng.NumberFormat = "@";
                    Excel.Range rng1 = xlWorkSheet.get_Range("G:G").Cells;
                    rng1.NumberFormat = "@";
                    Excel.Range rng2 = xlWorkSheet.get_Range("H:H").Cells;
                    rng2.NumberFormat = "@";
                    Excel.Range rng3 = xlWorkSheet.get_Range("J:J").Cells;
                    rng3.NumberFormat = "@";

                    // Paste clipboard results to worksheet range
                    Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                    // Delete blank column A and select cell A1
                    Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                    delRng.Delete(Type.Missing);
                    xlWorkSheet.get_Range("A1").Select();

                    // Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkSheet.Columns.AutoFit();
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dataGridView_full.ClearSelection();

                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "File đang mở or Data quá lớn ! Bấm Export Data");
                    //throw new Exception("ExportToExcel: \n" + ex.Message);
                    Clipboard.Clear();
                    dataGridView_full.ClearSelection();
                    return;
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void check_all_CheckedChanged(object sender, EventArgs e)
        {
            //Node_Id.Text = "Toàn bộ Mart/MiniMart";
            ////Node_Id.Enabled = false;
            //Node_Id.ReadOnly = true;
            //Node_Id.BackColor = System.Drawing.SystemColors.Window;
            Stk_Id.Enabled = true;
            Stk_Id.ReadOnly = false;
            Stk_Id.Text = "";
            Stk_Id.Focus();
        }

        private void check_mart_CheckedChanged(object sender, EventArgs e)
        {
            if (check_mart.Checked == true)
                Stk_Id.Text = "";
            cn.Load_TextBox(Stk_Id, @"SELECT  (a.STK_ID)  from stock   a with(nolock) 
                            inner join node_def as b  with(nolock) on a.stk_id=b.stk_id
                            where left(b.GRP_ID,2)='01' and a.type='01' and a.STATUS ='1'");
            //Node_Id.Enabled = false;
            Stk_Id.ReadOnly = true;
            Stk_Id.BackColor = System.Drawing.SystemColors.Window;
        }


        private void check_minimart_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart.Checked == true)
                Stk_Id.Text = "";
            cn.Load_TextBox(Stk_Id, @"SELECT  (a.STK_ID)  from stock   a with(nolock) 
                            inner join node_def as b with(nolock)  on a.stk_id=b.stk_id
                            where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1'");
            //Node_Id.Enabled = false;
            Stk_Id.ReadOnly = true;
            Stk_Id.BackColor = System.Drawing.SystemColors.Window;
        }

        private void check_minimart_hn_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart_hn.Checked == true)
                Stk_Id.Text = "";
            cn.Load_TextBox(Stk_Id, @"SELECT  (a.STK_ID)   from dbo.stock   a with(nolock) 
                              inner join node_def as b with(nolock)  on a.stk_id=b.stk_id
                              where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1' and a.PCPR_CODE='901'");
            //Node_Id.Enabled = false;
            Stk_Id.ReadOnly = true;
            Stk_Id.BackColor = System.Drawing.SystemColors.Window;
        }

        private void check_minimart_sg_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart_sg.Checked == true)
                Stk_Id.Text = "";

            cn.Load_TextBox(Stk_Id, @"SELECT  (a.STK_ID)   from dbo.stock   a with(nolock) 
                              inner join node_def as b  with(nolock) on a.stk_id=b.stk_id
                              where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1' and a.PCPR_CODE<>'901'");
            //Node_Id.Enabled = false;
            Stk_Id.ReadOnly = true;
            Stk_Id.BackColor = System.Drawing.SystemColors.Window;
        }

        private void Stk_Id_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(Stk_Id.Text.ToString(), "Danh sách STK_ID");
        }
    }

}
