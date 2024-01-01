using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface ICart
{
    void AddToCart(int userId, int bookId, int quantity);

    Cart GetUserCart(int userId);

    List<CartDetail> GetCartDetails(int cartId);

    CartDetail GetCartDetail(int bookId, int cartId);

    decimal CalculateCartTotal(int cartId);
}