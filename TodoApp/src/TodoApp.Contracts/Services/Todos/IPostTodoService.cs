using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services.Todos
{
    public interface IPostTodoService
    {
        Task<Todo> CreateTodoAsync(Todo todo);
    }
}
