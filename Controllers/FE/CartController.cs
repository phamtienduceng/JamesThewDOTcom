using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class CartController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/FE/Cart/GetUserCart.cshtml");
    }
}