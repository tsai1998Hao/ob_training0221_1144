using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


namespace ob_training0221_1144
{
    public partial class showCustomers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomers();
            }
        }

        private void LoadCustomers()
        {
            //資料庫連線設定
            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            //SQL 語法
            string query = "SELECT id, name, phone, address FROM customers";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvCustomers.DataSource = dt;
                gvCustomers.DataBind();
            }
        }



        protected void gvCustomers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCustomers.EditIndex = e.NewEditIndex;
            LoadCustomers();  // 重新加載資料並顯示在編輯模式
        }





        protected void gvCustomers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);
            string name = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("txtName")).Text;
            // 沒有email
            string email = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("txtEmail")).Text;

            string connectionString = "Server=your_server;Initial Catalog=your_database;User ID=your_user;Password=your_password;";
            string query = "UPDATE Customers SET Name = @Name, Email = @Email WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvCustomers.EditIndex = -1;  // 退出編輯模式
            LoadCustomers();  // 重新加載資料
        }


        protected void gvCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);

            string connectionString = "Server=your_server;Initial Catalog=your_database;User ID=your_user;Password=your_password;";
            string query = "DELETE FROM Customers WHERE CustomerId = @CustomerId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadCustomers();  // 重新加載資料
        }


        protected void gvCustomers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // 設定 GridView 的 EditIndex 為 -1，這樣就能退出編輯模式
            gvCustomers.EditIndex = -1;

            // 重新綁定資料（這樣就可以顯示更新後的資料）
            BindGridView();
        }





        private void BindGridView()
        {
            string connectionString = "你的資料庫連接字串";
            string query = "SELECT * FROM Customers";  // 這是查詢資料的 SQL，根據你的資料庫設計調整

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvCustomers.DataSource = dt;  // 將資料表設為 GridView 的資料源
                gvCustomers.DataBind();  // 重新綁定資料
            }
        }






    }
}