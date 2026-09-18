using Microsoft.AspNetCore.Mvc;
using cyber_arena.ViewModels;

namespace cyber_arena.Controllers
{
    /// <summary>
    /// Leaderboard — displays ranked participants with podium top-3.
    /// TODO: Replace mock data with EF Core query once Identity is connected.
    /// </summary>
    public class LeaderboardController : Controller
    {
        // ─── Mock Data ────────────────────────────────────────
        private static List<LeaderboardEntryViewModel> GetMockEntries() =>
        [
            new() { Rank=1,  Username="CipherMaster",  AvatarInitials="CM", AvatarGradient="gold",   TeamName="ByteForce",    TotalScore=9850, ChallengesSolved=22, Country="US" },
            new() { Rank=2,  Username="n3tR4nger",     AvatarInitials="NR", AvatarGradient="silver", TeamName="CodeStrike",   TotalScore=9340, ChallengesSolved=21, Country="DE" },
            new() { Rank=3,  Username="0x_exploit",    AvatarInitials="0E", AvatarGradient="bronze", TeamName="NullByte",     TotalScore=8720, ChallengesSolved=20, Country="GB" },
            new() { Rank=4,  Username="shell_ghost",   AvatarInitials="SG", AvatarGradient="",       TeamName="ByteForce",    TotalScore=7980, ChallengesSolved=19, Country="FR" },
            new() { Rank=5,  Username="r00t_daemon",   AvatarInitials="RD", AvatarGradient="",       TeamName="PhantomByte",  TotalScore=7450, ChallengesSolved=18, Country="IN" },
            new() { Rank=6,  Username="h4x0r_pro",     AvatarInitials="AC", AvatarGradient="",       TeamName="NullByte",     TotalScore=6920, ChallengesSolved=17, Country="IN",  IsCurrentUser=true },
            new() { Rank=7,  Username="pwn_wizard",    AvatarInitials="PW", AvatarGradient="",       TeamName="CodeStrike",   TotalScore=6300, ChallengesSolved=16, Country="CA" },
            new() { Rank=8,  Username="xor_queen",     AvatarInitials="XQ", AvatarGradient="",       TeamName="ByteForce",    TotalScore=5870, ChallengesSolved=15, Country="AU" },
            new() { Rank=9,  Username="packet_ghost",  AvatarInitials="PG", AvatarGradient="",       TeamName="PhantomByte",  TotalScore=5400, ChallengesSolved=14, Country="BR" },
            new() { Rank=10, Username="hex_ninja",     AvatarInitials="HN", AvatarGradient="",       TeamName="NullByte",     TotalScore=4990, ChallengesSolved=13, Country="JP" },
            new() { Rank=11, Username="vuln_hunter",   AvatarInitials="VH", AvatarGradient="",       TeamName="ShadowStack",  TotalScore=4620, ChallengesSolved=12, Country="KR" },
            new() { Rank=12, Username="stack_smasher", AvatarInitials="SS", AvatarGradient="",       TeamName="CodeStrike",   TotalScore=4200, ChallengesSolved=11, Country="NL" },
            new() { Rank=13, Username="rop_chain",     AvatarInitials="RC", AvatarGradient="",       TeamName="ByteForce",    TotalScore=3850, ChallengesSolved=10, Country="PL" },
            new() { Rank=14, Username="fuzzy_logic",   AvatarInitials="FL", AvatarGradient="",       TeamName="ShadowStack",  TotalScore=3500, ChallengesSolved=9,  Country="SE" },
            new() { Rank=15, Username="b1nary_b0ss",   AvatarInitials="BB", AvatarGradient="",       TeamName="NullByte",     TotalScore=3200, ChallengesSolved=8,  Country="ES" },
            new() { Rank=16, Username="crypto_cracker",AvatarInitials="CC", AvatarGradient="",       TeamName="PhantomByte",  TotalScore=2900, ChallengesSolved=7,  Country="IT" },
            new() { Rank=17, Username="rev_eng_pro",   AvatarInitials="RP", AvatarGradient="",       TeamName="CodeStrike",   TotalScore=2600, ChallengesSolved=6,  Country="PT" },
            new() { Rank=18, Username="mem_leak",      AvatarInitials="ML", AvatarGradient="",       TeamName="ShadowStack",  TotalScore=2300, ChallengesSolved=5,  Country="RO" },
            new() { Rank=19, Username="sqli_master",   AvatarInitials="SM", AvatarGradient="",       TeamName="ByteForce",    TotalScore=2100, ChallengesSolved=4,  Country="PL" },
            new() { Rank=20, Username="xss_panda",     AvatarInitials="XP", AvatarGradient="",       TeamName="NullByte",     TotalScore=1850, ChallengesSolved=3,  Country="HU" },
        ];

        // ─── GET /Leaderboard ─────────────────────────────────
        [HttpGet]
        public IActionResult Index(string? q = null, string? filter = null)
        {
            ViewData["Title"]      = "Leaderboard";
            ViewData["Breadcrumb"] = "Leaderboard";

            var entries = GetMockEntries();

            // Search
            if (!string.IsNullOrWhiteSpace(q))
                entries = entries.Where(e =>
                    e.Username.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                    e.TeamName.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();

            var vm = new LeaderboardViewModel
            {
                Entries         = entries,
                SearchQuery     = q,
                FilterBy        = filter ?? "All",
                CurrentUserRank = 6,
                CurrentUsername = "h4x0r_pro"
            };

            return View(vm);
        }
    }
}
