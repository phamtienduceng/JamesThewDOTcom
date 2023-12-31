using System;
using System.Collections.Generic;

namespace JamesRecipes.Models.Book;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
