using System;
using System.Collections.Generic;
using System.Web.UI;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;

namespace CyberArenaWebForms.Dashboard
{
    /// <summary>
    /// Dashboard.aspx.cs â€” Code-behind for the participant Dashboard page.
    ///
    /// Converted from:
    ///   DashboardController.Index() â†’ Page_Load
    ///
    /// All mock data is ported exactly from the original controller.
    /// The ViewModel is exposed as a public property (VM) so that the
    /// .aspx markup can use <%= VM.Property %> inline expressions cleanly.
    ///
    /// Data binding:
    ///   â€¢ asp:Literal controls   â† scalar values (score, rank, usernameâ€¦)
    ///   â€¢ asp:Repeater controls  â† collections (submissions, chart, categoriesâ€¦)
    ///   â€¢ asp:Panel visibility   â† replaces @if / @else blocks
    /// </summary>
    public partial class DashboardPage : Page
    {
        // Exposed to .aspx markup via <%= VM.X %> expressions
        public DashboardViewModel VM { get; private set; }

        // â”€â”€ Page Load (GET equivalent) â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        protected void Page_Load(object sender, EventArgs e)
        {
            // Enforce user session (redirect to login if not authenticated)
            SessionHelper.RequireUserSession(this.Page);

            // Set page title and master page properties
            Page.Title = "Dashboard â€” CyberArena";

            var master = Master as SiteMaster;
            if (master != null)
            {
                master.PageTitle = "Dashboard";
            }

            if (!IsPostBack)
            {
                VM = BuildViewModel();
                BindPage();
            }
        }

        // â”€â”€ Build ViewModel (ported from DashboardController.Index) â”€â”€
        private DashboardViewModel BuildViewModel()
        {
            // Use the logged-in username from session; fall back to mock data
            string sessionUser = SessionHelper.GetUsername();
            string displayName = (sessionUser != "Guest") ? sessionUser : "h4x0r_pro";

            return new DashboardViewModel
            {
                Username          = displayName,
                AvatarInitials    = "AC",
                TotalScore        = 4250,
                ChallengesSolved  = 34,
                TotalChallenges   = 120,
                CurrentRank       = 7,
                TotalParticipants = 214,
                TeamName          = "ByteBreakers",
                TeamRank          = 3,
                ScoreDelta        = 350,
                RankDelta         = 2,

                RecentSubmissions = new List<RecentSubmissionItem>
                {
                    new RecentSubmissionItem { ChallengeName = "SQL Injection Basics",  Category = "Web",      Points = 100, Difficulty = "easy",   SolvedAt = DateTime.UtcNow.AddMinutes(-25) },
                    new RecentSubmissionItem { ChallengeName = "Buffer Overflow 101",   Category = "Pwn",      Points = 250, Difficulty = "medium", SolvedAt = DateTime.UtcNow.AddHours(-2) },
                    new RecentSubmissionItem { ChallengeName = "RSA Weak Key",          Category = "Crypto",   Points = 300, Difficulty = "medium", SolvedAt = DateTime.UtcNow.AddHours(-5) },
                    new RecentSubmissionItem { ChallengeName = "Wireshark Hunt",        Category = "Forensics",Points = 150, Difficulty = "easy",   SolvedAt = DateTime.UtcNow.AddHours(-8) },
                    new RecentSubmissionItem { ChallengeName = "Kernel Exploit 0-day",  Category = "Pwn",      Points = 500, Difficulty = "expert", SolvedAt = DateTime.UtcNow.AddDays(-1) },
                },

                RecentActivities = new List<ActivityItem>
                {
                    new ActivityItem { Icon = "fa-trophy",        IconColorClass = "warning", Text = "You climbed to <strong>Rank #7</strong> on the global leaderboard.",  Timestamp = DateTime.UtcNow.AddMinutes(-10) },
                    new ActivityItem { Icon = "fa-flag-checkered",IconColorClass = "success", Text = "Solved <strong>SQL Injection Basics</strong> (+100 pts)",             Timestamp = DateTime.UtcNow.AddMinutes(-25) },
                    new ActivityItem { Icon = "fa-users",         IconColorClass = "accent",  Text = "Joined team <strong>ByteBreakers</strong>",                           Timestamp = DateTime.UtcNow.AddHours(-3) },
                    new ActivityItem { Icon = "fa-shield-halved", IconColorClass = "info",    Text = "Registered for <strong>NIT CTF 2026</strong> competition",            Timestamp = DateTime.UtcNow.AddHours(-6) },
                    new ActivityItem { Icon = "fa-circle-check",  IconColorClass = "success", Text = "Solved <strong>Buffer Overflow 101</strong> (+250 pts)",              Timestamp = DateTime.UtcNow.AddHours(-2) },
                },

                ActiveCompetitions = new List<ActiveCompetition>
                {
                    new ActiveCompetition
                    {
                        Name              = "NIT CTF 2026",
                        Status            = "active",
                        EndsAt            = DateTime.UtcNow.AddDays(2).AddHours(6),
                        TeamRank          = 3,
                        TotalTeams        = 48,
                        ChallengesSolved  = 8,
                        TotalChallenges   = 20,
                        Score             = 1800
                    },
                    new ActiveCompetition
                    {
                        Name              = "CyberArena Weekly #12",
                        Status            = "active",
                        EndsAt            = DateTime.UtcNow.AddHours(18),
                        TeamRank          = 5,
                        TotalTeams        = 92,
                        ChallengesSolved  = 4,
                        TotalChallenges   = 10,
                        Score             = 650
                    }
                },

                ScoreHistory = new List<ScoreHistoryPoint>
                {
                    new ScoreHistoryPoint { Label = "7d",  Score = 1200, HeightPercent = 28 },
                    new ScoreHistoryPoint { Label = "6d",  Score = 1500, HeightPercent = 35 },
                    new ScoreHistoryPoint { Label = "5d",  Score = 1500, HeightPercent = 35 },
                    new ScoreHistoryPoint { Label = "4d",  Score = 2100, HeightPercent = 49 },
                    new ScoreHistoryPoint { Label = "3d",  Score = 2800, HeightPercent = 65 },
                    new ScoreHistoryPoint { Label = "2d",  Score = 3600, HeightPercent = 84 },
                    new ScoreHistoryPoint { Label = "1d",  Score = 3900, HeightPercent = 91 },
                    new ScoreHistoryPoint { Label = "Now", Score = 4250, HeightPercent = 100, IsHighest = true },
                },

                CategoryProgress = new List<CategoryProgress>
                {
                    new CategoryProgress { Name = "Web",      Icon = "fa-globe",           Solved = 10, Total = 25, ColorClass = "accent" },
                    new CategoryProgress { Name = "Pwn",      Icon = "fa-terminal",        Solved = 8,  Total = 20, ColorClass = "danger" },
                    new CategoryProgress { Name = "Crypto",   Icon = "fa-key",             Solved = 7,  Total = 18, ColorClass = "warning" },
                    new CategoryProgress { Name = "Forensics",Icon = "fa-magnifying-glass", Solved = 5, Total = 22, ColorClass = "info" },
                    new CategoryProgress { Name = "Reversing",Icon = "fa-code",            Solved = 3,  Total = 15, ColorClass = "success" },
                    new CategoryProgress { Name = "OSINT",    Icon = "fa-satellite-dish",  Solved = 1,  Total = 10, ColorClass = "accent" },
                },
            };
        }

        // â”€â”€ Bind all server controls to the ViewModel data â”€â”€â”€â”€â”€â”€â”€â”€â”€
        private void BindPage()
        {
            // â”€â”€ Header â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            litUsername.Text = System.Web.HttpUtility.HtmlEncode(VM.Username);
            litDate.Text     = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            if (!string.IsNullOrEmpty(VM.TeamName))
            {
                pnlTeamName.Visible = true;
                litTeamName.Text    = System.Web.HttpUtility.HtmlEncode(VM.TeamName);
            }

            // â”€â”€ Stat cards â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            litTotalScore.Text      = VM.TotalScore.ToString("N0");
            litScoreDelta.Text      = VM.ScoreDelta.ToString();
            litChallengesSolved.Text = VM.ChallengesSolved.ToString();
            litTotalChallenges.Text  = VM.TotalChallenges.ToString();
            litSolvedPercent.Text    = VM.SolvedPercent.ToString();
            litCurrentRank.Text      = VM.CurrentRank.ToString();
            litTeamRank.Text         = VM.TeamRank.ToString();
            litTeamNameStat.Text     = System.Web.HttpUtility.HtmlEncode(VM.TeamName ?? "No team");
            litChartDelta.Text       = VM.ScoreDelta.ToString();

            // Rank delta display (up/down arrow + colour class)
            string rankDirClass = VM.RankDelta > 0 ? "up" : "down";
            string rankDirIcon  = VM.RankDelta > 0 ? "up" : "down";
            string rankPrefix   = VM.RankDelta > 0 ? "+" : "";
            litRankDelta.Text = string.Format(
                "<i class=\"fas fa-arrow-{0}\" aria-hidden=\"true\"></i> {1}{2} positions",
                rankDirIcon, rankPrefix, VM.RankDelta);

            // â”€â”€ Score history chart repeaters â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            rptScoreHistory.DataSource  = VM.ScoreHistory;
            rptScoreHistory.DataBind();
            rptChartLabels.DataSource   = VM.ScoreHistory;
            rptChartLabels.DataBind();

            // â”€â”€ Submissions table -â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            if (VM.RecentSubmissions.Count > 0)
            {
                pnlSubmissionsTable.Visible  = true;
                pnlSubmissionsEmpty.Visible  = false;
                rptSubmissions.DataSource    = VM.RecentSubmissions;
                rptSubmissions.DataBind();
            }
            else
            {
                pnlSubmissionsTable.Visible = false;
                pnlSubmissionsEmpty.Visible = true;
            }

            // â”€â”€ Category progress â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            rptCategoryProgress.DataSource = VM.CategoryProgress;
            rptCategoryProgress.DataBind();

            // â”€â”€ Active competitions â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            litCompCount.Text = VM.ActiveCompetitions.Count.ToString();
            if (VM.ActiveCompetitions.Count > 0)
            {
                pnlCompetitions.Visible      = true;
                pnlCompetitionsEmpty.Visible = false;
                rptCompetitions.DataSource   = VM.ActiveCompetitions;
                rptCompetitions.DataBind();
            }
            else
            {
                pnlCompetitions.Visible      = false;
                pnlCompetitionsEmpty.Visible = true;
            }

            // â”€â”€ Recent activity â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
            if (VM.RecentActivities.Count > 0)
            {
                pnlActivity.Visible      = true;
                pnlActivityEmpty.Visible = false;
                rptActivity.DataSource   = VM.RecentActivities;
                rptActivity.DataBind();
            }
            else
            {
                pnlActivity.Visible      = false;
                pnlActivityEmpty.Visible = true;
            }
        }
    }
}

