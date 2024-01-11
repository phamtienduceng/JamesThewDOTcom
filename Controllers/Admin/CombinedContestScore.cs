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
using JamesRecipes.Models.ViewModels;

namespace JamesRecipes.Controllers.Admin
{
    public class CombinedContestScoreController : Controller
    {
        private readonly JamesrecipesContext _context;

        public CombinedContestScoreController(JamesrecipesContext context)
        {
            _context = context;
        }

        // GET: CombinedContestScores
        public async Task<IActionResult> Index()
        {
            var combinedScores = await _context.CombinedContestScores
                                  .OrderBy(c => c.ContestId)
                                  .ThenBy(c => c.EntryId)
                                  .ToListAsync();
            return View(combinedScores);
        }

        // GET: CombinedContestScores/Details/5
        public async Task<IActionResult> Details(int? entryId)
        {
            if (entryId == null)
            {
                return NotFound();
            }

            var combinedContestScore = await _context.CombinedContestScores
                .FirstOrDefaultAsync(m => m.EntryId == entryId);

            if (combinedContestScore == null)
            {
                return NotFound();
            }

            return View(combinedContestScore);
        }

    }
}
