using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class BookController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/FE/Book/Index.cshtml");
    }
}