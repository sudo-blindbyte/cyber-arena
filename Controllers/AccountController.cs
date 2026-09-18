using Microsoft.AspNetCore.Mvc;
using cyber_arena.ViewModels;

namespace cyber_arena.Controllers
{
    public class AccountController : Controller
    {
        // ─────────────────────────────────────────────────────
        // LOGIN
        // ─────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["Title"] = "Sign In";
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["Title"] = "Sign In";
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            // TODO: Connect ASP.NET Core Identity
            // var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            // if (result.Succeeded) { ... redirect ... }
            // if (result.IsLockedOut) { ... }
            // ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            // Temporary: redirect to dashboard for UI demonstration
            TempData["DemoMode"] = "true";
            return RedirectToAction("Index", "Dashboard");
        }

        // ─────────────────────────────────────────────────────
        // REGISTER
        // ─────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = "Create Account";
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            ViewData["Title"] = "Create Account";

            if (!ModelState.IsValid)
                return View(model);

            // TODO: Connect ASP.NET Core Identity
            // var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            // var result = await _userManager.CreateAsync(user, model.Password);
            // if (result.Succeeded)
            // {
            //     await _signInManager.SignInAsync(user, isPersistent: false);
            //     return RedirectToAction("Index", "Dashboard");
            // }
            // foreach (var error in result.Errors)
            //     ModelState.AddModelError(string.Empty, error.Description);

            // Demo mode: simulate successful registration → go straight to dashboard
            TempData["DemoMode"] = "true";
            TempData["AdminSuccess"] = $"Welcome to CyberArena, {model.Username}! Your account has been created.";
            return RedirectToAction("Index", "Dashboard");
        }


        // ─────────────────────────────────────────────────────
        // VERIFY EMAIL
        // ─────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult VerifyEmail(string? email = null)
        {
            ViewData["Title"] = "Verify Your Email";
            return View(new VerifyEmailViewModel { Email = email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyEmail(VerifyEmailViewModel model)
        {
            // TODO: Validate OTP code against Identity email confirmation token
            // For now, simulate success
            model.IsSuccess = true;
            ViewData["Title"] = "Email Verified";
            return View(model);
        }

        // ─────────────────────────────────────────────────────
        // FORGOT PASSWORD
        // ─────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewData["Title"] = "Forgot Password";
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            ViewData["Title"] = "Forgot Password";

            if (!ModelState.IsValid)
                return View(model);

            // TODO: Connect ASP.NET Core Identity
            // var user = await _userManager.FindByEmailAsync(model.Email);
            // if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            // {
            //     var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //     var resetLink = Url.Action(nameof(ResetPassword), "Account",
            //         new { token, email = model.Email }, Request.Scheme);
            //     // send email with resetLink
            // }
            // Always show the same confirmation (to prevent email enumeration)

            TempData["ForgotPasswordSent"] = model.Email;
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            ViewData["Title"] = "Check Your Email";
            return View();
        }

        // ─────────────────────────────────────────────────────
        // RESET PASSWORD
        // ─────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult ResetPassword(string? token = null, string? email = null)
        {
            ViewData["Title"] = "Reset Password";
            if (token == null)
                return BadRequest("A token must be supplied for password reset.");

            return View(new ResetPasswordViewModel { Token = token, Email = email ?? string.Empty });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            ViewData["Title"] = "Reset Password";

            if (!ModelState.IsValid)
                return View(model);

            // TODO: Connect ASP.NET Core Identity
            // var user = await _userManager.FindByEmailAsync(model.Email);
            // if (user != null)
            // {
            //     var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            //     if (result.Succeeded) return RedirectToAction(nameof(ResetPasswordConfirmation));
            //     foreach (var error in result.Errors)
            //         ModelState.AddModelError(string.Empty, error.Description);
            //     return View(model);
            // }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            ViewData["Title"] = "Password Reset";
            return View();
        }

        // ─────────────────────────────────────────────────────
        // PROFILE
        // ─────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Profile()
        {
            ViewData["Title"] = "My Profile";

            // TODO: Retrieve from Identity + DB
            // var user = await _userManager.GetUserAsync(User);

            // Mock data for UI demonstration
            var vm = new ProfileViewModel
            {
                Username        = "h4x0r_pro",
                FullName        = "Alex Carter",
                Email           = "alex@cyberarena.io",
                AvatarUrl       = null,
                TotalScore      = 4250,
                CurrentRank     = 7,
                TotalParticipants = 214,
                ChallengesSolved = 34,
                TotalChallenges  = 120,
                CompetitionsEntered = 6,
                CompetitionsWon     = 2,
                TeamName  = "ByteBreakers",
                TeamRole  = "Captain",
                JoinDate  = new DateTime(2025, 3, 12),
                LastActiveDate = DateTime.UtcNow.AddHours(-3),
                Bio      = "Security researcher & CTF enthusiast. Love binary exploitation and reverse engineering.",
                Location = "Mumbai, India",
                Website  = "https://h4x0r.dev",
                GitHubHandle = "h4x0rpro",
                IsEditMode = false,
                EditBio      = "Security researcher & CTF enthusiast. Love binary exploitation and reverse engineering.",
                EditLocation = "Mumbai, India",
                EditWebsite  = "https://h4x0r.dev",
                EditGitHubHandle = "h4x0rpro",
                EditFullName  = "Alex Carter"
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(ProfileViewModel model)
        {
            ViewData["Title"] = "My Profile";

            // TODO: Save updated profile via Identity UserManager
            // var user = await _userManager.GetUserAsync(User);
            // user.FullName = model.EditFullName; etc.
            // await _userManager.UpdateAsync(user);

            TempData["ProfileSaved"] = "true";
            return RedirectToAction(nameof(Profile));
        }

        // ─────────────────────────────────────────────────────
        // CHANGE PASSWORD
        // ─────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            // TODO: Connect ASP.NET Core Identity
            // var user = await _userManager.GetUserAsync(User);
            // var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            // if (result.Succeeded) { await _signInManager.RefreshSignInAsync(user); ... }

            TempData["PasswordChanged"] = "true";
            return RedirectToAction(nameof(Profile));
        }

        // ─────────────────────────────────────────────────────
        // LOGOUT
        // ─────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // TODO: Connect ASP.NET Core Identity
            // await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        // GET logout for convenience (redirect to POST form)
        [HttpGet]
        public IActionResult Logout(string? returnUrl)
        {
            // TODO: use POST-based logout only after Identity is connected
            return RedirectToAction(nameof(Login));
        }
    }
}
