using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CyberArenaWebForms.Models
{
    public class AdminRecentSubmission
    {
        public string Username { get; set; } = string.Empty;
        public string ChallengeName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Points { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime SubmittedAt { get; set; }

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - SubmittedAt.ToUniversalTime();
                if (diff.TotalMinutes < 1) return "just now";
                if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalHours < 24) return $"{(int)diff.TotalHours}h ago";
                return $"{(int)diff.TotalDays}d ago";
            }
        }
    }

    public class AdminUserActivityItem
    {
        public string Username { get; set; } = string.Empty;
        public string AvatarInitials { get; set; }
        public string Action { get; set; } = string.Empty;  // "solved", "registered", "joined_team"
        public string Detail { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        public string IconClass
        {
            get
            {
                switch (Action)
                {
                    case "solved": return "fa-flag";
                    case "registered": return "fa-user-plus";
                    case "joined_team": return "fa-users";
                    case "joined_comp": return "fa-bolt";
                    default: return "fa-circle-info";
                }
            }
        }

        public string IconColorClass
        {
            get
            {
                switch (Action)
                {
                    case "solved": return "success";
                    case "registered": return "accent";
                    case "joined_team": return "info";
                    case "joined_comp": return "warning";
                    default: return "accent";
                }
            }
        }

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - Timestamp.ToUniversalTime();
                if (diff.TotalMinutes < 1) return "just now";
                if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalHours < 24) return $"{(int)diff.TotalHours}h ago";
                return $"{(int)diff.TotalDays}d ago";
            }
        }
    }

    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalChallenges { get; set; }
        public int ActiveCompetitions { get; set; }
        public int TotalSubmissions { get; set; }
        public int NewUsersToday { get; set; }
        public int SubmissionsToday { get; set; }
        public List<AdminRecentSubmission> RecentSubmissions { get; set; } = new List<AdminRecentSubmission>();
        public List<AdminUserActivityItem> UserActivity { get; set; } = new List<AdminUserActivityItem>();
    }

    public class ChallengeSolveStatItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Points { get; set; }
        public int SolverCount { get; set; }
        public double SolveRate { get; set; }
        public string Difficulty { get; set; } = "Medium";
        public string DifficultyLower { get { return Difficulty.ToLower(); } }
    }

    public class TeamPerformanceItem
    {
        public int Rank { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public int Solved { get; set; }
        public int Members { get; set; }
        
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

    public class ReportViewModel
    {
        public List<ChallengeSolveStatItem> MostSolved { get; set; } = new List<ChallengeSolveStatItem>();
        public List<ChallengeSolveStatItem> LeastSolved { get; set; } = new List<ChallengeSolveStatItem>();
        public List<TeamPerformanceItem> TeamPerformance { get; set; } = new List<TeamPerformanceItem>();
        public int TotalSubmissions { get; set; }
        public int CorrectSubmissions { get; set; }
        public int UniqueUsers { get; set; }
        public double AvgSolveTime { get; set; }
        public double CorrectRate
        {
            get
            {
                return TotalSubmissions > 0 ? Math.Round((double)CorrectSubmissions / TotalSubmissions * 100, 1) : 0;
            }
        }
    }

    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsPinned { get; set; }
        public DateTime PublishedAt { get; set; }
        public string AuthorName { get; set; } = "Admin";

        public string Excerpt { get { return Body.Length > 160 ? Body.Substring(0, 157) + "…" : Body; } }

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - PublishedAt.ToUniversalTime();
                if (diff.TotalMinutes < 1) return "just now";
                if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalHours < 24) return $"{(int)diff.TotalHours}h ago";
                if (diff.TotalDays < 7) return $"{(int)diff.TotalDays}d ago";
                return PublishedAt.ToString("dd MMM yyyy");
            }
        }
    }

    public class AnnouncementListViewModel
    {
        public List<AnnouncementViewModel> Announcements { get; set; } = new List<AnnouncementViewModel>();
        public List<AnnouncementViewModel> Pinned { get { return Announcements.Where(a => a.IsPinned).ToList(); } }
        public List<AnnouncementViewModel> Regular { get { return Announcements.Where(a => !a.IsPinned).ToList(); } }
    }

    public class AnnouncementFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be 3–200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Body is required.")]
        public string Body { get; set; } = string.Empty;

        public bool IsPinned { get; set; }

        public bool IsEdit { get { return Id > 0; } }
        public string PageTitle { get { return IsEdit ? "Edit Announcement" : "Create Announcement"; } }
        public string SubmitLabel { get { return IsEdit ? "Save Changes" : "Publish"; } }
    }

    public class AdminChallengeListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = ChallengeDifficulty.Medium;
        public int Points { get; set; }
        public string Status { get; set; } = ChallengeStatus.Active;
        public int SolverCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool HasAttachment { get; set; }

        public string DifficultyLower { get { return Difficulty.ToLower(); } }
        public string StatusLower { get { return Status.ToLower(); } }
    }

    public class AdminChallengeListViewModel
    {
        public List<AdminChallengeListItemViewModel> Challenges { get; set; } = new List<AdminChallengeListItemViewModel>();
        public string SearchQuery { get; set; }
        public string FilterCategory { get; set; }
        public string FilterStatus { get; set; }

        public int TotalCount { get { return Challenges.Count; } }
        public int ActiveCount { get { return Challenges.Count(c => c.Status == ChallengeStatus.Active); } }
        public int DraftCount { get { return Challenges.Count(c => c.Status == ChallengeStatus.Draft); } }
        public int ArchivedCount { get { return Challenges.Count(c => c.Status == ChallengeStatus.Archived); } }
    }

    public class ChallengeFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be 3–200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = ChallengeCategories.Web;

        [Required(ErrorMessage = "Difficulty is required.")]
        public string Difficulty { get; set; } = ChallengeDifficulty.Medium;

        [Required(ErrorMessage = "Points value is required.")]
        [Range(1, 10000, ErrorMessage = "Points must be between 1 and 10,000.")]
        public int Points { get; set; } = 100;

        [Required(ErrorMessage = "Flag is required.")]
        [StringLength(500, ErrorMessage = "Flag cannot exceed 500 characters.")]
        public string Flag { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Hint cannot exceed 1000 characters.")]
        public string Hint { get; set; }

        [Range(0, 10000)]
        public int HintPenalty { get; set; } = 0;

        public string Status { get; set; } = ChallengeStatus.Draft;
        public string ExistingAttachmentName { get; set; }
    }

    public static class AdminMockData
    {
        public static List<AdminChallengeListItemViewModel> GetAdminChallenges()
        {
            return new List<AdminChallengeListItemViewModel>
            {
                new AdminChallengeListItemViewModel { Id=1,  Title="SQL Injection Basics",        Category=ChallengeCategories.Web,        Difficulty="Easy",   Points=100,  Status="Active",   SolverCount=184, CreatedAt=DateTime.UtcNow.AddDays(-30), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=2,  Title="XSS Reflected Attack",        Category=ChallengeCategories.Web,        Difficulty="Easy",   Points=150,  Status="Active",   SolverCount=142, CreatedAt=DateTime.UtcNow.AddDays(-29), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=3,  Title="CSRF Token Bypass",           Category=ChallengeCategories.Web,        Difficulty="Medium", Points=250,  Status="Active",   SolverCount=67,  CreatedAt=DateTime.UtcNow.AddDays(-28), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=4,  Title="JWT Secret Cracking",         Category=ChallengeCategories.Web,        Difficulty="Medium", Points=300,  Status="Active",   SolverCount=53,  CreatedAt=DateTime.UtcNow.AddDays(-27), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=5,  Title="GraphQL Introspection Leak",  Category=ChallengeCategories.Web,        Difficulty="Hard",   Points=500,  Status="Active",   SolverCount=21,  CreatedAt=DateTime.UtcNow.AddDays(-26), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=6,  Title="Caesar's Secret",             Category=ChallengeCategories.Crypto,     Difficulty="Easy",   Points=75,   Status="Active",   SolverCount=213, CreatedAt=DateTime.UtcNow.AddDays(-25), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=7,  Title="RSA Weak Key",                Category=ChallengeCategories.Crypto,     Difficulty="Medium", Points=300,  Status="Active",   SolverCount=58,  CreatedAt=DateTime.UtcNow.AddDays(-24), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=8,  Title="AES ECB Penguin",             Category=ChallengeCategories.Crypto,     Difficulty="Medium", Points=250,  Status="Active",   SolverCount=72,  CreatedAt=DateTime.UtcNow.AddDays(-23), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=9,  Title="Elliptic Curve Discrete Log", Category=ChallengeCategories.Crypto,     Difficulty="Hard",   Points=600,  Status="Draft",    SolverCount=0,   CreatedAt=DateTime.UtcNow.AddDays(-5),  HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=10, Title="Crackme Level 1",             Category=ChallengeCategories.Reversing,  Difficulty="Easy",   Points=100,  Status="Active",   SolverCount=134, CreatedAt=DateTime.UtcNow.AddDays(-22), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=11, Title="Anti-Debug Bypass",           Category=ChallengeCategories.Reversing,  Difficulty="Medium", Points=350,  Status="Active",   SolverCount=41,  CreatedAt=DateTime.UtcNow.AddDays(-21), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=12, Title="Obfuscated Python",           Category=ChallengeCategories.Reversing,  Difficulty="Medium", Points=275,  Status="Active",   SolverCount=63,  CreatedAt=DateTime.UtcNow.AddDays(-20), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=13, Title="LLVM Bitcode Maze",           Category=ChallengeCategories.Reversing,  Difficulty="Hard",   Points=550,  Status="Draft",    SolverCount=0,   CreatedAt=DateTime.UtcNow.AddDays(-3),  HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=14, Title="Wireshark Hunt",              Category=ChallengeCategories.Forensics,  Difficulty="Easy",   Points=150,  Status="Active",   SolverCount=165, CreatedAt=DateTime.UtcNow.AddDays(-19), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=15, Title="Memory Dump Analysis",        Category=ChallengeCategories.Forensics,  Difficulty="Medium", Points=325,  Status="Active",   SolverCount=47,  CreatedAt=DateTime.UtcNow.AddDays(-18), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=16, Title="Deleted File Recovery",       Category=ChallengeCategories.Forensics,  Difficulty="Medium", Points=200,  Status="Active",   SolverCount=81,  CreatedAt=DateTime.UtcNow.AddDays(-17), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=17, Title="TCP Handshake Hijack",        Category=ChallengeCategories.Networking, Difficulty="Easy",   Points=125,  Status="Active",   SolverCount=109, CreatedAt=DateTime.UtcNow.AddDays(-16), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=18, Title="BGP Route Poisoning",         Category=ChallengeCategories.Networking, Difficulty="Hard",   Points=500,  Status="Active",   SolverCount=18,  CreatedAt=DateTime.UtcNow.AddDays(-15), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=19, Title="Hidden in Plain Sight",       Category=ChallengeCategories.Stego,      Difficulty="Easy",   Points=100,  Status="Active",   SolverCount=152, CreatedAt=DateTime.UtcNow.AddDays(-14), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=20, Title="Audio Spectrogram Secret",    Category=ChallengeCategories.Stego,      Difficulty="Medium", Points=225,  Status="Active",   SolverCount=74,  CreatedAt=DateTime.UtcNow.AddDays(-13), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=21, Title="The Mysterious Developer",    Category=ChallengeCategories.Osint,      Difficulty="Easy",   Points=100,  Status="Active",   SolverCount=193, CreatedAt=DateTime.UtcNow.AddDays(-12), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=22, Title="GeoGuessr Intelligence",      Category=ChallengeCategories.Osint,      Difficulty="Medium", Points=200,  Status="Archived", SolverCount=88,  CreatedAt=DateTime.UtcNow.AddDays(-60), UpdatedAt=DateTime.UtcNow.AddDays(-7), HasAttachment=false },
                new AdminChallengeListItemViewModel { Id=23, Title="Buffer Overflow 101",         Category=ChallengeCategories.Pwn,        Difficulty="Medium", Points=250,  Status="Active",   SolverCount=76,  CreatedAt=DateTime.UtcNow.AddDays(-11), HasAttachment=true  },
                new AdminChallengeListItemViewModel { Id=24, Title="Kernel Exploit 0-day",        Category=ChallengeCategories.Pwn,        Difficulty="Hard",   Points=1000, Status="Active",   SolverCount=5,   CreatedAt=DateTime.UtcNow.AddDays(-10), HasAttachment=true  }
            };
        }

        public static AdminChallengeListItemViewModel GetAdminById(int id)
        {
            return GetAdminChallenges().FirstOrDefault(c => c.Id == id);
        }

        public static List<AnnouncementViewModel> GetAnnouncements()
        {
            return new List<AnnouncementViewModel>
            {
                new AnnouncementViewModel { Id=1, Title="Welcome to CyberArena 2026", Body="We are thrilled to launch the new platform.", IsPinned=true, PublishedAt=DateTime.UtcNow.AddDays(-10) },
                new AnnouncementViewModel { Id=2, Title="Maintenance window scheduled", Body="The platform will be down for 2 hours this Sunday.", IsPinned=false, PublishedAt=DateTime.UtcNow.AddDays(-2) }
            };
        }
    }
}
