using Lib.Utils.Package;
using Report_Center.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Report_Center.Main;
using Newtonsoft.Json;

namespace Report_Center.Presentation
{
    public partial class fr_DangNhap : Form
    {
        //private string connectionString = "Data Source=172.16.71.170;Initial Catalog=HCRC_Report_Center;User ID=hieund;Password=123@123123;";
        //public int UserID { get; private set; } // Thuộc tính để lưu trữ UserID
        public fr_DangNhap()
        {
            InitializeComponent();
            ApplyEnterKeyToAllControls(this);
        }
        //ConnectDB db = new ConnectDB();
        //public string Server { get; set; }
        private async void cmddn_Click(object sender, EventArgs e)
        {
            string username = txtuser.Text.Trim().ToUpper();
            string password = txtpass.Text; // password gốc

            GlobalVariables.User_Name = username;
            GlobalVariables.User_Pass = AES.Encrypt(password); // vẫn lưu encrypt nếu cần

            // 1. Check DB trước
            int userID = AuthenticateUserAndGetUserID(username, GlobalVariables.User_Pass);

            if (userID != -1)
            {
                // Login DB OK
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            // 2. Nếu DB fail → gọi API
            var token = await GetTokenAsync(username, password);

            if (!string.IsNullOrEmpty(token))
            {
                // Login API OK
                GlobalVariables.AccessToken = token;

                DialogResult = DialogResult.OK;
                // 1. Check API
                int userID1 = AuthenticateUserAndGetUserID_API(username, GlobalVariables.User_Pass);
                Close();
            }
            else
            {
                // Fail cả 2
                MessageBox.Show(
                    "Đăng nhập không thành công (DB + API).",
                    "Lỗi đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        public async Task<string> GetTokenAsync(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var url = "https://hrapi.hcrc.vn/oauth/token";

                var formData = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password },
                    { "domainId", "1" },
                    { "grant_type", "password" }
                };

                var content = new FormUrlEncodedContent(formData);

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(json);

                return result?.access_token;
            }
        }
        private void cmddn_Click_Chua_goi_API(object sender, EventArgs e)
        {
            GlobalVariables.User_Name = txtuser.Text.UpperCase();
            GlobalVariables.User_Pass = AES.Encrypt(txtpass.Text);
            string tam1111 = AES.Decrypt("AQAAAAEAACcQAAAAEAMWn7rf+uY9uHJEJyV6yZrOFrUutZ8X6c4baosrIEeqk48Dll07cx4jZ7Disc+48w==");
            var t = GlobalVariables.User_Pass;
            //var User_Pass_Decrypt = AES.Decrypt(t);
            //var tt = User_Pass_Decrypt;
            int userID = AuthenticateUserAndGetUserID(GlobalVariables.User_Name, GlobalVariables.User_Pass);

            if (userID != -1)
            {
                // Đăng nhập thành công, có thể sử dụng giá trị userID ở đây
                // ...

                // Đặt DialogResult là OK và đóng hộp thoại
                DialogResult = DialogResult.OK;

                Close();
            }
            else
            {
                // Xử lý trường hợp đăng nhập không thành công
                MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int AuthenticateUserAndGetUserID(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetUser_SSO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        var aaaa = pass_all.ToString();
                        command.Parameters.AddWithValue("@pass_all", pass_all.ToString());

                        // Thêm tham số output
                        SqlParameter userIdParam = new SqlParameter("@UserID", SqlDbType.Int);
                        userIdParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(userIdParam);

                        SqlParameter demSlParam = new SqlParameter("@DEM_SL", SqlDbType.Int);
                        demSlParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(demSlParam);

                        // Thực thi stored procedure
                        command.ExecuteNonQuery();

                        // Lấy giá trị từ tham số output
                        GlobalVariables.UserID = Convert.ToInt32(userIdParam.Value);
                        GlobalVariables.demSl = Convert.ToInt32(demSlParam.Value);

                        // Bây giờ bạn có thể sử dụng giá trị của UserID và DEM_SL theo cách mong muốn
                        return GlobalVariables.UserID;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fr_Ketnoi fr = new fr_Ketnoi();
                    //read.Close();
                    fr.ShowDialog();
                    return -1; // Đặt giá trị đặc biệt khi có lỗi
                }
            }
        }

        private int AuthenticateUserAndGetUserID_API(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetUser_API", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Username", username);                        
                        var aaaa = pass_all.ToString();
                        command.Parameters.AddWithValue("@pass_all", pass_all.ToString());

                        // Thêm tham số output
                        SqlParameter userIdParam = new SqlParameter("@UserID", SqlDbType.Int);
                        userIdParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(userIdParam);

                        SqlParameter demSlParam = new SqlParameter("@DEM_SL", SqlDbType.Int);
                        demSlParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(demSlParam);

                        // Thực thi stored procedure
                        command.ExecuteNonQuery();

                        // Lấy giá trị từ tham số output
                        GlobalVariables.UserID = Convert.ToInt32(userIdParam.Value);
                        GlobalVariables.demSl = Convert.ToInt32(demSlParam.Value);

                        // Bây giờ bạn có thể sử dụng giá trị của UserID và DEM_SL theo cách mong muốn
                        return GlobalVariables.UserID;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fr_Ketnoi fr = new fr_Ketnoi();
                    //read.Close();
                    fr.ShowDialog();
                    return -1; // Đặt giá trị đặc biệt khi có lỗi
                }
            }
        }

        private string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the hashed bytes to a string (hex representation)
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    stringBuilder.Append(hashedBytes[i].ToString("X2"));
                }

                return stringBuilder.ToString();
            }
        }

        private void cmdthoat_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.First_Time == 1)
            {
                Application.Exit();
            }
            else
            {
                this.Close();
            }
        }
        // Sự kiện xử lý khi nhấn Enter
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Chuyển focus sang control tiếp theo
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        // Phương thức áp dụng sự kiện KeyDown cho tất cả các control trên form
        private void ApplyEnterKeyToAllControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                ctrl.KeyDown += OnKeyDown;
                ApplyEnterKeyToAllControls(ctrl);
            }
        }
        public class TokenResponse
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
        }

    }
}
