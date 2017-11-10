using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services.Todos
{
    public interface ICreateTodoService
    {
        Task<Todo> CreateTodoAsync(IConvertibleTo<Todo> todoViewModel);
    }
}
