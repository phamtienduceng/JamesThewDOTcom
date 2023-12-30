using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace JamesRecipes.Controllers.Admin;

public class RecipeManagementController : Controller
{
    private readonly IRecipeManagementRepository _recipeManagement;

    public RecipeManagementController(IRecipeManagementRepository recipeManagement)
    {
        _recipeManagement = recipeManagement;
    }

    [AuthenticationAdmin]
    public IActionResult Index(int page = 1)
    {
        var reps = _recipeManagement.GetAllRecipes();
        var recipes = _recipeManagement.PagedList(page, 10, reps);
        return View("~/Views/Admin/Recipe/Index.cshtml", recipes);
    }
    
    [HttpGet("get_recipe")]
    public IActionResult GetRecipe(int id)
    {
        var rep = _recipeManagement.GetRecipe(id);
        return View("~/Views/Admin/Recipe/SingleRecipe.cshtml", rep);
    }
}