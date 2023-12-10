using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class AccountController : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View("~/Views/FE/Account/Login.cshtml");
    }
    
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View("~/Views/FE/Account/Register.cshtml");
    }
    
    [HttpGet("myprofile")]
    public IActionResult MyProfile()
    {
        return View("~/Views/FE/Account/MyProfile.cshtml");
    }
    
    [HttpGet("changepassword")]
    public IActionResult ChangePassword()
    {
        return View("~/Views/FE/Account/ChangePassword.cshtml");
    }
    

}