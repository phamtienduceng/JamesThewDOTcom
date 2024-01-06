using System;
using System.Collections.Generic;

namespace JamesRecipes.Models.View;

public partial class ViewHomepage
{
    public string RecipeTitle { get; set; } = null!;

    public string? RecipeCategoryName { get; set; }

    public string? RecipeImage { get; set; }

    public int? RecipeRating { get; set; }

    public TimeSpan? RecipeTimeNeeds { get; set; }

    public string? TipTitle { get; set; }

    public string? TipCategoryName { get; set; }

    public string? TipImage { get; set; }

    public int? TipRating { get; set; }

    public string? ContestTitle { get; set; }

    public string? ContestGuidelines { get; set; }

    public DateTime? ContestStartDate { get; set; }

    public DateTime? ContestEndDate { get; set; }

    public string? ContestImage { get; set; }

    public int? AnnouncementWinnerId { get; set; }

    public string? AnnouncementContent { get; set; }

    public string? AnnouncementTitle { get; set; }

    public string? AnnouncementImage { get; set; }


}
