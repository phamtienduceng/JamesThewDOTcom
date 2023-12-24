using JamesRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class AccountManagementController : Controller
{
    private readonly JamesrecipesContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountManagementController(JamesrecipesContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }
    [AuthenticationAdmin]
    [HttpGet("Index")]
    public IActionResult Index()
    {
        var users = _db.ViewUserRoles.OrderByDescending(u => u.UserId).ToList();
        return View("~/Views/Admin/AccountManagement/Index.cshtml", users);
    }
 
    [HttpGet("registeradmin")]
    public IActionResult Registeradmin()
    {
        return View("~/Views/Admin/AccountManagement/Registeradmin.cshtml");
    }

    [HttpPost("registeradmin")]
    public IActionResult RegisterAdmin(User newAdmin)
    {
        if (ModelState.IsValid)
        {
            var existingAdmin = _db.Users.SingleOrDefault(a => a.Email.Equals(newAdmin.Email));

            if (existingAdmin == null)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newAdmin.Password);
                newAdmin.Password = hashedPassword;
                newAdmin.RoleId = 1;
                _db.Users.Add(newAdmin);
                _db.SaveChanges();
                return RedirectToAction("Index", "AccountManagement");
            }
            else
            {
                ModelState.AddModelError("", "Email is already registered.");
            }
        }

        return View("~/Views/Admin/AccountManagement/Registeradmin.cshtml");
    }
    public ActionResult LockAccount(int id)
    {
        // L?y thông tin tài kho?n t? ngu?n d? li?u (ví d?: c? s? d? li?u)
        User user = _db.Users.FirstOrDefault(u => u.UserId == id);

        if (user != null)
        {
            user.Avatar = "true";
            _db.SaveChanges();

            return Json(new { success = true });
        }

        return Json(new { success = false });
    }

    [HttpGet("logoutadmin")]
    public IActionResult LogoutAdmin()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }


}