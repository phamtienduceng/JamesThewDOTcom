using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class CommentManagementController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Comment/Index.cshtml");
    }
}