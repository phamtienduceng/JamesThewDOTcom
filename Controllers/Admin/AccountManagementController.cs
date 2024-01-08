using JamesRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.FE;
using JamesRecipes.Repository.Admin;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class AccountManagementController : Controller
{
    private readonly IAccountManagementRepository _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountManagementController(IAccountManagementRepository accountRepository , IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    [AuthenticationAdmin]
    [HttpGet("Index")]
    public IActionResult Index()
    {
        var users = _accountRepository.GetAllUsers();
        return View("~/Views/Admin/AccountManagement/Index.cshtml", users);
    }
 
    [AuthenticationAdmin]
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
            var Admin = _accountRepository.GetAdminByEmail(newAdmin.Email);

            if (Admin == null)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newAdmin.Password);
                newAdmin.Password = hashedPassword;
                newAdmin.RoleId = 1;
                _accountRepository.AddAdmin(newAdmin);
                return RedirectToAction("Index", "AccountManagement");
            }
            else
            {
                ModelState.AddModelError("", "Email is already registered.");
            }
        }

        return View("~/Views/Admin/AccountManagement/Registeradmin.cshtml");
    }

    [AuthenticationAdmin]
    [HttpGet("logoutadmin")]
    public IActionResult LogoutAdmin()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }
    
}