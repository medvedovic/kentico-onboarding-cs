using System.Collections.Generic;
using NUnit.Framework;
using KenticoOnboardingCs.Api.Models;
using KenticoOnboardingCs.Api.Controllers;
using Newtonsoft.Json;
using System.Web.Http.Results;
using System.Net;

namespace KenticoOnboardingCs.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests
    {
        private DummyController dummyController;
        private List<Todo> todos = new List<Todo>()
        {
            new Todo() { Id = 1, Name = "Make coffee", Done = true },
            new Todo() { Id = 2, Name = "Master ASP.NET web api", Done = false }
        };

        [SetUp]
        public void SetUp()
        {
            dummyController = new DummyController(todos);
        }

        [Test]
        public void GetDummyItem_ReturnsItemWithSameId()
        {
            var todoId = 2;
            var expectedItem = todos.Find(todo => todo.Id == todoId);

            var response = dummyController.GetTodo(todoId);

            Assert.IsInstanceOf<OkNegotiatedContentResult<Todo>>(response);
            Assert.AreEqual(expectedItem, (response as OkNegotiatedContentResult<Todo>).Content);
        }

        [Test]
        public void GetDummyItem_ReturnsNotFound()
        {
            var todoId = 5;
            var expectedItem = todos.Find(todo => todo.Id == todoId);

            var response = dummyController.GetTodo(todoId);

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        public void GetDummyItems_ReturnsAllProducts()
        {
            var response = dummyController.GetAllTodos();

            Assert.AreEqual(todos, response);
        }
    }
}
