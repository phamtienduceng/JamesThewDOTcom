using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamesRecipes.Models.Book;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Image { get; set; }

    public int CategoryBookId { get; set; }
    [NotMapped]
    public string CategoryName { get; set; }

    public virtual CategoriesBook? Category { get; set; }

    public virtual ICollection<OrderItem> OrderDetails { get; set; } = new List<OrderItem>();
}
