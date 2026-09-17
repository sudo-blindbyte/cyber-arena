namespace cyber_arena.ViewModels
{
    // ─── Recent Submission ────────────────────────────────────
    public class RecentSubmissionItem
    {
        public string ChallengeName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Points { get; set; }
        public string Difficulty { get; set; } = "medium"; // easy | medium | hard | expert
        public DateTime SolvedAt { get; set; }
        public bool IsCorrect { get; set; } = true;
    }

    // ─── Activity Item ────────────────────────────────────────
    public class ActivityItem
    {
        public string Icon { get; set; } = "fa-flag";
        public string IconColorClass { get; set; } = "accent"; // accent | success | warning | danger | info
        public string Text { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - Timestamp.ToUniversalTime();
                if (diff.TotalMinutes < 1)  return "just now";
                if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalHours   < 24) return $"{(int)diff.TotalHours}h ago";
                if (diff.TotalDays    < 7)  return $"{(int)diff.TotalDays}d ago";
                return Timestamp.ToString("MMM d");
            }
        }
    }

    // ─── Active Competition ───────────────────────────────────
    public class ActiveCompetition
    {
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = "active"; // active | upcoming | ended
        public DateTime EndsAt { get; set; }
        public int TeamRank { get; set; }
        public int TotalTeams { get; set; }
        public int ChallengesSolved { get; set; }
        public int TotalChallenges { get; set; }
        public int Score { get; set; }

        public string TimeRemaining
        {
            get
            {
                var diff = EndsAt - DateTime.UtcNow;
                if (diff.TotalSeconds <= 0) return "Ended";
                if (diff.TotalDays >= 1) return $"{(int)diff.TotalDays}d {diff.Hours}h";
                if (diff.TotalHours >= 1) return $"{(int)diff.TotalHours}h {diff.Minutes}m";
                return $"{diff.Minutes}m";
            }
        }

        public int ProgressPercent =>
            TotalChallenges > 0 ? (int)((double)ChallengesSolved / TotalChallenges * 100) : 0;
    }

    // ─── Score History Point ──────────────────────────────────
    public class ScoreHistoryPoint
    {
        public string Label { get; set; } = string.Empty;
        public int Score { get; set; }
        public int HeightPercent { get; set; }
        public bool IsHighest { get; set; }
    }

    // ─── Category Progress ────────────────────────────────────
    public class CategoryProgress
    {
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = "fa-terminal";
        public int Solved { get; set; }
        public int Total { get; set; }
        public string ColorClass { get; set; } = "accent";
        public int Percent => Total > 0 ? (int)((double)Solved / Total * 100) : 0;
    }

    // ─── Dashboard ViewModel ──────────────────────────────────
    public class DashboardViewModel
    {
        // Header
        public string Username { get; set; } = "Participant";
        public string? AvatarInitials { get; set; }

        // Stats
        public int TotalScore { get; set; }
        public int ChallengesSolved { get; set; }
        public int TotalChallenges { get; set; }
        public int CurrentRank { get; set; }
        public int TotalParticipants { get; set; }
        public string? TeamName { get; set; }
        public int TeamRank { get; set; }

        // Score change
        public int ScoreDelta { get; set; }   // points gained today
        public int RankDelta { get; set; }    // positions gained (positive = improved)

        // Progress percent
        public int SolvedPercent =>
            TotalChallenges > 0 ? (int)((double)ChallengesSolved / TotalChallenges * 100) : 0;

        // Collections
        public List<RecentSubmissionItem> RecentSubmissions { get; set; } = new();
        public List<ActivityItem> RecentActivities { get; set; } = new();
        public List<ActiveCompetition> ActiveCompetitions { get; set; } = new();
        public List<ScoreHistoryPoint> ScoreHistory { get; set; } = new();
        public List<CategoryProgress> CategoryProgress { get; set; } = new();
    }
}
