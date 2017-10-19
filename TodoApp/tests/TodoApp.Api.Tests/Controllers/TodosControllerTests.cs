﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.Controllers;
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

            _controller = new TodosController(_mockRepo, _mockPostService, _uriHelper);

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

            var response = _controller.GetAllTodosAsync().Result;

            CollectionAssert.AreEqual(((OkNegotiatedContentResult<IEnumerable<Todo>>)response).Content, _mockTodos);
            Assert.That(response, Is.TypeOf<OkNegotiatedContentResult<IEnumerable<Todo>>>());
        }

        [Test]
        public void GetTodo_ReturnsOk()
        {
            _mockRepo.RetrieveAsync(_guid).Returns(_mockTodo);

            var responseResult = _controller.GetTodoAsync(_guid).Result;

            Assert.That(responseResult, Is.TypeOf<OkNegotiatedContentResult<Todo>>());
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

            var responseResult = _controller.PostTodoAsync(todo).Result;

            Assert.That(((CreatedNegotiatedContentResult<Todo>)responseResult).Location, Is.EqualTo(expectedUriResult));
            Assert.That(responseResult, Is.TypeOf<CreatedNegotiatedContentResult<Todo>>());
        }

        [Test]
        public void PostTodo_ReturnsBadRequest_OnInvalidModelState()
        {
            var todo = new TodoViewModel();
            _controller.ModelState.AddModelError("test", "test");

            var responseResult = _controller.PostTodoAsync(todo).Result;

            Assert.That(responseResult, Is.TypeOf<InvalidModelStateResult>());
        }

        [Test]
        public void DeleteTodo_ReturnsNoContent()
        {
            var responseResult = _controller
                .DeleteTodoAsync(_guid)
                .Result;
            
            Assert.That(((StatusCodeResult) responseResult).StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public void PutTodo_ReturnsOK()
        {
            _mockRepo.UpdateAsync(_mockTodo).Returns(_mockTodo);

            var responseResult =
                _controller.PutTodoAsync(_guid, _mockTodo)
                .Result;

            Assert.That(responseResult, Is.TypeOf<OkNegotiatedContentResult<Todo>>());
            Assert.That(((OkNegotiatedContentResult<Todo>)responseResult).Content, Is.EqualTo(_mockTodo));
        }
    }
}
