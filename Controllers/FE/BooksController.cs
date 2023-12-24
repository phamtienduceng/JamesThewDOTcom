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
    public class BooksController : Controller
    {
        private readonly IBookManagementRepository _bookManagementRepository;

        public BooksController(IBookManagementRepository bookManagementRepository)
        {
            _bookManagementRepository = bookManagementRepository;
        }

        public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
        {
            IEnumerable<Book> books = await _bookManagementRepository.GetBooks(sterm, genreId);
            IEnumerable<Genre> genres = await _bookManagementRepository.Genres();
            BookDisplayModel bookModel = new BookDisplayModel
            {
                Books = books,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId
            };
            return View("~/Views/FE/Books/Index.cshtml", bookModel);
        }
    }
}
