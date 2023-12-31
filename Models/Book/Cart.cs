using System;
using System.Collections.Generic;

namespace JamesRecipes.Models.Book;

public partial class Cart
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal totalAmount { get; set; }
    public string? Image { get; set; }
    public int BookId { get; set; }
    public string Titile { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual ICollection<CartItems> CartItems { get; set; }
    public Book? Books { get; set; }
}
