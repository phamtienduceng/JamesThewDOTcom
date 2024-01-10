using JamesRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Drawing;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using MailKit.Net.Smtp;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class AccountController : Controller
{
    private readonly IAccount _account;

    public AccountController(IAccount account)
    {
        _account = account;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View("~/Views/FE/Account/Login.cshtml");
    }

    [HttpPost("login")]
    public IActionResult Login(string email, string pass)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                ViewData["Error"] = "Please enter email and password.";
                ViewBag.EmailValue = email;
                return View("~/Views/FE/Account/Login.cshtml");
            }
            else
            {
                var user = _account.GetUserByEmail(email);
                if (user != null && _account.VerifyPassword(pass, user.Password))
                {
                    var userJson = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("userLogged", userJson);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Error"] = "Wrong email or password!";
                    ViewBag.EmailValue = email;
                    return View("~/Views/FE/Account/Login.cshtml");
                }
            }

        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return View("~/Views/FE/Account/Login.cshtml");
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View("~/Views/FE/Account/Register.cshtml");
    }

    [HttpPost("register")]
    public IActionResult Register(User newUser, string confirm)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var User = _account.GetUserByEmail(newUser.Email);

                if (User == null)
                {
                    if (newUser.Password == confirm)
                    {
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                        newUser.Password = hashedPassword;
                        newUser.RoleId = 2;
                        _account.AddUser(newUser);
                        ViewData["Er"] = "";
                        ViewData["Em"] = "";
                        ViewData["Success"] = "You have successfully registered an account.";
                        return View("~/Views/FE/Account/Register.cshtml");
                    }
                    else
                    {
                        ViewData["Error"] = "Password and confirm password do not match.";
                    }
                }
                else
                {
                    ViewData["Error"] = "Email is already registered.";
                }
            }
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ tại đây, ví dụ ghi log lỗi
            ModelState.AddModelError("", "An error occurred during registration.");
        }

        return View("~/Views/FE/Account/Register.cshtml");
    }

    [HttpGet("MyProfile")]
    public IActionResult MyProfile(int id)
    {
        var user = _account.GetUserById(id);
        return View("~/Views/FE/Account/MyProfile.cshtml", user);
    }

    [HttpPost("MyProfile")]
    public IActionResult MyProfile(int id, User model)
    {
        if (string.IsNullOrEmpty(model.PhoneNumber) || string.IsNullOrEmpty(model.Address))
        {
            TempData["msg"] = "Please input at least all field";
            return RedirectToAction("MyProfile", "Account", new { id = id });
        }
        else
        {
            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                if (model.PhoneNumber.Length != 10)
                {
                    TempData["msg"] = "Phone number should have 10 digits";
                    return RedirectToAction("MyProfile", "Account", new { id = id });
                }
            }
            _account.UpdateProfile(id, model);
            ViewData["Success"] = "You have successfully registered an account.";
            return RedirectToAction("MyProfile", "Account", new { id = id });
        }
    }

    [HttpGet("changepassword/{id}")]
    public IActionResult ChangePassword(int id)
    {
        var user = _account.GetUserById(id);

        if (user != null)
        {
            var model = new User
            {
                Password = user.Password
            };

            return View("~/Views/FE/Account/ChangePassword.cshtml", model);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("changepassword/{id}")]
    public IActionResult ChangePassword(int id, string newPassword, string confirm, string oldpass)
    {
        var user = _account.GetUserById(id);
        if (!_account.VerifyPassword(oldpass, user.Password))
        {
            ViewData["Error"] = "The old password is incorrect!";
            ViewBag.Oldpass = oldpass;
            ViewBag.NewPassword = newPassword;
            ViewBag.Confirm = confirm;
            return View("~/Views/FE/Account/ChangePassword.cshtml");
        }
        else if (newPassword == oldpass)
        {
            ViewData["Error"] = "The new and old passwords cannot be the same!";
            ViewBag.Oldpass = oldpass;
            ViewBag.NewPassword = newPassword;
            ViewBag.Confirm = confirm;
            return View("~/Views/FE/Account/ChangePassword.cshtml");
        }
        else if (newPassword != confirm)
        {
            ViewData["Error"] = "The new password and confirm password do not match!";
            ViewBag.Oldpass = oldpass;
            ViewBag.NewPassword = newPassword;
            ViewBag.Confirm = confirm;
            return View("~/Views/FE/Account/ChangePassword.cshtml");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.Password = hashedPassword;
        _account.UpdateUser(user);
        ViewData["Success"] = "Your password has been changed successfully.";
        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
        return View("~/Views/FE/Account/ChangePassword.cshtml");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("user");
        return View("~/Views/FE/Account/Login.cshtml");
    }

    [HttpGet("ForgotPassword")]
    public ActionResult ForgotPassword()
    {
        return View("~/Views/FE/Account/ForgotPassword.cshtml");
    }

    [HttpPost("ForgotPassword")]
    public ActionResult ForgotPassword(string email)
    {
        if (IsEmailValid(email))
        {
            SendResetPasswordEmail(email);
            ViewData["Success"] = "You have successfully sent the password verification email.";
            return View("~/Views/FE/Account/ForgotPassword.cshtml");
        }

        ModelState.AddModelError("", "Email is not invalid.");
        return View("~/Views/FE/Account/ForgotPassword.cshtml");
    }
    [HttpGet("ResetPassword/{email}")]
    public ActionResult ResetPassword(string email)
    {
        var acc = _account.GetUserByEmail(email);
        return View("~/Views/FE/Account/ResetPassword.cshtml");
    }

    [HttpPost("ResetPassword/{email}")]
    public ActionResult ResetPassword(string email, string newpass, string confirm)
    {

        var acc = _account.GetUserByEmail(email);
        if (string.IsNullOrWhiteSpace(newpass) || string.IsNullOrWhiteSpace(confirm))
        {
            ViewData["Error"] = "Please enter data.";
            return View("~/Views/FE/Account/ResetPassword.cshtml");
        }
        else if (newpass != confirm)
        {
            ViewData["Error"] = "The new password and confirm password do not match!";
            return View("~/Views/FE/Account/ResetPassword.cshtml");
        }
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newpass);
        acc.Password = hashedPassword;
        _account.UpdateUser(acc);
        ViewData["Success"] = "You have successfully changed your password.";
        return View("~/Views/FE/Account/ResetPassword.cshtml");
    }
   
    private bool IsEmailValid(string email)
    {
        // Kiểm tra email có tồn tại trong hệ thống hay không
        // Trong ví dụ này, chúng ta sẽ sử dụng một danh sách người dùng ảo để kiểm tra
        List<string> users = GetListOfUsersFromDatabase();
        return users.Contains(email);
    }

    private void SendResetPasswordEmail(string email)
    {
        string resetUrl = Url.Action("ResetPassword", "Account", new { email = email }, Request.Scheme);
        Console.WriteLine("Đường dẫn đặt lại mật khẩu: " + resetUrl);

        // Set email details
        string fromAddress = "phuhoang136@gmail.com"; // Sender email address
        string toAddress = email; // Recipient email address
        string subject = "Reset Password"; // Email subject
        string body = "Hello, you can reset your password by accessing the following link: " + resetUrl; // Email body

        // Send email
        using (var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("phuhoang136@gmail.com", "cocw c lij nh kr sye m");
            smtpClient.EnableSsl = true; // Enable SSL/TLS encryption

            // Set email properties
            var mailMessage = new System.Net.Mail.MailMessage(fromAddress, toAddress, subject, body);
            mailMessage.IsBodyHtml = true; // Set body as HTML

            // Send the email
            smtpClient.Send(mailMessage);
        }
    }

    private List<string> GetListOfUsersFromDatabase()
    {
        List<string> userList = new List<string>();

        // Kết nối và truy vấn cơ sở dữ liệu
        using (SqlConnection connection = new SqlConnection("Server=.,1500;Database=s20;User=sa;Password=12345678;TrustServerCertificate=True"))
        {
            connection.Open();

            string query = "SELECT email FROM Users";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        string email = reader.GetString(0);
                        userList.Add(email);
                    }
                }
            }
        }

        return userList;
    }
}
