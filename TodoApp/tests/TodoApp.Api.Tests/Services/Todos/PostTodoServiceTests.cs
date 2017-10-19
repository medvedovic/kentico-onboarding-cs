using System;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.Tests.Helpers;
using TodoApp.Api.ViewModels;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Services.Todos;

namespace TodoApp.Api.Tests.Services.Todos
{
    [TestFixture]
    class PostTodoServiceTests
    {
        private ITodoRepository _repository;
        private IServiceHelper _helper;
        private IPostTodoService _service;

        [SetUp]
        public void Init()
        {
            _repository = Substitute.For<ITodoRepository>();
            _helper = Substitute.For<IServiceHelper>();
            _service = new PostTodoService(_repository, _helper);
        }

        [Test]
        public void BuildsModelCorrectly()
        {
            var expectedResult = new Todo
            {
                Id = Guid.Parse("f6deb310-7052-4a3f-b9cb-2827767803c7"),
                Value = "Do stuff",
                CreatedAt = new DateTime(2017, 10, 17, 12, 00, 00)
            };
            _repository.CreateAsync(Arg.Any<Todo>()).Returns(parameters => parameters.Arg<Todo>());
            _helper.GenerateGuid().Returns(Guid.Parse("f6deb310-7052-4a3f-b9cb-2827767803c7"));
            _helper.GetCurrentDateTime().Returns(new DateTime(2017, 10, 17, 12, 00, 00));
            var dto = new TodoViewModel
            {
                Value = "Do stuff"
            };

            var result = _service.CreateTodoAsync(dto).Result;

            Assert.That(result, Is.EqualTo(expectedResult).Using(new TodosEqualityComparer()));
        }
    }
}
