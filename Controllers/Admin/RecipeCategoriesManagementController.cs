using JamesRecipes.Models;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class RecipeCategoriesManagementController : Controller
{
    private readonly IRecipeCategoriesManagementRepository _recipeCategories;

    public RecipeCategoriesManagementController(IRecipeCategoriesManagementRepository recipeCategories)
    {
        _recipeCategories = recipeCategories;
    }

    [AuthenticationAdmin]
    [HttpGet("index")]
    public IActionResult Index()
    {
        var cate = _recipeCategories.GetAllCategories();
        return View("~/Views/Admin/RecipeCategories/Index.cshtml", cate);
    }
    
    [AuthenticationAdmin]
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/RecipeCategories/Create.cshtml");
    }
    
    [HttpPost("create")]
    public IActionResult Create(CategoriesRecipe newCategoriesRecipe)
    {
        if (ModelState.IsValid)
        {
            var model = _recipeCategories.GetAllCategories();
            bool isNameUsed = model.Any(c => c.CategoryName.Equals(newCategoriesRecipe.CategoryName, StringComparison.OrdinalIgnoreCase));

            if (!isNameUsed)
            {
                _recipeCategories.PostCategory(newCategoriesRecipe);
                return RedirectToAction("Index", "RecipeCategoriesManagement");
            }
            else
            {
                ViewBag.msg = "Name is already used";
            }
        }
        return View("~/Views/Admin/RecipeCategories/Create.cshtml");
    }

    
    [AuthenticationAdmin]
    [HttpGet("edit")]
    public IActionResult Edit(int id)
    {
        var cate = _recipeCategories.GetCategory(id);

        return View("~/Views/Admin/RecipeCategories/Edit.cshtml", cate);
    }

    
    [HttpPost("edit")]
    public IActionResult Edit(int id, CategoriesRecipe categoriesRecipe)
    {
        if (ModelState.IsValid)
        {
            var allCategories = _recipeCategories.GetAllCategories();
            bool isNameUsed = allCategories.Any(c =>
                c.CategoryRecipeId != id && 
                c.CategoryName.Equals(categoriesRecipe.CategoryName, StringComparison.OrdinalIgnoreCase));

            if (!isNameUsed)
            {
                _recipeCategories.PutCategory(id, categoriesRecipe);
                return RedirectToAction("Index", "RecipeCategoriesManagement");
            }
            else
            {
                TempData["msgEdit"] = "Name is already used";
                return RedirectToAction("Edit", new { id = id });
            }
        }
        return View("~/Views/Admin/RecipeCategories/Edit.cshtml");
    }
    

    public IActionResult Delete(int id)
    {
        var model = _recipeCategories.GetCategory(id);

        if (_recipeCategories.CheckRecipe(model))
        {
            return Json(new { success = false, message = "Can not delete because there is a related recipe" });
            
        }
        else
        {
            _recipeCategories.DeleteCategory(id);
            
            return Json(new { success = true });
        }
    }


    [AuthenticationAdmin]
    [HttpGet("get_recipe_by_cate")]
    public IActionResult GetRecipeByCategory(int id)
    {
        var reps = _recipeCategories.GetRecipeByCategory(id);
        return View("~/Views/Admin/RecipeCategories/RecipeByCate.cshtml", reps);
    }
}