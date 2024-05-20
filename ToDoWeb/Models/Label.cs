using System.ComponentModel.DataAnnotations;

namespace ToDoWeb.Models
{
    public class Label
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

    }
}
