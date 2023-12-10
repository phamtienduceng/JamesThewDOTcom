using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class RecipeManagementController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Recipe/Index.cshtml");
    }
}