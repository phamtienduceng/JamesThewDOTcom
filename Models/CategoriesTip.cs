using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class CategoriesTip
{
    public int CategoryTipId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();
}
