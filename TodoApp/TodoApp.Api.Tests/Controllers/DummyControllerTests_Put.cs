using System;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using NUnit.Framework;
using System.Web.Http.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Put
    {
        #region Synchronous calls

        [Test]
        public void PutDummyItem_ReturnsOk_WithValidId_WithValidModel()
        {
            var todo = new Todo() { Value = "Make coffee asap" };
            var mockRepository = Substitute.For<ITodoRepository>();
            mockRepository.Update(1, todo).Returns(true);
            var controller = new TodosController(mockRepository);


            var response = controller.PutTodo(1, todo);


            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<Todo>>());
            Assert.That(todo, Is.EqualTo(((OkNegotiatedContentResult<Todo>) response).Content));
        }

        [Test]
        public void PutDummyItem_ReturnsBadRequest_WithValidId_WithInvalidModel()
        {
            var mockRepo = Substitute.For<ITodoRepository>();
            var controller = new TodosController(mockRepo);

            
            controller.ModelState.AddModelError("test", "test");
            var response = controller.PutTodo(1, new Todo());


            Assert.That(response, Is.InstanceOf<InvalidModelStateResult>());
        }

        [Test]
        public void PutDummyItem_ReturnsNotFound_WithInvalidId()
        {
            var mockRepo = Substitute.For<ITodoRepository>();
            mockRepo.Update(50, new Todo()).Returns(false);
            var controller = new TodosController(mockRepo);

        
            var response = controller.PutTodo(50, new Todo());


            Assert.That(response, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void PutDummyItem_ReturnsBadRequest_WithTodoIsNull()
        {
            var mockRepo = Substitute.For<ITodoRepository>();
            mockRepo.Update(1, null).Throws(new ArgumentNullException());
            var controller = new TodosController(mockRepo);


            var response = controller.PutTodo(1, null);


            Assert.That(response, Is.InstanceOf<BadRequestErrorMessageResult>());
        }

        #endregion

        #region Asynchronous calls

        [Test]
        public void PutAsyncDummyItem_ReturnsOk_WithValidId_WithValidModel()
        {
            var todo = new Todo() { Value = "Make coffee asap" };
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            mockRepo.UpdateAsync(1, todo).Returns(true);
            var controller = new TodosV2Controller(mockRepo);


            var response = controller.PutTodoAsync(1, todo);


            Assert.That(response.Result, Is.InstanceOf<OkNegotiatedContentResult<Todo>>());
            Assert.That(todo, Is.EqualTo(((OkNegotiatedContentResult<Todo>) response.Result).Content));
        }

        [Test]
        public void PutAsyncDummyItem_ReturnsBadRequest_WithValidId_WithInvalidModel()
        {
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            var controller = new TodosV2Controller(mockRepo);


            controller.ModelState.AddModelError("test", "test");
            var response = controller.PutTodoAsync(1, new Todo());


            Assert.That(response.Result, Is.InstanceOf<InvalidModelStateResult>());
        }

        [Test]
        public void PutAsyncDummyItem_ReturnsNotFound_WithInvalidId()
        {
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            mockRepo.UpdateAsync(50, new Todo()).Returns(false);
            var controller = new TodosV2Controller(mockRepo);


            var response = controller.PutTodoAsync(50, new Todo());


            Assert.That(response.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void PutAsyncDummyItem_ReturnsBadRequest_WithTodoIsNull()
        {
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            mockRepo.UpdateAsync(1, null).Throws(new ArgumentNullException());
            var controller = new TodosV2Controller(mockRepo);


            var response = controller.PutTodoAsync(1, null);


            Assert.That(response.Result, Is.InstanceOf<BadRequestErrorMessageResult>());
        }

        #endregion
    }
}
