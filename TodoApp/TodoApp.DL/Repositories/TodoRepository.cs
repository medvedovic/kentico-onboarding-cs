using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;

namespace TodoApp.DL.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<Todo> _todos;

        public TodoRepository()
        {
            _todos = new List<Todo>
            {
                new Todo() { Id = new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"), Value = "Make coffee" },
                new Todo() { Id = new Guid("466083f9-14f9-4286-ae56-b95a2ccdc7d3"), Value = "Master ASP.NET web api" }
            };
        }

        public async Task<IEnumerable<Todo>> GetAll()
            => await Task.FromResult(_todos);      

        public Task<Todo> Get(Guid id)
            => Task.FromResult(_todos[0]);

        public Task<Todo> Add(Todo todo)
            => Task.FromResult(todo);            

        public Task<bool> Remove(Guid id)
            => Task.FromResult(true);

        public Task<bool> Update(Guid id, Todo todo)
            => Task.FromResult(true);
    }
}