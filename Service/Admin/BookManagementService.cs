using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using X.PagedList;

namespace JamesRecipes.Service.Admin;

public class BookManagementService: IBookManagementRepository
{
    private readonly JamesrecipesContext _db;
    

    public BookManagementService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<Book> GetAllBooks()
    {
        return _db.Books.ToList();
    }

    public Book GetBook(int id)
    {
        var book =  _db.Books.SingleOrDefault(b => b.BookId == id);
        return book!;
    }

    public void PostBook(Book newBook)
    {
        _db.Books.Add(newBook);
        _db.SaveChanges();
    }

    public void PutBook(int id, Book book)
    {
        var b = _db.Books.SingleOrDefault(b => b.BookId == id);
        b!.Author = book.Author;
        b.Price = book.Price;
        b.CategoryId = book.CategoryId;
        b.StockQuantity = book.StockQuantity;
        b.Image = book.Image;
    }

    public IPagedList<Book> PageList(int page, int pageSize, List<Book> books)
    {
        throw new NotImplementedException();
    }

    public void DeleteBook(int id)
    {
        var b = _db.Books.SingleOrDefault(b => b.BookId == id);
        _db.Books.Remove(b!);
    }
}