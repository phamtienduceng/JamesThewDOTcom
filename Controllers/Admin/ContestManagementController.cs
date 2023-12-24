using JamesRecipes.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class ContestManagementController : Controller
{
    [Authentication]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Contest/Index.cshtml");
    }
}