using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ob_training0221_1144
{
    public partial class GetUserByEmail : Page
    {
        // 定義資料庫連線字串
        private string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";

        protected void Page_Loadd(object sender, EventArgs e)
        {
            // 如果有發送 POST 請求處理
            if (Request.HttpMethod == "POST")
            {
                string email = Request.Form["email"];
                if (!string.IsNullOrEmpty(email))
                {
                    GetUserByEmailFromD(email);
                }
                else
                {
                    SendErrorResponsee("Email is required.");
                }
            }
        }

        // 點擊搜尋按鈕時觸發
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            GetUserByEmailFromD(email);
        }

        // 根據 email 查詢使用者資料
        private void GetUserByEmailFromD(string email)
        {
            string query = "SELECT user_id, name, email, created_at FROM Users WHERE email = @Email";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string userId = reader["user_id"].ToString();
                        string name = reader["name"].ToString();
                        string userEmail = reader["email"].ToString();
                        string createdAt = reader["created_at"].ToString();

                        // 送回成功的 JSON 資料
                        SendSuccessResponse(userId, name, userEmail, createdAt);
                    }
                    else
                    {
                        SendErrorResponsee("User not found.");
                    }
                }
            }
        }

        // 回傳成功的 JSON 回應
        private void SendSuccessResponse(string userId, string name, string email, string createdAt)
        {
            var response = new
            {
                status = "success",
                data = new
                {
                    user_id = userId,
                    name = name,
                    email = email,
                    created_at = createdAt
                }
            };

            string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            Response.ContentType = "application/json";
            Response.Write(jsonResponse);
            Response.End();
        }

        // 回傳錯誤的 JSON 回應
        private void SendErrorResponsee(string message)
        {
            var response = new
            {
                status = "error",
                message = message
            };

            string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            Response.ContentType = "application/json";
            Response.Write(jsonResponse);
            Response.End();
        }
    }
}
