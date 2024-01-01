using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.FE;

public interface IBook
{
    List<Book> GetAllBooks();

    Book GetBook(int id);
    
    List<Book> Sorting(List<Book> books, string sortOrder);
    
    List<Book> Search(string searchString);

    IPagedList<Book> PageList(int page, int pageSize, List<Book> books);

    List<Book> Filter(int? categoryId, int? priceMin, int? priceMax, List<Book> books);

    List<Book> RelatedBook();
}