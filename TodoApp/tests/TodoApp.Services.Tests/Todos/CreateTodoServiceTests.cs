using System;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.ViewModels;
using TodoApp.Contract.Base.EqualityComparer;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Services.Todos;

namespace TodoApp.Services.Tests.Todos
{
    [TestFixture]
    internal class CreateTodoServiceTests
    {
        private ITodoRepository _repository;
        private IGuidGenerator _mockGuidGenerator;
        private IDateTimeProvider _mockDateTimeProvider;
        private ICreateTodoService _service;

        [SetUp]
        public void Init()
        {
            _repository = Substitute.For<ITodoRepository>();
            _mockDateTimeProvider = Substitute.For<IDateTimeProvider>();
            _mockGuidGenerator = Substitute.For<IGuidGenerator>();
            _service = new CreateTodoService(_repository, _mockDateTimeProvider, _mockGuidGenerator);
        }

        [Test]
        public void BuildsModelCorrectly()
        {
            var id = Guid.Parse("f6deb310-7052-4a3f-b9cb-2827767803c7");
            var expectedResult = new Todo
            {
                Id = id,
                Value = "Do stuff",
                CreatedAt = new DateTime(2017, 10, 17, 12, 00, 00)
            };
            _repository.CreateAsync(Arg.Any<Todo>()).Returns(parameters => parameters.Arg<Todo>());
            _mockGuidGenerator.GenerateGuid().Returns(id);
            _mockDateTimeProvider.GetCurrentDateTime().Returns(new DateTime(2017, 10, 17, 12, 00, 00));
            var todo = new TodoViewModel
            {
                Value = "Do stuff"
            };

            var result = _service.CreateTodoAsync(todo).Result;

            Assert.That(result, Is.EqualTo(expectedResult).UsingTodosEqualityComparer());
        }
    }
}
