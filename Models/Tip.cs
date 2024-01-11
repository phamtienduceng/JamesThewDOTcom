using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JamesRecipes.Models;

public partial class Tip
{
    public int TipId { get; set; }

    public int? UserId { get; set; }

    public int CategoryTipId { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Title between 10 and 50 characters")]
    [MinLength(10, ErrorMessage = "Title between 10 and 50 characters")]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(50, ErrorMessage = "Title between 10 and 50 characters")]
    [MinLength(10, ErrorMessage = "Title between 10 and 50 characters")]
    public string Content { get; set; } = null!;

    public bool IsMembershipOnly { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool Status { get; set; }

    [Required]
    public string? Image { get; set; }

    public int? Rating { get; set; }

    public virtual CategoriesTip CategoryTip { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User? User { get; set; }
}
