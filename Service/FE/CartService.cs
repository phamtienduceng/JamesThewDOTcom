using JamesRecipes.Data;
using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Service.Admin
{

    public class CartService : ICart
    {
        private readonly JamesrecipesContext _db;

        public CartService(JamesrecipesContext db)
        {
            _db = db;
        }

        public Task<int> AddBook(int bookId, int qty)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Checkout()
        {
            throw new NotImplementedException();
        }

        public Task<Cart> EditBook(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetBooks(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveBook(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
