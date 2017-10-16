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
            var configuration = new DependencyBootstrapperConfig
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
            };

            var container = new UnityContainer()
                .Register<RepositoryDependencyBootstrapper>(configuration)
                .Register<ApiDependencyBootstrapper>();

            config.DependencyResolver = new UnityResolver(container);
        }

        public static IUnityContainer Register<TAssemblyBootstrapper>(this IUnityContainer container, DependencyBootstrapperConfig configuration = null) 
            where TAssemblyBootstrapper : IUnityBootstrapper, new() => 
                new TAssemblyBootstrapper().RegisterType(container, configuration);
    }
}