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
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITodoRepository _repository;

        public UpdateTodoService(ITodoRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Todo> UpdateTodoAsync(Todo existingTodo, IConvertibleTo<Todo> todoViewModel)
        {
            var todo = todoViewModel.Convert();

            existingTodo.Value = todo.Value;
            existingTodo.UpdatedAt = _dateTimeProvider.GetCurrentDateTime();

            return await _repository.UpdateAsync(existingTodo);
        }
    }
}
