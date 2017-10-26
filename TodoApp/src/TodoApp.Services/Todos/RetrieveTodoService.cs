using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Services.Todos
{
    class RetrieveTodoService: IRetrieveTodoService
    {
        private readonly ITodoRepository _repository;
        private Todo CachedTodo { get; set; }

        public RetrieveTodoService(ITodoRepository repository)
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
                if (CachedTodo?.Id == id)
                    return true;

                CachedTodo = await _repository.RetrieveAsync(id);

                return CachedTodo != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
