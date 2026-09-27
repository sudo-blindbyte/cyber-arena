using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;

namespace CyberArenaWebForms
{
    public partial class AdminLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as SiteMaster;
            if (master != null)
            {
                master.PageTitle = "Admin Login";
            }

            if (!IsPostBack)
            {
                if (SessionHelper.IsAdminAuthenticated())
                {
                    Response.Redirect("~/Admin/Admin.aspx");
                }

                if (Request.QueryString["reason"] == "unauthorized")
                {
                    ShowError("Please authorise first.");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                ShowError("Username and password are required.");
                return;
            }

            if (user == "admin" && pass == "CyberArena@Admin2026!")
            {
                SessionHelper.SetAdminSession(user);
                Response.Redirect("~/Admin/Admin.aspx");
            }
            else
            {
                System.Threading.Thread.Sleep(500); // Deliberate delay
                ShowError("Invalid admin credentials.");
            }
        }

        private void ShowError(string msg)
        {
            pnlError.Visible = true;
            litErrorMsg.Text = System.Web.HttpUtility.HtmlEncode(msg);
        }
    }
}

