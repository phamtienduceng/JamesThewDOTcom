using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Membership
{
    public int MembershipId { get; set; }

    public int? UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual User? User { get; set; }
}
