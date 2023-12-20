using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IBookManagementRepository
{
<<<<<<< Updated upstream
    Task<IEnumerable<Genre>> Genres();
    Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0);

=======
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book> GetBook(int bookID);
    Task CreateBook(Book newBook);
    Task UpdateBook(Book editBook);
    Task DeleteBook(int bookId);
    
>>>>>>> Stashed changes
}