using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class DashboardController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Dashboard/Index.cshtml");
    }
}