using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Contracts.Dtos
{
    public class TodoDto
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }
    }
}