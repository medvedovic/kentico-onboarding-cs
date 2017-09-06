using System;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http.Results;
using NSubstitute.ReturnsExtensions;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    class TodosControllerTests
    {
        #region SetUp

        private TodosController _controller;
        private ITodoRepository _mockRepo;

        [SetUp]
        public void Init()
        {
            _mockRepo = Substitute.For<ITodoRepository>();
            _controller = new TodosController(_mockRepo);
        }

        #endregion

        #region Get

        [Test]
        public void GetAllTodos_ReturnsAllItems()
        {
            _mockRepo.GetAllAsync().Returns(new List<Todo>()
            {
                new Todo() {Id = new Guid("2e2253c5-4bdb-45d8-8cbf-1a24e9b04d1c"), Value = "Make coffee"},
                new Todo() {Id = new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"), Value = "Make more coffee"}
            });

            var response = _controller.GetAllTodos();

            CollectionAssert.AreEqual(response.Result, _mockRepo.GetAllAsync().Result);
        }

        [Test]
        public void GetTodo_ReturnsTodoWithSameId()
        {
            var expectedResult = new Todo()
            {
                Id = new Guid("56d9ed92-91ad-4171-9be9-11356384ce37"),
                Value = "Make more coffee"
            };
            _mockRepo.GetAsync(Guid.Parse("56d9ed92-91ad-4171-9be9-11356384ce37")).Returns(expectedResult);

            var responseResult = _controller.GetTodo(Guid.Parse("56d9ed92-91ad-4171-9be9-11356384ce37")).Result;

            Assert.That(responseResult, Is.InstanceOf<OkNegotiatedContentResult<Todo>>());
            Assert.That(((OkNegotiatedContentResult<Todo>) responseResult).Content, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetTodo_ReturnsNotFound()
        {
            _mockRepo.GetAsync(Guid.Parse("00000000-0000-0000-0000-000000000000")).ReturnsNull();

            var result = _controller.GetTodo(Guid.Parse("00000000-0000-0000-0000-000000000000"));

            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        #endregion

    }
}
