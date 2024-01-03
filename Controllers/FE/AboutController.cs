using JamesRecipes.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/FE/About/Index.cshtml");
    }
}