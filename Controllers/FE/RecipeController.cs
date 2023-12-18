using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class RecipeController : Controller
{
    private readonly IRecipe _recipe;
    private readonly ICategoriesRecipe _categoriesRecipe;

    public RecipeController(IRecipe recipe, ICategoriesRecipe categoriesRecipe)
    {
        _recipe = recipe;
        _categoriesRecipe = categoriesRecipe;
    }

    [HttpGet("index")]
    public IActionResult Index()
    {
        return View("~/Views/FE/Recipe/Index.cshtml", _recipe.GetAllRecipes());
    }
    
    [HttpGet("single_recipe")]
    public IActionResult SingleRecipe(int id)
    {
        var rep = _recipe.GetRecipe(id);
        return View("~/Views/FE/Recipe/SingleRecipe.cshtml", rep);
    }
    
    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewBag.CategoryId = new SelectList(_categoriesRecipe.GetCategoriesRecipes(), "CategoryRecipeId", "CategoryName");
        return View("~/Views/FE/Recipe/Create.cshtml");
    }

    [HttpPost("create")]
    public IActionResult Create(Recipe newRecipe, IFormFile file)
    {
        try
        {
            if (file != null)
            {
                var path = Path.Combine("wwwroot/fe/img", file.FileName);
                var stream = new FileStream(path, FileMode.Create);
                file.CopyToAsync(stream);
                newRecipe.Image = "fe/img/" + file.FileName;
                _recipe.PostRecipe(newRecipe);
                return RedirectToAction("Index");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        ViewBag.CategoryId = new SelectList(_categoriesRecipe.GetCategoriesRecipes(), "CategoryRecipeId", "CategoryName");
        return View("~/Views/FE/Recipe/Create.cshtml");
    }
}
