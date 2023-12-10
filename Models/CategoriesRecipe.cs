using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class CategoriesRecipe
{
    public int CategoryRecipeId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
