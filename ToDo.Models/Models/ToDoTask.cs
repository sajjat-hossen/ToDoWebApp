using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDo.DomainLayer.CustomDataAnnotation;

namespace ToDo.DomainLayer.Models
{
    public class ToDoTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ValidDueDate]
        public DateTime DueDate { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DisplayName("Label")]
        public int LabelId { get; set; }

        [ForeignKey("LabelId")]
        [ValidateNever]
        public Label Label { get; set; }

        [ValidateNever]
        public string? UserId { get; set; }

    }
}
