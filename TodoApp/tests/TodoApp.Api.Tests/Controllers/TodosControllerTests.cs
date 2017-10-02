using System;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using NSubstitute.ReturnsExtensions;
using TodoApp.Api.Controllers;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class TodosControllerTests
    {
        private TodosController _controller;
        private ITodoRepository _mockRepo;
        private IUriHelper _uriHelper;
        private Todo _mockTodo;
        private List<Todo> _mockTodos;

        [SetUp]
        public void Init()
        {
            _mockRepo = Substitute.For<ITodoRepository>();

            _uriHelper = Substitute.For<IUriHelper>();
            _uriHelper.BuildUriForPostTodo(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"))
                .Returns(new Uri("http://localhost/api/v1/todos/56d9ed92-91ad-4171-9be9-11356384ce37"));

            _controller = new TodosController(_mockRepo, _uriHelper);

            _mockTodo = new Todo()
            {
                Id = new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"),
                Value = "Make more coffee"
            };

            _mockTodos = new List<Todo>
            {
                new Todo() {Id = new Guid("2e2253c5-4bdb-45d8-8cbf-1a24e9b04d1c"), Value = "Make coffee"},
                new Todo() {Id = new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), Value = "Make more coffee"}
            };
        }

        [Test]
        public void GetAllTodos_ReturnsAllItems()
        {
            _mockRepo.RetrieveAllAsync().Returns(_mockTodos);


            var response = _controller.GetAllTodos().Result;


            CollectionAssert.AreEqual(((OkNegotiatedContentResult<IEnumerable<Todo>>)response).Content, _mockTodos);
            Assert.That(response, Is.TypeOf<OkNegotiatedContentResult<IEnumerable<Todo>>>());
        }

        [Test]
        public void GetTodo_ReturnsOk()
        {
            _mockRepo.RetrieveAsync(Guid.Parse("56d9ed92-91ad-4171-9be9-11356384ce37")).Returns(_mockTodo);


            var responseResult = _controller.GetTodo(Guid.Parse("56d9ed92-91ad-4171-9be9-11356384ce37")).Result;


            Assert.That(responseResult, Is.TypeOf<OkNegotiatedContentResult<Todo>>());
        }

        [Test]
        public void PostTodo_ReturnsOk()
        {
            var todo = new Todo()
            {
                Value = "Make more coffee"
            };
            _mockRepo.CreateAsync(todo).Returns(_mockTodo);


            var responseResult = _controller.PostTodo(todo).Result;


            Assert.That(((CreatedNegotiatedContentResult<Todo>)responseResult).Location.AbsoluteUri, Is.EqualTo("http://localhost/api/v1/todos/56d9ed92-91ad-4171-9be9-11356384ce37"));
            Assert.That(responseResult, Is.TypeOf<CreatedNegotiatedContentResult<Todo>>());
        }

        [Test]
        public void DeleteTodo_ReturnsNoContent()
        {
            var responseResult = _controller
                .DeleteTodo(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"))
                .Result;
            

            Assert.That(((StatusCodeResult) responseResult).StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public void PutTodo_ReturnsOK()
        {
            var responseResult =
                _controller.PutTodo(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), _mockTodo)
                .Result;


            Assert.That(responseResult, Is.TypeOf<OkNegotiatedContentResult<Todo>>());
            Assert.That(((OkNegotiatedContentResult<Todo>)responseResult).Content, Is.EqualTo(_mockTodo));
        }
        
        private HttpRequestMessage ConfigureRequestMessage()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/v1/todos");

            var configuration = new HttpConfiguration();
            var route = configuration.Routes.MapHttpRoute(
                name: "DefautApi",
                routeTemplate: "api/v1/{controller}/{id}",
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
