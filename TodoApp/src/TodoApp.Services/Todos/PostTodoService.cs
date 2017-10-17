using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Dtos;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Services.Todos
{
    class PostTodoService: IPostTodoService
    {
        private readonly ITodoRepository _repository;

        public PostTodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Todo> CreateTodoAsync(TodoDto todoDto)
        {
            var newTodo = new Todo
            {
                Id = Guid.NewGuid(),
                Value = todoDto.Value,
                CreatedAt = DateTime.Now
            };        

            return await _repository.CreateAsync(newTodo);
        }
    }
}
