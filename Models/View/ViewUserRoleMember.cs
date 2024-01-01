using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class ViewUserRoleMember
{
    public int MembershipId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public string RoleName { get; set; } = null!;

    public int RoleId { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? Avatar { get; set; }

    public int? UserRole { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? UserMem { get; set; }
}
