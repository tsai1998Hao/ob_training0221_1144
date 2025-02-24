using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Security.Policy;
using System.Net;


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
            // string connectionString = "Server=localhost;Initial Catalog=TestProject_DB;User ID=sa;Password=test0713;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";

            //SQL 語法
            string query = "SELECT * FROM customers";

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
            System.Diagnostics.Debug.WriteLine(id);
            string name = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("txtName")).Text;

            string phone = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("txtPhone")).Text;
            string address = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("txtAddress")).Text;
            string user_id = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("txtUser_id")).Text;


            //資料庫連線設定
            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            //string connectionString = "Server=localhost;Initial Catalog=TestProject_DB;User ID=sa;Password=test0713;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            string query = "UPDATE Customers SET Name = @Name, Phone = @Phone, Address= @Address, User_id = @User_id WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@User_id", user_id);
                cmd.Parameters.AddWithValue("@Id", id);  // 確保 @Id 被正確綁定

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvCustomers.EditIndex = -1;  // 退出編輯模式
            LoadCustomers();  // 重新加載資料
        }


        protected void gvCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);

            //資料庫連線設定
            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            //string connectionString = "Server=localhost;Initial Catalog=TestProject_DB;User ID=sa;Password=test0713;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            string query = "DELETE FROM Customers WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);  // 確保 @Id 被正確綁定

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
            //資料庫連線設定
            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            //string connectionString = "Server=localhost;Initial Catalog=TestProject_DB;User ID=sa;Password=test0713;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
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

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)  // 修改: 改為 GridViewCommandEventArgs
        {
            if (e.CommandName == "Insert")  // 檢查 CommandName 是否為 Insert
            {
                // 取得新增的資料
                string name = ((TextBox)gvCustomers.FooterRow.FindControl("txtInsertName")).Text;
                string phone = ((TextBox)gvCustomers.FooterRow.FindControl("txtInsertPhone")).Text;
                string address = ((TextBox)gvCustomers.FooterRow.FindControl("txtInsertAddress")).Text;
                string userId = ((TextBox)gvCustomers.FooterRow.FindControl("txtInsertUserId")).Text;

                // SQL 插入語句
                string query = "INSERT INTO Customers (Name, Phone, Address, User_id) VALUES (@Name, @Phone, @Address, @UserId)";

                //資料庫連線設定
                string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
                //string connectionString = "Server=localhost;Initial Catalog=TestProject_DB;User ID=sa;Password=test0713;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";



                // 使用 SqlConnection 和 SqlCommand 執行插入操作
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }

                // 更新 GridView，顯示新增的資料
                LoadCustomers();  // 確保重新載入資料並顯示最新的資料  // 確保 GridView 顯示最新的資料
            }
        }





    }

}