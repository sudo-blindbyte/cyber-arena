using System;
using System.Collections.Generic;

namespace CyberArenaWebForms.Models
{
    // ─── Recent Submission ────────────────────────────────────
    public class RecentSubmissionItem
    {
        public string ChallengeName { get; set; }
        public string Category      { get; set; }
        public int    Points        { get; set; }
        public string Difficulty    { get; set; } // easy | medium | hard | expert
        public DateTime SolvedAt   { get; set; }
        public bool IsCorrect      { get; set; } = true;
    }

    // ─── Activity Item ────────────────────────────────────────
    public class ActivityItem
    {
        public string Icon           { get; set; } = "fa-flag";
        public string IconColorClass { get; set; } = "accent"; // accent | success | warning | danger | info
        public string Text           { get; set; }
        public DateTime Timestamp   { get; set; }

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - Timestamp.ToUniversalTime();
                if (diff.TotalMinutes < 1)  return "just now";
                if (diff.TotalMinutes < 60) return string.Format("{0}m ago", (int)diff.TotalMinutes);
                if (diff.TotalHours   < 24) return string.Format("{0}h ago", (int)diff.TotalHours);
                if (diff.TotalDays    < 7)  return string.Format("{0}d ago", (int)diff.TotalDays);
                return Timestamp.ToString("MMM d");
            }
        }
    }

    // ─── Active Competition ───────────────────────────────────
    public class ActiveCompetition
    {
        public string Name             { get; set; }
        public string Status           { get; set; } = "active"; // active | upcoming | ended
        public DateTime EndsAt        { get; set; }
        public int TeamRank            { get; set; }
        public int TotalTeams          { get; set; }
        public int ChallengesSolved    { get; set; }
        public int TotalChallenges     { get; set; }
        public int Score               { get; set; }

        public string TimeRemaining
        {
            get
            {
                var diff = EndsAt - DateTime.UtcNow;
                if (diff.TotalSeconds <= 0) return "Ended";
                if (diff.TotalDays >= 1)    return string.Format("{0}d {1}h", (int)diff.TotalDays, diff.Hours);
                if (diff.TotalHours >= 1)   return string.Format("{0}h {1}m", (int)diff.TotalHours, diff.Minutes);
                return string.Format("{0}m", diff.Minutes);
            }
        }

        public int ProgressPercent
        {
            get { return TotalChallenges > 0 ? (int)((double)ChallengesSolved / TotalChallenges * 100) : 0; }
        }
    }

    // ─── Score History Point ──────────────────────────────────
    public class ScoreHistoryPoint
    {
        public string Label         { get; set; }
        public int    Score         { get; set; }
        public int    HeightPercent { get; set; }
        public bool   IsHighest     { get; set; }
    }

    // ─── Category Progress ────────────────────────────────────
    public class CategoryProgress
    {
        public string Name       { get; set; }
        public string Icon       { get; set; } = "fa-terminal";
        public int    Solved     { get; set; }
        public int    Total      { get; set; }
        public string ColorClass { get; set; } = "accent";
        public int    Percent
        {
            get { return Total > 0 ? (int)((double)Solved / Total * 100) : 0; }
        }
    }

    // ─── Dashboard ViewModel ──────────────────────────────────
    public class DashboardViewModel
    {
        // Header
        public string Username        { get; set; } = "Participant";
        public string AvatarInitials  { get; set; }

        // Stats
        public int  TotalScore        { get; set; }
        public int  ChallengesSolved  { get; set; }
        public int  TotalChallenges   { get; set; }
        public int  CurrentRank       { get; set; }
        public int  TotalParticipants { get; set; }
        public string TeamName        { get; set; }
        public int  TeamRank          { get; set; }

        // Deltas
        public int  ScoreDelta        { get; set; }
        public int  RankDelta         { get; set; }

        // Computed
        public int  SolvedPercent
        {
            get { return TotalChallenges > 0 ? (int)((double)ChallengesSolved / TotalChallenges * 100) : 0; }
        }

        // Collections
        public List<RecentSubmissionItem> RecentSubmissions  { get; set; } = new List<RecentSubmissionItem>();
        public List<ActivityItem>         RecentActivities   { get; set; } = new List<ActivityItem>();
        public List<ActiveCompetition>    ActiveCompetitions { get; set; } = new List<ActiveCompetition>();
        public List<ScoreHistoryPoint>    ScoreHistory       { get; set; } = new List<ScoreHistoryPoint>();
        public List<CategoryProgress>     CategoryProgress   { get; set; } = new List<CategoryProgress>();
    }
}
