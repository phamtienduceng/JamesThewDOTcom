using JamesRecipes.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class AnnounceManagementController : Controller
{
    [Authentication]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Announcement/Index.cshtml");
    }
}