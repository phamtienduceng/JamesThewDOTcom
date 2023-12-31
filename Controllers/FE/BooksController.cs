using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Repository.Admin;
using System.Diagnostics;
using JamesRecipes.Repository.FE;
using JamesRecipes.Models.Book;
using JamesRecipes.Models.Book.DTOs;

namespace JamesRecipes.Controllers.Admin
{
    public class BooksController : Controller
    {
        private readonly IBook _repository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBook repository, ILogger<BooksController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm = "", int categoryId = 0)
        {
            IEnumerable<Book> books = await _repository.GetBooks(sterm, categoryId);
            IEnumerable<CategoriesBook> categoriesBooks = await _repository.CategoriesBooks();
            ViewBookCate bookModel = new ViewBookCate
            {
                Books = books,
                CategoriesBooks = categoriesBooks,
                STerm = sterm,
                CategoryBookId = categoryId
            };
            return View("~/Views/FE/Books/Index.cshtml",bookModel);
        }
    }
}
