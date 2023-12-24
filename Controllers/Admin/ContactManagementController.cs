using JamesRecipes.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class ContactManagementController : Controller
{
    [Authentication]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Contact/Index.cshtml");
    }
}