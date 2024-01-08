using System;
using System.Collections.Generic;

namespace JamesRecipes.Models.Book;
public class Order
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; }
    public string UserName { get; set; }
    public DateTime OrderDate { get; set; }
    public int Status { get; set; }
    public int UserId { get; set; }
}
