using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAll();
        Todo Get(Guid id);
        Todo Add(Todo item);
        bool Remove(Guid id);
        bool Update(Guid id, Todo item);
        Task<Todo> GetAsync(Guid id);
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> AddAsync(Todo todo);
        Task<bool> RemoveAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, Todo todo);
    }
}
