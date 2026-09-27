using System;
using System.Web.UI;
using System.Linq;
using System.Collections.Generic;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;
using System.Web.Script.Serialization;

namespace CyberArenaWebForms.Admin
{
    public partial class ReportsPage : Page
    {
        public ReportViewModel VM { get; private set; }
        
        protected string SubLabelsJson { get; set; } = "[]";
        protected string SubDataJson { get; set; } = "[]";
        protected string CatLabelsJson { get; set; } = "[]";
        protected string CatSolvesJson { get; set; } = "[]";
        protected string TeamLabelsJson { get; set; } = "[]";
        protected string TeamScoresJson { get; set; } = "[]";
        protected string UserLabelsJson { get; set; } = "[]";
        protected string UserActiveJson { get; set; } = "[]";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireAdminSession(this.Page);
            
            var master = Master as AdminMaster;
            if (master != null)
            {
                master.PageTitle = "Reports";
            }
            Page.Title = "Reports â€” CyberArena";

            if (!IsPostBack)
            {
                BindPage();
            }
        }

        private void BindPage()
        {
            var allChallenges = AdminMockData.GetAdminChallenges();

            VM = new ReportViewModel
            {
                TotalSubmissions   = 4872,
                CorrectSubmissions = 3109,
                UniqueUsers        = 287,
                AvgSolveTime       = 2.4,
                MostSolved = allChallenges
                    .OrderByDescending(c => c.SolverCount)
                    .Take(8)
                    .Select(c => new ChallengeSolveStatItem
                    {
                        Id=c.Id, Title=c.Title, Category=c.Category,
                        Points=c.Points, SolverCount=c.SolverCount,
                        SolveRate=Math.Round((double)c.SolverCount / 312 * 100, 1),
                        Difficulty=c.Difficulty
                    }).ToList(),
                LeastSolved = allChallenges
                    .Where(c => c.Status == ChallengeStatus.Active)
                    .OrderBy(c => c.SolverCount)
                    .Take(8)
                    .Select(c => new ChallengeSolveStatItem
                    {
                        Id=c.Id, Title=c.Title, Category=c.Category,
                        Points=c.Points, SolverCount=c.SolverCount,
                        SolveRate=Math.Round((double)c.SolverCount / 312 * 100, 1),
                        Difficulty=c.Difficulty
                    }).ToList(),
                TeamPerformance = new List<TeamPerformanceItem>
                {
                    new TeamPerformanceItem { Rank=1, Name="ByteForce",   Score=32400, Solved=58, Members=4 },
                    new TeamPerformanceItem { Rank=2, Name="CodeStrike",  Score=28900, Solved=52, Members=3 },
                    new TeamPerformanceItem { Rank=3, Name="NullByte",    Score=25600, Solved=47, Members=4 },
                    new TeamPerformanceItem { Rank=4, Name="PhantomByte", Score=21300, Solved=41, Members=3 },
                    new TeamPerformanceItem { Rank=5, Name="ShadowStack", Score=17800, Solved=34, Members=2 },
                    new TeamPerformanceItem { Rank=6, Name="ZeroDay",     Score=14200, Solved=28, Members=4 },
                    new TeamPerformanceItem { Rank=7, Name="PwnStars",    Score=11500, Solved=22, Members=2 },
                    new TeamPerformanceItem { Rank=8, Name="CryptoKings", Score=9100,  Solved=17, Members=3 },
                }
            };

            rptMostSolved.DataSource = VM.MostSolved;
            rptMostSolved.DataBind();

            rptLeastSolved.DataSource = VM.LeastSolved;
            rptLeastSolved.DataBind();

            rptTeamPerformance.DataSource = VM.TeamPerformance;
            rptTeamPerformance.DataBind();

            // Chart data
            var js = new JavaScriptSerializer();
            SubLabelsJson  = js.Serialize(new[] { "Sep 12","Sep 13","Sep 14","Sep 15","Sep 16","Sep 17","Sep 18" });
            SubDataJson    = js.Serialize(new[] { 132, 176, 118, 279, 241, 197, 226 });
            CatLabelsJson  = js.Serialize(ChallengeCategories.All);
            CatSolvesJson  = js.Serialize(new[] { 426, 343, 278, 312, 127, 226, 193, 81 });
            TeamLabelsJson = js.Serialize(new[] { "ByteForce","CodeStrike","NullByte","PhantomByte","ShadowStack","ZeroDay" });
            TeamScoresJson = js.Serialize(new[] { 32400, 28900, 25600, 21300, 17800, 14200 });
            UserLabelsJson = js.Serialize(new[] { "Sep 12","Sep 13","Sep 14","Sep 15","Sep 16","Sep 17","Sep 18" });
            UserActiveJson = js.Serialize(new[] { 87, 112, 76, 134, 128, 109, 121 });
        }
    }
}

