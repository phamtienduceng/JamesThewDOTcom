using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Models;
using Newtonsoft.Json;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Controllers.Admin
{
    public class ContestEntriesController : Controller
    {
        private readonly JamesrecipesContext _context;
        private readonly IRecipe _recipe;

        public ContestEntriesController(JamesrecipesContext context, IRecipe recipe)
        {
            _context = context;
            _recipe = recipe;
        }

        // GET: ContestEntries
        public async Task<IActionResult> Index()
        {
            var jamesrecipesContext = _context.ContestEntries.Include(c => c.Contest).Include(c => c.Recipe).Include(c => c.User);
            return View("~/Views/Admin/ContestEntries/Index.cshtml", await jamesrecipesContext.ToListAsync());
        }

        // GET: ContestEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContestEntries == null)
            {
                return NotFound();
            }

            var contestEntry = await _context.ContestEntries
                .Include(c => c.Contest)
                .Include(c => c.Recipe)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (contestEntry == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/ContestEntries/Details.cshtml", contestEntry);
        }

        // GET: ContestEntries/Create
        public IActionResult Create()
        {
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View("~/Views/Admin/ContestEntries/Create.cshtml");
        }

        // POST: ContestEntries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryId,ContestId,UserId,RecipeId,CreatedAt,Score,Image")] ContestEntry contestEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contestEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", contestEntry.ContestId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", contestEntry.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", contestEntry.UserId);
            return View("~/Views/Admin/ContestEntries/Create.cshtml", contestEntry);
        }


        // GET: ContestEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContestEntries == null)
            {
                return NotFound();
            }

            var contestEntry = await _context.ContestEntries.FindAsync(id);
            if (contestEntry == null)
            {
                return NotFound();
            }
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", contestEntry.ContestId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", contestEntry.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", contestEntry.UserId);
            return View("~/Views/Admin/ContestEntries/Edit.cshtml", contestEntry);
        }

        // POST: ContestEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntryId,ContestId,UserId,RecipeId,CreatedAt,Score,Image")] ContestEntry contestEntry)
        {
            if (id != contestEntry.EntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contestEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContestEntryExists(contestEntry.EntryId))
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
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", contestEntry.ContestId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", contestEntry.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", contestEntry.UserId);
            return View("~/Views/Admin/ContestEntries/Edit.cshtml", contestEntry);
        }

        // GET: ContestEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContestEntries == null)
            {
                return NotFound();
            }

            var contestEntry = await _context.ContestEntries
                .Include(c => c.Contest)
                .Include(c => c.Recipe)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (contestEntry == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/ContestEntries/Delete.cshtml", contestEntry);
        }

        // POST: ContestEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContestEntries == null)
            {
                return Problem("Entity set 'JamesrecipesContext.ContestEntries'  is null.");
            }
            var contestEntry = await _context.ContestEntries.FindAsync(id);
            if (contestEntry != null)
            {
                _context.ContestEntries.Remove(contestEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContestEntryExists(int id)
        {
            return (_context.ContestEntries?.Any(e => e.EntryId == id)).GetValueOrDefault();
        }



        // CreateContestRecipe
        // Đảm bảo _recipe (IRecipe) được tiêm vào constructor của controller
        // GET: ContestEntries/CreateContestRecipe
        [HttpGet]
        public IActionResult CreateContestRecipe(int? contestId)
        {
            // Lấy userId từ session
            string userJson = HttpContext.Session.GetString("userLogged");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToAction("Login", "Account");
            }
            User currentUser = JsonConvert.DeserializeObject<User>(userJson);
            int userId = currentUser.UserId;

            // lấy ContestId từ session 
            string contestIdSession = HttpContext.Session.GetString("contestId");
            if(!string.IsNullOrEmpty(contestIdSession))
            {
                contestId = int.Parse(contestIdSession);
            }
            else if(!contestId.HasValue)
            {
                return NotFound();
            }

            // Giờ bạn có userId, sử dụng nó để lấy recipes
            // Sử dụng phương thức GetRecipesByUser từ interface IRecipe để lấy công thức của người dùng
            var userRecipes = _recipe.GetRecipesByUser(userId);

            ViewData["RecipeSelectList"] = new SelectList(userRecipes, "RecipeId", "Title");
            ViewData["ContestTitle"] = _context.Contests.Find(contestId).Title;
            ViewData["Username"] = _context.Users.Find(userId).Username;

            // Tạo một đối tượng ContestEntry mới và cài đặt các giá trị mặc định
            var contestEntry = new ContestEntry
            {
                ContestId = contestId.HasValue ? contestId.Value : 0, // Kiểm tra contestId có giá trị không trước khi gán
                UserId = userId // userId đã là một int
                                // Cài đặt các thuộc tính khác nếu cần
            };

            ViewBag.RecipeId = new SelectList(userRecipes, "RecipeId", "Title");

            return View("~/Views/fe/Contest/CreateContestRecipe.cshtml", contestEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContestRecipe([Bind("EntryId,ContestId,UserId,RecipeId,CreatedAt,Score,Image")] ContestEntry contestEntry)
        {
            // Lấy userId từ session, ví dụ: string userJson = HttpContext.Session.GetString("userLogged");
            string userJson = HttpContext.Session.GetString("userLogged");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToAction("Login", "Account");
            }
            // Xác nhận người dùng hiện tại có quyền thực hiện hành động này không
            User currentUser = JsonConvert.DeserializeObject<User>(userJson);
            if (currentUser.RoleId == 1)
            {
                return Unauthorized();
            }

            // lấy ContestId từ session
            string contestIdSession = HttpContext.Session.GetString("contestId");
            if (!string.IsNullOrEmpty(contestIdSession))
            {
                contestEntry.ContestId = int.Parse(contestIdSession);
            }
            else if (!contestEntry.ContestId.HasValue)
            {
                return NotFound();
            }

            // Kiểm tra ModelState trước khi thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                // Thêm logic kiểm tra xem contestEntry đã tồn tại hay chưa, nếu cần
                // Nếu contestEntry đã tồn tại, trả về BadRequest
                if (_context.ContestEntries.Any(ce => ce.ContestId == contestEntry.ContestId && ce.UserId == contestEntry.UserId))
                {
                    return BadRequest();
                }
                _context.Add(contestEntry);
                await _context.SaveChangesAsync();

                // Sau khi lưu, chuyển hướng đến trang danh sách cuộc thi hoặc trang chi tiết cuộc thi
                ViewData["ContestTitle"] = _context.Contests.Find(contestEntry.ContestId).Title;
                ViewData["Username"] = _context.Users.Find(contestEntry.UserId).Username;

                return RedirectToAction("index", "home", new { id = contestEntry.ContestId });
            }

            // Nếu ModelState không hợp lệ, lấy lại danh sách công thức của người dùng để hiển thị lại form
            var userRecipes = _recipe.GetRecipesByUser(currentUser.UserId);
            ViewData["RecipeId"] = new SelectList(userRecipes, "RecipeId", "Title", contestEntry.RecipeId);

            // Trả về view với contestEntry và thông báo lỗi nếu có
            return View("~/Views/fe/Contest/CreateContestRecipe.cshtml", contestEntry);
        }


    }
}
