using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> RetrieveAsync(Guid id);
        Task<IEnumerable<Todo>> RetrieveAllAsync();
        Task<Todo> CreateAsync(Todo todo);
        Task<bool> RemoveAsync(Guid id);
        Task<bool> UpdateAsync(Todo todo);
    }
}
