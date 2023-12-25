using Humanizer.Localisation;
using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface IBook
{
    List<Book> Search(string searchString);
    IEnumerable<Book> GetBooks();
}