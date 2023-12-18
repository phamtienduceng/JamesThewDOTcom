using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Models.Authentication
{
    public class AuthenticationAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("admin") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                    { "Controller","AccountManagement"},
                    {"Action","LoginAdmin" },
                    });
            }
        }
    }
}
