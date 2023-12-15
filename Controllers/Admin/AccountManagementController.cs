using JamesRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class AccountManagementController : Controller
{
    JamesrecipesContext _db;

    public AccountManagementController(JamesrecipesContext db)
    {
        _db = db;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Account/Index.cshtml");
    }
    [HttpGet("loginadmin")]
    public IActionResult LoginAdmin()
    {
        return View("~/Views/Admin/Account/LoginAdmin.cshtml");
    }

    [HttpPost("loginadmin")]
    public IActionResult LoginAdmin(string email, string pass)
    {
        var admin = _db.Users.FirstOrDefault(u => u.Email.Equals(email) && u.RoleId == 1);

        if (admin != null && admin.Password.Equals(pass))
        {
            var adminJson = JsonConvert.SerializeObject(admin);
            HttpContext.Session.SetString("admin", adminJson);
            return RedirectToAction("Index", "Dashboard");
        }

        return RedirectToAction("Error");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("admin");
        return View("~/Views/Admin/AccountManagement/LoginAdmin.cshtml");
    }

}