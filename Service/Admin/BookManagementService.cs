using Humanizer.Localisation;
using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public List<Book> GetBooks()
    {
        return _db.Books.ToList();
    }

    public Book GetBook(int id)
    {
        var book = _db.Books.FirstOrDefault(c => c.BookId == id);
        return book ?? null;
    }

    public void AddBook(Book book)
    {
        _db.Books.Add(book);
        _db.SaveChanges();
    }
    public void UpdateBook(int id, Book updateBook)
    {
        var existingBook = GetBook(id);
        if (existingBook != null)
        {
            existingBook.Title = updateBook.Title;
            existingBook.Author = updateBook.Author;
            existingBook.Price = updateBook.Price;
            existingBook.Quantity = updateBook.Quantity;
            existingBook.CategoryName = updateBook.CategoryName;
            existingBook.Image = updateBook.Image;

            _db.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Book with the provided id does not exist.", nameof(id));
        }
    }

    public bool CheckBook(Book book)
    {
        var model = _db.Books
            .Include(b => b.Category)
            .SingleOrDefault(b => b.BookId == book.BookId);

        if (model != null && model.Category != null)
        {
            return true;
        }

        return false;
    }

    public Book DeleteBook(int id)
    {
        var book = _db.Books.SingleOrDefault(c => c.BookId == id);
        if (book != null)
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
        return book!;
    }
}