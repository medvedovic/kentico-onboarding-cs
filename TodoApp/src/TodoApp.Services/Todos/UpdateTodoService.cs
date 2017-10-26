using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Services.Todos
{
    class UpdateTodoService: IUpdateTodoService
    {
        private readonly IServiceHelper _helper;
        private readonly ITodoRepository _repository;
        private readonly IRetrieveTodoService _retrieveTodoService;

        public UpdateTodoService(IServiceHelper helper, ITodoRepository repository, IRetrieveTodoService retrieveTodoService)
        {
            _helper = helper;
            _repository = repository;
            _retrieveTodoService = retrieveTodoService;
        }

        public async Task<Todo> UpdateTodoAsync(IConvertibleTo<Todo> todoViewModel)
        {
            var todo = todoViewModel.Convert();

            _retrieveTodoService.CachedTodo.Value = todo.Value;
            _retrieveTodoService.CachedTodo.UpdatedAt = _helper.GetCurrentDateTime();

            return await _repository.UpdateAsync(_retrieveTodoService.CachedTodo);
        }
    }
}
