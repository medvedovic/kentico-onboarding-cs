using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Routing;

namespace KenticoOnboardingCs.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof(ApiVersionRouteConstraint)
                }
            };

            config.MapHttpAttributeRoutes(constraintResolver);
            config.AddApiVersioning();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
