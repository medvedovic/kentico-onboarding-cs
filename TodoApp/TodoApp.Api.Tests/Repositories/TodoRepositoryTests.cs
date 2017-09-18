using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests.Repositories
{
    [TestFixture]
    class TodoRepositoryTests
    {
        private ITodoRepository _repo;
        private List<Todo> _mockList;

        [SetUp]
        public void Init()
        {
            _mockList = MockListData();
            _repo = new TodoRepository(_mockList);
        }

        #region Get
        
        [Test]
        public void GetAll()
        {
            var todos = _repo.GetAll();
            var asyncTodos = _repo.GetAllAsync().Result;

            Assert.That(() => todos.SequenceEqual(MockListData()));
            Assert.That(() => asyncTodos.SequenceEqual(MockListData()));
        }

        [Test]
        public void GetTodo()
        {
            var guid = new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38");
            var expectedResult = MockListData()[0];

            var todo = _repo.Get(guid);
            var asyncTodo = _repo.GetAsync(guid).Result;

            Assert.That(todo, Is.EqualTo(expectedResult));
            Assert.That(asyncTodo, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetTodo_ReturnsNull()
        {
            var todo = _repo.Get(Guid.Empty);
            var asyncTodo = _repo.GetAsync(Guid.Empty).Result;

            Assert.That(todo, Is.Null);
            Assert.That(asyncTodo, Is.Null);
        }

        #endregion

        #region Add

        [Test]
        public void Add_ReturnsNewTodo()
        {
            var todo = new Todo()
            {
                Value = "Go home"
            };


            var newTodo = _repo.Add(todo);


            Assert.That(newTodo, Is.EqualTo(todo));
            Assert.That(newTodo.Id, Is.Not.Null);
            Assert.That(_repo.GetAll().Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task AddAsync_ReturnsNewTodo()
        {
            var todo = new Todo()
            {
                Value = "Go home"
            };
            

            var newAsyncTodo = await _repo.AddAsync(todo);


            Assert.That(_repo.GetAll().Count(), Is.EqualTo(3));
            Assert.That(newAsyncTodo, Is.EqualTo(todo));
            Assert.That(newAsyncTodo.Id, Is.Not.Null);
        }

        [Test]
        public void Add_ThrowsError_OnNull()
        {
            Assert.Throws<ArgumentNullException>(() => _repo.Add(null));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repo.AddAsync(null));
        }

        #endregion

        #region Remove

        [Test]
        public void RemoveTodo_ReturnsTrue()
        {
            var result = _repo.Remove(new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"));
            var asyncResult = _repo.RemoveAsync(new Guid("466083f9-14f9-4286-ae56-b95a2ccdc7d3"));

            Assert.That(result, Is.True);
            Assert.That(asyncResult.Result, Is.True);
        }

        [Test]
        public void RemoveTodo_ReturnsFalse()
        {
            var result = _repo.Remove(Guid.Empty);
            var asyncResult = _repo.RemoveAsync(Guid.Empty);

            Assert.That(result, Is.False);
            Assert.That(asyncResult.Result, Is.False);
        }

        #endregion

        #region Update

        [Test]
        public void UpdateTodo_ReturnsTrue()
        {
            var updated = new Todo()
            {
                Id = new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"),
                Value = "Make more coffee"
            };

            var result = _repo.Update(new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"), updated);

            Assert.That(result, Is.True);
        }

        [Test]
        public void UpdateTodo_ReturnsFalse()
        {
            var updated = new Todo()
            {
                Id = Guid.Empty,
                Value = "Make more coffee"
            };

            var result = _repo.Update(Guid.Empty, updated);

            Assert.That(result, Is.False);
        }

        [Test]
        public void UpdateTodo_ThrowsException_OnIdsNotCorresponding()
        {
            var updated = new Todo()
            {
                Id = new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"),
                Value = "Make more coffee"
            };

            Assert.Throws<ArgumentException>(() =>
                _repo.Update(new Guid("466083f9-14f9-4286-ae56-b95a2ccdc7d3"), updated));
        }

        [Test]
        public void UpdateTodo_ThrowsException_OnTodoIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _repo.Update(Guid.Empty, null));
        }

        [Test]
        public void UpdateAsyncTodo_ReturnsTrue()
        {
            var updated = new Todo()
            {
                Id = new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"),
                Value = "Make more coffee"
            };

            var result = _repo.UpdateAsync(new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"), updated);

            Assert.That(result.Result, Is.True);
        }

        [Test]
        public void UpdateAsyncTodo_ReturnsFalse()
        {
            var updated = new Todo()
            {
                Id = Guid.Empty,
                Value = "Make more coffee"
            };

            var result = _repo.UpdateAsync(Guid.Empty, updated);

            Assert.That(result.Result, Is.False);
        }

        [Test]
        public void UpdateAsyncTodo_ThrowsException_OnIdsNotCorresponding()
        {
            var updated = new Todo()
            {
                Id = new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"),
                Value = "Make more coffee"
            };

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _repo.UpdateAsync(new Guid("466083f9-14f9-4286-ae56-b95a2ccdc7d3"), updated));
        }

        [Test]
        public void UpdateAsyncTodo_ThrowsException_OnTodoIsNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repo.UpdateAsync(Guid.Empty, null));
        }

        #endregion

        #region HelperMethods

        private static List<Todo> MockListData()
            => new List<Todo>()
            {
                new Todo() {Id = new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"), Value = "Make coffee"},
                new Todo() {Id = new Guid("466083f9-14f9-4286-ae56-b95a2ccdc7d3"), Value = "Master ASP.NET web api"}
            };

        #endregion
    }
}
