using JamesRecipes.Models;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class BookCategoriesManagementController : Controller
{
    private readonly IBookCategoriesManagementRepository _bookCategories;

    public BookCategoriesManagementController(IBookCategoriesManagementRepository bookCategories)
    {
        _bookCategories = bookCategories;
    }

    [AuthenticationAdmin]
    [HttpGet("index")]
    public IActionResult Index()
    {
        var cates = _bookCategories.GetAllCategories();
        return View("~/Views/Admin/BookCategories/Index.cshtml", cates);
    }
    
    [AuthenticationAdmin]
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/BookCategories/Create.cshtml");
    }
    
    [HttpPost("create")]
    public IActionResult Create(CategoriesBook newCategoriesBook)
    {
        if (ModelState.IsValid)
        {
            var model = _bookCategories.GetAllCategories();
            bool isNameUsed = model.Any(c => c.CategoryName.Equals(newCategoriesBook.CategoryName, StringComparison.OrdinalIgnoreCase));

            if (!isNameUsed)
            {
                _bookCategories.PostCategory(newCategoriesBook);
                return RedirectToAction("Index", "BookCategoriesManagement");
            }
            else
            {
                ViewBag.msg = "Name is already used";
            }
        }
        return View("~/Views/Admin/BookCategories/Create.cshtml");
    }

    
    [AuthenticationAdmin]
    [HttpGet("edit")]
    public IActionResult Edit(int id)
    {
        var cate = _bookCategories.GetCategory(id);

        return View("~/Views/Admin/BookCategories/Edit.cshtml", cate);
    }

    
    [HttpPost("edit")]
    public IActionResult Edit(int id, CategoriesBook categoriesBook)
    {
        if (ModelState.IsValid)
        {
            var allCategories = _bookCategories.GetAllCategories();
            bool isNameUsed = allCategories.Any(c =>
                c.CategoryBookId != id && 
                c.CategoryName.Equals(categoriesBook.CategoryName, StringComparison.OrdinalIgnoreCase));

            if (!isNameUsed)
            {
                _bookCategories.PutCategory(id, categoriesBook);
                return RedirectToAction("Index", "BookCategoriesManagement");
            }
            else
            {
                TempData["msgEdit"] = "Name is already used";
                return RedirectToAction("Edit", new { id = id });
            }
        }
        return View("~/Views/Admin/BookCategories/Edit.cshtml");
    }
    

    public IActionResult Delete(int id)
    {
        var model = _bookCategories.GetCategory(id);
        if (!_bookCategories.CheckBook(model))
        {
            _bookCategories.DeleteCategory(id);
            return RedirectToAction("Index", "BookCategoriesManagement");
        }
        else
        {
            TempData["msgDelete"] = "Can not delete because there is a related book";
            return RedirectToAction("Index", "BookCategoriesManagement");
        }
    }

    [AuthenticationAdmin]
    [HttpGet("get_recipe_by_cate")]
    public IActionResult GetRecipeByCategory(int id)
    {
        var books = _bookCategories.GetBooksByCategory(id);
        return View("~/Views/Admin/BookCategories/BookByCate.cshtml", books);
    }
}