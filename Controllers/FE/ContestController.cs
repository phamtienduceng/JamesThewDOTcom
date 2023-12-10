using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class ContestController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/FE/Contest/Index.cshtml");
    }
}