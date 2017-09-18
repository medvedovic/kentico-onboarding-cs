using System;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using TodoApp.Api.Controllers;
using TodoApp.Api.Helpers;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class TodosControllerTests
    {
        #region SetUp

        private TodosController _controller;
        private ITodoRepository _mockRepo;
        private IUriHelper _uriHelper;
        private Todo _mockTodo;

        [SetUp]
        public void Init()
        {
            _mockRepo = Substitute.For<ITodoRepository>();

            var httpRequestMessage = ConfigureRequestMessage();

            _uriHelper = Substitute.For<IUriHelper>();
            _uriHelper.BuildUri(httpRequestMessage, new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"))
                .Returns(new Uri("http://localhost/api/v1/todos/56d9ed92-91ad-4171-9be9-11356384ce37"));

            _controller = new TodosController(_mockRepo, _uriHelper)
            {
                Request = httpRequestMessage
            };

            _mockTodo = new Todo()
            {
                Id = new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"),
                Value = "Make more coffee"
            };
        }

        #endregion

        #region Get

        [Test]
        public void GetAllTodos_ReturnsAllItems()
        {
            _mockRepo.GetAllAsync().Returns(new List<Todo>()
            {
                new Todo() {Id = new Guid("2e2253c5-4bdb-45d8-8cbf-1a24e9b04d1c"), Value = "Make coffee"},
                new Todo() {Id = new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), Value = "Make more coffee"}
            });

            var response = _controller.GetAllTodos();

            CollectionAssert.AreEqual(response.Result, _mockRepo.GetAllAsync().Result);
        }

        [Test]
        public void GetTodo_ReturnsTodoWithSameId()
        {
            _mockRepo.GetAsync(Guid.Parse("56d9ed92-91ad-4171-9be9-11356384ce37")).Returns(_mockTodo);

            var responseResult = _controller.GetTodo(Guid.Parse("56d9ed92-91ad-4171-9be9-11356384ce37")).Result;

            Assert.That(responseResult, Is.InstanceOf<OkNegotiatedContentResult<Todo>>());
            Assert.That(((OkNegotiatedContentResult<Todo>) responseResult).Content, Is.EqualTo(_mockTodo));
        }

        [Test]
        public void GetTodo_ReturnsNotFound()
        {
            _mockRepo.GetAsync(Guid.Parse("00000000-0000-0000-0000-000000000000")).ReturnsNull();

            var result = _controller.GetTodo(Guid.Parse("00000000-0000-0000-0000-000000000000"));

            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        #endregion

        #region Post

        [Test]
        public void PostTodo_ValidatesModelCorrectly()
        {
            var todo = new Todo();
            var context = new ValidationContext(todo);
            var results = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(todo, context, results);

            Assert.That(results.Count, Is.Not.Zero);
            Assert.That(isModelStateValid, Is.False);
        }

        [Test]
        public void PostTodo_ReturnsNewTodo_WithValidModel()
        {
            var todo = new Todo()
            {
                Value = "Make more coffee"
            };
            _mockRepo.AddAsync(todo).Returns(_mockTodo);

            var responseResult = _controller.PostTodo(todo).Result;

            Assert.That(responseResult, Is.InstanceOf<CreatedNegotiatedContentResult<Todo>>());
            Assert.That(((CreatedNegotiatedContentResult<Todo>) responseResult).Location.ToString(), Is.EqualTo("http://localhost/api/v1/todos/56d9ed92-91ad-4171-9be9-11356384ce37"));
        }

        [Test]
        public void PostTodo_ReturnsBadRequest_WithInvalidModel()
        {
            var todo = new Todo();

            _controller.ModelState.AddModelError("test", "test");
            var responseResult = _controller.PostTodo(todo).Result;
            
            Assert.That(responseResult, Is.InstanceOf<InvalidModelStateResult>());
        }

        #endregion

        #region Delete

        [Test]
        public void DeleteTodo_ReturnsOk_OnValidId()
        {
            _mockRepo.RemoveAsync(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"))
                .Returns(true);

            var responseResult = _controller.DeleteTodo(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37")).Result;
            
            Assert.That(((StatusCodeResult) responseResult).StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public void DeleteTodo_ReturnsNotFound_OnInvalidInput()
        {
            _mockRepo.RemoveAsync(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"))
                .Returns(false);

            var responseResult = _controller.DeleteTodo(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37")).Result;

            Assert.That(responseResult, Is.InstanceOf<NotFoundResult>());
        }

        #endregion

        #region Put

        [Test]
        public void PutTodo_ReturnsNewTodo_OnValidId_OnValidTodo()
        {
            _mockRepo.UpdateAsync(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), _mockTodo)
                .Returns(true);

            var responseResult =
                _controller.PutTodo(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), _mockTodo)
                .Result;

            Assert.That(responseResult, Is.InstanceOf<OkNegotiatedContentResult<Todo>>());
            Assert.That(((OkNegotiatedContentResult<Todo>)responseResult).Content, Is.EqualTo(_mockTodo));
        }

        [Test]
        public void PutTodo_ReturnsInvalidModel_OnIdNotEqualTodoId()
        {
            _mockRepo.UpdateAsync(Guid.Empty, _mockTodo)
                .Throws(new ArgumentException(nameof(Todo)));

            var responseResult =
                _controller.PutTodo(Guid.Empty, _mockTodo)
                .Result;

            Assert.That(responseResult, Is.InstanceOf<InvalidModelStateResult>());
        }

        [Test]
        public void PutTodo_ReturnsNotFound_OnWrongId()
        {
            _mockRepo.Update(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), _mockTodo).Returns(false);

            var responseResult = _controller
                .PutTodo(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), _mockTodo)
                .Result;

            Assert.That(responseResult, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void PutTodo_ReturnsInvalidModel_OnTodoIsNull()
        {
            _mockRepo.UpdateAsync(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), null)
                .Throws(new ArgumentNullException(nameof(Todo)));

            var responseResult = _controller
                .PutTodo(new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), null)
                .Result;

            Assert.That(responseResult, Is.InstanceOf<InvalidModelStateResult>());
        }
        
        #endregion

        #region Helper methods

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

        #endregion
    }
}
