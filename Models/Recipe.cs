using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public int? UserId { get; set; }

    public int CategoryRecipeId { get; set; }

    public string Title { get; set; } = null!;

    public string Ingredients { get; set; } = null!;

    public string Procedure { get; set; } = null!;

    public bool IsMembershipOnly { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool Status { get; set; }

    public string? Image { get; set; }

    public TimeSpan? Timeneeds { get; set; }

    public string? VideoUrl { get; set; }

    public int? Rating { get; set; }

    public virtual CategoriesRecipe CategoryRecipe { get; set; } = null!;

    public virtual ICollection<ContestEntry> ContestEntries { get; set; } = new List<ContestEntry>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User? User { get; set; }
}