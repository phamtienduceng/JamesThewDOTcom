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
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Controllers.Admin
{
    public class BooksController : Controller
    {
        private readonly IBook _book;

        public BooksController(IBook book)
        {
            _book = book;
        }

        public IActionResult Index()
        {
            var books = _book.GetBooks();
            return View("~/Views/FE/Books/Index.cshtml", books);
        }
    }
}
