using System.Web.Http;
using Microsoft.Practices.Unity;
using TodoApp.Api.Helpers;
using TodoApp.Api.Repositories;
using TodoApp.Contracts.Repositories;
using TodoApp.DL.Repositories;

namespace TodoApp.Api
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<ITodoRepository, TodoRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUriHelper, UriHelper>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}