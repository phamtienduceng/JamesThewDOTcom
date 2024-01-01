using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class Faq
{
    public int Faqid { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
