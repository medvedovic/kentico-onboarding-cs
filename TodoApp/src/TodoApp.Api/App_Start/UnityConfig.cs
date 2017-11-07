using System.Web.Http;
using TodoApp.Contracts.Bootstrap;
using TodoApp.Repository;
using TodoApp.Services;
using Unity;

namespace TodoApp.Api
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer()
                .Register<RepositoryDependencyBootstrapper>()
                .Register<ApiDependencyBootstrapper>()
                .Register<ServicesDependencyBootstrapper>();

            config.DependencyResolver = new UnityResolver(container);
        }

        public static IUnityContainer Register<TAssemblyBootstrapper>(this IUnityContainer container) 
            where TAssemblyBootstrapper : IUnityBootstrapper, new() => 
                new TAssemblyBootstrapper().RegisterType(container);
    }
}