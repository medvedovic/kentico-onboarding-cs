using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Repository.Repositories
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly IMongoCollection<Todo> _todosCollection;

        public TodoRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("onboarding_todoapp");
            _todosCollection = db.GetCollection<Todo>("todos");
        }

        public async Task<IEnumerable<Todo>> RetrieveAllAsync()
            => await _todosCollection.AsQueryable().ToListAsync();

        public Task<Todo> RetrieveAsync(Guid id)
            => _todosCollection.Find(doc => doc.Id == id).SingleAsync();

        public async Task<Todo> CreateAsync(Todo todo)
        {
            await _todosCollection.InsertOneAsync(todo);
            
            return todo;
        }

        public Task<bool> RemoveAsync(Guid id)
            => Task.FromResult(true);

        public Task<Todo> UpdateAsync(Todo todo)
            => Task.FromResult(todo);
    }
}