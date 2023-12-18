using System.ComponentModel.DataAnnotations;

namespace JamesRecipes.Models
{
    public partial class Contact
    {
        [Key]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Massage { get; set; }

    }
}
