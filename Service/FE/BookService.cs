//using Humanizer.Localisation;
//using JamesRecipes.Models;
//using JamesRecipes.Models.Book;
//using JamesRecipes.Repository.Admin;
//using JamesRecipes.Repository.FE;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Net;
//using System.Net.Http;
//using System.Security.Policy;

//namespace JamesRecipes.Service.FE;

//public class BookService : IBook
//{
//    private readonly JamesrecipesContext _db;

//    public BookService(JamesrecipesContext db)
//    {
//        _db = db;
//    }

//    //public async Task<IEnumerable<CategoriesBook>> CategoriesBooks()
//    //{
//    //    return await _db.CategoriesBooks.ToListAsync();
//    //}

//    public List<BookModel> GetAllBook()
//    {
//        return _db.Books.ToList();
//    }

//}