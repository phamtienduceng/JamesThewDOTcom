using Humanizer.Localisation;

namespace JamesRecipes.Models.Book.DTOs
{
    public class ViewBookCate
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<CategoriesBook> CategoriesBooks { get; set; }
        public string STerm { get; set; } = "";
        public int CategoryBookId { get; set; } = 0;
    }
}
