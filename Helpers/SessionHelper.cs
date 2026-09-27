using System;
using System.Web;
using System.Web.UI;

namespace CyberArenaWebForms.Helpers
{
    /// <summary>
    /// SessionHelper — replaces AdminAuthorizeAttribute from MVC.
    /// Call RequireAdminSession() in Page_Load of every Admin page to
    /// gate access behind the admin session cookie.
    /// </summary>
    public static class SessionHelper
    {
        // Session key constants — keep in sync with AdminLogin.aspx.cs
        public const string KEY_ADMIN_AUTH     = "IsAdminAuthenticated";
        public const string KEY_ADMIN_USERNAME = "AdminUsername";
        public const string KEY_USER_LOGGEDIN  = "IsUserLoggedIn";
        public const string KEY_USERNAME       = "Username";
        public const string KEY_USER_EMAIL     = "UserEmail";

        // ── Admin session ──────────────────────────────────────────

        /// <summary>Returns true if an admin session is active.</summary>
        public static bool IsAdminAuthenticated()
        {
            return HttpContext.Current.Session[KEY_ADMIN_AUTH] != null
                && (bool)HttpContext.Current.Session[KEY_ADMIN_AUTH] == true;
        }

        /// <summary>
        /// Redirects to AdminLogin.aspx if no admin session exists.
        /// Call this at the top of Page_Load on every protected admin page.
        /// </summary>
        public static void RequireAdminSession(Page page)
        {
            if (!IsAdminAuthenticated())
            {
                page.Response.Redirect("~/AdminLogin.aspx?reason=unauthorized", true);
            }
        }

        /// <summary>Gets the currently logged-in admin username, or "admin" as fallback.</summary>
        public static string GetAdminUsername()
        {
            return HttpContext.Current.Session[KEY_ADMIN_USERNAME] as string ?? "admin";
        }

        /// <summary>Sets the admin session after successful login.</summary>
        public static void SetAdminSession(string username)
        {
            HttpContext.Current.Session[KEY_ADMIN_AUTH]     = true;
            HttpContext.Current.Session[KEY_ADMIN_USERNAME] = username;
        }

        /// <summary>Clears the admin session on logout.</summary>
        public static void ClearAdminSession()
        {
            HttpContext.Current.Session.Remove(KEY_ADMIN_AUTH);
            HttpContext.Current.Session.Remove(KEY_ADMIN_USERNAME);
        }

        // ── User session ───────────────────────────────────────────

        /// <summary>Returns true if a regular user session is active.</summary>
        public static bool IsUserLoggedIn()
        {
            return HttpContext.Current.Session[KEY_USER_LOGGEDIN] != null
                && (bool)HttpContext.Current.Session[KEY_USER_LOGGEDIN] == true;
        }

        /// <summary>
        /// Redirects to Login.aspx if no user session exists.
        /// Call this at the top of Page_Load on every protected user page.
        /// </summary>
        public static void RequireUserSession(Page page)
        {
            if (!IsUserLoggedIn())
            {
                page.Response.Redirect("~/Account/Login.aspx?reason=unauthorized", true);
            }
        }

        /// <summary>Gets the logged-in username, or "Guest" as fallback.</summary>
        public static string GetUsername()
        {
            return HttpContext.Current.Session[KEY_USERNAME] as string ?? "Guest";
        }

        /// <summary>Gets the logged-in user email, or empty string as fallback.</summary>
        public static string GetUserEmail()
        {
            return HttpContext.Current.Session[KEY_USER_EMAIL] as string ?? string.Empty;
        }

        /// <summary>Sets the user session after successful login.</summary>
        public static void SetUserSession(string username, string email)
        {
            HttpContext.Current.Session[KEY_USER_LOGGEDIN] = true;
            HttpContext.Current.Session[KEY_USERNAME]      = username;
            HttpContext.Current.Session[KEY_USER_EMAIL]    = email;
        }

        /// <summary>Clears the user session on logout.</summary>
        public static void ClearUserSession()
        {
            HttpContext.Current.Session.Remove(KEY_USER_LOGGEDIN);
            HttpContext.Current.Session.Remove(KEY_USERNAME);
            HttpContext.Current.Session.Remove(KEY_USER_EMAIL);
        }

        // ── Flash messages (replaces TempData) ────────────────────
        // Convention: store in Session with a "Flash_" prefix, read once then clear.

        public static void SetFlash(string key, string message)
        {
            HttpContext.Current.Session["Flash_" + key] = message;
        }

        public static string GetFlash(string key)
        {
            string val = HttpContext.Current.Session["Flash_" + key] as string;
            HttpContext.Current.Session.Remove("Flash_" + key);
            return val;
        }
    }
}
