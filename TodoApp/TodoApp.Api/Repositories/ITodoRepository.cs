using System.Collections.Generic;

namespace TodoApp.Api.Models.Repositories
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
