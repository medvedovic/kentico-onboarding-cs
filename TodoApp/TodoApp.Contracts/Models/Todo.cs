using System;

namespace TodoApp.Contracts.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Value { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            return ((obj is Todo)
                && Id == ((Todo)obj).Id
                && Value == ((Todo) obj).Value);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}