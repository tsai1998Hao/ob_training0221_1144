using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Http;
using System.Diagnostics;
using Newtonsoft.Json;


// API測試  localhost:44380/api/customer/get-by-userid

//{
//    "user_id": 6
//}


namespace ob_training0221_1144.ApiControllers
{

    // 取得所有客戶資
    public class CustomersController : ApiController
    {
        private string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;"; // 請根據實際的資料庫連線字串修改

        // 根據 email 查詢客戶資料
        [HttpPost]
        [Route("api/customer/get-by-userid")] // 對應到特定路由
        public IHttpActionResult GetCustomerByUserId([FromBody] IdRequest idRequest)
        {
            try
            {
                if (idRequest == null || idRequest.UserId <= 0)
                {
                    Debug.WriteLine($"UserId: {idRequest.UserId}");
                    Console.WriteLine(idRequest.UserId.ToString(), 999999);

                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        status = "error",
                        message = "Invalid UserId."
                    }));
                }
                List<Customer> customers = new List<Customer>(); // 用來儲存多筆資料


                //List<Customer> customers = new List<Customer>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // 修改 SQL 查詢，選擇需要的欄位
                    string sql = "SELECT id, name, phone, address, created_at FROM Customers WHERE user_id = @UserId";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.Parameters.AddWithValue("@Email", emailRequest.Email); // 防止 SQL 注入
                        cmd.Parameters.AddWithValue("@UserId", idRequest.UserId); // 防止 SQL 注入

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer
                                {
                                    Customer_id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Phone = reader.GetString(2),
                                    Address = reader.GetString(3),
                                    CreatedAt = reader.GetDateTime(4)
                                });
                            }
                        }
                    }
                }

                if (customers.Count == 0)
                {
                    return Ok(new
                    {
                        status = "error",
                        message = "No customers found for this user ID."
                    });
                }

                return Ok(new
                {
                    status = "success",
                    data = customers
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // 發生錯誤時返回 500
            }
        }
    }


    // **Customer 類別 old**
    //    public class Customer
    //    {
    //        public int Id { get; set; }
    //        public string Name { get; set; }
    //        public string Phone { get; set; }
    //    }
    //}


    // 客戶資料類別，包含所需欄位
    public class Customer
    {
        public int Customer_id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        [JsonIgnore] // 隱藏原本的 DateTime 欄位，避免輸出兩個 created_at
        public DateTime CreatedAt { get; set; }

        [JsonProperty("Created_at")] // 讓這個欄位取代原本的 created_at
        public string CreatedAtFormatted => CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
    }

    // 用來接收 POST 請求中的 email 參數
    //    public class EmailRequest
    //    {
    //        public string Email { get; set; }
    //    }
    //}


    // 用來接收 POST 請求中的 id 參數

    public class IdRequest
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }

}