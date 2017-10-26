using System;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Services.Todos;

namespace TodoApp.Api.Tests.Services.Todos
{
    class GetTodoServiceTests
    {
        private IRetrieveTodoService _retrieveTodoService;
        private ITodoRepository _mockRepository;
        private static readonly Guid _guid = Guid.Parse("bba2acfe-9f08-4a07-b72f-1540156a857a");
        private static readonly Todo _retrieveTodoResult = new Todo
        {
            Id = _guid,
            Value = "Go home",
            CreatedAt = new DateTime(2017, 10, 20),
            UpdatedAt = new DateTime(2017, 10, 20)
        };

        [SetUp]
        public void Init()
        {
            _mockRepository = Substitute.For<ITodoRepository>();
            _retrieveTodoService = new RetrieveTodoService(_mockRepository);
        }

        [Test]
        public void IsTodoInDb_ReturnsTrue_OnTodoFound()
        {
            _mockRepository.RetrieveAsync(_guid).Returns(_retrieveTodoResult);

            var isTodoInDbResult = _retrieveTodoService.IsTodoInDbAsync(_guid)
                .Result;

            Assert.That(isTodoInDbResult, Is.True);
        }

        [Test]
        public void IsTodoInDb_ReturnsFalse_OnTodoNotFound()
        {
            _mockRepository.RetrieveAsync(_guid).ReturnsNull();

            var isTodoInDbResult = _retrieveTodoService.IsTodoInDbAsync(_guid)
                .Result;

            Assert.That(isTodoInDbResult, Is.False);
        }

        [Test]
        public void RetrieveTodoAsync_ReturnsCachedResult_OnTodoInCache()
        {
            _mockRepository.RetrieveAsync(_guid).Returns(_retrieveTodoResult);
            _retrieveTodoService.IsTodoInDbAsync(_guid);

            var actualResult = _retrieveTodoService.RetrieveTodoAsync(_guid).Result;

            Assert.That(actualResult, Is.EqualTo(_retrieveTodoResult));
        }

        [Test]
        public void RetrieveTodoAsync_CallsRepository_OnTodoNotInCache()
        {
            var guid = new Guid("e88b0743-e4e4-41ec-8319-a13c529016bd");
            var expectedResult = new Todo
            {
                Id = guid,
                Value = "Go home ASAP",
                CreatedAt = new DateTime(2017, 10, 14),
                UpdatedAt = new DateTime(2017, 10, 14)
            };
            _mockRepository.RetrieveAsync(_guid).Returns(_retrieveTodoResult);
            _mockRepository.RetrieveAsync(guid).Returns(expectedResult);
            _retrieveTodoService.IsTodoInDbAsync(_guid);


            var actualResult = _retrieveTodoService.RetrieveTodoAsync(guid).Result;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
