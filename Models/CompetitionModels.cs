using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberArenaWebForms.Models
{
    public class CompetitionListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "upcoming"; // active | upcoming | ended
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int ChallengeCount { get; set; }
        public int ParticipantCount { get; set; }
        public int MaxParticipants { get; set; }
        public bool IsJoined { get; set; }
        public string Format { get; set; } = "Individual"; // Individual | Team

        public string StatusClass
        {
            get
            {
                switch (Status.ToLower())
                {
                    case "active": return "active";
                    case "upcoming": return "upcoming";
                    case "ended": return "ended";
                    default: return "ended";
                }
            }
        }

        public string TimeDisplay
        {
            get
            {
                switch (Status.ToLower())
                {
                    case "active": return $"Ends {TimeRemaining}";
                    case "upcoming": return $"Starts {StartAt:dd MMM yyyy HH:mm} UTC";
                    case "ended": return $"Ended {EndAt:dd MMM yyyy}";
                    default: return string.Empty;
                }
            }
        }

        private string TimeRemaining
        {
            get
            {
                var diff = EndAt - DateTime.UtcNow;
                if (diff.TotalSeconds <= 0) return "now";
                if (diff.TotalDays >= 1) return $"in {(int)diff.TotalDays}d {diff.Hours}h";
                if (diff.TotalHours >= 1) return $"in {(int)diff.TotalHours}h {diff.Minutes}m";
                return $"in {diff.Minutes}m";
            }
        }
    }

    public class CompetitionListViewModel
    {
        public List<CompetitionListItemViewModel> Competitions { get; set; } = new List<CompetitionListItemViewModel>();
        public string FilterStatus { get; set; } = "all";  // all | active | upcoming | ended

        public List<CompetitionListItemViewModel> Active
        {
            get { return Competitions.Where(c => c.Status == "active").ToList(); }
        }

        public List<CompetitionListItemViewModel> Upcoming
        {
            get { return Competitions.Where(c => c.Status == "upcoming").ToList(); }
        }

        public List<CompetitionListItemViewModel> Ended
        {
            get { return Competitions.Where(c => c.Status == "ended").ToList(); }
        }
    }

    public class CompetitionChallengeItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = "Medium";
        public int Points { get; set; }
        public int SolverCount { get; set; }
        public bool IsSolved { get; set; }

        public string DifficultyLower { get { return Difficulty.ToLower(); } }
        public string CategoryIcon { get { return ChallengeCategories.Icon(Category); } }
        public string CategoryCssClass { get { return ChallengeCategories.CssClass(Category); } }
    }

    public class CompetitionLeaderboardEntry
    {
        public int Rank { get; set; }
        public string Username { get; set; } = string.Empty;
        public string TeamName { get; set; }
        public int Score { get; set; }
        public int Solved { get; set; }
        public DateTime LastSolve { get; set; }
        public bool IsCurrentUser { get; set; }

        public string RankBadgeClass
        {
            get
            {
                switch (Rank)
                {
                    case 1: return "gold";
                    case 2: return "silver";
                    case 3: return "bronze";
                    default: return "other";
                }
            }
        }
    }

    public class CompetitionDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Rules { get; set; } = string.Empty;
        public string Status { get; set; } = "upcoming";
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int ParticipantCount { get; set; }
        public int MaxParticipants { get; set; }
        public string Format { get; set; } = "Individual";
        public bool IsJoined { get; set; }

        public List<CompetitionChallengeItem> Challenges { get; set; } = new List<CompetitionChallengeItem>();
        public List<CompetitionLeaderboardEntry> Leaderboard { get; set; } = new List<CompetitionLeaderboardEntry>();

        public string StatusClass
        {
            get
            {
                switch (Status.ToLower())
                {
                    case "active": return "active";
                    case "upcoming": return "upcoming";
                    default: return "ended";
                }
            }
        }

        public string TimeRemaining
        {
            get
            {
                var diff = EndAt - DateTime.UtcNow;
                if (diff.TotalSeconds <= 0) return "Ended";
                if (diff.TotalDays >= 1) return $"{(int)diff.TotalDays}d {diff.Hours}h remaining";
                if (diff.TotalHours >= 1) return $"{(int)diff.TotalHours}h {diff.Minutes}m remaining";
                return $"{diff.Minutes}m remaining";
            }
        }
    }

    public class CompetitionMockData
    {
        public static List<CompetitionListItemViewModel> GetAll()
        {
            return new List<CompetitionListItemViewModel>
            {
                new CompetitionListItemViewModel
                {
                    Id=1, Name="CyberArena Open 2026",
                    Description="Annual flagship CTF open to all skill levels. Compete across 8 categories.",
                    Status="active", StartAt=DateTime.UtcNow.AddDays(-2), EndAt=DateTime.UtcNow.AddDays(3),
                    ChallengeCount=24, ParticipantCount=312, MaxParticipants=500, IsJoined=true, Format="Individual"
                },
                new CompetitionListItemViewModel
                {
                    Id=2, Name="Red Team Rumble",
                    Description="Advanced offensive security competition — pwn, rev, and crypto only.",
                    Status="upcoming", StartAt=DateTime.UtcNow.AddDays(5), EndAt=DateTime.UtcNow.AddDays(7),
                    ChallengeCount=12, ParticipantCount=87, MaxParticipants=200, IsJoined=false, Format="Team"
                },
                new CompetitionListItemViewModel
                {
                    Id=3, Name="Forensics & OSINT Sprint",
                    Description="48-hour speed competition focused on digital forensics and OSINT challenges.",
                    Status="upcoming", StartAt=DateTime.UtcNow.AddDays(14), EndAt=DateTime.UtcNow.AddDays(16),
                    ChallengeCount=16, ParticipantCount=45, MaxParticipants=150, IsJoined=false, Format="Individual"
                },
                new CompetitionListItemViewModel
                {
                    Id=4, Name="Beginner Bootcamp",
                    Description="Introductory CTF for newcomers to the security field. Learn while you hack.",
                    Status="ended", StartAt=DateTime.UtcNow.AddDays(-30), EndAt=DateTime.UtcNow.AddDays(-28),
                    ChallengeCount=10, ParticipantCount=198, MaxParticipants=300, IsJoined=true, Format="Individual"
                },
                new CompetitionListItemViewModel
                {
                    Id=5, Name="Crypto Clash",
                    Description="Pure cryptography competition. Break the ciphers and earn the flags.",
                    Status="ended", StartAt=DateTime.UtcNow.AddDays(-60), EndAt=DateTime.UtcNow.AddDays(-58),
                    ChallengeCount=14, ParticipantCount=276, MaxParticipants=400, IsJoined=false, Format="Individual"
                }
            };
        }

        public static CompetitionDetailsViewModel GetDetail(int id)
        {
            var list = GetAll();
            var item = list.FirstOrDefault(c => c.Id == id);
            if (item == null) return null;

            return new CompetitionDetailsViewModel
            {
                Id               = item.Id,
                Name             = item.Name,
                Description      = item.Description,
                Rules            = "1. No flag sharing or collaboration outside your registered team.\n2. Automated brute-force attacks against the infrastructure are prohibited.\n3. Each participant/team may only submit flags under one account.\n4. Any form of cheating, plagiarism, or abuse will result in disqualification.\n5. Organiser decisions regarding scoring are final.\n6. Writeups may only be published 24 hours after the competition ends.",
                Status           = item.Status,
                StartAt          = item.StartAt,
                EndAt            = item.EndAt,
                ParticipantCount = item.ParticipantCount,
                MaxParticipants  = item.MaxParticipants,
                Format           = item.Format,
                IsJoined         = item.IsJoined,
                Challenges = new List<CompetitionChallengeItem>
                {
                    new CompetitionChallengeItem { Id=1,  Title="SQL Injection Basics",       Category=ChallengeCategories.Web,       Difficulty="Easy",   Points=100, SolverCount=184, IsSolved=true  },
                    new CompetitionChallengeItem { Id=2,  Title="XSS Reflected Attack",       Category=ChallengeCategories.Web,       Difficulty="Easy",   Points=150, SolverCount=142, IsSolved=true  },
                    new CompetitionChallengeItem { Id=3,  Title="CSRF Token Bypass",          Category=ChallengeCategories.Web,       Difficulty="Medium", Points=250, SolverCount=67,  IsSolved=false },
                    new CompetitionChallengeItem { Id=4,  Title="Caesar's Secret",            Category=ChallengeCategories.Crypto,    Difficulty="Easy",   Points=75,  SolverCount=213, IsSolved=true  },
                    new CompetitionChallengeItem { Id=5,  Title="RSA Weak Key",               Category=ChallengeCategories.Crypto,    Difficulty="Medium", Points=300, SolverCount=58,  IsSolved=false },
                    new CompetitionChallengeItem { Id=6,  Title="Crackme Level 1",            Category=ChallengeCategories.Reversing, Difficulty="Easy",   Points=100, SolverCount=134, IsSolved=true  },
                    new CompetitionChallengeItem { Id=7,  Title="Anti-Debug Bypass",          Category=ChallengeCategories.Reversing, Difficulty="Medium", Points=350, SolverCount=41,  IsSolved=false },
                    new CompetitionChallengeItem { Id=8,  Title="Wireshark Hunt",             Category=ChallengeCategories.Forensics, Difficulty="Easy",   Points=150, SolverCount=165, IsSolved=false },
                    new CompetitionChallengeItem { Id=9,  Title="Hidden in Plain Sight",      Category=ChallengeCategories.Stego,     Difficulty="Easy",   Points=100, SolverCount=152, IsSolved=false },
                    new CompetitionChallengeItem { Id=10, Title="The Mysterious Developer",   Category=ChallengeCategories.Osint,     Difficulty="Easy",   Points=100, SolverCount=193, IsSolved=false },
                    new CompetitionChallengeItem { Id=11, Title="Buffer Overflow 101",        Category=ChallengeCategories.Pwn,       Difficulty="Medium", Points=250, SolverCount=76,  IsSolved=false },
                    new CompetitionChallengeItem { Id=12, Title="BGP Route Poisoning",        Category=ChallengeCategories.Networking,Difficulty="Hard",   Points=500, SolverCount=18,  IsSolved=false },
                },
                Leaderboard = new List<CompetitionLeaderboardEntry>
                {
                    new CompetitionLeaderboardEntry { Rank=1, Username="CipherMaster",  TeamName="ByteForce",   Score=975, Solved=9, LastSolve=DateTime.UtcNow.AddMinutes(-45),  IsCurrentUser=false },
                    new CompetitionLeaderboardEntry { Rank=2, Username="n3tR4nger",     TeamName="CodeStrike",  Score=925, Solved=8, LastSolve=DateTime.UtcNow.AddMinutes(-120), IsCurrentUser=false },
                    new CompetitionLeaderboardEntry { Rank=3, Username="0x_exploit",    TeamName="NullByte",    Score=870, Solved=8, LastSolve=DateTime.UtcNow.AddMinutes(-200), IsCurrentUser=false },
                    new CompetitionLeaderboardEntry { Rank=4, Username="shell_ghost",   TeamName="ByteForce",   Score=798, Solved=7, LastSolve=DateTime.UtcNow.AddHours(-3),    IsCurrentUser=false },
                    new CompetitionLeaderboardEntry { Rank=5, Username="h4x0r_pro",     TeamName="NullByte",    Score=725, Solved=6, LastSolve=DateTime.UtcNow.AddHours(-4),    IsCurrentUser=true  },
                    new CompetitionLeaderboardEntry { Rank=6, Username="pwn_wizard",    TeamName="CodeStrike",  Score=600, Solved=5, LastSolve=DateTime.UtcNow.AddHours(-5),    IsCurrentUser=false },
                    new CompetitionLeaderboardEntry { Rank=7, Username="xor_queen",     TeamName="ByteForce",   Score=525, Solved=4, LastSolve=DateTime.UtcNow.AddHours(-7),    IsCurrentUser=false },
                    new CompetitionLeaderboardEntry { Rank=8, Username="packet_ghost",  TeamName="PhantomByte", Score=450, Solved=4, LastSolve=DateTime.UtcNow.AddHours(-9),    IsCurrentUser=false },
                }
            };
        }
    }
}
