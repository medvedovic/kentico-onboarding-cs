﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<Todo> _todos;

        public TodoRepository()
        {
            _todos = new List<Todo>()
            {
                new Todo() { Id = new Guid("454ad8b4-d5c8-4117-81d6-6a8385cfdc38"), Value = "Make coffee" },
                new Todo() { Id = new Guid("466083f9-14f9-4286-ae56-b95a2ccdc7d3"), Value = "Master ASP.NET web api" }
            };
        }

        public TodoRepository(List<Todo> todos)
        {
            _todos = todos;
        }

        public IEnumerable<Todo> GetAll()
            => _todos;

        public async Task<IEnumerable<Todo>> GetAllAsync()
            => await Task.FromResult(_todos);      

        public Todo Get(Guid id)
            => _todos.Find(todo => todo.Id == id);

        public async Task<Todo> GetAsync(Guid id)
            => await Task.FromResult(_todos.FirstOrDefault(todo => todo.Id == id));

        public Todo Add(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            todo.Id = Guid.NewGuid();
            _todos.Add(todo);

            return todo;
        }

        public Task<Todo> AddAsync(Todo todo)
        {
            return Task.Run(() =>
            {
                if (todo == null)
                    throw new ArgumentNullException(nameof(todo));

                todo.Id = Guid.NewGuid();
                _todos.Add(todo);

                return todo;
            });
        }

        public bool Remove(Guid id)
        {
            var todoToRemove = _todos.Find(todo => todo.Id == id);

            return _todos.Remove(todoToRemove);
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var todoToRemove = _todos.Find(todo => todo.Id == id);

                return _todos.Remove(todoToRemove);
            });
        }

        public bool Update(Guid id, Todo todo)
        {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            if (todo.Id != id)
                throw new ArgumentException("Provided ids do not correspont");

            var index = _todos.FindIndex(p => p.Id == id);
            if (index == -1)
                return false;

            _todos.RemoveAt(index);
            _todos.Add(todo);

            return true;
        }

        public Task<bool> UpdateAsync(Guid id, Todo todo)
        {
            return Task.Run(() =>
            {
                if (todo == null)
                    throw new ArgumentNullException(nameof(todo));
                
                if(todo.Id != id)
                    throw new ArgumentException("Provided ids do not correspont");

                var index = _todos.FindIndex(p => p.Id == id);
                if (index == -1)
                    return false;

                _todos.RemoveAt(index);
                _todos.Add(todo);

                return true;
            });
        }
    }
}