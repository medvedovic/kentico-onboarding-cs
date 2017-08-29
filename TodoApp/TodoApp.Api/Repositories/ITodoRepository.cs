using System.Collections.Generic;
using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories
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
