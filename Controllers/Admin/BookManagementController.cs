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
using MailKit.Net.Imap;

namespace JamesRecipes.Controllers.Admin
{
    public class BookManagementController : Controller
    {
        private readonly IBookManagementRepository _repository;
        private readonly JamesrecipesContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public BookManagementController(IBookManagementRepository repository, JamesrecipesContext dataContext, IWebHostEnvironment webHostEnviroment)
        {
            _repository = repository;
            _dataContext = dataContext;
            _webHostEnviroment = webHostEnviroment;
        }

        public IActionResult Index()
        {
            var book = _repository.GetBooks();
            return View("~/Views/Admin/Book/Index.cshtml", book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "CategoryName");
            return View("~/Views/Admin/Book/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookModel book)
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "CategoryName", book.CategoryId);
            if (ModelState.IsValid)
            {
                book.Slug = book.Title.Replace(" ", " - ");
                var slug = _dataContext.Books.FirstOrDefault(b => b.Slug == book.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError(" ", "The book is already in the database");
                    return View(book);
                }
                if (book.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnviroment.WebRootPath, "images");
                    string imageName = Guid.NewGuid().ToString() + "_" + book.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    book.ImageUpload.CopyTo(fs);
                    fs.Close();
                    book.Image = imageName;
                }
                _dataContext.Add(book);
                _dataContext.SaveChanges();
                TempData["success"] = "Add book successfully !!!";
                return RedirectToAction("Index");
            }
            return View("~/Views/Admin/Book/Create.cshtml",book);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            BookModel book = _repository.GetBook(Id);
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "CategoryName", book.CategoryId);
            return View("~/Views/Admin/Book/Edit.cshtml", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, BookModel book)
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "CategoryName", book.CategoryId);
            if (ModelState.IsValid)
            {
                book.Slug = book.Title.Replace(" ", " - ");
                var slug = _dataContext.Books.FirstOrDefault(b => b.Slug == book.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError(" ", "The book is already in the database");
                    return View(book);
                }
                if (book.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnviroment.WebRootPath, "images");
                    string imageName = Guid.NewGuid().ToString() + "_" + book.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    book.ImageUpload.CopyTo(fs);
                    fs.Close();
                    book.Image = imageName;
                }
                _dataContext.Update(book);
                _dataContext.SaveChanges();
                TempData["success"] = "Update book successfully !!!";
                return RedirectToAction("Index");
            }
            return View("~/Views/Admin/Book/Edit.cshtml", book);
        }

        public IActionResult Delete(int Id)
        {
            BookModel book = _dataContext.Books.Find(Id);
            if (!string.Equals(book.Image, "noname.jpg")) 
            {
                string uploadDir = Path.Combine(_webHostEnviroment.WebRootPath, "images");
                string oldfileImage = Path.Combine(uploadDir,book.Image);
                if (System.IO.File.Exists(oldfileImage))
                {
                    System.IO.File.Delete(oldfileImage);
                }
            }
            _dataContext.Remove(book);
            _dataContext.SaveChanges();
            TempData["error"] = "Delete book successfully !!!";
            return RedirectToAction("Index");
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
