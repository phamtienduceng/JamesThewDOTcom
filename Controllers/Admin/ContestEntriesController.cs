using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Models;

namespace JamesRecipes.Controllers.Admin
{
    public class ContestEntriesController : Controller
    {
        private readonly JamesrecipesContext _context;

        public ContestEntriesController(JamesrecipesContext context)
        {
            _context = context;
        }

        // GET: ContestEntries
        public async Task<IActionResult> Index()
        {
            var jamesrecipesContext = _context.ContestEntries.Include(c => c.Contest).Include(c => c.Recipe).Include(c => c.User);
            return View("~/Views/Admin/ContestEntries/Index.cshtml",await jamesrecipesContext.ToListAsync());
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
            return View("~/Views/Admin/ContestEntries/Edit.cshtml",contestEntry);
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

        // POST: ContestEntries/CreateRecipe/5
        [HttpPost("CreateRecipe")]
        public IActionResult CreateRecipe(int contestId, int recipeId)
        {
            var newEntry = new ContestEntry
            {
                ContestId = contestId,
                RecipeId = recipeId,

                CreatedAt = DateTime.Now,
                Score = 0,
                Image = null
            };
            _context.Add(newEntry);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
