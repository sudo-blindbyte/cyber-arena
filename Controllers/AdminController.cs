using Microsoft.AspNetCore.Mvc;
using cyber_arena.ViewModels;
using cyber_arena.Filters;

namespace cyber_arena.Controllers
{
    /// <summary>
    /// Admin controller — protected by [AdminAuthorize] (session-based).
    /// All routes require a valid admin session (set by AdminLoginController).
    /// TODO: Replace [AdminAuthorize] with [Authorize(Roles="Admin")] once ASP.NET Identity is connected.
    /// </summary>
    [AdminAuthorize]
    public class AdminController : Controller
    {
        // ─── Mock Data ────────────────────────────────────────
        // TODO: Replace with EF Core DbContext injection
        private static List<AdminChallengeListItemViewModel> GetMockAdminChallenges() =>
        [
            new() { Id=1,  Title="SQL Injection Basics",        Category=ChallengeCategories.Web,        Difficulty="Easy",   Points=100,  Status="Active",   SolverCount=184, CreatedAt=DateTime.UtcNow.AddDays(-30), HasAttachment=false },
            new() { Id=2,  Title="XSS Reflected Attack",        Category=ChallengeCategories.Web,        Difficulty="Easy",   Points=150,  Status="Active",   SolverCount=142, CreatedAt=DateTime.UtcNow.AddDays(-29), HasAttachment=false },
            new() { Id=3,  Title="CSRF Token Bypass",           Category=ChallengeCategories.Web,        Difficulty="Medium", Points=250,  Status="Active",   SolverCount=67,  CreatedAt=DateTime.UtcNow.AddDays(-28), HasAttachment=false },
            new() { Id=4,  Title="JWT Secret Cracking",         Category=ChallengeCategories.Web,        Difficulty="Medium", Points=300,  Status="Active",   SolverCount=53,  CreatedAt=DateTime.UtcNow.AddDays(-27), HasAttachment=true  },
            new() { Id=5,  Title="GraphQL Introspection Leak",  Category=ChallengeCategories.Web,        Difficulty="Hard",   Points=500,  Status="Active",   SolverCount=21,  CreatedAt=DateTime.UtcNow.AddDays(-26), HasAttachment=false },
            new() { Id=6,  Title="Caesar's Secret",             Category=ChallengeCategories.Crypto,     Difficulty="Easy",   Points=75,   Status="Active",   SolverCount=213, CreatedAt=DateTime.UtcNow.AddDays(-25), HasAttachment=false },
            new() { Id=7,  Title="RSA Weak Key",                Category=ChallengeCategories.Crypto,     Difficulty="Medium", Points=300,  Status="Active",   SolverCount=58,  CreatedAt=DateTime.UtcNow.AddDays(-24), HasAttachment=true  },
            new() { Id=8,  Title="AES ECB Penguin",             Category=ChallengeCategories.Crypto,     Difficulty="Medium", Points=250,  Status="Active",   SolverCount=72,  CreatedAt=DateTime.UtcNow.AddDays(-23), HasAttachment=false },
            new() { Id=9,  Title="Elliptic Curve Discrete Log", Category=ChallengeCategories.Crypto,     Difficulty="Hard",   Points=600,  Status="Draft",    SolverCount=0,   CreatedAt=DateTime.UtcNow.AddDays(-5),  HasAttachment=false },
            new() { Id=10, Title="Crackme Level 1",             Category=ChallengeCategories.Reversing,  Difficulty="Easy",   Points=100,  Status="Active",   SolverCount=134, CreatedAt=DateTime.UtcNow.AddDays(-22), HasAttachment=true  },
            new() { Id=11, Title="Anti-Debug Bypass",           Category=ChallengeCategories.Reversing,  Difficulty="Medium", Points=350,  Status="Active",   SolverCount=41,  CreatedAt=DateTime.UtcNow.AddDays(-21), HasAttachment=true  },
            new() { Id=12, Title="Obfuscated Python",           Category=ChallengeCategories.Reversing,  Difficulty="Medium", Points=275,  Status="Active",   SolverCount=63,  CreatedAt=DateTime.UtcNow.AddDays(-20), HasAttachment=true  },
            new() { Id=13, Title="LLVM Bitcode Maze",           Category=ChallengeCategories.Reversing,  Difficulty="Hard",   Points=550,  Status="Draft",    SolverCount=0,   CreatedAt=DateTime.UtcNow.AddDays(-3),  HasAttachment=false },
            new() { Id=14, Title="Wireshark Hunt",              Category=ChallengeCategories.Forensics,  Difficulty="Easy",   Points=150,  Status="Active",   SolverCount=165, CreatedAt=DateTime.UtcNow.AddDays(-19), HasAttachment=true  },
            new() { Id=15, Title="Memory Dump Analysis",        Category=ChallengeCategories.Forensics,  Difficulty="Medium", Points=325,  Status="Active",   SolverCount=47,  CreatedAt=DateTime.UtcNow.AddDays(-18), HasAttachment=true  },
            new() { Id=16, Title="Deleted File Recovery",       Category=ChallengeCategories.Forensics,  Difficulty="Medium", Points=200,  Status="Active",   SolverCount=81,  CreatedAt=DateTime.UtcNow.AddDays(-17), HasAttachment=true  },
            new() { Id=17, Title="TCP Handshake Hijack",        Category=ChallengeCategories.Networking, Difficulty="Easy",   Points=125,  Status="Active",   SolverCount=109, CreatedAt=DateTime.UtcNow.AddDays(-16), HasAttachment=false },
            new() { Id=18, Title="BGP Route Poisoning",         Category=ChallengeCategories.Networking, Difficulty="Hard",   Points=500,  Status="Active",   SolverCount=18,  CreatedAt=DateTime.UtcNow.AddDays(-15), HasAttachment=false },
            new() { Id=19, Title="Hidden in Plain Sight",       Category=ChallengeCategories.Stego,      Difficulty="Easy",   Points=100,  Status="Active",   SolverCount=152, CreatedAt=DateTime.UtcNow.AddDays(-14), HasAttachment=true  },
            new() { Id=20, Title="Audio Spectrogram Secret",    Category=ChallengeCategories.Stego,      Difficulty="Medium", Points=225,  Status="Active",   SolverCount=74,  CreatedAt=DateTime.UtcNow.AddDays(-13), HasAttachment=true  },
            new() { Id=21, Title="The Mysterious Developer",    Category=ChallengeCategories.Osint,      Difficulty="Easy",   Points=100,  Status="Active",   SolverCount=193, CreatedAt=DateTime.UtcNow.AddDays(-12), HasAttachment=false },
            new() { Id=22, Title="GeoGuessr Intelligence",      Category=ChallengeCategories.Osint,      Difficulty="Medium", Points=200,  Status="Archived", SolverCount=88,  CreatedAt=DateTime.UtcNow.AddDays(-60), UpdatedAt=DateTime.UtcNow.AddDays(-7), HasAttachment=false },
            new() { Id=23, Title="Buffer Overflow 101",         Category=ChallengeCategories.Pwn,        Difficulty="Medium", Points=250,  Status="Active",   SolverCount=76,  CreatedAt=DateTime.UtcNow.AddDays(-11), HasAttachment=true  },
            new() { Id=24, Title="Kernel Exploit 0-day",        Category=ChallengeCategories.Pwn,        Difficulty="Hard",   Points=1000, Status="Active",   SolverCount=5,   CreatedAt=DateTime.UtcNow.AddDays(-10), HasAttachment=true  },
        ];

        private static AdminChallengeListItemViewModel? GetAdminById(int id)
            => GetMockAdminChallenges().FirstOrDefault(c => c.Id == id);

        // ─── GET /Admin/Challenges ────────────────────────────
        [HttpGet]
        [Route("Admin/Challenges")]
        public IActionResult Challenges(string? q = null, string? category = null, string? status = null)
        {
            ViewData["Title"]      = "Challenge Management";
            ViewData["Breadcrumb"] = "Admin · Challenges";

            // TODO: Replace with EF Core query
            // var challenges = await _db.Challenges
            //     .Select(c => new AdminChallengeListItemViewModel { ... })
            //     .ToListAsync();

            var challenges = GetMockAdminChallenges();

            if (!string.IsNullOrWhiteSpace(q))
                challenges = challenges.Where(c =>
                    c.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                    c.Category.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!string.IsNullOrWhiteSpace(category) && category != "All")
                challenges = challenges.Where(c => c.Category == category).ToList();
            if (!string.IsNullOrWhiteSpace(status) && status != "All")
                challenges = challenges.Where(c => c.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

            var vm = new AdminChallengeListViewModel
            {
                Challenges      = challenges,
                SearchQuery     = q,
                FilterCategory  = category,
                FilterStatus    = status,
            };
            return View("Challenges/Index", vm);
        }

        // ─── GET /Admin/Challenges/Details/{id} ───────────────
        [HttpGet]
        [Route("Admin/Challenges/Details/{id}")]
        public IActionResult ChallengeDetails(int id)
        {
            // TODO: Replace with EF Core query
            var ch = GetAdminById(id);
            if (ch == null) return NotFound();
            ViewData["Title"]      = $"Challenge #{id} — {ch.Title}";
            ViewData["Breadcrumb"] = "Admin · Challenge Details";
            return View("Challenges/Details", ch);
        }

        // ─── GET /Admin/Challenges/Create ─────────────────────
        [HttpGet]
        [Route("Admin/Challenges/Create")]
        public IActionResult ChallengeCreate()
        {
            ViewData["Title"]      = "Create Challenge";
            ViewData["Breadcrumb"] = "Admin · Create Challenge";
            return View("Challenges/Form", new ChallengeFormViewModel());
        }

        // ─── POST /Admin/Challenges/Create ────────────────────
        [HttpPost]
        [Route("Admin/Challenges/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult ChallengeCreate(ChallengeFormViewModel model)
        {
            ViewData["Title"]      = "Create Challenge";
            ViewData["Breadcrumb"] = "Admin · Create Challenge";

            if (!ModelState.IsValid)
                return View("Challenges/Form", model);

            // TODO: Connect EF Core
            // var challenge = new Challenge
            // {
            //     Title       = model.Title,
            //     Description = model.Description,
            //     Category    = model.Category,
            //     Difficulty  = model.Difficulty,
            //     Points      = model.Points,
            //     Flag        = model.Flag,   // TODO: hash the flag at rest
            //     Status      = model.Status,
            //     Hint        = model.Hint,
            //     HintPenalty = model.HintPenalty,
            //     CreatedAt   = DateTime.UtcNow,
            // };
            // if (model.AttachmentFile != null)
            // {
            //     // TODO: Securely save the file via IFormFile
            //     // DO NOT save directly to wwwroot — use a secure storage path
            // }
            // _db.Challenges.Add(challenge);
            // await _db.SaveChangesAsync();

            TempData["AdminSuccess"] = $"Challenge \"{model.Title}\" created successfully.";
            return RedirectToAction(nameof(Challenges));
        }

        // ─── GET /Admin/Challenges/Edit/{id} ──────────────────
        [HttpGet]
        [Route("Admin/Challenges/Edit/{id}")]
        public IActionResult ChallengeEdit(int id)
        {
            // TODO: Replace with EF Core query
            var ch = GetAdminById(id);
            if (ch == null) return NotFound();

            var vm = new ChallengeFormViewModel
            {
                Id                   = ch.Id,
                Title                = ch.Title,
                Category             = ch.Category,
                Difficulty           = ch.Difficulty,
                Points               = ch.Points,
                Status               = ch.Status,
                Flag                 = "CTF{placeholder_flag_load_from_db}",  // TODO: Load from DB (hashed)
                Hint                 = "Example hint text loaded from database.",
                ExistingAttachmentName = ch.HasAttachment ? $"challenge_{id}_files.zip" : null,
            };
            ViewData["Title"]      = $"Edit — {ch.Title}";
            ViewData["Breadcrumb"] = "Admin · Edit Challenge";
            return View("Challenges/Form", vm);
        }

        // ─── POST /Admin/Challenges/Edit/{id} ─────────────────
        [HttpPost]
        [Route("Admin/Challenges/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult ChallengeEdit(int id, ChallengeFormViewModel model)
        {
            model.Id = id;
            ViewData["Title"]      = "Edit Challenge";
            ViewData["Breadcrumb"] = "Admin · Edit Challenge";

            if (!ModelState.IsValid)
                return View("Challenges/Form", model);

            // TODO: Connect EF Core
            // var challenge = await _db.Challenges.FindAsync(id);
            // if (challenge == null) return NotFound();
            // challenge.Title       = model.Title;
            // challenge.Description = model.Description;
            // challenge.Category    = model.Category;
            // challenge.Difficulty  = model.Difficulty;
            // challenge.Points      = model.Points;
            // challenge.Status      = model.Status;
            // if (!string.IsNullOrEmpty(model.Flag))
            //     challenge.Flag = model.Flag;  // TODO: hash if changed
            // challenge.Hint        = model.Hint;
            // challenge.HintPenalty = model.HintPenalty;
            // challenge.UpdatedAt   = DateTime.UtcNow;
            // if (model.AttachmentFile != null) { /* handle new file */ }
            // await _db.SaveChangesAsync();

            TempData["AdminSuccess"] = $"Challenge \"{model.Title}\" updated successfully.";
            return RedirectToAction(nameof(Challenges));
        }

        // ─── POST /Admin/Challenges/Delete/{id} ───────────────
        [HttpPost]
        [Route("Admin/Challenges/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult ChallengeDelete(int id)
        {
            // TODO: Connect EF Core
            // var challenge = await _db.Challenges.FindAsync(id);
            // if (challenge == null) return NotFound();
            // _db.Challenges.Remove(challenge);
            // await _db.SaveChangesAsync();

            TempData["AdminSuccess"] = $"Challenge #{id} has been deleted.";
            return RedirectToAction(nameof(Challenges));
        }

        // ══════════════════════════════════════════════════════
        //  ADMIN DASHBOARD  —  /Admin
        // ══════════════════════════════════════════════════════

        [HttpGet]
        [Route("Admin")]
        public IActionResult Index()
        {
            ViewData["Title"]      = "Admin Dashboard";
            ViewData["Breadcrumb"] = "Admin · Dashboard";

            // TODO: Replace with EF Core aggregates
            var vm = new AdminDashboardViewModel
            {
                TotalUsers         = 312,
                TotalChallenges    = 24,
                ActiveCompetitions = 1,
                TotalSubmissions   = 4872,
                NewUsersToday      = 18,
                SubmissionsToday   = 143,
                RecentSubmissions  =
                [
                    new() { Username="CipherMaster", ChallengeName="SQL Injection Basics",       Category="Web Security",       Points=100, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-3)  },
                    new() { Username="n3tR4nger",    ChallengeName="RSA Weak Key",               Category="Cryptography",       Points=300, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-7)  },
                    new() { Username="0x_exploit",   ChallengeName="CSRF Token Bypass",          Category="Web Security",       Points=250, IsCorrect=false, SubmittedAt=DateTime.UtcNow.AddMinutes(-12) },
                    new() { Username="h4x0r_pro",    ChallengeName="Wireshark Hunt",             Category="Digital Forensics",  Points=150, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-18) },
                    new() { Username="shell_ghost",  ChallengeName="Anti-Debug Bypass",          Category="Reverse Engineering", Points=350, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-25) },
                    new() { Username="xor_queen",    ChallengeName="Buffer Overflow 101",        Category="Binary Exploitation", Points=250, IsCorrect=false, SubmittedAt=DateTime.UtcNow.AddMinutes(-31) },
                    new() { Username="pwn_wizard",   ChallengeName="The Mysterious Developer",   Category="OSINT",              Points=100, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-40) },
                    new() { Username="hex_ninja",    ChallengeName="Hidden in Plain Sight",      Category="Steganography",      Points=100, IsCorrect=true,  SubmittedAt=DateTime.UtcNow.AddMinutes(-52) },
                ],
                UserActivity =
                [
                    new() { Username="h4x0r_pro",    AvatarInitials="AC", Action="solved",      Detail="SQL Injection Basics",  Timestamp=DateTime.UtcNow.AddMinutes(-18) },
                    new() { Username="vuln_hunter",  AvatarInitials="VH", Action="joined_comp", Detail="CyberArena Open 2026",  Timestamp=DateTime.UtcNow.AddMinutes(-35) },
                    new() { Username="sqli_master",  AvatarInitials="SM", Action="registered",  Detail="New account created",   Timestamp=DateTime.UtcNow.AddHours(-1)   },
                    new() { Username="xss_panda",    AvatarInitials="XP", Action="joined_team", Detail="NullByte",              Timestamp=DateTime.UtcNow.AddHours(-2)   },
                    new() { Username="fuzzy_logic",  AvatarInitials="FL", Action="solved",      Detail="Caesar's Secret",       Timestamp=DateTime.UtcNow.AddHours(-3)   },
                    new() { Username="mem_leak",     AvatarInitials="ML", Action="registered",  Detail="New account created",   Timestamp=DateTime.UtcNow.AddHours(-5)   },
                ],
            };

            // Chart data passed via ViewBag — arrays serialised to JSON in the view
            ViewBag.SubLabels   = new[] { "Sep 12","Sep 13","Sep 14","Sep 15","Sep 16","Sep 17","Sep 18" };
            ViewBag.SubCorrect  = new[] { 98, 124, 87, 201, 176, 143, 165 };
            ViewBag.SubWrong    = new[] { 34, 52, 31, 78, 65, 54, 61 };
            ViewBag.CatLabels   = ChallengeCategories.All;
            ViewBag.CatCounts   = new[] { 5, 4, 4, 3, 2, 2, 2, 2 };

            return View(vm);
        }

        // ══════════════════════════════════════════════════════
        //  REPORTS  —  /Admin/Reports
        // ══════════════════════════════════════════════════════

        [HttpGet]
        [Route("Admin/Reports")]
        public IActionResult Reports()
        {
            ViewData["Title"]      = "Reports";
            ViewData["Breadcrumb"] = "Admin · Reports";

            var allChallenges = GetMockAdminChallenges();

            var vm = new ReportViewModel
            {
                TotalSubmissions   = 4872,
                CorrectSubmissions = 3109,
                UniqueUsers        = 287,
                AvgSolveTime       = 2.4,
                MostSolved  = allChallenges
                    .OrderByDescending(c => c.SolverCount)
                    .Take(8)
                    .Select(c => new ChallengeSolveStatItem
                    {
                        Id=c.Id, Title=c.Title, Category=c.Category,
                        Points=c.Points, SolverCount=c.SolverCount,
                        SolveRate=Math.Round((double)c.SolverCount / 312 * 100, 1),
                        Difficulty=c.Difficulty
                    }).ToList(),
                LeastSolved = allChallenges
                    .Where(c => c.Status == ChallengeStatus.Active)
                    .OrderBy(c => c.SolverCount)
                    .Take(8)
                    .Select(c => new ChallengeSolveStatItem
                    {
                        Id=c.Id, Title=c.Title, Category=c.Category,
                        Points=c.Points, SolverCount=c.SolverCount,
                        SolveRate=Math.Round((double)c.SolverCount / 312 * 100, 1),
                        Difficulty=c.Difficulty
                    }).ToList(),
                TeamPerformance =
                [
                    new() { Rank=1, Name="ByteForce",   Score=32400, Solved=58, Members=4 },
                    new() { Rank=2, Name="CodeStrike",  Score=28900, Solved=52, Members=3 },
                    new() { Rank=3, Name="NullByte",    Score=25600, Solved=47, Members=4 },
                    new() { Rank=4, Name="PhantomByte", Score=21300, Solved=41, Members=3 },
                    new() { Rank=5, Name="ShadowStack", Score=17800, Solved=34, Members=2 },
                    new() { Rank=6, Name="ZeroDay",     Score=14200, Solved=28, Members=4 },
                    new() { Rank=7, Name="PwnStars",    Score=11500, Solved=22, Members=2 },
                    new() { Rank=8, Name="CryptoKings", Score=9100,  Solved=17, Members=3 },
                ]
            };

            // Chart data
            ViewBag.SubLabels      = new[] { "Sep 12","Sep 13","Sep 14","Sep 15","Sep 16","Sep 17","Sep 18" };
            ViewBag.SubData        = new[] { 132, 176, 118, 279, 241, 197, 226 };
            ViewBag.CatLabels      = ChallengeCategories.All;
            ViewBag.CatSolves      = new[] { 426, 343, 278, 312, 127, 226, 193, 81 };
            ViewBag.TeamLabels     = new[] { "ByteForce","CodeStrike","NullByte","PhantomByte","ShadowStack","ZeroDay" };
            ViewBag.TeamScores     = new[] { 32400, 28900, 25600, 21300, 17800, 14200 };
            ViewBag.UserLabels     = new[] { "Sep 12","Sep 13","Sep 14","Sep 15","Sep 16","Sep 17","Sep 18" };
            ViewBag.UserActive     = new[] { 87, 112, 76, 134, 128, 109, 121 };

            return View(vm);
        }

        // ══════════════════════════════════════════════════════
        //  ADMIN ANNOUNCEMENTS  —  /Admin/Announcements
        // ══════════════════════════════════════════════════════

        [HttpGet]
        [Route("Admin/Announcements")]
        public IActionResult Announcements()
        {
            ViewData["Title"]      = "Manage Announcements";
            ViewData["Breadcrumb"] = "Admin · Announcements";

            var vm = new AnnouncementListViewModel
            {
                Announcements = AnnouncementsController.GetMockAnnouncements()
            };
            return View("Announcements/Index", vm);
        }

        [HttpGet]
        [Route("Admin/Announcements/Create")]
        public IActionResult AnnouncementCreate()
        {
            ViewData["Title"]      = "Create Announcement";
            ViewData["Breadcrumb"] = "Admin · Announcements · Create";
            return View("Announcements/Form", new AnnouncementFormViewModel());
        }

        [HttpPost]
        [Route("Admin/Announcements/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult AnnouncementCreate(AnnouncementFormViewModel model)
        {
            ViewData["Title"]      = "Create Announcement";
            ViewData["Breadcrumb"] = "Admin · Announcements · Create";

            if (!ModelState.IsValid)
                return View("Announcements/Form", model);

            // TODO: EF Core — _db.Announcements.Add(...); await _db.SaveChangesAsync();
            TempData["AdminSuccess"] = $"Announcement \"{model.Title}\" published successfully.";
            return RedirectToAction(nameof(Announcements));
        }

        [HttpGet]
        [Route("Admin/Announcements/Edit/{id}")]
        public IActionResult AnnouncementEdit(int id)
        {
            var ann = AnnouncementsController.GetMockAnnouncements().FirstOrDefault(a => a.Id == id);
            if (ann == null) return NotFound();

            var vm = new AnnouncementFormViewModel
            {
                Id       = ann.Id,
                Title    = ann.Title,
                Body     = ann.Body,
                IsPinned = ann.IsPinned,
            };
            ViewData["Title"]      = $"Edit — {ann.Title}";
            ViewData["Breadcrumb"] = "Admin · Announcements · Edit";
            return View("Announcements/Form", vm);
        }

        [HttpPost]
        [Route("Admin/Announcements/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult AnnouncementEdit(int id, AnnouncementFormViewModel model)
        {
            model.Id = id;
            ViewData["Title"]      = "Edit Announcement";
            ViewData["Breadcrumb"] = "Admin · Announcements · Edit";

            if (!ModelState.IsValid)
                return View("Announcements/Form", model);

            // TODO: EF Core — find, update, save
            TempData["AdminSuccess"] = $"Announcement \"{model.Title}\" updated successfully.";
            return RedirectToAction(nameof(Announcements));
        }

        [HttpPost]
        [Route("Admin/Announcements/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult AnnouncementDelete(int id)
        {
            // TODO: EF Core — find, remove, save
            TempData["AdminSuccess"] = $"Announcement #{id} has been deleted.";
            return RedirectToAction(nameof(Announcements));
        }
    }
}
