using System;

namespace TodoApp.Api.Dtos
{
    public class TodoDto
    {
        public Guid? Id { get; set; }
        public string Value { get; set; }
    }
}