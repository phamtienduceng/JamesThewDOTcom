using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class FaqController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/FE/Faq/Index.cshtml");
    }
}