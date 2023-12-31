using JamesRecipes.Data.Helper;
using JamesRecipes.Models.Book;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Service.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayPalCheckoutSdk.Orders;
using System;
using System.Net;
using System.Security.Policy;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class CartController : Controller
{
    private readonly ICart _repository;
    private readonly JamesrecipesContext _dbContext;

    public CartController(ICart repository, JamesrecipesContext dbContext)
    {
        _repository = repository;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var cart = SessionHelper.GetObjectJson<List<Cart>>(HttpContext.Session, "cart");
        ViewBag.cart = cart;
        ViewBag.total = cart?.Sum(item => item?.Books?.Price * item?.Quantity) ?? 0;

        return View("~/Views/FE/Cart/Index.cshtml", cart);
    }

    [HttpGet("CheckBook")]
    public int CheckBook(int id)
    {
        var cart = SessionHelper.GetObjectJson<List<Cart>>(HttpContext.Session, "cart");
        for (int i = 0; i < cart!.Count; i++)
        {
            if (cart[i].Books!.BookId.Equals(id))
            {
                return i;
            }
        }
        return -1;
    }

    [HttpGet("AddToCart")]
    public IActionResult AddToCart(int id)
    {
        Book book = _repository.GetBookById(id);

        if (book != null)
        {
            List<Cart> cart = SessionHelper.GetObjectJson<List<Cart>>(HttpContext.Session, "cart") ?? new List<Cart>();
            Cart existingCartItem = cart.FirstOrDefault(item => item.Books?.BookId == id);
            if (existingCartItem != null)
            {
                // Nếu sách đã có trong giỏ hàng, kiểm tra số lượng
                if (existingCartItem.Quantity < book.Quantity)
                {
                    existingCartItem.Quantity++;
                    SessionHelper.SetObjectJson(HttpContext.Session, "cart", cart);
                }
                else
                {
                    ViewBag.ErrorMessage = "Không thể thêm sách vào giỏ hàng vì số lượng đã đạt đến giới hạn.";
                    return RedirectToAction("Index", "Cart");
                }
            }
            else
            {
                if (book.Quantity > 0)
                {
                    cart.Add(new Cart
                    {
                        Books = book,
                        Price = book.Price,
                        Quantity = 1,
                        OrderDate = DateTime.Now
                    });

                    SessionHelper.SetObjectJson(HttpContext.Session, "cart", cart);
                }
                else
                {
                    ViewBag.ErrorMessage = "Không thể thêm sách vào giỏ hàng vì số lượng không đủ.";
                    return RedirectToAction("Index", "Cart");
                }
            }
        }
        else
        {
            ViewBag.ErrorMessage = "Sách không tồn tại trong cơ sở dữ liệu.";
            return RedirectToAction("Index", "Cart");
        }
        return RedirectToAction("Index", "Cart");
    }

    [HttpPost("Checkout")]
    public IActionResult Checkout()
    {
        List<Cart> cart = SessionHelper.GetObjectJson<List<Cart>>(HttpContext.Session, "cart") ?? new List<Cart>();
        if (cart.Any())
        {
            try
            {
                decimal totalAmount = CalculateTotalAmount(cart);
                bool paymentSuccess = ProcessPayment(totalAmount);
                if (paymentSuccess)
                {
                    SaveOrderToDatabase(cart);
                    SessionHelper.SetObjectJson(HttpContext.Session, "cart", null);
                    ViewBag.SuccessMessage = "Your order has been processed successfully.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Payment failed. Please try again or contact support.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during payment processing. Please try again or contact support.";
            }
        }
        else
        {
            ViewBag.ErrorMessage = "There are no products in the cart to checkout.";
        }
        if (ViewBag.SuccessMessage == null && ViewBag.ErrorMessage == null)
        {
            ViewBag.WarningMessage = "Processing payment. Please wait...";
        }

        return View("~/Views/FE/Cart/Checkout.cshtml", cart);
    }


    [HttpPost("Remove")]
    public IActionResult Remove(int id)
    {
        var cart = SessionHelper.GetObjectJson<List<Cart>>(HttpContext.Session, "cart");
        int index = CheckBook(id);
        cart!.RemoveAt(index);
        SessionHelper.SetObjectJson(HttpContext.Session, "cart", cart);
        return RedirectToAction("Index", "Cart");
    }

    [HttpPost("Update")]
    public IActionResult Update(int id, int quantity)
    {
        var cart = SessionHelper.GetObjectJson<List<Cart>>(HttpContext.Session, "cart");
        int index = CheckBook(id);
        if (index != -1)
        {
            cart![index].Quantity = quantity;
            SessionHelper.SetObjectJson(HttpContext.Session, "cart", cart);
        }
        return RedirectToAction("Index", "Cart");
    }


    // Hàm tính tổng số tiền trong giỏ hàng
    private decimal CalculateTotalAmount(List<Cart> cart)
    {
        return cart.Sum(item => item.Price * item.Quantity);
    }

    // Hàm xử lý thanh toán (giả định: trả về true nếu thanh toán thành công, ngược lại trả về false)
    private bool ProcessPayment(decimal totalAmount)
    {
        // Thực hiện xử lý thanh toán thực tế ở đây
        // (gọi cổng thanh toán, xác nhận thanh toán, cập nhật trạng thái thanh toán, vv.)
        // Trong ví dụ này, giả định thanh toán luôn thành công
        return true;
    }

    // Lưu đơn hàng vào cơ sở dữ liệu
    private void SaveOrderToDatabase(List<Cart> cart)
    {
        // Tạo đối tượng Order từ thông tin trong giỏ hàng
        Models.Book.Order order = new Models.Book.Order
        {
            OrderDate = DateTime.Now,
            TotalAmount = CalculateTotalAmount(cart),
            //Thêm các thông tin khác tùy theo yêu cầu của bạn
        };

        // Lưu đối tượng Order vào cơ sở dữ liệu (ví dụ: sử dụng Entity Framework)
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }

    //// Gửi email xác nhận đơn hàng
    //private void SendOrderConfirmationEmail(List<Cart> cart)
    //{
    //    string customerEmail = "customer@example.com"; // Lấy địa chỉ email của khách hàng từ đối tượng cart hoặc thông tin đăng nhập (nếu có)

    //    // Tạo nội dung email từ thông tin đơn hàng
    //    string emailContent = "Cảm ơn bạn đã đặt hàng. Đơn hàng của bạn đã được xử lý thành công.";

    //    // Gửi email (ví dụ: sử dụng thư viện gửi email)
    //    EmailService.SendEmail(customerEmail, "Xác nhận đơn hàng", emailContent);
    //}

    // Cập nhật số lượng sách trong kho
    private void UpdateBookQuantities(List<Cart> cart)
    {
        foreach (var cartItem in cart)
        {
            Book book = _repository.GetBookById(cartItem.Books.BookId);

            // Giả sử cập nhật số lượng sách trong kho
            if (book != null)
            {
                book.Quantity -= cartItem.Quantity;
                _repository.UpdateBook(book);
            }
        }
    }




}