using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class AnnouncementController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/FE/Announcement/Index.cshtml");
    }
}