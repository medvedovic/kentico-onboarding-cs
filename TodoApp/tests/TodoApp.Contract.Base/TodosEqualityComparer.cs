using System.Collections.Generic;
using TodoApp.Contracts.Models;

namespace TodoApp.Contract.Base
{
    public class TodosEqualityComparer : IEqualityComparer<Todo>
    {
        public bool Equals(Todo x, Todo y)
        {
            return x.Id == y.Id
                && x.Value == y.Value
                && x.CreatedAt == y.CreatedAt;
        }

        public int GetHashCode(Todo obj)
        {
            return obj.Id.GetHashCode() ^ obj.CreatedAt.GetHashCode();
        }
    }
}