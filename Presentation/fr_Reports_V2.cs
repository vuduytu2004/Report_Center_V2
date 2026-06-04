using Lib.Utils.Package;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Report_Center.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Report_Center.Main;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Report_Center.Presentation
{
    public partial class fr_Reports_V2 : Form
    {

        DataTable dataTable = new DataTable();
        // Khai báo biến apiUrl ở mức độ của lớp
        private string apiUrl = "";
        private bool isDownloading = false; // Biến cờ để theo dõi trạng thái tải xuống
        int _ma_core;
        public fr_Reports_V2()
        {
            InitializeComponent();
            //PopulateTreeView();
            frdate.CustomFormat = "dd/MM/yyyy";
            todate.CustomFormat = "dd/MM/yyyy";

            // Cập nhật các Label
            report_name.Text = GlobalVariables.txt_Node_name.ToString();
            Pro_name.Text = GlobalVariables.txt_Proc_name;
            gr_para_name.Text = GlobalVariables.txt_Gr_Parameter;
            para_name.Text = GlobalVariables.txt_Parameter;

            object dayValue = GlobalVariables.txt_Day;
            int daysToAdd = Convert.IsDBNull(dayValue) ? 0 : Convert.ToInt32(dayValue);
            frdate.MaxDate = DateTime.Now.AddDays(daysToAdd);
            todate.MaxDate = DateTime.Now.AddDays(daysToAdd);
            //todate.MaxDate = DateTime.Now.AddDays(daysToAdd);



        }

        private void test()
        {
            foreach (Control control in this.Controls.OfType<Control>().OrderBy(c => c.TabIndex))
            {
                // Kiểm tra xem control có phải là GroupBox và có tên nằm trong danh sách groupNames không
                if (control is GroupBox groupBox)
                {

                    // Ẩn GroupBox và thiết lập vị trí dọc
                    groupBox.Visible = false;

                }
            }
            // Tách các tên trong gr_para_name.Text bằng dấu phẩy
            string[] groupNames = gr_para_name.Text.Split(',');

            // Thiết lập vị trí dọc ban đầu
            int currentTop = 126;

            // Duyệt qua tất cả các controls trên form
            //foreach (Control control in this.Controls)
            //{
            foreach (Control control in this.Controls.OfType<Control>().OrderBy(c => c.TabIndex))
            {
                // Kiểm tra xem control có phải là GroupBox và có tên nằm trong danh sách groupNames không
                if (control is GroupBox groupBox)
                {
                    //// In giá trị của groupBox.Text ra output (Console hoặc Debug)
                    ////Console.WriteLine($"GroupBox Text: {groupBox.Name}");

                    if (groupNames.Contains(groupBox.Name))
                    {
                        // Hiển thị GroupBox và thiết lập vị trí dọc
                        groupBox.Visible = true;
                        groupBox.Location = new Point(30, currentTop);

                        // Cập nhật vị trí dọc cho control tiếp theo
                        currentTop += groupBox.Height + 5; // 5 là khoảng cách giữa các GroupBox
                    }
                }
            }
            //if (para_name.Text.Contains("todate"))
            //{
            //    // Nếu chuỗi chứa "todate", đặt thuộc tính Enabled của Date thành True
            //    todate.Enabled = true;
            //}
            //else
            //{
            //    // Ngược lại, đặt thuộc tính Enabled của Date thành False
            //    todate.Enabled = false;
            //}
            todate.Enabled = para_name.Text.Contains("todate");
            NPH.Visible = para_name.Text.IndexOf("NPH", StringComparison.OrdinalIgnoreCase) >= 0;

        }
        //private void PopulateTreeView()
        //{

        //    var test = GlobalVariables.User_Name;
        //    //if (GlobalVariables.User_Name == "ADMIN" || GlobalVariables.User_Name == "Bl160")
        //    //{
        //    //    Node_id.Visible = true;
        //    //    Pro_name.Visible = true;
        //    //    gr_para_name.Visible = true;
        //    //    para_name.Visible = true;
        //    //    lbl_API.Visible = true;

        //    //}    
        //    //else
        //    //{
        //    //    Node_id.Visible = false;
        //    //    Pro_name.Visible = false;
        //    //    gr_para_name.Visible = false;
        //    //    para_name.Visible = false;
        //    //    lbl_API.Visible = false;
        //    //}    
        //    using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
        //    {
        //        connection.Open();
        //        //string query = "SELECT [NodeID],[ParentID],[NodeName],[Proc_name],[Gr_Parameter],[Parameter],[Enable_Check] FROM [TreeNodes_Report] WHERE [Enable_Check]=1;";
        //        string query = "";
        //        if (GlobalVariables.User_Name == "ADMIN")// || GlobalVariables.User_Name == "Bl160")
        //        {
        //            query = "SELECT * FROM [TreeNodes_Report] WHERE [Enable_Check]=1;";

        //        }
        //        else
        //        {
        //            query = $"SELECT * FROM [TreeNodes_Report] WHERE [Enable_Check]=1 and Department in ( select distinct RoleGroupID from UserPermissionRoleGroups where UserPermissionID={GlobalVariables.UserID} );";
        //        }


        //        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        //        //DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);

        //        // Tạo cây nút
        //        TreeNodeCollection nodes = treeView1.Nodes;
        //        CreateTree(dataTable, nodes, "");

        //        // Mở rộng nút gốc
        //        treeView1.ExpandAll();
        //        // Gọi hàm để tô màu các nút trong cây
        //        ColorTreeViewNodes(treeView1.Nodes);
        //    }
        //}
        private void ColorTreeViewNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                // Gọi hàm để tô màu nút hiện tại
                ColorNode(node);


                // Đệ quy gọi hàm cho các nút con
                ColorTreeViewNodes(node.Nodes);
            }
        }

        private void ColorNode(TreeNode node)
        {
            // Lấy thông tin từ Tag của nút
            var nodeInfo = node.Tag;

            // Kiểm tra thông tin không null và thực hiện tô màu dựa trên điều kiện nào đó
            if (nodeInfo != null)
            {
                // Ví dụ: Tô màu đỏ cho các nút có ProcName chứa chuỗi "DIO"
                if (nodeInfo.ToString() == "0")
                {
                    node.BackColor = Color.Aqua;
                }
                // Ví dụ: Tô màu xanh cho các nút có Parameter chứa chuỗi "Tháng"
                else if (nodeInfo.ToString() == "1")
                {
                    node.BackColor = Color.Lavender;
                }
                else
                {
                    node.BackColor = Color.LemonChiffon;
                }
                // Các điều kiện khác có thể thêm tùy theo yêu cầu
            }
        }
        private void CreateTree(DataTable dataTable, TreeNodeCollection nodes, string parentID)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["ParentID"].ToString() == parentID)
                {
                    //ReportNodeInfo nodeInfo = new ReportNodeInfo(nodeName, procName, parameter);
                    TreeNode node = new TreeNode(row["NodeName"].ToString());
                    node.Tag = row["Node_color"].ToString();
                    nodes.Add(node);

                    // Gọi đệ quy để thêm các nút con
                    CreateTree(dataTable, node.Nodes, row["NodeID"].ToString());
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Xử lý sự kiện khi người dùng chọn một nút trong TreeView
            //MessageBox.Show($"Selected Node: {e.Node.Text}", "Node Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Làm mới các Label
            report_name.Text = "";
            Node_id.Text = "";
            Pro_name.Text = "";
            gr_para_name.Text = "";
            para_name.Text = "";
            lbl_API.Text = "";

            // Tìm kiếm trong DataTable
            DataRow[] foundRows = dataTable.Select($"NodeName = '{e.Node.Text}'");

            // Kiểm tra xem có dòng nào được tìm thấy không
            if (foundRows.Length > 0)
            {
                // Lấy thông tin từ dòng đầu tiên tìm thấy (có thể có nhiều dòng, bạn có thể xử lý theo cách khác nếu cần)
                DataRow row = foundRows[0];

                // Cập nhật các Label
                report_name.Text = row["NodeName"].ToString();
                Node_id.Text = row["NodeID"].ToString();
                Pro_name.Text = row["Proc_name"].ToString();
                gr_para_name.Text = row["Gr_Parameter"].ToString();
                para_name.Text = row["Parameter"].ToString();
                lbl_API.Text = row["API"].ToString();

                object dayValue = row["day"];
                int daysToAdd = Convert.IsDBNull(dayValue) ? 0 : Convert.ToInt32(dayValue);
                frdate.MaxDate = DateTime.Now.AddDays(daysToAdd);
                todate.MaxDate = DateTime.Now.AddDays(daysToAdd);
            }
            //gr_fr_to_date.Visible = false;
            //gr_stk_id.Visible = false;
            //gr_dept_id.Visible = false;
            //string[] groupNames1 = gr_para_name.Text.Split(',');
            foreach (Control control in this.Controls.OfType<Control>().OrderBy(c => c.TabIndex))
            {
                // Kiểm tra xem control có phải là GroupBox và có tên nằm trong danh sách groupNames không
                if (control is GroupBox groupBox)
                {

                    // Ẩn GroupBox và thiết lập vị trí dọc
                    groupBox.Visible = false;

                }
            }
            // Tách các tên trong gr_para_name.Text bằng dấu phẩy
            string[] groupNames = gr_para_name.Text.Split(',');

            // Thiết lập vị trí dọc ban đầu
            int currentTop = 126;

            // Duyệt qua tất cả các controls trên form
            //foreach (Control control in this.Controls)
            //{
            foreach (Control control in this.Controls.OfType<Control>().OrderBy(c => c.TabIndex))
            {
                // Kiểm tra xem control có phải là GroupBox và có tên nằm trong danh sách groupNames không
                if (control is GroupBox groupBox)
                {
                    //// In giá trị của groupBox.Text ra output (Console hoặc Debug)
                    ////Console.WriteLine($"GroupBox Text: {groupBox.Name}");

                    if (groupNames.Contains(groupBox.Name))
                    {
                        // Hiển thị GroupBox và thiết lập vị trí dọc
                        groupBox.Visible = true;
                        groupBox.Location = new Point(400, currentTop);

                        // Cập nhật vị trí dọc cho control tiếp theo
                        currentTop += groupBox.Height + 5; // 5 là khoảng cách giữa các GroupBox
                    }
                }
            }
            //if (para_name.Text.Contains("todate"))
            //{
            //    // Nếu chuỗi chứa "todate", đặt thuộc tính Enabled của Date thành True
            //    todate.Enabled = true;
            //}
            //else
            //{
            //    // Ngược lại, đặt thuộc tính Enabled của Date thành False
            //    todate.Enabled = false;
            //}
            todate.Enabled = para_name.Text.Contains("todate");
            NPH.Visible = para_name.Text.IndexOf("NPH", StringComparison.OrdinalIgnoreCase) >= 0;


        }

        public class ComboItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        private void fr_Reports_Load(object sender, EventArgs e)
        {
            // Gọi hàm PopulateTreeView trong sự kiện Load của Form
            //PopulateTreeView();
            test();
            List<ComboItem> items = new List<ComboItem>
                {
                    new ComboItem { Text = "Mart", Value = 1 },
                    new ComboItem { Text = "MiniMart", Value = 2 }
                };
            ma_core.DataSource = items;
            ma_core.DisplayMember = "Text";  // Hiển thị "Mart" hoặc "MiniMart"
            ma_core.ValueMember = "Value";  // Khi chọn thì lấy ra 1 hoặc 2

            ma_core.SelectedIndex = 0; // Chọn mặc định là "Mart"
            // lấy đơn vị , CH cho HRM
            LoadLevel1ManagerOrgName();
        }
        private List<string> _allItems = new List<string>();
        private void LoadLevel1ManagerOrgName_Luu()
        {
            _allItems.Clear();

            string sql = @"
        SELECT DISTINCT Lv1OrgName
        FROM View_KpiEvaluation_Organization
        WHERE Level1ManagerOrgName IS NOT NULL";

            using (SqlConnection conn = new SqlConnection(bientoancuc.connectionString_HRM))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _allItems.Add(rd[0].ToString());
                    }
                }
            }

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(_allItems.ToArray());
        }
        private void LoadLevel1ManagerOrgName()
        {
            _allItems.Clear();

            string sql = @"
        SELECT DISTINCT Lv1OrgName
        FROM View_KpiEvaluation_Organization
        WHERE Level1ManagerOrgName IS NOT NULL";

            using (SqlConnection conn =
                   new SqlConnection(bientoancuc.connectionString_HRM))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _allItems.Add(rd[0].ToString());
                    }
                }
            }

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(_allItems.ToArray());

            AutoCompleteStringCollection ac =
                new AutoCompleteStringCollection();

            ac.AddRange(_allItems.ToArray());

            comboBox1.AutoCompleteMode =
                AutoCompleteMode.SuggestAppend;

            comboBox1.AutoCompleteSource =
                AutoCompleteSource.CustomSource;

            comboBox1.AutoCompleteCustomSource = ac;
        }
        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gr_para_name_TextChanged(object sender, EventArgs e)
        {
            gr_fr_to_date.Visible = false;
            gr_stk_id.Visible = false;
            gr_dept_id.Visible = false;
            // Tách các tên trong gr_para_name.Text bằng dấu phẩy
            string[] groupNames = gr_para_name.Text.Split(',');

            // Thiết lập vị trí dọc ban đầu
            int currentTop = 126;

            // Duyệt qua tất cả các controls trên form
            //foreach (Control control in this.Controls)
            //{
            foreach (Control control in this.Controls.OfType<Control>().OrderBy(c => c.TabIndex))
            {
                // Kiểm tra xem control có phải là GroupBox và có tên nằm trong danh sách groupNames không
                if (control is GroupBox groupBox && groupNames.Contains(groupBox.Text))
                {
                    // Hiển thị GroupBox và thiết lập vị trí dọc
                    groupBox.Visible = true;
                    groupBox.Location = new Point(559, currentTop);

                    // Cập nhật vị trí dọc cho control tiếp theo
                    currentTop += groupBox.Height + 5; // 5 là khoảng cách giữa các GroupBox
                }
            }
        }

        private async void bt_BC_Click(object sender, EventArgs e)
        {
            if (Pro_name.Text == null || Pro_name.Text == "" || Pro_name.Text == "1") { return; }

            using (SqlConnection conn = new SqlConnection(bientoancuc.connectionString))
            {
                conn.Open();

                string sql = @"SELECT COUNT(*) FROM sys.procedures WHERE name = @ProcName";

                using (SqlCommand cmd =
                       new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@ProcName",
                        Pro_name.Text);

                    int count =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show(
                            "Báo cáo đang trong giai đoạn đang phát triển",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        return;
                    }
                }
            }


            progressBar1.Style = ProgressBarStyle.Marquee;

            string Dirpath = Directory.GetCurrentDirectory();
            string Template = Dirpath + @"/Media/Template/";
            string file_temp = "";

            file_temp = $"Template-{Pro_name.Text}.xlsx";
            //string dateAndRandom = DateTime.Now.ToString("yyyyMMdd") + "_" + new Random().Next(1000, 9999);
            string dateAndRandom = frdate.Value.ToString("yyyyMMddHHmm") + "_" + new Random().Next(100, 999);
            string templatePath = Path.Combine(Template, file_temp);

            //var templatePath = RootPathConfig.TemplatePath.Template + "Template-BC-do_phu_ASM.xlsx";
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.AddExtension = true;

                saveFileDialog.FileName = $"Template-{Pro_name.Text}_{dateAndRandom}.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                    string uniqueFileName = GetUniqueFileName(Path.GetFileName(saveFileDialog.FileName), selectedDirectory);

                    // Chạy thủ tục trong một luồng riêng biệt
                    if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry")
                    {
                        await Task.Run(() => RunReportAsync_Total(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_TotalRetailWholesaleByMonth")
                    {
                        await Task.Run(() => RunReportAsync_ByMonth(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_truyen_nhan")
                    {
                        await Task.Run(() => RunReportAsync_truyen_nhan(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_StkISQty_Core_Mart_MiniMart")
                    {
                        await Task.Run(() => RunReportAsync_rpt_StkISQty_Core_Mart_MiniMart(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "Tach_Don_333_314_315")
                    {
                        await Task.Run(() => RunReportAsync_Tach_Don_333_314_315(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_voucher_In_Stock")
                    {
                        await Task.Run(() => RunReportAsync_rpt_voucher_In_Stock(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_Member_Card")
                    {
                        await Task.Run(() => RunReportAsync_rpt_Member_Card(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_Industry_Gross_Profit_Marginy")
                    {
                        await Task.Run(() => RunReportAsync_rpt_Industry_Gross_Profit_Marginy(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_B2B")
                    {
                        await Task.Run(() => RunReportAsync_rpt_B2B(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_timekeeping_details")
                    {
                        await Task.Run(() => RunReportAsync_rpt_timekeeping_details(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_Nhap_Khau")
                    {
                        await Task.Run(() => RunReportAsync_rpt_Nhap_Khau(templatePath, uniqueFileName));
                    }
                    //else if (Pro_name.Text == "HRM_KPI_Thang")
                    //{
                    //    string nameEn = comboBox1.Text;
                    //    DateTime atMonth = todate.Value.Date;
                    //    //await Task.Run(() => RunReportAsync_HRM_KPI_Thang(templatePath, uniqueFileName));
                    //    await Task.Run(() =>RunReportAsync_HRM_KPI_Thang(templatePath,uniqueFileName,atMonth,nameEn));
                    //}
                    else if (Pro_name.Text == "HRM_KPI_Thang")
                    {
                        string Lv1OrgName = comboBox1.Text;
                        DateTime atMonth = frdate.Value.Date;
                        //MessageBox.Show(atMonth.ToString("yyyy-MM-dd"));
                        await RunReportAsync_HRM_KPI_Thang(
                            templatePath,
                            uniqueFileName,
                            atMonth,
                            Lv1OrgName);
                    }
                    else
                    {
                        await Task.Run(() => RunReportAsync(templatePath, uniqueFileName));
                    }
                    Console.WriteLine("File saved to: " + uniqueFileName);
                }
                else
                {
                    progressBar1.Style = ProgressBarStyle.Blocks;

                    Console.WriteLine("Operation canceled by the user.");
                }
            }

            progressBar1.Style = ProgressBarStyle.Blocks;

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
        private async Task RunReportAsync_rpt_B2B(string templatePath, string savePath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (frdate.Visible)
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    //string test = frdate.Value.ToString("yyyyMMdd");
                    if (todate.Visible)
                        command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    //string test1 = todate.Value.ToString("yyyyMMdd");
                    if (dept_id.Visible)
                        command.Parameters.AddWithValue("@dept_id", dept_id.Text);

                    command.CommandTimeout = 0;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                    {
                        // --- Sheet "Target" ---
                        if (await reader.ReadAsync())
                        {
                            ExcelWorksheet sheet1 = package.Workbook.Worksheets["Target"];
                            int row = 8;

                            do
                            {
                                if (reader.FieldCount >= 3)
                                {
                                    sheet1.Cells[row, 5].Value = reader.GetValue(0);  // E
                                    sheet1.Cells[row, 9].Value = reader.GetValue(1);  // I
                                    sheet1.Cells[row, 10].Value = reader.GetValue(2); // J
                                }
                                row++;
                            } while (await reader.ReadAsync());
                        }

                        // --- Sheet "K076" ---
                        await reader.NextResultAsync();
                        if (await reader.ReadAsync())
                        {
                            ExcelWorksheet sheet2 = package.Workbook.Worksheets["K076"];
                            sheet2.Cells["A1"].Value = "Tháng " + frdate.Value.ToString("MM");

                            int row = 6;

                            do
                            {
                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    sheet2.Cells[row, col + 3].Value = reader.GetValue(col);
                                }
                                row++;
                            } while (await reader.ReadAsync());
                        }

                        // --- Sheet "K080" ---
                        await reader.NextResultAsync();
                        if (await reader.ReadAsync())
                        {
                            ExcelWorksheet sheet3 = package.Workbook.Worksheets["K080"];
                            int row = 7;

                            do
                            {
                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    sheet3.Cells[row, col + 4].Value = reader.GetValue(col);
                                }
                                row++;
                            } while (await reader.ReadAsync());
                        }

                        // --- Save and Open File ---
                        await Task.Run(() => package.SaveAs(new FileInfo(savePath)));

                        System.Diagnostics.Process.Start(new ProcessStartInfo(savePath) { UseShellExecute = true });
                        //progressBar1.Style = ProgressBarStyle.Blocks;
                    }
                }
            }
        }

        private async Task RunReportAsync_rpt_B2B_Luu(string templatePath, string savePath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }
                    if (todate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@todate", frdate.Value.ToString("yyyyMMdd"));
                    }
                    if (dept_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@dept_id", dept_id.Text);
                    }
                    command.CommandTimeout = 0;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet1 = package.Workbook.Worksheets["Target"];
                                int row = 8;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        int columnIndex;
                                        if (col == 0) columnIndex = 5;   // E
                                        else if (col == 1) columnIndex = 9; // I
                                        else if (col == 2) columnIndex = 10; // J
                                        else continue; // bỏ qua nếu có hơn 3 cột

                                        sheet1.Cells[row, columnIndex].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                            }

                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet2 = package.Workbook.Worksheets["K076"];
                                sheet2.Cells["A1"].Value = "Tháng " + frdate.Value.ToString("MM");
                                int row = 6;
                                do
                                {
                                    for (int col = 3; col < reader.FieldCount; col++)
                                    {
                                        sheet2.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                            }

                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet2 = package.Workbook.Worksheets["K080"];
                                int row = 7;
                                do
                                {
                                    for (int col = 4; col < reader.FieldCount; col++)
                                    {
                                        sheet2.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
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
        private async Task RunReportAsync(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào thủ tục
                    if (stk_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@stk_id", stk_id.Text);
                    }
                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }
                    if (todate.Visible == true && todate.Enabled == true)
                    {
                        command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    }
                    if (dept_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@dept_id", dept_id.Text);
                    }
                    if (sku_code.Visible == true)
                    {
                        command.Parameters.AddWithValue("@sku_id", sku_code.Text);
                    }
                    if (supp_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@supp_id", supp_id.Text);
                    }
                    if (sl_fr.Visible == true)
                    {
                        command.Parameters.AddWithValue("@sl_fr", sl_fr.Text);
                    }
                    if (sl_to.Visible == true)
                    {
                        command.Parameters.AddWithValue("@sl_to", sl_to.Text);
                    }
                    if (ma_core.Visible == true)
                    {
                        //command.Parameters.AddWithValue("@ma_core", ma_core.SelectedValue);
                        object selectedValue = null;

                        // Đảm bảo truy cập từ đúng UI thread
                        if (ma_core.InvokeRequired)
                        {
                            ma_core.Invoke(new MethodInvoker(delegate
                            {
                                selectedValue = ma_core.SelectedValue;
                                _ma_core = ma_core.SelectedValue != null ? Convert.ToInt32(ma_core.SelectedValue) : 0;
                            }));
                        }
                        else
                        {
                            selectedValue = ma_core.SelectedValue;
                            _ma_core = ma_core.SelectedValue != null ? Convert.ToInt32(ma_core.SelectedValue) : 0;
                        }

                        command.Parameters.AddWithValue("@ma_core", selectedValue ?? DBNull.Value);

                    }
                    //command.Parameters.AddWithValue("@stk_id", stk_id.Text);
                    //command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    //command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));

                    command.CommandTimeout = 0; // Set timeout as needed
                                                // Giả sử bạn có một DataTable để lưu trữ kết quả
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    // Kiểm tra xem có dữ liệu trong dataTable không
                    if (dataTable.Rows.Count == 0)
                    {
                        // Thông báo hoặc xử lý khi không có dữ liệu
                        MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Không có dữ liệu để xuất Excel.");
                        return; // hoặc thực hiện các hành động khác theo yêu cầu của bạn
                    }
                    // Set the license context to use EPPlus
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
                                                                                // Load tệp Excel mẫu
                    FileInfo templateFile = new FileInfo(templatePath);

                    // Tạo một gói Excel mới dựa trên mẫu
                    using (ExcelPackage package = new ExcelPackage(templateFile, true))
                    {
                        // Truy cập vào tờ công việc đầu tiên
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        int startRow = 7;
                        if (Pro_name.Text == "rpt_Do_Phu_ASM")
                        {
                            worksheet.Cells["A5"].Value = $"Từ ngày: {frdate.Value} - Đến ngày : {todate.Value} ";
                        }
                        else if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv3" || Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv2")
                        {
                            startRow = 5;
                            worksheet.Cells["B2"].Value = $"Ngày: {frdate.Value}";// - Đến ngày : {todate.Value} ";
                        }
                        else if (Pro_name.Text == "rpt_StkISQty_Order_New")// || Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv2")
                        {
                            startRow = 5;
                            string tenCore = _ma_core == 1 ? "MART" : _ma_core == 2 ? "MiniMart" : "Không xác định";

                            worksheet.Cells["A1"].Value = $"BÁO CÁO ĐỘ PHỦ CORE {tenCore} ";
                            worksheet.Cells["A2"].Value = $"Từ ngày: {frdate.Value} - Đến ngày : {todate.Value} ";
                        }
                        else if (Pro_name.Text == "rpt_target_Dt_By_Day")
                        {
                            startRow = 5;
                            worksheet.Cells["B3"].Value = $"Từ ngày: {frdate.Value:dd/MM/yyyy} - Đến ngày: {todate.Value:dd/MM/yyyy}";
                        }
                        else if (Pro_name.Text == "rpt_target_Dt_By_Month")
                        {
                            startRow = 5;
                            var fromDate = new DateTime(frdate.Value.Year, frdate.Value.Month, 1);
                            var toDate = new DateTime(todate.Value.Year, todate.Value.Month, DateTime.DaysInMonth(todate.Value.Year, todate.Value.Month));

                            worksheet.Cells["B3"].Value = $"Từ ngày: {fromDate:dd/MM/yyyy} - Đến ngày: {toDate:dd/MM/yyyy}";

                            //worksheet.Cells["B3"].Value = $"Từ ngày: {frdate.Value:dd/MM/yyyy} - Đến ngày: {todate.Value:dd/MM/yyyy}";
                        }
                        else //if (Pro_name.Text == "rpt_Nonmoving" || Pro_name.Text == "rpt_SUPP_IMPORT")
                        {
                            worksheet.Cells["A4"].Value = $"Từ ngày: {frdate.Value} - Đến ngày : {todate.Value} ";
                        }

                        int columns_dem = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int i;
                            for (i = 0; i < dataTable.Columns.Count; i++)
                            {
                                // Giả sử các cột trong DataTable tương ứng với các cột trong Excel
                                //worksheet.Cells[startRow, i + 1].Value = row[i].ToString();
                                worksheet.Cells[startRow, i + 1].Value = row[i];
                                columns_dem = i;
                            }
                            if (Pro_name.Text == "rpt_Do_Phu_ASM")
                            {
                                worksheet.Cells[startRow, i + 1].Formula = $"=IFERROR({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 1].Address}, 0)";
                            }
                            else if (Pro_name.Text == "rpt_Nonmoving")
                            {
                                worksheet.Cells[startRow, i + 3].Formula = $"=IF({worksheet.Cells[startRow, i - 3].Address}={worksheet.Cells[startRow, i - 1].Address}-{worksheet.Cells[startRow, i + 1].Address},\"Nonmoving\",\"Hàng có GD\")";
                            }
                            else if (Pro_name.Text == "rpt_DioByNCC")
                            {
                                worksheet.Cells[startRow, i + 1].Formula = $"={worksheet.Cells[startRow, i - 1].Address} + {worksheet.Cells[startRow, i - 3].Address}-{worksheet.Cells[startRow, i - 5].Address}";
                                worksheet.Cells[startRow, i + 2].Formula = $"={worksheet.Cells[startRow, i].Address} + {worksheet.Cells[startRow, i - 2].Address}-{worksheet.Cells[startRow, i - 4].Address}";
                                worksheet.Cells[startRow, i + 3].Formula = $"=IFERROR((({worksheet.Cells[startRow, i + 2].Address} / {worksheet.Cells[startRow, i + 1].Address})*90)/3, 0)";
                                //// Định dạng điều kiện: Nếu giá trị của ô là số và < 0, thì đổi màu sắc
                                //var range = worksheet.Cells[1, 1, 10, 1]; // Phạm vi từ A1 đến A10
                                //var rule = range.ConditionalFormatting.AddExpression(range);
                                //rule.Formula = $"AND(ISNUMBER({range.Address}), {range.Address}<0)";
                                //rule.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                //rule.Style.Fill.BackgroundColor.Color.SetColor(System.Drawing.Color.Red); // Màu sắc bạn muốn áp dụng
                            }
                            else if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv3" || Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv2")
                            {
                                worksheet.Cells[startRow, i + 1].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 1].Address})-1, 0)";
                                worksheet.Cells[startRow, i + 2].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 1].Address}";
                                worksheet.Cells[startRow, i + 3].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 2].Address})-1, 0)";
                                worksheet.Cells[startRow, i + 4].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 2].Address}";
                            }
                            else if (Pro_name.Text == "rpt_SUPP_IMPORT")
                            {
                                //// Định dạng điều kiện: Nếu giá trị của ô là số và < 0, thì đổi màu sắc
                                //var range = worksheet.Cells[1, 1, 10, 1]; // Phạm vi từ A1 đến A10
                                //var rule = range.ConditionalFormatting.AddExpression(range);
                                //rule.Formula = $"AND(ISNUMBER({range.Address}), {range.Address}<0)";
                                //rule.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                //rule.Style.Fill.BackgroundColor.Color.SetColor(System.Drawing.Color.Red); // Màu sắc bạn muốn áp dụng
                            }
                            startRow++;
                        }
                        if (Pro_name.Text == "rpt_Do_Phu_ASM")
                        {
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        }
                        else if (Pro_name.Text == "rpt_Nonmoving")
                        {
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"G7:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Numberformat.Format = "_-* #,##0_-;-* #,##0_-;_-* \"-\"??_-;_-@_-";
                        }
                        else if (Pro_name.Text == "rpt_SUPP_IMPORT")
                        {
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"E7:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Numberformat.Format = "_-* #,##0_-;-* #,##0_-;_-* \"-\"??_-;_-@_-";
                        }
                        else if (Pro_name.Text == "rpt_DioByNCC")
                        {
                            //var a = GetExcelColumnName(columns_dem + 4);
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"G7:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Numberformat.Format = "_-* #,##0_-;-* #,##0_-;_-* \"-\"??_-;_-@_-";
                        }
                        else if (Pro_name.Text == "rpt_StkISQty_Order_New")
                        {
                            //var a = GetExcelColumnName(columns_dem + 4);
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 1)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        }
                        else if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv3")
                        {
                            // Áp dụng quy tắc định dạng
                            ExcelAddress address = new ExcelAddress($"M5:P{startRow}");
                            var formattingRule = worksheet.ConditionalFormatting.AddLessThan(address);
                            formattingRule.Formula = "0";
                            formattingRule.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink); //= "#FFC7CE"
                            formattingRule.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            //formattingRule.Style.Fill.BackgroundColor.Color = System.Drawing.Color.from(255, 205, 92, 92); // IndianRed color
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"J5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Numberformat.Format = "#,##0.0,,\"tr\"";

                            worksheet.Cells[$"M5:M{startRow}"].Style.Numberformat.Format = "0%";
                            worksheet.Cells[$"O5:O{startRow}"].Style.Numberformat.Format = "0%";

                            //worksheet.Cells[$"J5:L{startRow}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlanchedAlmond);
                            var range = worksheet.Cells[$"J5:L{startRow}"];
                            var fill = range.Style.Fill;
                            // Thiết lập kiểu mẫu trước khi đặt màu nền
                            fill.PatternType = ExcelFillStyle.Solid;
                            // Bây giờ đặt màu nền
                            //fill.BackgroundColor.SetColor(System.Drawing.Color.Cornsilk);
                            fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#fce4d6"));
                            ////--------------------------------------------------------------------------------------------------------
                            ////var range = worksheet.Cells[$"J5:L{startRow}"];

                            //// Thiết lập kiểu mẫu trước khi đặt màu nền
                            //range.Style.Fill.PatternType = ExcelFillStyle.Solid;

                            //// Bây giờ đặt màu nền
                            //range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlanchedAlmond);

                            //// Tạo điều kiện định dạng cho giá trị âm
                            //var negativeFormat = range.ConditionalFormatting.AddExpression();
                            //negativeFormat.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            //negativeFormat.Formula = "[$J5]<0";  // Áp dụng cho cột J, bạn có thể thay đổi thành cột khác nếu cần.
                            ////--------------------------------------------------------------------------------------------------------
                            worksheet.Cells["L4"].Value = frdate.Value.ToString("dd/MM/yyyy");
                            DateTime frdateValue = frdate.Value;
                            DateTime previousMonth = frdateValue.AddMonths(-1);
                            worksheet.Cells["K4"].Value = previousMonth.ToString("dd/MM/yyyy");
                            DateTime previousYear = frdateValue.AddYears(-1);
                            worksheet.Cells["J4"].Value = previousYear.ToString("dd/MM/yyyy");
                        }
                        else if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv2")
                        {
                            // Áp dụng quy tắc định dạng
                            ExcelAddress address = new ExcelAddress($"K5:N{startRow}");
                            var formattingRule = worksheet.ConditionalFormatting.AddLessThan(address);
                            formattingRule.Formula = "0";
                            formattingRule.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink); //= "#FFC7CE"
                            formattingRule.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            //formattingRule.Style.Fill.BackgroundColor.Color = System.Drawing.Color.from(255, 205, 92, 92); // IndianRed color
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"H5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Numberformat.Format = "#,##0.0,,\"tr\"";

                            worksheet.Cells[$"M5:M{startRow}"].Style.Numberformat.Format = "0%";
                            worksheet.Cells[$"K5:K{startRow}"].Style.Numberformat.Format = "0%";

                            //worksheet.Cells[$"J5:L{startRow}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlanchedAlmond);
                            var range = worksheet.Cells[$"H5:J{startRow}"];
                            var fill = range.Style.Fill;
                            // Thiết lập kiểu mẫu trước khi đặt màu nền
                            fill.PatternType = ExcelFillStyle.Solid;
                            // Bây giờ đặt màu nền
                            //fill.BackgroundColor.SetColor(System.Drawing.Color.Cornsilk);
                            fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#fce4d6"));

                            worksheet.Cells["J4"].Value = frdate.Value.ToString("dd/MM/yyyy");
                            DateTime frdateValue = frdate.Value;
                            DateTime previousMonth = frdateValue.AddMonths(-1);
                            worksheet.Cells["I4"].Value = previousMonth.ToString("dd/MM/yyyy");
                            DateTime previousYear = frdateValue.AddYears(-1);
                            worksheet.Cells["H4"].Value = previousYear.ToString("dd/MM/yyyy");
                        }

                        // Lưu gói Excel đã được sửa đổi vào tệp đầu ra.
                        //string uniqueFileName = GetUniqueFileName(Path.GetFileName(saveFileDialog.FileName), selectedDirectory);
                        package.SaveAs(new FileInfo(outputPath));
                        // Mở tệp Excel sau khi đã lưu
                        System.Diagnostics.Process.Start(outputPath);
                    }
                }
            }
        }
        private async Task RunReportAsync_Total(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }

                    if (dept_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@dept_id", dept_id.Text);
                    }

                    command.CommandTimeout = 0; // Đặt thời gian chờ theo cần thiết

                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Không có dữ liệu để xuất Excel.");
                        return;
                    }

                    // Đặt ngữ cảnh giấy phép để sử dụng EPPlus
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Nạp mẫu Excel
                    FileInfo templateFile = new FileInfo(templatePath);

                    using (ExcelPackage package = new ExcelPackage(templateFile, true))
                    {
                        // Truy cập vào tờ công việc đầu tiên
                        ExcelWorksheet mainWorksheet = package.Workbook.Worksheets[0];

                        // Lấy các nhóm duy nhất từ DataTable
                        var distinctGroups = dataTable.AsEnumerable().Select(row => row.Field<string>("Groups")).Distinct();
                        //var distinctGroups = dataTable.AsEnumerable()
                        //                    .Select(row => row.Field<int>("Groups").ToString())
                        //                    .Distinct()
                        //                    .OrderBy(group => group)
                        //                    .ToList();
                        foreach (var group in distinctGroups)
                        {
                            // Sao chép tờ công việc chính cho mỗi nhóm
                            //ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(group, mainWorksheet);
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[group];
                            if (worksheet == null)
                            {
                                // Nếu tờ không tồn tại, tạo một tờ mới
                                worksheet = package.Workbook.Worksheets.Add(group, mainWorksheet);

                                // Cấu hình tờ mới nếu cần
                                // ...
                            }

                            // Lọc dữ liệu cho nhóm hiện tại
                            //var groupData = dataTable.Select($"Groups = '{group}'").CopyToDataTable();
                            // Lọc dữ liệu cho nhóm hiện tại và sắp xếp theo cột dept_id
                            var groupData = dataTable.Select($"Groups = '{group}'", "Dept_id").CopyToDataTable();
                            //var groupData = dataTable.Select($"Groups = {int.Parse(group)}", "Dept_id").CopyToDataTable();

                            int startRow = 5;

                            // Đặt dữ liệu cụ thể cho nhóm trên tờ công việc
                            worksheet.Cells["B2"].Value = $"Ngày: {frdate.Value}";// - Đến ngày : {todate.Value} ";

                            int columns_dem = 0;

                            foreach (DataRow row in groupData.Rows)
                            {
                                int i;
                                for (i = 0; i < groupData.Columns.Count - 1; i++)
                                {
                                    worksheet.Cells[startRow, i + 1].Value = row[i];
                                    columns_dem = i;
                                }


                                worksheet.Cells[startRow, i + 1].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 1].Address})-1, 0)";
                                worksheet.Cells[startRow, i + 2].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 1].Address}";
                                worksheet.Cells[startRow, i + 3].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 2].Address})-1, 0)";
                                worksheet.Cells[startRow, i + 4].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 2].Address}";
                                // Công thức và xử lý bổ sung cho mỗi hàng (giống như mã hiện tại của bạn)

                                startRow++;
                            }
                            //startRow--;
                            // Tính tổng cho từng cột từ C đến E
                            for (int colIndex = 3; colIndex <= 5; colIndex++) // Cột C đến E
                            {
                                worksheet.Cells[startRow, colIndex].Formula = $"SUM({GetExcelColumnName(colIndex)}5:{GetExcelColumnName(colIndex)}{startRow - 1})";
                            }

                            worksheet.Cells[startRow, 6].Formula = $"=IFERROR(({worksheet.Cells[startRow, 5].Address} / {worksheet.Cells[startRow, 4].Address})-1, 0)";
                            worksheet.Cells[startRow, 7].Formula = $"=+{worksheet.Cells[startRow, 5].Address}-{worksheet.Cells[startRow, 4].Address}";
                            worksheet.Cells[startRow, 8].Formula = $"=IFERROR(({worksheet.Cells[startRow, 5].Address} / {worksheet.Cells[startRow, 3].Address})-1, 0)";
                            worksheet.Cells[startRow, 9].Formula = $"=+{worksheet.Cells[startRow, 5].Address}-{worksheet.Cells[startRow, 3].Address}";
                            // Gán giá trị "Tổng" cho ô đã merge
                            worksheet.Cells[startRow, 1].Value = "Tổng cộng";
                            // Merge cột A5 đến I5
                            worksheet.Cells[startRow, 1, startRow, 2].Merge = true;

                            worksheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            // Đặt định dạng chữ đậm cho ô đã merge
                            worksheet.Cells[startRow, 1, startRow, columns_dem + 5].Style.Font.Bold = true;

                            // Áp dụng các quy tắc định dạng và kiểu cho nhóm hiện tại
                            ExcelAddress address = new ExcelAddress($"F5:I{startRow}");
                            var formattingRule = worksheet.ConditionalFormatting.AddLessThan(address);
                            formattingRule.Formula = "0";
                            formattingRule.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink);
                            formattingRule.Style.Font.Color.SetColor(System.Drawing.Color.Red);

                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"C5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Numberformat.Format = "#,##0.0,,\"tr\"";

                            worksheet.Cells[$"F5:F{startRow}"].Style.Numberformat.Format = "0%";
                            worksheet.Cells[$"H5:H{startRow}"].Style.Numberformat.Format = "0%";

                            var range = worksheet.Cells[$"C5:E{startRow}"];
                            var fill = range.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            //fill.BackgroundColor.SetColor(System.Drawing.Color.Cornsilk);
                            fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#fce4d6"));

                            //fill.Style.Fill.BackgroundColor.Color = System.Drawing.Color.from(252, 228, 214, 255); // IndianRed color
                            worksheet.Cells["E4"].Value = frdate.Value.ToString("dd/MM/yyyy");
                            DateTime frdateValue = frdate.Value;
                            DateTime previousMonth = frdateValue.AddMonths(-1);
                            worksheet.Cells["D4"].Value = previousMonth.ToString("dd/MM/yyyy");
                            DateTime previousYear = frdateValue.AddYears(-1);
                            worksheet.Cells["C4"].Value = previousYear.ToString("dd/MM/yyyy");

                            //// Tính tổng cho cột cuối cùng (ví dụ: cột 5)
                            //worksheet.Cells[startRow, 3].Formula = $"SUM({GetExcelColumnName(1)}{startRow}:{GetExcelColumnName(groupData.Columns.Count)}{startRow + groupData.Rows.Count - 1})";

                        }
                        // Chọn tờ cuối cùng
                        int numberOfWorksheets = package.Workbook.Worksheets.Count;
                        ExcelWorksheet lastWorksheet = package.Workbook.Worksheets[0];
                        lastWorksheet.Select();
                        // Lưu gói Excel đã được sửa đổi vào tệp đầu ra
                        package.SaveAs(new FileInfo(outputPath));

                        // Mở tệp Excel sau khi đã lưu
                        System.Diagnostics.Process.Start(outputPath);
                    }
                }
            }
        }
        private async Task RunReportAsync_truyen_nhan(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));

                    command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));

                    command.CommandTimeout = 0; // Đặt thời gian chờ theo cần thiết

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            // Thông báo hoặc xử lý khi không có dữ liệu
                            MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Không có dữ liệu để xuất Excel.");
                            return; // hoặc thực hiện các hành động khác theo yêu cầu của bạn
                        }

                        FileInfo templateFile = new FileInfo(templatePath);
                        FileInfo outputFile = new FileInfo(outputPath);

                        using (ExcelPackage package = new ExcelPackage(templateFile))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                            // Ghi các biến @frdate và @todate vào ô C3
                            worksheet.Cells["C3"].Value = $"Từ ngày: {frdate.Value.ToString("dd/MM/yyyy")} - đến ngày: {todate.Value.ToString("dd/MM/yyyy")}";

                            // Ghi dữ liệu từ stored procedure vào từ ô A6

                            worksheet.Cells["A6"].LoadFromDataTable(dataTable, false);

                            // Kẻ ô cho file Excel từ A5 đến hết dữ liệu
                            int rows = dataTable.Rows.Count + 5;
                            int columns = dataTable.Columns.Count;

                            using (var range = worksheet.Cells[5, 1, rows, columns])
                            {
                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            }

                            // Lưu file Excel
                            //package.SaveAs(outputFile);
                            // Lưu gói Excel đã được sửa đổi vào tệp đầu ra
                            package.SaveAs(new FileInfo(outputPath));



                            // Mở tệp Excel sau khi đã lưu
                            System.Diagnostics.Process.Start(outputPath);
                        }
                    }
                }
            }
        }
        private async Task RunReportAsync_Tach_Don_333_314_315(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));

                    command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));

                    command.CommandTimeout = 0; // Đặt thời gian chờ theo cần thiết

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {

                        if (!reader.HasRows)
                        {
                            // Thông báo hoặc xử lý khi không có dữ liệu
                            MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Không có dữ liệu để xuất Excel.");
                            return; // hoặc thực hiện các hành động khác theo yêu cầu của bạn
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {

                            var frto = "Từ ngày: " + frdate.Value.ToString("dd-MM-yyyy") + " đến ngày: " + todate.Value.ToString("dd-MM-yyyy");
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet1 = package.Workbook.Worksheets["333"];
                                sheet1.Cells["D3"].Value = frto;
                                int row = 7;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        sheet1.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                                // Kẻ ô cho file Excel từ A5 đến hết dữ liệu
                                int Endrows = row - 1;
                                int columns = reader.FieldCount;

                                using (var range = sheet1.Cells[7, 1, Endrows, columns])
                                {
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                            }

                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet2 = package.Workbook.Worksheets["314"];
                                sheet2.Cells["D3"].Value = frto;
                                int row = 7;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        sheet2.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                                // Kẻ ô cho file Excel từ A5 đến hết dữ liệu
                                int Endrows = row - 1;
                                int columns = reader.FieldCount;

                                using (var range = sheet2.Cells[7, 1, Endrows, columns])
                                {
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                            }

                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet3 = package.Workbook.Worksheets["315"];
                                sheet3.Cells["D3"].Value = frto;
                                int row = 7;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        sheet3.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                                // Kẻ ô cho file Excel từ A5 đến hết dữ liệu
                                int Endrows = row - 1;
                                int columns = reader.FieldCount;

                                using (var range = sheet3.Cells[7, 1, Endrows, columns])
                                {
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                            }

                            await package.SaveAsAsync(new FileInfo(outputPath));
                            System.Diagnostics.Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
                        }
                    }
                }
            }
        }
        private async Task RunReportAsync_rpt_voucher_In_Stock1(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));

                    command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));

                    command.CommandTimeout = 0; // Đặt thời gian chờ theo cần thiết

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {

                        if (!reader.HasRows)
                        {
                            // Thông báo hoặc xử lý khi không có dữ liệu
                            MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Không có dữ liệu để xuất Excel.");
                            return; // hoặc thực hiện các hành động khác theo yêu cầu của bạn
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {

                            var date_create = "Ngày tạo : " + DateTime.Now.ToString("dd/MM/yyyy - H:mm");
                            var frto = "Từ ngày: " + frdate.Value.ToString("dd-MM-yyyy") + " đến ngày: " + todate.Value.ToString("dd-MM-yyyy");

                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet1 = package.Workbook.Worksheets["Sheet1"];
                                sheet1.Cells["A2"].Value = frto;
                                sheet1.Cells["N1"].Value = date_create;
                                int row = 5;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        sheet1.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                                // Kẻ ô cho file Excel từ A5 đến hết dữ liệu
                                int Endrows = row - 1;
                                int columns = reader.FieldCount;

                                using (var range = sheet1.Cells[5, 1, Endrows, columns])
                                {
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                            }


                            await package.SaveAsAsync(new FileInfo(outputPath));
                            System.Diagnostics.Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
                        }
                    }
                }
            }
        }
        private async Task RunReportAsync_rpt_voucher_In_Stock(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    command.CommandTimeout = 0;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông Báo");
                            return;
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            var sheet = package.Workbook.Worksheets["Sheet1"];
                            if (sheet == null)
                            {
                                MessageBox.Show("Không tìm thấy Sheet1!");
                                return;
                            }

                            sheet.Cells["A2"].Value = $"Từ ngày: {frdate.Value:dd/MM/yyyy} - đến ngày: {todate.Value:dd/MM/yyyy}";
                            sheet.Cells["N1"].Value = $"Ngày tạo : {DateTime.Now:dd/MM/yyyy - H:mm}";

                            int row = 5;
                            int sheetIndex = 1;
                            int maxRowsPerSheet = 500000;

                            Dictionary<string, int> sheetLastRow = new Dictionary<string, int>();
                            string currentSheetName = sheet.Name;

                            while (await reader.ReadAsync())
                            {
                                if (row > maxRowsPerSheet + 4)
                                {
                                    sheetLastRow[currentSheetName] = row - 1; // Lưu số dòng cuối sheet cũ

                                    sheetIndex++;
                                    currentSheetName = $"Sheet{sheetIndex}";

                                    sheet = package.Workbook.Worksheets.Add(currentSheetName, package.Workbook.Worksheets["Sheet1"]);
                                    // XÓA dữ liệu từ dòng 5 trở đi
                                    int lastRow = sheet.Dimension.End.Row;
                                    sheet.Cells[5, 1, lastRow, 20].Clear();
                                    row = 5;
                                }

                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    sheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                }

                                row++;
                            }

                            // Lưu dòng cuối của sheet cuối cùng
                            sheetLastRow[currentSheetName] = row - 1;

                            // Kẻ khung theo từng sheet đúng số dòng
                            foreach (var ws in package.Workbook.Worksheets)
                            {
                                if (!sheetLastRow.ContainsKey(ws.Name))
                                    continue;

                                int endRow = sheetLastRow[ws.Name];
                                int colCount = reader.FieldCount;

                                using (var range = ws.Cells[5, 1, endRow, colCount])
                                {
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            }

                            await package.SaveAsAsync(new FileInfo(outputPath));
                            System.Diagnostics.Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
                        }
                    }
                }
            }
        }

        private async Task RunReportAsync_rpt_voucher_In_Stock_trung_Dulieu(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    command.CommandTimeout = 0; // Không giới hạn thời gian chạy

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Không có dữ liệu để xuất Excel.");
                            return;
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            var date_create = "Ngày tạo : " + DateTime.Now.ToString("dd/MM/yyyy - H:mm");
                            var frto = "Từ ngày: " + frdate.Value.ToString("dd/MM/yyyy") + " - đến ngày: " + todate.Value.ToString("dd/MM/yyyy");

                            ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet1"];
                            if (sheet == null)
                            {
                                MessageBox.Show("Không tìm thấy sheet 'Sheet1' trong file template!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Ghi thông tin ngày tháng vào Sheet1
                            sheet.Cells["A2"].Value = frto;
                            sheet.Cells["N1"].Value = date_create;

                            int row = 5;         // Dòng bắt đầu ghi dữ liệu
                            int sheetIndex = 1;  // Đánh số sheet
                            int maxRowsPerSheet = 500000; // Số dòng tối đa trên 1 sheet

                            while (await reader.ReadAsync())
                            {
                                if (row > maxRowsPerSheet + 4) // Nếu vượt 50,000 dòng, tạo sheet mới
                                {
                                    sheetIndex++;
                                    string newSheetName = "Sheet" + sheetIndex;
                                    sheet = package.Workbook.Worksheets.Add(newSheetName, package.Workbook.Worksheets["Sheet1"]);
                                    // XÓA dữ liệu từ dòng 5 trở đi
                                    int lastRow = sheet.Dimension.End.Row;
                                    sheet.Cells[5, 1, lastRow, 30].Clear();
                                    row = 5; // Reset lại số dòng để ghi dữ liệu vào Sheet mới
                                }

                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    sheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                }
                                row++;
                            }

                            // Kẻ ô cho toàn bộ dữ liệu trên tất cả các sheet
                            foreach (var ws in package.Workbook.Worksheets)
                            {
                                int endRow = row - 1;
                                int columns = reader.FieldCount;

                                using (var range = ws.Cells[5, 1, endRow, columns])
                                {
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                                // Tự động điều chỉnh độ rộng cột
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            }

                            await package.SaveAsAsync(new FileInfo(outputPath));
                            System.Diagnostics.Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
                        }
                    }
                }
            }
        }
        private async Task RunReportAsync_rpt_timekeeping_details(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }
                    if (todate.Visible == true && todate.Enabled == true)
                    {
                        command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    }
                    if (userId.Visible == true)
                    {
                        command.Parameters.AddWithValue("@user_id", userId.Text);
                    }
                    if (StoreId.Visible == true)
                    {
                        command.Parameters.AddWithValue("@store_id", StoreId.Text);
                    }
                    //command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    //command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    command.CommandTimeout = 0; // Không giới hạn thời gian chạy

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Không có dữ liệu để xuất Excel.");
                            return;
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            var date_create = "Ngày tạo : " + DateTime.Now.ToString("dd/MM/yyyy - H:mm");
                            var frto = "Từ ngày: " + frdate.Value.ToString("dd/MM/yyyy") + " - đến ngày: " + todate.Value.ToString("dd/MM/yyyy");

                            ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet1"];
                            if (sheet == null)
                            {
                                MessageBox.Show("Không tìm thấy sheet 'Sheet1' trong file template!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Ghi thông tin ngày tháng vào Sheet1
                            sheet.Cells["A2"].Value = frto;
                            sheet.Cells["I1"].Value = date_create;

                            int row = 5;         // Dòng bắt đầu ghi dữ liệu
                            int sheetIndex = 1;  // Đánh số sheet
                            int maxRowsPerSheet = 500000; // Số dòng tối đa trên 1 sheet

                            while (await reader.ReadAsync())
                            {
                                if (row > maxRowsPerSheet + 4) // Nếu vượt 50,000 dòng, tạo sheet mới
                                {
                                    sheetIndex++;
                                    string newSheetName = "Sheet" + sheetIndex;
                                    sheet = package.Workbook.Worksheets.Add(newSheetName, package.Workbook.Worksheets["Sheet1"]);
                                    // XÓA dữ liệu từ dòng 5 trở đi
                                    int lastRow = sheet.Dimension.End.Row;
                                    sheet.Cells[5, 1, lastRow, 20].Clear();
                                    row = 5; // Reset lại số dòng để ghi dữ liệu vào Sheet mới
                                }

                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    sheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                }
                                row++;
                            }

                            // Kẻ ô cho toàn bộ dữ liệu trên tất cả các sheet
                            foreach (var ws in package.Workbook.Worksheets)
                            {
                                int endRow = row - 1;
                                int columns = reader.FieldCount;

                                using (var range = ws.Cells[5, 1, endRow, columns])
                                {
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                                // Tự động điều chỉnh độ rộng cột
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            }

                            await package.SaveAsAsync(new FileInfo(outputPath));
                            System.Diagnostics.Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
                        }
                    }
                }
            }
        }
        private async Task RunReportAsync_rpt_StkISQty_Core_Mart_MiniMart(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("rpt_StkISQty_Core_Mart_MiniMart", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    if (frdate.Visible && frdate.Enabled)
                    {
                        command.Parameters.AddWithValue("@toDate", todate.Value.Date);
                    }

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            ExcelWorksheet wsSumMart = package.Workbook.Worksheets["SUM Mart"];
                            ExcelWorksheet wsSumMiniMart = package.Workbook.Worksheets["SUM MiniMart"];
                            ExcelWorksheet wsCoreMart = package.Workbook.Worksheets["Core Mart-G"];
                            ExcelWorksheet wsCoreMiniMart = package.Workbook.Worksheets["Core mini-G"];

                            int startRow = 3;

                            // Result set 1: SUM Mart
                            WriteReaderToSheet(reader, wsSumMart, startRow);

                            // Result set 2: SUM MiniMart
                            if (reader.NextResult())
                                WriteReaderToSheet(reader, wsSumMiniMart, startRow);

                            // Result set 3: Core Mart-G
                            if (reader.NextResult())
                                WriteReaderToSheet(reader, wsCoreMart, startRow);

                            // Result set 4: Core mini-G
                            if (reader.NextResult())
                                WriteReaderToSheet(reader, wsCoreMiniMart, startRow);


                            foreach (var ws in package.Workbook.Worksheets)
                            {
                                // Bỏ qua sheet không có dữ liệu
                                if (ws.Dimension == null)
                                    continue;
                                // Xác định vùng có dữ liệu từ dòng 3
                                var range = ws.Cells[
                                    startRow,
                                    ws.Dimension.Start.Column,
                                    ws.Dimension.End.Row,
                                    ws.Dimension.End.Column
                                ];

                                // Kẻ viền
                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                // Auto resize cột
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            }

                            await package.SaveAsAsync(new FileInfo(outputPath));
                        }
                    }
                }
            }

            Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
        }

        private async Task RunReportAsync_HRM_KPI_Thang(string templatePath, string outputPath, DateTime atMonth, string Lv1OrgName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection =
                   new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command =
                       new SqlCommand("HRM_KPI_Thang", connection))
                {
                    command.CommandType =
                        CommandType.StoredProcedure;

                    command.CommandTimeout = 0;

                    command.Parameters.Add(
                        "@atMonth",
                        SqlDbType.Date).Value = atMonth;

                    command.Parameters.AddWithValue(
                        "@Lv1OrgName",
                        string.IsNullOrWhiteSpace(Lv1OrgName)
                            ? DBNull.Value
                            : (object)Lv1OrgName);

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Không có dữ liệu",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            ExcelPackage.LicenseContext =
                LicenseContext.NonCommercial;

            using (var package =
                   new ExcelPackage(
                       new FileInfo(templatePath)))
            {
                var ws =
                    package.Workbook.Worksheets[
                        "Bao_cao_them_BTH KPI_thang"];

                if (ws == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy sheet Bao_cao_them_BTH KPI_thang");

                    return;
                }

                ws.Cells["A2"].Value =
                    $"THÁNG {atMonth.Month:00} NĂM {atMonth.Year}";

                int row = 5;

                string oldLv1OrgName = "";
                string oldNameEn = "";

                int stt = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    string lv1Org =
                        dr["Lv1OrgName"]?.ToString();

                    string nameEn =
                        dr["NameEn"]?.ToString();

                    // Group cấp 1
                    if (oldLv1OrgName != lv1Org)
                    {
                        stt = 0;

                        ws.Cells[row, 1].Value =
                            lv1Org;

                        ws.Cells[row, 1, row, 11]
                          .Merge = true;

                        ws.Cells[row, 1].Style.Font.Bold = true;

                        row++;

                        oldLv1OrgName = lv1Org;
                        oldNameEn = "";
                    }

                    // Group cấp 2
                    if (oldNameEn != nameEn)
                    {
                        ws.Cells[row, 1].Value =
                            nameEn;

                        ws.Cells[row, 1, row, 11]
                          .Merge = true;

                        ws.Cells[row, 1].Style.Font.Bold = true;

                        row++;

                        oldNameEn = nameEn;
                    }

                    // Dữ liệu chi tiết
                    stt++;

                    ws.Cells[row, 1].Value = stt;
                    ws.Cells[row, 2].Value = dr["UserName"];
                    ws.Cells[row, 3].Value = dr["EmployeeName"];
                    ws.Cells[row, 4].Value = dr["EmployeeJobTitle"];
                    ws.Cells[row, 5].Value = dr["LevelName"];
                    ws.Cells[row, 6].Value = dr["NameEn"];
                    ws.Cells[row, 7].Value = dr["EmpKpiPoint"];
                    ws.Cells[row, 8].Value = dr["EmpKpiClassification"]?.ToString().Trim();
                    ws.Cells[row, 9].Value = dr["Level1ManagerKpiPoint"];
                    ws.Cells[row, 10].Value = dr["Level1ManagerKpiClassification"]?.ToString().Trim();
                    ws.Cells[row, 11].Value = dr["reportPoint"];
                    ws.Cells[row, 12].Value = dr["reportClassification"]?.ToString().Trim();

                    row++;
                }

                int endRow = row - 1;

                var range =
                    ws.Cells[6, 1, endRow, 13];

                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;

                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                // Cột B căn giữa
                ws.Cells[6, 2, endRow, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Cột G -> L căn giữa
                ws.Cells[6, 7, endRow, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                // Tạo bảng tổng hợp dưới cùng
                int summaryRow = row + 2;
                var summaryRange = ws.Cells[summaryRow, 2, summaryRow + 8, 5];

                for (int r = summaryRow; r <= summaryRow + 8; r++)
                {
                    ws.Cells[r, 2, r, 3].Merge = true;
                }
                summaryRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                summaryRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                var headerRange = ws.Cells[summaryRow, 2, summaryRow, 5];

                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;

                headerRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 230, 153));
                var totalRange = ws.Cells[summaryRow + 8, 2, summaryRow + 8, 5];

                totalRange.Style.Fill.PatternType = ExcelFillStyle.Solid;

                totalRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 230, 153));
                //totalRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 230, 153));

                headerRange.Style.Font.Bold = true;
                totalRange.Style.Font.Bold = true;

                ws.Cells[summaryRow, 2].Value = "Bảng xếp loại";
                ws.Cells[summaryRow, 4].Value = "Số CBNV";
                ws.Cells[summaryRow, 5].Value = "Xếp loại theo tỉ lệ";

                ws.Cells[summaryRow, 7].Value = "Trưởng bộ phận";
                ws.Cells[summaryRow, 11].Value = "Tổng Giám Đốc";
                ws.Cells[summaryRow + 1, 2].Value = "Loại A+ (Hoàn thành xuất sắc)";

                ws.Cells[summaryRow + 1, 4].Formula = $"COUNTIF(L:L,\"*A+*\")";

                ws.Cells[summaryRow + 1, 5].Formula = $"D{summaryRow + 1}/D{summaryRow + 8}";
                ws.Cells[summaryRow + 1, 2].Value = "Loại A+ (Hoàn thành xuất sắc)";

                ws.Cells[summaryRow + 1, 4].Formula = $"COUNTIF(L:L,\"*A+*\")";

                ws.Cells[summaryRow + 1, 5].Formula = $"D{summaryRow + 1}/D{summaryRow + 8}";
                ws.Cells[summaryRow + 2, 2].Value = "Loại A (Hoàn thành nhiệm vụ)";

                ws.Cells[summaryRow + 2, 4].Formula = $"COUNTIF(L:L,\"A\")";

                ws.Cells[summaryRow + 2, 5].Formula = $"D{summaryRow + 2}/D{summaryRow + 8}";
                ws.Cells[summaryRow + 3, 2].Value = "Loại A- (Cơ bản hoàn thành nhiệm vụ)";

                ws.Cells[summaryRow + 3, 4].Formula = $"COUNTIF(L:L,\"*A-*\")";

                ws.Cells[summaryRow + 3, 5].Formula = $"D{summaryRow + 3}/D{summaryRow + 8}";
                ws.Cells[summaryRow + 4, 2].Value = "Loại B+ (Cần cải thiện)";
                ws.Cells[summaryRow + 4, 4].Formula = $"COUNTIF(L:L,\"*B+*\")";

                ws.Cells[summaryRow + 4, 5].Formula = $"D{summaryRow + 4}/D{summaryRow + 8}";
                ws.Cells[summaryRow + 5, 2].Value = "Loại B (Cần cải thiện)";

                ws.Cells[summaryRow + 5, 4].Formula = $"COUNTIF(L:L,\"B\")";

                ws.Cells[summaryRow + 5, 5].Formula = $"D{summaryRow + 5}/D{summaryRow + 8}";
                ws.Cells[summaryRow + 6, 2].Value = "Loại B- (Cần cải thiện)";

                ws.Cells[summaryRow + 6, 4].Formula = $"COUNTIF(L:L,\"*B-*\")";

                ws.Cells[summaryRow + 6, 5].Formula = $"D{summaryRow + 6}/D{summaryRow + 8}";
                ws.Cells[summaryRow + 7, 2].Value = "Loại C (Không đạt yêu cầu)";

                ws.Cells[summaryRow + 7, 4].Formula = $"COUNTIF(L:L,\"C\")";

                ws.Cells[summaryRow + 7, 5].Formula = $"D{summaryRow + 7}/D{summaryRow + 8}";
                ws.Cells[summaryRow + 8, 2].Value = "Tổng số CBNV đã đánh giá";

                ws.Cells[summaryRow + 8, 4].Formula = $"SUM(D{summaryRow + 1}:D{summaryRow + 7})";
                ws.Cells[summaryRow + 1, 5, summaryRow + 7, 5].Style.Numberformat.Format = "0%";
                var summaryRange1 = ws.Cells[summaryRow, 2, summaryRow + 8, 5];

                summaryRange1.Style.Border.Top.Style = ExcelBorderStyle.Thin;

                summaryRange1.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                summaryRange1.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                summaryRange1.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                // Tạo bảng tổng hợp dưới cùng ---------------------------------------------------------

                await package.SaveAsAsync(new FileInfo(outputPath));
            }

            Process.Start(
                new ProcessStartInfo(outputPath)
                {
                    UseShellExecute = true
                });
        }
        private async Task RunReportAsync_rpt_Nhap_Khau_dong_to_o_Cuoi_group(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("rpt_Nhap_Khau", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    if (todate.Visible && todate.Enabled)
                        command.Parameters.AddWithValue("@toDate", todate.Value.Date);

                    if (frdate.Visible && frdate.Enabled)
                        command.Parameters.AddWithValue("@frDate", frdate.Value.Date);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            var ws = package.Workbook.Worksheets["BÁO CÁO"];
                            if (ws == null)
                            {
                                MessageBox.Show("Không tìm thấy sheet 'BÁO CÁO'");
                                return;
                            }


                            var date_create = "Ngày tạo : " + DateTime.Now.ToString("dd/MM/yyyy - H:mm");
                            var frto = "Theo kỳ - " + "Từ: " + frdate.Value.ToString("dd/MM/yyyy") + " - đến: " + todate.Value.ToString("dd/MM/yyyy");
                            var LuyKe_frto = $"Lũy kế - Từ: {new DateTime(frdate.Value.Year, frdate.Value.Month, 1):dd/MM/yyyy} - đến: {todate.Value:dd/MM/yyyy}";

                            // Ghi thông tin ngày tháng vào Sheet1
                            ws.Cells["A1"].Value = date_create;
                            ws.Cells["G2"].Value = frto;
                            ws.Cells["N2"].Value = LuyKe_frto;

                            int startRow = 4;
                            int currentRow = startRow;

                            int colCount = reader.FieldCount;

                            // ===== CONFIG CỘT TÍNH TỔNG (E → Q) =====
                            int startSumCol = 5; // cột E
                            int endSumCol = 17;  // cột Q

                            int dataStartIndex = startSumCol - 2;
                            int dataEndIndex = endSumCol - 2;

                            decimal[] sumGroup = new decimal[colCount];
                            decimal[] sumAll = new decimal[colCount];

                            // ===== GROUP =====
                            int stt = 1;
                            string currentRptKey = "";
                            int groupStartRow = startRow;

                            while (await reader.ReadAsync())
                            {
                                string rptKey = reader["Rpt_key"]?.ToString() ?? "";

                                // ===== ĐỔI GROUP =====
                                if (currentRptKey != "" && currentRptKey != rptKey)
                                {
                                    int rptKeyCol = 2;

                                    // merge
                                    ws.Cells[groupStartRow, rptKeyCol, currentRow - 1, rptKeyCol].Merge = true;
                                    ws.Cells[groupStartRow, rptKeyCol].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                                    // ghi tổng group
                                    ws.Cells[currentRow, rptKeyCol].Value = "Tổng " + currentRptKey;

                                    for (int i = dataStartIndex; i <= dataEndIndex && i < colCount; i++)
                                    {
                                        string colName = reader.GetName(i);

                                        if (colName == "Tyle_lai" || colName == "Lai_gop")
                                        {
                                            decimal dt = sumGroup[i - 2];
                                            decimal lai = sumGroup[i - 1];

                                            ws.Cells[currentRow, i + 2].Value = (dt != 0) ? lai / dt : 0;
                                        }
                                        else
                                        {
                                            ws.Cells[currentRow, i + 2].Value = sumGroup[i];
                                        }
                                    }

                                    // style
                                    using (var range = ws.Cells[currentRow, 1, currentRow, colCount + 1])
                                    {
                                        range.Style.Font.Bold = true;
                                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                                    }

                                    currentRow++;
                                    stt = 1;
                                    Array.Clear(sumGroup, 0, sumGroup.Length);
                                    groupStartRow = currentRow;
                                }

                                currentRptKey = rptKey;

                                // ===== GHI DATA =====
                                ws.Cells[currentRow, 1].Value = stt++;

                                for (int i = 0; i < colCount; i++)
                                {
                                    ws.Cells[currentRow, i + 2].Value = reader[i];
                                }

                                // ===== CỘNG TỔNG =====
                                for (int i = dataStartIndex; i <= dataEndIndex && i < colCount; i++)
                                {
                                    if (reader[i] != DBNull.Value &&
                                        decimal.TryParse(reader[i].ToString(), out decimal val))
                                    {
                                        sumGroup[i] += val;
                                        sumAll[i] += val;
                                    }
                                }

                                currentRow++;
                            }

                            // ===== GROUP CUỐI =====
                            int rptKeyColumn = 2;

                            ws.Cells[groupStartRow, rptKeyColumn, currentRow - 1, rptKeyColumn].Merge = true;

                            ws.Cells[currentRow, rptKeyColumn].Value = "Tổng " + currentRptKey;

                            for (int i = dataStartIndex; i <= dataEndIndex && i < colCount; i++)
                            {
                                string colName = reader.GetName(i);

                                if (colName == "Tyle_lai" || colName == "Lai_gop")
                                {
                                    decimal dt = sumGroup[i - 2];
                                    decimal lai = sumGroup[i - 1];

                                    ws.Cells[currentRow, i + 2].Value = (dt != 0) ? lai / dt : 0;
                                }
                                else
                                {
                                    ws.Cells[currentRow, i + 2].Value = sumGroup[i];
                                }
                            }

                            using (var range = ws.Cells[currentRow, 1, currentRow, colCount + 1])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                            }

                            currentRow++;

                            // ===== TỔNG TOÀN BỘ =====

                            ws.Cells[currentRow, rptKeyColumn].Value = "TỔNG CỘNG";

                            for (int i = dataStartIndex; i <= dataEndIndex && i < colCount; i++)
                            {
                                string colName = reader.GetName(i);

                                if (colName == "Tyle_lai" || colName == "Lai_gop")
                                {
                                    decimal dt = sumAll[i - 2];
                                    decimal lai = sumAll[i - 1];

                                    ws.Cells[currentRow, i + 2].Value = (dt != 0) ? lai / dt : 0;
                                }
                                else
                                {
                                    ws.Cells[currentRow, i + 2].Value = sumAll[i];
                                }
                            }

                            using (var range = ws.Cells[currentRow, 1, currentRow, colCount + 2])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange);
                            }

                            // ===== FORMAT =====
                            using (var range = ws.Cells[startRow, 1, currentRow, colCount + 2])
                            {
                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            }

                            // format số (E → Q)
                            ws.Cells[startRow, startSumCol, currentRow, endSumCol].Style.Numberformat.Format = "#,##0";
                            // Format % cho cột M (13)
                            ws.Cells[startRow, 13, currentRow, 13].Style.Numberformat.Format = "0.00%";

                            // Format % cho cột Q (17)
                            ws.Cells[startRow, 17, currentRow, 17].Style.Numberformat.Format = "0.00%";

                            // căn giữa STT
                            ws.Cells[startRow, 1, currentRow, 1].Style.HorizontalAlignment =
                                OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                            // autofit
                            if (ws.Dimension != null)
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                            await package.SaveAsAsync(new FileInfo(outputPath));
                        }
                    }
                }
            }

            Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
        }
        private async Task RunReportAsync_rpt_Nhap_Khau(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("rpt_Nhap_Khau", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    if (todate.Visible && todate.Enabled)
                        command.Parameters.AddWithValue("@toDate", todate.Value.Date);

                    if (frdate.Visible && frdate.Enabled)
                        command.Parameters.AddWithValue("@frDate", frdate.Value.Date);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            var ws = package.Workbook.Worksheets["BÁO CÁO"];
                            if (ws == null)
                            {
                                MessageBox.Show("Không tìm thấy sheet 'BÁO CÁO'");
                                return;
                            }

                            // ===== HEADER =====
                            ws.Cells["D1"].Value = "Ngày tạo : " + DateTime.Now.ToString("dd/MM/yyyy - H:mm");
                            ws.Cells["G2"].Value = $"Theo kỳ - Từ: {frdate.Value:dd/MM/yyyy} - đến: {todate.Value:dd/MM/yyyy}";
                            ws.Cells["N2"].Value = $"Lũy kế - Từ: {new DateTime(frdate.Value.Year, frdate.Value.Month, 1):dd/MM/yyyy} - đến: {todate.Value:dd/MM/yyyy}";

                            int startRow = 4;
                            int currentRow = startRow;

                            int colCount = reader.FieldCount;

                            // ===== CONFIG =====
                            int startSumCol = 5; // E
                            int endSumCol = 17;  // Q

                            int dataStartIndex = startSumCol - 2;
                            int dataEndIndex = endSumCol - 2;

                            decimal[] sumGroup = new decimal[colCount];
                            decimal[] sumAll = new decimal[colCount];

                            // ===== GROUP =====
                            int sttGroup = 1;
                            string currentRptKey = "";
                            int groupStartRow = startRow;

                            while (await reader.ReadAsync())
                            {
                                string rptKey = reader["Rpt_key"]?.ToString() ?? "";

                                // ===== ĐỔI GROUP =====
                                if (currentRptKey != "" && currentRptKey != rptKey)
                                {
                                    int rptKeyCol = 2;

                                    // merge
                                    ws.Cells[groupStartRow, rptKeyCol, currentRow - 1, rptKeyCol].Merge = true;

                                    // ===== DÒNG TỔNG GROUP =====
                                    ws.Cells[currentRow, 1].Value = sttGroup++; // STT chỉ ở đây
                                    ws.Cells[currentRow, rptKeyCol].Value = "Tổng " + currentRptKey;

                                    for (int i = dataStartIndex; i <= dataEndIndex && i < colCount; i++)
                                    {
                                        string colName = reader.GetName(i);

                                        if (colName == "Tyle_lai" || colName == "Lai_gop")
                                        {
                                            decimal dt = sumGroup[i - 2];
                                            decimal lai = sumGroup[i - 1];
                                            ws.Cells[currentRow, i + 2].Value = (dt != 0) ? lai / dt : 0;
                                        }
                                        else
                                        {
                                            ws.Cells[currentRow, i + 2].Value = sumGroup[i];
                                        }
                                    }

                                    using (var range = ws.Cells[currentRow, 1, currentRow, colCount + 1])
                                    {
                                        range.Style.Font.Bold = true;
                                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                                    }

                                    currentRow++;
                                    Array.Clear(sumGroup, 0, sumGroup.Length);
                                    groupStartRow = currentRow;
                                }

                                currentRptKey = rptKey;

                                // ===== DÒNG CHI TIẾT =====
                                ws.Cells[currentRow, 1].Value = ""; // KHÔNG đánh STT

                                for (int i = 0; i < colCount; i++)
                                {
                                    ws.Cells[currentRow, i + 2].Value = reader[i];
                                }

                                // ===== CỘNG TỔNG =====
                                for (int i = dataStartIndex; i <= dataEndIndex && i < colCount; i++)
                                {
                                    if (reader[i] != DBNull.Value &&
                                        decimal.TryParse(reader[i].ToString(), out decimal val))
                                    {
                                        sumGroup[i] += val;
                                        sumAll[i] += val;
                                    }
                                }

                                currentRow++;
                            }

                            // ===== GROUP CUỐI =====
                            int rptKeyColumn = 2;

                            ws.Cells[groupStartRow, rptKeyColumn, currentRow - 1, rptKeyColumn].Merge = true;

                            ws.Cells[currentRow, 1].Value = sttGroup++;
                            ws.Cells[currentRow, rptKeyColumn].Value = "Tổng " + currentRptKey;

                            for (int i = dataStartIndex; i <= dataEndIndex && i < colCount; i++)
                            {
                                string colName = reader.GetName(i);

                                if (colName == "Tyle_lai" || colName == "Lai_gop")
                                {
                                    decimal dt = sumGroup[i - 2];
                                    decimal lai = sumGroup[i - 1];
                                    ws.Cells[currentRow, i + 2].Value = (dt != 0) ? lai / dt : 0;
                                }
                                else
                                {
                                    ws.Cells[currentRow, i + 2].Value = sumGroup[i];
                                }
                            }

                            using (var range = ws.Cells[currentRow, 1, currentRow, colCount + 1])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                            }

                            currentRow++;

                            // ===== TỔNG TOÀN BỘ =====
                            ws.Cells[currentRow, 1].Value = "";
                            ws.Cells[currentRow, rptKeyColumn].Value = "TỔNG CỘNG";

                            for (int i = dataStartIndex; i <= dataEndIndex && i < colCount; i++)
                            {
                                string colName = reader.GetName(i);

                                if (colName == "Tyle_lai" || colName == "Lai_gop")
                                {
                                    decimal dt = sumAll[i - 2];
                                    decimal lai = sumAll[i - 1];
                                    ws.Cells[currentRow, i + 2].Value = (dt != 0) ? lai / dt : 0;
                                }
                                else
                                {
                                    ws.Cells[currentRow, i + 2].Value = sumAll[i];
                                }
                            }

                            using (var range = ws.Cells[currentRow, 1, currentRow, colCount + 2])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange);
                            }

                            // ===== FORMAT =====
                            using (var range = ws.Cells[startRow, 1, currentRow, colCount + 2])
                            {
                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            }

                            // format số
                            ws.Cells[startRow, startSumCol, currentRow, endSumCol].Style.Numberformat.Format = "#,##0";

                            // format %
                            ws.Cells[startRow, 13, currentRow, 13].Style.Numberformat.Format = "0.00%";
                            ws.Cells[startRow, 17, currentRow, 17].Style.Numberformat.Format = "0.00%";

                            // căn giữa STT
                            ws.Cells[startRow, 1, currentRow, 1].Style.HorizontalAlignment =
                                ExcelHorizontalAlignment.Center;

                            if (ws.Dimension != null)
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                            await package.SaveAsAsync(new FileInfo(outputPath));
                        }
                    }
                }
            }

            Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
        }
        private void WriteReaderToSheet(SqlDataReader reader, ExcelWorksheet ws, int startRow)
        {
            int colCount = reader.FieldCount;
            int row = startRow;

            // Ghi dữ liệu
            while (reader.Read())
            {
                for (int col = 0; col < colCount; col++)
                {
                    ws.Cells[row, col + 1].Value = reader.GetValue(col);
                }
                row++;
            }

            // Kẻ border
            if (row > startRow)
            {
                using (var range = ws.Cells[startRow, 1, row - 1, colCount])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
        }

        private async Task RunReportAsync_rpt_Industry_Gross_Profit_Marginy(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    command.CommandTimeout = 0; // Không giới hạn thời gian chạy

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Không có dữ liệu để xuất Excel.");
                            return;
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {

                            ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet1"];
                            if (sheet == null)
                            {
                                MessageBox.Show("Không tìm thấy sheet 'Sheet1' trong file template!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            int row = 1;         // Dòng bắt đầu ghi dữ liệu
                                                 // 👉 Ghi tên các cột vào dòng đầu tiên
                            for (int col = 0; col < reader.FieldCount; col++)
                            {
                                sheet.Cells[row, col + 1].Value = reader.GetName(col);
                                sheet.Cells[row, col + 1].Style.Font.Bold = true; // In đậm
                                sheet.Cells[row, col + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                sheet.Cells[row, col + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray); // Nền xám
                            }

                            row++; // Bắt đầu ghi dữ liệu từ dòng 2

                            // 👉 Ghi dữ liệu
                            while (await reader.ReadAsync())
                            {
                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    sheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                }
                                row++;
                            }
                            //while (await reader.ReadAsync())
                            //{
                            //    for (int col = 0; col < reader.FieldCount; col++)
                            //    {
                            //        sheet.Cells[row, col + 1].Value = reader.GetValue(col);
                            //    }
                            //    row++;
                            //}
                            await package.SaveAsAsync(new FileInfo(outputPath));
                            System.Diagnostics.Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
                        }
                    }
                }
            }
        }
        private async Task RunReportAsync_rpt_Member_Card(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    //command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    command.CommandTimeout = 0; // Không giới hạn thời gian chạy

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Không có dữ liệu để xuất Excel.");
                            return;
                        }

                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            var date_create = "Ngày tạo : " + DateTime.Now.ToString("dd/MM/yyyy - H:mm");
                            var frto = "Từ ngày: " + frdate.Value.ToString("dd/MM/yyyy") + " - đến ngày: " + todate.Value.ToString("dd/MM/yyyy");

                            ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet1"];
                            if (sheet == null)
                            {
                                MessageBox.Show("Không tìm thấy sheet 'Sheet1' trong file template!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Ghi thông tin ngày tháng vào Sheet1
                            sheet.Cells["A2"].Value = frto;
                            sheet.Cells["J1"].Value = date_create;

                            int row = 5;         // Dòng bắt đầu ghi dữ liệu
                            int sheetIndex = 1;  // Đánh số sheet
                            int maxRowsPerSheet = 500000; // Số dòng tối đa trên 1 sheet

                            while (await reader.ReadAsync())
                            {
                                if (row > maxRowsPerSheet + 4) // Nếu vượt 50,000 dòng, tạo sheet mới
                                {
                                    sheetIndex++;
                                    string newSheetName = "Sheet" + sheetIndex;
                                    sheet = package.Workbook.Worksheets.Add(newSheetName, package.Workbook.Worksheets["Sheet1"]);
                                    // XÓA dữ liệu từ dòng 5 trở đi
                                    int lastRow = sheet.Dimension.End.Row;
                                    sheet.Cells[5, 1, lastRow, 20].Clear();
                                    row = 5; // Reset lại số dòng để ghi dữ liệu vào Sheet mới
                                }

                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    sheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                }
                                row++;
                            }

                            // Kẻ ô cho toàn bộ dữ liệu trên tất cả các sheet
                            foreach (var ws in package.Workbook.Worksheets)
                            {
                                int endRow = row - 1;
                                int columns = reader.FieldCount;

                                using (var range = ws.Cells[5, 1, endRow, columns])
                                {
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                                // Tự động điều chỉnh độ rộng cột
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            }

                            await package.SaveAsAsync(new FileInfo(outputPath));
                            System.Diagnostics.Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
                        }
                    }
                }
            }
        }

        private async Task RunReportAsync_ByMonth(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }

                    if (stk_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@stk_id", stk_id.Text);
                    }

                    command.CommandTimeout = 0; // Đặt thời gian chờ theo cần thiết

                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Không có dữ liệu để xuất Excel.");
                        return;
                    }

                    // Đặt ngữ cảnh giấy phép để sử dụng EPPlus
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Nạp mẫu Excel
                    FileInfo templateFile = new FileInfo(templatePath);

                    using (ExcelPackage package = new ExcelPackage(templateFile, true))
                    {
                        // Truy cập vào tờ công việc đầu tiên
                        ExcelWorksheet mainWorksheet = package.Workbook.Worksheets[0];

                        // Lấy các nhóm duy nhất từ DataTable
                        var distinctGroups = dataTable.AsEnumerable().Select(row => row.Field<string>("Groups")).Distinct();

                        foreach (var group in distinctGroups)
                        {
                            // Sao chép tờ công việc chính cho mỗi nhóm
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[group];
                            if (worksheet == null)
                            {
                                // Nếu tờ không tồn tại, tạo một tờ mới
                                worksheet = package.Workbook.Worksheets.Add(group, mainWorksheet);
                            }
                            // Lọc dữ liệu cho nhóm hiện tại 
                            var groupData = dataTable.Select($"Groups = '{group}'").CopyToDataTable();
                            //var groupData = dataTable.Select($"Groups = '{group}'").OrderBy(r => r.Field<string>("grp_id").Substring(0, 2)).ThenBy(r => Convert.ToInt64(r.Field<long>("TT"))).CopyToDataTable();
                            // Lấy các nhóm duy nhất từ groupData
                            var distinctGroups_small = groupData.AsEnumerable().Select(row => row.Field<string>("nhom")).Distinct();
                            int startRow = 7;
                            // Giả sử bạn có một DateTimePicker tên là dateTimePicker1
                            DateTime selectedDate = frdate.Value;
                            int numberOfDaysInMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);

                            string year = selectedDate.Year.ToString();
                            string thang = selectedDate.Month.ToString("00");
                            string ngay = group.Substring(0, 2);

                            worksheet.Cells["Z3"].Value = $"NĂM {year}";
                            // Đặt dữ liệu cụ thể cho nhóm trên tờ công việc
                            worksheet.Cells["I3"].Value = $"Ngày: {ngay}/{thang}/{year}";// - Đến ngày : {todate.Value} ";
                            worksheet.Cells["O3"].Value = $"Tháng {thang}/{year}";
                            var RowTT1 = 0;
                            var RowTT2 = 0;
                            var RowTT3 = 0;
                            int columns_dem = 0;
                            foreach (var group_small in distinctGroups_small)
                            {

                                // Lọc dữ liệu cho nhóm hiện tại 
                                var groupData_small = groupData.Select($"nhom = '{group_small}'").CopyToDataTable();
                                string ten_nhom = groupData_small.Rows[0]["Ten_nhom"].ToString();
                                worksheet.Cells[startRow, 4].Value = ten_nhom;
                                worksheet.Cells[startRow, 4, startRow, 5].Merge = true;

                                worksheet.Cells[startRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                // Đặt định dạng chữ đậm cho ô đã merge
                                worksheet.Cells[startRow, 4, startRow, 5].Style.Font.Bold = true;

                                startRow++;
                                int strartGroup_row = startRow;
                                foreach (DataRow row in groupData_small.Rows)
                                {
                                    int i;
                                    for (i = 0; i < groupData_small.Columns.Count - 3; i++)
                                    {
                                        worksheet.Cells[startRow, i + 1].Value = row[i];
                                        columns_dem = i;
                                    }
                                    // DT bán lẻ bình quân
                                    worksheet.Cells[startRow, 13].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 9].Address}, 0)";
                                    worksheet.Cells[startRow, 14].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";
                                    worksheet.Cells[startRow, 24].Formula = $"=IFERROR({worksheet.Cells[startRow, 22].Address} / {worksheet.Cells[startRow, 20].Address}, 0)";
                                    worksheet.Cells[startRow, 25].Formula = $"=IFERROR({worksheet.Cells[startRow, 22].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";
                                    worksheet.Cells[startRow, 30].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 28].Address}, 0)";
                                    worksheet.Cells[startRow, 31].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";

                                    worksheet.Cells[startRow, 17].Formula = $"=IFERROR({worksheet.Cells[startRow, 15].Address} / {worksheet.Cells[startRow, 16].Address}, 0)";
                                    worksheet.Cells[startRow, 18].Formula = $"=IFERROR({worksheet.Cells[startRow, 23].Address} / {worksheet.Cells[startRow, 15].Address}, 0)";
                                    worksheet.Cells[startRow, 19].Formula = $"=IFERROR(({worksheet.Cells[startRow, 22].Address}/ {ngay}*{numberOfDaysInMonth} + {worksheet.Cells[startRow, 21].Address})/{worksheet.Cells[startRow, 15].Address}, 0)";
                                    worksheet.Cells[startRow, 33].Formula = $"=IFERROR({worksheet.Cells[startRow, 29].Address} / {worksheet.Cells[startRow, 32].Address}, 0)";


                                    startRow++;
                                }
                                // Gán giá trị "Tổng" cho ô đã merge
                                worksheet.Cells[startRow, 4].Value = $"Tổng {ten_nhom}";
                                // Merge cột A5 đến I5
                                worksheet.Cells[startRow, 4, startRow, 5].Merge = true;

                                worksheet.Cells[startRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                // Đặt định dạng chữ đậm cho ô đã merge
                                worksheet.Cells[startRow, 1, startRow, columns_dem + 2].Style.Font.Bold = true;
                                for (int colIndex = 6; colIndex <= 33; colIndex++) /// Cột F đến AG
                                {
                                    worksheet.Cells[startRow, colIndex].Formula = $"SUM({GetExcelColumnName(colIndex)}{strartGroup_row}:{GetExcelColumnName(colIndex)}{startRow - 1})";
                                }
                                // DT bán lẻ bình quân
                                worksheet.Cells[startRow, 13].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 9].Address}, 0)";
                                worksheet.Cells[startRow, 14].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";
                                worksheet.Cells[startRow, 24].Formula = $"=IFERROR({worksheet.Cells[startRow, 22].Address} / {worksheet.Cells[startRow, 20].Address}, 0)";
                                worksheet.Cells[startRow, 25].Formula = $"=IFERROR({worksheet.Cells[startRow, 22].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";
                                worksheet.Cells[startRow, 30].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 28].Address}, 0)";
                                worksheet.Cells[startRow, 31].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";

                                worksheet.Cells[startRow, 17].Formula = $"=IFERROR({worksheet.Cells[startRow, 15].Address} / {worksheet.Cells[startRow, 16].Address}, 0)";
                                worksheet.Cells[startRow, 18].Formula = $"=IFERROR({worksheet.Cells[startRow, 23].Address} / {worksheet.Cells[startRow, 15].Address}, 0)";
                                worksheet.Cells[startRow, 19].Formula = $"=IFERROR(({worksheet.Cells[startRow, 22].Address} / {ngay}*{numberOfDaysInMonth} + {worksheet.Cells[startRow, 21].Address})/{worksheet.Cells[startRow, 15].Address}, 0)";
                                worksheet.Cells[startRow, 33].Formula = $"=IFERROR({worksheet.Cells[startRow, 29].Address} / {worksheet.Cells[startRow, 32].Address}, 0)";

                                if (ten_nhom == "MART")
                                {
                                    RowTT1 = startRow;
                                }
                                if (ten_nhom == "MiniMART")
                                {
                                    RowTT2 = startRow;
                                }
                                if (ten_nhom == "FujiMART")
                                {
                                    RowTT3 = startRow;
                                }
                                startRow++;
                            }
                            for (int colIndex = 6; colIndex <= 33; colIndex++) // Cột F đến AG
                            {
                                worksheet.Cells[startRow, colIndex].Formula = $"({GetExcelColumnName(colIndex)}{RowTT1}+{GetExcelColumnName(colIndex)}{RowTT2}+{GetExcelColumnName(colIndex)}{RowTT3})";
                            }
                            // Gán giá trị "Tổng" cho ô đã merge
                            worksheet.Cells[startRow, 1].Value = "TỔNG HỆ THỐNG";
                            worksheet.Cells[startRow, 1, startRow, 2].Merge = true;
                            worksheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                            // Đặt định dạng chữ đậm cho ô đã merge
                            worksheet.Cells[startRow, 1, startRow, columns_dem + 2].Style.Font.Bold = true;

                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"F7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Numberformat.Format = "_(* #,##0_);_(* (#,##0);_(* \"-\"??_);_(@_)";

                            worksheet.Cells[$"{GetExcelColumnName(33)}7:{GetExcelColumnName(33)}{startRow}"].Style.Numberformat.Format = "0%";
                            worksheet.Cells[$"{GetExcelColumnName(18)}7:{GetExcelColumnName(19)}{startRow}"].Style.Numberformat.Format = "0%";
                        }
                        // Kiểm tra xem tờ đã tồn tại hay chưa
                        if (package.Workbook.Worksheets.Any(ws => ws.Name == "Templ"))
                        {
                            // Nếu tờ đã tồn tại, xóa nó đi
                            package.Workbook.Worksheets.Delete("Templ");
                        }
                        // Kiểm tra xem sheet theo ngày xem đã có chưa

                        var sheet_name_sear = frdate.Value.ToString("dd") + "." + frdate.Value.ToString("MM");
                        int sheetIndex = -1; // Khởi tạo giá trị mặc định

                        for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                        {
                            if (package.Workbook.Worksheets[i].Name == sheet_name_sear)
                            {
                                sheetIndex = i; // Lưu vị trí tờ khi tìm thấy tờ có tên giống
                                break; // Thoát khỏi vòng lặp sau khi tìm thấy tờ
                            }
                        }

                        if (sheetIndex != -1)
                        {
                            // Tồn tại tờ có tên giống với sheet_name_sear
                            // Sử dụng sheetIndex để thực hiện các thao tác tiếp theo với tờ đó
                            ExcelWorksheet foundWorksheet = package.Workbook.Worksheets[sheetIndex];
                            foundWorksheet.Select();
                        }
                        // Lưu gói Excel đã được sửa đổi vào tệp đầu ra
                        package.SaveAs(new FileInfo(outputPath));

                        // Mở tệp Excel sau khi đã lưu
                        System.Diagnostics.Process.Start(outputPath);
                    }
                }
            }
        }
        private async Task RunReportAsync_Total_old(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }

                    if (dept_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@dept_id", dept_id.Text);
                    }

                    command.CommandTimeout = 0; // Set timeout as needed
                                                // Giả sử bạn có một DataTable để lưu trữ kết quả
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    // Kiểm tra xem có dữ liệu trong dataTable không
                    if (dataTable.Rows.Count == 0)
                    {
                        // Thông báo hoặc xử lý khi không có dữ liệu
                        MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Không có dữ liệu để xuất Excel.");
                        return; // hoặc thực hiện các hành động khác theo yêu cầu của bạn
                    }
                    // Set the license context to use EPPlus
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
                                                                                // Load tệp Excel mẫu
                    FileInfo templateFile = new FileInfo(templatePath);

                    // Tạo một gói Excel mới dựa trên mẫu
                    using (ExcelPackage package = new ExcelPackage(templateFile, true))
                    {
                        // Truy cập vào tờ công việc đầu tiên
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        int startRow = 5;

                        worksheet.Cells["B2"].Value = $"Từ ngày: {frdate.Value} - Đến ngày : {todate.Value} ";


                        int columns_dem = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int i;
                            for (i = 0; i < dataTable.Columns.Count; i++)
                            {
                                // Giả sử các cột trong DataTable tương ứng với các cột trong Excel
                                //worksheet.Cells[startRow, i + 1].Value = row[i].ToString();
                                worksheet.Cells[startRow, i + 1].Value = row[i];
                                columns_dem = i;
                            }

                            worksheet.Cells[startRow, i + 1].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 1].Address})-1, 0)";
                            worksheet.Cells[startRow, i + 2].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 1].Address}";
                            worksheet.Cells[startRow, i + 3].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 2].Address})-1, 0)";
                            worksheet.Cells[startRow, i + 4].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 2].Address}";


                            startRow++;
                        }

                        // Áp dụng quy tắc định dạng
                        ExcelAddress address = new ExcelAddress($"F5:I{startRow}");
                        var formattingRule = worksheet.ConditionalFormatting.AddLessThan(address);
                        formattingRule.Formula = "0";
                        formattingRule.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink); //= "#FFC7CE"
                        formattingRule.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                        //formattingRule.Style.Fill.BackgroundColor.Color = System.Drawing.Color.from(255, 205, 92, 92); // IndianRed color
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].AutoFitColumns();
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"C5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Numberformat.Format = "#,##0.0,,\"tr\"";

                        worksheet.Cells[$"G5:G{startRow}"].Style.Numberformat.Format = "0%";
                        worksheet.Cells[$"I5:I{startRow}"].Style.Numberformat.Format = "0%";

                        //worksheet.Cells[$"J5:L{startRow}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlanchedAlmond);
                        var range = worksheet.Cells[$"H5:J{startRow}"];
                        var fill = range.Style.Fill;
                        // Thiết lập kiểu mẫu trước khi đặt màu nền Cho cột Doanh THu MTD
                        fill.PatternType = ExcelFillStyle.Solid;
                        // Bây giờ đặt màu nền Cho cột Doanh THu MTD
                        fill.BackgroundColor.SetColor(System.Drawing.Color.Cornsilk);

                        worksheet.Cells["E4"].Value = frdate.Value.ToString("dd/MM/yyyy");
                        DateTime frdateValue = frdate.Value;
                        DateTime previousMonth = frdateValue.AddMonths(-1);
                        worksheet.Cells["D4"].Value = previousMonth.ToString("dd/MM/yyyy");
                        DateTime previousYear = frdateValue.AddYears(-1);
                        worksheet.Cells["C4"].Value = previousYear.ToString("dd/MM/yyyy");


                        // Lưu gói Excel đã được sửa đổi vào tệp đầu ra
                        package.SaveAs(new FileInfo(outputPath));
                        // Mở tệp Excel sau khi đã lưu
                        System.Diagnostics.Process.Start(outputPath);
                    }
                }
            }
        }
        private async void bt_BC_Click_Gọi_API(object sender, EventArgs e)
        {
            if (lbl_API.Text.ToLower() == "true")
            {
                apiUrl = Pro_name.Text.Trim();
                if (!isDownloading)
                {
                    isDownloading = true;

                    // Đường dẫn mặc định cho SaveFileDialog
                    string defaultSavePath = "path/to/save/excel.xlsx";

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                        saveFileDialog.Title = "Chọn vị trí để lưu tệp Excel";
                        saveFileDialog.FileName = "excel.xlsx"; // Tên mặc định của tệp

                        // Hiển thị hộp thoại và kiểm tra nếu người dùng đã chọn một vị trí
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Lấy đường dẫn được chọn
                            string filePath = saveFileDialog.FileName;

                            try
                            {
                                await DownloadAndSaveExcel(apiUrl, filePath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi khi tải xuống file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                isDownloading = false;
                            }
                        }
                        else
                        {
                            // Người dùng đã hủy hoặc đóng hộp thoại, bạn có thể xử lý tùy thuộc vào yêu cầu của ứng dụng
                            isDownloading = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("File đang được tải xuống. Vui lòng đợi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async Task DownloadAndSaveExcel(string apiUrl, string filePath)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string requestData = "{ \"frDate\": \"2019-11-21T08:56:05.314Z\", \"toDate\": \"2023-11-21T08:56:05.314Z\", \"supp_ID\": \"string\", \"branchId\": \"string\", \"hcrC1_2FUJI_3All\": 0, \"contractType_1Mua_2Kg\": 0 }";
                    //string requestData = "{ \"frDate\": \"2019-11-21T08:56:05.314Z\", \"toDate\": \"2023-11-21T08:56:05.314Z\", \"supp_ID\": null, \"branchId\": null, \"hcrC1_2FUJI_3All\": 0, \"contractType_1Mua_2Kg\": 0 }";
                    //para_name
                    //string requestData = $"stk_id ={stk_id.Text},frDate= {frdate.Value} ,toDate={todate.Value}";
                    //apiUrl = "https://uat-reports_center-api.hcrc.vn/api/Ngam_Cuu/BC-Do_Phu_ASM";

                    string stkId = stk_id.Text;
                    DateTime frDate1 = frdate.Value;
                    DateTime toDate1 = todate.Value;

                    // Tạo đối tượng chứa các tham số
                    var parameters = new
                    {
                        stk_id = stkId,
                        FrDate = frDate1,
                        ToDate = toDate1
                    };
                    // Chuyển đối tượng thành chuỗi JSON
                    //string requestData = JsonConvert.SerializeObject(parameters);
                    string requestData = "?stk_id=001%2C002&FrDate=12%2F05%2F2023&ToDate=12%2F10%2F2023";
                    // Gửi request POST đồng bộ
                    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(requestData, Encoding.UTF8, "application/json"));
                    // Thực hiện yêu cầu GET đến API
                    //HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    // Kiểm tra xem request có thành công không
                    // Kiểm tra xem request có thành công không
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc dữ liệu từ response và lưu vào file
                        byte[] content = await response.Content.ReadAsByteArrayAsync();
                        System.IO.File.WriteAllBytes(filePath, content);

                        MessageBox.Show("Tải file thành công!" +
                            "", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //MessageBox.Show($"Lỗi khi tải file: {response.StatusCode}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Lỗi khi tải file: {response.StatusCode}\n{responseContent}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void report_name_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.User_Name.UpperCase() == "ADMIN")
            {
                Node_id.Visible = true;
                Pro_name.Visible = true;
                gr_para_name.Visible = true;
                para_name.Visible = true;
                lbl_API.Visible = true;
            }
        }
        private string GetExcelColumnName(int columnIndex)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int dividend = columnIndex;
            string columnName = "";

            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = letters[modulo] + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        private void ma_core_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ma_core.SelectedValue != null && int.TryParse(ma_core.SelectedValue.ToString(), out int selectedValue))
            //{
            //    MessageBox.Show("Giá trị chọn là: " + selectedValue);
            //}
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private bool _updating = false;

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            //if (_updating) return;

            //try
            //{
            //    _updating = true;

            //    string txt = comboBox1.Text;

            //    var filtered = _allItems
            //        .Where(x =>
            //            x.IndexOf(txt,
            //                StringComparison.OrdinalIgnoreCase) >= 0)
            //        .ToArray();

            //    comboBox1.BeginUpdate();

            //    comboBox1.Items.Clear();
            //    comboBox1.Items.AddRange(filtered);

            //    comboBox1.EndUpdate();

            //    comboBox1.DroppedDown = true;

            //    comboBox1.SelectionStart = txt.Length;
            //    comboBox1.SelectionLength = 0;
            //}
            //finally
            //{
            //    _updating = false;
            //}
        }

        private bool _loading = false;

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_loading) return;

            string txt = comboBox1.Text;

            var filtered = _allItems
                .Where(x =>
                    x.IndexOf(txt,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();

            BeginInvoke(new Action(() =>
            {
                _loading = true;

                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(filtered);

                comboBox1.DroppedDown = true;

                comboBox1.Text = txt;
                comboBox1.SelectionStart = txt.Length;

                _loading = false;
            }));
        }
    }
}
