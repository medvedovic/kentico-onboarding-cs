using System;
using System.Collections.Generic;
using NUnit.Framework;
using TodoApp.Api.Models;
using TodoApp.Api.Controllers;
using System.Web.Http.Results;
using System.ComponentModel.DataAnnotations;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Post
    {
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
            Assert.That(results.Count, Is.Not.Zero);
            Assert.That(isModelStateValid, Is.False);
        }

        [Test]
        public void PostDummyItem_ReturnsNewTodo_WithValidModel()
        {
            var itemToAdd = new Todo() {Value = "Go home"};
            var mockRepo = Substitute.For<ITodoRepository>();
            mockRepo.Add(itemToAdd).Returns(new Todo() { Id = 3, Value = "Go home" });
            var controller = new TodosController(mockRepo);


            var response = controller.PostTodo(itemToAdd);
            

            Assert.IsInstanceOf<CreatedAtRouteNegotiatedContentResult<Todo>>(response);
            Assert.AreEqual("Todos", (response as CreatedAtRouteNegotiatedContentResult<Todo>).RouteName);
            Assert.AreEqual(3, (response as CreatedAtRouteNegotiatedContentResult<Todo>).RouteValues["id"]);
        }

        [Test]
        public void PostDummyItem_ReturnsError_WithInvalidModel()
        {
            var itemToPost = new Todo();
            var mockRepo = Substitute.For<ITodoRepository>();
            var controller = new TodosController(mockRepo);


            controller.ModelState.AddModelError("test", "test");
            var response = controller.PostTodo(itemToPost);


            Assert.That(response, Is.InstanceOf<InvalidModelStateResult>());
        }

        [Test]
        public void PostAsyncDummyItem_ReturnsNewTodo_WithValidModel()
        {
            var itemToPost = new Todo() { Value = "Go home" };
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            mockRepo.AddAsync(itemToPost).Returns(new Todo() {Id = 3, Value = "Go home"});
            var controller = new TodosV2Controller(mockRepo);

            var response = controller.PostTodoAsync(itemToPost);


            Assert.That(response.Result, Is.InstanceOf<CreatedAtRouteNegotiatedContentResult<Todo>>());
            Assert.That((response.Result as CreatedAtRouteNegotiatedContentResult<Todo>).RouteValues["id"], Is.EqualTo(3));
        }

        [Test]
        public void PostAsyncDummyItem_ReturnsError_WithInvalidModel()
        {
            var itemToPost = new Todo();
            var mockRepo = Substitute.For<IAsyncTodoRepository>();
            var controller = new TodosV2Controller(mockRepo);


            controller.ModelState.AddModelError("test", "test");
            var response = controller.PostTodoAsync(itemToPost);

            Assert.That(response.Result, Is.InstanceOf<InvalidModelStateResult>());
        }
    }
}
