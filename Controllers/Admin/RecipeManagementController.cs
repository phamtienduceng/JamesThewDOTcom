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
    public IActionResult Index(string sortOrder, string searchString, int page = 1)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";
        ViewData["CurrentFilter"] = searchString;
        
        var reps = _recipeManagement.GetAllRecipes();
        
        if (!string.IsNullOrEmpty(searchString))
        {
            reps = _recipeManagement.Search(searchString);
        }
        
        reps = _recipeManagement.Sorting(reps, sortOrder);
        
        var recipes = _recipeManagement.PagedList(page, 10, reps);
        return View("~/Views/Admin/Recipe/Index.cshtml", recipes);
    }
    
    [HttpGet("get_recipe")]
    public IActionResult GetRecipe(int id)
    {
        var rep = _recipeManagement.GetRecipe(id);
        return View("~/Views/Admin/Recipe/SingleRecipe.cshtml", rep);
    }
    
    [HttpPost("admin_switch_status")]
    public IActionResult SwitchStatus(int id, bool status)
    {
        _recipeManagement.SwitchStatus(id, status);
        return RedirectToAction("Index");
    }
    
    [HttpPost("admin_switch_premium")]
    public IActionResult SwitchPremium(int id, bool isPre)
    {
        _recipeManagement.PremiumStatus(id, isPre);
        return RedirectToAction("Index");
    }
}