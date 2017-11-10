using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoApp.Api.Controllers;
using TodoApp.Contracts.Wrappers;

namespace TodoApp.Api.Wrappers
{
    internal class TodoLocationHelper : ITodoLocationHelper
    {
        private readonly UrlHelper _urlHelper;

        public TodoLocationHelper(HttpRequestMessage httpRequestMessage)
        {
            _urlHelper = new UrlHelper(httpRequestMessage);
        }

        public Uri BuildRouteUri(Guid id) =>
            new Uri(_urlHelper.Route(TodosController.DEFAULT_ROUTE, new {id}), UriKind.Relative);
    }
}