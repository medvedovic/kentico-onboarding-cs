using System;
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

        public Todo ExistingTodo { get; set; }

        public PutTodoService(IServiceHelper helper, ITodoRepository repository)
        {
            _helper = helper;
            _repository = repository;
        }

        public async Task<Todo> UpdateTodoAsync(Guid id, IConvertibleTo<Todo> todoViewModel)
        {
            var todo = todoViewModel.Convert();

            ExistingTodo.Value = todo.Value;
            ExistingTodo.UpdatedAt = _helper.GetCurrentDateTime();

            return await _repository.UpdateAsync(ExistingTodo);
        }
    }
}
