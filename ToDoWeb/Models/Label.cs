using System.ComponentModel.DataAnnotations;

namespace ToDoWeb.Models
{
    public class Label
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
