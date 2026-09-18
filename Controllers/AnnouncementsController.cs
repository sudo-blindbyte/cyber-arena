using Microsoft.AspNetCore.Mvc;
using cyber_arena.ViewModels;

namespace cyber_arena.Controllers
{
    /// <summary>
    /// Public Announcements — list and detail views for participants.
    /// TODO: Replace mock data with EF Core once Identity is connected.
    /// </summary>
    public class AnnouncementsController : Controller
    {
        // ─── Mock Data ────────────────────────────────────────
        internal static List<AnnouncementViewModel> GetMockAnnouncements() =>
        [
            new()
            {
                Id=1, Title="Welcome to CyberArena Open 2026!",
                Body="We're thrilled to announce the official launch of CyberArena Open 2026. This year's competition features 24 challenges across 8 categories. Whether you're a seasoned hacker or just starting your journey, there's something for everyone. Good luck, hack responsibly, and may the best hacker win!\n\nThe competition runs from September 16th to September 21st, 2026 (UTC). All flags must be submitted before the deadline.",
                IsPinned=true, PublishedAt=DateTime.UtcNow.AddHours(-6), AuthorName="CyberArena Admin"
            },
            new()
            {
                Id=2, Title="Hint System Now Available",
                Body="By popular demand, we've enabled the optional hint system for all challenges. Each hint comes with a point penalty, so use them wisely. You can reveal hints from the challenge detail page. Hints do not expire and remain visible for the rest of the competition.",
                IsPinned=true, PublishedAt=DateTime.UtcNow.AddHours(-24), AuthorName="CyberArena Admin"
            },
            new()
            {
                Id=3, Title="Server Maintenance Window — 02:00–04:00 UTC",
                Body="We will be performing routine infrastructure maintenance on September 19th from 02:00 to 04:00 UTC. The platform will be in read-only mode during this window. You will not be able to submit flags. We apologise for any inconvenience.",
                IsPinned=false, PublishedAt=DateTime.UtcNow.AddDays(-1), AuthorName="CyberArena Admin"
            },
            new()
            {
                Id=4, Title="New Challenge Added: Kernel Exploit 0-day",
                Body="A brand new 1000-point challenge has been added to the Binary Exploitation category: \"Kernel Exploit 0-day\". This is our hardest challenge this season — only the most experienced pwn experts will conquer it. The first three solvers will receive a special badge on their profile.",
                IsPinned=false, PublishedAt=DateTime.UtcNow.AddDays(-2), AuthorName="CyberArena Admin"
            },
            new()
            {
                Id=5, Title="Red Team Rumble Registration Open",
                Body="Registration for the upcoming Red Team Rumble competition is now open. This advanced-level team competition starts in 5 days and is limited to 200 participants. Spots are filling up fast — register your team now from the Competitions page.",
                IsPinned=false, PublishedAt=DateTime.UtcNow.AddDays(-3), AuthorName="CyberArena Admin"
            },
            new()
            {
                Id=6, Title="Scoring System Update",
                Body="We have updated the dynamic scoring system. Challenge point values now adjust based on the number of solvers. The more people solve a challenge, the fewer points future solvers will earn. This rewards speed and encourages participants to tackle challenges early.",
                IsPinned=false, PublishedAt=DateTime.UtcNow.AddDays(-5), AuthorName="CyberArena Admin"
            },
        ];

        // ─── GET /Announcements ───────────────────────────────
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"]      = "Announcements";
            ViewData["Breadcrumb"] = "Announcements";

            var vm = new AnnouncementListViewModel
            {
                Announcements = GetMockAnnouncements()
            };

            return View(vm);
        }

        // ─── GET /Announcements/Details/{id} ──────────────────
        [HttpGet]
        public IActionResult Details(int id)
        {
            // TODO: EF Core — fetch by id
            var ann = GetMockAnnouncements().FirstOrDefault(a => a.Id == id);
            if (ann == null) return NotFound();

            ViewData["Title"]      = ann.Title;
            ViewData["Breadcrumb"] = "Announcements · Details";
            return View(ann);
        }
    }
}
