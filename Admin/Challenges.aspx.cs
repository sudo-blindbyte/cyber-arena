using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;
using System.Linq;

namespace CyberArenaWebForms.Admin
{
    public partial class ChallengesPage : Page
    {
        public AdminChallengeListViewModel VM { get; private set; } = new AdminChallengeListViewModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireAdminSession(this.Page);
            
            var master = Master as AdminMaster;
            if (master != null)
            {
                master.PageTitle = "Challenge Management";
            }
            Page.Title = "Challenge Management â€” CyberArena";

            string flashMsg = SessionHelper.GetFlash("AdminSuccess");
            if (!string.IsNullOrEmpty(flashMsg))
            {
                pnlSuccess.Visible = true;
                litSuccessMsg.Text = System.Web.HttpUtility.HtmlEncode(flashMsg);
            }

            if (!IsPostBack)
            {
                BindPage();
            }
        }

        private void BindPage()
        {
            var challenges = AdminMockData.GetAdminChallenges();
            
            string q = Request.QueryString["q"];
            string category = Request.QueryString["category"];
            string status = Request.QueryString["status"];

            if (!string.IsNullOrWhiteSpace(q))
            {
                challenges = challenges.Where(c => 
                    c.Title.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    c.Category.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            if (!string.IsNullOrWhiteSpace(category) && category != "All")
            {
                challenges = challenges.Where(c => c.Category == category).ToList();
            }
            if (!string.IsNullOrWhiteSpace(status) && status != "All")
            {
                challenges = challenges.Where(c => c.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            VM.Challenges = challenges;
            VM.SearchQuery = q;
            VM.FilterCategory = category;
            VM.FilterStatus = status;

            rptChallenges.DataSource = VM.Challenges;
            rptChallenges.DataBind();
        }

        protected void btnHiddenDelete_Click(object sender, EventArgs e)
        {
            string idStr = hfDeleteId.Value;
            if (int.TryParse(idStr, out int id))
            {
                // TODO: EF Core â€” find, remove, save
                
                SessionHelper.SetFlash("AdminSuccess", $"Challenge #{id} has been deleted.");
                Response.Redirect("~/Admin/Challenges.aspx");
            }
        }
    }
}

