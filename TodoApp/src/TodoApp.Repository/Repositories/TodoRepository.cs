using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Repository.Repositories
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly IMongoCollection<Todo> _todosCollection;

        public TodoRepository(DependencyBootstrapperConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var db = client.GetDatabase("onboarding_todoapp");
            _todosCollection = db.GetCollection<Todo>("todos");
        }

        public async Task<IEnumerable<Todo>> RetrieveAllAsync()
            => await _todosCollection.AsQueryable().ToListAsync();

        public Task<Todo> RetrieveAsync(Guid id)
            => _todosCollection.Find(doc => doc.Id == id).SingleOrDefaultAsync();

        public async Task<Todo> CreateAsync(Todo todo)
        {
            await _todosCollection.InsertOneAsync(todo);
            
            return todo;
        }

        public async Task RemoveAsync(Guid id)
            => await _todosCollection.DeleteOneAsync(todo => todo.Id == id);

        public async Task<Todo> UpdateAsync(Todo todo)
            => await _todosCollection.FindOneAndReplaceAsync(doc => doc.Id == todo.Id, todo);      
    }
}