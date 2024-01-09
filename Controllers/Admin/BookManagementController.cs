using JamesRecipes.Models;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class BookManagementController : Controller
{
    private readonly IBookManagementRepository _book;
    private readonly IBookCategoriesManagementRepository _bookCategories;

    public BookManagementController(IBookManagementRepository book, IBookCategoriesManagementRepository bookCategories)
    {
        _book = book;
        _bookCategories = bookCategories;
    }

    [AuthenticationAdmin]
    [HttpGet("index")]
    public IActionResult Index()
    {
        var books = _book.GetAllBooks();
        return View("~/Views/Admin/BookManagement/Index.cshtml", books);
    }
    
    [AuthenticationAdmin]
    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewBag.CategoryId = new SelectList(_bookCategories.GetAllCategories(), "CategoryBookId", "CategoryName");
        return View("~/Views/Admin/BookManagement/Create.cshtml");
    }
    
    [HttpPost("create")]
    public IActionResult Create(Book newBook, IFormFile file)
    {
        try
        {
            if (file != null)
            {
                var path = Path.Combine("wwwroot/fe/img", file.FileName);
                var stream = new FileStream(path, FileMode.Create);
                file.CopyToAsync(stream);
                newBook.Image = "fe/img/" + file.FileName;
                _book.PostBook(newBook);
                return RedirectToAction("Index");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        ViewBag.CategoryId = new SelectList(_bookCategories.GetAllCategories(), "CategoryBookId", "CategoryName");
        return View("~/Views/FE/Recipe/Create.cshtml");
    }

    
    [AuthenticationAdmin]
    [HttpGet("edit")]
    public IActionResult Edit(int id)
    {
        var book = _book.GetBook(id);
        ViewBag.CategoryId = new SelectList(_bookCategories.GetAllCategories(), "CategoryBookId", "CategoryName");
        return View("~/Views/Admin/BookManagement/Edit.cshtml", book);
    }
    
    

    public IActionResult Delete(int id)
    {
        _book.DeleteBook(id);
        return RedirectToAction("Index", "BookManagement");
    }

    [AuthenticationAdmin]
    [HttpGet("book_detail")]
    public IActionResult GetBookById(int id)
    {
        var book = _book.GetBook(id);
        return View("~/Views/Admin/BookManagement/BookDetail.cshtml", book);
    }
}