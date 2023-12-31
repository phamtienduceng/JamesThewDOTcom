using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace JamesRecipes.Controllers.FE;

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
    
    public IActionResult Index(string sortOrder, string searchString, int? categoryId, int? ratingMin, int? ratingMax, int page = 1)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";
        ViewData["CurrentFilter"] = searchString;

        var userJson = HttpContext.Session.GetString("userLogged");
        
        var tips = _tip.GetAllTips();
        
        if (!string.IsNullOrEmpty(userJson))
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user!.RoleId == 1 || user.RoleId == 3)
            {
                tips = _tip.GetAllTipsPremium();
            }
            
        }   

        if (!string.IsNullOrEmpty(searchString))
        {
            tips = _tip.Search(searchString);
        }
        
        ViewBag.CategoryId = new SelectList(_categoriesTip.GetCategoriesTips(), "CategoryTipId", "CategoryName", categoryId);
        if (categoryId != 0 || ratingMin != 0 || ratingMax != 0)
        {
            tips = _tip.Filter(categoryId, ratingMin, ratingMax, tips);
        }
        tips = _tip.Sorting(tips, sortOrder);
        
        page = page < 1 ? 1 : page;
        var ts = _tip.PageList(page, 9, tips);

        return View("~/Views/FE/Tip/Index.cshtml", ts);
    }
    
    [HttpGet("single_tip")]
    public IActionResult SingleTip(int id)
    {
        var tip = _tip.GetTip(id);
        tip.Feedbacks = _feedback.GetFeedbacksByTipId(id);
        return View("~/Views/FE/Tip/SingleTip.cshtml", tip);
    }
    
    [HttpGet("tip_create")]
    public IActionResult Create()
    {
        ViewBag.CategoryId = new SelectList(_categoriesTip.GetCategoriesTips(), "CategoryTipId", "CategoryName");
        return View("~/Views/FE/Tip/Create.cshtml");
    }

    [HttpPost("tip_create")]
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
    
    [HttpPost("post_tip_comment")]
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
        _tip.UpdateRatingTip(tipId);
        
        var updatedComments = _feedback.GetFeedbacksByTipId(tipId).ToList();
        return PartialView("_CommentsPartial", updatedComments);
    }
    
    [HttpPost("tip_switch_status")]
    public IActionResult SwitchStatus(int id, bool status)
    {
        _tip.SwitchStatus(id, status);
        return RedirectToAction("GetTipsByUser");
    }
    
    [HttpPost("tip_switch_premium")]
    public IActionResult SwitchPremium(int id, bool isPre)
    {
        _tip.PremiumStatus(id, isPre);
        return RedirectToAction("GetTipsByUser");
    }

    public IActionResult GetTipsByUser(int id, string sortOrder, string searchString, int? categoryId, int? ratingMin, int? ratingMax, int page = 1)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";
        ViewData["CurrentFilter"] = searchString;

        var tips = _tip.GetTipsByUser(id);

        if (!string.IsNullOrEmpty(searchString))
        {
            tips = _tip.Search(searchString);
        }
        
        ViewBag.CategoryId = new SelectList(_categoriesTip.GetCategoriesTips(), "CategoryTipId", "CategoryName", categoryId);
        if (categoryId != 0 || ratingMin != 0 || ratingMax != 0)
        {
            tips = _tip.Filter(categoryId, ratingMin, ratingMax, tips);
        }
        tips = _tip.Sorting(tips, sortOrder);
        
        page = page < 1 ? 1 : page;
        var ts = _tip.PageList(page, 9, tips);

        return View("~/Views/FE/Tip/MyTip.cshtml", ts);
    }

    public IActionResult DeleteMyTip(int tipId, int userId)
    {
        _tip.DeleteMyTip(tipId);
        return RedirectToAction("GetTipsByUser", new {id = userId});
    }
    
    [HttpGet("update_tip")]
    public IActionResult UpdateTip(int id)
    {
        ViewBag.CategoryId = new SelectList(_categoriesTip.GetCategoriesTips(), "CategoryTipId", "CategoryName");
        var tip = _tip.GetTip(id);
        return View("~/Views/FE/Tip/Update.cshtml", tip);
    }
    
    [HttpPost("update_tip")]
    public IActionResult UpdateRecipe(int id, Tip tip, IFormFile file)
    {
        var existRecipe = _tip.GetOneTip(id);
        var userId = existRecipe.UserId;

        if (file != null)
        {
            var path = Path.Combine("wwwroot/fe/img", file.FileName);
            var stream = new FileStream(path, FileMode.Create);
            file.CopyToAsync(stream);
            tip.Image = "fe/img/" + file.FileName;
            _tip.UpdateTip(id,tip);
            return RedirectToAction("GetTipsByUser", new{id = userId});
        }
        else
        {
            tip.Image = existRecipe.Image;
            _tip.UpdateTip(id, tip);
            return RedirectToAction("GetTipsByUser", new{id = userId});
        }
    }
    
    [HttpGet("export-tip-to-word/{tipId}")]
    public IActionResult ExportRecipeToWord(int tipId)
    {
        var t = _tip.GetOneTip(tipId);
        var wordDocumentBytes = _tip.GeneratedWord(t);

        return File(wordDocumentBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"Recipe_{tipId}.docx");
    }
}