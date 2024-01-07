using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class AnonymousContestEntry
{
    public int AnonymousEntryId { get; set; }

    public int ContestId { get; set; }

    public int AnonymousRecipeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Score { get; set; }

    public string? Image { get; set; }

    //public virtual AnonymousRecipe AnonymousRecipe { get; set; } = null!;

    //public virtual Contest Contest { get; set; } = null!;

    
}
