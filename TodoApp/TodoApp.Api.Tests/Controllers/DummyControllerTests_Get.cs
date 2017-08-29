using System.Collections.Generic;
using NUnit.Framework;
using TodoApp.Api.Models;
using TodoApp.Api.Controllers;
using System.Web.Http.Results;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Get
    {
        [Test]
        public void GetDummyItem_ReturnsItemWithSameId()
        {
            var mockRepo = Substitute.For<ITodoRepository>();
            var expectedItem = new Todo() { Id = 2, Value = "Go home" };
            mockRepo.Get(2).Returns(expectedItem);


            var controller = new TodosController(mockRepo);
            var response = controller.GetTodo(2);


            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<Todo>>());
            Assert.That((response as OkNegotiatedContentResult<Todo>).Content, Is.EqualTo(expectedItem));
        }

        [Test]
        public void GetAsyncDummyItem_ReturnsItemWithSameId()
        {
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            var expectedItem = new Todo() { Id = 2, Value = "Go home" };
            mockRepo.GetAsync(2).Returns(expectedItem);


            var controller = new TodosV2Controller(mockRepo);
            var responseResult = controller.GetTodoAsync(2).Result;


            Assert.That(responseResult, Is.InstanceOf<OkNegotiatedContentResult<Todo>>());
            Assert.That((responseResult as OkNegotiatedContentResult<Todo>).Content, Is.EqualTo(expectedItem));
        }

        [Test]
        public void GetDummyItem_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<ITodoRepository>();
            mockRepo.Get(5).ReturnsNull();
            

            var controller = new TodosController(mockRepo);
            var response = controller.GetTodo(5);


            Assert.That(response, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void GetAsyncDummyItem_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            mockRepo.GetAsync(5).ReturnsNull();


            var controller = new TodosV2Controller(mockRepo);
            var response = controller.GetTodoAsync(5);


            Assert.That(response.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void GetDummyItems_ReturnsAllTodos()
        {
            var mockRepo = Substitute.For<ITodoRepository>();
            mockRepo.GetAll().Returns(new List<Todo>
            {
                new Todo {Id = 1, Value = "Make coffee"},
                new Todo {Id = 2, Value = "Go home"}
            });
            var controller = new TodosController(mockRepo);


            var response = controller.GetAllTodos();


            Assert.That(response, Is.EqualTo(mockRepo.GetAll()));
        }

        [Test]
        public void GetAllAsync_ReturnsAllTodos()
        {
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            mockRepo.GetAllAsync().Returns(new List<Todo>
            {
                new Todo {Id = 1, Value = "Make coffee"},
                new Todo {Id = 2, Value = "Go home"}
            });
            var controller = new TodosV2Controller(mockRepo);


            var response = controller.GetAllTodosAsync();


            Assert.That(response.Result, Is.EqualTo(mockRepo.GetAllAsync().Result));
        }
    }
}
