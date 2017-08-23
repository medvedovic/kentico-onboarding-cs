using System.Collections.Generic;
using NUnit.Framework;
using KenticoOnboardingCs.Api.Models;
using KenticoOnboardingCs.Api.Controllers;
using System.Web.Http.Results;
using KenticoOnboardingCs.Api.Models.Repositories;
using System.ComponentModel.DataAnnotations;

namespace KenticoOnboardingCs.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Post
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
            Assert.AreEqual("DefaultApi", (response as CreatedAtRouteNegotiatedContentResult<Todo>).RouteName);
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
    }
}
