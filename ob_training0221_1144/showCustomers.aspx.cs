﻿using System;
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
using System.Reflection.Emit;


namespace ob_training0221_1144
{
    public partial class showCustomers : System.Web.UI.Page
    {








        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    int userId = Convert.ToInt32(Session["UserId"]);
                    LoadCustomers(userId); // 用 Session 取得的 userId 傳給 LoadCustomers
                }
                else
                {
                    Response.Write("<script>alert('請先登入！'); window.location='login.aspx';</script>");
                }
            }



        }


        private void LoadCustomers(int userId)
        {
            //資料庫連線設定
            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            // string connectionString = "Server=localhost;Initial Catalog=TestProject_DB;User ID=sa;Password=test0713;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";

            //SQL 語法
            string query = "SELECT * FROM Customers WHERE user_id = @UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);  // 綁定參數，避免 SQL 注入

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    gvCustomers.DataSource = dt;
                    gvCustomers.DataBind();
                }
            }
        }





        private void LoadCustomers2 (int userId, string searchKeyword)
        {
            //資料庫連線設定
            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            // string connectionString = "Server=localhost;Initial Catalog=TestProject_DB;User ID=sa;Password=test0713;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";

            //SQL 語法
            string query = "SELECT * FROM Customers WHERE (Name LIKE @SearchKeyword OR Phone LIKE @SearchKeyword OR Address LIKE @SearchKeyword) AND User_id = @userId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);  // 綁定參數，避免 SQL 注入
                    cmd.Parameters.AddWithValue("@SearchKeyword", "%" + searchKeyword + "%"); // 正確綁定參數

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    gvCustomers.DataSource = dt;
                    gvCustomers.DataBind();
                }
            }
        }














        int edit_id;

        protected void gvCustomers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCustomers.EditIndex = e.NewEditIndex;

            int id = Convert.ToInt32(gvCustomers.DataKeys[e.NewEditIndex].Value);
            edit_id = Convert.ToInt32(gvCustomers.DataKeys[e.NewEditIndex].Value);
            // 顯示抓到的 Id
            System.Diagnostics.Debug.WriteLine("我抓到了這一筆 Id: " + id);


            int userId = Convert.ToInt32(Session["UserId"]);
            string searchKeyword = txtSearch.Text.Trim(); // 取得搜尋框的值

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                LoadCustomers2(userId, searchKeyword); // 帶入搜尋關鍵字
            }
            else
            {
                LoadCustomers(userId); // 沒有輸入關鍵字時，載入所有資料
            }

        }





        protected void gvCustomers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);
            System.Diagnostics.Debug.WriteLine("我抓到了這一筆",id);
            //Label1.Text = "Id: " + id.ToString();
            string name = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("updateName")).Text;

            string phone = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("updatePhone")).Text;
            string address = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("updateAddress")).Text;
            //string user_id = ((TextBox)gvCustomers.Rows[e.RowIndex].FindControl("txtUser_id")).Text;


            //資料庫連線設定
            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            //string connectionString = "Server=localhost;Initial Catalog=TestProject_DB;User ID=sa;Password=test0713;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            string query = "UPDATE Customers SET Name = @Name, Phone = @Phone, Address= @Address WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                //cmd.Parameters.AddWithValue("@User_id", user_id);
                cmd.Parameters.AddWithValue("@Id", id);  // 確保 @Id 被正確綁定

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvCustomers.EditIndex = -1;  // 退出編輯模式
            if (Session["UserId"] != null) // 檢查是否有存 userId
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                LoadCustomers(userId);  // 傳入 userId，重新加載資料
            }
            else
            {
                Response.Write("<script>alert('請先登入！'); window.location='login.aspx';</script>");
            }
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

            if (Session["UserId"] != null) // 檢查是否有存 userId
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                LoadCustomers(userId);  // 傳入 userId，重新加載資料
            }
            else
            {
                Response.Write("<script>alert('請先登入！'); window.location='login.aspx';</script>");
            }
        }


        protected void gvCustomers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // 設定 GridView 的 EditIndex 為 -1，這樣就能退出編輯模式
            gvCustomers.EditIndex = -1;

            // 重新綁定資料（這樣就可以顯示更新後的資料）
            if (Session["UserId"] != null) // 檢查是否有存 userId
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                LoadCustomers(userId);  // 傳入 userId，重新加載資料
            }
            else
            {
                Response.Write("<script>alert('請先登入！'); window.location='login.aspx';</script>");
            }
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

                int userId = 0;  // 這樣在全程中都能訪問到 userId


                if (Session["UserId"] != null) // 檢查是否有存 userId
                {
                    userId = Convert.ToInt32(Session["UserId"]);
                }
                else
                {
                    Response.Write("<script>alert('請先登入！'); window.location='login.aspx';</script>");
                }



                //string userId = ((TextBox)gvCustomers.FooterRow.FindControl("txtInsertUserId")).Text;

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


                    LoadCustomers(userId);  // 傳入 userId，重新加載資料，確保重新載入資料並顯示最新的資料  // 確保 GridView 顯示最新的資料
                
            }
        }


        //查詢-同步
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //string searchKeyword = txtSearch.Text.Trim(); // 取得搜尋關鍵字
            //LoadCustomers(searchKeyword); // 根據搜尋關鍵字加載資料
                                          
        }

        //查詢-按鈕
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim(); // 取得搜尋關鍵字
            LoadCustomers_sch(searchKeyword); // 根據搜尋關鍵字加載資料

            //txtSearch.Text = string.Empty;// 清空搜尋框的文字

        }

        private void LoadCustomers_sch(string searchKeyword)
        {
            int userId = 0;  // 這樣在全程中都能訪問到 userId

            if (Session["UserId"] != null) // 檢查是否有存 userId
            {
                userId = Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                Response.Write("<script>alert('請先登入！'); window.location='login.aspx';</script>");
            }

            string connectionString = "Server=115.85.156.59;Initial Catalog=TestProject_DB;User ID=tpe003sql;Password=!gomypay#20250219;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
            string query = "SELECT * FROM Customers WHERE (Name LIKE @SearchKeyword OR Phone LIKE @SearchKeyword OR Address LIKE @SearchKeyword) AND User_id = @userId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchKeyword", "%" + searchKeyword + "%"); // 搜尋關鍵字包含在 Name、Phone 或 Address 欄位中
                    cmd.Parameters.AddWithValue("@userId", userId); // 加入 userId 參數

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    if (dt.Rows.Count == 0) // 如果搜尋結果為空
                    {
                        // 顯示沒有符合的結果訊息
                        string alertMessage = "沒有找到符合搜尋條件的資料！";
                        string queryToShow = "SELECT * FROM Customers WHERE User_id = " + userId;
                        alertMessage += "查無資料，請重新輸入";

                        Response.Write("<script>alert('" + alertMessage + "');</script>");
                        LoadCustomers(userId);

                    }
                    else
                    {
                        gvCustomers.DataSource = dt;
                        gvCustomers.DataBind();
                    }
                }
            }
        }









    }

}