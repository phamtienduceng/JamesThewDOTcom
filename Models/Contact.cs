using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JamesRecipes.Models;

public partial class Contact
{
    public int ContactId { get; set; }

    [Required(ErrorMessage = "Required")]
    public string? Message { get; set; }

    [Required(ErrorMessage = "Required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Required")]
    public string? Email { get; set; }
}
