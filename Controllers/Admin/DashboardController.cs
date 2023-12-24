using JamesRecipes.Models;
using JamesRecipes.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class DashboardController : Controller
{

    [Authentication]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Dashboard/Index.cshtml");
    }

}