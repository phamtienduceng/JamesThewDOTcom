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



namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class AccountController : Controller
{
    JamesrecipesContext _db;

    public AccountController(JamesrecipesContext db)
    {
        _db = db;
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
                return View("~/Views/FE/Account/Login.cshtml");
            }

            var user = _db.Users.SingleOrDefault(u => u.Email.Equals(email));

            if (user != null && BCrypt.Net.BCrypt.Verify(pass, user.Password) && user.RoleId == 2)
            {
                var userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("user", userJson);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Error"] = "Wrong email or password!";
                return View("~/Views/FE/Account/Login.cshtml");
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
    public IActionResult Register(User newUsers)
    {
        if (ModelState.IsValid)
        {
            var existingUser = _db.Users.SingleOrDefault(u => u.Email.Equals(newUsers.Email));

            if (existingUser == null)
            {
                // Mã hóa mật khẩu bằng bcrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUsers.Password);
                newUsers.Password = hashedPassword;
                newUsers.RoleId = 2;
                _db.Users.Add(newUsers);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email is already registered.");
            }
        }

        return View("~/Views/Home/Index.cshtml");
    }

    [HttpGet("MyProfile/{id}")]
    public IActionResult MyProfile(int id)
    {
        var acc = _db.Users.SingleOrDefault(u => u.UserId == id);
        var userJson = HttpContext.Session.GetString("user");
        var user = JsonConvert.DeserializeObject<User>(userJson);
        var model = new User
        {
            Username = acc.Username,
            PhoneNumber = acc.PhoneNumber,
            Address = acc.Address
        };

        return View("~/Views/FE/Account/MyProfile.cshtml", model);
    }

    [HttpPost("MyProfile/{id}")]
    public IActionResult MyProfile(int id, User model)
    {
        var acc = _db.Users.Find(id);
        if (acc != null)
        {
            acc.Username = model.Username;
            acc.PhoneNumber = model.PhoneNumber;
            acc.Address = model.Address;
            _db.SaveChanges();
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(acc));
        }

        return View("~/Views/FE/Account/MyProfile.cshtml");
    }

    [HttpGet("changepassword/{id}")]
    public IActionResult ChangePassword(int id)
    {
        var acc = _db.Users.SingleOrDefault(u => u.UserId == id);
        var model = new User
        {
            Password = acc.Password,
        };

        return View("~/Views/FE/Account/ChangePassword.cshtml", model);
    }

    [HttpPost("changepassword/{id}")]
    public IActionResult ChangePassword(int id, string newpass, string conform)
    {
        var acc = _db.Users.Find(id);
        if (string.IsNullOrWhiteSpace(newpass) || string.IsNullOrWhiteSpace(conform) || string.IsNullOrWhiteSpace(acc.Password))
        {
            ViewData["Error"] = "Please enter data.";
            return View("~/Views/FE/Account/Login.cshtml");
        }
        else if (acc.Password == newpass)
        {
            ViewData["Error"] = "The new and old passwords cannot be the same!";
            return View("~/Views/FE/Account/ChangePassword.cshtml");
        }
        else if (acc.Password != conform)
        {
            ViewData["Error"] = "The new password and conform password do not match!";
            return View("~/Views/FE/Account/ChangePassword.cshtml");
        }

        acc.Password = newpass;
        _db.SaveChanges();

        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(acc));

        return View("~/Views/FE/Account/ChangePassword.cshtml");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("user");
        return View("~/Views/Home/Index.cshtml");
    }





    [HttpGet("ForgotPassword")]
    public ActionResult ForgotPassword()
    {
        return View("~/Views/FE/Account/ForgotPassword.cshtml");
    }

    HttpPost("ForgotPassword")]
    public ActionResult ForgotPassword(string email)
    {
        // Kiểm tra xem email có tồn tại trong hệ thống hay không
        if (IsEmailValid(email))
        {
            // Gửi email chứa đường dẫn đặt lại mật khẩu đến địa chỉ email người dùng
            SendResetPasswordEmail(email);

            return RedirectToAction("ForgotPasswordConfirmation", "Account");
        }

        // Nếu email không tồn tại trong hệ thống, hiển thị form quên mật khẩu lại với thông báo lỗi
        ModelState.AddModelError("", "Email không hợp lệ.");
        return View();
    }
    [HttpGet("ResetPassword/{email}")]
    public ActionResult ResetPassword(string email)
    {
        var acc = _db.Users.SingleOrDefault(u => u.Email == email);
        return View("~/Views/FE/Account/ResetPassword.cshtml");
    }

    [HttpPost("ResetPassword/{email}")]
    public ActionResult ResetPassword(string email, string newpass, string conform)
    {
        var acc = _db.Users.SingleOrDefault(u => u.Email == email);
        if (string.IsNullOrWhiteSpace(newpass) || string.IsNullOrWhiteSpace(conform))
        {
            ViewData["Error"] = "Please enter data.";
            return View("~/Views/FE/Account/ResetPassword.cshtml");
        }
        else if (newpass != conform)
        {
            ViewData["Error"] = "The new password and conform password do not match!";
            return View("~/Views/FE/Account/ResetPassword.cshtml");
        }
        acc.Password = newpass;
        _db.SaveChanges();
        return View("~/Views/FE/Account/ResetPassword.cshtml");
    }

    [HttpGet("ForgotPasswordConfirmation")]
    public ActionResult ForgotPasswordConfirmation()
    {
        return View("~/Views/FE/Account/ForgotPasswordConfirmation.cshtml");
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
        string body = "Hello, you can reset your password by accessing System.Net.Mail.SmtpExceptionthe following link: " + resetUrl; // Email body

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
        using (SqlConnection connection = new SqlConnection("Server=.,1500;Database=jamesrecipes;User=sa;Password=12345678;TrustServerCertificate=True"))
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
