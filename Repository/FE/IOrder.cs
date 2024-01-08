using JamesRecipes.Models.Book;

namespace JamesRecipes.Repository.FE
{
    public interface IOrder
    {
        void ProcessOrder(int userId, List<CartItem> cartItems);
    }
}
