using JamesRecipes.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace JamesRecipes.Controllers.FE 
{ 
    public class StoreController : Controller
    {
        private readonly JamesrecipesContext _context;

        public StoreController(JamesrecipesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string minPrice, string maxPrice)
        {
            var books = _context.Books.Select(b => b);
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(minPrice))
            {
                var min = int.Parse(minPrice);
                books = books.Where(b => b.Price >= min);
            }
            if (!string.IsNullOrEmpty(maxPrice))
            {
                var max = int.Parse(maxPrice);
                books = books.Where(b => b.Price <= max);
            }
            return View("~/Views/FE/Store/Index.cshtml",await books.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View("~/Views/FE/Store/Details.cshtml",book);
        }
    }
}
