using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using X.PagedList;

namespace JamesRecipes.Controllers.FE;


public class BookController : Controller
{
    private readonly IBook _book;
    private readonly IBookCategoriesManagementRepository _bookCategories;

    public BookController(IBook book, IBookCategoriesManagementRepository bookCategories)
    {
        _book = book;
        _bookCategories = bookCategories;
    }

    public IActionResult Index(string sortOrder, string searchString, int? categoryId, int? priceMin, int? priceMax, int page = 1)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["CurrentFilter"] = searchString;
        
        var bs = _book.GetAllBooks();


        if (!string.IsNullOrEmpty(searchString))
        {
            bs = _book.Search(searchString);
        }
        
        ViewBag.CategoryId = new SelectList(_bookCategories.GetAllCategories(), "CategoryBookId", "CategoryName", categoryId);
        
        if (categoryId != 0 || priceMin != null || priceMax != null)
        {
            bs = _book.Filter(categoryId, priceMin, priceMax, bs);
        }
        bs = _book.Sorting(bs, sortOrder);
        
        page = page < 1 ? 1 : page;
        var books = _book.PageList(page, 9, bs);

        return View("~/Views/FE/Book/Index.cshtml", books);
    }
    
    [HttpGet("single_Book")]
    public IActionResult SingleBook(int id)
    {
        var book = _book.GetBook(id);
        return View("~/Views/FE/Book/SingleBook.cshtml", book);
    }
}
