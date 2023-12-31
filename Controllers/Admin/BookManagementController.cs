using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Service.Admin;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using System.Security.Policy;
using Humanizer.Localisation;
using JamesRecipes.Models.Book;

namespace JamesRecipes.Controllers.Admin
{
    public class BookManagementController : Controller
    {
        private readonly IBookManagementRepository _repository;

        public BookManagementController(IBookManagementRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var books = _repository.GetBooks();
            return View("~/Views/Admin/Book/Index.cshtml", books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Book/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Book book, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    var folderPath = Path.Combine("wwwroot/images");
                    var pathToSave = Path.Combine(folderPath, file.FileName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (var stream = new FileStream(pathToSave, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    book.Image = Path.Combine("images", file.FileName);
                    _repository.AddBook(book);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            ViewBag.CategoryBookId = new SelectList(_repository.GetBooks(), "CategoryBookId", "CategoryName");
            return View("~/Views/Admin/Book/Create.cshtml", book);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/Book/Edit.cshtml", book);
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(int id, Book book, IFormFile file)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }
            try
            {
                if (file != null)
                {
                    var folderPath = Path.Combine("wwwroot/images");
                    var pathToSave = Path.Combine(folderPath, file.FileName);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (var stream = new FileStream(pathToSave, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    book.Image = Path.Combine("images", file.FileName);
                }

                _repository.UpdateBook(id, book);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IActionResult Delete(int id)
        {
            var model = _repository.GetBook(id);
            if (!_repository.CheckBook(model))
            {
                _repository.DeleteBook(id);
                return RedirectToAction("Index", "BookManagement");
            }
            else
            {
                TempData["msgDelete"] = "Can not delete";
                return RedirectToAction("Index", "BookManagement");
            }
        }

        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/Book/Details.cshtml", book);
        }
    }
}
