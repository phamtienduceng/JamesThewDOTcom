using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE
{
    public class CartController : Controller
    {
        private readonly JamesrecipesContext _context;
        private readonly Cart _cart;

        public CartController(JamesrecipesContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;

            return View("~/Views/FE/Cart/Index.cshtml",_cart);
        }

        public IActionResult AddToCart(int id)
        {
            var selectedBook = GetBookById(id);

            if (selectedBook != null)
            {
                _cart.AddToCart(selectedBook, 1);
            }

            return RedirectToAction("Index", "Store");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var selectedBook = GetBookById(id);

            if (selectedBook != null)
            {
                _cart.RemoveFromCart(selectedBook);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ReduceQuantity(int id)
        {
            var selectedBook = GetBookById(id);

            if (selectedBook != null)
            {
                _cart.ReduceQuantity(selectedBook);
            }

            return RedirectToAction("Index");
        }

        public IActionResult IncreaseQuantity(int id)
        {
            var selectedBook = GetBookById(id);

            if (selectedBook != null)
            {
                _cart.IncreaseQuantity(selectedBook);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }
    }
}
