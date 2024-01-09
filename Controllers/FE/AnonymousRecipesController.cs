using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Models;
using JamesRecipes.Models;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace JamesRecipes.Controllers.FE
{
    public class AnonymousRecipesController : Controller
    {
        private readonly JamesrecipesContext _context;

        public AnonymousRecipesController(JamesrecipesContext context)
        {
            _context = context;
        }

        // GET: AnonymousRecipes
        public async Task<IActionResult> Index()
        {
            // Chỉ cần lấy danh sách AnonymousRecipes, không cần Include Contest
            var jamesrecipesContext = _context.AnonymousRecipes;
            return View(await jamesrecipesContext.ToListAsync());
        }


        // GET: AnonymousRecipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnonymousRecipes == null)
            {
                return NotFound();
            }

            var anonymousRecipe = await _context.AnonymousRecipes.FirstOrDefaultAsync(m => m.AnonymousRecipeId == id);
            if (anonymousRecipe == null)
            {
                return NotFound();
            }

            return View(anonymousRecipe);
        }


        [HttpGet]
        public IActionResult Create(int contestId)
        {
            // Bạn có thể muốn kiểm tra xem contestId có tồn tại trong DB không trước khi tạo AnonymousRecipe
            var contestExists = _context.Contests.Any(c => c.ContestId == contestId);
            if (!contestExists)
            {
                // Xử lý trường hợp contestId không hợp lệ
                return NotFound(); // Hoặc một View khác với thông báo lỗi
            }

            var anonymousRecipe = new AnonymousRecipe
            {
                ContestId = contestId
            };

            return View(anonymousRecipe);
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Ingredients,Procedure,Image,Timeneeds,VideoUrl,ContactEmail,ContactPhone,ContestId,AnonymousName")] AnonymousRecipe anonymousRecipe)
        {
            // Bạn không cần bind AnonymousRecipeId và AnonymousId vì chúng sẽ được tạo tự động
            if (ModelState.IsValid)
            {
                anonymousRecipe.AnonymousId = Guid.NewGuid(); // Tạo AnonymousId mới
                _context.Add(anonymousRecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Chuyển hướng đến action Index
            }

            // Nếu ModelState không hợp lệ, hiển thị lại form với thông tin đã nhập
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "Title", anonymousRecipe.ContestId);
            return View(anonymousRecipe);
        }



        // GET: AnonymousRecipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnonymousRecipes == null)
            {
                return NotFound();
            }

            var anonymousRecipe = await _context.AnonymousRecipes.FindAsync(id);
            if (anonymousRecipe == null)
            {
                return NotFound();
            }
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "Title", anonymousRecipe.ContestId);
            return View(anonymousRecipe);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnonymousRecipeId,AnonymousId,Title,Ingredients,Procedure,CreatedAt,Image,Timeneeds,VideoUrl,ContactEmail,ContactPhone,ContestId,AnonymousName")] AnonymousRecipe anonymousRecipe)
        {
            if (id != anonymousRecipe.AnonymousRecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anonymousRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnonymousRecipeExists(anonymousRecipe.AnonymousRecipeId))
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
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", anonymousRecipe.ContestId);
            return View(anonymousRecipe);
        }

        // GET: AnonymousRecipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnonymousRecipes == null)
            {
                return NotFound();
            }

            var anonymousRecipe = await _context.AnonymousRecipes.FirstOrDefaultAsync(m => m.AnonymousRecipeId == id);
            if (anonymousRecipe == null)
            {
                return NotFound();
            }

            return View(anonymousRecipe);
        }

        // POST: AnonymousRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnonymousRecipes == null)
            {
                return Problem("Entity set 'JamesrecipesContext.AnonymousRecipes'  is null.");
            }
            var anonymousRecipe = await _context.AnonymousRecipes.FindAsync(id);
            if (anonymousRecipe != null)
            {
                _context.AnonymousRecipes.Remove(anonymousRecipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnonymousRecipeExists(int id)
        {
            return (_context.AnonymousRecipes?.Any(e => e.AnonymousRecipeId == id)).GetValueOrDefault();
        }

        // FECreate
        [HttpGet]
        public IActionResult FECreate(int contestId)
        {
            // Bạn có thể muốn kiểm tra xem contestId có tồn tại trong DB không trước khi tạo AnonymousRecipe
            var contestExists = _context.Contests.Any(c => c.ContestId == contestId);
            if (!contestExists)
            {
                // Xử lý trường hợp contestId không hợp lệ
                return NotFound(); // Hoặc một View khác với thông báo lỗi
            }

            var anonymousRecipe = new AnonymousRecipe
            {
                ContestId = contestId
            };

            return View("~/Views/AnonymousRecipes/FECreate.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> FECreate([Bind("Title,Ingredients,Procedure,Image,Timeneeds,VideoUrl,ContactEmail,ContactPhone,ContestId,AnonymousName")] AnonymousRecipe anonymousRecipe)
        {
            // Bạn không cần bind AnonymousRecipeId và AnonymousId vì chúng sẽ được tạo tự động
            if (ModelState.IsValid)
            {
                anonymousRecipe.AnonymousId = Guid.NewGuid(); // Tạo AnonymousId mới
                _context.Add(anonymousRecipe);
                await _context.SaveChangesAsync();
                //return View("~/Views/AnonymousContestEntries/Create.cshtml");
                return RedirectToAction("Create", "AnonymousContestEntries"); // Chuyển hướng đến action Create cua AnonymousContestEntries
            }

            // Nếu ModelState không hợp lệ, hiển thị lại form với thông tin đã nhập
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "Title", anonymousRecipe.ContestId);
            return View("~/Views/AnonymousRecipes/FECreate.cshtml");
        }
    }
}
