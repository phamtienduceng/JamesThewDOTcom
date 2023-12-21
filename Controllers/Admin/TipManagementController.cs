using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class TipManagementController : Controller
{
    private readonly ITipManagementRepository _tipManagement;

    public TipManagementController(ITipManagementRepository tipManagement)
    {
        _tipManagement = tipManagement;
    }

    [AuthenticationAdmin]
    [HttpGet]
    public IActionResult Index()
    {
        var tips = _tipManagement.GetAllTips();
        return View("~/Views/Admin/Tip/Index.cshtml", tips);
    }
    
    [HttpGet("get_recipe")]
    public IActionResult GetTip(int id)
    {
        var tip = _tipManagement.GetTip(id);
        return View("~/Views/Admin/Tip/SingleTip.cshtml", tip);
    }
}