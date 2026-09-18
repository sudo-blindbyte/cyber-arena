using Microsoft.AspNetCore.Mvc;
using cyber_arena.ViewModels;

namespace cyber_arena.Controllers
{
    /// <summary>
    /// Teams — list, create, and view team details.
    /// TODO: Replace mock data with EF Core once Identity is connected.
    /// </summary>
    public class TeamsController : Controller
    {
        // ─── Mock Data ────────────────────────────────────────
        private static List<TeamListItemViewModel> GetMockTeams() =>
        [
            new() { Id=1, Name="ByteForce",   CaptainUsername="CipherMaster", Rank=1, Score=32400, MemberCount=4, ChallengesSolved=58, Country="US" },
            new() { Id=2, Name="CodeStrike",  CaptainUsername="n3tR4nger",    Rank=2, Score=28900, MemberCount=3, ChallengesSolved=52, Country="DE" },
            new() { Id=3, Name="NullByte",    CaptainUsername="0x_exploit",   Rank=3, Score=25600, MemberCount=4, ChallengesSolved=47, Country="GB", IsMyTeam=true },
            new() { Id=4, Name="PhantomByte", CaptainUsername="r00t_daemon",  Rank=4, Score=21300, MemberCount=3, ChallengesSolved=41, Country="IN" },
            new() { Id=5, Name="ShadowStack", CaptainUsername="vuln_hunter",  Rank=5, Score=17800, MemberCount=2, ChallengesSolved=34, Country="KR" },
            new() { Id=6, Name="ZeroDay",     CaptainUsername="b1nary_b0ss",  Rank=6, Score=14200, MemberCount=4, ChallengesSolved=28, Country="ES" },
            new() { Id=7, Name="PwnStars",    CaptainUsername="stack_smasher",Rank=7, Score=11500, MemberCount=2, ChallengesSolved=22, Country="NL" },
            new() { Id=8, Name="CryptoKings", CaptainUsername="crypto_cracker",Rank=8, Score=9100, MemberCount=3, ChallengesSolved=17, Country="IT" },
        ];

        private static TeamDetailsViewModel GetMockTeamDetails(int id) => id switch
        {
            1 => new()
            {
                Id=1, Name="ByteForce", Description="Elite offensive security team specialising in web exploitation and binary pwn.", CaptainUsername="CipherMaster",
                Rank=1, Score=32400, ChallengesSolved=58, TotalChallenges=72, Country="US",
                CreatedAt=DateTime.UtcNow.AddDays(-90),
                Members =
                [
                    new() { Id=1, Username="CipherMaster",  AvatarInitials="CM", AvatarClass="",     IsCaptain=true,  Score=9850, ChallengesSolved=22, JoinedAt=DateTime.UtcNow.AddDays(-90) },
                    new() { Id=2, Username="shell_ghost",   AvatarInitials="SG", AvatarClass="alt-1", IsCaptain=false, Score=7980, ChallengesSolved=19, JoinedAt=DateTime.UtcNow.AddDays(-85) },
                    new() { Id=3, Username="xor_queen",     AvatarInitials="XQ", AvatarClass="alt-2", IsCaptain=false, Score=5870, ChallengesSolved=15, JoinedAt=DateTime.UtcNow.AddDays(-80) },
                    new() { Id=4, Username="rop_chain",     AvatarInitials="RC", AvatarClass="alt-3", IsCaptain=false, Score=3850, ChallengesSolved=10, JoinedAt=DateTime.UtcNow.AddDays(-70) },
                ],
                ScoreHistory       = [800, 2100, 3900, 6200, 9100, 14500, 20300, 28100, 32400],
                ScoreHistoryLabels = ["Sep 1","Sep 8","Sep 15","Sep 22","Sep 29","Oct 6","Oct 13","Oct 20","Oct 27"],
            },
            2 => new()
            {
                Id=2, Name="CodeStrike", Description="Red team veterans from Europe focused on cryptography and reverse engineering.", CaptainUsername="n3tR4nger",
                Rank=2, Score=28900, ChallengesSolved=52, TotalChallenges=72, Country="DE",
                CreatedAt=DateTime.UtcNow.AddDays(-80),
                Members =
                [
                    new() { Id=5, Username="n3tR4nger",    AvatarInitials="NR", AvatarClass="",     IsCaptain=true,  Score=9340, ChallengesSolved=21, JoinedAt=DateTime.UtcNow.AddDays(-80) },
                    new() { Id=6, Username="pwn_wizard",   AvatarInitials="PW", AvatarClass="alt-2", IsCaptain=false, Score=6300, ChallengesSolved=16, JoinedAt=DateTime.UtcNow.AddDays(-75) },
                    new() { Id=7, Username="stack_smasher",AvatarInitials="SS", AvatarClass="alt-3", IsCaptain=false, Score=4200, ChallengesSolved=11, JoinedAt=DateTime.UtcNow.AddDays(-65) },
                ],
                ScoreHistory       = [500, 1800, 3400, 5900, 8800, 13200, 18600, 24000, 28900],
                ScoreHistoryLabels = ["Sep 1","Sep 8","Sep 15","Sep 22","Sep 29","Oct 6","Oct 13","Oct 20","Oct 27"],
            },
            3 => new()
            {
                Id=3, Name="NullByte", Description="Your team — hackers by day, defenders by night. Specialising in forensics and OSINT.", CaptainUsername="0x_exploit",
                Rank=3, Score=25600, ChallengesSolved=47, TotalChallenges=72, Country="GB",
                CreatedAt=DateTime.UtcNow.AddDays(-75), IsMyTeam=true,
                Members =
                [
                    new() { Id=8,  Username="0x_exploit",  AvatarInitials="0E", AvatarClass="",     IsCaptain=true,  Score=8720, ChallengesSolved=20, JoinedAt=DateTime.UtcNow.AddDays(-75) },
                    new() { Id=9,  Username="h4x0r_pro",   AvatarInitials="AC", AvatarClass="alt-1", IsCaptain=false, Score=6920, ChallengesSolved=17, JoinedAt=DateTime.UtcNow.AddDays(-70) },
                    new() { Id=10, Username="hex_ninja",   AvatarInitials="HN", AvatarClass="alt-2", IsCaptain=false, Score=4990, ChallengesSolved=13, JoinedAt=DateTime.UtcNow.AddDays(-60) },
                    new() { Id=11, Username="xss_panda",   AvatarInitials="XP", AvatarClass="alt-3", IsCaptain=false, Score=1850, ChallengesSolved=3,  JoinedAt=DateTime.UtcNow.AddDays(-20) },
                ],
                ScoreHistory       = [300, 1400, 2900, 5000, 7600, 11900, 17000, 22500, 25600],
                ScoreHistoryLabels = ["Sep 1","Sep 8","Sep 15","Sep 22","Sep 29","Oct 6","Oct 13","Oct 20","Oct 27"],
            },
            _ => new()
            {
                Id=id, Name="Unknown Team", Rank=99, Score=0, ChallengesSolved=0, TotalChallenges=72,
                CaptainUsername="—", CreatedAt=DateTime.UtcNow.AddDays(-30)
            }
        };

        // ─── GET /Teams ───────────────────────────────────────
        [HttpGet]
        public IActionResult Index(string? q = null)
        {
            ViewData["Title"]      = "Teams";
            ViewData["Breadcrumb"] = "Teams";

            var teams = GetMockTeams();

            if (!string.IsNullOrWhiteSpace(q))
                teams = teams.Where(t => t.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                                         t.CaptainUsername.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();

            var vm = new TeamListViewModel
            {
                Teams       = teams,
                SearchQuery = q,
                UserHasTeam = true,
                MyTeamId    = 3
            };

            return View(vm);
        }

        // ─── GET /Teams/Create ────────────────────────────────
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"]      = "Create Team";
            ViewData["Breadcrumb"] = "Teams · Create";
            return View(new TeamCreateViewModel());
        }

        // ─── POST /Teams/Create ───────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeamCreateViewModel model)
        {
            ViewData["Title"]      = "Create Team";
            ViewData["Breadcrumb"] = "Teams · Create";

            if (!ModelState.IsValid)
                return View(model);

            // TODO: EF Core — create team and assign current user as captain
            // var team = new Team { Name = model.Name, Description = model.Description, ... };
            // _db.Teams.Add(team);
            // await _db.SaveChangesAsync();

            TempData["AdminSuccess"] = $"Team \"{model.Name}\" created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ─── GET /Teams/Details/{id} ──────────────────────────
        [HttpGet]
        public IActionResult Details(int id)
        {
            var vm = GetMockTeamDetails(id);
            if (vm == null) return NotFound();

            ViewData["Title"]      = vm.Name;
            ViewData["Breadcrumb"] = $"Teams · {vm.Name}";
            return View(vm);
        }
    }
}
