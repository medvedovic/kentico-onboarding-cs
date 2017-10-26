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
    class PutTodoServiceTests
    {
        private IUpdateTodoService _updateTodoService;
        private IServiceHelper _mockServiceHelper;
        private ITodoRepository _mockTodoRepository;
        private IRetrieveTodoService _mockRetrieveTodoService;

        [SetUp]
        public void Init()
        {
            _mockTodoRepository = Substitute.For<ITodoRepository>();
            _mockServiceHelper = Substitute.For<IServiceHelper>();
            _mockRetrieveTodoService = Substitute.For<IRetrieveTodoService>();
            _updateTodoService = new UpdateTodoService(_mockServiceHelper, _mockTodoRepository, _mockRetrieveTodoService);
        }

        [Test]
        public void UpdateTodoAsync_ReturnsCorrectTodo()
        {
            var guid = new Guid("128539cb-a41e-42a1-805b-1eb533e86461");
            var todoViewModel = new TodoViewModel {Value = "Test UpdateTodoService"};
            var returnedTodo = new Todo
            {
                Id = guid,
                Value = "Test stuff",
                CreatedAt = new DateTime(2017, 10, 17, 10, 00, 00)
            };
            var expectedResult = new Todo
            {
                Id = guid,
                Value = "Test UpdateTodoService",
                CreatedAt = new DateTime(2017, 10, 17, 10, 00, 00),
                UpdatedAt = new DateTime(2017, 10, 21, 10, 44, 12)
            };
            _mockRetrieveTodoService.CachedTodo.Returns(returnedTodo);
            _mockServiceHelper.GetCurrentDateTime().Returns(new DateTime(2017, 10, 21, 10, 44, 12));

            _mockTodoRepository.UpdateAsync(Arg.Any<Todo>()).Returns(parameters => parameters.Arg<Todo>());

            var result = _updateTodoService.UpdateTodoAsync(todoViewModel).Result;

            Assert.That(result, Is.EqualTo(expectedResult).Using(new TodosEqualityComparer()));
        }
    }
}
