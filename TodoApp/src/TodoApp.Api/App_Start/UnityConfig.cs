using System.Web.DynamicData;
using System.Web.Http;
using Microsoft.Practices.Unity;
using TodoApp.Api.Repositories;
using TodoApp.Contracts;
using TodoApp.Repository;

namespace TodoApp.Api
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer()
                .Register<RepositoryDependencyBootstrapper>()
                .Register<ApiDependencyBootstrapper>();

            config.DependencyResolver = new UnityResolver(container);
        }

        public static IUnityContainer Register<TAssemblyBootstrapper>(this IUnityContainer container) 
            where TAssemblyBootstrapper : IUnityBootstrapper, new() => 
                new TAssemblyBootstrapper().RegisterType(container);
    }
}