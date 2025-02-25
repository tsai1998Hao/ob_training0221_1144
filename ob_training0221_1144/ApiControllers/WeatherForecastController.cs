using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;  // 確保加入這行，才能使用 Web API 相關


namespace ob_training0221_1144.ApiControllers
{
    public class WeatherForecastController : ApiController
    {
        [HttpGet]  // 明確標示這是 GET 方法
        public IHttpActionResult Get()
        {
            var result = new
            {
                name = "John Doe"
            };
            return Ok(result);  // 返回 JSON 格式的數據
        }
    }
}
