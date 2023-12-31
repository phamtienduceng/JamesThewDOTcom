using Humanizer.Localisation;
using JamesRecipes.Models.Book;

namespace JamesRecipes.Repository.Admin;

public interface IBookManagementRepository
{
    List<Book> GetBooks();
    Book GetBook(int id);
    void AddBook(Book book);
    void UpdateBook(int id, Book updateBook);
    Book DeleteBook(int id);
    bool CheckBook(Book book);
}