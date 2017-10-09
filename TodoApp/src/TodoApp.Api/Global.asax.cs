using System.Web.Http;

namespace TodoApp.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(RouteConfig.Register);
            GlobalConfiguration.Configure(UnityConfig.Register);
            GlobalConfiguration.Configure(FormattingConfig.Register);
        }
    }
}
