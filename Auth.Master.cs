using System;
using System.Web.UI;

namespace CyberArenaWebForms
{
    /// <summary>
    /// Code-behind for Auth.Master — the authentication pages master page.
    /// Used by Login, Register, ForgotPassword, ResetPassword, and VerifyEmail.
    /// Kept minimal: no session checks here — individual pages handle their own logic.
    /// </summary>
    public partial class AuthMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Auth master intentionally lightweight.
            // Individual auth pages handle session checks and redirects themselves.
        }
    }
}
