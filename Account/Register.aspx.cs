using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;

namespace CyberArenaWebForms.Account
{
    /// <summary>
    /// Register.aspx.cs â€” Code-behind for the Registration page.
    ///
    /// Converted from:
    ///   â€¢ AccountController.Register (GET)  â†’ Page_Load (non-postback)
    ///   â€¢ AccountController.Register (POST) â†’ btnRegister_Click
    ///
    /// Demo behaviour matches the original MVC controller:
    ///   Any valid form â†’ session created â†’ redirect to Dashboard
    ///   with a "Welcome to CyberArena, {username}!" success flash.
    ///
    /// Validation replaces data annotations from RegisterViewModel:
    ///   â€¢ [Required]                  â†’ RequiredFieldValidator (server + client)
    ///   â€¢ [EmailAddress]              â†’ RegularExpressionValidator (server + client)
    ///   â€¢ [StringLength(min/max)]     â†’ RegularExpressionValidator (server + client)
    ///   â€¢ [RegularExpression]         â†’ RegularExpressionValidator (server + client)
    ///   â€¢ [Compare("Password")]       â†’ CompareValidator (server + client)
    ///   â€¢ [Range(bool, true, true)]   â†’ CustomValidator (server + client)
    /// </summary>
    public partial class RegisterPage : Page
    {
        // â”€â”€ Page Load (GET equivalent) â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Create Account â€” CyberArena";

                // Already logged in â†’ go to Dashboard
                if (SessionHelper.IsUserLoggedIn())
                {
                    Response.Redirect("~/Dashboard/Dashboard.aspx", true);
                    return;
                }

                pnlError.Visible = false;
            }
        }

        // â”€â”€ Terms CustomValidator â€” server-side validate â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        protected void cvAcceptTerms_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            // chkAcceptTerms.Checked is the server-side equivalent of
            // [Range(typeof(bool), "true", "true")] on AcceptTerms property.
            args.IsValid = chkAcceptTerms.Checked;
        }

        // â”€â”€ Register Submit (POST equivalent) â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Re-validate server-side (covers JS-disabled browsers)
            if (!Page.IsValid)
                return;

            // Read form values
            string username = txtUsername.Text.Trim();
            string email    = txtEmail.Text.Trim();
            string password = txtPassword.Text;          // never trim passwords
            string teamName = txtTeamName.Text.Trim();   // optional

            // â”€â”€ Additional server-side cross-field checks â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

            // Password match (CompareValidator covers client-side; re-check server-side)
            if (password != txtConfirmPassword.Text)
            {
                ShowError("Passwords do not match. Please try again.");
                return;
            }

            // Password minimum length (8 chars, matching [StringLength(100, MinimumLength=8)])
            if (password.Length < 8)
            {
                ShowError("Password must be at least 8 characters.");
                return;
            }

            // â”€â”€ Demo mode â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            // TODO: Replace with real user creation (DB insert + password hashing)
            // when a database is connected.
            //
            // Example real flow (commented out):
            //   bool emailTaken = UserRepository.EmailExists(email);
            //   if (emailTaken) { ShowError("An account with this email already exists."); return; }
            //
            //   bool usernameTaken = UserRepository.UsernameExists(username);
            //   if (usernameTaken) { ShowError("This username is already taken."); return; }
            //
            //   string hash = PasswordHelper.Hash(password);
            //   UserRepository.Create(username, email, hash, teamName);

            // â”€â”€ Create session (demo â€” skip identity verification) â”€
            SessionHelper.SetUserSession(username, email);

            // Set flash messages (replaces TempData from the controller)
            SessionHelper.SetFlash("DemoMode", "true");
            SessionHelper.SetFlash("Success",
                $"Welcome to CyberArena, {HtmlEncode(username)}! Your account has been created.");

            // â”€â”€ Redirect to Dashboard â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            Response.Redirect("~/Dashboard/Dashboard.aspx", true);
        }

        // â”€â”€ Helpers â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

        /// <summary>Shows the error panel with the given message.</summary>
        private void ShowError(string message)
        {
            litErrorMessage.Text = message;
            pnlError.Visible     = true;
        }

        /// <summary>
        /// Basic HTML encode for user-supplied strings rendered into the page
        /// (prevents XSS in flash messages containing the username).
        /// </summary>
        private string HtmlEncode(string value)
        {
            return System.Web.HttpUtility.HtmlEncode(value);
        }
    }
}

