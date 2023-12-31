﻿using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class ContestEntry
{
    public int EntryId { get; set; }

    public int? ContestId { get; set; }

    public int? UserId { get; set; }

    public int? RecipeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    // min score is 1, max score is 100
    [System.ComponentModel.DataAnnotations.Range(1, 100)]
    public int? Score { get; set; }

    public string? Image { get; set; }

    public virtual Contest? Contest { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
