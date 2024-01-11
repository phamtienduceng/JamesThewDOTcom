using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

    [NotMapped]
    [Display(Name = "Image")]
    public IFormFile ImageFile { get; set; }

    public virtual Contest? Contest { get; set; }
}
