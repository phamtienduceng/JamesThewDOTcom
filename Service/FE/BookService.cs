using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using X.PagedList;

namespace JamesRecipes.Service.FE;

public class BookService: IBook
{
    private readonly JamesrecipesContext _db;

    public BookService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<Book> GetAllBooks()
    {
        return _db.Books.ToList();
    }

    public Book GetBook(int id)
    {
        var book = _db.Books.SingleOrDefault(b => b.BookId == id);
        return book!;
    }

    public List<Book> Sorting(List<Book> books, string sortOrder)
    {
        switch (sortOrder)
        {
            case "name_desc":
                books = books.OrderByDescending(r => r.Title).ToList();
                break;
            case "name_asc":
                books = books.OrderBy(r => r.Title).ToList();
                break;
            case "Date":
                books = books.OrderBy(r => r.CreatedAt).ToList();
                break;
            case "date_desc":
                books = books.OrderByDescending(r => r.CreatedAt).ToList();
                break;
        }
        return books;
    }

    public List<Book> Search(string searchString)
    {
        return _db.Books.Where(r => r.Title.Contains(searchString)).ToList();
    }

    public IPagedList<Book> PageList(int page, int pageSize, List<Book> books)
    {
        return books.ToPagedList(page, pageSize);

    }

    public List<Book> Filter(int? categoryId, int? priceMin, int? priceMax, List<Book> books)
    {
        var bs = books;
        if (categoryId.HasValue)
        {
            bs = bs.Where(r => r.BookId == categoryId).ToList();

        }
        if (priceMin.HasValue && priceMax.HasValue)
        {
            bs = bs.Where(r => r.Price >= priceMin && r.Price <= priceMax).ToList();
        }
        return bs;
    }

    public List<Book> RelatedBook()
    {
        return _db.Books.Take(10).ToList();
    }
}