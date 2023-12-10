using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class TipController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/FE/Tip/Index.cshtml");
    }
}