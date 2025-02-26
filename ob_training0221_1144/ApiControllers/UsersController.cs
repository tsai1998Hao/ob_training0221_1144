using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Http;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ob_training0221_1144.ApiControllers
{

    // 取得所有客戶資
    public class UsersController : ApiController
    {
        private string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;"; // 請根據實際的資料庫連線字串修改

        // 根據 email 查詢客戶資料
        [HttpPost]
        [Route("api/user/get-by-email")] // 對應到特定路由
        public IHttpActionResult GetUserByEmail([FromBody] EmailRequest emailRequest)
        {
            try
            {
                if (emailRequest == null || string.IsNullOrEmpty(emailRequest.Email))
                {
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        status = "error",
                        message = "Email is required."
                    }));
                }
                //List<Customer> customers = new List<Customer>(); // 用來儲存多筆資料


                User user = null;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // 修改 SQL 查詢，選擇需要的欄位
                    string sql = "SELECT id, name, email, created_at FROM Users WHERE email = @Email";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {

                        cmd.Parameters.AddWithValue("@Email", emailRequest.Email); // 防止 SQL 注入

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    User_id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    CreatedAt = reader.GetDateTime(3)
                                };
                            }
                        }
                    }
                }

                if (user == null)
                {
                    return Ok(new
                    {
                        status = "error",
                        message = "User not found."
                    });
                }

                return Ok(new
                {
                    status = "success",
                    data = user
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // 發生錯誤時返回 500
            }
        }
    }





    // 客戶資料類別，包含所需欄位
    public class User
    {
        public int User_id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore] // 隱藏原本的 DateTime 欄位，避免輸出兩個 created_at
        public DateTime CreatedAt { get; set; }

        [JsonProperty("Created_at")] // 讓這個欄位取代原本的 created_at
        public string CreatedAtFormatted => CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
    }

    // 用來接收 POST 請求中的 email 參數
    public class EmailRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
