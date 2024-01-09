using System;
using System.Collections.Generic;
using JamesRecipes.Models;
namespace JamesRecipes.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Image { get; set; }

    public int? CategoryId { get; set; }

    public virtual CategoriesBook? Category { get; set; }
}