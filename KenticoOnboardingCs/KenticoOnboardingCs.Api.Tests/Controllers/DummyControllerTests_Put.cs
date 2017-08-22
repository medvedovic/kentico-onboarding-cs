using KenticoOnboardingCs.Api.Controllers;
using KenticoOnboardingCs.Api.Models;
using KenticoOnboardingCs.Api.Models.Repositories;
using NUnit.Framework;
using System.Web.Http.Results;

namespace KenticoOnboardingCs.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Put
    {
        private DummyController dummyController;
        private ITodoRepository repository;

        [SetUp]
        public void SetUp()
        {
            repository = new TodoRepository();
            dummyController = new DummyController(repository);
        }

        [Test]
        public void PutDummyItem_ReturnsOk_WithValidId_WithValidModel()
        {
            var newTodo = new Todo() { Name = "Make coffee asap" };

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
    }
}
