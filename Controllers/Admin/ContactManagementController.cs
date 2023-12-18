using JamesRecipes.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class ContactManagementController : Controller
{
    [AuthenticationAdmin]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Contact/Index.cshtml");
    }
}