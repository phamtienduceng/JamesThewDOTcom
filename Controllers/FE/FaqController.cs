using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class FaqController : Controller
{
    private readonly IFaq _faq;

    public FaqController(IFaq faq)
    {
        _faq = faq;
    }

    public IActionResult Index()
    {
        var faq = _faq.GetAllFaqs();
        return View("~/Views/FE/Faq/Index.cshtml", faq);
    }
}