using JamesRecipes.Models;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class TipCategoriesManagementController : Controller
{
    private readonly ITipCategoriesManagementRepository _tipCategories;

    public TipCategoriesManagementController(ITipCategoriesManagementRepository tipCategories)
    {
        _tipCategories = tipCategories;
    }

    [AuthenticationAdmin]
    [HttpGet("index")]
    public IActionResult Index()
    {
        var cate = _tipCategories.GetAllCategories();
        return View("~/Views/Admin/TipCategories/Index.cshtml", cate);
    }
    
    [AuthenticationAdmin]
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/TipCategories/Create.cshtml");
    }
    
    [HttpPost("create")]
    public IActionResult Create(CategoriesTip newCategoriesTip)
    {
        if (ModelState.IsValid)
        {
            var model = _tipCategories.GetAllCategories();
            bool isNameUsed = model.Any(c => c.CategoryName.Equals(newCategoriesTip.CategoryName, StringComparison.OrdinalIgnoreCase));

            if (!isNameUsed)
            {
                _tipCategories.PostCategory(newCategoriesTip);
                return RedirectToAction("Index", "TipCategoriesManagement");
            }
            else
            {
                ViewBag.msg = "Name is already used";
            }
        }
        return View("~/Views/Admin/TipCategories/Create.cshtml");
    }

    
    [AuthenticationAdmin]
    [HttpGet("edit")]
    public IActionResult Edit(int id)
    {
        var cate = _tipCategories.GetCategory(id);

        return View("~/Views/Admin/TipCategories/Edit.cshtml", cate);
    }

    
    [HttpPost("edit")]
    public IActionResult Edit(int id, CategoriesTip categoriesRecipe)
    {
        if (ModelState.IsValid)
        {
            var allCategories = _tipCategories.GetAllCategories();
            bool isNameUsed = allCategories.Any(c =>
                c.CategoryTipId != id && 
                c.CategoryName.Equals(categoriesRecipe.CategoryName, StringComparison.OrdinalIgnoreCase));

            if (!isNameUsed)
            {
                _tipCategories.PutCategory(id, categoriesRecipe);
                return RedirectToAction("Index", "TipCategoriesManagement");
            }
            else
            {
                TempData["msgEdit"] = "Name is already used";
                return RedirectToAction("Edit", new { id = id });
            }
        }
        return View("~/Views/Admin/TipCategories/Edit.cshtml");
    }
    

    public IActionResult Delete(int id)
    {
        var model = _tipCategories.GetCategory(id);
        if (!_tipCategories.CheckTip(model))
        {
            _tipCategories.DeleteCategory(id);
            return RedirectToAction("Index", "TipCategoriesManagement");
        }
        else
        {
            TempData["msgDelete"] = "Can not delete because there is a related recipe";
            return RedirectToAction("Index", "TipCategoriesManagement");
        }
    }

    [AuthenticationAdmin]
    [HttpGet("get_recipe_by_cate")]
    public IActionResult GetRecipeByCategory(int id)
    {
        var reps = _tipCategories.GetTipsByCategory(id);
        return View("~/Views/Admin/TipCategories/TipByCate.cshtml", reps);
    }
}