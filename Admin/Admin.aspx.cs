using System;
using System.Web.UI;
using System.Collections.Generic;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;
using System.Web.Script.Serialization;

namespace CyberArenaWebForms.Admin
{
    public partial class AdminPage : Page
    {
        public AdminDashboardViewModel VM { get; private set; }
        
        protected string SubLabelsJson { get; set; } = "[]";
        protected string SubCorrectJson { get; set; } = "[]";
        protected string SubWrongJson { get; set; } = "[]";
        protected string CatLabelsJson { get; set; } = "[]";
        protected string CatCountsJson { get; set; } = "[]";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireAdminSession(this.Page);
            
            var master = Master as AdminMaster;
            if (master != null)
            {
                master.PageTitle = "Admin Dashboard";
            }
            Page.Title = "Admin Dashboard â€” CyberArena";

            if (!IsPostBack)
            {
                BindPage();
            }
        }

        private void BindPage()
        {
            // TODO: Replace with EF Core aggregates
            VM = new AdminDashboardViewModel
            {
                TotalUsers         = 312,
                TotalChallenges    = 24,
                ActiveCompetitions = 1,
                TotalSubmissions   = 4872,
                NewUsersToday      = 18,
                SubmissionsToday   = 143,
                RecentSubmissions  = new List<AdminRecentSubmission>
                {
                    new AdminRecentSubmission { Username="CipherMaster", ChallengeName="SQL Injection Basics",       Category="Web Security",       Points=100, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-3)  },
                    new AdminRecentSubmission { Username="n3tR4nger",    ChallengeName="RSA Weak Key",               Category="Cryptography",       Points=300, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-7)  },
                    new AdminRecentSubmission { Username="0x_exploit",   ChallengeName="CSRF Token Bypass",          Category="Web Security",       Points=250, IsCorrect=false, SubmittedAt=DateTime.UtcNow.AddMinutes(-12) },
                    new AdminRecentSubmission { Username="h4x0r_pro",    ChallengeName="Wireshark Hunt",             Category="Digital Forensics",  Points=150, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-18) },
                    new AdminRecentSubmission { Username="shell_ghost",  ChallengeName="Anti-Debug Bypass",          Category="Reverse Engineering", Points=350, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-25) },
                    new AdminRecentSubmission { Username="xor_queen",    ChallengeName="Buffer Overflow 101",        Category="Binary Exploitation", Points=250, IsCorrect=false, SubmittedAt=DateTime.UtcNow.AddMinutes(-31) },
                    new AdminRecentSubmission { Username="pwn_wizard",   ChallengeName="The Mysterious Developer",   Category="OSINT",              Points=100, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-40) },
                    new AdminRecentSubmission { Username="hex_ninja",    ChallengeName="Hidden in Plain Sight",      Category="Steganography",      Points=100, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-52) },
                },
                UserActivity = new List<AdminUserActivityItem>
                {
                    new AdminUserActivityItem { Username="h4x0r_pro",    AvatarInitials="AC", Action="solved",      Detail="SQL Injection Basics",  Timestamp=DateTime.UtcNow.AddMinutes(-18) },
                    new AdminUserActivityItem { Username="vuln_hunter",  AvatarInitials="VH", Action="joined_comp", Detail="CyberArena Open 2026",  Timestamp=DateTime.UtcNow.AddMinutes(-35) },
                    new AdminUserActivityItem { Username="sqli_master",  AvatarInitials="SM", Action="registered",  Detail="New account created",   Timestamp=DateTime.UtcNow.AddHours(-1)   },
                    new AdminUserActivityItem { Username="xss_panda",    AvatarInitials="XP", Action="joined_team", Detail="NullByte",              Timestamp=DateTime.UtcNow.AddHours(-2)   },
                    new AdminUserActivityItem { Username="fuzzy_logic",  AvatarInitials="FL", Action="solved",      Detail="Caesar's Secret",       Timestamp=DateTime.UtcNow.AddHours(-3)   },
                    new AdminUserActivityItem { Username="mem_leak",     AvatarInitials="ML", Action="registered",  Detail="New account created",   Timestamp=DateTime.UtcNow.AddHours(-5)   },
                },
            };

            rptRecentSubmissions.DataSource = VM.RecentSubmissions;
            rptRecentSubmissions.DataBind();

            rptUserActivity.DataSource = VM.UserActivity;
            rptUserActivity.DataBind();

            // Chart data
            var js = new JavaScriptSerializer();
            SubLabelsJson  = js.Serialize(new[] { "Sep 12","Sep 13","Sep 14","Sep 15","Sep 16","Sep 17","Sep 18" });
            SubCorrectJson = js.Serialize(new[] { 98, 124, 87, 201, 176, 143, 165 });
            SubWrongJson   = js.Serialize(new[] { 34, 52, 31, 78, 65, 54, 61 });
            CatLabelsJson  = js.Serialize(ChallengeCategories.All);
            CatCountsJson  = js.Serialize(new[] { 5, 4, 4, 3, 2, 2, 2, 2 });
        }
    }
}

