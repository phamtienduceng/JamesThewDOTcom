using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class ContactManagementController : Controller
{
    private readonly IContactManagementRepository _contact;

    public ContactManagementController(IContactManagementRepository contact)
    {
        _contact = contact;
    }

    [HttpGet]
    public IActionResult Index(int page = 1)
    {
        var cts = _contact.GetAllContact();
        var contacts = _contact.PagedList(page, 9, cts);
        return View("~/Views/Admin/Contact/Index.cshtml", contacts);
    }
    
    [HttpGet("contact_detail")]
    public IActionResult GetContactDetail(int id)
    {
        var ct = _contact.GetContact(id);
        return View("~/Views/Admin/Contact/ContactDetail.cshtml", ct);
    }
    
    [HttpPost("reply_contact")]
    public async Task<IActionResult> SendEMail(ContactViewModel model)
    {
        // Validate input data as needed

        await _contact.SendEmailAsync(model.ToEmail, model.Subject, model.Body);

        // You can redirect or return a response as needed
        return RedirectToAction("Index");
    }
}