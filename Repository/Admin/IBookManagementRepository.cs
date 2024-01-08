using Humanizer.Localisation;
using JamesRecipes.Models.Book;

namespace JamesRecipes.Repository.Admin;

public interface IBookManagementRepository
{
    List<BookModel> GetBooks();
    BookModel GetBook(int id);
    void AddBook(BookModel book);
    void UpdateBook(int id, BookModel updateBook);
    void DeleteBook(int id);
    bool CheckBook(BookModel book);
}