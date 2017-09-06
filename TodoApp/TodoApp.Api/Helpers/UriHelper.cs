using System;
using System.Net.Http;

namespace TodoApp.Api.Helpers
{
    public class UriHelper : IUriHelper
    {
        public Uri BuildUri(HttpRequestMessage request, Guid id)
        {
            return new Uri(request.RequestUri + id.ToString());
        }
    }
}