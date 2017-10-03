using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http.Routing;
using TodoApp.Api.Controllers;
using TodoApp.Contracts.Helpers;

[assembly: InternalsVisibleTo("TodoApp.Api.Tests")]
namespace TodoApp.Api.Helpers
{
    internal class UriHelper : IUriHelper
    {
        private readonly UrlHelper _urlHelper;

        public UriHelper(HttpRequestMessage httpRequestMessage)
        {
            _urlHelper = new UrlHelper(httpRequestMessage);
        }

        public Uri BuildUriForPostTodo(Guid id) =>
            new Uri(_urlHelper.Route(TodosController.POST_TODO_ROUTE, new {id}), UriKind.Relative);
    }
}