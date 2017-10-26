using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Services.Todos
{
    class CreateTodoService: ICreateTodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IServiceHelper _helper;

        public CreateTodoService(ITodoRepository repository, IServiceHelper helper)
        {
            _repository = repository;
            _helper = helper;
        }

        public async Task<Todo> CreateTodoAsync(IConvertibleTo<Todo> todoViewModel)
        {
            var newTodo = todoViewModel.Convert();
            newTodo.Id = _helper.GenerateGuid();
            newTodo.CreatedAt = _helper.GetCurrentDateTime();

            return await _repository.CreateAsync(newTodo);
        }
    }
}
