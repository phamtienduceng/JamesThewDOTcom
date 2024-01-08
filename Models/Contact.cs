using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Contact
{
    public int ContactId { get; set; }

    public string? Message { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }
    
    public bool Status { get; set; }
}
