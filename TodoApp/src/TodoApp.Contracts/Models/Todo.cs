using System;

namespace TodoApp.Contracts.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}