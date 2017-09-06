using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAll();
        Todo Get(int id);
        Todo Add(Todo item);
        bool Remove(int id);
        bool Update(int id, Todo item);
        Task<Todo> GetAsync(int id);
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> AddAsync(Todo todo);
        Task<bool> RemoveAsync(int id);
        Task<bool> UpdateAsync(int id, Todo todo);
    }
}
