using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public string Content { get; set; } = null!;

    public int? Rating { get; set; }

    public bool IsAdminFeedback { get; set; }

    public int? TipId { get; set; }

    public int? RecipeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual Tip? Tip { get; set; }

    public virtual User? User { get; set; }
}
