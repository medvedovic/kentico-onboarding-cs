using System.Collections.Generic;
using NUnit.Framework;
using TodoApp.Api.Models;
using TodoApp.Api.Controllers;
using System.Web.Http.Results;
using System.ComponentModel.DataAnnotations;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Post
    {
        private TodosController dummyController;
        private TodosV2Controller asyncController;
        private ITodoRepository repository;
        private IAsyncTodoRepository asyncRepository;

        [SetUp]
        public void SetUp()
        {
            repository = new TodoRepository();
            dummyController = new TodosController(repository);
            asyncController = new TodosV2Controller(asyncRepository);
        }

        [Test]
        public void PostDummyItem_ValidatesModelCorrectly()
        {
            // Arrange
            var newTodo = new Todo();
            var context = new ValidationContext(newTodo);
            var results = new List<ValidationResult>();

            //act
            var isModelStateValid = Validator.TryValidateObject(newTodo, context, results);

            //assert
            Assert.NotZero(results.Count);
            Assert.IsFalse(isModelStateValid);
        }

        [Test]
        public void PostDummyItem_ReturnsNewTodo_WithValidModel()
        {
            var itemToPost = new Todo() { Value = "Go home" };

            dummyController.ModelState.Clear();
            var response = dummyController.PostTodo(itemToPost);
            
            Assert.IsInstanceOf<CreatedAtRouteNegotiatedContentResult<Todo>>(response);
            Assert.AreEqual("Todos", (response as CreatedAtRouteNegotiatedContentResult<Todo>).RouteName);
            Assert.AreEqual(3, (response as CreatedAtRouteNegotiatedContentResult<Todo>).RouteValues["id"]);
        }

        [Test]
        public void PostDummyItem_ReturnsError_WithInvalidModel()
        {
            var itemToPost = new Todo();

            dummyController.ModelState.AddModelError("test", "test");
            var response = dummyController.PostTodo(itemToPost);

            Assert.IsInstanceOf<InvalidModelStateResult>(response);
        }

        [Test]
        public void PostAsyncDummyItem_ReturnsNewTodo_WithValidModel()
        {
            var itemToPost = new Todo() { Value = "Go home" };

            var response = asyncController.PostTodoAsync(itemToPost);

            Assert.IsInstanceOf<CreatedAtRouteNegotiatedContentResult<Todo>>(response.Result);
            Assert.AreEqual(3, (response.Result as CreatedAtRouteNegotiatedContentResult<Todo>).RouteValues["id"]);
        }

        [Test]
        public void PostAsyncDummyItem_ReturnsError_WithInvalidModel()
        {
            var itemToPost = new Todo();

            asyncController.ModelState.AddModelError("test", "test");
            var response = asyncController.PostTodoAsync(itemToPost);

            Assert.IsInstanceOf<InvalidModelStateResult>(response.Result);
        }
    }
}
