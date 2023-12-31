using JamesRecipes.Data;
using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Orders;
using System.Net;

namespace JamesRecipes.Service.Admin
{
    public class CartService : ICart
    {
        private readonly JamesrecipesContext _dbContext;

        public CartService(JamesrecipesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Book GetBookById(int bookId)
        {
            return _dbContext.Books.FirstOrDefault(book => book.BookId == bookId);
        }

        public void UpdateBook(Book book)
        {
            Book existingBook = _dbContext.Books.FirstOrDefault(b => b.BookId == book.BookId);
            if (existingBook != null)
            { }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Quantity = book.Quantity;
            existingBook.Price = book.Price;
            _dbContext.SaveChanges();
        }
    }
}
