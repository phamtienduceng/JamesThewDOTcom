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

namespace JamesRecipes.Controllers.Admin
{
    public class BooksController : Controller
    {
        private readonly JamesrecipesContext _dataContext;
        private readonly ILogger<BooksController> _logger;

		public BooksController(JamesrecipesContext dataContext, ILogger<BooksController> logger)
		{
			_dataContext = dataContext;
			_logger = logger;
		}

		public IActionResult Index()
        {
            var book = _dataContext.Books.ToList();
            return View("~/Views/FE/Book/Book.cshtml",book);
        }
    }
}
