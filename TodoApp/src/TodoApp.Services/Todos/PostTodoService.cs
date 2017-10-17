using System.Threading.Tasks;
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

        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            todo.Id = _helper.GenerateGuid();
            todo.CreatedAt = _helper.GetCurrentDateTime();

            return await _repository.CreateAsync(todo);
        }
    }
}
