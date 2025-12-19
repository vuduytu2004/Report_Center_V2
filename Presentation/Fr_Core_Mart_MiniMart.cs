using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using OfficeOpenXml;
using Report_Center.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;


//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Report_Center.Presentation
{
    public partial class Fr_Core_Mart_MiniMart : Form
    {

        DataTable dataTable = new DataTable();
        // Khai báo biến apiUrl ở mức độ của lớp
        private string apiUrl = "";
        private bool isDownloading = false; // Biến cờ để theo dõi trạng thái tải xuống

        public Fr_Core_Mart_MiniMart()
        {
            InitializeComponent();
            InitializeCustomComponents();

            //PopulateTreeView();
            //frdate.CustomFormat = "dd/MM/yyyy";
            //todate.CustomFormat = "dd/MM/yyyy";
            //frdate.MaxDate = DateTime.Now.AddDays(-1); // Chỉ cho phép chọn đến ngày hôm qua
            //todate.MaxDate = DateTime.Now.AddDays(-1); // Chỉ cho phép chọn đến ngày hôm qua
            //DateTime Ngay_Xem = DateTime.Now.AddDays(-textBox1.ToInt()) //DateTime.Now- textBox1.ToInt();
            //DateTime Ngay_Xem = DateTime.Now.AddDays(-(int.TryParse(textBox1.Text, out int days)));
        }
        private void InitializeCustomComponents()
        {
            // Gắn sự kiện cho Label
            label1.Click += new EventHandler(button1_Click);
            label1.MouseEnter += new EventHandler(label1_MouseEnter);
            label1.MouseLeave += new EventHandler(label1_MouseLeave);
        }
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                label.Font = new System.Drawing.Font(label.Font, FontStyle.Italic | FontStyle.Underline);
            }
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                label.Font = new System.Drawing.Font(label.Font, FontStyle.Regular | FontStyle.Underline);
            }
        }

        private void Fr_Core_Mart_MiniMart_Load(object sender, EventArgs e)
        {
            // Gọi hàm PopulateTreeView trong sự kiện Load của Form
            //PopulateTreeView();
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetUniqueFileName(string baseFileName, string directory)
        {
            string fileName = Path.Combine(directory, baseFileName);
            int counter = 1;

            while (File.Exists(fileName))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);
                string fileExtension = Path.GetExtension(baseFileName);

                fileName = Path.Combine(directory, $"{fileNameWithoutExtension}_{counter}{fileExtension}");
                counter++;
            }
            return fileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy đường dẫn hiện tại của ứng dụng
            string Dirpath = Directory.GetCurrentDirectory();

            // Đường dẫn tới thư mục Template
            string Template = Path.Combine(Dirpath, "Media", "Template");

            // Tên file trong Template
            string file_temp = "Template-Core_Mart_MiniMart.xlsx";

            // Kết hợp đường dẫn thư mục Template với tên file để tạo đường dẫn đầy đủ tới file
            string templatePath = Path.Combine(Template, file_temp);

            // Khởi tạo SaveFileDialog để người dùng chọn nơi lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = file_temp; // Tên file mặc định

            // Hiển thị hộp thoại SaveFileDialog và kiểm tra xem người dùng đã chọn một đường dẫn hợp lệ chưa
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn mà người dùng đã chọn
                string savePath = saveFileDialog.FileName;

                try
                {
                    // Copy file từ Template đến vị trí mới mà người dùng đã chọn
                    File.Copy(templatePath, savePath, true);
                    MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có vấn đề xảy ra trong quá trình sao chép file
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void import_data_Click_luu(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string filePath = openFileDialog.FileName;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var ws = package.Workbook.Worksheets["Sheet1"];
                if (ws == null)
                {
                    MessageBox.Show("Không tìm thấy Sheet1.");
                    return;
                }

                // 1️⃣ Tạo DataTable tương ứng bảng SQL
                DataTable dt = new DataTable();
                dt.Columns.Add("STK_ID", typeof(string));
                dt.Columns.Add("SKU_ID", typeof(string));
                dt.Columns.Add("Rpt_key", typeof(string));
                dt.Columns.Add("LAST_UPDATE", typeof(DateTime));

                // 2️⃣ Đọc Excel → DataTable
                for (int row = 3; row <= ws.Dimension.End.Row; row++)
                {
                    string stk_id = ws.Cells[row, 1].Text.Trim();
                    string sku_id = ws.Cells[row, 2].Text.Trim();
                    string rpt_key = ws.Cells[row, 3].Text.Trim();

                    // bỏ dòng trống hoàn toàn
                    if (string.IsNullOrEmpty(stk_id) &&
                        string.IsNullOrEmpty(sku_id) &&
                        string.IsNullOrEmpty(rpt_key))
                        continue;

                    // validate lỗi
                    if (string.IsNullOrEmpty(stk_id) ||
                        string.IsNullOrEmpty(sku_id) ||
                        string.IsNullOrEmpty(rpt_key))
                    {
                        MessageBox.Show($"Dữ liệu lỗi tại dòng {row}");
                        return;
                    }

                    dt.Rows.Add(stk_id, sku_id, rpt_key, DateTime.Now);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu hợp lệ.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(bientoancuc.connectionString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 3️⃣ Update dữ liệu cũ
                            string sqlUpdate = @"UPDATE Core_Mart_MiniMart 
                                         SET status = 0, LAST_UPDATE = GETDATE()
                                         WHERE status = 1";

                            using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            // 4️⃣ Bulk insert (CỰC NHANH)
                            using (SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
                            {
                                bulk.DestinationTableName = "Core_Mart_MiniMart";

                                bulk.ColumnMappings.Add("STK_ID", "STK_ID");
                                bulk.ColumnMappings.Add("SKU_ID", "SKU_ID");
                                bulk.ColumnMappings.Add("Rpt_key", "Rpt_key");
                                bulk.ColumnMappings.Add("LAST_UPDATE", "LAST_UPDATE");

                                bulk.WriteToServer(dt);
                            }

                            tran.Commit();
                            MessageBox.Show("Import dữ liệu thành công!");
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            MessageBox.Show("Lỗi:\n" + ex.Message);
                        }
                    }
                }
            }
        }
        private void import_data_Click(object sender, EventArgs e)
        {
            import_data.Enabled = false;      // 🔒 khóa nút
            progressBar1.Value = 0;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                import_data.Enabled = true;
                return;
            }

            try
            {
                string filePath = openFileDialog.FileName;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var ws = package.Workbook.Worksheets["Sheet1"];
                    if (ws == null)
                        throw new Exception("Không tìm thấy Sheet1.");

                    // ===== 1️⃣ ĐỌC EXCEL → DATATABLE =====
                    DataTable dt = new DataTable();
                    dt.Columns.Add("STK_ID", typeof(string));
                    dt.Columns.Add("SKU_ID", typeof(string));
                    dt.Columns.Add("Rpt_key", typeof(string));
                    dt.Columns.Add("LAST_UPDATE", typeof(DateTime));

                    int totalExcelRows = ws.Dimension.End.Row - 2;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalExcelRows;

                    int processed = 0;

                    for (int row = 3; row <= ws.Dimension.End.Row; row++)
                    {
                        string stk_id = ws.Cells[row, 1].Text.Trim();
                        string sku_id = ws.Cells[row, 2].Text.Trim();
                        string rpt_key = ws.Cells[row, 3].Text.Trim();

                        if (string.IsNullOrEmpty(stk_id) &&
                            string.IsNullOrEmpty(sku_id) &&
                            string.IsNullOrEmpty(rpt_key))
                            continue;

                        if (string.IsNullOrEmpty(stk_id) ||
                            string.IsNullOrEmpty(sku_id) ||
                            string.IsNullOrEmpty(rpt_key))
                            throw new Exception($"Dữ liệu lỗi tại dòng {row}");

                        dt.Rows.Add(stk_id, sku_id, rpt_key, DateTime.Now);

                        processed++;
                        if (processed % 200 == 0)
                        {
                            progressBar1.Value = Math.Min(processed, progressBar1.Maximum);
                            Application.DoEvents();
                        }
                    }

                    if (dt.Rows.Count == 0)
                        throw new Exception("Không có dữ liệu hợp lệ.");

                    // ===== 2️⃣ GHI SQL (TRANSACTION + BULK) =====
                    using (SqlConnection conn = new SqlConnection(bientoancuc.connectionString))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {
                                // Update dữ liệu cũ
                                string sqlUpdate = @"UPDATE Core_Mart_MiniMart
                                             SET status = 0, LAST_UPDATE = GETDATE()
                                             WHERE status = 1";

                                using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                // Reset progress cho giai đoạn insert
                                progressBar1.Value = 0;
                                progressBar1.Maximum = dt.Rows.Count;

                                using (SqlBulkCopy bulk = new SqlBulkCopy(
                                       conn, SqlBulkCopyOptions.Default, tran))
                                {
                                    bulk.DestinationTableName = "Core_Mart_MiniMart";

                                    bulk.ColumnMappings.Add("STK_ID", "STK_ID");
                                    bulk.ColumnMappings.Add("SKU_ID", "SKU_ID");
                                    bulk.ColumnMappings.Add("Rpt_key", "Rpt_key");
                                    bulk.ColumnMappings.Add("LAST_UPDATE", "LAST_UPDATE");

                                    bulk.NotifyAfter = 500;
                                    bulk.SqlRowsCopied += (s, args) =>
                                    {
                                        this.Invoke(new Action(() =>
                                        {
                                            progressBar1.Value = (int)args.RowsCopied;
                                        }));
                                    };

                                    bulk.WriteToServer(dt);
                                }

                                tran.Commit();
                                MessageBox.Show("Import dữ liệu thành công!",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            catch
                            {
                                tran.Rollback();
                                throw;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // 🔓 bật lại nút dù thành công hay lỗi
                import_data.Enabled = true;
                progressBar1.Value = 0;
            }
        }

        private void exp_target_BRG_Click(object sender, EventArgs e)
        {
            string dirPath = Directory.GetCurrentDirectory();
            string templateDir = Path.Combine(dirPath, "Media", "Template");
            string fileTemp = "Template-Core_Mart_MiniMart";
            string dateAndRandom = DateTime.Now.ToString("MMyyyy") + "_" + new Random().Next(100, 999); // Khởi tạo giá trị mặc định
            //string dateAndRandom = frdate.Value.ToString("yyyyMMddHHmm") + "_" + new Random().Next(100, 999);

            string templatePath = Path.Combine(templateDir, $"{fileTemp}.xlsx");

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = $"{fileTemp}_{dateAndRandom}.xlsx"; // Đặt tên file theo định dạng

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Kết nối đến cơ sở dữ liệu và thực thi truy vấn
                        using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
                        {
                            connection.Open();

                            string sqlQuery = @"SELECT STK_ID,SKU_ID,Rpt_key FROM [Core_Mart_MiniMart] WHERE status=1 ORDER BY STK_ID,SKU_ID";

                            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                            {
                                // Thêm tham số cho câu lệnh SQL
                                //command.Parameters.AddWithValue("@Year", MONTH_Target_BRG.Value.Year);
                                //command.Parameters.AddWithValue("@Month", MONTH_Target_BRG.Value.Month);

                                // Đọc dữ liệu từ truy vấn
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    System.Data.DataTable dataTable = new System.Data.DataTable(); // Sử dụng namespace rõ ràng
                                    dataTable.Load(reader);

                                    // Đảm bảo tên file được tạo đúng
                                    string selectedDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                                    string uniqueFileName = Path.Combine(selectedDirectory, $"{fileTemp}_{dateAndRandom}.xlsx");

                                    //using (var package = new ExcelPackage())
                                    using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                                    {
                                        //var worksheet = package.Workbook.Worksheets.Add("Data");
                                        //var worksheet = package.Workbook.Worksheets["Sheet1"]; // Lấy sheet có sẵn


                                        var worksheet = package.Workbook.Worksheets["Sheet1"]; // Lấy sheet có sẵn

                                        // Kiểm tra nếu worksheet không phải là null
                                        if (worksheet == null)
                                        {
                                            MessageBox.Show("Sheet1 does not exist in the template.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }

                                        worksheet.Cells["A3"].LoadFromDataTable(dataTable, false); // Tải dữ liệu từ DataTable vào worksheet
                                        worksheet.Cells.AutoFitColumns(); // Tự động điều chỉnh độ rộng cột

                                        // Lưu file Excel
                                        package.SaveAs(new FileInfo(uniqueFileName));
                                    }

                                    // Mở file sau khi lưu
                                    System.Diagnostics.Process.Start(uniqueFileName);
                                    //MessageBox.Show($"File saved to: {uniqueFileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}