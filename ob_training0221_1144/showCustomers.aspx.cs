using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ob_training0221_1144
{
    public partial class showCustomers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomerData();
            }
        }

        private void LoadCustomerData()
        {
            //資料庫連線設定
            string connectSetting = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            //SQL 語法
            string Sql = "SELECT name, phone, address FROM customers";

            using (SqlConnection linkConnection = new SqlConnection(connectSetting))
            {
                SqlCommand cmd = new SqlCommand(Sql, linkConnection);
                linkConnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                string html = "";

                while (reader.Read())
                {
                    html += "<tr>";
                    html += "<td>" + reader["name"].ToString() + "</td>";
                    html += "<td>" + reader["phone"].ToString() + "</td>";
                    html += "<td>" + reader["address"].ToString() + "</td>";
                    html += "</tr>";
                }

                CustomerData.Text = html;
            }
        }



    }
}