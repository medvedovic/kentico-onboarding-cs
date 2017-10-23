using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services.Todos
{
    public interface IPutTodoService
    {
        Todo ExistingTodo { get; set; }
        Task<Todo> UpdateTodoAsync(Guid id, IConvertibleTo<Todo> todoViewModel);
    }
}
