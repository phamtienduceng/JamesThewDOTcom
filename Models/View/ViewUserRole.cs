using System;
using System.Collections.Generic;

namespace JamesRecipes.Models.View;

public partial class ViewUserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? Idroles { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Avatar { get; set; }
}
