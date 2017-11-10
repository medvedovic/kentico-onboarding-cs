using System.ComponentModel.DataAnnotations;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;

namespace TodoApp.Api.ViewModels
{
    public class TodoViewModel: IConvertibleTo<Todo>
    {
        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        public Todo Convert() =>
            new Todo
            {
                Value = this.Value
            };
    }
}