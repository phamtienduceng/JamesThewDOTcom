using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class ViewAnonymousContact
{
    public int AnonymousRecipeId { get; set; }

    public Guid AnonymousId { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
