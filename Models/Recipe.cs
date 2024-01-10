using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JamesRecipes.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public int? UserId { get; set; }
    
    public int? CategoryRecipeId { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "Title between 3 and 250 characters")]
    [MaxLength(250, ErrorMessage = "Title between 3 and 250 characters")]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(10, ErrorMessage = "Ingredients between 10 and 250 characters")]
    [MaxLength(250, ErrorMessage = "Ingredients between 10 and 250 characters")]
    public string Ingredients { get; set; } = null!;

    [Required]
    [MinLength(10, ErrorMessage = "ErrorMessage between 10 and 250 characters")]
    [MaxLength(250, ErrorMessage = "ErrorMessage between 10 and 250 characters")]
    public string Procedure { get; set; } = null!;

    public bool IsMembershipOnly { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool Status { get; set; }
    
    public string? Image { get; set; }

    [Required]
    public TimeSpan? Timeneeds { get; set; }

    public string? VideoUrl { get; set; }

    public int? Rating { get; set; }

    public virtual CategoriesRecipe CategoryRecipe { get; set; } = null!;

    public virtual ICollection<ContestEntry> ContestEntries { get; set; } = new List<ContestEntry>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User? User { get; set; }
}
