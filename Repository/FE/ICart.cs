using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin
{
    public interface ICart
    {
        Task<int> AddBook(int bookId, int qty);
        Task<int> RemoveBook(int bookId);
        Task<Cart> GetBooks(string userId);
        Task<Cart> EditBook(string userId);
        Task<bool> Checkout();
    }
}
