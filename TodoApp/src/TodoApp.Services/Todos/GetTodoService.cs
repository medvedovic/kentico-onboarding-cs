using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Services.Todos
{
    class GetTodoService: IGetTodoService
    {
        private readonly ITodoRepository _repository;
        public Todo CachedTodo { get; private set; }

        public GetTodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Todo> RetrieveTodoAsync(Guid id)
        {
            if (CachedTodo.Id != id)
            {
                CachedTodo = await _repository.RetrieveAsync(id);
            }

            return CachedTodo;
        }

        public async Task<bool> IsTodoInDbAsync(Guid id)
        {
            try
            {
                CachedTodo = await _repository.RetrieveAsync(id);

                return CachedTodo != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
