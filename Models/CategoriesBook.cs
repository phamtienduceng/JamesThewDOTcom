using System;
using System.Collections.Generic;

namespace JamesRecipes.Models;

public partial class CategoriesBook
{
    public int CategoryBookId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
