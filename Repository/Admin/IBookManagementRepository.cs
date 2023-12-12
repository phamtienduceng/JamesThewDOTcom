using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IBookManagementRepository
{
    Task<List<Book>> GetAllBooks();
    Task<Book> GetBook(int bookID);
    Task<Book> CreateBook(Book newBook);
    Task<Book> UpdateBook(int bookId,int price ,int quantity);
    Task<bool> DeleteBook(int bookId);
    
}