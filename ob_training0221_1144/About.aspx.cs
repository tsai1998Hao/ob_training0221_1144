using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using System.Data.SqlClient;

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
            string Name = name.Text;
            string Email = email.Text;
            string Password = password.Text;

            // 確保所有欄位都有填寫
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                Response.Write("<script>alert('請填寫所有欄位');</script>");
                return;
            }

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
                        cmd.Parameters.AddWithValue("@password", Password);
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












        //protected void Page_LoadComplete(object sender, EventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("Page_LoadComplete triggered.");

        //    if (Request.HttpMethod == "POST")
        //    {
        //        System.Diagnostics.Debug.WriteLine("跑出來1");

        //        try
        //        {

        //            // 確保請求的 Content-Type 為 application/json
        //            if (!Request.ContentType.ToLower().Contains("application/json"))
        //            {
        //                Response.ContentType = "application/json";
        //                Response.Write("{\"success\": false, \"error\": \"Invalid content type. Expected application/json.\"}");
        //                Response.End();
        //                return;
        //            }

        //            // 讀取 JSON 數據
        //            string jsonData = new StreamReader(Request.InputStream).ReadToEnd();
        //            System.Diagnostics.Debug.WriteLine("Received JSON: " + jsonData); // 用來 Debug



        //            // 解析 JSON
        //            var userData = JsonConvert.DeserializeObject<User>(jsonData);
        //            if (userData == null)
        //            {
        //                throw new Exception("Invalid JSON format.");
        //            }
        //            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
        //            string query = "INSERT INTO users (name, email, password, created_at) VALUES (@name, @email, @password, @created_at)";

        //            using (SqlConnection conn = new SqlConnection(connectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand(query, conn);
        //                cmd.Parameters.AddWithValue("@name", userData.Name);
        //                cmd.Parameters.AddWithValue("@email", userData.Email);
        //                cmd.Parameters.AddWithValue("@password", userData.Password);
        //                cmd.Parameters.AddWithValue("@created_at", DateTime.Now);

        //                conn.Open();
        //                cmd.ExecuteNonQuery();
        //            }

        //            Response.ContentType = "application/json";
        //            Response.Write("{\"success\": true}");
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.ContentType = "application/json";
        //            Response.Write("{\"success\": false, \"error\": \"" + ex.Message + "\"}");
        //        }
        //        Response.End();
        //    }
        //}
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
