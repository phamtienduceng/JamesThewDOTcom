using Humanizer.Localisation;
using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Policy;

namespace JamesRecipes.Service.Admin;

public class BookService : IBook
{
    private readonly JamesrecipesContext _db;

    public BookService(JamesrecipesContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CategoriesBook>> CategoriesBooks()
    {
        return await _db.CategoriesBooks.ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int caterogyId = 0)
    {
        sTerm = sTerm.ToLower();
        IEnumerable<Book> books = await (from book in _db.Books
                                         join CategoriesBook in _db.CategoriesBooks
                                         on book.CategoryBookId equals CategoriesBook.CategoryBookId
                                         where string.IsNullOrWhiteSpace(sTerm) || (book != null && book.Title.ToLower().StartsWith(sTerm))
                                         select new Book
                                         {
                                             BookId = book.BookId,
                                             Title = book.Title,
                                             Author = book.Author,
                                             Image = book.Image,
                                             Price = book.Price,
                                             Quantity = book.Quantity,
                                             CategoryBookId = book.CategoryBookId,
                                             CategoryName = CategoriesBook.CategoryName
                                         }
                     ).ToListAsync();
        if (caterogyId > 0)
        {

            books = books.Where(a => a.CategoryBookId == caterogyId).ToList();
        }
        return books;

    }
}