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
    
    [AuthenticationAdmin]
    [HttpGet("get_recipe")]
    public IActionResult GetRecipe(int id)
    {
        var rep = _recipeManagement.GetRecipe(id);
        return View("~/Views/Admin/Recipe/SingleRecipe.cshtml", rep);
    }
    
    [AuthenticationAdmin]
    [HttpGet("export_to_excel")]
    public FileResult ExportToExcel()
    {
        var recipes = _recipeManagement.GetAllRecipes();
        var fileName = "recipes.xlsx";
        byte[] excelData = _recipeManagement.GeneratedExcel(fileName, recipes);

        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}