using System.Web.Http;
using Microsoft.Practices.Unity;
using TodoApp.Api.Helpers;
using TodoApp.Api.Repositories;
using TodoApp.DL;

namespace TodoApp.Api
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            RepositoryDependencyResolver.RegisterType(container);
            container.RegisterType<IUriHelper, UriHelper>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}