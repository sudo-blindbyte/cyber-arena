using Microsoft.AspNetCore.Mvc;
using cyber_arena.ViewModels;

namespace cyber_arena.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";

            // TODO: Replace with real data from EF Core / Identity
            // var userId = _userManager.GetUserId(User);
            // var stats = await _dashboardService.GetParticipantStats(userId);

            var vm = new DashboardViewModel
            {
                Username          = "h4x0r_pro",
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
                    new() { ChallengeName = "SQL Injection Basics",   Category = "Web",     Points = 100, Difficulty = "easy",   SolvedAt = DateTime.UtcNow.AddMinutes(-25) },
                    new() { ChallengeName = "Buffer Overflow 101",    Category = "Pwn",     Points = 250, Difficulty = "medium", SolvedAt = DateTime.UtcNow.AddHours(-2) },
                    new() { ChallengeName = "RSA Weak Key",           Category = "Crypto",  Points = 300, Difficulty = "medium", SolvedAt = DateTime.UtcNow.AddHours(-5) },
                    new() { ChallengeName = "Wireshark Hunt",         Category = "Forensics", Points = 150, Difficulty = "easy", SolvedAt = DateTime.UtcNow.AddHours(-8) },
                    new() { ChallengeName = "Kernel Exploit 0-day",   Category = "Pwn",     Points = 500, Difficulty = "expert", SolvedAt = DateTime.UtcNow.AddDays(-1) },
                },

                RecentActivities = new List<ActivityItem>
                {
                    new() { Icon = "fa-trophy",      IconColorClass = "warning", Text = "You climbed to <strong>Rank #7</strong> on the global leaderboard.",   Timestamp = DateTime.UtcNow.AddMinutes(-10) },
                    new() { Icon = "fa-flag-checkered", IconColorClass = "success", Text = "Solved <strong>SQL Injection Basics</strong> (+100 pts)",            Timestamp = DateTime.UtcNow.AddMinutes(-25) },
                    new() { Icon = "fa-users",        IconColorClass = "accent",  Text = "Joined team <strong>ByteBreakers</strong>",                            Timestamp = DateTime.UtcNow.AddHours(-3) },
                    new() { Icon = "fa-shield-halved", IconColorClass = "info",   Text = "Registered for <strong>NIT CTF 2026</strong> competition",              Timestamp = DateTime.UtcNow.AddHours(-6) },
                    new() { Icon = "fa-circle-check", IconColorClass = "success", Text = "Solved <strong>Buffer Overflow 101</strong> (+250 pts)",                Timestamp = DateTime.UtcNow.AddHours(-2) },
                },

                ActiveCompetitions = new List<ActiveCompetition>
                {
                    new()
                    {
                        Name = "NIT CTF 2026",
                        Status = "active",
                        EndsAt = DateTime.UtcNow.AddDays(2).AddHours(6),
                        TeamRank = 3,
                        TotalTeams = 48,
                        ChallengesSolved = 8,
                        TotalChallenges = 20,
                        Score = 1800
                    },
                    new()
                    {
                        Name = "CyberArena Weekly #12",
                        Status = "active",
                        EndsAt = DateTime.UtcNow.AddHours(18),
                        TeamRank = 5,
                        TotalTeams = 92,
                        ChallengesSolved = 4,
                        TotalChallenges = 10,
                        Score = 650
                    }
                },

                ScoreHistory = new List<ScoreHistoryPoint>
                {
                    new() { Label = "7d", Score = 1200, HeightPercent = 28 },
                    new() { Label = "6d", Score = 1500, HeightPercent = 35 },
                    new() { Label = "5d", Score = 1500, HeightPercent = 35 },
                    new() { Label = "4d", Score = 2100, HeightPercent = 49 },
                    new() { Label = "3d", Score = 2800, HeightPercent = 65 },
                    new() { Label = "2d", Score = 3600, HeightPercent = 84 },
                    new() { Label = "1d", Score = 3900, HeightPercent = 91 },
                    new() { Label = "Now", Score = 4250, HeightPercent = 100, IsHighest = true },
                },

                CategoryProgress = new List<CategoryProgress>
                {
                    new() { Name = "Web",       Icon = "fa-globe",         Solved = 10, Total = 25, ColorClass = "accent" },
                    new() { Name = "Pwn",        Icon = "fa-terminal",      Solved = 8,  Total = 20, ColorClass = "danger" },
                    new() { Name = "Crypto",     Icon = "fa-key",           Solved = 7,  Total = 18, ColorClass = "warning" },
                    new() { Name = "Forensics",  Icon = "fa-magnifying-glass", Solved = 5, Total = 22, ColorClass = "info" },
                    new() { Name = "Reversing",  Icon = "fa-code",          Solved = 3,  Total = 15, ColorClass = "success" },
                    new() { Name = "OSINT",      Icon = "fa-satellite-dish", Solved = 1,  Total = 10, ColorClass = "accent" },
                },
            };

            return View(vm);
        }
    }
}
