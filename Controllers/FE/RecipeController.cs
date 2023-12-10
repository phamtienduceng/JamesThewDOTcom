using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class RecipeController : Controller
{
    // GET: /FE/Recipe/Index
    public IActionResult Index()
    {
        return View("~/Views/FE/Recipe/Index.cshtml");
    }
}
