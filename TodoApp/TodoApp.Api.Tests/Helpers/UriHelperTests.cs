using System;
using System.Net.Http;
using System.Web;
using NUnit.Framework;
using TodoApp.Api.Helpers;

namespace TodoApp.Api.Tests.Helpers
{
    class UriHelperTests
    {
        private IUriHelper _uriHelper;

        [SetUp]
        public void Init()
        {
            _uriHelper = new UriHelper();
        }

        [Test]
        public void UriHelper_ReturnsCorrectUrl()
        {
            var expectedResult = new Uri("http://example.com/api/v1/todos/56d9ed92-91ad-4171-9be9-11356384ce37");
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://example.com/api/v1/todos/");
            
            var location = _uriHelper.BuildUri(httpRequest, new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"));

            Assert.That(location, Is.EqualTo(expectedResult));
        }
    }
}
