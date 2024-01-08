using System.ComponentModel.DataAnnotations;

namespace JamesRecipes.Models;

public class ContactViewModel
{
    [Required]
    public string ToEmail { get; set; }
    [Required]
    public string Subject { get; set; }
    [Required]
    public string Body { get; set; }
}