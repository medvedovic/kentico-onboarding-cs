using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.ViewModels
{
    public class TodoViewModel
    {
        [Required]
        [StringLength(255)]
        public string Value { get; set; }
    }
}