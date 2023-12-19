using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using System.Diagnostics;

namespace JamesRecipes.Controllers.Admin
{
    public class BookManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookManagementRepository _bookRepository;

        public BookManagementController(ILogger<HomeController> logger, IBookManagementRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
        {
            IEnumerable<Book> books = await _bookRepository.GetBooks(sterm, genreId);
            IEnumerable<Genre> genres = await _bookRepository.Genres();
            BookDisplayModel bookModel = new BookDisplayModel
            {
                Books = books,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId
            };
            return View(bookModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
