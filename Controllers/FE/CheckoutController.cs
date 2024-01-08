using JamesRecipes.Data.Helper;
using JamesRecipes.Models.Book;
using JamesRecipes.Service.Admin;
using JamesRecipes.Service.FE;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JamesRecipes.Controllers.FE
{
    public class CheckoutController : Controller
    {
        private readonly JamesrecipesContext _dataContext;

        public CheckoutController(JamesrecipesContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Checkout() 
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail != null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var orderCode = Guid.NewGuid().ToString();
                var orderItem = new Order();
                orderItem.OrderCode = orderCode;
                orderItem.UserName = userEmail;
                orderItem.Status = 1;
                orderItem.OrderDate = DateTime.Now;
                _dataContext.Add(orderItem);
                _dataContext.SaveChanges();

                List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
                foreach (var cart in cartItems)
                {
                    var orderDetails = new OrderDetails();
                    orderDetails.UserName = userEmail;
                    orderDetails.OrderCode = orderCode;
                    orderDetails.BookId = cart.BookId;
                    orderDetails.Price = cart.Price;
                    orderDetails.Quantity = cart.Quantity;
                    _dataContext.Add(orderDetails);
                    _dataContext.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "CHeckout thành công, vui long chờ duyệt đơn hàng";
                return RedirectToAction("Index", "Cart");
            }
            return View();
        }
    }      
}
