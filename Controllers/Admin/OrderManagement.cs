using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

public class OrderManagement : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Order/Index.cshtml");
    }
}