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
    public class AnnouncementsManagementController : Controller
    {
        private readonly JamesrecipesContext _context;

        public AnnouncementsManagementController(JamesrecipesContext context)
        {
            _context = context;
        }

        // GET: AnnouncementsManagement
        public async Task<IActionResult> Index()
        {
            var jamesrecipesContext = _context.Announcements.Include(a => a.Contest);
            return View(await jamesrecipesContext.ToListAsync());
        }

        // GET: AnnouncementsManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .Include(a => a.Contest)
                .FirstOrDefaultAsync(m => m.AnnouncementId == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: AnnouncementsManagement/Create
        public IActionResult Create()
        {
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId");
            return View();
        }

        // POST: AnnouncementsManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: AnnouncementsManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnnouncementId,WinnerId,AnonymousWinnerId,ContestId,Content,Title,DatePost,ImageFile")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                if (announcement.ImageFile != null && announcement.ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(announcement.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await announcement.ImageFile.CopyToAsync(fileStream);
                    }

                    // Lưu đường dẫn file vào property Image
                    announcement.Image = fileName; // Bạn có thể muốn thêm đường dẫn tương đối nếu cần
                }

                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", announcement.ContestId);
            return View(announcement);
        }


        // GET: AnnouncementsManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", announcement.ContestId);
            return View(announcement);
        }

        // POST: AnnouncementsManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnnouncementId,WinnerId,AnonymousWinnerId,ContestId,Content,Title,DatePost,Image")] Announcement announcement)
        {
            if (id != announcement.AnnouncementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.AnnouncementId))
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
            ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestId", announcement.ContestId);
            return View(announcement);
        }

        // GET: AnnouncementsManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .Include(a => a.Contest)
                .FirstOrDefaultAsync(m => m.AnnouncementId == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: AnnouncementsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Announcements == null)
            {
                return Problem("Entity set 'JamesrecipesContext.Announcements'  is null.");
            }
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
          return (_context.Announcements?.Any(e => e.AnnouncementId == id)).GetValueOrDefault();
        }

      

        // GET: AnnouncementsManagement
        public async Task<IActionResult> FEIndex()
        {
            var jamesrecipesContext = _context.Announcements.Include(a => a.Contest);
            return View(await jamesrecipesContext.ToListAsync());
        }

        // GET: AnnouncementsManagement/Details/5
        public async Task<IActionResult> FEDetails(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .Include(a => a.Contest)
                .FirstOrDefaultAsync(m => m.AnnouncementId == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }
    }
}
