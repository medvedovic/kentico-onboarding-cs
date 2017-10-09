using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Api.Helpers;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Api.Tests.Helpers
{
    [TestFixture]
    class UriHelperTests
    {
        private IUriHelper _uriHelper;
        private string routeTemplate = "api/todos";

        [SetUp]
        public void Init()
        {
            _uriHelper = new UriHelper(ConfigurePostRequestMessage());
        }

        [Test]
        public void BuidlsUriCorrectly()
        {
            var guid = new Guid("93c4a131-5d1d-4856-818b-b5d234731f1b");
            var expectedResult = $"/{routeTemplate}/{guid}";

            var uri = _uriHelper.BuildRouteUri(guid);


            Assert.That(uri.ToString(), Is.EqualTo(expectedResult));
        }

        private HttpRequestMessage ConfigurePostRequestMessage()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost/");

            var configuration = new HttpConfiguration();
            var route = configuration.Routes.MapHttpRoute(
                name: TodosController.DEFAULT_ROUTE,
                routeTemplate: $"{routeTemplate}/{{id}}",
                defaults: new { id = RouteParameter.Optional }
            );
            var routeData = new HttpRouteData(route,
                new HttpRouteValueDictionary
                {
                    { "controller", "todos" }
                }
            );

            httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);
            httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            return httpRequestMessage;
        }
    }
}
