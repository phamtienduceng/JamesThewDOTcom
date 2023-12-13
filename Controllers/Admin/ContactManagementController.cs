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
    public IActionResult ContactMail()
    {
        return View();
    }

    [HttpPost]
    public async  Task<IActionResult> ContactMail(Contact contact)
    {
        var msg = contact.Name + " " + contact.Massage;
        await _emailSender.SendEmailAsync(contact.Email,"Contact Mail",contact.Massage);
        ViewBag.ConfirmMsg = "Thanks For Your Mail";
        return View();
    }
}