using Microsoft.AspNetCore.Mvc;
using cyber_arena.ViewModels;

namespace cyber_arena.Controllers
{
    /// <summary>
    /// Competitions — list all competitions and view individual competition details.
    /// TODO: Replace mock data with EF Core once Identity is connected.
    /// </summary>
    public class CompetitionsController : Controller
    {
        // ─── Mock Data ────────────────────────────────────────
        public static List<CompetitionListItemViewModel> GetMockCompetitions() =>
        [
            new()
            {
                Id=1, Name="CyberArena Open 2026",
                Description="Annual flagship CTF open to all skill levels. Compete across 8 categories.",
                Status="active", StartAt=DateTime.UtcNow.AddDays(-2), EndAt=DateTime.UtcNow.AddDays(3),
                ChallengeCount=24, ParticipantCount=312, MaxParticipants=500, IsJoined=true, Format="Individual"
            },
            new()
            {
                Id=2, Name="Red Team Rumble",
                Description="Advanced offensive security competition — pwn, rev, and crypto only.",
                Status="upcoming", StartAt=DateTime.UtcNow.AddDays(5), EndAt=DateTime.UtcNow.AddDays(7),
                ChallengeCount=12, ParticipantCount=87, MaxParticipants=200, IsJoined=false, Format="Team"
            },
            new()
            {
                Id=3, Name="Forensics & OSINT Sprint",
                Description="48-hour speed competition focused on digital forensics and OSINT challenges.",
                Status="upcoming", StartAt=DateTime.UtcNow.AddDays(14), EndAt=DateTime.UtcNow.AddDays(16),
                ChallengeCount=16, ParticipantCount=45, MaxParticipants=150, IsJoined=false, Format="Individual"
            },
            new()
            {
                Id=4, Name="Beginner Bootcamp",
                Description="Introductory CTF for newcomers to the security field. Learn while you hack.",
                Status="ended", StartAt=DateTime.UtcNow.AddDays(-30), EndAt=DateTime.UtcNow.AddDays(-28),
                ChallengeCount=10, ParticipantCount=198, MaxParticipants=300, IsJoined=true, Format="Individual"
            },
            new()
            {
                Id=5, Name="Crypto Clash",
                Description="Pure cryptography competition. Break the ciphers and earn the flags.",
                Status="ended", StartAt=DateTime.UtcNow.AddDays(-60), EndAt=DateTime.UtcNow.AddDays(-58),
                ChallengeCount=14, ParticipantCount=276, MaxParticipants=400, IsJoined=false, Format="Individual"
            },
        ];

        private static CompetitionDetailsViewModel GetMockCompetitionDetails(int id)
        {
            var list = GetMockCompetitions();
            var item = list.FirstOrDefault(c => c.Id == id);
            if (item == null) return null!;

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
                Challenges =
                [
                    new() { Id=1,  Title="SQL Injection Basics",       Category=ChallengeCategories.Web,       Difficulty="Easy",   Points=100, SolverCount=184, IsSolved=true  },
                    new() { Id=2,  Title="XSS Reflected Attack",       Category=ChallengeCategories.Web,       Difficulty="Easy",   Points=150, SolverCount=142, IsSolved=true  },
                    new() { Id=3,  Title="CSRF Token Bypass",          Category=ChallengeCategories.Web,       Difficulty="Medium", Points=250, SolverCount=67,  IsSolved=false },
                    new() { Id=4,  Title="Caesar's Secret",            Category=ChallengeCategories.Crypto,    Difficulty="Easy",   Points=75,  SolverCount=213, IsSolved=true  },
                    new() { Id=5,  Title="RSA Weak Key",               Category=ChallengeCategories.Crypto,    Difficulty="Medium", Points=300, SolverCount=58,  IsSolved=false },
                    new() { Id=6,  Title="Crackme Level 1",            Category=ChallengeCategories.Reversing, Difficulty="Easy",   Points=100, SolverCount=134, IsSolved=true  },
                    new() { Id=7,  Title="Anti-Debug Bypass",          Category=ChallengeCategories.Reversing, Difficulty="Medium", Points=350, SolverCount=41,  IsSolved=false },
                    new() { Id=8,  Title="Wireshark Hunt",             Category=ChallengeCategories.Forensics, Difficulty="Easy",   Points=150, SolverCount=165, IsSolved=false },
                    new() { Id=9,  Title="Hidden in Plain Sight",      Category=ChallengeCategories.Stego,     Difficulty="Easy",   Points=100, SolverCount=152, IsSolved=false },
                    new() { Id=10, Title="The Mysterious Developer",   Category=ChallengeCategories.Osint,     Difficulty="Easy",   Points=100, SolverCount=193, IsSolved=false },
                    new() { Id=11, Title="Buffer Overflow 101",        Category=ChallengeCategories.Pwn,       Difficulty="Medium", Points=250, SolverCount=76,  IsSolved=false },
                    new() { Id=12, Title="BGP Route Poisoning",        Category=ChallengeCategories.Networking,Difficulty="Hard",   Points=500, SolverCount=18,  IsSolved=false },
                ],
                Leaderboard =
                [
                    new() { Rank=1, Username="CipherMaster",  TeamName="ByteForce",   Score=975, Solved=9, LastSolve=DateTime.UtcNow.AddMinutes(-45),  IsCurrentUser=false },
                    new() { Rank=2, Username="n3tR4nger",     TeamName="CodeStrike",  Score=925, Solved=8, LastSolve=DateTime.UtcNow.AddMinutes(-120), IsCurrentUser=false },
                    new() { Rank=3, Username="0x_exploit",    TeamName="NullByte",    Score=870, Solved=8, LastSolve=DateTime.UtcNow.AddMinutes(-200), IsCurrentUser=false },
                    new() { Rank=4, Username="shell_ghost",   TeamName="ByteForce",   Score=798, Solved=7, LastSolve=DateTime.UtcNow.AddHours(-3),    IsCurrentUser=false },
                    new() { Rank=5, Username="h4x0r_pro",     TeamName="NullByte",    Score=725, Solved=6, LastSolve=DateTime.UtcNow.AddHours(-4),    IsCurrentUser=true  },
                    new() { Rank=6, Username="pwn_wizard",    TeamName="CodeStrike",  Score=600, Solved=5, LastSolve=DateTime.UtcNow.AddHours(-5),    IsCurrentUser=false },
                    new() { Rank=7, Username="xor_queen",     TeamName="ByteForce",   Score=525, Solved=4, LastSolve=DateTime.UtcNow.AddHours(-7),    IsCurrentUser=false },
                    new() { Rank=8, Username="packet_ghost",  TeamName="PhantomByte", Score=450, Solved=4, LastSolve=DateTime.UtcNow.AddHours(-9),    IsCurrentUser=false },
                ],
            };
        }

        // ─── GET /Competitions ────────────────────────────────
        [HttpGet]
        public IActionResult Index(string? status = null)
        {
            ViewData["Title"]      = "Competitions";
            ViewData["Breadcrumb"] = "Competitions";

            var comps = GetMockCompetitions();

            var vm = new CompetitionListViewModel
            {
                Competitions = comps,
                FilterStatus = status ?? "all"
            };

            return View(vm);
        }

        // ─── GET /Competitions/Details/{id} ───────────────────
        [HttpGet]
        public IActionResult Details(int id)
        {
            var vm = GetMockCompetitionDetails(id);
            if (vm == null) return NotFound();

            ViewData["Title"]      = vm.Name;
            ViewData["Breadcrumb"] = $"Competitions · {vm.Name}";
            return View(vm);
        }

        // ─── POST /Competitions/Join/{id} ─────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(int id)
        {
            // TODO: EF Core — register current user in competition
            TempData["AdminSuccess"] = "You have successfully joined the competition!";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
