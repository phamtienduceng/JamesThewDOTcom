﻿using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public decimal? Total { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual User? User { get; set; }
}
