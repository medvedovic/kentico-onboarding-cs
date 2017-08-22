using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoOnboardingCs.Api.Models.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAll();
        Todo Get(int id);
        Todo Add(Todo item);
        void Remove(int id);
        bool Update(Todo item);
    }
}
