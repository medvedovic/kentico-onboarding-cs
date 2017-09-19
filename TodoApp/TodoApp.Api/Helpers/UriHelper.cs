using System;
using System.Net.Http;
using Microsoft.Practices.Unity;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Api.Helpers
{
    internal class UriHelper : IUriHelper
    {
        public Uri BuildUri(HttpRequestMessage request, Guid id)
        {
            return new Uri(request.RequestUri + id.ToString());
        }

        public static void Register(UnityContainer container)
        {
            container.RegisterType<IUriHelper, UriHelper>(new ContainerControlledLifetimeManager());
        }
    }
}