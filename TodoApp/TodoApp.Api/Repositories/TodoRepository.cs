using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Api.Models.Repositories
{
    public class TodoRepository : ITodoRepository, IAsyncTodoRepository
    {
        public List<Todo> Todos { get; }
        private int nextId = 3;
        public TodoRepository()
        {
            Todos = new List<Todo>()
            {
                new Todo() { Id = 1, Value = "Make coffee" },
                new Todo() { Id = 2, Value = "Master ASP.NET web api" }
            };
        }

        public IEnumerable<Todo> GetAll()
        {
            return Todos;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
            => await Task.FromResult(Todos);
        

        public Todo Get(int id)
        {
            return Todos
                .Find(todo => todo.Id == id);
        }

        public async Task<Todo> GetAsync(int id)
            => await Task.FromResult(Todos.SingleOrDefault(todo => todo.Id == id));

        public Todo Add(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException("todo");
            }

            todo.Id = nextId++;
            Todos.Add(todo);

            return todo;
        }

        public async Task<Todo> AddAsync(Todo todo)
        {
            return await Task.Run(() => 
            {
                if (todo == null)
                {
                    throw new ArgumentNullException("Todo");
                }

                todo.Id = nextId++;
                Todos.Add(todo);
            })
            .ContinueWith((prevResult) =>
            {
                return todo;
            });
        }

        public bool Remove(int id)
        {
            var todoToRemove = Todos.Find(todo => todo.Id == id);

            if(todoToRemove == null)
            {
                return false;
            }

            Todos.Remove(todoToRemove);

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await Task.Run(() =>
            {
                var todoToRemove = Todos.Find(todo => todo.Id == id);

                if (todoToRemove == null)
                {
                    return false;
                }

                Todos.Remove(todoToRemove);

                return true;
            });
        }

        public bool Update(int id, Todo todo)
        {
            if (todo == null)
                throw new ArgumentNullException("todo");            

            int index = Todos.FindIndex(p => p.Id == id);
            if (index == -1)
                return false;

            Todos.RemoveAt(index);
            Todos.Add(todo);

            return true;
        }

        public async Task<bool> UpdateAsync(int id, Todo todo)
        {
            return await Task.Run(() =>
            {
                if (todo == null)
                    throw new ArgumentNullException("todo");

                int index = Todos.FindIndex(p => p.Id == id);
                if (index == -1)
                    return false;

                Todos.RemoveAt(index);
                Todos.Add(todo);

                return true;
            });
        }
    }
}