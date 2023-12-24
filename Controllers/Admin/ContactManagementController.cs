using JamesRecipes.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class ContactManagementController : Controller
{
    private readonly IEmailSender _emailSender;

    public ContactManagementController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Contact/Index.cshtml");
    }

    [HttpPost]
    public async  Task<IActionResult> Index(Contact contact)
    {
        var msg = contact.Name + " " + contact.Massage;
        await _emailSender.SendEmailAsync(contact.Email,"Contact Mail",contact.Massage);
        ViewBag.ConfirmMsg = "Thanks For Your Mail";
        return View("~/Views/Admin/Contact/Index.cshtml");
    }
}