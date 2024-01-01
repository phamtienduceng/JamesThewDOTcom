using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class CartController : Controller
{
    private readonly ICart _cart;

    public CartController(ICart cart)
    {
        _cart = cart;
    }

    public IActionResult Index()
    {
        var userJson = HttpContext.Session.GetString("userLogged");

        if (!string.IsNullOrEmpty(userJson))
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);
            var userCart = _cart.GetUserCart(user!.UserId);
            if (userCart != null)
            {
                var cartDetails = _cart.GetCartDetails(userCart.CartId);
                decimal cartTotal = _cart.CalculateCartTotal(userCart.CartId);
                return View("~/Views/FE/Cart/Index.cshtml", cartDetails);
            }
        }
        return View("Error");
    }


    [HttpPost("add_to_cart")]
    public IActionResult AddToCart(int bookId, int quantity = 1)
    {
        var userJson = HttpContext.Session.GetString("userLogged");
        
        if (!string.IsNullOrEmpty(userJson))
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);
            _cart.AddToCart(user.UserId, bookId, quantity);
        }

        return RedirectToAction("Index", "Cart");
    }
}