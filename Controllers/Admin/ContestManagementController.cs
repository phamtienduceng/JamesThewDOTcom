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
    private readonly JamesrecipesContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;

    // Constructor
    public ContestManagementController(JamesrecipesContext db, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IContestManagementRepository contestManagementRepository)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _webHostEnvironment = webHostEnvironment;
        _contestManagementRepository = contestManagementRepository;
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
                // Chỉ cần lưu đường dẫn từ sau 'wwwroot'
                var folderPath = Path.Combine("wwwroot", "Admin", "images", "contest");
                var pathToSave = Path.Combine(folderPath, file.FileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (var stream = new FileStream(pathToSave, FileMode.Create))
                {
                    // Sử dụng phương thức đồng bộ khi lưu file
                    file.CopyTo(stream);
                }
                // Lưu đường dẫn tương đối vào cơ sở dữ liệu
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
    [HttpGet("Edit")]
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
    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Contest contest, IFormFile file)
    {
        if (id != contest.ContestId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
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
                }

                _contestManagementRepository.UpdateContest(contest);
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
    [HttpGet("Details")]
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