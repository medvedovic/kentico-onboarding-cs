﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Api.Tests.Helpers;
using TodoApp.Api.ViewModels;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class TodosControllerTests
    {
        private TodosController _controller;
        private ITodoRepository _mockRepo;
        private IUriHelper _uriHelper;
        private IPostTodoService _mockPostService;
        private Todo _mockTodo;
        private List<Todo> _mockTodos;
        private readonly Guid _guid = new Guid("38f61793-bf01-48ae-8e00-ccee139adba2");

        [SetUp]
        public void Init()
        {
            _mockRepo = Substitute.For<ITodoRepository>();

            _uriHelper = Substitute.For<IUriHelper>();
            _uriHelper.BuildRouteUri(Arg.Any<Guid>())
                .Returns(parameters => new Uri($"/localhost/todos/{parameters.Arg<Guid>()}", UriKind.Relative));

            _mockPostService = Substitute.For<IPostTodoService>();

            _controller = new TodosController(_mockRepo, _mockPostService, _uriHelper)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };

            _mockTodo = new Todo
            {
                Id = _guid,
                Value = "Make more coffee"
            };

            _mockTodos = new List<Todo>
            {
                new Todo {Id = new Guid("2e2253c5-4bdb-45d8-8cbf-1a24e9b04d1c"), Value = "Make coffee"},
                new Todo {Id = _guid, Value = "Make more coffee"}
            };
        }

        [Test]
        public void GetAllTodos_ReturnsAllItems()
        {
            _mockRepo.RetrieveAllAsync().Returns(_mockTodos);

            var response = _controller.GetAllTodosAsync().Result
                .ExecuteAsync(CancellationToken.None).Result;
            response.TryGetContentValue(out List<Todo> actualTodos);

            CollectionAssert.AreEqual(actualTodos, _mockTodos);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetTodo_ReturnsOk_OnValidId()
        {
            _mockRepo.RetrieveAsync(_guid).Returns(_mockTodo);

            var responseResult = _controller.GetTodoAsync(_guid).Result
                .ExecuteAsync(CancellationToken.None).Result;
            responseResult.TryGetContentValue(out Todo actualtodo);

            Assert.That(actualtodo, Is.EqualTo(_mockTodo).Using(TodosEqualityComparer()));
            Assert.That(responseResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetTodo_ReturnsNotFound_OnInvalidId()
        {
            _mockRepo.RetrieveAsync(Guid.Empty).Returns(_mockTodo);

            var responseResult = _controller.GetTodoAsync(_guid).Result
                .ExecuteAsync(CancellationToken.None).Result;
            
            Assert.That(responseResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void PostTodo_ReturnsOk_OnValidModelState()
        {
            var todo = new TodoViewModel
            {
                Value = "Make more coffee"
            };
            _mockPostService.CreateTodoAsync(Arg.Any<IConvertibleTo<Todo>>()).Returns(_mockTodo);
            var expectedUriResult = new Uri($"/localhost/todos/{_guid}", UriKind.Relative);

            var responseResult = _controller.PostTodoAsync(todo).Result
                .ExecuteAsync(CancellationToken.None).Result;
            responseResult.TryGetContentValue(out Todo actualTodo);

            Assert.That(actualTodo, Is.EqualTo(_mockTodo).Using(TodosEqualityComparer()));
            Assert.That(responseResult.Headers.Location, Is.EqualTo(expectedUriResult));
            Assert.That(responseResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void PostTodo_ReturnsBadRequest_OnInvalidModelState()
        {
            var todo = new TodoViewModel();
            _controller.ModelState.AddModelError("test", "test");

            var responseResult = _controller.PostTodoAsync(todo).Result
                .ExecuteAsync(CancellationToken.None).Result;

            Assert.That(responseResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void PostTodo_ReturnsBadRequest_OnNull()
        {
            var responseResult = _controller.PostTodoAsync(null).Result
                .ExecuteAsync(CancellationToken.None).Result;

            Assert.That(responseResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void DeleteTodo_ReturnsNoContent()
        {
            var responseResult = _controller.DeleteTodoAsync(_guid)
                .Result
                .ExecuteAsync(CancellationToken.None)
                .Result;
            
            Assert.That(responseResult.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public void PutTodo_ReturnsOK()
        {
            _mockRepo.UpdateAsync(_mockTodo).Returns(_mockTodo);

            var responseResult = _controller.PutTodoAsync(_guid, _mockTodo)
                .Result
                .ExecuteAsync(CancellationToken.None)
                .Result;

            Assert.That(responseResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private static TodosEqualityComparer TodosEqualityComparer() =>
            new TodosEqualityComparer();
    }
}
