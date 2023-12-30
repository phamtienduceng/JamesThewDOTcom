using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.EntityFramework;
using X.PagedList;

namespace JamesRecipes.Controllers.FE;


public class ContestController : Controller
{
    // Fields
    private readonly IContest _contestRepository;

    // Constructor
    public ContestController(IContest contestRepository)
    {
        _contestRepository = contestRepository;
    }

    // GET: FE/Contest

    public IActionResult Index(string sortOrder, string searchString, DateTime? startDate, DateTime? endDate, int page = 1)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["CurrentFilter"] = searchString;
        ViewData["StartDate"] = startDate;
        ViewData["EndDate"] = endDate;

        var contests = _contestRepository.GetContests();
        if (!string.IsNullOrEmpty(searchString))
        {
            contests = _contestRepository.Search(searchString);
        }
        if (startDate != null && endDate != null)
        {
            contests = _contestRepository.Filter(startDate, endDate, contests);
        }
        contests = _contestRepository.Sorting(contests, sortOrder);

        page = page < 1 ? 1 : page;
        var contest = _contestRepository.PageList(page, 2, contests);
        return View("~/Views/FE/Contest/Index.cshtml", contest);
    }

    private bool UserNeedToLogin()
    {
        // Trả về true nếu người dùng hiện tại chưa được xác thực
        return !User.Identity.IsAuthenticated;
    }

    // GET: FE/Contest/Details/5
    [HttpGet("Details/{id}")]
    public IActionResult Details(int id)
    {
        var contest = _contestRepository.GetContest(id);
        if (contest == null)
        {
            return NotFound();
        }
        // Kiểm tra nếu người dùng cần đăng nhập
        if (UserNeedToLogin())
        {
            HttpContext.Session.SetInt32("LastViewedContestId", id);
        }
        return View("~/Views/FE/Contest/Details.cshtml", contest);
    }
}