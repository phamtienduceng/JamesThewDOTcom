using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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
    public IActionResult Index(string sortOrder, string searchString, int page = 1)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";
        ViewData["CurrentFilter"] = searchString;
        
        var tips = _tipManagement.GetAllTip();
        
        if (!string.IsNullOrEmpty(searchString))
        {
            tips = _tipManagement.Search(searchString);
        }
        
        tips = _tipManagement.Sorting(tips, sortOrder);
        
        var ts = _tipManagement.PagedList(page, 10, tips);
        return View("~/Views/Admin/Tip/Index.cshtml", ts);
    }
    
    [AuthenticationAdmin]
    [HttpGet("get_tip")]
    public IActionResult GetTip(int id)
    {
        var tip = _tipManagement.GetTip(id);
        return View("~/Views/Admin/Tip/SingleTIp.cshtml", tip);
    }
    
    [AuthenticationAdmin]
    [HttpGet("export_tip_to_excel")]
    public FileResult ExportToExcel()
    {
        var tips = _tipManagement.GetAllTip();
        var fileName = "tips.xlsx";
        byte[] excelData = _tipManagement.GeneratedExcel(fileName, tips);

        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}