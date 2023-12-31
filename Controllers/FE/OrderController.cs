//using JamesRecipes.Models;
//using JamesRecipes.Repository.FE;
//using Microsoft.AspNetCore.Mvc;

//namespace JamesRecipes.Controllers.FE
//{
//    public class OrderController : Controller
//    {
//        //private readonly IOrder _orderRepository;

//        //public OrderController(IOrder orderRepository)
//        //{
//        //    _orderRepository = orderRepository;
//        //}

//        //public IActionResult Index()
//        //{
//        //    var allOrders = _orderRepository.GetAllOrders();
//        //    return View(allOrders);
//        //}

//        //public IActionResult Details(int id)
//        //{
//        //    var order = _orderRepository.GetOrderById(id);
//        //    if (order == null)
//        //    {
//        //        return NotFound(); // Handle not found
//        //    }
//        //    return View(order);
//        //}

//        //public IActionResult UserOrders(string userId)
//        //{
//        //    var userOrders = _orderRepository.GetOrdersByUserId(userId);
//        //    return View(userOrders);
//        //}

//        //[HttpPost]
//        //public IActionResult CreateOrder(Order order)
//        //{
//        //    // Validate and process the order
//        //    if (ModelState.IsValid)
//        //    {
//        //        _orderRepository.CreateOrder(order);
//        //        return RedirectToAction(nameof(Index));
//        //    }

//        //    // If validation fails, return to the create view with error messages
//        //    return View(order);
//        //}

//        //[HttpPost]
//        //public IActionResult UpdateOrderStatus(int orderId, OrderStatus newStatus)
//        //{
//        //    _orderRepository.UpdateOrderStatus(orderId, newStatus);
//        //    return RedirectToAction(nameof(Details), new { id = orderId });
//        //}

//        //[HttpPost]
//        //public IActionResult CancelOrder(int orderId)
//        //{
//        //    _orderRepository.CancelOrder(orderId);
//        //    return RedirectToAction(nameof(Index));
//        //}
//    }
//}
