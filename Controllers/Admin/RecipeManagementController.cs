using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class RecipeManagementController : Controller
{
    private readonly IRecipeManagementRepository _recipeManagement;

    public RecipeManagementController(IRecipeManagementRepository recipeManagement)
    {
        _recipeManagement = recipeManagement;
    }

    [AuthenticationAdmin]
    [HttpGet("index")]
    public IActionResult Index()
    {
        var reps = _recipeManagement.GetAllRecipes();
        return View("~/Views/Admin/Recipe/Index.cshtml", reps);
    }
    
    [HttpGet("get_recipe")]
    public IActionResult GetRecipe(int id)
    {
        var rep = _recipeManagement.GetRecipe(id);
        return View("~/Views/Admin/Recipe/SingleRecipe.cshtml", rep);
    }
}