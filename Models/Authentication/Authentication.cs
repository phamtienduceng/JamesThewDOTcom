using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JamesRecipes.Models.Authentication
{
    public class Authentication:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userJson = context.HttpContext.Session.GetString("userLogged");

            if (string.IsNullOrEmpty(userJson))
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
