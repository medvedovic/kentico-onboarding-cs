using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KenticoOnboardingCs.Api.Models.Repositories;
using NUnit.Framework;
using KenticoOnboardingCs.Api.Models;

namespace KenticoOnboardingCs.Api.Tests.Models.Repositories
{
    [TestFixture]
    class TodoRepository_Tests
    {
        private IAsyncTodoRepository _repository;

        public TodoRepository_Tests()
        {
            _repository = new TodoRepository();
        }

        [Test]
        public void TodoRepository_GetAllAsync()
        {
            var fetchedItems = _repository.GetAllAsync().Result;

            Assert.AreEqual(fetchedItems, _repository.Todos);
        }

        [Test]
        public void TodoRepository_GetAsync()
        {
            var id = 1;
            var expectedResult = _repository.Todos.SingleOrDefault(todo => todo.Id == id);

            var fetchedItem = _repository.GetAsync(id).Result;

            Assert.AreEqual(fetchedItem, expectedResult);
        }

        [Test]
        public void TodoRepository_AddAsync()
        {
            var newTodo = new Todo() { Value = "Make stuff async" };

            var addResult = _repository.AddAsync(newTodo).Result;

            Assert.AreEqual(newTodo, addResult);
        }

        [Test]
        public async Task TodoRepository_AddAsync_ThrowsError()
        {
            var newTodo = new Todo() { Value = "Make stuff async" };

            try
            {
                await _repository.AddAsync(null);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void TodoRepository_RemoveAsync_ReturnsTrue_WithValidId()
        {
            var isRemoved = _repository.RemoveAsync(1);

            Assert.IsTrue(isRemoved.Result);
        }

        [Test]
        public void TodoRepository_RemoveAsync_ReturnsFalse_WithInvalidId()
        {
            var isRemoved = _repository.RemoveAsync(5);

            Assert.IsFalse(isRemoved.Result);
        }

        [Test]
        public void TodoRepository_UpdateAsync_ReturnsTrue_WithValidParams()
        {
            var updatedTodo = new Todo() { Value = "Go home" };

            var isUpdated = _repository.UpdateAsync(1, updatedTodo);

            Assert.IsTrue(isUpdated.Result);
        }

        [Test]
        public void TodoRepository_UpdateAsync_ReturnsFalse_WithInvalidId()
        {
            var updatedTodo = new Todo() { Value = "Go home" };

            var isUpdated = _repository.UpdateAsync(50, updatedTodo);

            Assert.IsFalse(isUpdated.Result);
        }

        [Test]
        public async Task TodoRepository_UpdateAsync_ReturnsFlase_WithInvalidTodo()
        {
            try
            {
                await _repository.UpdateAsync(1, null);
            }
            catch(Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }
    }
}
