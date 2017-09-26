using System.Net.Http;
using System.Web;
using Microsoft.Practices.Unity;
using TodoApp.Api.Helpers;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Api
{
    public class ApiDependencyBootstrapper: IUnityBootstrapper
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<HttpRequestMessage>(
                new HierarchicalLifetimeManager(), 
                new InjectionFactory(GetHttpRequestMessage)
            );
            container.RegisterType<IUriHelper, UriHelper>(new ContainerControlledLifetimeManager());
        }

        private static HttpRequestMessage GetHttpRequestMessage(IUnityContainer container) =>
            (HttpRequestMessage) HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}