using System.Collections.Generic;
using System.Linq;

namespace CyberArenaWebForms.Models
{
    // ─── Leaderboard Entry ────────────────────────────────────
    public class LeaderboardEntry
    {
        public int    Rank             { get; set; }
        public string Username         { get; set; }
        public string AvatarInitials   { get; set; }
        public string AvatarGradient   { get; set; }  // "gold" | "silver" | "bronze" | ""
        public string TeamName         { get; set; }
        public int    TotalScore       { get; set; }
        public int    ChallengesSolved { get; set; }
        public bool   IsCurrentUser    { get; set; }
        public string Country          { get; set; }

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

        // Avatar alt class (matches original Razor logic)
        public string AvatarAltClass
        {
            get
            {
                switch (RankBadgeClass)
                {
                    case "gold":   return "alt-2";
                    case "silver": return "alt-0";
                    case "bronze": return "alt-2";
                    default:       return "";
                }
            }
        }
    }

    // ─── Leaderboard Page ViewModel ───────────────────────────
    public class LeaderboardViewModel
    {
        public List<LeaderboardEntry> Entries         { get; set; } = new List<LeaderboardEntry>();
        public string                 SearchQuery     { get; set; }
        public string                 FilterBy        { get; set; } = "All";
        public int                    CurrentUserRank { get; set; }
        public string                 CurrentUsername { get; set; } = "h4x0r_pro";

        // Top-3 podium helpers
        public LeaderboardEntry Gold   { get { return Entries.FirstOrDefault(e => e.Rank == 1); } }
        public LeaderboardEntry Silver { get { return Entries.FirstOrDefault(e => e.Rank == 2); } }
        public LeaderboardEntry Bronze { get { return Entries.FirstOrDefault(e => e.Rank == 3); } }
    }

    // ─── Mock Data (from LeaderboardController) ───────────────
    public static class LeaderboardMockData
    {
        public static List<LeaderboardEntry> GetAll()
        {
            return new List<LeaderboardEntry>
            {
                new LeaderboardEntry { Rank=1,  Username="CipherMaster",   AvatarInitials="CM", AvatarGradient="gold",   TeamName="ByteForce",   TotalScore=9850, ChallengesSolved=22, Country="US" },
                new LeaderboardEntry { Rank=2,  Username="n3tR4nger",      AvatarInitials="NR", AvatarGradient="silver", TeamName="CodeStrike",  TotalScore=9340, ChallengesSolved=21, Country="DE" },
                new LeaderboardEntry { Rank=3,  Username="0x_exploit",     AvatarInitials="0E", AvatarGradient="bronze", TeamName="NullByte",    TotalScore=8720, ChallengesSolved=20, Country="GB" },
                new LeaderboardEntry { Rank=4,  Username="shell_ghost",    AvatarInitials="SG", AvatarGradient="",       TeamName="ByteForce",   TotalScore=7980, ChallengesSolved=19, Country="FR" },
                new LeaderboardEntry { Rank=5,  Username="r00t_daemon",    AvatarInitials="RD", AvatarGradient="",       TeamName="PhantomByte", TotalScore=7450, ChallengesSolved=18, Country="IN" },
                new LeaderboardEntry { Rank=6,  Username="h4x0r_pro",      AvatarInitials="AC", AvatarGradient="",       TeamName="NullByte",    TotalScore=6920, ChallengesSolved=17, Country="IN", IsCurrentUser=true },
                new LeaderboardEntry { Rank=7,  Username="pwn_wizard",     AvatarInitials="PW", AvatarGradient="",       TeamName="CodeStrike",  TotalScore=6300, ChallengesSolved=16, Country="CA" },
                new LeaderboardEntry { Rank=8,  Username="xor_queen",      AvatarInitials="XQ", AvatarGradient="",       TeamName="ByteForce",   TotalScore=5870, ChallengesSolved=15, Country="AU" },
                new LeaderboardEntry { Rank=9,  Username="packet_ghost",   AvatarInitials="PG", AvatarGradient="",       TeamName="PhantomByte", TotalScore=5400, ChallengesSolved=14, Country="BR" },
                new LeaderboardEntry { Rank=10, Username="hex_ninja",      AvatarInitials="HN", AvatarGradient="",       TeamName="NullByte",    TotalScore=4990, ChallengesSolved=13, Country="JP" },
                new LeaderboardEntry { Rank=11, Username="vuln_hunter",    AvatarInitials="VH", AvatarGradient="",       TeamName="ShadowStack", TotalScore=4620, ChallengesSolved=12, Country="KR" },
                new LeaderboardEntry { Rank=12, Username="stack_smasher",  AvatarInitials="SS", AvatarGradient="",       TeamName="CodeStrike",  TotalScore=4200, ChallengesSolved=11, Country="NL" },
                new LeaderboardEntry { Rank=13, Username="rop_chain",      AvatarInitials="RC", AvatarGradient="",       TeamName="ByteForce",   TotalScore=3850, ChallengesSolved=10, Country="PL" },
                new LeaderboardEntry { Rank=14, Username="fuzzy_logic",    AvatarInitials="FL", AvatarGradient="",       TeamName="ShadowStack", TotalScore=3500, ChallengesSolved=9,  Country="SE" },
                new LeaderboardEntry { Rank=15, Username="b1nary_b0ss",    AvatarInitials="BB", AvatarGradient="",       TeamName="NullByte",    TotalScore=3200, ChallengesSolved=8,  Country="ES" },
                new LeaderboardEntry { Rank=16, Username="crypto_cracker", AvatarInitials="CC", AvatarGradient="",       TeamName="PhantomByte", TotalScore=2900, ChallengesSolved=7,  Country="IT" },
                new LeaderboardEntry { Rank=17, Username="rev_eng_pro",    AvatarInitials="RP", AvatarGradient="",       TeamName="CodeStrike",  TotalScore=2600, ChallengesSolved=6,  Country="PT" },
                new LeaderboardEntry { Rank=18, Username="mem_leak",       AvatarInitials="ML", AvatarGradient="",       TeamName="ShadowStack", TotalScore=2300, ChallengesSolved=5,  Country="RO" },
                new LeaderboardEntry { Rank=19, Username="sqli_master",    AvatarInitials="SM", AvatarGradient="",       TeamName="ByteForce",   TotalScore=2100, ChallengesSolved=4,  Country="PL" },
                new LeaderboardEntry { Rank=20, Username="xss_panda",      AvatarInitials="XP", AvatarGradient="",       TeamName="NullByte",    TotalScore=1850, ChallengesSolved=3,  Country="HU" },
            };
        }
    }
}
