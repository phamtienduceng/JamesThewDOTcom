﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JamesRecipes.Models;

public partial class AnonymousRecipe
{
    public int AnonymousRecipeId { get; set; }

    public Guid AnonymousId { get; set; }

    public string Title { get; set; } = null!;

    public string Ingredients { get; set; } = null!;

    public string Procedure { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? Image { get; set; }

    public TimeSpan? Timeneeds { get; set; }

    public string? VideoUrl { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public int ContestId { get; set; }

    public string AnonymousName { get; set; } = null!;

    public virtual ICollection<AnonymousContestEntry> AnonymousContestEntries { get; set; } = new List<AnonymousContestEntry>();

    //public virtual Contest Contest { get; set; } = null!;
}
