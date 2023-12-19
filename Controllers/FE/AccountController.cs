using JamesRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Security.Policy;


namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class AccountController : Controller
{
    JamesrecipesContext _db;

    public AccountController(JamesrecipesContext db)
    {
        _db = db;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View("~/Views/FE/Account/Login.cshtml");
    }

    [HttpPost("login")]
    public IActionResult Login(string email, string pass)
    {
        var user = _db.Users.SingleOrDefault(u => u.Email.Equals(email));
        try
        {
            if (user != null && user.Password.Equals(pass) && user.RoleId == 2)
            {
                var userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("user", userJson);
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return View();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View("~/Views/FE/Account/Register.cshtml");
    }

    [HttpPost("register")]
    public IActionResult Register(User newUsers)
    {
        if (ModelState.IsValid)
        {
            var existingUser = _db.Users.SingleOrDefault(u => u.Email.Equals(newUsers.Email));

            if (existingUser == null)
            {
                newUsers.RoleId = 2;
                _db.Users.Add(newUsers);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email is already registered.");
            }
        }

        return View("~/Views/Home/Index.cshtml");
    }

    [HttpGet("MyProfile/{id}")]
    public IActionResult MyProfile(int id)
    {
        var acc = _db.Users.SingleOrDefault(u => u.UserId == id);
        var model = new User
        {
            UserId = acc.UserId,
            Username = acc.Username,
            PhoneNumber = acc.PhoneNumber,
            Address = acc.Address
        };

        return View("~/Views/FE/Account/MyProfile.cshtml", model);
    }

    [HttpPost("MyProfile")]
    public IActionResult MyProfile(User model)
    {
        var ac = _db.Users.Find(model.UserId);
        if (ac != null)
        {
            ac.Username = model.Username;
            ac.PhoneNumber = model.PhoneNumber;
            ac.Address = model.Address;
            _db.SaveChanges();
        }
        return View("~/Views/FE/Account/MyProfile.cshtml", model);
    }

        [HttpGet("changepassword")]
    public IActionResult ChangePassword()
    {
        return View("~/Views/FE/Account/ChangePassword.cshtml");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("user");
       return View("~/Views/Home/Index.cshtml");
    }

}