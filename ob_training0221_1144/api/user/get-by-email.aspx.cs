using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json;

public partial class GetUserByEmail : Page
{
    protected async void btnSearch_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();

        if (string.IsNullOrEmpty(email))
        {
            lblResult.Text = "Please enter an email address.";
            return;
        }

        // 調用 API 並顯示結果
        var result = await GetUserByEmailAsync(email);

        if (result != null)
        {
            lblResult.Text = $"User ID: {result.UserId}, Name: {result.Name}, Email: {result.Email}, Created At: {result.CreatedAt}";
        }
        else
        {
            lblResult.Text = "User not found.";
        }
    }

    // 發送 POST 請求至 API 並返回結果
    private async Task<UserResponse> GetUserByEmailAsync(string email)
    {
        string apiUrl = "https://yourapiurl.com/api/user/get-by-email"; // 替換為實際 API URL

        using (HttpClient client = new HttpClient())
        {
            var requestData = new
            {
                email = email
            };

            // 將物件序列化為 JSON
            string json = JsonConvert.SerializeObject(requestData);

            // 構建 POST 請求
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 發送 POST 請求並等待回應
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            // 如果回應成功
            if (response.IsSuccessStatusCode)
            {
                // 讀取回應的 JSON 並反序列化為物件
                string responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

                if (apiResponse.Status == "success")
                {
                    return apiResponse.Data;
                }
                else
                {
                    return null; // 若查無此用戶
                }
            }
            else
            {
                return null; // 請求失敗
            }
        }
    }

    // 定義回應的資料結構
    public class ApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public UserResponse Data { get; set; }
    }

    // 定義用戶資料的結構
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
    }
}
