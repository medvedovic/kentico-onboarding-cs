using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApp.Contracts.Models
{
    public class Todo
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}