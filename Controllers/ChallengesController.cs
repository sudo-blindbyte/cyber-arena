using Microsoft.AspNetCore.Mvc;
using cyber_arena.ViewModels;

namespace cyber_arena.Controllers
{
    public class ChallengesController : Controller
    {
        // ─── Mock Data ────────────────────────────────────────
        // TODO: Replace with EF Core DbContext injection and database queries
        private static List<ChallengeListItemViewModel> GetMockChallenges() =>
        [
            // ── Web Security ──────────────────────────────────
            new() { Id=1,  Title="SQL Injection Basics",         Category=ChallengeCategories.Web,        Difficulty="Easy",   Points=100,  IsSolved=true,  SolverCount=184, ShortDescription="Find the hidden flag by exploiting a classic SQL injection vulnerability in the login form." },
            new() { Id=2,  Title="XSS Reflected Attack",         Category=ChallengeCategories.Web,        Difficulty="Easy",   Points=150,  IsSolved=true,  SolverCount=142, ShortDescription="Craft a reflected XSS payload to steal the admin cookie from the vulnerable search endpoint." },
            new() { Id=3,  Title="CSRF Token Bypass",            Category=ChallengeCategories.Web,        Difficulty="Medium", Points=250,  IsSolved=false, SolverCount=67,  ShortDescription="Bypass the CSRF protection mechanism and perform an unauthorized state-changing action." },
            new() { Id=4,  Title="JWT Secret Cracking",          Category=ChallengeCategories.Web,        Difficulty="Medium", Points=300,  IsSolved=false, SolverCount=53,  ShortDescription="The application uses a weak HS256 JWT secret. Crack it and forge an admin token.", HasAttachment=true },
            new() { Id=5,  Title="GraphQL Introspection Leak",   Category=ChallengeCategories.Web,        Difficulty="Hard",   Points=500,  IsSolved=false, SolverCount=21,  ShortDescription="The GraphQL endpoint has misconfigured introspection. Enumerate the schema and retrieve the hidden flag." },

            // ── Cryptography ──────────────────────────────────
            new() { Id=6,  Title="Caesar's Secret",              Category=ChallengeCategories.Crypto,     Difficulty="Easy",   Points=75,   IsSolved=true,  SolverCount=213, ShortDescription="A simple Caesar cipher stands between you and the flag. Brute force or calculate the shift." },
            new() { Id=7,  Title="RSA Weak Key",                 Category=ChallengeCategories.Crypto,     Difficulty="Medium", Points=300,  IsSolved=true,  SolverCount=58,  ShortDescription="The RSA public key has a small prime factor. Factor the modulus and decrypt the message.", HasAttachment=true },
            new() { Id=8,  Title="AES ECB Penguin",              Category=ChallengeCategories.Crypto,     Difficulty="Medium", Points=250,  IsSolved=false, SolverCount=72,  ShortDescription="AES in ECB mode reveals patterns. Exploit the deterministic block cipher to recover the plaintext." },
            new() { Id=9,  Title="Elliptic Curve Discrete Log",  Category=ChallengeCategories.Crypto,     Difficulty="Hard",   Points=600,  IsSolved=false, SolverCount=12,  ShortDescription="Solve the ECDLP on a small non-secure curve using Pohlig-Hellman or baby-step giant-step." },

            // ── Reverse Engineering ────────────────────────────
            new() { Id=10, Title="Crackme Level 1",              Category=ChallengeCategories.Reversing,  Difficulty="Easy",   Points=100,  IsSolved=false, SolverCount=134, ShortDescription="A simple license key checker binary. Analyze the control flow and find the valid key.", HasAttachment=true },
            new() { Id=11, Title="Anti-Debug Bypass",            Category=ChallengeCategories.Reversing,  Difficulty="Medium", Points=350,  IsSolved=false, SolverCount=41,  ShortDescription="The binary detects debuggers and exits. Patch the anti-debug checks and extract the flag.", HasAttachment=true },
            new() { Id=12, Title="Obfuscated Python",            Category=ChallengeCategories.Reversing,  Difficulty="Medium", Points=275,  IsSolved=true,  SolverCount=63,  ShortDescription="A heavily obfuscated Python script hides the flag validation logic. Deobfuscate and extract it.", HasAttachment=true },
            new() { Id=13, Title="LLVM Bitcode Maze",            Category=ChallengeCategories.Reversing,  Difficulty="Hard",   Points=550,  IsSolved=false, SolverCount=9,   ShortDescription="The flag checker is compiled to LLVM bitcode. Lift it to readable IR and solve the constraints." },

            // ── Digital Forensics ──────────────────────────────
            new() { Id=14, Title="Wireshark Hunt",               Category=ChallengeCategories.Forensics,  Difficulty="Easy",   Points=150,  IsSolved=true,  SolverCount=165, ShortDescription="A suspicious .pcap file contains a flag exfiltrated over DNS. Find it.", HasAttachment=true },
            new() { Id=15, Title="Memory Dump Analysis",         Category=ChallengeCategories.Forensics,  Difficulty="Medium", Points=325,  IsSolved=false, SolverCount=47,  ShortDescription="Analyze the Windows memory dump with Volatility to recover the attacker's credentials.", HasAttachment=true },
            new() { Id=16, Title="Deleted File Recovery",        Category=ChallengeCategories.Forensics,  Difficulty="Medium", Points=200,  IsSolved=false, SolverCount=81,  ShortDescription="A disk image contains a recently deleted file with the flag. Recover it using file carving.", HasAttachment=true },

            // ── Networking ────────────────────────────────────
            new() { Id=17, Title="TCP Handshake Hijack",         Category=ChallengeCategories.Networking, Difficulty="Easy",   Points=125,  IsSolved=false, SolverCount=109, ShortDescription="Intercept the TCP stream and identify the flag hidden in the application-layer payload." },
            new() { Id=18, Title="BGP Route Poisoning",          Category=ChallengeCategories.Networking, Difficulty="Hard",   Points=500,  IsSolved=false, SolverCount=18,  ShortDescription="A simulated BGP environment has a misconfigured router. Exploit it and capture the flag." },

            // ── Steganography ─────────────────────────────────
            new() { Id=19, Title="Hidden in Plain Sight",        Category=ChallengeCategories.Stego,      Difficulty="Easy",   Points=100,  IsSolved=false, SolverCount=152, ShortDescription="A JPEG image hides a flag using LSB steganography. Extract it with steghide or zsteg.", HasAttachment=true },
            new() { Id=20, Title="Audio Spectrogram Secret",     Category=ChallengeCategories.Stego,      Difficulty="Medium", Points=225,  IsSolved=false, SolverCount=74,  ShortDescription="Load the WAV file in Audacity or SoX and examine the spectrogram for hidden text.", HasAttachment=true },

            // ── OSINT ─────────────────────────────────────────
            new() { Id=21, Title="The Mysterious Developer",     Category=ChallengeCategories.Osint,      Difficulty="Easy",   Points=100,  IsSolved=false, SolverCount=193, ShortDescription="A developer accidentally committed secrets to a public GitHub repo. Find the flag in the history." },
            new() { Id=22, Title="GeoGuessr Intelligence",       Category=ChallengeCategories.Osint,      Difficulty="Medium", Points=200,  IsSolved=false, SolverCount=88,  ShortDescription="Given only a blurry street-level image, identify the exact city and street to retrieve the flag." },

            // ── Binary Exploitation ───────────────────────────
            new() { Id=23, Title="Buffer Overflow 101",          Category=ChallengeCategories.Pwn,        Difficulty="Medium", Points=250,  IsSolved=true,  SolverCount=76,  ShortDescription="Overflow the stack buffer to overwrite the return address and jump to the win() function.", HasAttachment=true },
            new() { Id=24, Title="Kernel Exploit 0-day",         Category=ChallengeCategories.Pwn,        Difficulty="Hard",   Points=1000, IsSolved=false, SolverCount=5,   ShortDescription="Exploit a use-after-free in the simulated kernel module to escalate privileges and read /flag.", HasAttachment=true },
        ];

        private static ChallengeDetailViewModel? GetMockDetail(int id)
        {
            var all = GetMockChallenges();
            var item = all.FirstOrDefault(c => c.Id == id);
            if (item == null) return null;

            var hints = new List<ChallengeHint>();
            if (id % 3 == 0 || id % 5 == 0)
                hints.Add(new() { Number = 1, Text = "Try using common tools like Burp Suite, Wireshark, or strings utility on the binary.", PointPenalty = 25 });
            if (id % 2 == 0)
                hints.Add(new() { Number = 2, Text = "The flag format is CTF{...}. Pay close attention to the error messages returned by the server.", PointPenalty = 50 });

            var attachments = new List<ChallengeAttachment>();
            if (item.HasAttachment)
                attachments.Add(new()
                {
                    FileName   = $"challenge_{id}_files.zip",
                    FileSize   = $"{new Random(id).Next(1, 15)}.{new Random(id*2).Next(1,9)} MB",
                    DownloadUrl = "#",
                    FileIcon   = "fa-file-zipper"
                });

            var allIds = all.Select(c => c.Id).OrderBy(x => x).ToList();
            int idx    = allIds.IndexOf(id);
            int? prev  = idx > 0               ? allIds[idx - 1] : null;
            int? next  = idx < allIds.Count - 1 ? allIds[idx + 1] : null;

            return new ChallengeDetailViewModel
            {
                Id           = item.Id,
                Title        = item.Title,
                Category     = item.Category,
                Difficulty   = item.Difficulty,
                Points       = item.Points,
                Description  = item.ShortDescription + "\n\n" +
                               "Connect to the challenge environment using the provided credentials. " +
                               "The flag is in the standard format: CTF{...}\n\n" +
                               "Good luck, and remember — enumerate everything!",
                IsSolved         = item.IsSolved,
                SolverCount      = item.SolverCount,
                TotalParticipants = 214,
                SolvedAt     = item.IsSolved ? DateTime.UtcNow.AddHours(-new Random(id).Next(1, 72)) : null,
                SolvedRank   = item.IsSolved ? new Random(id).Next(1, 30) : null,
                Hints        = hints,
                Attachments  = attachments,
                PrevChallengeId = prev,
                NextChallengeId = next,
            };
        }

        // ─── GET /Challenges ──────────────────────────────────
        public IActionResult Index(
            string? category   = null,
            string? difficulty = null,
            string? q          = null,
            string  sort       = "points-asc")
        {
            ViewData["Title"]      = "Challenges";
            ViewData["Breadcrumb"] = "Challenges";

            // TODO: Replace with EF Core:
            // var challenges = await _db.Challenges
            //     .Where(c => c.Status == "Active")
            //     .Select(c => new ChallengeListItemViewModel { ... })
            //     .ToListAsync();

            var challenges = GetMockChallenges();

            // Apply server-side filters (also done client-side via JS for responsiveness)
            if (!string.IsNullOrWhiteSpace(category) && category != "All")
                challenges = challenges.Where(c => c.Category == category).ToList();
            if (!string.IsNullOrWhiteSpace(difficulty) && difficulty != "All")
                challenges = challenges.Where(c => c.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!string.IsNullOrWhiteSpace(q))
                challenges = challenges.Where(c =>
                    c.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                    (c.ShortDescription?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    c.Category.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();

            challenges = sort switch
            {
                "points-desc"   => challenges.OrderByDescending(c => c.Points).ToList(),
                "points-asc"    => challenges.OrderBy(c => c.Points).ToList(),
                "solvers-desc"  => challenges.OrderByDescending(c => c.SolverCount).ToList(),
                "title-asc"     => challenges.OrderBy(c => c.Title).ToList(),
                _               => challenges.OrderBy(c => c.Points).ToList()
            };

            // Build category counts from ALL challenges (before filter)
            var all = GetMockChallenges();
            var catCounts = ChallengeCategories.All
                .ToDictionary(cat => cat, cat => all.Count(c => c.Category == cat));

            var vm = new ChallengeListViewModel
            {
                Challenges       = challenges,
                ActiveCategory   = category,
                ActiveDifficulty = difficulty,
                SearchQuery      = q,
                SortBy           = sort,
                CategoryCounts   = catCounts,
            };

            return View(vm);
        }

        // ─── GET /Challenges/Details/{id} ─────────────────────
        public IActionResult Details(int id)
        {
            // TODO: Replace with EF Core:
            // var challenge = await _db.Challenges
            //     .Include(c => c.Hints)
            //     .Include(c => c.Attachments)
            //     .FirstOrDefaultAsync(c => c.Id == id && c.Status == "Active");
            // if (challenge == null) return NotFound();

            var vm = GetMockDetail(id);
            if (vm == null) return NotFound();

            ViewData["Title"]      = vm.Title;
            ViewData["Breadcrumb"] = vm.Title;
            return View(vm);
        }

        // ─── POST /Challenges/Submit/{id} ─────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(int id, FlagSubmissionViewModel model)
        {
            // TODO: Connect ASP.NET Core Identity + EF Core
            // var userId = _userManager.GetUserId(User);
            // var challenge = await _db.Challenges.FindAsync(id);
            // if (challenge == null) return NotFound();
            //
            // // Check if already solved
            // var existing = await _db.Submissions
            //     .FirstOrDefaultAsync(s => s.ChallengeId == id && s.UserId == userId && s.IsCorrect);
            // if (existing != null) { ... AlreadySolved ... }
            //
            // // Verify flag (constant-time comparison)
            // bool correct = CryptographicOperations.FixedTimeEquals(
            //     Encoding.UTF8.GetBytes(model.Flag.Trim()),
            //     Encoding.UTF8.GetBytes(challenge.Flag));
            //
            // // Record submission
            // await _db.Submissions.AddAsync(new Submission { ... });
            // if (correct) { /* award points, update score */ }
            // await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                var detailVm = GetMockDetail(id);
                if (detailVm == null) return NotFound();
                detailVm.SubmissionResult = FlagSubmissionResult.Incorrect;
                ViewData["Title"] = detailVm.Title;
                ViewData["Breadcrumb"] = detailVm.Title;
                return View("Details", detailVm);
            }

            // Demo: simulate incorrect (real verification requires database)
            TempData["SubmitResult"] = "incorrect";
            TempData["SubmitMsg"]   = "Incorrect flag. Keep trying!";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
