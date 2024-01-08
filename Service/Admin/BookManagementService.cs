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
    public List<BookModel> GetBooks()
    {
        return _db.Books.Include(b=>b.Category).ToList();
    }

    public BookModel GetBook(int Id)
    {
        var book = _db.Books.FirstOrDefault(c => c.Id == Id);
        return book!;
    }

    public void AddBook(BookModel book)
    {
        _db.Books.Add(book);
        _db.SaveChanges();
    }

    public void UpdateBook(int id, BookModel updateBook)
    {
        if (updateBook == null)
        {
            throw new ArgumentNullException(nameof(updateBook), "The provided book for update is null.");
        }
        var existingBook = GetBook(id);
        if (existingBook != null)
        {
            existingBook.Title = updateBook.Title;
            existingBook.Author = updateBook.Author;
            existingBook.Price = updateBook.Price;
            existingBook.Description = updateBook.Description;
            existingBook.Image = updateBook.Image;
            _db.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("Book with the provided id does not exist.");
        }
    }

    public bool CheckBook(BookModel book)
    {
        var model = _db.Books.SingleOrDefault(b => b.Id == book.Id);
        if (model != null)
        {
            return true;
        }
        return false;
    }
    public void DeleteBook(int id)
    {
        var book = _db.Books.FirstOrDefault(c => c.Id == id);
        if (book != null)
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
    }
}