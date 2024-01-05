using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? BookId { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }

    public decimal UnitPrice { get; set; }

    public bool Status { get; set; }

    public virtual Book? Book { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Order? Order { get; set; }
}
