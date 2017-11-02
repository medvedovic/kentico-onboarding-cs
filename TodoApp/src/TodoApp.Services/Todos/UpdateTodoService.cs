using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Services.Todos
{
    internal class UpdateTodoService: IUpdateTodoService
    {
        private readonly IServiceHelper _helper;
        private readonly ITodoRepository _repository;

        public UpdateTodoService(IServiceHelper helper, ITodoRepository repository)
        {
            _helper = helper;
            _repository = repository;
        }

        public async Task<Todo> UpdateTodoAsync(Todo existingTodo, IConvertibleTo<Todo> todoViewModel)
        {
            var todo = todoViewModel.Convert();

            existingTodo.Value = todo.Value;
            existingTodo.UpdatedAt = _helper.GetCurrentDateTime();

            return await _repository.UpdateAsync(existingTodo);
        }
    }
}
