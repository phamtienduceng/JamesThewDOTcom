﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JamesRecipes.Models;

public partial class User
{
    public int UserId { get; set; }
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(6, ErrorMessage = "At least 6 characters")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "Password is required.")]
    [RegularExpression(@"^(?=.*[!@#$%^&*()\-_=+{}[\]|\\;:'""<>,./?])(?=.*[A-Z])(?=.*[a-z]).{6,}$", ErrorMessage = "Password must be at least 6 characters, 1 uppercase letter and 1 lowercase letter  and contain at least one special character.")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Email is is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }
    [RegularExpression(@"^[0-9]+\d{9}$", ErrorMessage = "Phone number must be 10 digits.")]
    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Avatar { get; set; }
    
    public virtual ICollection<ContestEntry> ContestEntries { get; set; } = new List<ContestEntry>();

    public virtual ICollection<Contest> Contests { get; set; } = new List<Contest>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    
    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();
}
