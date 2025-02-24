using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ob_training0221_1144
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e){ }

        //登入功能
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string Email = email.Text;
            string Password = password.Text;
            System.Diagnostics.Debug.WriteLine(Email, Password);

            // 將密碼加密成 MD5 
            string encryptedPassword = GetMD5Hash(Password);

            try
            {
                // 資料庫連線字串
                string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";

                // 查詢是否有符合的電子郵件和密碼
                string query = "SELECT COUNT(1) FROM users WHERE email = @email AND password = @password";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // 使用參數化查詢，防止 SQL 注入攻擊
                        cmd.Parameters.AddWithValue("@email", Email);
                        cmd.Parameters.AddWithValue("@password", encryptedPassword); // 比對加密後的密碼

                        conn.Open();
                        int userExists = (int)cmd.ExecuteScalar();  // 會返回該查詢語句的第一行第一列的數值

                        if (userExists > 0)
                        {
                            // 登入成功
                            Response.Write("<script>alert('登入成功！');</script>");
                            Response.Redirect("showCustomers.aspx");
                        }
                        else
                        {
                            // 登入失敗
                            Response.Write("<script>alert('登入失敗，請檢查電子郵件或密碼');</script>");
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
}