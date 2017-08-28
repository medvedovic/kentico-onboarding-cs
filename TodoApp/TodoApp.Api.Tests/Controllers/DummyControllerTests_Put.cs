using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using TodoApp.Api.Models.Repositories;
using NUnit.Framework;
using System.Web.Http.Results;

namespace KenticoOnboardingCs.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Put
    {
        private TodosController dummyController;
        private ITodoRepository repository;
        private TodosV2Controller asyncController;
        private IAsyncTodoRepository asyncRepository;

        [SetUp]
        public void SetUp()
        {
            repository = new TodoRepository();
            dummyController = new TodosController(repository);
            asyncRepository = new TodoRepository();
            asyncController = new TodosV2Controller(asyncRepository);
        }

        [Test]
        public void PutDummyItem_ReturnsOk_WithValidId_WithValidModel()
        {
            var newTodo = new Todo() { Value = "Make coffee asap" };

            var response = dummyController.PutTodo(1, newTodo);

            Assert.IsInstanceOf<OkNegotiatedContentResult<Todo>>(response);
            Assert.AreEqual(newTodo, (response as OkNegotiatedContentResult<Todo>).Content);
        }

        [Test]
        public void PutDummyItem_ReturnsBadRequest_WithValidId_WithInvalidModel()
        {
            dummyController.ModelState.AddModelError("test", "test");

            var response = dummyController.PutTodo(1, new Todo());

            Assert.IsInstanceOf<InvalidModelStateResult>(response);
        }

        [Test]
        public void PutDummyItem_ReturnsNotFound_WithInvalidId()
        {
            var response = dummyController.PutTodo(50, new Todo());

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        public void PutDummyItem_ReturnsBadRequest_WithTodoIsNull()
        {
            var response = dummyController.PutTodo(1, null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
        }

        [Test]
        public void PutAsyncDummyItem_ReturnsOk_WithValidId_WithValidModel()
        {
            var newTodo = new Todo() { Value = "Make coffee asap" };

            var response = asyncController.PutTodoAsync(1, newTodo);

            Assert.IsInstanceOf<OkNegotiatedContentResult<Todo>>(response.Result);
            Assert.AreEqual(newTodo, (response.Result as OkNegotiatedContentResult<Todo>).Content);
        }

        [Test]
        public void PutAsyncDummyItem_ReturnsBadRequest_WithValidId_WithInvalidModel()
        {
            asyncController.ModelState.AddModelError("test", "test");

            var response = asyncController.PutTodoAsync(1, new Todo());

            Assert.IsInstanceOf<InvalidModelStateResult>(response.Result);
        }

        [Test]
        public void PutAsyncDummyItem_ReturnsNotFound_WithInvalidId()
        {
            var response = dummyController.PutTodo(50, new Todo());

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        public void PutAsyncDummyItem_ReturnsBadRequest_WithTodoIsNull()
        {
            var response = asyncController.PutTodoAsync(1, null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response.Result);
        }
    }
}
