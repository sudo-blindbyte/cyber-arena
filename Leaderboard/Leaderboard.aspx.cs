using System;
using System.Linq;
using System.Web.UI;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;

namespace CyberArenaWebForms.Leaderboard
{
    /// <summary>
    /// Leaderboard.aspx.cs â€” Code-behind for the Leaderboard page.
    ///
    /// Converted from:
    ///   LeaderboardController.Index(string q, string filter)  â†’  Page_Load
    ///
    /// Filtering:
    ///   â€¢ Server-side: reads ?q= query string and pre-filters the list.
    ///   â€¢ Client-side: the same search box also drives the JS live-filter
    ///     so typing updates the table without a page reload.
    ///
    /// Podium section:
    ///   â€¢ pnlPodium is only visible when the unfiltered list contains
    ///     rank-1/2/3 entries (same as @if (Model.Gold != null â€¦) in Razor).
    /// </summary>
    public partial class LeaderboardPage : Page
    {
        // Exposed to .aspx for <%= VM.X %> inline expressions
        public LeaderboardViewModel VM { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireUserSession(this.Page);
            Page.Title = "Leaderboard â€” CyberArena";

            var master = Master as SiteMaster;
            if (master != null) master.PageTitle = "Leaderboard";

            if (!IsPostBack)
            {
                VM = BuildViewModel();
                BindPage();
            }
        }

        // â”€â”€ Build ViewModel (mirrors LeaderboardController.Index) â”€â”€
        private LeaderboardViewModel BuildViewModel()
        {
            string q      = Request.QueryString["q"];
            string filter = Request.QueryString["filter"] ?? "All";

            var entries = LeaderboardMockData.GetAll();

            // Server-side search (mirrors MVC controller)
            if (!string.IsNullOrWhiteSpace(q))
                entries = entries.Where(e =>
                    e.Username.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    e.TeamName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            // Get current username from session (fall back to mock)
            string sessionUser = SessionHelper.GetUsername();
            string currentUser = (sessionUser != "Guest") ? sessionUser : "h4x0r_pro";
            int    currentRank = 6; // TODO: look up from DB when connected

            return new LeaderboardViewModel
            {
                Entries         = entries,
                SearchQuery     = q,
                FilterBy        = filter,
                CurrentUserRank = currentRank,
                CurrentUsername = currentUser,
            };
        }

        // â”€â”€ Bind all controls to the ViewModel â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        private void BindPage()
        {
            // Current user rank banner
            litCurrentRank.Text     = VM.CurrentUserRank.ToString();
            litCurrentUsername.Text = System.Web.HttpUtility.HtmlEncode(VM.CurrentUsername);

            // Entry count
            litEntryCount.Text = VM.Entries.Count.ToString();
            litFooterCount.Text = VM.Entries.Count.ToString();

            // â”€â”€ Podium (top 3) â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            // Always pull podium from the FULL unfiltered list so the
            // podium stays visible even when the user searches.
            var allEntries = LeaderboardMockData.GetAll();
            var gold   = allEntries.FirstOrDefault(e => e.Rank == 1);
            var silver = allEntries.FirstOrDefault(e => e.Rank == 2);
            var bronze = allEntries.FirstOrDefault(e => e.Rank == 3);

            if (gold != null && silver != null && bronze != null)
            {
                pnlPodium.Visible = true;

                litGoldInitials.Text   = System.Web.HttpUtility.HtmlEncode(gold.AvatarInitials);
                litGoldName.Text       = System.Web.HttpUtility.HtmlEncode(gold.Username);
                litGoldTeam.Text       = System.Web.HttpUtility.HtmlEncode(gold.TeamName);
                litGoldScore.Text      = gold.TotalScore.ToString("N0");

                litSilverInitials.Text = System.Web.HttpUtility.HtmlEncode(silver.AvatarInitials);
                litSilverName.Text     = System.Web.HttpUtility.HtmlEncode(silver.Username);
                litSilverTeam.Text     = System.Web.HttpUtility.HtmlEncode(silver.TeamName);
                litSilverScore.Text    = silver.TotalScore.ToString("N0");

                litBronzeInitials.Text = System.Web.HttpUtility.HtmlEncode(bronze.AvatarInitials);
                litBronzeName.Text     = System.Web.HttpUtility.HtmlEncode(bronze.Username);
                litBronzeTeam.Text     = System.Web.HttpUtility.HtmlEncode(bronze.TeamName);
                litBronzeScore.Text    = bronze.TotalScore.ToString("N0");
            }

            // â”€â”€ Full table â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            rptLeaderboard.DataSource = VM.Entries;
            rptLeaderboard.DataBind();
        }
    }
}

