using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<Todo> todos;
        private int nextId = 3;
        public TodoRepository()
        {
            todos = new List<Todo>()
            {
                new Todo() { Id = 1, Value = "Make coffee" },
                new Todo() { Id = 2, Value = "Master ASP.NET web api" }
            };
        }

        public IEnumerable<Todo> GetAll()
            => todos;

        public async Task<IEnumerable<Todo>> GetAllAsync()
            => await Task.FromResult(todos);      

        public Todo Get(int id)
            => todos
                .Find(todo => todo.Id == id);

        public async Task<Todo> GetAsync(int id)
            => await Task.FromResult(todos.SingleOrDefault(todo => todo.Id == id));

        public Todo Add(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            todo.Id = nextId++;
            todos.Add(todo);

            return todo;
        }

        public async Task<Todo> AddAsync(Todo todo)
        {
            return await Task.Run(() => 
            {
                if (todo == null)
                {
                    throw new ArgumentNullException(nameof(todo));
                }

                todo.Id = nextId++;
                todos.Add(todo);
            })
            .ContinueWith((prevResult) =>
            {
                return todo;
            });
        }

        public bool Remove(int id)
        {
            var todoToRemove = todos.Find(todo => todo.Id == id);

            return todos.Remove(todoToRemove);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await Task.Run(() =>
            {
                var todoToRemove = todos.Find(todo => todo.Id == id);

                if (todoToRemove == null)
                {
                    return false;
                }

                todos.Remove(todoToRemove);

                return true;
            });
        }

        public bool Update(int id, Todo todo)
        {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));            

            int index = todos.FindIndex(p => p.Id == id);
            if (index == -1)
                return false;

            todos.RemoveAt(index);
            todos.Add(todo);

            return true;
        }

        public async Task<bool> UpdateAsync(int id, Todo todo)
        {
            return await Task.Run(() =>
            {
                if (todo == null)
                    throw new ArgumentNullException(nameof(todo));

                int index = todos.FindIndex(p => p.Id == id);
                if (index == -1)
                    return false;

                todos.RemoveAt(index);
                todos.Add(todo);

                return true;
            });
        }
    }
}