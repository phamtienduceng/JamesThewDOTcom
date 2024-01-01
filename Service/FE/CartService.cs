using System.Data.Entity;
using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.FE;

public class CartService: ICart
{
    private readonly JamesrecipesContext _db;

    public CartService(JamesrecipesContext db)
    {
        _db = db;
    }


    public void AddToCart(int userId, int bookId, int quantity)
    {
        var userCart = _db.Carts.FirstOrDefault(c => c.UserId == userId);
        if (userCart == null)
        {
            userCart = new Cart()
            {
                UserId = userId
            };

            _db.Carts.Add(userCart);
            _db.SaveChanges();
        }

        if (CheckBook(bookId, userCart.CartId))
        {
            var existingCartItem = GetCartDetail(bookId, userCart.CartId);
            existingCartItem.Quantity += quantity;
            _db.SaveChanges();
        }
        else
        {
            _db.CartDetails.Add(new CartDetail()
            {
                CartId = userCart.CartId,
                BookId = bookId,
                Quantity = 1,
            });
            _db.SaveChanges();
        }
        
    }

    public decimal CalculateCartTotal(int cartId)
    {
        decimal total = (decimal) _db.CartDetails
            .Where(cd => cd.CartId == cartId)
            .Sum(cd => cd.Quantity * cd.UnitPrice)!;
        return total;
    }

    
    bool CheckBook(int bookId, int cartId)
    {
        return _db.CartDetails.Any(c => c.BookId == bookId && c.CartId == cartId);
    }

    public Cart GetUserCart(int userId)
    {
        return _db.Carts.SingleOrDefault(c => c.UserId == userId)!;
    }
    public List<CartDetail> GetCartDetails(int cartId)
    {
        return _db.CartDetails
            .Include(cd => cd.Book)
            .Where(cd => cd.CartId == cartId)
            .ToList();
    }


    public CartDetail GetCartDetail(int bookId, int cartId)
    {
        return _db.CartDetails.SingleOrDefault(c => c.BookId == bookId && c.CartId == cartId)!;
    }
}