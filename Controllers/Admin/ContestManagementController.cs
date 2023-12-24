using JamesRecipes.Models;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Controllers.Admin;

// Controller for managing contests
[Route("admin/[controller]")]
public class ContestManagementController : Controller
{
    // Fields
    private readonly IContestManagementRepository _contestManagementRepository;
    
    // Constructor
    public ContestManagementController(IContestManagementRepository contestManagementRepository)
    {
        _contestManagementRepository = contestManagementRepository;
    }

    // GET: Admin/ContestManagement
    [AuthenticationAdmin]
    [HttpGet("Index")]
    public IActionResult Index()
    {
        var contests = _contestManagementRepository.GetContests();
        return View("~/Views/Admin/Contest/Index.cshtml", contests);
    }

    // GET: Admin/ContestManagement/Details/5
    [AuthenticationAdmin]
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Contest/Create.cshtml");
    }

    // POST: Admin/ContestManagement/Create
    [AuthenticationAdmin]
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Contest contest, IFormFile file)
    {
        try
        {
            if (file != null)
            {
                var folderPath = Path.Combine("wwwroot", "Admin", "images", "contest");
                var pathToSave = Path.Combine(folderPath, file.FileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (var stream = new FileStream(pathToSave, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                contest.Image = Path.Combine("Admin", "images", "contest", file.FileName);
                _contestManagementRepository.AddContest(contest);
                return RedirectToAction("Index");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        ViewBag.CategoryId = new SelectList(_contestManagementRepository.GetContests(), "ContestId", "ContestName");
        return View("~/Views/Admin/Contest/Create.cshtml", contest);
    }

    // GET: Admin/ContestManagement/Edit/5
    [AuthenticationAdmin]
    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var contest = _contestManagementRepository.GetContest(id);
        if (contest == null)
        {
            return NotFound();
        }
        return View("~/Views/Admin/Contest/Edit.cshtml", contest);
    }

    // POST: Admin/ContestManagement/Edit/5
    [AuthenticationAdmin]
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Contest contest, IFormFile file)
    {
        if (id != contest.ContestId)
        {
            return NotFound();
        }

        try
        {
            if (file != null)
            {
                var folderPath = Path.Combine("wwwroot", "Admin", "images", "contest");
                var pathToSave = Path.Combine(folderPath, file.FileName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (var stream = new FileStream(pathToSave, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                contest.Image = Path.Combine("Admin", "images", "contest", file.FileName);
            }

            _contestManagementRepository.UpdateContest(id, contest);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // POST: Admin/ContestManagement/Delete/5
    public IActionResult DeleteConfirmed(int id)
    {
        var contest = _contestManagementRepository.GetContest(id);
        if (!_contestManagementRepository.CheckContest(contest))
        {
            _contestManagementRepository.DeleteContest(id);
            TempData["msgDelete"] = "Deleted Successfully!"; // Thêm dòng này
            return RedirectToAction("Index");
        }
        else
        {
            TempData["msgDelete"] = "Contest is already used";
            return RedirectToAction("Index");
        }
    }

    [HttpGet("Details/{id}")]
    public IActionResult Details(int id)
    {
        var contest = _contestManagementRepository.GetContest(id);
        if(contest == null)
        {
            return NotFound();
        }
        return View("~/Views/Admin/Contest/Details.cshtml", contest);
    }


    [HttpGet("get_contestentries_by_contest")]
    public IActionResult GetContestEntriesByContest(int id)
    {
        var contestEntries = _contestManagementRepository.GetAllContestEntries(id);
        return View("~/Views/Admin/Contest/ContestEntries.cshtml", contestEntries);
    }
}