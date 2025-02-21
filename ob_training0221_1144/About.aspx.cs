using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ob_training0221_1144
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 這行不會在 Web Forms 顯示
            System.Diagnostics.Debug.WriteLine("Page_Load called.");
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            //要先設置 asp:TextBox ID="email" runat="server"
            string Name = name.Text;
            string Email = email.Text;
            string Password = password.Text;

            // 確保所有欄位都有填寫
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                Response.Write("<script>alert('請填寫所有欄位');</script>");
                return;
            }


            // 將密碼加密成 MD5 
            string encryptedPassword = GetMD5Hash(Password);

            try
            {
                // 資料庫連線字串
                string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";

                // SQL 新增語法
                string query = "INSERT INTO users (name, email, password, created_at) VALUES (@name, @email, @password, @created_at)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // 使用參數化查詢，防止 SQL 注入攻擊
                        cmd.Parameters.AddWithValue("@name", Name);
                        cmd.Parameters.AddWithValue("@email", Email);
                        cmd.Parameters.AddWithValue("@password", encryptedPassword);
                        cmd.Parameters.AddWithValue("@created_at", DateTime.Now);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery(); // 執行 SQL 語法
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('會員註冊成功！');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('會員註冊失敗，請稍後再試！');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('發生錯誤：" + ex.Message + "');</script>");
            }
        }

        //密碼加密
        private string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // 轉換成 16 進位字串
                }
                return sb.ToString();
            }
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}


