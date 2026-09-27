using System;
using System.Web;
using System.Web.Routing;

namespace CyberArenaWebForms
{
    /// <summary>
    /// ASP.NET Web Forms Global Application class.
    /// Handles application-level events: startup, error, session, etc.
    /// </summary>
    public class Global : HttpApplication
    {
        // ── Application Start ──────────────────────────────────────
        protected void Application_Start(object sender, EventArgs e)
        {
            // Register any routes (friendly URLs) if needed
            RouteTable.Routes.MapPageRoute(
                "Default",
                "",
                "~/Default.aspx"
            );
        }

        // ── Session Start ──────────────────────────────────────────
        protected void Session_Start(object sender, EventArgs e)
        {
            // Session started — can initialize default session values here
        }

        // ── Begin Request ──────────────────────────────────────────
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Runs on every request — can add global request logic here
        }

        // ── Authentication ─────────────────────────────────────────
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        // ── Global Error Handler ───────────────────────────────────
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex != null)
            {
                // Log error (extend with a proper logger if needed)
                System.Diagnostics.Debug.WriteLine("[CyberArena Error] " + ex.Message);
            }

            // Redirect to custom error page for unhandled exceptions
            // (Web.config customErrors also handles this)
        }

        // ── Session End ────────────────────────────────────────────
        protected void Session_End(object sender, EventArgs e)
        {
            // Session expired or abandoned
        }

        // ── Application End ────────────────────────────────────────
        protected void Application_End(object sender, EventArgs e)
        {
            // Application shutting down
        }
    }
}
