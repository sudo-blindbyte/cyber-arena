using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;

namespace CyberArenaWebForms
{
    /// <summary>
    /// Code-behind for Site.Master — the main user-facing master page.
    /// Handles active nav detection, flash messages (replacing TempData),
    /// and exposes properties for content pages to set the page title / breadcrumb.
    /// </summary>
    public partial class SiteMaster : MasterPage
    {
        // ── Public properties (set by content pages in Page_Load) ──

        /// <summary>Sets the H1 page title shown in the topbar.</summary>
        public string PageTitle
        {
            get { return litPageTitle.Text; }
            set { litPageTitle.Text = value; }
        }

        /// <summary>
        /// Sets the breadcrumb text. When set, the breadcrumb nav becomes visible.
        /// </summary>
        public string Breadcrumb
        {
            get { return litBreadcrumb.Text; }
            set
            {
                litBreadcrumb.Text       = value;
                breadcrumb_nav.Visible   = !string.IsNullOrEmpty(value);
            }
        }

        // ── Lifecycle ──────────────────────────────────────────────
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFlashMessages();
            }
        }

        // ── Flash / TempData replacement ───────────────────────────
        private void LoadFlashMessages()
        {
            // Demo mode banner
            string demoMode = SessionHelper.GetFlash("DemoMode");
            pnlDemoMode.Visible = !string.IsNullOrEmpty(demoMode);

            // Profile saved
            string profileSaved = SessionHelper.GetFlash("ProfileSaved");
            pnlProfileSaved.Visible = !string.IsNullOrEmpty(profileSaved);

            // Password changed
            string pwChanged = SessionHelper.GetFlash("PasswordChanged");
            pnlPasswordChanged.Visible = !string.IsNullOrEmpty(pwChanged);

            // General success (e.g. admin actions performed from user pages)
            string success = SessionHelper.GetFlash("Success");
            if (!string.IsNullOrEmpty(success))
            {
                litSuccess.Text    = success;
                pnlSuccess.Visible = true;
            }
        }

        // ── Active nav helper (used inline in .aspx with <%= %>) ──

        /// <summary>
        /// Returns "ca-nav-link active" if the current page URL contains
        /// the given segment, otherwise "ca-nav-link".
        /// </summary>
        public string IsActive(string segment)
        {
            string path = Request.AppRelativeCurrentExecutionFilePath ?? string.Empty;
            return path.IndexOf(segment, StringComparison.OrdinalIgnoreCase) >= 0
                ? "ca-nav-link active"
                : "ca-nav-link";
        }

        // ── User info helpers (used inline in .aspx with <%= %>) ──

        /// <summary>Returns the logged-in username from session.</summary>
        public string GetUsername()
        {
            return SessionHelper.GetUsername();
        }

        /// <summary>Returns the logged-in user email from session.</summary>
        public string GetUserEmail()
        {
            return SessionHelper.GetUserEmail();
        }

        /// <summary>
        /// Returns the user's initials (up to 2 characters) for the avatar chip.
        /// </summary>
        public string GetUserInitials()
        {
            string name = SessionHelper.GetUsername();
            if (string.IsNullOrWhiteSpace(name)) return "?";

            string[] parts = name.Trim().Split(new[] { ' ', '_', '-' },
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 2)
                return (parts[0][0].ToString() + parts[1][0].ToString()).ToUpper();

            return name.Length >= 2
                ? name.Substring(0, 2).ToUpper()
                : name.ToUpper();
        }
    }
}
