using System.Web.Http;

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
        }
    }
}
