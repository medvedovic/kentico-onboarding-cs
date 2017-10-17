using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Dtos
{
    public class TodoDto
    {
        [Required]
        [StringLength(255)]
        public string Value { get; set; }
    }
}