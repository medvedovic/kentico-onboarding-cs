﻿using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoApp.Api.Controllers;
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

        public Uri BuildRouteUri(Guid id) =>
            new Uri(_urlHelper.Route(TodosController.DEFAULT_ROUTE, new {id}), UriKind.Relative);
    }
}