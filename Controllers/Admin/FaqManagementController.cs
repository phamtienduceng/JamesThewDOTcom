using JamesRecipes.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class FaqManagementController : Controller
{
    [AuthenticationAdmin]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Faq/Index.cshtml");
    }
}