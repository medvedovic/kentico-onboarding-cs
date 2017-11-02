using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contracts.Bootstrap;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Repository.Repositories
{
    internal class TodoRepository : ITodoRepository
    {
        private const string MONGO_COLLECTION_NAME = "todos";
        private readonly IMongoCollection<Todo> _todosCollection;

        public TodoRepository(IDatabaseConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var databaseName = config.ConnectionString.Split('/').Last();
            var db = client.GetDatabase(databaseName);
            _todosCollection = db.GetCollection<Todo>(MONGO_COLLECTION_NAME);
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

        public async Task<bool> RemoveAsync(Guid id)
        {
            var result = await _todosCollection.DeleteOneAsync(todo => todo.Id == id);

            return result.IsAcknowledged;
        }

        public async Task<Todo> UpdateAsync(Todo todo)
            => await _todosCollection.FindOneAndReplaceAsync(doc => doc.Id == todo.Id, todo);      
    }
}