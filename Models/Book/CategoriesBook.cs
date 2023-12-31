using System;
using System.Collections.Generic;

namespace JamesRecipes.Models.Book;

public partial class CategoriesBook
{
    public int CategoryBookId { get; set; }

    public string CategoryName { get; set; }

    public virtual List<Book> Books { get; set; }
}