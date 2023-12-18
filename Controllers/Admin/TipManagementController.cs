using JamesRecipes.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class TipManagementController : Controller
{
    [AuthenticationAdmin]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Tip/Index.cshtml");
    }
}