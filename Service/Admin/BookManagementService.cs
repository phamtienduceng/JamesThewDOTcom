using Humanizer.Localisation;
using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Policy;

namespace JamesRecipes.Service.Admin;

public class BookManagementService : IBookManagementRepository
{
    private readonly JamesrecipesContext _db;

    public BookManagementService(JamesrecipesContext db)
    {
        this._db = db;
    }

<<<<<<< Updated upstream
    public async Task<IEnumerable<Genre>> Genres()
    {
        return await _db.Genres.ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0)
    {
        sTerm = sTerm.ToLower();
        IEnumerable<Book> books = await (from book in _db.Books
                                         join genre in _db.Genres
                                         on book.GenreId equals genre.Id
                                         where string.IsNullOrWhiteSpace(sTerm) || (book != null && book.BookName.ToLower().StartsWith(sTerm))
                                         select new Book
                                         {
                                             Id = book.Id,
                                             Image = book.Image,
                                             AuthorName = book.AuthorName,
                                             BookName = book.BookName,
                                             GenreId = book.GenreId,
                                             Price = book.Price,
                                             GenreName = genre.GenreName
                                         }
                     ).ToListAsync();
        if (genreId > 0)
        {

            books = books.Where(a => a.GenreId == genreId).ToList();
        }
        return books;

=======
    public Task CreateBook(Book newBook)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBook(int bookId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        var books = await _context.Books.ToListAsync();
        return books;
    }

    public Task<Book> GetBook(int bookID)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBook(Book editBook)
    {
        throw new NotImplementedException();
>>>>>>> Stashed changes
    }
}