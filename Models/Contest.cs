using JamesRecipes.Models.Tri;
using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Contest
{
    public int ContestId { get; set; }

    public int? AdminId { get; set; }

    public string Title { get; set; } = null!;

    public string Guidelines { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Image { get; set; }

    public virtual User? Admin { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<AnonymousContestEntry> AnonymousContestEntries { get; set; } = new List<AnonymousContestEntry>();

    public virtual ICollection<ContestEntry> ContestEntries { get; set; } = new List<ContestEntry>();

    public virtual ICollection<AnonymousContestEntry> AnonymousContestEntries { get; set; } = new List<AnonymousContestEntry>();

    public virtual ICollection<AnonymousRecipe> AnonymousRecipes { get; set; } = new List<AnonymousRecipe>();
}
