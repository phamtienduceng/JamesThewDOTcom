using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHome _home;

    public HomeController(ILogger<HomeController> logger, IHome home)
    {
        _logger = logger;
        _home = home;
    }

    public IActionResult Index()
    {
        var model = _home.GetHomepages();
        return View("~/Views/Home/Index.cshtml", model);;
    }
    
}