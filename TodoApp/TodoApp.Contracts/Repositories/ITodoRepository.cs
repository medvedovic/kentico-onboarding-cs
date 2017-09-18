using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> Get(Guid id);
        Task<IEnumerable<Todo>> GetAll();
        Task<Todo> Add(Todo todo);
        Task<bool> Remove(Guid id);
        Task<bool> Update(Guid id, Todo todo);
    }
}
