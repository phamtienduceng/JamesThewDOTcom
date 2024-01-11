using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamesRecipes.Models;

public partial class CombinedContestScore
{
    public int? EntryId { get; set; }
    public int? ContestId { get; set; }
    public int? ContestEntryScore { get; set; }
    public int? AnonymousEntryId { get; set; }
    public int? AnonymousContestId { get; set; }
    public int? AnonymousContestEntryScore { get; set; }
}


