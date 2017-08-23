using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KenticoOnboardingCs.Api.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Value { get; set; }
    }
}