using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JamesRecipes.Controllers.FE
{
    [Route("fe/[controller]")]
    public class ContestLoginController : Controller
    {
        private readonly IContestLogin _contestLogin;
        public ContestLoginController(IContestLogin contestLogin)
        {
            _contestLogin = contestLogin;
        }

        [HttpGet("ContestLogin")]
        public IActionResult Login()
        {
            return View("~/Views/FE/Contest/ContestLogin.cshtml");
        }

        [HttpPost("ContestLogin")]
        public IActionResult Login(string email, string pass)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
                {
                    ViewData["Error"] = "Please enter email and password.";
                    return View("~/Views/FE/Contest/ContestLogin.cshtml");
                }

                var user = _contestLogin.GetUserByEmail(email);
                if (user != null && _contestLogin.VerifyPassword(pass, user.Password))
                {
                    var userJson = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("userLogged", userJson);

                    // Kiểm tra xem có ContestId nào được lưu trong session không
                    int? lastViewedContestId = HttpContext.Session.GetInt32("LastViewedContestId");
                    if (lastViewedContestId.HasValue)
                    {
                        // Xóa ContestId khỏi session sau khi lấy
                        HttpContext.Session.Remove("LastViewedContestId");

                        // Chuyển hướng trở lại trang "Details" với ContestId cụ thể
                        return RedirectToAction("Details", "Contest", new { id = lastViewedContestId.Value });
                    }

                    // Nếu không có ContestId, chuyển hướng đến trang mặc định
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Error"] = "Wrong email or password!";
                    return View("~/Views/FE/Contest/ContestLogin.cshtml");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("~/Views/FE/Contest/ContestLogin.cshtml");
            }
        }



    }
}
