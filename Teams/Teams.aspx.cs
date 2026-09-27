using System;
using System.Linq;
using System.Web.UI;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;

namespace CyberArenaWebForms.Teams
{
    public partial class TeamsPage : Page
    {
        public TeamListViewModel VM { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireUserSession(this.Page);
            Page.Title = "Teams â€” CyberArena";

            var master = Master as SiteMaster;
            if (master != null) master.PageTitle = "Teams";

            if (!IsPostBack)
            {
                VM = BuildViewModel();
                BindPage();
                ShowFlash();
            }
        }

        private TeamListViewModel BuildViewModel()
        {
            string q = Request.QueryString["q"];
            var teams = TeamMockData.GetAll();

            if (!string.IsNullOrWhiteSpace(q))
            {
                teams = teams.Where(t => 
                    t.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    t.CaptainUsername.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            return new TeamListViewModel
            {
                Teams = teams,
                SearchQuery = q,
                UserHasTeam = true,
                MyTeamId = 3 // Hardcoded mock value
            };
        }

        private void BindPage()
        {
            litTeamCount.Text = VM.Teams.Count.ToString();

            if (!VM.UserHasTeam)
            {
                pnlCreateTeam.Visible = true;
            }
            else
            {
                pnlMyTeam.Visible = true;
                lnkMyTeam.NavigateUrl = ResolveUrl($"~/Teams/TeamDetails.aspx?id={VM.MyTeamId}");
            }

            if (VM.Teams.Count == 0 && string.IsNullOrWhiteSpace(VM.SearchQuery))
            {
                pnlNoTeams.Visible = true;
                pnlTeamsGrid.Visible = false;
            }
            else
            {
                pnlTeamsGrid.Visible = true;
                rptTeams.DataSource = VM.Teams;
                rptTeams.DataBind();
            }
        }

        // private void ShowFlash()
        // {
        //     string msg = SessionHelper.GetFlash("AdminSuccess");
        //     if (!string.IsNullOrEmpty(msg))
        //     {
        //         pnlFlash.Visible = true;
        //         litFlashMsg.Text = System.Web.HttpUtility.HtmlEncode(msg);
        //     }
        // }
    }
}

