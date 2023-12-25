using Humanizer.Localisation;
using JamesRecipes.Models;
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

    public List<Book> Search(string searchString)
    {
        var books = _db.Books.Where(b => b.Title.Contains(searchString.ToLower())).ToList();
        return books;
    }

    public IEnumerable<Book> GetBooks()
    {
        return _db.Books.ToList();
    }
}