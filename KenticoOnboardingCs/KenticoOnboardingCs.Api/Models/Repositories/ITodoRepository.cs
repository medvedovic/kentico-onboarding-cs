using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoOnboardingCs.Api.Models.Repositories
{
    public interface ITodoRepository
    {
        List<Todo> Todos { get; }
        IEnumerable<Todo> GetAll();
        Todo Get(int id);
        Todo Add(Todo item);
        bool Remove(int id);
        bool Update(int id, Todo item);
    }
}
