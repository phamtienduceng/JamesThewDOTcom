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

    [HttpGet("loginadmin")]
    public IActionResult LoginAdmin()
    {
        return View("~/Views/Admin/AccountManagement/LoginAdmin.cshtml");
    }

    [HttpPost("loginadmin")]
    public IActionResult LoginAdmin(string email, string pass)
    {
       

       
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                ViewData["Error"] = "Please enter email and password.";
                return View("~/Views/Admin/AccountManagement/LoginAdmin.cshtml");
            }

            var admin = _db.Users.FirstOrDefault(u => u.Email.Equals(email) && u.RoleId == 1);

            if (admin != null && BCrypt.Net.BCrypt.Verify(pass, admin.Password))
            {
                var adminJson = JsonConvert.SerializeObject(admin);
                _httpContextAccessor.HttpContext.Session.SetString("admin", adminJson);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewData["Error"] = "Wrong email or password!";
                return View("~/Views/Admin/AccountManagement/LoginAdmin.cshtml");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return View("~/Views/Admin/AccountManagement/LoginAdmin.cshtml");
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

    [HttpGet("logoutadmin")]
    public IActionResult LogoutAdmin()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("LoginAdmin", "AccountManagement");
    }


}