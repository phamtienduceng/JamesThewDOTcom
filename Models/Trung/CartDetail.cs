﻿using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class CartDetail
{
    public int CartDetailId { get; set; }

    public int? CartId { get; set; }

    public int? BookId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public Book? Book { get; set; }

    public virtual Cart? Cart { get; set; }
}