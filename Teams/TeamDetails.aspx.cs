using System;
using System.Web.UI;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;
using System.Web.Script.Serialization;

namespace CyberArenaWebForms.Teams
{
    public partial class TeamDetailsPage : Page
    {
        public TeamDetailsViewModel VM { get; private set; }
        
        // Properties used in .aspx script block
        protected string ScoreHistoryJson { get; set; } = "[]";
        protected string ScoreLabelsJson { get; set; } = "[]";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireUserSession(this.Page);

            if (!IsPostBack)
            {
                int id = ParseId();
                if (id <= 0) { Response.Redirect("~/Teams/Teams.aspx", true); return; }

                VM = TeamMockData.GetDetail(id);
                if (VM == null) { Response.Redirect("~/Teams/Teams.aspx", true); return; }

                BindPage();
            }
        }

        private void BindPage()
        {
            Page.Title = VM.Name + " â€” CyberArena";
            litPageTitle.Text = System.Web.HttpUtility.HtmlEncode(VM.Name);
            
            var master = Master as SiteMaster;
            if (master != null) master.PageTitle = "Teams Â· " + System.Web.HttpUtility.HtmlEncode(VM.Name);

            litAvatarInitials.Text = VM.Name.Length > 0 ? System.Web.HttpUtility.HtmlEncode(VM.Name.Substring(0, 1).ToUpper()) : "?";
            litTeamName.Text = System.Web.HttpUtility.HtmlEncode(VM.Name);

            if (VM.IsMyTeam)
            {
                pnlMyTeamBadge.Visible = true;
            }

            spanRankStatus.Attributes["class"] = "ca-comp-status " + (VM.Rank == 1 ? "active" : VM.Rank <= 3 ? "upcoming" : "ended");
            litRank.Text = VM.Rank.ToString();
            litGlobalRank.Text = VM.Rank.ToString();

            if (!string.IsNullOrEmpty(VM.Description))
            {
                pnlDescription.Visible = true;
                litDescription.Text = System.Web.HttpUtility.HtmlEncode(VM.Description);
            }

            litCaptain.Text = System.Web.HttpUtility.HtmlEncode(VM.CaptainUsername);
            litMemberCount.Text = $"{VM.Members.Count} member{(VM.Members.Count != 1 ? "s" : "")}";
            litMemberCountTitle.Text = VM.Members.Count.ToString();
            litCountry.Text = System.Web.HttpUtility.HtmlEncode(VM.Country);
            litCreatedAt.Text = VM.CreatedAt.ToString("MMM yyyy");

            litScore.Text = VM.Score.ToString("N0");
            litChallengesSolved.Text = VM.ChallengesSolved.ToString();
            
            if (VM.IsCaptain)
            {
                pnlInviteButton.Visible = true;
            }

            rptMembers.DataSource = VM.Members;
            rptMembers.DataBind();

            litSolvedChallengesText.Text = VM.ChallengesSolved.ToString();
            litTotalChallengesText.Text = VM.TotalChallenges.ToString();
            litSolvedPercentText.Text = VM.SolvedPercent.ToString();
            litSolvedFooter.Text = VM.ChallengesSolved.ToString();
            litTotalFooter.Text = VM.TotalChallenges.ToString();

            // Prepare JSON for chart
            var serializer = new JavaScriptSerializer();
            ScoreHistoryJson = serializer.Serialize(VM.ScoreHistory);
            ScoreLabelsJson = serializer.Serialize(VM.ScoreHistoryLabels);
        }

        private int ParseId()
        {
            int id;
            return int.TryParse(Request.QueryString["id"], out id) ? id : 0;
        }
    }
}

