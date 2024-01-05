using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class ContactController : Controller
{
    private readonly IContact _contact;

    public ContactController(IContact contact)
    {
        _contact = contact;
    }

    public IActionResult Index()
    {
        return View("~/Views/FE/Contact/Index.cshtml");
    }

    [HttpPost]
    public IActionResult SendContact(Contact newContact)
    {
        var userJson = HttpContext.Session.GetString("userLogged");
        if (ModelState.IsValid)
        {
            if (!string.IsNullOrEmpty(userJson))
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                newContact.Name = user!.Username;
                newContact.Email = user.Email;
                _contact.SendContact(newContact);
                TempData["ContactSent"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                _contact.SendContact(newContact);
                return RedirectToAction("Index");
            }
        }
        return View("~/Views/FE/Contact/Index.cshtml", newContact);
    }
}