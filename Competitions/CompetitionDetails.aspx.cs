using System;
using System.Linq;
using System.Web.UI;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;

namespace CyberArenaWebForms.Competitions
{
    public partial class CompetitionDetailsPage : Page
    {
        public CompetitionDetailsViewModel VM { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireUserSession(this.Page);

            int id = ParseId();
            if (id <= 0) { Response.Redirect("~/Competitions/Competitions.aspx", true); return; }

            VM = CompetitionMockData.GetDetail(id);
            if (VM == null) { Response.Redirect("~/Competitions/Competitions.aspx", true); return; }

            if (!IsPostBack)
            {
                BindPage();
                ShowFlash();
            }
        }

        private void BindPage()
        {
            Page.Title = VM.Name + " â€” CyberArena";
            litPageTitle.Text = System.Web.HttpUtility.HtmlEncode(VM.Name);

            var master = Master as SiteMaster;
            if (master != null) master.PageTitle = "Competitions Â· " + System.Web.HttpUtility.HtmlEncode(VM.Name);

            heroIcon.Attributes["class"] = "comp-icon " + VM.StatusClass;
            litCompName.Text = System.Web.HttpUtility.HtmlEncode(VM.Name);
            spanStatus.Attributes["class"] = "ca-comp-status " + VM.StatusClass;
            litStatusUpper.Text = VM.Status.ToUpper();

            if (VM.IsJoined)
            {
                pnlJoinedBadge.Visible = true;
            }

            litDescription.Text = System.Web.HttpUtility.HtmlEncode(VM.Description);
            litAboutDesc.Text = System.Web.HttpUtility.HtmlEncode(VM.Description);

            // Join buttons
            if (VM.Status == "active" && !VM.IsJoined)
            {
                pnlJoinAction.Visible = true;
                btnJoinActive.Visible = true;
                btnJoinActive.Text = "<i class=\"fas fa-bolt me-2\"></i>Join Competition"; // We can't use HTML in asp:Button text easily, so it will just be "Join Competition" but we can fix that later or just leave it.
                btnJoinActive.Text = "Join Competition"; 
            }
            else if (VM.Status == "upcoming" && !VM.IsJoined)
            {
                pnlJoinAction.Visible = true;
                btnRegisterUpcoming.Visible = true;
                btnRegisterUpcoming.Text = "Register";
            }

            // Stat Row
            litChallengeCount.Text = VM.Challenges.Count.ToString();
            litParticipantCount.Text = VM.ParticipantCount.ToString();
            litFormat.Text = VM.Format;

            if (VM.Status == "active")
            {
                litTimeDisplay.Text = VM.TimeRemaining;
                litTimeLabel.Text = "Time Left";
            }
            else if (VM.Status == "upcoming")
            {
                litTimeDisplay.Text = VM.StartAt.ToString("dd MMM");
                litTimeLabel.Text = "Starts";
            }
            else
            {
                litTimeDisplay.Text = "Finished";
                litTimeLabel.Text = "Status";
            }

            // Schedule
            litStartAt.Text = VM.StartAt.ToString("dd MMM yyyy HH:mm");
            litEndAt.Text = VM.EndAt.ToString("dd MMM yyyy HH:mm");

            // Tabs
            litTabChallengeCount.Text = VM.Challenges.Count.ToString();

            // Overview Panel - Capacity
            if (VM.MaxParticipants > 0)
            {
                pnlCapacity.Visible = true;
                litCapacityLabel.Text = $"{VM.ParticipantCount} / {VM.MaxParticipants}";
                int pct = (int)((double)VM.ParticipantCount / VM.MaxParticipants * 100);
                
                string classes = "ca-progress-bar";
                if (pct > 80) classes += " warning";
                
                capacityBar.Attributes["class"] = classes;
                capacityBar.Attributes["data-width"] = pct.ToString();
            }

            // Overview Panel - Categories
            var cats = VM.Challenges.GroupBy(c => c.Category)
                        .Select(g => new { 
                            Cat = g.Key, 
                            Count = g.Count(),
                            Pct = VM.Challenges.Count > 0 ? (int)((double)g.Count() / VM.Challenges.Count * 100) : 0
                        })
                        .OrderByDescending(x => x.Count).ToList();
            rptCategories.DataSource = cats;
            rptCategories.DataBind();

            // Challenges Panel
            rptChallenges.DataSource = VM.Challenges;
            rptChallenges.DataBind();

            // Leaderboard Panel
            rptLeaderboard.DataSource = VM.Leaderboard;
            rptLeaderboard.DataBind();

            // Rules Panel
            var rulesList = VM.Rules.Split('\n')
                                    .Where(r => !string.IsNullOrWhiteSpace(r))
                                    .Select(r => r.TrimStart('0','1','2','3','4','5','6','7','8','9','.',' '))
                                    .ToList();
            rptRules.DataSource = rulesList;
            rptRules.DataBind();
        }

        protected void btnJoin_Click(object sender, EventArgs e)
        {
            // TODO: EF Core â€” register current user in competition
            SessionHelper.SetFlash("AdminSuccess", "You have successfully joined the competition!");
            Response.Redirect($"~/Competitions/CompetitionDetails.aspx?id={VM.Id}");
        }

        ///* private void ShowFlash()
        //{
        //    string msg = SessionHelper.GetFlash("AdminSuccess");
        //    if (!string.IsNullOrEmpty(msg))
        //    {
        //        pnlFlash.Visible = true;
        //        litFlashMsg.Text = System.Web.HttpUtility.HtmlEncode(msg);
        //    }
        //}

        private int ParseId()
        {
            int id;
            return int.TryParse(Request.QueryString["id"], out id) ? id : 0;
        }
    }
}

