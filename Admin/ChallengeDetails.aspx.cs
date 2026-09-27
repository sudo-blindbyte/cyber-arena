using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;

namespace CyberArenaWebForms.Admin
{
    public partial class ChallengeDetailsPage : Page
    {
        public AdminChallengeListItemViewModel VM { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireAdminSession(this.Page);
            
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int id))
            {
                VM = AdminMockData.GetAdminById(id);
                if (VM == null)
                {
                    Response.Redirect("~/Admin/Challenges.aspx");
                    return;
                }
            }
            else
            {
                Response.Redirect("~/Admin/Challenges.aspx");
                return;
            }

            var master = Master as AdminMaster;
            if (master != null)
            {
                master.PageTitle = $"Challenge #{VM.Id} â€” {VM.Title}";
            }
            Page.Title = $"Challenge #{VM.Id} â€” {VM.Title} â€” CyberArena";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // TODO: EF Core delete
            SessionHelper.SetFlash("AdminSuccess", $"Challenge #{VM.Id} deleted successfully.");
            Response.Redirect("~/Admin/Challenges.aspx");
        }
    }
}

