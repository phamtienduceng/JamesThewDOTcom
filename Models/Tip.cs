using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Tip
{
    public int TipId { get; set; }

    public int? UserId { get; set; }

    public int CategoryTipId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsMembershipOnly { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }

    public string? Image { get; set; }
    
    public int? Rating { get; set; }

    public virtual CategoriesTip CategoryTip { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User? User { get; set; }
}
