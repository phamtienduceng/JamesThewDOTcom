using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Models;
using JamesRecipes.Service.Admin;
using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using System.Security.Policy;

namespace JamesRecipes.Controllers.Admin
{
    public class BookManagementController : Controller
    {
        private readonly IBookManagementRepository _repository ;

        public BookManagementController(IBookManagementRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var book = _repository.GetList();
            return View("~/Views/Admin/Book/Index.cshtml", book);
        }

        public IActionResult Details(int id)
        {
            var book = _repository.BookDetail(id);
            if (book == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/Book/Details.cshtml", book);
        }

        // GET: BookManagement/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Book/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book newBook, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    var path = Path.Combine("wwwroot/images", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyToAsync(stream);
                    newBook.Image = "images/" + file.FileName;

                    _repository.CreateBook(newBook);
                    return RedirectToAction("Index");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = _repository.BookDetail(id);
            if (book == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/Book/Edit.cshtml", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    var path = Path.Combine("wwwroot/images", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyToAsync(stream);
                    book.Image = "images/" + file.FileName;
                }

                _repository.EditBook(id, book);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var book = _repository.BookDetail(id);
            if (book == null) 
            {
                return NotFound();
            }
            _repository.DeleteBook(id);
            return RedirectToAction("Index");
        }

    }
}
