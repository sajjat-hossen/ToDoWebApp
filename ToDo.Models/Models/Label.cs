using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDo.DomainLayer.Models
{
    public class Label
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Label Name")]
        public string Name { get; set; }

    }
}
