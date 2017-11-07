using System.Net.Http;
using System.Web;
using Microsoft.Practices.Unity;
using TodoApp.Api.Wrappers;
using TodoApp.Contracts.Bootstrap;
using TodoApp.Contracts.Wrappers;

namespace TodoApp.Api
{
    public class ApiDependencyBootstrapper: IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) => 
            container
                .RegisterType<IDatabaseConfig>(new HierarchicalLifetimeManager(), new InjectionFactory(DatabaseConfigConstructor.Create))
                .RegisterType<HttpRequestMessage>(new HierarchicalLifetimeManager(), new InjectionFactory(GetHttpRequestMessage))
                .RegisterType<ITodoLocationHelper, TodoLocationHelper>(new HierarchicalLifetimeManager());

        private static HttpRequestMessage GetHttpRequestMessage(IUnityContainer container) =>
            (HttpRequestMessage) HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}