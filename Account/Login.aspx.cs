using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;

namespace CyberArenaWebForms.Account
{
    /// <summary>
    /// Login.aspx.cs â€” Code-behind for the Login page.
    ///
    /// Converted from:
    ///   â€¢ AccountController.Login (GET)  â†’ Page_Load (non-postback)
    ///   â€¢ AccountController.Login (POST) â†’ btnLogin_Click
    ///
    /// Demo behaviour matches the original controller:
    ///   Any credentials â†’ session created â†’ redirect to Dashboard.
    ///   Empty / invalid fields â†’ ASP.NET validator controls show inline errors.
    ///   Bad credentials (wrong user/pass) â†’ pnlError panel shown.
    /// </summary>
    public partial class LoginPage : Page
    {
        // â”€â”€ Page Load (GET equivalent) â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Sign In â€” CyberArena";

                // If already logged in, go straight to Dashboard
                if (SessionHelper.IsUserLoggedIn())
                {
                    Response.Redirect("~/Dashboard/Dashboard.aspx", true);
                    return;
                }

                // Clear any lingering error panel
                pnlError.Visible = false;
            }
        }

        // â”€â”€ Login Submit (POST equivalent) â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // ASP.NET validators already fired client-side.
            // Re-validate server-side (defence-in-depth: catches JS-disabled clients).
            if (!Page.IsValid)
                return;

            string email    = txtEmail.Text.Trim();
            string password = txtPassword.Text;   // raw â€” never trim passwords
            bool   remember = chkRememberMe.Checked;

            // â”€â”€ Demo mode â€” accept any non-empty credentials â”€â”€â”€â”€â”€â”€â”€
            // TODO: Replace with real authentication (e.g. database lookup,
            //       bcrypt hash comparison, etc.) when a database is connected.
            //
            // Example real-auth flow (commented out):
            //   var user = UserRepository.FindByEmail(email);
            //   if (user == null || !PasswordHelper.Verify(password, user.PasswordHash))
            //   {
            //       ShowError("Invalid email or password.");
            //       return;
            //   }

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Please enter your email and password.");
                return;
            }

            // Simulate a hardcoded demo account check (mirrors MVC demo behaviour)
            // In the MVC version ANY valid model â†’ Dashboard redirect + DemoMode flash.
            // We do the same here.
            bool validCredentials = true; // demo: always pass if fields are non-empty

            if (!validCredentials)
            {
                ShowError("Invalid email or password. Please try again.");
                return;
            }

            // â”€â”€ Create user session â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            // Derive a display username from the email (e.g. "alex@example.com" â†’ "alex")
            string username = email.Contains("@")
                ? email.Split('@')[0]
                : email;

            SessionHelper.SetUserSession(username, email);

            // Set Demo Mode flash (replaces TempData["DemoMode"] from the controller)
            SessionHelper.SetFlash("DemoMode", "true");

            // â”€â”€ Redirect â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            // Check for a returnUrl in the query string (mirrors asp-route-returnUrl)
            string returnUrl = Request.QueryString["returnUrl"];
            if (!string.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                Response.Redirect(returnUrl, true);
            }
            else
            {
                Response.Redirect("~/Dashboard/Dashboard.aspx", true);
            }
        }

        // â”€â”€ Helpers â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

        /// <summary>Shows the error panel with the given message.</summary>
        private void ShowError(string message)
        {
            litErrorMessage.Text = message;
            pnlError.Visible     = true;
        }

        /// <summary>
        /// Guards against open-redirect attacks â€” only allow relative URLs.
        /// Mirrors the LocalRedirect behaviour from ASP.NET Core.
        /// </summary>
        private bool IsLocalUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            // Must start with / but NOT // (protocol-relative)
            return url.StartsWith("/") && !url.StartsWith("//");
        }
    }
}

