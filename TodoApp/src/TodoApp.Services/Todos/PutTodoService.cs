using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Services.Todos
{
    class PutTodoService: IPutTodoService
    {
        private readonly IServiceHelper _helper;
        private readonly ITodoRepository _repository;
        private readonly IGetTodoService _getTodoService;

        public PutTodoService(IServiceHelper helper, ITodoRepository repository, IGetTodoService getTodoService)
        {
            _helper = helper;
            _repository = repository;
            _getTodoService = getTodoService;
        }

        public async Task<Todo> UpdateTodoAsync(IConvertibleTo<Todo> todoViewModel)
        {
            var todo = todoViewModel.Convert();

            _getTodoService.CachedTodo.Value = todo.Value;
            _getTodoService.CachedTodo.UpdatedAt = _helper.GetCurrentDateTime();

            return await _repository.UpdateAsync(_getTodoService.CachedTodo);
        }
    }
}
