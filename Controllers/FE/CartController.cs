using JamesRecipes.Data.Helper;
using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using JamesRecipes.Models.Book.DTOs;
using JamesRecipes.Repository.FE;
using JamesRecipes.Service.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Policy;

namespace JamesRecipes.Controllers.FE;
public class CartController : Controller
{
    private readonly JamesrecipesContext _dataContext;

    public CartController(JamesrecipesContext context)
    {
        _dataContext = context;
    }

    public IActionResult Index()
    {
        List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
        CartItemViewModel cartVM = new()
        {
            CartItems = cartItems,
            GrandTotal = cartItems.Sum(x => x.Price * x.Quantity)
        };
        return View("~/Views/FE/Cart/Cart.cshtml", cartVM);
    }

    public IActionResult Add(int Id)
    {
        BookModel book = _dataContext.Books.Find(Id);
        List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
        CartItemModel cartItems = cart.Where(c => c.BookID == Id).FirstOrDefault();
        if (cartItems == null)
        {
            cart.Add(new CartItemModel(book));
        }
        else
        {
            cartItems.Quantity += 1;
        }
        HttpContext.Session.SetJson("Cart", cart);
        TempData["success"] = "Add Item to Cart Successfully!!!";
        return Redirect(Request.Headers["Referer"].ToString());
    }

    public IActionResult Decrease(int Id)
    {
        List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
        CartItemModel cartItems = cart.Where(c => c.BookID == Id).FirstOrDefault();
        if (cartItems.Quantity > 1)
        {
            --cartItems.Quantity;
        }
        else
        {
            cart.RemoveAll(b => b.BookID == Id);
        }
        if (cart.Count == 0)
        {
            HttpContext.Session.Remove("Cart");
        }
        else
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
        TempData["success"] = "Decrease Item quantity to cart Successfully!!!";
        return RedirectToAction("Index");
    }

    public IActionResult Increase(int Id)
    {
        List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
        CartItemModel cartItems = cart.Where(c => c.BookID == Id).FirstOrDefault();
        if (cartItems.Quantity >= 1)
        {
            ++cartItems.Quantity;
        }
        else
        {
            cart.RemoveAll(b => b.BookID == Id);
        }
        if (cart.Count == 0)
        {
            HttpContext.Session.Remove("Cart");
        }
        else
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
        TempData["success"] = "Increase Item quantity to cart Successfully!!!";
        return RedirectToAction("Index");
    }

    public IActionResult Remove(int Id)
    {
        List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
        cart.RemoveAll(b => b.BookID == Id);
        if (cart.Count == 0)
        {
            HttpContext.Session.Remove("Cart");
        }
        else
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
        TempData["error"] = "Remove Item of cart Successfully!!!";
        return RedirectToAction("Index");
    }

    public IActionResult Clear()
    {
        HttpContext.Session.Remove("Cart");
        TempData["error"] = "Clear all Item of cart Successfully!!!";
        return RedirectToAction("Index");
    }

}

