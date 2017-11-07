using System;
using System.Collections.Generic;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Base.EqualityComparer
{
    internal class TodosEqualityComparer : IEqualityComparer<Todo>
    {
        private static readonly Lazy<TodosEqualityComparer> _instance = new Lazy<TodosEqualityComparer>(() => new TodosEqualityComparer());

        public static TodosEqualityComparer Instance => _instance.Value;
        
        private TodosEqualityComparer() { }

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