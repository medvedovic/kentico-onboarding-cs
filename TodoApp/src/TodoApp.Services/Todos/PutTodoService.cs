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

        public PutTodoService(IServiceHelper helper, ITodoRepository repository)
        {
            _helper = helper;
            _repository = repository;
        }

        public async Task<Todo> UpdateTodoAsync(Guid id, IConvertibleTo<Todo> todoViewModel)
        {
            var existingTodo = await _repository.RetrieveAsync(id);
            
            if (existingTodo == null)
                throw new ArgumentNullException();

            var todo = todoViewModel.Convert();

            existingTodo.Value = todo.Value;
            existingTodo.UpdatedAt = _helper.GetCurrentDateTime();

            return await _repository.UpdateAsync(existingTodo);
        }
    }
}
