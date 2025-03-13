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
    public partial class Fr_BRG_Target_Imp : Form
    {

        DataTable dataTable = new DataTable();
        // Khai báo biến apiUrl ở mức độ của lớp
        private string apiUrl = "";
        private bool isDownloading = false; // Biến cờ để theo dõi trạng thái tải xuống

        public Fr_BRG_Target_Imp()
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

        private void Fr_BRG_Target_Imp_Load(object sender, EventArgs e)
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
            string file_temp = "Template-Target_tap_doan.xlsx";

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

        private void import_data_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                //// Truncate table trước khi nhập dữ liệu mới
                //using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString_BRGReports_97_30))
                //{
                //    connection.Open();
                //    using (SqlCommand command = new SqlCommand("TRUNCATE TABLE DATA_IMP", connection))
                //    {
                //        command.ExecuteNonQuery();
                //    }
                //}

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheetByItems = package.Workbook.Worksheets["Sheet1"];
                    //var worksheetKeHoachVH = package.Workbook.Worksheets["Kế hoạch VH"];
                    if (worksheetByItems == null)
                    {
                        MessageBox.Show("Không tìm thấy các Sheet1 trong file Excel.");
                        return;
                    }

                    int totalRows = (worksheetByItems.Dimension.End.Row - 2);
                    #region D dòng dưới là cách tính lại totalRows loại bỏ các dòng trống ngay,stk_id,tg_dt,tg_bill
                    //------------------------------------------------------------------------
                    //int totalRows = 0;

                    //for (int row = 3; row <= worksheetByItems.Dimension.End.Row; row++)
                    //{
                    //    if (!string.IsNullOrWhiteSpace(worksheetByItems.Cells[row, 1].Text) &&
                    //        !string.IsNullOrWhiteSpace(worksheetByItems.Cells[row, 3].Text) &&
                    //        !string.IsNullOrWhiteSpace(worksheetByItems.Cells[row, 5].Text) &&
                    //        !string.IsNullOrWhiteSpace(worksheetByItems.Cells[row, 6].Text))
                    //    {
                    //        totalRows++;
                    //    }
                    //}
                    //------------------------------------------------------------------------
                    #endregion 
                    int currentRow = 0;

                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalRows;
                    progressBar1.Value = 0;

                    using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
                    {
                        connection.Open();

                        // Insert dữ liệu từ sheet 'By items'
                        for (int row = 3; row <= worksheetByItems.Dimension.End.Row; row++)
                        {
                            var ngay = GetCellValue(worksheetByItems.Cells[row, 1])?.ToString();
                            var stk_id = GetCellValue(worksheetByItems.Cells[row, 3]).ToString();
                            var tg_dt = GetCellValue(worksheetByItems.Cells[row, 5]).ToString();
                            var tg_bill = GetCellValue(worksheetByItems.Cells[row, 6]).ToString();

                            if (!string.IsNullOrEmpty(ngay) && !string.IsNullOrEmpty(stk_id) && !string.IsNullOrEmpty(tg_dt) && !string.IsNullOrEmpty(tg_bill))
                            {
                                string query = @"
                                update Target_DS_BRG set  status=0,modi_date=getdate() where PRD_CODE=@PRD_CODE and RPS_CODE=@RPS_CODE and status=1
                                INSERT INTO Target_DS_BRG (
                                    [PRD_CODE],[RPS_CODE],[TRG_TYPE],[TRG_AMT],[STATUS],create_date
                                ) VALUES (
                                    @PRD_CODE, @RPS_CODE, '01', @TRG_AMT, 1,getdate()
                                )
                                INSERT INTO Target_DS_BRG(
                                    [PRD_CODE],[RPS_CODE],[TRG_TYPE],[TRG_AMT],[STATUS],create_date
                                ) VALUES(
                                    @PRD_CODE, @RPS_CODE, '03', @TRG_AMT_Bill, 1,getdate()
                                )";
                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    //// Chuyển đổi giá trị ô từ object sang string
                                    //string cellValue = Convert.ToString(GetCellValue(worksheetByItems.Cells[row, 1]));
                                    //// Định dạng giá trị để phù hợp với kiểu char(8)
                                    //string formattedValue = FormatToChar8(cellValue);
                                    ////command.Parameters.AddWithValue("@PRD_CODE", GetCellValue(worksheetByItems.Cells[row, 1]));

                                    command.Parameters.AddWithValue("@PRD_CODE", GetCellValue(worksheetByItems.Cells[row, 1]).ToString().Substring(0, 8));

                                    ////command.Parameters.AddWithValue("@PRD_CODE", formattedValue);
                                    command.Parameters.AddWithValue("@RPS_CODE", GetCellValue(worksheetByItems.Cells[row, 3]).ToString().Substring(0, 3));
                                    //command.Parameters.AddWithValue("@TRG_TYPE", "01");
                                    //command.Parameters.AddWithValue("@TRG_TYPE_Bill", "03");
                                    command.Parameters.AddWithValue("@TRG_AMT", Convert.ToString(GetCellValue(worksheetByItems.Cells[row, 5])).Split('.')[0]);
                                    command.Parameters.AddWithValue("@TRG_AMT_Bill", Convert.ToString(GetCellValue(worksheetByItems.Cells[row, 6])).Split('.')[0]);
                                    //command.Parameters.AddWithValue("@STATUS", "1");l
                                    command.ExecuteNonQuery();
                                }
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Không tìm thấy các Sheet1 trong file Excel.");
                                //}    
                            }
                            currentRow++;
                            progressBar1.Value = currentRow;

                        }
                    }
                }
            }

            MessageBox.Show("Import completed successfully.");
        }

        // Hàm để lấy giá trị ô và định dạng lại để phù hợp với kiểu char(8)
        private string FormatToChar8(string cellValue)
        {
            if (cellValue == null)
                return new string(' ', 8); // Trả về khoảng trắng nếu giá trị là null

            // Cắt hoặc điền khoảng trắng để đảm bảo độ dài 8 ký tự
            if (cellValue.Length > 8)
                return cellValue.Substring(0, 8); // Cắt nếu dài hơn 8 ký tự
            else
                return cellValue.PadRight(8); // Điền khoảng trắng nếu ngắn hơn 8 ký tự
        }


        private object GetCellValue(ExcelRange cell)
        {
            if (cell.Value == null)
            {
                return DBNull.Value;
            }

            if (cell.Value is ExcelErrorValue)
            {
                return DBNull.Value;
            }

            return cell.Value;
        }

        private async void Exp_data_Click(object sender, EventArgs e)
        {

            string Dirpath = Directory.GetCurrentDirectory();
            string Template = Path.Combine(Dirpath, "Media", "Template");
            string file_temp = "Template_Marketting";
            string dateAndRandom = DateTime.Now.ToString("yyyyMMddHHmm") + "_" + new Random().Next(100, 999); // Khởi tạo giá trị mặc định
            DateTime Ngay_Xem = DateTime.Now.AddDays(-1);
            if (int.TryParse(textBox1.Text, out int days))
            {
                Ngay_Xem = DateTime.Now.AddDays(-days);
                dateAndRandom = Ngay_Xem.ToString("yyyyMMddHHmm") + "_" + new Random().Next(100, 999);
            }


            string templatePath = Path.Combine(Template, $"{file_temp}.xlsx");

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = $"{file_temp}_{dateAndRandom}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Thiết lập progressBar1 vào chế độ Marquee
                    progressBar1.Style = ProgressBarStyle.Marquee;

                    string selectedDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                    string uniqueFileName = GetUniqueFileName(Path.GetFileName(saveFileDialog.FileName), selectedDirectory);
                    Exp_data.Enabled = false;
                    await RunReportAsync_MKT(templatePath, uniqueFileName);
                    Exp_data.Enabled = true;
                    //MessageBox.Show($"File saved to: {uniqueFileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else
                //{
                //    MessageBox.Show("Operation canceled by the user.", "Export Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
        }
        
        private async Task RunReportAsync_MKT(string templatePath, string savePath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString_BRGReports_97_30))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("rptSalesNow_DS_Manager_BRG", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@ngay", int.TryParse(textBox1.Text);                    
                    // Thêm tham số vào command với giá trị từ textBox1 nếu chuyển đổi thành công, hoặc gán 0 nếu không thành công
                    command.Parameters.AddWithValue("@ngay", int.TryParse(textBox1.Text, out int ngay) ? ngay : 0);
                    command.CommandTimeout = 0;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet2 = package.Workbook.Worksheets["By items"];
                                int row = 10;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        sheet2.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                            }

                            //await reader.NextResultAsync();
                            //if (await reader.ReadAsync())
                            //{
                            //    ExcelWorksheet sheet3 = package.Workbook.Worksheets["Kế hoạch VH"];
                            //    int row = 3;
                            //    do
                            //    {
                            //        for (int col = 0; col < reader.FieldCount; col++)
                            //        {
                            //            sheet3.Cells[row, col + 3].Value = reader.GetValue(col);
                            //        }
                            //        row++;
                            //    } while (await reader.ReadAsync());
                            //}

                            // Tạo một danh sách để lưu trữ dữ liệu từ reader
                            var dataList = new List<object[]>();

                            await reader.NextResultAsync();
                            while (await reader.ReadAsync())
                            {
                                var values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                dataList.Add(values);
                            }

                            // Ghi vào sheet "Kế hoạch VH"
                            ExcelWorksheet sheet3 = package.Workbook.Worksheets["Kế hoạch VH"];
                            int row1 = 3;
                            foreach (var data in dataList)
                            {
                                for (int col = 0; col < data.Length; col++)
                                {
                                    sheet3.Cells[row1, col + 3].Value = data[col];
                                }
                                row1++;
                            }

                            // Ghi vào sheet "Data chi tiết"
                            ExcelWorksheet sheet4 = package.Workbook.Worksheets["Data chi tiết"];
                            int row2 = 3;
                            foreach (var data in dataList)
                            {
                                for (int col = 0; col < 2; col++)  // Chỉ lấy 2 cột đầu tiên
                                {
                                    sheet4.Cells[row2, col + 2].Value = data[col];
                                }
                                row2++;
                            }

                            ////await package.SaveAsAsync(new FileInfo(savePath));
                            await Task.Run(() => package.SaveAsAsync(new FileInfo(savePath)));
                            ////await package.SaveAsAsync(new FileInfo(savePath)).ConfigureAwait(false);
                            // Sau khi công việc dài hạn hoàn tất, bạn có thể đặt lại progressBar1 vào chế độ mặc định


                            System.Diagnostics.Process.Start(new ProcessStartInfo(savePath) { UseShellExecute = true });
                            progressBar1.Style = ProgressBarStyle.Blocks;
                        }
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự nhập vào không phải là số
            // cho phép các ký tự số (0-9) và phím Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ngăn chặn ký tự không hợp lệ
            }
        }


        private void exp_target_BRG_Click(object sender, EventArgs e)
        {
            string dirPath = Directory.GetCurrentDirectory();
            string templateDir = Path.Combine(dirPath, "Media", "Template");
            string fileTemp = "Template-Target_tap_doan";
            string dateAndRandom = MONTH_Target_BRG.Value.ToString("MMyyyy") + "_" + new Random().Next(100, 999); // Khởi tạo giá trị mặc định

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

                            string sqlQuery = @"
                    SELECT 
                        a.PRD_CODE,
                        RIGHT(a.PRD_CODE, 2) AS Ngay,
                        a.RPS_CODE,
                        '' AS NHOM,
                        a.TRG_AMT AS DT,
                        b.TRG_AMT AS BILL
                    FROM [HCRC_Report_Center_V2].[dbo].[Target_DS_BRG] AS a
                    LEFT JOIN [HCRC_Report_Center_V2].[dbo].[Target_DS_BRG] AS b 
                        ON b.PRD_CODE = a.PRD_CODE 
                        AND b.RPS_CODE = a.RPS_CODE 
                        AND b.TRG_TYPE = '03'
                    WHERE a.TRG_TYPE = '01'
                        AND a.Create_DATE IS NOT NULL 
                        AND a.STATUS = 1
                        AND YEAR(a.PRD_CODE) = @Year 
                        AND MONTH(a.PRD_CODE) = @Month";

                            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                            {
                                // Thêm tham số cho câu lệnh SQL
                                command.Parameters.AddWithValue("@Year", MONTH_Target_BRG.Value.Year);
                                command.Parameters.AddWithValue("@Month", MONTH_Target_BRG.Value.Month);

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