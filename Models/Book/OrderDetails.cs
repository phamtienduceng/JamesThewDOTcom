using System;
using System.Collections.Generic;

namespace JamesRecipes.Models.Book;

public class OrderDetails
{
    public int OrderDetailsId { get; set; }
    public string UserName { get; set; }
    public string OrderCode { get; set; }
    public long BookId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
