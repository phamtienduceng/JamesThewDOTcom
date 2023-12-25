using Humanizer.Localisation;
using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IBookManagementRepository
{
    IEnumerable<Book> GetList();
    IEnumerable<Book> Search(string search);
    Book BookDetail(int Id);
    Book EditBook(int bookId, Book updatedBook);
    Book CreateBook(Book newBook);
    Book DeleteBook(int Id);
}