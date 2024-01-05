using JamesRecipes.Models;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

public class FaqManagementController : Controller
{
    private readonly IFaqManagementRepository _faq;

    public FaqManagementController(IFaqManagementRepository faq)
    {
        _faq = faq;
    }

    [AuthenticationAdmin]
    public IActionResult Index(string sortOrder, string searchString, int page = 1)
    {
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["CurrentFilter"] = searchString;
        
        var faqs = _faq.GetAll();
        
        if (!string.IsNullOrEmpty(searchString))
        {
            faqs = _faq.Search(searchString);
        }
        
        faqs = _faq.Sorting(faqs, sortOrder);
        
        var f = _faq.PagedList(page, 10, faqs);
        return View("~/Views/Admin/Faq/Index.cshtml", f);
    }
    
    [HttpGet("get_faq")]
    public IActionResult GetOneFaq(int id)
    {
        var f = _faq.GetOneFaq(id);
        return View("~/Views/Admin/Faq/SingleFaq.cshtml", f);
    }

    [HttpGet("create_faq")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Faq/Create.cshtml");
    }
    
    
    [HttpPost("create_faq")]
    public IActionResult Create(Faq faq)
    {
        if (ModelState.IsValid)
        {
            _faq.CreateFaq(faq);
            return RedirectToAction("Index");
        }
        return View("~/Views/Admin/Faq/Create.cshtml", faq);
    }
    
    [HttpGet("update_faq")]
    public IActionResult Update(int id)
    {
        var f = _faq.GetOneFaq(id);
        return View("~/Views/Admin/Faq/Update.cshtml", f);
    }
    
    
    [HttpPost("update_faq")]
    public IActionResult Update(int id, Faq faq)
    {
        if (ModelState.IsValid)
        {
            _faq.UpdateFaq(id, faq);
            return RedirectToAction("Index");
        }
        return View("~/Views/Admin/Faq/Update.cshtml", faq);
    }

    public IActionResult Delete(int id)
    {
        var f = _faq.GetOneFaq(id);
        if (f != null)
        {
            _faq.DeleteFaq(id);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
}