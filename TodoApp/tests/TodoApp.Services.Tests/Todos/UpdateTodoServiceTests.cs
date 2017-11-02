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
    internal class UpdateTodoServiceTests
    {
        private IUpdateTodoService _updateTodoService;
        private IDateTimeProvider _mockDateTimeProvider;
        private ITodoRepository _mockTodoRepository;

        [SetUp]
        public void Init()
        {
            _mockTodoRepository = Substitute.For<ITodoRepository>();
            _mockDateTimeProvider = Substitute.For<IDateTimeProvider>();
            _updateTodoService = new UpdateTodoService(_mockTodoRepository, _mockDateTimeProvider);
        }

        [Test]
        public void UpdateTodoAsync_ReturnsCorrectTodo()
        {
            var id = new Guid("128539cb-a41e-42a1-805b-1eb533e86461");
            var todoViewModel = new TodoViewModel {Value = "Test UpdateTodoService"};
            var returnedTodo = new Todo
            {
                Id = id,
                Value = "Test stuff",
                CreatedAt = new DateTime(2017, 10, 17, 10, 00, 00)
            };
            var expectedResult = new Todo
            {
                Id = id,
                Value = "Test UpdateTodoService",
                CreatedAt = new DateTime(2017, 10, 17, 10, 00, 00),
                UpdatedAt = new DateTime(2017, 10, 21, 10, 44, 12)
            };
            _mockDateTimeProvider.GetCurrentDateTime().Returns(new DateTime(2017, 10, 21, 10, 44, 12));

            _mockTodoRepository.UpdateAsync(Arg.Any<Todo>()).Returns(parameters => parameters.Arg<Todo>());

            var result = _updateTodoService.UpdateTodoAsync(returnedTodo, todoViewModel).Result;

            Assert.That(result, Is.EqualTo(expectedResult).UsingTodosEqualityComparer());
        }
    }
}
