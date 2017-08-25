using NUnit.Framework;
using TodoApp.Api.Models;
using TodoApp.Api.Controllers;
using System.Web.Http.Results;
using TodoApp.Api.Models.Repositories;

namespace KenticoOnboardingCs.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Get
    {
        private TodosController dummyController;
        private ITodoRepository repository;

        [SetUp]
        public void SetUp()
        {
            repository = new TodoRepository();
            dummyController = new TodosController(repository);
        }

        [Test]
        public void GetDummyItem_ReturnsItemWithSameId()
        {
            var todoId = 2;
            var expectedItem = repository.Get(todoId);

            var response = dummyController.GetTodo(todoId);

            Assert.IsInstanceOf<OkNegotiatedContentResult<Todo>>(response);
            Assert.AreEqual(expectedItem, (response as OkNegotiatedContentResult<Todo>).Content);
        }

        [Test]
        public void GetDummyItem_ReturnsNotFound()
        {
            var todoId = 5;
            var expectedItem = repository.Get(todoId);

            var response = dummyController.GetTodo(todoId);

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        public void GetDummyItems_ReturnsAllTodos()
        {
            var response = dummyController.GetAllTodos();

            Assert.AreEqual(repository.GetAll(), response);
        }
    }
}
