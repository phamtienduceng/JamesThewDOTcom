using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Controllers.FE
{
    [Route("fe/[controller]")]
    public class PaypalController : Controller
    {
        private readonly IAccount _account;
        private readonly IPaypal _paypal;

        public PaypalController(IAccount account,IPaypal paypal)
        {
            _account = account;
            _paypal = paypal;
        }

        [HttpGet("Register/{ID}")]
        public IActionResult Register(int ID)
        {
            var user = _account.GetUserById(ID);
            if (user != null)
            {
                return View("~/Views/FE/Paypal/Register.cshtml");
            }
            return RedirectToAction("Login", "Account");

        }

        [HttpPost("Register/{ID}")]
        public IActionResult Register(int ID, int price)
        {
            if (price == 10)
            {
                var membership = _paypal.MemberById(ID);
                if (membership != null)
                {
                    membership.EndDate = membership.EndDate?.AddMonths(1);
                    _paypal.UpdateMember(membership);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var ViewRoleUserMember = new Membership { UserId = ID, IsActive = true, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
                    _paypal.AddMember(ViewRoleUserMember);
                    var user = _account.GetUserById(ID);
                    if (user != null)
                    {
                        user.RoleId = 3;
                        _account.UpdateUser(user);
                        return View("~/Views/FE/Paypal/Register.cshtml");
                    }
                }
            }
            else
            {
                var membership = _paypal.MemberById(ID);
                if (membership != null)
                {
                    membership.EndDate = membership.EndDate?.AddYears(1);
                    _paypal.UpdateMember(membership);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var ViewRoleUserMember = new Membership { UserId = ID, IsActive = true, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1) };
                    _paypal.AddMember(ViewRoleUserMember);
                    var user = _account.GetUserById(ID);
                    if (user != null)
                    {
                        user.RoleId = 3;
                        _account.UpdateUser(user);
                        return View("~/Views/FE/Paypal/Register.cshtml");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet("CheckMember/{ID}")]
        public IActionResult CheckMember(int ID)
        {
            var membership = _paypal.MemberById(ID);
            return View("~/Views/FE/Paypal/CheckMember.cshtml");
        }

        [HttpPost("CheckMember/{ID}")]
        public IActionResult CheckMember(int ID, int model)
        {
            var membership = _paypal.MemberById(ID);
            DateTime now = DateTime.Now;
            DateTime endday = (DateTime)membership.EndDate;
            if (endday < now)
            {
                membership.IsActive = false;
                _paypal.UpdateMember(membership);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                membership.IsActive = true;
                _paypal.UpdateMember(membership);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}