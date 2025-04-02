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
using System.Threading;
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
    public partial class rpt_StkISQty_Order_SUM : Form
    {

        public rpt_StkISQty_Order_SUM()
        {
            InitializeComponent();
            //Shown += new EventHandler(rpt_StkISQty_Order_SUM_Shown);
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
            Node_Id.Text = "";
            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)   from dbo.stock   a with(nolock) 
                              inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                              where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1' and a.PCPR_CODE='901'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }
        ConnectDB cn = new ConnectDB();
        DateTime da;
        //this.Ma_NCC.GotFocus += Ma_NCC_GotFocus;
        //this.Ma_NCC.Click += Ma_NCC_Click;
        DataTable table = new DataTable();
        DataTable table1 = new DataTable();
        DataSet table_set = new DataSet();
        DataSet table_set1 = new DataSet();
        //ConnectDB cn = new ConnectDB();
        string dk;
        int dong, dong1 = 0;
        int cot, cot1 = 0;
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
                rpt_StkISQty_Order_SUM f3 = (rpt_StkISQty_Order_SUM)Application.OpenForms["rpt_StkISQty_Order_SUM"];
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
        void rpt_StkISQty_Order_SUM_Shown(object sender, EventArgs e)
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

            string sql1 = "rpt_StkISQty_Order_CoreMini_New";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            table_set1 = cn.taobang_from_Procedure_Parameter4(sql1, frdate, todate, Node_Id.Text.ToString());

            //dataGridView_recap.DataSource=table_set1.Tables[0];

            //table = table1.TableName("met");
            //table1.TableName.
            //Response.clear()
            //    Creating DataTable.
            //DataTable dt_grid_nhap = new DataTable();

            foreach (DataRow dong_row in table_set1.Tables[0].Rows)
            {
                dong_row[3] = Converter.TCVN3ToUnicode(dong_row[3].ToString());
                dong_row[4] = Converter.TCVN3ToUnicode(dong_row[4].ToString());
            }
            foreach (DataColumn cot_cot in table_set1.Tables[0].Columns)
            {
                cot_cot.ColumnName = Converter.TCVN3ToUnicode(cot_cot.ToString());
            }




            foreach (DataRow dong_row in table_set1.Tables[1].Rows)
            {
                dong_row[4] = Converter.TCVN3ToUnicode(dong_row[4].ToString());
                dong_row[6] = Converter.TCVN3ToUnicode(dong_row[6].ToString());
                //dong_row[3] = Converter.TCVN3ToUnicode(dong_row[5].ToString());
                //dong_row[4] = Converter.TCVN3ToUnicode(dong_row[7].ToString());
            }
            foreach (DataColumn cot_cot in table_set1.Tables[1].Columns)
            {
                cot_cot.ColumnName = Converter.TCVN3ToUnicode(cot_cot.ToString());
            }


            DataRow totalsRow = table_set1.Tables[1].NewRow();
            foreach (DataColumn col in table_set1.Tables[1].Columns)
            {
                int colTotal = 0;
                if (col.ColumnName.Contains("_1_") || col.ColumnName.Contains("_2_") || col.ColumnName.Contains("_3_") || col.ColumnName.Contains("_4_"))
                {
                    int abc = 0;
                    foreach (DataRow row in col.Table.Rows)
                    {
                        string test = row[col].ToString();
                        if (test != "")
                        {
                            colTotal += Int32.Parse(row[col].ToString());
                            abc++;
                        }
                    }
                    totalsRow[col.ColumnName] = colTotal;
                }
                else if (col.ColumnName.Contains("_ngay"))
                {
                    totalsRow[col.ColumnName] = todate.Value.ToString();
                }
                else if (col.ColumnName.Contains("STT"))
                {
                    totalsRow[col.ColumnName] = "99999";
                }
                else if (col.ColumnName.Contains("Ten Level 2"))
                {
                    totalsRow[col.ColumnName] = "Tổng cộng";
                }
                else
                {
                    totalsRow[col.ColumnName] = colTotal;
                }

            }
            table_set1.Tables[1].Rows.Add(totalsRow);
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

            setDataSource(table_set1);
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

        internal delegate void SetDataSourceDelegate(DataSet table);
        private void setDataSource(DataSet table)
        {
            // Invoke method if required:
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource), table);
            }
            else
            {
                dataGridView_full.DataSource = table_set1.Tables[0];
                dataGridView_recap.DataSource = table_set1.Tables[1];


                progressBar1.Visible = false;
                if (dataGridView_full.Columns.Count > 4) { dataGridView_full.Columns[4].Frozen = true; }
                if (dataGridView_recap.Columns.Count > 6) { dataGridView_recap.Columns[6].Frozen = true; }
                timer1.Stop();
                tk_Full.Enabled = true;
                export_full.Enabled = true;
                //dataGridView_recap. .FooterRow.Cells(k).Font.Bold = True
                for (int i = 0; i < dataGridView_recap.Columns.Count; i++)
                {
                    if (dataGridView_recap.Columns[i].Name.Contains("_1_") || dataGridView_recap.Columns[i].Name.Contains("_2_") || dataGridView_recap.Columns[i].Name.Contains("_3_") || dataGridView_recap.Columns[i].Name.Contains("_4_"))
                    {
                        dataGridView_recap.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView_recap.Columns[i].DefaultCellStyle.Format = "#,##";  //"N2";
                    }
                }
            }
        }
        internal delegate void SetDataSourceDelegate1(DataTable table);
        private void setDataSource_DataTable1(DataTable table, DataGridView dtgrv)
        {
            // Invoke method if required:
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource), table);
            }
            else
            {

                //////////////DataRow totalsRow = table.NewRow();
                //////////////foreach (DataColumn col in table.Columns)
                //////////////{
                //////////////    int colTotal = 0;
                //////////////    if (col.ColumnName.Contains("_1_") || col.ColumnName.Contains("_2_") || col.ColumnName.Contains("_3_") || col.ColumnName.Contains("_4_"))
                //////////////    {
                //////////////        foreach (DataRow row in col.Table.Rows)
                //////////////        {
                //////////////            colTotal += Int32.Parse(row[col].ToString());
                //////////////        }
                //////////////    }
                //////////////    totalsRow[col.ColumnName] = colTotal;
                //////////////}
                //////////////table.Rows.Add(totalsRow);


                dtgrv.DataSource = table;
                //dataGridView_recap.DataSource = table;
                if (dataGridView_recap.Columns.Count > 0)
                {
                    for (int i = 0; i < dataGridView_recap.Columns.Count; i++)
                    {
                        if (dataGridView_recap.Columns[i].Name.Contains("_1_") || dataGridView_recap.Columns[i].Name.Contains("_2_") || dataGridView_recap.Columns[i].Name.Contains("_3_") || dataGridView_recap.Columns[i].Name.Contains("_4_"))
                        {
                            dataGridView_recap.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView_recap.Columns[i].DefaultCellStyle.Format = "#,##";
                        }
                    }

                    //dataGridView_recap.Columns[9].DefaultCellStyle.Format = "#,##";
                    //dataGridView_recap.Columns[8].DefaultCellStyle.Format = "#,##";
                    //dataGridView_recap.Columns[13].DefaultCellStyle.Format = "N2";
                    //dataGridView_recap.Columns[23].DefaultCellStyle.Format = "#,##";
                }
                progressBar1.Visible = false;
                //dataGridView_full.Columns[4].Frozen = true;
                //dataGridView_recap.Columns[6].Frozen = true;
                timer1.Stop();
                tk_Full.Enabled = true;
                export_full.Enabled = true;

                //for (int i = 0; i < dataGridView_full.Rows.Count; i++)
                //{
                //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
                //}

            }
        }

        private void rpt_StkISQty_Order_SUM_Load(object sender, EventArgs e)
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
        private void Ma_NCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != ','))
            {
                e.Handled = true;
                MessageBox.Show("Chỉ nhập ký tự số .", "Chú ý !");
            }

            if ((e.KeyChar == ',') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
        private void tk_Full_Click(object sender, EventArgs e)
        {
            // Kiểm tra điều kiện
            string phrase = Node_Id.Text;
            phrase = phrase.Replace(" ", "");
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
                string sql1 = @"select count(*)  from STK_INFO with(nolock) where right(left(stk_id,4),3) in (" + dk + ")";
                i = cn.Gan_max_progressbar(sql1);
                if (i == 0)
                {
                    MessageBox.Show("Điều kiện Siêu Thị/Vùng.. không có." + "\n" + "Hãy kiểm tra lại .", "Chú ý !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();
            //bientoancuc.
            bientoancuc.tenfilenhap = DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_";

            ClearTable(table1);
            ClearTable(table);
            ClearDataset(table_set1);
            dataGridView_full.Columns.Clear();
            dataGridView_full.Refresh();
            dataGridView_recap.Columns.Clear();
            dataGridView_recap.Refresh();
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
        private void ClearDataset(DataSet table)
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

        private void copyAlltoClipboard(DataGridView dtgr)
        {
            dtgr.SelectAll();
            DataObject dataObj = dtgr.GetClipboardContent();
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
            if (dataGridView_full.RowCount == 0)
            { return; }
            //ClearTable(table);
            GC.Collect();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "STK_Qty_On_Order_Sum.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Copy DataGridView results to clipboard
                    copyAlltoClipboard(dataGridView_full);

                    object misValue = System.Reflection.Missing.Value;  // sau khi copy vào Clipboard bộ nhớ chiếm : 787 MB
                    Excel.Application xlexcel = new Excel.Application();

                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                    Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Name = "Detail";
                    // Format column D as text before pasting results, this was required for my data
                    Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                    rng.NumberFormat = "@";
                    Excel.Range rng1 = xlWorkSheet.get_Range("G:G").Cells;
                    rng1.NumberFormat = "@";


                    // Paste clipboard results to worksheet range
                    Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                    // Delete blank column A and select cell A1
                    Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                    delRng.Delete(Type.Missing);
                    xlWorkSheet.get_Range("A1").Select();
                    ////-----------------------------------------------------------------
                    ///// Copy DataGridView results to clipboard
                    copyAlltoClipboard(dataGridView_recap);
                    //xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    ////Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                    Excel.Worksheet xlWorkSheet1 = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                    Excel.Worksheet xlWorkSheet2 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet2.Name = "Recap-1248 SKU_ID";
                    // Format column D as text before pasting results, this was required for my data
                    Excel.Range rng2 = xlWorkSheet2.get_Range("D:D").Cells;
                    rng2.NumberFormat = "@";
                    Excel.Range rng3 = xlWorkSheet2.get_Range("G:G").Cells;
                    rng3.NumberFormat = "@";
                    // Paste clipboard results to worksheet range
                    xlWorkSheet2.Activate();
                    Excel.Range CR1 = (Excel.Range)xlWorkSheet2.Cells[1, 1];
                    CR1.Select();

                    xlWorkSheet2.PasteSpecial(CR1, false, false, Type.Missing, Type.Missing, Type.Missing, true);
                    // Delete blank column A and select cell A1
                    Excel.Range delRng1 = xlWorkSheet2.get_Range("A:A").Cells;
                    delRng1.Delete(Type.Missing);
                    xlWorkSheet2.get_Range("A1").Select();
                    //-----------------------------------------------------------------------------------------------------

                    // Save the excel file under the captured location from the SaveFileDialog
                    try
                    {
                        xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkSheet.Columns.AutoFit();
                        xlWorkSheet2.Columns.AutoFit();
                        xlexcel.DisplayAlerts = true;
                        xlWorkBook.Close(true, misValue, misValue);
                        xlexcel.Quit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error,file maybe is opening！\n" + ex.Message);
                        return;
                    }

                    //xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    //xlWorkSheet.Columns.AutoFit();
                    //xlexcel.DisplayAlerts = true;
                    //xlWorkBook.Close(true, misValue, misValue);
                    //xlexcel.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dataGridView_full.ClearSelection();
                    dataGridView_recap.ClearSelection();

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


        private void Node_Id_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(Node_Id.Text.ToString(), "Danh sách Vùng");
        }

        private void check_all_CheckedChanged(object sender, EventArgs e)
        {
            //Node_Id.Text = "Toàn bộ Mart/MiniMart";
            ////Node_Id.Enabled = false;
            //Node_Id.ReadOnly = true;
            //Node_Id.BackColor = System.Drawing.SystemColors.Window;
            Node_Id.Enabled = true;
            Node_Id.ReadOnly = false;
            Node_Id.Text = "";
            Node_Id.Focus();
        }

        private void check_mart_CheckedChanged(object sender, EventArgs e)
        {
            if (check_mart.Checked == true)
                Node_Id.Text = "";
            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)  from stock   a with(nolock) 
                            inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                            where left(b.GRP_ID,2)='01' and a.type='01' and a.STATUS ='1'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }


        private void check_minimart_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart.Checked == true)
                Node_Id.Text = "";
            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)  from stock   a with(nolock) 
                            inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                            where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }

        private void check_minimart_hn_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart_hn.Checked == true)
                Node_Id.Text = "";
            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)   from dbo.stock   a with(nolock) 
                              inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                              where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1' and a.PCPR_CODE='901'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }

        private void check_minimart_sg_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart_sg.Checked == true)
                Node_Id.Text = "";

            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)   from dbo.stock   a with(nolock) 
                              inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                              where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1' and a.PCPR_CODE<>'901'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }

        private void btm_Fillter_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "Details")
            {
                if ((dataGridView_full.DataSource as DataTable) != null)
                {
                    if (txt_Fillter.Text.Length != 0)
                    {

                        //(dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = string.Format("sku_id LIKE '%{0}%'", txt_Fillter.Text);
                        (dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = string.Format("sku_id LIKE '%'", txt_Fillter.Text);

                        string rowFilter = string.Format("sku_id LIKE '%{0}%'", txt_Fillter.Text);
                        rowFilter += string.Format("or dept_id LIKE '%{0}%'", txt_Fillter.Text);
                        rowFilter += string.Format("or grp_id LIKE '%{0}%'", txt_Fillter.Text);
                        rowFilter += string.Format("or grp_name LIKE '%{0}%'", txt_Fillter.Text);
                        rowFilter += string.Format("or full_name LIKE '%{0}%'", txt_Fillter.Text);
                        (dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                    }
                    else
                    {
                        (dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = string.Format("sku_id LIKE '%'", txt_Fillter.Text);
                        //dataGridView_full.DataSource = table1;
                        //dataGridView_full.Refresh();
                    }
                }
            }
        }

        private void Show_all_Click(object sender, EventArgs e)
        {
            if ((dataGridView_full.DataSource as DataTable) is null)
            { return; }
            txt_Fillter.Text = "";
            (dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = string.Format("sku_id LIKE '%'", txt_Fillter.Text);
        }

        private void dataGridView_full_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable table2 = new DataTable();
            /// -----------------------------test//----------------------------------------------------------------------------------
            string sql1 = @"Select * from BRG_Info with(nolock) ";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            //table = cn.taobang_from_Procedure(sql1);
            table2 = cn.taobang(sql1);
            setDataSource_DataTable1(table2, dataGridView_full);
            DataTable table3 = new DataTable();
            /// -----------------------------test//----------------------------------------------------------------------------------
            string sql2 = @"Select * from NHAP_ORDER_SUM with(nolock) ";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            //table = cn.taobang_from_Procedure(sql1);
            table3 = cn.taobang(sql2);
            setDataSource_DataTable1(table3, dataGridView_recap);
            //dataGridView_full = table;
        }
        private bool DataGridviewImportToExcel(DataGridView[] dgv, string fileName)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel file|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0)
                return false;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("can not create Excel");
                return false;
            }
            Workbooks workbooks = xlApp.Workbooks;
            Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

            for (int index = 0; index < dgv.Length; index++)
            {
                for (int i = 0; i < dgv[index].ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dgv[index].Columns[i].HeaderText;
                }

                for (int r = 0; r < dgv[index].Rows.Count; r++)
                {
                    for (int i = 0; i < dgv[index].ColumnCount; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = dgv[index].Rows[r].Cells[i].Value;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();
                if (index < dgv.Length - 1)
                    worksheet.Name = @"firstSheet";
                worksheet = (Worksheet)workbook.Worksheets.Add();

            }

            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error,file maybe is opening！\n" + ex.Message);
                    return false;
                }
            }
            xlApp.Quit();
            GC.Collect();
            MessageBox.Show("File： " + fileName + ".xls save Successfully", "tip ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        private void New_Export_Click(object sender, EventArgs e)
        {
            if (dataGridView_full.RowCount == 0)
            { return; }

            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            da = DateTime.Now;
            timer1.Start();
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(New_Export_Click1));

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            //timer1.Stop();
        }
        private void New_Export_Click1()
        {
            //if (dataGridView_full.RowCount == 0)
            //{ return; }
            //ClearTable(table);
            GC.Collect();
            ////Lấy số ngẫu nhiên
            //Random _r = new Random();
            //string n = _r.Next(1, 10000).ToString();
            //da = DateTime.Now;
            //timer1.Start();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "STK_Qty_On_Order_Sum.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //F003_Splash.ShowSplash();
                    //------------------------------------------------------------------
                    //Lấy số ngẫu nhiên
                    Random _r = new Random();
                    string n = _r.Next(1, 10000).ToString();

                    string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_1.csv";
                    string Filename1 = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_2.csv";

                    using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
                    {

                        // Loop through the fields and add headers
                        //Adding the Columns.
                        dong = 0;
                        cot = 0;
                        dong1 = 0;
                        cot1 = 0;
                        foreach (DataGridViewColumn column in dataGridView_full.Columns)
                        {
                            //dt_grid_nhap.Columns.Add(column.HeaderText, column.ValueType);
                            //dt_grid_nhap.Columns.Add(Converter.TCVN3ToUnicode(column.HeaderText), column.ValueType);
                            string name = column.HeaderText;
                            if (name.Contains(","))
                                name = "\"" + name + "\"";
                            fs.Write(name + ",");
                            cot++;

                        }
                        fs.WriteLine();
                        //Adding the Rows.
                        //int dong = 0;
                        foreach (DataGridViewRow row in dataGridView_full.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string value = cell.Value.ToString();
                                if (value.Contains(","))
                                    value = "\"" + value + "\"";

                                fs.Write(value + ",");
                            }
                            fs.WriteLine();
                            dong++;
                        }
                        fs.Close();
                        dong++;
                    }
                    ///---------------------------------------------------- Sheet 2 --------------------------------
                    using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename1, false, Encoding.UTF8))
                    {
                        int dong = 0;
                        // Loop through the fields and add headers
                        //Adding the Columns.
                        foreach (DataGridViewColumn column in dataGridView_recap.Columns)
                        {

                            string name = column.HeaderText;
                            if (name.Contains(","))
                                name = "\"" + name + "\"";
                            fs.Write(name + ",");
                            cot1++;
                        }
                        fs.WriteLine();
                        //Adding the Rows.
                        //int dong = 0;
                        foreach (DataGridViewRow row in dataGridView_recap.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string value = cell.Value.ToString();
                                if (value.Contains(","))
                                    value = "\"" + value + "\"";

                                fs.Write(value + ",");
                            }
                            fs.WriteLine();
                            dong1++;
                        }
                        fs.Close();
                        dong1++;
                    }

                    //----------------------------

                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook curWorkBook = null;
                    Workbook destWorkbook = null;
                    Worksheet workSheet = null;
                    Worksheet workSheet1 = null;
                    Worksheet newWorksheet = null;
                    Object defaultArg = Type.Missing;
                    app.DisplayAlerts = false;
                    try
                    {
                        curWorkBook = app.Workbooks.Open(Filename1, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg);
                        workSheet = (Worksheet)curWorkBook.Sheets[1];
                        workSheet.UsedRange.Copy(defaultArg);
                        destWorkbook = app.Workbooks.Open(Filename, defaultArg, false, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg);
                        newWorksheet = (Worksheet)destWorkbook.Worksheets.Add(defaultArg, defaultArg, defaultArg, defaultArg);
                        newWorksheet.UsedRange._PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                        workSheet = (Worksheet)destWorkbook.Sheets[1];
                        workSheet.Activate();
                        workSheet.Name = "Recap";
                        //newWorksheet.Columns.AutoFit();
                        workSheet.Range["F:F"].NumberFormat = "0";
                        workSheet.Columns[cot1].NumberFormat = "d/m/yyyy";
                        workSheet.Columns[cot1 - 1].NumberFormat = "d/m/yyyy";
                        //workSheet.Cells.Replace("0", "-");
                        workSheet.Rows[1].WrapText = true;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].Borders.LineStyle = XlLineStyle.xlContinuous;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].VerticalAlignment = XlVAlign.xlVAlignCenter;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong1, cot1]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatTable5);
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].Interior.Color = XlRgbColor.rgbSkyBlue;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong1, cot1]].Borders.LineStyle = XlLineStyle.xlContinuous;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong1, cot1]].font.size = 10;
                        workSheet.Rows[1].Insert();
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong1, cot1]].Borders.LineStyle = XlLineStyle.xlContinuous;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].Interior.Color = XlRgbColor.rgbBlue;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].font.Color = XlRgbColor.rgbWhite;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].RowHeight = 24;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[2, cot1]].font.size = 9;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].VerticalAlignment = XlVAlign.xlVAlignCenter;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot1]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        workSheet.Columns.AutoFit();
                        workSheet.Cells[1, 9] = "Tổng Cộng";  //frdate.CustomFormat = "dd/MM/yyyy";
                        workSheet.Range[workSheet.Cells[3, 9], workSheet.Cells[dong1 + 1, cot1 - 2]].NumberFormat = "#,##";
                        workSheet.Range[workSheet.Cells[dong1 + 1, "A"], workSheet.Cells[dong1 + 1, cot1]].font.bold = true;
                        workSheet.Cells[1, 5] = "Core MiniMart, Thời gian : " + frdate.Value.ToString("dd/M/yyyy") + " - " + todate.Value.ToString("dd/M/yyyy");
                        workSheet.Range[workSheet.Cells[1, 9], workSheet.Cells[1, 10]].Merge();
                        workSheet.Cells[2, 9] = " Tổng mã có tồn";
                        workSheet.Cells[2, 10] = " Tổng mã đang đặt";
                        //workSheet.Columns[1] = "aaaaa";
                        //newWorksheet.Activate();
                        int next = 0;
                        for (int i = 1; i <= cot1; i++)
                        {
                            try
                            {
                                string name = workSheet.Cells[2, i].Value.ToString();
                                if (name.Contains("_4_"))
                                {
                                    // Do Something // 
                                    workSheet.Columns[i].font.Color = XlRgbColor.rgbRed;
                                    //workSheet.Cells[1, i - 3] =  name.Length;
                                    workSheet.Cells[1, i - 3] = name.Substring(19);
                                    workSheet.Range[workSheet.Cells[1, i - 3], workSheet.Cells[1, i]].Merge();
                                    if (next == 0)
                                    {
                                        workSheet.Range[workSheet.Cells[2, i - 3], workSheet.Cells[2, i]].Interior.Color = XlRgbColor.rgbPeachPuff;
                                        next = 1;
                                        workSheet.Cells[2, i - 3] = " Tổng mã ";
                                        workSheet.Cells[2, i - 2] = " Số mã có tồn ";
                                        workSheet.Cells[2, i - 1] = " Số mã đang chờ giao ";
                                        workSheet.Cells[2, i] = " Số mã phủ hàng thiếu ";
                                    }
                                    else
                                    {
                                        workSheet.Range[workSheet.Cells[2, i - 3], workSheet.Cells[2, i]].Interior.Color = XlRgbColor.rgbLightYellow;
                                        next = 0;
                                        workSheet.Cells[2, i - 3] = " Tổng mã ";
                                        workSheet.Cells[2, i - 2] = " Số mã có tồn ";
                                        workSheet.Cells[2, i - 1] = " Số mã đang chờ giao ";
                                        workSheet.Cells[2, i] = " Số mã phủ hàng thiếu ";
                                    }
                                    //workSheet.Columns[i].Font.Color = Color.Red; ;
                                    //workSheet.Range["B6"].CellStyle.Font.Color = ExcelKnownColors.Green;
                                }
                            }
                            catch (Exception exe)
                            {
                            }
                        }

                        //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong1+2, cot1]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatLocalFormat2);



                        workSheet = (Worksheet)destWorkbook.Sheets[2];
                        workSheet.Activate();
                        workSheet.Name = "Detail";
                        workSheet.Rows[1].WrapText = true;
                        workSheet.Range["F:F"].NumberFormat = "0";
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].VerticalAlignment = XlVAlign.xlVAlignCenter;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong, cot]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatList3);
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].Borders.LineStyle = XlLineStyle.xlContinuous;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong, cot]].font.size = 10;
                        workSheet.Rows[1].Insert();
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong, cot]].Borders.LineStyle = XlLineStyle.xlContinuous;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].Interior.Color = XlRgbColor.rgbBlue;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].font.Color = XlRgbColor.rgbWhite;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[2, cot]].font.size = 9;
                        workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[2, cot]].RowHeight = 24;
                        //workSheet.Columns[3].Interior.Color = XlRgbColor.rgbRed ;
                        //workSheet.Columns[7].font.Color = XlRgbColor.rgbRed;
                        //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong, cot]].ShowZeroValues = false;
                        //workSheet.Range[workSheet.Cells[1, workSheet.Columns[5]], workSheet.Cells[dong, workSheet.Columns[5]]].Interior.Color = XlRgbColor.rgbRed;
                        // duyệt tuần tự từ dòng thứ 2 đến dòng cuối cùng của file. lưu ý file excel bắt đầu từ số 1 không phải số 0

                        for (int i = 1; i <= cot; i++)
                        {
                            try
                            {
                                string name = workSheet.Cells[2, i].Value.ToString();


                                if (name.Contains("_2_"))
                                {
                                    // Do Something // 
                                    workSheet.Columns[i].font.Color = XlRgbColor.rgbRed;
                                    workSheet.Cells[1, i - 1] = name.Substring(15);
                                    workSheet.Range[workSheet.Cells[1, i - 1], workSheet.Cells[1, i]].Merge();
                                    if (next == 0)
                                    {
                                        workSheet.Range[workSheet.Cells[2, i - 1], workSheet.Cells[2, i]].Interior.Color = XlRgbColor.rgbPeachPuff;
                                        next = 1;
                                        workSheet.Cells[2, i - 1] = " SL Tồn ";
                                        workSheet.Cells[2, i] = " SL chờ giao ";
                                    }
                                    else
                                    {
                                        workSheet.Range[workSheet.Cells[2, i - 1], workSheet.Cells[2, i]].Interior.Color = XlRgbColor.rgbLightYellow;
                                        next = 0;
                                        workSheet.Cells[2, i - 1] = " SL Tồn ";
                                        workSheet.Cells[2, i] = " SL chờ giao ";
                                    }
                                }


                            }

                            catch (Exception exe)
                            {

                            }
                        }
                        workSheet = (Worksheet)destWorkbook.Sheets[1];
                        workSheet.Activate();
                        workSheet.Cells[1, 1].select();
                        //F003_Splash.CloseSplash();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                        File.Delete(Filename);
                        File.Delete(Filename1);
                        timer1.Stop();
                        progressBar1.Visible = false;
                    }
                    //xlWorkbookNormal //xlOpenXMLWorkbook

                    try
                    {
                        destWorkbook.SaveAs(sfd.FileName, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, ConflictResolution: Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        curWorkBook.Close(false, defaultArg, defaultArg);
                        destWorkbook.Close(false, defaultArg, defaultArg);
                        releaseObject(newWorksheet);
                        //releaseObject(workSheet1);
                        releaseObject(workSheet);
                        releaseObject(curWorkBook);
                        releaseObject(destWorkbook);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(new Form() { TopMost = true }, "Chú ý!");

                        MessageBox.Show("File đang mở...\n" + "Đóng file rồi xuất BC lại...\n" + ex.Message, "Chú ý!");
                        curWorkBook.Close(false, defaultArg, defaultArg);
                        destWorkbook.Close(false, defaultArg, defaultArg);
                        releaseObject(newWorksheet);
                        releaseObject(workSheet);
                        //releaseObject(workSheet1);
                        releaseObject(curWorkBook);
                        releaseObject(destWorkbook);
                        return;
                    }

                    // EndFlashing 
                    //F003_Splash.CloseSplash();

                    app.Quit();


                    File.Delete(Filename);
                    File.Delete(Filename1);

                    //GridViewtoExcel(dataGridView_full, dataGridView_recap, sfd.FileName);

                    System.Diagnostics.Process.Start(sfd.FileName);
                    GC.Collect();

                    //////////// Open the newly saved excel file
                    //////////if (File.Exists(sfd.FileName))
                    //////////    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Chả biết lỗi gì ? Chụp ảnh gửi mềnh ! ");
                    return;
                    timer1.Stop();
                    progressBar1.Visible = false;
                }
            }
            timer1.Stop();
            //progressBar1.Visible = false;

            Invoke(new System.Action(() => this.progressBar1.Visible = false));
        }

        private void GridViewtoExcel(DataGridView grView, DataGridView grView1, string Output)
        {



            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_1.csv";
            string Filename1 = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_2.csv";

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
            {

                // Loop through the fields and add headers
                //Adding the Columns.
                dong = 0;
                cot = 0;
                dong1 = 0;
                cot1 = 0;
                foreach (DataGridViewColumn column in grView.Columns)
                {
                    //dt_grid_nhap.Columns.Add(column.HeaderText, column.ValueType);
                    //dt_grid_nhap.Columns.Add(Converter.TCVN3ToUnicode(column.HeaderText), column.ValueType);
                    string name = column.HeaderText;
                    if (name.Contains(","))
                        name = "\"" + name + "\"";
                    fs.Write(name + ",");
                    cot++;

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
                    dong++;
                }
                fs.Close();
                dong++;
            }
            ///---------------------------------------------------- Sheet 2 --------------------------------
            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename1, false, Encoding.UTF8))
            {
                int dong = 0;
                // Loop through the fields and add headers
                //Adding the Columns.
                foreach (DataGridViewColumn column in grView1.Columns)
                {

                    string name = column.HeaderText;
                    if (name.Contains(","))
                        name = "\"" + name + "\"";
                    fs.Write(name + ",");
                    cot1++;
                }
                fs.WriteLine();
                //Adding the Rows.
                //int dong = 0;
                foreach (DataGridViewRow row in grView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string value = cell.Value.ToString();
                        if (value.Contains(","))
                            value = "\"" + value + "\"";

                        fs.Write(value + ",");
                    }
                    fs.WriteLine();
                    dong1++;
                }
                fs.Close();
                dong1++;
            }

            //----------------------------

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook curWorkBook = null;
            Workbook destWorkbook = null;
            Worksheet workSheet = null;
            Worksheet workSheet1 = null;
            Worksheet newWorksheet = null;
            Object defaultArg = Type.Missing;
            app.DisplayAlerts = false;
            try
            {
                curWorkBook = app.Workbooks.Open(Filename1, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg);
                workSheet = (Worksheet)curWorkBook.Sheets[1];
                workSheet.UsedRange.Copy(defaultArg);
                destWorkbook = app.Workbooks.Open(Filename, defaultArg, false, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg);
                newWorksheet = (Worksheet)destWorkbook.Worksheets.Add(defaultArg, defaultArg, defaultArg, defaultArg);
                newWorksheet.UsedRange._PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                newWorksheet.Activate();
                newWorksheet.Name = "Recap";
                newWorksheet.Columns.AutoFit();
                newWorksheet.Range["F:F"].NumberFormat = "0";
                newWorksheet = (Worksheet)destWorkbook.Sheets[1];
                newWorksheet.Activate();
                newWorksheet.Range[newWorksheet.Cells[1, "A"], newWorksheet.Cells[dong1, cot1]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatClassic2);
                workSheet = (Worksheet)destWorkbook.Sheets[2];
                workSheet.Activate();
                workSheet.Name = "Detail";
                workSheet.Rows[1].WrapText = true;
                workSheet.Range["F:F"].NumberFormat = "0";
                workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].VerticalAlignment = XlVAlign.xlVAlignCenter;
                workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong, cot]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatList3);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            //xlWorkbookNormal //xlOpenXMLWorkbook

            try
            {
                destWorkbook.SaveAs(Output, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, ConflictResolution: Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                curWorkBook.Close(false, defaultArg, defaultArg);
                destWorkbook.Close(false, defaultArg, defaultArg);
                releaseObject(newWorksheet);
                //releaseObject(workSheet1);
                releaseObject(workSheet);
                releaseObject(curWorkBook);
                releaseObject(destWorkbook);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ,file maybe is opening！\n" + ex.Message);
                curWorkBook.Close(false, defaultArg, defaultArg);
                destWorkbook.Close(false, defaultArg, defaultArg);
                releaseObject(newWorksheet);
                releaseObject(workSheet);
                //releaseObject(workSheet1);
                releaseObject(curWorkBook);
                releaseObject(destWorkbook);
                return;
            }


            app.Quit();


            File.Delete(Filename);
            File.Delete(Filename1);
        }
        private void New_Export_Click_luu(object sender, EventArgs e)
        {
            if (dataGridView_full.RowCount == 0)
            { return; }
            //ClearTable(table);
            GC.Collect();
            ////Lấy số ngẫu nhiên
            //Random _r = new Random();
            //string n = _r.Next(1, 10000).ToString();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "STK_Qty_On_Order_Sum.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //------------------------------------------------------------------


                    GridViewtoExcel(dataGridView_full, dataGridView_recap, sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                    GC.Collect();
                    //-----------------------------------------------------------------------------------------------------

                    //Save the excel file under the captured location from the SaveFileDialog
                    //try
                    //{
                    //    xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    //    xlWorkSheet.Columns.AutoFit();
                    //    xlWorkSheet2.Columns.AutoFit();
                    //    xlexcel.DisplayAlerts = true;
                    //    xlWorkBook.Close(true, misValue, misValue);
                    //    xlexcel.Quit();
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("error,file maybe is opening！\n" + ex.Message);
                    //    return;
                    //}



                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Chả biết lỗi gì ? Chụp ảnh gửi mềnh ! ");
                    return;
                }
            }
        }

        private void GridViewtoExcel_luu(DataGridView grView, DataGridView grView1, string Output)
        {
            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_1.csv";
            string Filename1 = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_2.csv";

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
            {

                // Loop through the fields and add headers
                //Adding the Columns.
                dong = 0;
                cot = 0;
                dong1 = 0;
                cot1 = 0;
                foreach (DataGridViewColumn column in grView.Columns)
                {
                    //dt_grid_nhap.Columns.Add(column.HeaderText, column.ValueType);
                    //dt_grid_nhap.Columns.Add(Converter.TCVN3ToUnicode(column.HeaderText), column.ValueType);
                    string name = column.HeaderText;
                    if (name.Contains(","))
                        name = "\"" + name + "\"";
                    fs.Write(name + ",");
                    cot++;

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
                    dong++;
                }
                fs.Close();
                dong++;
            }
            ///---------------------------------------------------- Sheet 2 --------------------------------
            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename1, false, Encoding.UTF8))
            {
                int dong = 0;
                // Loop through the fields and add headers
                //Adding the Columns.
                foreach (DataGridViewColumn column in grView1.Columns)
                {

                    string name = column.HeaderText;
                    if (name.Contains(","))
                        name = "\"" + name + "\"";
                    fs.Write(name + ",");
                    cot1++;
                }
                fs.WriteLine();
                //Adding the Rows.
                //int dong = 0;
                foreach (DataGridViewRow row in grView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string value = cell.Value.ToString();
                        if (value.Contains(","))
                            value = "\"" + value + "\"";

                        fs.Write(value + ",");
                    }
                    fs.WriteLine();
                    dong1++;
                }
                fs.Close();
                dong1++;
            }

            //----------------------------

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook curWorkBook = null;
            Workbook destWorkbook = null;
            Worksheet workSheet = null;
            Worksheet workSheet1 = null;
            Worksheet newWorksheet = null;
            Object defaultArg = Type.Missing;
            app.DisplayAlerts = false;
            try
            {
                // Copy the source sheet
                curWorkBook = app.Workbooks.Open(Filename1, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg);
                workSheet = (Worksheet)curWorkBook.Sheets[1];
                //workSheet.Activate();
                //workSheet.Name = "a lo s e";
                //workSheet.Columns.AutoFit();
                workSheet.UsedRange.Copy(defaultArg);

                // Paste on destination sheet
                destWorkbook = app.Workbooks.Open(Filename, defaultArg, false, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg, defaultArg);
                newWorksheet = (Worksheet)destWorkbook.Worksheets.Add(defaultArg, defaultArg, defaultArg, defaultArg);
                //workSheet.Name = "Detail";
                //workSheet.Columns.AutoFit();
                newWorksheet.UsedRange._PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                newWorksheet.Activate();
                newWorksheet.Name = "Recap";
                //newWorksheet.Range["1:1"].WrapText();
                newWorksheet.Columns.AutoFit();
                newWorksheet.Range["F:F"].NumberFormat = "0";
                newWorksheet = (Worksheet)destWorkbook.Sheets[1];
                newWorksheet.Activate();
                newWorksheet.Range[newWorksheet.Cells[1, "A"], newWorksheet.Cells[dong1, cot1]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatClassic2);
                //newWorksheet.Range[newWorksheet.Cells[1, "A"], newWorksheet.Cells[dong1, cot1]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatList2); forat định dạng mầu xanh, 2 dòng 1
                workSheet = (Worksheet)destWorkbook.Sheets[2];
                workSheet.Activate();
                workSheet.Name = "Detail";
                //workSheet.Range["A1"].WrapText = true;
                //workSheet.Range["A1:A" + cot].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //workSheet.Rows[1].VerticalAlignment = XlVAlign.xlVAlignCenter;
                //workSheet.Rows[1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                workSheet.Rows[1].WrapText = true;
                //workSheet.Columns.AutoFit();
                workSheet.Range["F:F"].NumberFormat = "0";
                //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]]. .selection();
                //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].Select;
                workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].VerticalAlignment = XlVAlign.xlVAlignCenter;
                workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong, cot]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatList3);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            //xlWorkbookNormal //xlOpenXMLWorkbook

            try
            {
                destWorkbook.SaveAs(Output, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, ConflictResolution: Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //wb.SaveAs              fullFilePath, ,,AccessMode.xlExclusive,ConflictResolution:= Excel.XlSaveConflictResolution.xlLocalSessionChanges
                curWorkBook.Close(false, defaultArg, defaultArg);
                destWorkbook.Close(false, defaultArg, defaultArg);
                releaseObject(newWorksheet);
                //releaseObject(workSheet1);
                releaseObject(workSheet);
                releaseObject(curWorkBook);
                releaseObject(destWorkbook);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ,file maybe is opening！\n" + ex.Message);
                curWorkBook.Close(false, defaultArg, defaultArg);
                destWorkbook.Close(false, defaultArg, defaultArg);
                releaseObject(newWorksheet);
                releaseObject(workSheet);
                //releaseObject(workSheet1);
                releaseObject(curWorkBook);
                releaseObject(destWorkbook);
                return;
            }
            ////////////destWorkbook.SaveAs(Output, Excel.XlFileFormat.xlWorkbookNormal,      misValue, misValue,     misValue,        misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue,          misValue,     misValue,     misValue);
            //////////if (curWorkBook != null)
            //////////{
            //////////    //curWorkBook.Save();
            //////////    curWorkBook.Close(false, defaultArg, defaultArg);

            //////////}

            //////////if (destWorkbook != null)
            //////////{
            //////////    //destWorkbook.Save();
            //////////    //destWorkbook.SaveAs(Output, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //////////    destWorkbook.Close(false, defaultArg, defaultArg);

            //////////}

            app.Quit();

            //-------------------------------

            ////////////////////Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            //////////////////////XLWorkbook wb = new XLWorkbook();
            ////////////////////Workbook wb = app.Workbooks.Open(Filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //////////////////////Microsoft.Office.Interop.Excel.Worksheet ss = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];
            //////////////////////ss.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ////////////////////////ss.SaveAs(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            ////////////////////Workbook wb1 = app.Workbooks.Open(Filename1, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ////////////////////wb1.Worksheets[0].CopyFrom(wb.Worksheets[0]);



            //////////////////////Microsoft.Office.Interop.Excel.Worksheet workSheet_disk = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
            //////////////////////Microsoft.Office.Interop.Excel.Worksheet workSheet_copy = (Microsoft.Office.Interop.Excel.Worksheet)wb1.ActiveSheet;
            //////////////////////workSheet_disk.Copy(workSheet_copy);
            //////////////////////wb.Sheet.Copy(wb1.Sheets[3]);

            //////////////////////Worksheet sheet = wb.Worksheets.Copy(Worksheet sheet1 = wb1.Worksheets[0]);

            //////////////////////Excel.Worksheet worksheet1 = ((Excel.Worksheet)Application.wb.Worksheets[1]);
            //////////////////////Excel.Worksheet worksheet3 = ((Excel.Worksheet)Application.ActiveWorkbook.Worksheets[1]);
            //////////////////////worksheet1.Copy(worksheet3);

            //////////////////////wb.Worksheets.Add(table1, "Tồn_Bán");
            //////////////////////Workbook wb = app.Workbooks.Add(Type.Missing);
            //////////////////////Worksheets ws= wb.Worksheets.Add(Filename);
            //////////////////////Workbook wb3  = app.Workbooks.Add(Filename);
            //////////////////////Workbook wb4 = app.Workbooks.Add(Filename1);
            //////////////////////Workbook wb1 = app.Workbooks.Open(Filename1, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //////////////////////wb.Worksheets.Add();
            //////////////////////wb.Worksheets.Add(Filename1);
            //////////////////////wb1.Sheets(1).Copy , wb.Sheets(1)

            //////////////////////Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)wb1.ActiveSheet;
            //////////////////////Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)wb1.Sheets[0].Copy();
            //////////////////////Microsoft.Office.Interop.Excel.Worksheet workSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[1].paste();
            //////////////////////Worksheet sheet = wb.Worksheets.Copy;
            //////////////////////Worksheet sheet1 = wb1.Worksheets[0];
            //////////////////////sheet1.Copy(sheet1);
            //////////////////////sheet.Paste(sheet);
            //////////////////////Worksheet sheet111 = wb1.Worksheets[0];

            //////////////////////wb.Sheets.Add(); //' add a new worksheet
            ////////////////////////wb1.Sheets.;
            //////////////////////wb1.Sheets(1).Copy , wb.Sheets(1)
            //////////////////////wb1.Sheets(1).copy;
            //////////////////////wb.Sheets[1].paste();
            //////////////////////wb.Sheets[1].name = "ReCap"; //--'rename the worksheet


            //////////////////////Excel.Worksheet xlWorkSheetToUpload = default(Excel.Worksheet);
            //////////////////////xlWorkSheetToUpload = xlAppToUpload.Sheets["Sheet1"];

            //////////////////////Excel._Worksheet workSheet = (Excel.Worksheet)app.ActiveSheet;
            //////////////////////workSheet[0].Name = "aaaaa";
            //////////////////////workSheet.Columns.AutoFit();
            ////////////////////////workSheet.Range["A1", "H5"].AutoFormat(
            ////////////////////////        Excel.XlRangeAutoFormat.xlRangeAutoFormatList1); //xlRangeAutoFormat3DEffects1); //, xlRangeAutoFormatClassic2) ;
            //////////////////////workSheet.Range["F:F"].NumberFormat = "0";

            //////////////////////workSheet.Range["A6", "H10"].AutoFormat(
            //////////////////////        Excel.XlRangeAutoFormat.xlRangeAutoFormatList3);              // Thích cái Format này

            //////////////////////workSheet.Range["A11", "H15"].AutoFormat(
            //////////////////////        Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);
            //////////////////////workSheet.Range["A16", "H22"].AutoFormat(
            //////////////////////        Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);

            //////////////////////workSheet.Range["A23", "H30"].AutoFormat(
            //////////////////////        Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic3);
            //////////////////////workSheet.Range["A31", "H35"].AutoFormat(
            //////////////////////        Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat1);

            //////////////////////workSheet.Range["A1", "H10"].AutoFormat(
            //////////////////////     Excel.XlCalcMemNumberFormatType.xlNumberFormatTypeNumber();
            ////////////////////////workSheet.Range["A1", "H100"].AutoFormat(Excel.XlRangeAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1, object Number, object Font, object Alignment, object Border, object Pattern, object Width);


            //////////////////////Worksheets a = (Worksheets)app.Worksheets;
            ////////////////////////wb.Worksheets.
            //////////////////////a.. [2].NumberFormat = "@";      // column as a text
            ////////////////////wb1.Close(Type.Missing);
            ////////////////////wb.SaveAs(Output, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //////////////////////XlFileFormat. . .AutoFit();
            //////////////////////wb.Close();
            //app.Quit();
            File.Delete(Filename);
            File.Delete(Filename1);
        }
    }

}
