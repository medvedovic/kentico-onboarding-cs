using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Web.Http.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TodoApp.Api.Helpers;
using TodoApp.Api.Repositories;
using TodoApp.Contracts.Repositories;
using TodoApp.DL.Repositories;

namespace TodoApp.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<ITodoRepository, TodoRepository>("TodoRepositoryContract", new ContainerControlledLifetimeManager());
            container.RegisterType<IUriHelper, UriHelper>("UriHelperContract", new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

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
