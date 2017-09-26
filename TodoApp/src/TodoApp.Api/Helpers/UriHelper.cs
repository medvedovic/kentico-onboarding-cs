using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Api.Helpers
{
    internal class UriHelper : IUriHelper
    {
        private readonly UrlHelper _urlHelper;

        public UriHelper(HttpRequestMessage httpRequestMessage)
        {
            _urlHelper = new UrlHelper(httpRequestMessage);
        }

        public Uri BuildUri(Guid id, string atRoute) =>
            new Uri(_urlHelper.Route(atRoute, new {id}), UriKind.Relative);
    }
}