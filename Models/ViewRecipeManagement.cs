using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class ViewRecipeManagement
{
    public int RecipeId { get; set; }

    public string RecipeTitle { get; set; } = null!;

    public DateTime? RecipeCreatedAt { get; set; }

    public bool? RecipeStatus { get; set; }

    public int? RecipeRating { get; set; }

    public string RecipeIngredients { get; set; } = null!;

    public string RecipeProcedure { get; set; } = null!;

    public string RecipeCategoryName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string RoleName { get; set; } = null!;
}
