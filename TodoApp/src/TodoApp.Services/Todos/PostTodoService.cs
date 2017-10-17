using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Dtos;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Services.Todos
{
    class PostTodoService: IPostTodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IServiceHelper _helper;

        public PostTodoService(ITodoRepository repository, IServiceHelper helper)
        {
            _repository = repository;
            _helper = helper;
        }

        public async Task<Todo> CreateTodoAsync(TodoDto todoDto)
        {
            var newTodo = new Todo
            {
                Id = _helper.GenerateGuid(),
                Value = todoDto.Value,
                CreatedAt = _helper.GetCurrentDateTime()
            };        

            return await _repository.CreateAsync(newTodo);
        }
    }
}
