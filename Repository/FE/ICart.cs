using JamesRecipes.Models.Book;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Repository.FE
{
    public interface ICart
    {
        List<CartItem> GetCartItems();
        void ClearCart();
        void SaveCartSession(List<CartItem> ls);
    }
}
