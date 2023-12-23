//using JamesRecipes.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using PayPalCheckoutSdk.Orders;
//using PayPalHttp;

//    public class PaypalController : Controller
//    {
//        private readonly JamesrecipesContext _db;

//        public PaypalController(JamesrecipesContext db)
//        {
//            _db = db;
//        }

//    [HttpGet]
//    public IActionResult Register()
//    {
//        return View();
//    }

//    [HttpPost]
//    public IActionResult Register(Membership model, string membershipType)
//    {
//        if (ModelState.IsValid)
//        {
//            // Xử lý đăng ký thành viên theo membershipType
//            if (membershipType == "Monthly")
//            {
//                model.StartDate = DateTime.Now;
//                model.EndDate = model.StartDate.AddMonths(1);
//                model.MembershipFee = 10.0m; // Giá thành viên hàng tháng (USD)
//            }
//            else if (membershipType == "Yearly")
//            {
//                model.StartDate = DateTime.Now;
//                model.EndDate = model.StartDate.AddYears(1);
//                model.MembershipFee = 100.0m; // Giá thành viên hàng năm (USD)
//            }

//            // Tạo đơn hàng PayPal
//            OrderRequest orderRequest = CreatePayPalOrder(model.MembershipFee);

//            try
//            {
//                // Tạo yêu cầu tạo đơn hàng PayPal
//                OrdersCreateRequest request = new OrdersCreateRequest();
//                request.Headers.Add("prefer", "return=representation");
//                request.RequestBody(orderRequest);

//                // Gửi yêu cầu tạo đơn hàng PayPal
//                var response = PayPalClient().Execute(request);

//                // Lấy đường dẫn thanh toán từ response
//                var approvalUrl = response.Result<PayPalCheckoutSdk.Orders.Order>().Links.Find(link => link.Rel == "approve").Href;

//                // Chuyển hướng đến trang thanh toán PayPal
//                return Redirect(approvalUrl);
//            }
//            catch (HttpException ex)
//            {
//                // Xử lý lỗi tạo đơn hàng PayPal, hiển thị thông báo lỗi cho người dùng
//                ModelState.AddModelError("", "Payment failed. Please try again.");
//            }
//        }

//        return View(model);
//    }
//}