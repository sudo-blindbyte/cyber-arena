using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;

namespace CyberArenaWebForms
{
    /// <summary>
    /// Code-behind for Admin.Master — the admin panel master page.
    /// Converted from _AdminLayout.cshtml.
    ///
    /// Responsibilities:
    ///   • Enforce admin session on every admin page (RequireAdminSession).
    ///   • Expose Breadcrumb and AdminSuccess flash properties for content pages.
    ///   • Handle global admin logout.
    ///   • Provide AdmNav() helper for active-link highlighting.
    /// </summary>
    public partial class AdminMaster : MasterPage
    {
        // ── Public properties (set by content pages in Page_Load) ──

        /// <summary>
        /// Sets the breadcrumb text shown in the admin topbar.
        /// Defaults to "Admin".
        /// </summary>
        public string Breadcrumb
        {
            get { return litBreadcrumb.Text; }
            set { litBreadcrumb.Text = string.IsNullOrEmpty(value) ? "Admin" : value; }
        }

        /// <summary>
        /// Shows a success toast message (replaces TempData["AdminSuccess"]).
        /// Set this before Page renders; the panel becomes visible automatically.
        /// </summary>
        public string AdminSuccess
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    litAdminSuccess.Text   = value;
                    pnlAdminToast.Visible  = true;
                }
            }
        }

        // ── Lifecycle ──────────────────────────────────────────────
        protected void Page_Load(object sender, EventArgs e)
        {
            // ── Enforce admin session on every request ─────────────
            SessionHelper.RequireAdminSession(this.Page);

            if (!IsPostBack)
            {
                // Load any pending flash messages (set via SessionHelper.SetFlash)
                string success = SessionHelper.GetFlash("AdminSuccess");
                if (!string.IsNullOrEmpty(success))
                {
                    litAdminSuccess.Text  = success;
                    pnlAdminToast.Visible = true;
                }
            }
        }

        // ── Admin logout ───────────────────────────────────────────
        protected void btnAdminLogout_Click(object sender, EventArgs e)
        {
            SessionHelper.ClearAdminSession();
            Response.Redirect("~/Admin/AdminLogin.aspx", true);
        }

        // ── Active nav helper (used inline in .aspx with <%= %>) ──

        /// <summary>
        /// Returns "adm-nav-link active" if the current page's filename
        /// matches the given page name (case-insensitive), otherwise "adm-nav-link".
        /// </summary>
        public string AdmNav(string pageName)
        {
            string path = Request.AppRelativeCurrentExecutionFilePath ?? string.Empty;
            return path.IndexOf(pageName, StringComparison.OrdinalIgnoreCase) >= 0
                ? "adm-nav-link active"
                : "adm-nav-link";
        }

        // ── Admin user info ────────────────────────────────────────

        /// <summary>Returns the admin username from session.</summary>
        public string GetAdminUsername()
        {
            return SessionHelper.GetAdminUsername();
        }
    }
}
