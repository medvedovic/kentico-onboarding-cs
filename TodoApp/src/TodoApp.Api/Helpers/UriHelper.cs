using System;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.Practices.Unity;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Api.Helpers
{
    internal class UriHelper : IUriHelper, IUnityBootstrapper
    {
        public Uri BuildUri(HttpRequestMessage request, Guid id) =>
            new Uri(new UrlHelper(request).Route("PostTodo", new {id}), UriKind.Relative);

        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<IUriHelper, UriHelper>(new ContainerControlledLifetimeManager());
        }
    }
}