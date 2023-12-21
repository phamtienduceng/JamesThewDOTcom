using System;
using System.Collections.Generic;

namespace model.Models;

public partial class ViewTipManagement
{
    public int TipId { get; set; }

    public string TipTitle { get; set; } = null!;

    public DateTime? TipCreatedAt { get; set; }

    public bool? TipStatus { get; set; }

    public int? TipRating { get; set; }

    public string TipContent { get; set; } = null!;

    public string TipCategoryName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string RoleName { get; set; } = null!;
}
