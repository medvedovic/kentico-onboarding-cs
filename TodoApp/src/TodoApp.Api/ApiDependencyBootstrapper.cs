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
        public IUnityContainer RegisterType(IUnityContainer container, DependencyBootstrapperConfig configuration = null) => 
            container
                .RegisterType<HttpRequestMessage>(new HierarchicalLifetimeManager(), new InjectionFactory(GetHttpRequestMessage))
                .RegisterType<IUriHelper, UriHelper>(new HierarchicalLifetimeManager());

        private static HttpRequestMessage GetHttpRequestMessage(IUnityContainer container) =>
            (HttpRequestMessage) HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}