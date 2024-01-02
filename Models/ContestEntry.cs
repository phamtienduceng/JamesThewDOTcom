using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class ContestEntry
{
    public int EntryId { get; set; }

    public int? ContestId { get; set; }

    public int? UserId { get; set; }
    //public int UserID { get; internal set; }
    public int? RecipeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Score { get; set; }

    public string? Image { get; set; }

    public virtual Contest? Contest { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }

}
