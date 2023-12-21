using JamesRecipes.Models;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Controllers.Admin;

// Controller for managing contests
[Route("admin/[controller]")]
public class ContestManagementController : Controller
{
    // Fields
    private readonly JamesrecipesContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    // Constructor
    public ContestManagementController(JamesrecipesContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET: Admin/ContestManagement
    [AuthenticationAdmin]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var contests = await _db.Contests.ToListAsync();
        return View("~/Views/Admin/Contest/Index.cshtml", contests);
    }

    // GET: Admin/ContestManagement/Details/5
    [AuthenticationAdmin]
    [HttpGet]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Contest/Create.cshtml");
    }

    // POST: Admin/ContestManagement/Create
    [AuthenticationAdmin]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Contest contest)
    {
        if (ModelState.IsValid)
        {
            contest.CreatedAt = DateTime.Now;
            _db.Contests.Add(contest);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View("~/Views/Admin/Contest/Create.cshtml", contest);
    }

    // GET: Admin/ContestManagement/Edit/5
    [AuthenticationAdmin]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var contest = await _db.Contests.FindAsync(id);
        if (contest == null)
        {
            return NotFound();
        }
        return View("~/Views/Admin/Contest/Edit.cshtml", contest);
    }

    // POST: Admin/ContestManagement/Edit/5
    [AuthenticationAdmin]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Contest contest)
    {
        if (id != contest.ContestId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(contest);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ContestExists(contest.ContestId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View("~/Views/Admin/Contest/Edit.cshtml", contest);
    }

    // GET: Admin/ContestManagement/Delete/5
    [AuthenticationAdmin]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var contest = await _db.Contests.FindAsync(id);
        _db.Contests.Remove(contest!);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    private async Task<bool> ContestExists(int id)
    {
        return await _db.Contests.AnyAsync(c => c.ContestId == id);
    }

    // GET: Admin/ContestManagement/Details/5
    [AuthenticationAdmin]
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var contest = await _db.Contests
            .Include(c => c.Announcements) // hiển thị thông tin liên quan đến Announcements
            .Include(c => c.ContestEntries) // hiển thị thông tin liên quan đến ContestEntries
            .FirstOrDefaultAsync(m => m.ContestId == id);

        if (contest == null)
        {
            return NotFound();
        }

        return View("~/Views/Admin/Contest/Details.cshtml", contest);
    }


}