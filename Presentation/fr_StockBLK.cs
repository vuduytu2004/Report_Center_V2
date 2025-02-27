//using System.Collections;
using ClosedXML.Excel;
//using COMExcel = Microsoft.Office.Interop.Excel;
//using System.Threading.Tasks;
//using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using Report_Center.DataAccess;
using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
//using System.Drawing;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Threading;
//using Font = System.Drawing.Font;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;
//using Action = System.Action;

namespace Report_Center.Presentation
{
    public partial class fr_StockBLK : Form
    {

        public fr_StockBLK()
        {
            InitializeComponent();
            Shown += new EventHandler(fr_StockBLK_Shown);
            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);

        }
        ConnectDB cn = new ConnectDB();

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
                fr_StockBLK f3 = (fr_StockBLK)Application.OpenForms["fr_StockBLK"];
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
        void fr_StockBLK_Shown(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            tk_Full.Enabled = false;
            export_full.Enabled = false;
            button2.Enabled = false;
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();

        }
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //button2.Enabled = false;
            //progressBar11.Visible = true;
            //progressBar11.Style = ProgressBarStyle.Marquee;
            DataTable table = new DataTable();

            string tim_sql;
            tim_sql = @"SELECT sql_str_DWH FROM var_sql_str where [form_name]= '" + this.Name.Trim() + "' AND [id]=1";
            string sql = cn.LayQuen(tim_sql).Trim();

            /// -----------------------------test//----------------------------------------------------------------------------------
            //string sql = @" SELECT ROW_NUMBER() OVER (ORDER BY STOCKBLK.STK_ID) AS [STT], STOCKBLK.STK_ID, c.stk_NAME,b.GRP_ID,b.GRP_NAME ,STOCKBLK.SKU_ID,b.FULL_NAME, STOCKBLK.BLOCK_CODE, STOCKBLK.MODI_DATE
            //                FROM DSMART12.dbo.STOCKBLK STOCKBLK with(nolock) 
            //                left join sku_def as b with(nolock)  on STOCKBLK.sku_id=b.SKU_ID 
            //                left join stock as c  with(nolock) on STOCKBLK.stk_id=c.stk_id
            //                WHERE b.FULL_NAME IS NOT NULL AND b.STATUS NOT IN ('02','05') AND STOCKBLK.BLOCK_CODE='2'";

            table = cn.taobang2(sql);
            setDataSource(table);

            //for (int i = 0; i < dataGridView_full.Rows.Count; i++)
            //{
            //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
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
                dataGridView_full.Columns[4].Frozen = true;
                tk_Full.Enabled = true;
                export_full.Enabled = true;
                button2.Enabled = true;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void fr_StockBLK_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            sql = @"select count(*) from SPPRICE with(nolock) where sku_id in (select SKU_ID from SKU_DEF with(nolock) where status <> '02')";
            //p_max = cn.Gan_max_progressbar(sql);
            //            sql = @"select a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định',dbo.tcvn2unicode(b.SUPP_NAME) as 'Tên NCC' ,a.SKU_ID as 'Mã Hàng',c.SKU_CODE,dbo.tcvn2unicode(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT', c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            //,PCPR_CODE as 'Vùng Giá' from SPPRICE a
            //left join SUPPLIER as b on a.supp_id=b.supp_id
            //left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            //where a.sku_id in (select SKU_ID from SKU_DEF where status <> '02') order by a.SKU_ID";
            progressBar1.Maximum = cn.Gan_max_progressbar(sql);

            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            //sql = @"select SUPP_ID as 'Mã NCC', SKU_ID as 'Mã Hàng', SPPRICE as 'Giá Nhập chỉ định', PCPR_CODE as 'Vùng Giá' from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02') order by SKU_ID";
            //select* from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02')
            sql = @"select  a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
                    ,a.SKU_ID as 'Mã Hàng', c.BARCODE                     ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
                    ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm'                     ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
                    , c.TAX_RATE as 'Thuế Bán', SPPRICE as 'Giá Nhập chỉ định'                    , PREFPR as 'Giá Vốn chỉ định' , iif( f.tax_rate is null ,'Not Set', CAST(f.tax_rate as varchar(10))) as 'Thuế Nhập'    --, iif(  LEN(ISNULL(a.tax_code,''))=0, 'Not Set',a.tax_code ) as 'Thuế Nhập'  --f.tax_code as 'Thuế Nhập'--
                    ,PCPR_CODE as 'Vùng Giá'                     ,c.STATUS as 'Trạng Thái'                    ,c.ITEM_TYPE as 'Loại hàng'
                    ,e.OPEN_DATE ,e.MODI_DATE
					from  DSMART12.dbo.SPPRICE a with(nolock) 
                    left join  DSMART16.dbo.SUPPLIER as b with(nolock) on a.supp_id=b.supp_id
                    left join  DSMART16.dbo.SKU_DEF as c with(nolock) on a.SKU_ID=c.SKU_ID
					 left join  DSMART16.dbo.GOODS as e with(nolock) on right(left(a.SKU_ID,8),6)=e.GOODS_ID
					 left join  DSMART16.dbo.TAX_TYPE as f with(nolock) on  a.tax_code = f.tax_code
                    where c.status <> '02' order by a.SKU_ID";


            //  Ngày tạo, loại hàng, trạng thái ----Giá bán,Barcode, Mã nhóm, tên nhóm, 


            SQLtoExcel(sql, "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
            System.Diagnostics.Process.Start("D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
        }

        private void SQLtoExcel(string query, string Output)
        {
            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".csv";
            //SqlConnection conn = new SqlConnection("Server=192.168.1.18;Database=dsmart12;User Id=daisy;Password=hanoi");
            SqlConnection conn = cn.getcon1();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
            {
                // Loop through the fields and add headers
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    //Converter.TCVN3ToUnicode (dr.GetName(i));
                    string name = dr.GetName(i);
                    if (name.Contains(","))
                        name = "\"" + name + "\"";
                    fs.Write(name + ",");

                }
                fs.WriteLine();

                // Loop through the rows and output the data
                int dong = 0;
                while (dr.Read())
                {

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string value = Converter.TCVN3ToUnicode(dr[i].ToString());
                        //if (i == 19 )
                        //{
                        //    //CultureInfo frC = new CultureInfo("fr-FR");
                        //    value = string.Format("{0:MM/dd/yyyy}", value);
                        //    //value = String.Format("Number {0, 0:D5}", value);
                        //}


                        if (value.Contains(","))
                            value = "\"" + value + "\"";

                        fs.Write(value + ",");

                    }
                    fs.WriteLine();
                    progressBar1.Value = dong + 1;
                    dong++;
                }

                fs.Close();
            }

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = app.Workbooks.Open(Filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //wb.TableStyles.

            wb.SaveAs(Output, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wb.Close();
            app.Quit();
            File.Delete(Filename);
        }
        private void dataGridView_full_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //--------------------------------------------------------
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cellValue = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();

                switch (e.ColumnIndex)
                {
                    case 1:
                    case 2:
                        Ma_stk_id.Text = cellValue;
                        break;
                    case 3:
                    case 4:
                        Ma_nhom.Text = cellValue;
                        break;
                    case 5:
                    case 6:
                        Ma_hang.Text = cellValue;
                        break;
                }
            }
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
            // Tạo cột STT, add cột vào tb trước khi đổ dữ liệu vào table >> STT cột đầu tiên
            //DataColumn Col = new DataColumn("STT", typeof(int));
            //dataGridView_full.Columns.Add(Col);

            //......DataGridView1.DataSource = dt;
            if (backgroundWorker1.IsBusy)
            {
                //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                return;
            }


            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            string sql = @" SELECT ROW_NUMBER() OVER (ORDER BY STOCKBLK.STK_ID) AS [STT],STOCKBLK.STK_ID, c.stk_NAME,b.GRP_ID,b.GRP_NAME ,STOCKBLK.SKU_ID,b.FULL_NAME, STOCKBLK.BLOCK_CODE, STOCKBLK.MODI_DATE
                            FROM DSMART16.dbo.STOCKBLK STOCKBLK with(nolock) 
                            left join [172.16.70.30].DATA_DETAIL.dbo.sku_def as b  with(nolock) on STOCKBLK.sku_id=b.SKU_ID 
                            left join [172.16.70.30].DATA_DETAIL.dbo.stock as c  with(nolock) on STOCKBLK.stk_id=c.stk_id
                            where ";
            if ((Ma_nhom.Text == "") && (Ma_hang.Text == "") && (Ma_stk_id.Text == ""))
            {
                progressBar1.Visible = false;
                return;
            }
            if ((Ma_hang.Text != ""))
            { sql += @"  (STOCKBLK.SKU_ID like N'%" + Ma_hang.Text + "%' or b.FULL_NAME like N'%" + (Ma_hang.Text) + "%')"; }
            if ((Ma_nhom.Text != "") && Ma_hang.Text != "")
            { sql += @" and b.GRP_ID like N'%" + Ma_nhom.Text + "%' or b.grp_name like N'%" + (Ma_nhom.Text) + "%'"; }
            if ((Ma_nhom.Text != "") && Ma_hang.Text == "")
            { sql += @" b.GRP_ID like N'%" + Ma_nhom.Text + "%' or b.grp_name like N'%" + (Ma_nhom.Text) + "%'"; }
            if ((Ma_nhom.Text == "") && Ma_hang.Text == "" && (Ma_stk_id.Text != ""))
            { sql += @" STOCKBLK.STK_ID like N'%" + Ma_stk_id.Text + "%' or c.stk_NAME like N'%" + (Ma_stk_id.Text) + "%'"; }
            if (((Ma_nhom.Text != "") && (Ma_stk_id.Text != "")) || (Ma_hang.Text != "" && (Ma_stk_id.Text != "")))
            { sql += @" and STOCKBLK.STK_ID like N'%" + Ma_stk_id.Text + "%' or c.stk_NAME like N'%" + (Ma_stk_id.Text) + "%'"; }

            dataGridView_full.DataSource = cn.taobang2(sql);

            //for (int i = 0; i < dataGridView_full.Rows.Count ; i++)
            //{
            //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //}
            progressBar1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                return;
            }
            else
            {

                progressBar1.Visible = true;
                progressBar1.Style = ProgressBarStyle.Marquee;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void ClearTable(DataTable table)
        {
            try
            {
                table.Clear();
                table.Rows.Clear();
                table.Columns.Clear();
            }
            catch (DataException e)
            {
                // Process exception and return.
                Console.WriteLine("Exception of type {0} occurred.",
                    e.GetType());
            }
        }
        private void export_full_Click_Old(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Đang load dữ liệu, xin đợi ...", "Chú Ý !");
                return;
            }
            progressBar1.Visible = true;
            //Creating DataTable.
            DataTable dt_grid_nhap = new DataTable();

            //Adding the Columns.
            foreach (DataGridViewColumn column in dataGridView_full.Columns)
            {
                dt_grid_nhap.Columns.Add(column.HeaderText, column.ValueType);
            }

            //Adding the Rows.
            foreach (DataGridViewRow row in dataGridView_full.Rows)
            {
                dt_grid_nhap.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 2 || cell.ColumnIndex == 4 || cell.ColumnIndex == 6) // || cell.ColumnIndex == 10)
                    {
                        dt_grid_nhap.Rows[dt_grid_nhap.Rows.Count - 1][cell.ColumnIndex] = (cell.Value.ToString());
                    }
                    else
                    {
                        dt_grid_nhap.Rows[dt_grid_nhap.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }

                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt_grid_nhap, "HH_Da_Khoa");

                wb.Worksheet(1).Columns().AdjustToContents();

                //Lấy số ngẫu nhiên
                Random _r = new Random();
                string n = _r.Next(1, 10000).ToString();

                string Filename = "D:\\HH_Da_Khoa_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx";

                //dt_grid_nhap.Clear();
                ClearTable(dt_grid_nhap);
                //Save the Excel file.
                wb.SaveAs(Filename);

                //System.Diagnostics.Process.Start("D:\\Zone_Price_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
                System.Diagnostics.Process.Start(Filename);
                progressBar1.Visible = false;
            }
        }

        private void dataGridView_full_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Ma_hang_TextChanged(object sender, EventArgs e)
        {

        }


        private async void export_full_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Đang load dữ liệu, xin đợi ...", "Chú Ý !");
                return;
            }

            progressBar1.Visible = true;
            export_full.Enabled = false;

            int totalRows = dataGridView_full.Rows.Count;
            int maxRowsPerSheet = 800000; // Số dòng tối đa trên mỗi sheet

            await Task.Run(() =>
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = null;
                    int sheetNumber = 1;
                    int currentRow = 1; // Vị trí hiện tại trên sheet

                    for (int startIndex = 0; startIndex < totalRows; startIndex += maxRowsPerSheet)
                    {
                        int endIndex = Math.Min(startIndex + maxRowsPerSheet, totalRows);

                        DataTable dt_chunk = new DataTable();

                        foreach (DataGridViewColumn column in dataGridView_full.Columns)
                        {
                            dt_chunk.Columns.Add(column.HeaderText, column.ValueType);
                        }

                        for (int rowIndex = startIndex; rowIndex < endIndex; rowIndex++)
                        {
                            DataGridViewRow row = dataGridView_full.Rows[rowIndex];
                            DataRow newRow = dt_chunk.NewRow();

                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                //if (cell.ColumnIndex == 2 || cell.ColumnIndex == 4 || cell.ColumnIndex == 6)
                                //{
                                //    newRow[cell.ColumnIndex] = Converter.TCVN3ToUnicode(cell.Value.ToString());
                                //}
                                //else
                                //{
                                    newRow[cell.ColumnIndex] = cell.Value.ToString();
                                //}
                            }

                            dt_chunk.Rows.Add(newRow);
                        }

                        if (currentRow + dt_chunk.Rows.Count > maxRowsPerSheet)
                        {
                            // Nếu số dòng vượt quá ngưỡng, chuyển sang sheet mới
                            worksheet = excelPackage.Workbook.Worksheets.Add($"Sheet{sheetNumber}");
                            sheetNumber++;
                            currentRow = 1;
                        }

                        if (worksheet == null)
                        {
                            worksheet = excelPackage.Workbook.Worksheets.Add($"Sheet{sheetNumber}");
                            sheetNumber++;
                        }

                        for (int i = 0; i < dt_chunk.Columns.Count; i++)
                        {
                            worksheet.Cells[currentRow, i + 1].Value = dt_chunk.Columns[i].ColumnName;
                        }

                        for (int i = 0; i < dt_chunk.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt_chunk.Columns.Count; j++)
                            {
                                worksheet.Cells[currentRow + i + 1, j + 1].Value = dt_chunk.Rows[i][j];
                            }
                        }

                        currentRow += dt_chunk.Rows.Count;
                    }

                    string randomNum = new Random().Next(1, 10000).ToString();
                    string fileName = $"D:\\HH_Da_Khoa_{DateTime.Today.Day}{DateTime.Today.Month}{DateTime.Today.Year}_{randomNum}.xlsx";

                    FileInfo excelFile = new FileInfo(fileName);
                    excelPackage.SaveAs(excelFile);

                    // Mở file Excel sau khi xuất xong
                    System.Diagnostics.Process.Start(fileName);
                }
            });

            progressBar1.Visible = false;
            export_full.Enabled = true;

        }






    }

}
