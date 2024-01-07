using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Models;
using JamesRecipes.Models;

namespace JamesRecipes.Controllers.Admin
{
    public class AnonymousContestEntriesController : Controller
    {
        private readonly JamesrecipesContext _context;

        public AnonymousContestEntriesController(JamesrecipesContext context)
        {
            _context = context;
        }

        // GET: AnonymousContestEntries
        public async Task<IActionResult> Index()
        {
            var jamesrecipesContext = _context.AnonymousContestEntries;
            return View(await jamesrecipesContext.ToListAsync());
        }

        // GET: AnonymousContestEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnonymousContestEntries == null)
            {
                return NotFound();
            }

            var anonymousContestEntry = await _context.AnonymousContestEntries.FirstOrDefaultAsync(m => m.AnonymousEntryId == id);
            if (anonymousContestEntry == null)
            {
                return NotFound();
            }

            return View(anonymousContestEntry);
        }

        // GET: AnonymousContestEntries/Create
        public IActionResult Create()
        {
            ViewData["AnonymousRecipeId"] = new SelectList(_context.AnonymousRecipes, "AnonymousRecipeId", "AnonymousRecipeId");
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId");
            return View();
        }

        // POST: AnonymousContestEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnonymousEntryId,ContestId,AnonymousRecipeId,CreatedAt,Score,Image")] AnonymousContestEntry anonymousContestEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anonymousContestEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnonymousRecipeId"] = new SelectList(_context.AnonymousRecipes, "AnonymousRecipeId", "AnonymousRecipeId", anonymousContestEntry.AnonymousRecipeId);
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", anonymousContestEntry.ContestId);
            return View(anonymousContestEntry);
        }

        // GET: AnonymousContestEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnonymousContestEntries == null)
            {
                return NotFound();
            }

            var anonymousContestEntry = await _context.AnonymousContestEntries.FindAsync(id);
            if (anonymousContestEntry == null)
            {
                return NotFound();
            }
            ViewData["AnonymousRecipeId"] = new SelectList(_context.AnonymousRecipes, "AnonymousRecipeId", "AnonymousRecipeId", anonymousContestEntry.AnonymousRecipeId);
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", anonymousContestEntry.ContestId);
            return View(anonymousContestEntry);
        }

        // POST: AnonymousContestEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnonymousEntryId,ContestId,AnonymousRecipeId,CreatedAt,Score,Image")] AnonymousContestEntry anonymousContestEntry)
        {
            if (id != anonymousContestEntry.AnonymousEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anonymousContestEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnonymousContestEntryExists(anonymousContestEntry.AnonymousEntryId))
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
            ViewData["AnonymousRecipeId"] = new SelectList(_context.AnonymousRecipes, "AnonymousRecipeId", "AnonymousRecipeId", anonymousContestEntry.AnonymousRecipeId);
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", anonymousContestEntry.ContestId);
            return View(anonymousContestEntry);
        }

        // GET: AnonymousContestEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnonymousContestEntries == null)
            {
                return NotFound();
            }

            var anonymousContestEntry = await _context.AnonymousContestEntries.FirstOrDefaultAsync(m => m.AnonymousEntryId == id);
            if (anonymousContestEntry == null)
            {
                return NotFound();
            }

            return View(anonymousContestEntry);
        }

        // POST: AnonymousContestEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnonymousContestEntries == null)
            {
                return Problem("Entity set 'JamesrecipesContext.AnonymousContestEntries'  is null.");
            }
            var anonymousContestEntry = await _context.AnonymousContestEntries.FindAsync(id);
            if (anonymousContestEntry != null)
            {
                _context.AnonymousContestEntries.Remove(anonymousContestEntry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnonymousContestEntryExists(int id)
        {
          return (_context.AnonymousContestEntries?.Any(e => e.AnonymousEntryId == id)).GetValueOrDefault();
        }
    }
}
