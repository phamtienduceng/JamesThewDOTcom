using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JamesRecipes.Models.Authentication
{
    public class Authentication:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userJson = context.HttpContext.Session.GetString("user");
            var adminJson = context.HttpContext.Session.GetString("admin");

            if (string.IsNullOrEmpty(userJson) && string.IsNullOrEmpty(adminJson))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                { "Controller", "Account" },
                { "Action", "Login" }
                    });
            }
        }
    }
}
