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
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.Id.Equals(y.Id) 
                && string.Equals(x.Value, y.Value) 
                && x.CreatedAt.Equals(y.CreatedAt) 
                && x.UpdatedAt.Equals(y.UpdatedAt);
        }

        public int GetHashCode(Todo obj)
        {
            unchecked
            {
                var hashCode = obj.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.Value.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.CreatedAt.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.UpdatedAt.GetHashCode();
                return hashCode;
            }
        }
    }
}