using System.Web.Http;
using Newtonsoft.Json;

namespace ob_training0221_1144
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 註冊 Web API 路由
            config.MapHttpAttributeRoutes();

            // 這是基本的 Web API 路由設置
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // **強制 Web API 只回傳 JSON**
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            // **刪除 XML 格式支援**
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
