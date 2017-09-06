using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Value { get; set; }
    }
}