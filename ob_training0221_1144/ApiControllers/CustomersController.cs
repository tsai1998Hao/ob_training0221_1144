using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace ob_training0221_1144.ApiControllers
{
    public class CustomersController : ApiController
    {
        private string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";

        // 取得所有客戶資料
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            try
            {
                List<Customer> customers = new List<Customer>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Customers";  // 修改成你的資料表名稱
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Phone = reader.GetString(2)
                            });
                        }
                    }
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    // **定義 Customer 類別**
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
