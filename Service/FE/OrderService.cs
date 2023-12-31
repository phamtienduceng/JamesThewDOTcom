using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Service.FE
{
    public class OrderService 
    {
        //private readonly JamesrecipesContext _context;

        //public OrderService(JamesrecipesContext context)
        //{
        //    _context = context;
        //}

        //public Order CreateOrder(Order order)
        //{
        //    _context.Orders.Add(order);
        //    _context.SaveChanges();
        //    return order;
        //}

        //public Order GetOrderById(int orderId)
        //{
        //    return _context.Orders
        //        .Include(o => o.OrderDetails) // Include any related entities you need
        //        .FirstOrDefault(o => o.OrderId == orderId);
        //}

        //public List<Order> GetOrdersByUserId(string userId)
        //{
        //    return _context.Orders
        //        .Where(o => o.UserId == userId)
        //        .ToList();
        //}

        //public void UpdateOrderStatus(int orderId, OrderStatus newStatus)
        //{
        //    var order = _context.Orders.Find(orderId);
        //    if (order != null)
        //    {
        //        order.Status = newStatus;
        //        _context.SaveChanges();
        //    }
        //}

        //public void CancelOrder(int orderId)
        //{
        //    var order = _context.Orders.Find(orderId);
        //    if (order != null)
        //    {
        //        // Perform any additional logic for order cancellation, if needed
        //        _context.Orders.Remove(order);
        //        _context.SaveChanges();
        //    }
        //}

        //public List<Order> GetAllOrders()
        //{
        //    return _context.Orders.ToList();
        //}
    }
}
