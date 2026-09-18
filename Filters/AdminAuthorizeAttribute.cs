using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace cyber_arena.Filters
{
    /// <summary>
    /// Protects all Admin controller actions.
    /// Checks HttpContext.Session["IsAdminAuthenticated"] == "true".
    /// Redirects to /Admin/Login if not authenticated.
    /// Replace with [Authorize(Roles="Admin")] once ASP.NET Identity is connected.
    /// </summary>
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private const string SessionKey = "IsAdminAuthenticated";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;
            var isAuthenticated = session.GetString(SessionKey);

            if (isAuthenticated != "true")
            {
                // Preserve the originally requested URL so we can redirect back after login
                var returnUrl = context.HttpContext.Request.Path
                                + context.HttpContext.Request.QueryString;

                context.Result = new RedirectToActionResult(
                    actionName:     "Login",
                    controllerName: "AdminLogin",
                    routeValues:    new { returnUrl });
            }
        }
    }
}
