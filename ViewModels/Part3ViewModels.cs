using System.ComponentModel.DataAnnotations;

namespace cyber_arena.ViewModels
{
    // ══════════════════════════════════════════════════════════
    //  LEADERBOARD
    // ══════════════════════════════════════════════════════════

    public class LeaderboardEntryViewModel
    {
        public int    Rank           { get; set; }
        public string Username       { get; set; } = string.Empty;
        public string? AvatarInitials { get; set; }
        public string? AvatarGradient { get; set; }   // CSS gradient class
        public string  TeamName      { get; set; } = string.Empty;
        public int    TotalScore     { get; set; }
        public int    ChallengesSolved { get; set; }
        public bool   IsCurrentUser  { get; set; }
        public string Country        { get; set; } = string.Empty;

        // Derived
        public string RankBadgeClass => Rank switch
        {
            1 => "gold",
            2 => "silver",
            3 => "bronze",
            _ => "other"
        };
    }

    public class LeaderboardViewModel
    {
        public List<LeaderboardEntryViewModel> Entries { get; set; } = new();
        public string? SearchQuery    { get; set; }
        public string  FilterBy       { get; set; } = "All";    // All | MyTeam
        public int     CurrentUserRank { get; set; }
        public string  CurrentUsername { get; set; } = "h4x0r_pro";

        // Top 3 for podium
        public LeaderboardEntryViewModel? Gold   => Entries.FirstOrDefault(e => e.Rank == 1);
        public LeaderboardEntryViewModel? Silver => Entries.FirstOrDefault(e => e.Rank == 2);
        public LeaderboardEntryViewModel? Bronze => Entries.FirstOrDefault(e => e.Rank == 3);
    }

    // ══════════════════════════════════════════════════════════
    //  TEAMS
    // ══════════════════════════════════════════════════════════

    public class TeamMemberViewModel
    {
        public int    Id             { get; set; }
        public string Username       { get; set; } = string.Empty;
        public string? AvatarInitials { get; set; }
        public string  AvatarClass   { get; set; } = string.Empty;   // alt-1 … alt-4
        public bool   IsCaptain      { get; set; }
        public int    Score          { get; set; }
        public int    ChallengesSolved { get; set; }
        public DateTime JoinedAt     { get; set; }
    }

    public class TeamListItemViewModel
    {
        public int    Id              { get; set; }
        public string Name            { get; set; } = string.Empty;
        public string CaptainUsername { get; set; } = string.Empty;
        public int    Rank            { get; set; }
        public int    Score           { get; set; }
        public int    MemberCount     { get; set; }
        public int    ChallengesSolved { get; set; }
        public string Country         { get; set; } = string.Empty;
        public bool   IsMyTeam        { get; set; }

        public string RankBadgeClass => Rank switch { 1 => "gold", 2 => "silver", 3 => "bronze", _ => "other" };
    }

    public class TeamListViewModel
    {
        public List<TeamListItemViewModel> Teams { get; set; } = new();
        public string? SearchQuery { get; set; }
        public bool    UserHasTeam { get; set; }
        public int?    MyTeamId    { get; set; }
    }

    public class TeamDetailsViewModel
    {
        public int    Id               { get; set; }
        public string Name             { get; set; } = string.Empty;
        public string? Description     { get; set; }
        public string CaptainUsername  { get; set; } = string.Empty;
        public int    Rank             { get; set; }
        public int    Score            { get; set; }
        public int    ChallengesSolved { get; set; }
        public int    TotalChallenges  { get; set; }
        public string Country          { get; set; } = string.Empty;
        public DateTime CreatedAt      { get; set; }
        public bool   IsMyTeam         { get; set; }
        public bool   IsCaptain        { get; set; }

        public List<TeamMemberViewModel> Members { get; set; } = new();

        // Score history for mini chart
        public List<int> ScoreHistory { get; set; } = new();
        public List<string> ScoreHistoryLabels { get; set; } = new();

        public int SolvedPercent => TotalChallenges > 0
            ? (int)((double)ChallengesSolved / TotalChallenges * 100) : 0;
        public string RankBadgeClass => Rank switch { 1 => "gold", 2 => "silver", 3 => "bronze", _ => "other" };
    }

    public class TeamCreateViewModel
    {
        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Team name must be 2–50 characters.")]
        [Display(Name = "Team Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        [Display(Name = "Country / Region")]
        public string? Country { get; set; }
    }

    // ══════════════════════════════════════════════════════════
    //  COMPETITIONS
    // ══════════════════════════════════════════════════════════

    public class CompetitionListItemViewModel
    {
        public int      Id               { get; set; }
        public string   Name             { get; set; } = string.Empty;
        public string   Description      { get; set; } = string.Empty;
        public string   Status           { get; set; } = "upcoming"; // active | upcoming | ended
        public DateTime StartAt          { get; set; }
        public DateTime EndAt            { get; set; }
        public int      ChallengeCount   { get; set; }
        public int      ParticipantCount { get; set; }
        public int      MaxParticipants  { get; set; }
        public bool     IsJoined         { get; set; }
        public string   Format           { get; set; } = "Individual"; // Individual | Team

        public string StatusClass => Status.ToLower() switch
        {
            "active"   => "active",
            "upcoming" => "upcoming",
            "ended"    => "ended",
            _          => "ended"
        };

        public string TimeDisplay => Status.ToLower() switch
        {
            "active"   => $"Ends {TimeRemaining}",
            "upcoming" => $"Starts {StartAt:dd MMM yyyy HH:mm} UTC",
            "ended"    => $"Ended {EndAt:dd MMM yyyy}",
            _          => string.Empty
        };

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
        public List<CompetitionListItemViewModel> Competitions { get; set; } = new();
        public string FilterStatus { get; set; } = "all";  // all | active | upcoming | ended

        public List<CompetitionListItemViewModel> Active   => Competitions.Where(c => c.Status == "active").ToList();
        public List<CompetitionListItemViewModel> Upcoming => Competitions.Where(c => c.Status == "upcoming").ToList();
        public List<CompetitionListItemViewModel> Ended    => Competitions.Where(c => c.Status == "ended").ToList();
    }

    public class CompetitionChallengeItem
    {
        public int    Id         { get; set; }
        public string Title      { get; set; } = string.Empty;
        public string Category   { get; set; } = string.Empty;
        public string Difficulty { get; set; } = "Medium";
        public int    Points     { get; set; }
        public int    SolverCount { get; set; }
        public bool   IsSolved   { get; set; }

        public string DifficultyLower => Difficulty.ToLower();
        public string CategoryIcon    => ChallengeCategories.Icon(Category);
        public string CategoryCssClass => ChallengeCategories.CssClass(Category);
    }

    public class CompetitionLeaderboardEntry
    {
        public int    Rank     { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? TeamName { get; set; }
        public int    Score    { get; set; }
        public int    Solved   { get; set; }
        public DateTime LastSolve { get; set; }
        public bool   IsCurrentUser { get; set; }
        public string RankBadgeClass => Rank switch { 1 => "gold", 2 => "silver", 3 => "bronze", _ => "other" };
    }

    public class CompetitionDetailsViewModel
    {
        public int      Id               { get; set; }
        public string   Name             { get; set; } = string.Empty;
        public string   Description      { get; set; } = string.Empty;
        public string   Rules            { get; set; } = string.Empty;
        public string   Status           { get; set; } = "upcoming";
        public DateTime StartAt          { get; set; }
        public DateTime EndAt            { get; set; }
        public int      ParticipantCount { get; set; }
        public int      MaxParticipants  { get; set; }
        public string   Format           { get; set; } = "Individual";
        public bool     IsJoined         { get; set; }

        public List<CompetitionChallengeItem>   Challenges   { get; set; } = new();
        public List<CompetitionLeaderboardEntry> Leaderboard  { get; set; } = new();

        public string StatusClass => Status.ToLower() switch { "active" => "active", "upcoming" => "upcoming", _ => "ended" };

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

    // ══════════════════════════════════════════════════════════
    //  ADMIN DASHBOARD
    // ══════════════════════════════════════════════════════════

    public class AdminRecentSubmission
    {
        public string Username      { get; set; } = string.Empty;
        public string ChallengeName { get; set; } = string.Empty;
        public string Category      { get; set; } = string.Empty;
        public int    Points        { get; set; }
        public bool   IsCorrect     { get; set; }
        public DateTime SubmittedAt { get; set; }

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - SubmittedAt.ToUniversalTime();
                if (diff.TotalMinutes < 1)  return "just now";
                if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalHours   < 24) return $"{(int)diff.TotalHours}h ago";
                return $"{(int)diff.TotalDays}d ago";
            }
        }
    }

    public class AdminUserActivityItem
    {
        public string Username    { get; set; } = string.Empty;
        public string? AvatarInitials { get; set; }
        public string Action      { get; set; } = string.Empty;  // "solved", "registered", "joined_team"
        public string Detail      { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        public string IconClass => Action switch
        {
            "solved"      => "fa-flag",
            "registered"  => "fa-user-plus",
            "joined_team" => "fa-users",
            "joined_comp" => "fa-bolt",
            _             => "fa-circle-info"
        };
        public string IconColorClass => Action switch
        {
            "solved"      => "success",
            "registered"  => "accent",
            "joined_team" => "info",
            "joined_comp" => "warning",
            _             => "accent"
        };

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - Timestamp.ToUniversalTime();
                if (diff.TotalMinutes < 1)  return "just now";
                if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalHours   < 24) return $"{(int)diff.TotalHours}h ago";
                return $"{(int)diff.TotalDays}d ago";
            }
        }
    }

    public class AdminDashboardViewModel
    {
        // Top stats
        public int TotalUsers          { get; set; }
        public int TotalChallenges     { get; set; }
        public int ActiveCompetitions  { get; set; }
        public int TotalSubmissions    { get; set; }

        // Deltas (for stat cards)
        public int NewUsersToday       { get; set; }
        public int SubmissionsToday    { get; set; }

        // Lists
        public List<AdminRecentSubmission>  RecentSubmissions { get; set; } = new();
        public List<AdminUserActivityItem>  UserActivity      { get; set; } = new();

        // Chart data (JSON-serialisable arrays — passed as ViewBag in controller)
        // These are set separately in the controller via ViewBag
    }

    // ══════════════════════════════════════════════════════════
    //  REPORTS
    // ══════════════════════════════════════════════════════════

    public class ChallengeSolveStatItem
    {
        public int    Id          { get; set; }
        public string Title       { get; set; } = string.Empty;
        public string Category    { get; set; } = string.Empty;
        public int    Points      { get; set; }
        public int    SolverCount { get; set; }
        public double SolveRate   { get; set; }   // 0–100
        public string Difficulty  { get; set; } = "Medium";
        public string DifficultyLower => Difficulty.ToLower();
    }

    public class TeamPerformanceItem
    {
        public int    Rank    { get; set; }
        public string Name    { get; set; } = string.Empty;
        public int    Score   { get; set; }
        public int    Solved  { get; set; }
        public int    Members { get; set; }
        public string RankBadgeClass => Rank switch { 1 => "gold", 2 => "silver", 3 => "bronze", _ => "other" };
    }

    public class ReportViewModel
    {
        public List<ChallengeSolveStatItem> MostSolved  { get; set; } = new();
        public List<ChallengeSolveStatItem> LeastSolved { get; set; } = new();
        public List<TeamPerformanceItem>    TeamPerformance { get; set; } = new();
        public int TotalSubmissions   { get; set; }
        public int CorrectSubmissions { get; set; }
        public int UniqueUsers        { get; set; }
        public double AvgSolveTime    { get; set; }  // hours
        public double CorrectRate     => TotalSubmissions > 0
            ? Math.Round((double)CorrectSubmissions / TotalSubmissions * 100, 1) : 0;
    }

    // ══════════════════════════════════════════════════════════
    //  ANNOUNCEMENTS
    // ══════════════════════════════════════════════════════════

    public class AnnouncementViewModel
    {
        public int      Id          { get; set; }
        public string   Title       { get; set; } = string.Empty;
        public string   Body        { get; set; } = string.Empty;
        public bool     IsPinned    { get; set; }
        public DateTime PublishedAt { get; set; }
        public string   AuthorName  { get; set; } = "Admin";

        public string Excerpt => Body.Length > 160 ? Body[..157] + "…" : Body;

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - PublishedAt.ToUniversalTime();
                if (diff.TotalMinutes < 1)  return "just now";
                if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalHours   < 24) return $"{(int)diff.TotalHours}h ago";
                if (diff.TotalDays    < 7)  return $"{(int)diff.TotalDays}d ago";
                return PublishedAt.ToString("dd MMM yyyy");
            }
        }
    }

    public class AnnouncementListViewModel
    {
        public List<AnnouncementViewModel> Announcements { get; set; } = new();
        public List<AnnouncementViewModel> Pinned   => Announcements.Where(a => a.IsPinned).ToList();
        public List<AnnouncementViewModel> Regular  => Announcements.Where(a => !a.IsPinned).ToList();
    }

    public class AnnouncementFormViewModel
    {
        public int Id { get; set; }  // 0 = new

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be 3–200 characters.")]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Body is required.")]
        [Display(Name = "Announcement Body")]
        public string Body { get; set; } = string.Empty;

        [Display(Name = "Pin this announcement")]
        public bool IsPinned { get; set; }

        public bool   IsEdit      => Id > 0;
        public string PageTitle   => IsEdit ? "Edit Announcement" : "Create Announcement";
        public string SubmitLabel => IsEdit ? "Save Changes" : "Publish";
    }
}
