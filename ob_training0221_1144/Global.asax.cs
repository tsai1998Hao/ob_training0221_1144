using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http; // 加入這一行來使用 Web API

namespace ob_training0221_1144
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {

            // 註冊 Web API 路由
            GlobalConfiguration.Configure(WebApiConfig.Register);


            // 應用程式啟動時執行的程式碼
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}