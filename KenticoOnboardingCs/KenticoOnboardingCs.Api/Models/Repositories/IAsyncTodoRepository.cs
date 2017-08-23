using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoOnboardingCs.Api.Models.Repositories
{
    public interface IAsyncTodoRepository
    {
        List<Todo> Todos { get; }
        Task<Todo> GetAsync(int id);
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> AddAsync(Todo todo);
        Task<bool> RemoveAsync(int id);
        Task<bool> UpdateAsync(int id, Todo todo);
    }
}
