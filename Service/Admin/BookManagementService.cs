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

    public Book BookDetail(int Id)
    {
        var book = _db.Books.FirstOrDefault(b => b.BookId == Id);
        return null!;
    }

    public Book CreateBook(Book newBook)
    {
        var book = _db.Books.Add(newBook);
        _db.SaveChanges();
        return book.Entity;
    }

    public Book DeleteBook(int Id)
    {
        var book = _db.Books.SingleOrDefault(c => c.BookId == Id);
        if (book != null)
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
        return book!;
    }

    public Book EditBook(int bookId, Book updatedBook)
    {
        var book = _db.Books.FirstOrDefault(b => b.BookId == bookId);

        if (book != null)
        {
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Image = updatedBook.Image;
            book.Price = updatedBook.Price;
            _db.SaveChanges();
            return book;
        }
        return null!;
    }

    public IEnumerable<Book> GetList()
    {
       return _db.Books.ToList();
    }

    public IEnumerable<Book> Search(string search)
    {
        var result = _db.Books.Where(b => b.Title.ToUpper().Contains(search));
        return result.ToList();
    }

}