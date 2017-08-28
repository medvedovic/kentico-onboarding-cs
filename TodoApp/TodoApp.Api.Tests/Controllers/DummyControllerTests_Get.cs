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
        private TodosV2Controller asyncController;
        private ITodoRepository repository;
        private IAsyncTodoRepository asyncRepository;

        [SetUp]
        public void SetUp()
        {
            repository = new TodoRepository();
            asyncRepository = new TodoRepository();
            dummyController = new TodosController(repository);
            asyncController = new TodosV2Controller(asyncRepository);
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

        [Test]
        public void GetAllAsync_ReturnsAllTodos()
        {
            var expectedItem = asyncRepository.GetAllAsync();

            var response = asyncController.GetAllTodosAsync();

            Assert.AreEqual(expectedItem.Result, response.Result);
        }
    }
}
