using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JamesRecipes.Models.Authentication
{
    public class AuthenticationAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userJson = context.HttpContext.Session.GetString("userLogged");

            if (!string.IsNullOrEmpty(userJson) )
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                if (user.RoleId != 1)
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
}
