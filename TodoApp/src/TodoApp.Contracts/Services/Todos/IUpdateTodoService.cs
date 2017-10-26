using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services.Todos
{
    public interface IUpdateTodoService
    {
        Task<Todo> UpdateTodoAsync(IConvertibleTo<Todo> todoViewModel);
    }
}
