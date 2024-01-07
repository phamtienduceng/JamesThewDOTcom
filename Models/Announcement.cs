using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Announcement
{
    public int AnnouncementId { get; set; }

    public int? WinnerId { get; set; }

    public int? AnonymousWinnerId { get; set; }

    public int? ContestId { get; set; }

    public string Content { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime? DatePost { get; set; }

    public string? Image { get; set; }

    public virtual Contest? Contest { get; set; }
}
