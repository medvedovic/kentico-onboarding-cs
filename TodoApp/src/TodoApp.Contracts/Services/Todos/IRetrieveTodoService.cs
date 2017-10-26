using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services.Todos
{
    public interface IRetrieveTodoService
    {
        Task<Todo> RetrieveTodoAsync(Guid id);

        Task<bool> IsTodoInDbAsync(Guid id);
    }
}
