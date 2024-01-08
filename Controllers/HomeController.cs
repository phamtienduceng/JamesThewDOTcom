using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Controllers;

public class HomeController : Controller
{
    private readonly IRecipe _recipe;
    private readonly ITip _tips;
    private readonly IBook _book;

    public HomeController(IRecipe recipe, ITip tips, IBook book)
    {
        _recipe = recipe;
        _tips = tips;
        _book = book;
    }

    public IActionResult Index()
    {
        var reps = _recipe.GetAllRecipes();
        var tips = _tips.GetAllTips();
        var books = _book.GetAllBooks();

        var viewModel = new Home
        {
            Recipes = reps,
            Tips = tips,
            Books = books
        };
        return View("~/Views/Home/Index.cshtml", viewModel);
    }

}