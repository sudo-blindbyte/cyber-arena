using Microsoft.AspNetCore.Mvc;

namespace cyber_arena.Controllers
{
    /// <summary>
    /// Handles Admin authentication — separate from user login.
    /// Credentials are read from appsettings.json → AdminCredentials section.
    /// </summary>
    public class AdminLoginController : Controller
    {
        private const string SessionKey     = "IsAdminAuthenticated";
        private const string SessionUser    = "AdminUsername";

        private readonly IConfiguration _config;

        public AdminLoginController(IConfiguration config)
        {
            _config = config;
        }

        // ─── GET /Admin/Login ─────────────────────────────────
        [HttpGet]
        [Route("Admin/Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            // Already logged in → go straight to dashboard
            if (HttpContext.Session.GetString(SessionKey) == "true")
                return RedirectToAction("Index", "Admin");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // ─── POST /Admin/Login ────────────────────────────────
        [HttpPost]
        [Route("Admin/Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Read credentials from appsettings.json
            var adminUser = _config["AdminCredentials:Username"] ?? "admin";
            var adminPass = _config["AdminCredentials:Password"] ?? "CyberArena@Admin2026!";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Username and password are required.");
                return View();
            }

            if (!string.Equals(username.Trim(), adminUser, StringComparison.Ordinal) ||
                !string.Equals(password, adminPass, StringComparison.Ordinal))
            {
                // Deliberate delay to slow brute-force attempts
                Thread.Sleep(500);
                ModelState.AddModelError(string.Empty, "Invalid admin credentials.");
                return View();
            }

            // ── Auth success ─────────────────────────────────
            HttpContext.Session.SetString(SessionKey,  "true");
            HttpContext.Session.SetString(SessionUser, adminUser);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Admin");
        }

        // ─── POST /Admin/Logout ───────────────────────────────
        [HttpPost]
        [Route("Admin/Logout")]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
