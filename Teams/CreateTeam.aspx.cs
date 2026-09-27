using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;

namespace CyberArenaWebForms.Teams
{
    public partial class CreateTeamPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireUserSession(this.Page);
            Page.Title = "Create Team â€” CyberArena";

            var master = Master as SiteMaster;
            if (master != null) master.PageTitle = "Teams Â· Create";

            if (!IsPostBack)
            {
                string sessionUser = SessionHelper.GetUsername();
                litCaptainName.Text = (sessionUser != "Guest") ? sessionUser : "h4x0r_pro";
            }
        }

        protected void btnCreateTeam_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string name = txtName.Text.Trim();
            // string desc = txtDescription.Text.Trim();
            // string country = txtCountry.Text.Trim();

            // TODO: EF Core database integration here

            SessionHelper.SetFlash("AdminSuccess", $"Team \"{name}\" created successfully!");
            Response.Redirect("~/Teams/Teams.aspx");
        }
    }
}

