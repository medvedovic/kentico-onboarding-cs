using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KenticoOnboardingCs.Api.Models.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private List<Todo> todos;
        private int nextId = 3;
        public TodoRepository()
        {
            todos = new List<Todo>()
            {
                new Todo() { Id = 1, Name = "Make coffee", Done = true },
                new Todo() { Id = 2, Name = "Master ASP.NET web api" }
            };
        }

        public IEnumerable<Todo> GetAll()
        {
            return todos;
        }

        public Todo Get(int id)
        {
            return todos
                .Find(todo => todo.Id == id);
        }

        public Todo Add(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException("item");
            }

            todo.Id = nextId++;
            todos.Add(todo);

            return todo;
        }


        public bool Remove(int id)
        {
            var todoToRemove = todos.Find(todo => todo.Id == id);

            if(todoToRemove == null)
            {
                return false;
            }

            todos.Remove(todoToRemove);

            return true;
        }

        public bool Update(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = todos.FindIndex(p => p.Id == todo.Id);
            if (index == -1)
            {
                return false;
            }
            todos.RemoveAt(index);
            todos.Add(todo);
            return true;
        }
    }
}