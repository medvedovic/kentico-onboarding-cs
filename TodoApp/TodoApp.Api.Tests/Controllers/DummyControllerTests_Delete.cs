using TodoApp.Api.Controllers;
using NUnit.Framework;
using System.Net;
using System.Web.Http.Results;
using NSubstitute;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Delete
    {
        [Test]
        public void DeleteDummyItem_ReturnsNotFound_WithInvalidId()
        {
            var mockRepository = Substitute.For<ITodoRepository>();
            mockRepository.Remove(50).Returns(false);
            var controller = new TodosController(mockRepository);


            var response = controller.DeleteTodo(50);
  
            
            Assert.That(response, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void DeleteAsyncTodoItem_ReturnsNotFound_WithInvalidId()
        {
            var mockRepository = Substitute.For<IAsyncTodoRepository>();
            mockRepository.RemoveAsync(50).Returns(false);
            var controller = new TodosV2Controller(mockRepository);


            var response = controller.DeleteTodoAsync(50);


            Assert.That(response.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void DeleteDummyItem_ReturnsNoContent_WithValidId()
        {
            var mockRepository = Substitute.For<ITodoRepository>();
            mockRepository.Remove(1).Returns(true);
            var controller = new TodosController(mockRepository);


            var response = controller.DeleteTodo(1);


            Assert.That(((StatusCodeResult)response).StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public void DeleteAsyncTodoItem_ReturnsNoContent_WithValidId()
        {
            var mockRepository = Substitute.For<IAsyncTodoRepository>();
            mockRepository.RemoveAsync(1).Returns(true);
            var controller = new TodosV2Controller(mockRepository);


            var response = controller.DeleteTodoAsync(1);


            Assert.That(((StatusCodeResult)response.Result).StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }
    }
}
