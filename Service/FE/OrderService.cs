//using JamesRecipes.Models.Book;
//using JamesRecipes.Repository.FE;

//namespace JamesRecipes.Service.FE
//{
//    public class OrderService : IOrder
//    {
//        private readonly JamesrecipesContext _db;

//        public OrderService(JamesrecipesContext db)
//        {
//            _db = db;
//        }

//        public void ProcessOrder(int userId, List<CartItems> cartItems)
//        {
//            // Kiểm tra số lượng tồn kho và giảm số lượng sản phẩm
//            foreach (var cartItem in cartItems)
//            {
//                var book = _db.Books.Find(cartItem.BookId);

//                if (book != null && book.Quantity >= cartItem.Quantity)
//                {
//                    // Giảm số lượng tồn kho
//                    book.Quantity -= cartItem.Quantity;
//                }
//                else
//                {
//                    // Xử lý khi không đủ số lượng tồn kho
//                    // Ví dụ: thông báo lỗi, hủy đơn hàng, v.v.
//                }
//            }

//            // Tạo đơn hàng và chi tiết đơn hàng
//            var order = new Order
//            {
//                UserId = userId,
//                OrderDate = DateTime.Now,
//            };

//            foreach (var cartItem in cartItems)
//            {
//                var orderItem = new OrderItem
//                {
//                    Order = order,
//                    BookId = cartItem.BookId,
//                    Quantity = cartItem.Quantity,
//                    Price = cartItem.Book.Price,
//                };

//                _db.OrderItems.Add(orderItem);
//            }

//            // Xóa giỏ hàng đã thanh toán
//            _db.CartItems.RemoveRange(cartItems);
//            // Lưu thay đổi vào cơ sở dữ liệu
//            _db.SaveChanges();
//        }
//    }
//}
