using System.Web.Http;
using Microsoft.Practices.Unity;
using TodoApp.Api.Repositories;
using TodoApp.Repository;

namespace TodoApp.Api
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            new RepositoryDependencyBootstrapper().RegisterType(container);
            new ApiDependencyBootstrapper().RegisterType(container);
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}