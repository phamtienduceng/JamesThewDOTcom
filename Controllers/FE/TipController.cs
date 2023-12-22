using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class TipController : Controller
{
    private readonly ITip _tip;
    private readonly ICategoriesTip _categoriesTip;
    private readonly IFeedback _feedback;

    public TipController(ITip tip, ICategoriesTip categoriesTip, IFeedback feedback)
    {
        _tip = tip;
        _categoriesTip = categoriesTip;
        _feedback = feedback;
    }

    [HttpGet("index")]
    public IActionResult Index()
    {
        return View("~/Views/FE/Tip/Index.cshtml", _tip.GetAllTips());
    }
    
    [HttpGet("single_recipe")]
    public IActionResult SingleRecipe(int id)
    {
        var tip = _tip.GetTip(id);
        tip.Feedbacks = _feedback.GetFeedbacksByTipId(id);
        return View("~/Views/FE/Tip/SingleTip.cshtml", tip);
    }
    
    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewBag.CategoryId = new SelectList(_categoriesTip.GetCategoriesTips(), "CategoryTipId", "CategoryName");
        return View("~/Views/FE/Tip/Create.cshtml");
    }

    [HttpPost("create")]
    public IActionResult Create(Tip newTip, IFormFile file)
    {
        try
        {
            if (file != null)
            {
                var path = Path.Combine("wwwroot/fe/img", file.FileName);
                var stream = new FileStream(path, FileMode.Create);
                file.CopyToAsync(stream);
                newTip.Image = "fe/img/" + file.FileName;
                _tip.PostTip(newTip);
                return RedirectToAction("Index");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        ViewBag.CategoryId = new SelectList(_categoriesTip.GetCategoriesTips(), "CategoryTipId", "CategoryName");
        return View("~/Views/FE/Tip/Create.cshtml");
    }
    
    [HttpPost("post_comment")]
    public IActionResult PostFeedback(int tipId, int userId, string content, int rating)
    {
        var newFeedback = new Feedback
        {
            TipId = tipId,
            UserId = userId,
            Content = content,
            Rating = rating,
        };

        _feedback.AddFeedback(newFeedback);
        
        var updatedComments = _feedback.GetFeedbacksByTipId(tipId).ToList();
        return PartialView("_CommentsPartial", updatedComments);
    }
}