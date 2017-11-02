using System.Net.Http;
using System.Web;
using Microsoft.Practices.Unity;
using TodoApp.Api.Helpers;
using TodoApp.Contracts.Bootstrap;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Api
{
    public class ApiDependencyBootstrapper: IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) => 
            container
                .RegisterType<IDatabaseConfig>(new HierarchicalLifetimeManager(), new InjectionFactory(DatabaseConfigGenerator.Generate))
                .RegisterType<HttpRequestMessage>(new HierarchicalLifetimeManager(), new InjectionFactory(GetHttpRequestMessage))
                .RegisterType<IUriHelper, UriHelper>(new HierarchicalLifetimeManager());

        private static HttpRequestMessage GetHttpRequestMessage(IUnityContainer container) =>
            (HttpRequestMessage) HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}