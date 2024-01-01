using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.Admin;

public interface IBookManagementRepository
{
    List<Book> GetAllBooks();
    
    Book GetBook(int id);
    
    void PostBook(Book newBook);

    void PutBook(int id, Book book);
    
    IPagedList<Book> PageList(int page, int pageSize, List<Book> books);

    void DeleteBook(int id);
    
}