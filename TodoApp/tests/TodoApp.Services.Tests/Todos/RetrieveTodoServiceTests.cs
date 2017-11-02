using System;
using System.Linq;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Services.Todos;

namespace TodoApp.Services.Tests.Todos
{
    internal class RetrieveTodoServiceTests
    {
        private IRetrieveTodoService _retrieveTodoService;
        private ITodoRepository _mockRepository;
        private Guid _id;
        private Todo _retrieveTodoResult;

        [SetUp]
        public void Init()
        {
            _mockRepository = Substitute.For<ITodoRepository>();
            _retrieveTodoService = new RetrieveTodoService(_mockRepository);
            _id = Guid.Parse("bba2acfe-9f08-4a07-b72f-1540156a857a");
            _retrieveTodoResult = new Todo
            {
                Id = _id,
                Value = "Go home",
                CreatedAt = new DateTime(2017, 10, 20),
                UpdatedAt = new DateTime(2017, 10, 20)
            };
        }

        [Test]
        public void IsTodoInDb_ReturnsTrue_OnTodoFound()
        {
            _mockRepository.RetrieveAsync(_id).Returns(_retrieveTodoResult);

            var isTodoInDbResult = _retrieveTodoService.IsTodoInDbAsync(_id)
                .Result;

            Assert.That(isTodoInDbResult, Is.True);
        }

        [Test]
        public void IsTodoInDb_ReturnsFalse_OnTodoNotFound()
        {
            _mockRepository.RetrieveAsync(_id).ReturnsNull();

            var isTodoInDbResult = _retrieveTodoService.IsTodoInDbAsync(_id)
                .Result;

            Assert.That(isTodoInDbResult, Is.False);
        }

        [Test]
        public void RetrieveTodoAsync_ReturnsCachedResult_OnTodoInCache()
        {
            _mockRepository.RetrieveAsync(_id).Returns(_retrieveTodoResult);
            _retrieveTodoService.IsTodoInDbAsync(_id);

            var actualResult = _retrieveTodoService.RetrieveTodoAsync(_id).Result;

            Assert.That(actualResult, Is.EqualTo(_retrieveTodoResult));
            Assert.That(_mockRepository.ReceivedCalls().Count(), Is.Not.GreaterThan(1));
        }

        [Test]
        public void RetrieveTodoAsync_CallsRepository_OnTodoNotInCache()
        {
            var id = new Guid("e88b0743-e4e4-41ec-8319-a13c529016bd");
            var expectedResult = new Todo
            {
                Id = id,
                Value = "Go home ASAP",
                CreatedAt = new DateTime(2017, 10, 14),
                UpdatedAt = new DateTime(2017, 10, 14)
            };
            _mockRepository.RetrieveAsync(_id).Returns(_retrieveTodoResult);
            _mockRepository.RetrieveAsync(id).Returns(expectedResult);
            _retrieveTodoService.IsTodoInDbAsync(_id);


            var actualResult = _retrieveTodoService.RetrieveTodoAsync(id).Result;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
