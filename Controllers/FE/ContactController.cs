using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class ContactController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/FE/Contact/Index.cshtml");
    }
}