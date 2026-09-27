using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberArenaWebForms.Models
{
    // ─── Category Constants ───────────────────────────────────
    public static class ChallengeCategories
    {
        public const string Web        = "Web Security";
        public const string Crypto     = "Cryptography";
        public const string Reversing  = "Reverse Engineering";
        public const string Forensics  = "Digital Forensics";
        public const string Networking = "Networking";
        public const string Stego      = "Steganography";
        public const string Osint      = "OSINT";
        public const string Pwn        = "Binary Exploitation";

        public static readonly string[] All = new[]
        {
            Web, Crypto, Reversing, Forensics, Networking, Stego, Osint, Pwn
        };

        public static string Icon(string category)
        {
            switch (category)
            {
                case Web:        return "fa-globe";
                case Crypto:     return "fa-key";
                case Reversing:  return "fa-code";
                case Forensics:  return "fa-magnifying-glass";
                case Networking: return "fa-network-wired";
                case Stego:      return "fa-image";
                case Osint:      return "fa-satellite-dish";
                case Pwn:        return "fa-terminal";
                default:         return "fa-flag";
            }
        }

        public static string CssClass(string category)
        {
            switch (category)
            {
                case Web:        return "ch-cat-web";
                case Crypto:     return "ch-cat-crypto";
                case Reversing:  return "ch-cat-rev";
                case Forensics:  return "ch-cat-forensics";
                case Networking: return "ch-cat-network";
                case Stego:      return "ch-cat-stego";
                case Osint:      return "ch-cat-osint";
                case Pwn:        return "ch-cat-pwn";
                default:         return "ch-cat-web";
            }
        }
    }

    // ─── Difficulty Constants ─────────────────────────────────
    public static class ChallengeDifficulty
    {
        public const string Easy   = "Easy";
        public const string Medium = "Medium";
        public const string Hard   = "Hard";
        public static readonly string[] All = new[] { Easy, Medium, Hard };
    }

    // ─── Attachment ───────────────────────────────────────────
    public class ChallengeAttachment
    {
        public string FileName    { get; set; }
        public string FileSize    { get; set; }
        public string DownloadUrl { get; set; } = "#";
        public string FileIcon    { get; set; } = "fa-file";
    }

    // ─── Hint ─────────────────────────────────────────────────
    public class ChallengeHint
    {
        public int    Number       { get; set; }
        public string Text         { get; set; }
        public int    PointPenalty { get; set; }
    }

    // ─── List Item (card / table row) ────────────────────────
    public class ChallengeListItem
    {
        public int    Id               { get; set; }
        public string Title            { get; set; }
        public string Category         { get; set; }
        public string Difficulty       { get; set; } = ChallengeDifficulty.Medium;
        public int    Points           { get; set; }
        public bool   IsSolved         { get; set; }
        public int    SolverCount      { get; set; }
        public string ShortDescription { get; set; }
        public bool   HasAttachment    { get; set; }

        public string CategoryIcon     { get { return ChallengeCategories.Icon(Category); } }
        public string CategoryCssClass { get { return ChallengeCategories.CssClass(Category); } }
        public string DifficultyLower  { get { return Difficulty.ToLower(); } }
    }

    // ─── List Page ViewModel ──────────────────────────────────
    public class ChallengeListViewModel
    {
        public List<ChallengeListItem>    Challenges       { get; set; } = new List<ChallengeListItem>();
        public string                     ActiveCategory   { get; set; }
        public string                     ActiveDifficulty { get; set; }
        public string                     SearchQuery      { get; set; }
        public string                     SortBy           { get; set; } = "points-asc";
        public Dictionary<string, int>    CategoryCounts   { get; set; } = new Dictionary<string, int>();

        public int TotalCount   { get { return Challenges.Count; } }
        public int SolvedCount  { get { return Challenges.Count(c => c.IsSolved); } }
        public int TotalPoints  { get { return Challenges.Sum(c => c.Points); } }
        public int EarnedPoints { get { return Challenges.Where(c => c.IsSolved).Sum(c => c.Points); } }
        public int ProgressPct  { get { return TotalCount > 0 ? (int)((double)SolvedCount / TotalCount * 100) : 0; } }
    }

    // ─── Detail ViewModel ─────────────────────────────────────
    public class ChallengeDetailViewModel
    {
        public int    Id               { get; set; }
        public string Title            { get; set; }
        public string Category         { get; set; }
        public string Difficulty       { get; set; } = ChallengeDifficulty.Medium;
        public int    Points           { get; set; }
        public string Description      { get; set; }
        public bool   IsSolved         { get; set; }
        public int    SolverCount      { get; set; }
        public int    TotalParticipants { get; set; }
        public DateTime? SolvedAt      { get; set; }
        public int?   SolvedRank       { get; set; }

        public List<ChallengeHint>       Hints       { get; set; } = new List<ChallengeHint>();
        public List<ChallengeAttachment> Attachments { get; set; } = new List<ChallengeAttachment>();

        public int?  PrevChallengeId   { get; set; }
        public int?  NextChallengeId   { get; set; }

        // POST result
        public FlagSubmissionResult? SubmissionResult { get; set; }
        public string SubmissionMessage               { get; set; }

        public string CategoryIcon     { get { return ChallengeCategories.Icon(Category); } }
        public string CategoryCssClass { get { return ChallengeCategories.CssClass(Category); } }
        public string DifficultyLower  { get { return Difficulty.ToLower(); } }
        public int    SolvePercent     { get { return TotalParticipants > 0 ? (int)((double)SolverCount / TotalParticipants * 100) : 0; } }

        public string DifficultyColor
        {
            get
            {
                switch (Difficulty)
                {
                    case "Easy": return "var(--ca-success)";
                    case "Hard": return "var(--ca-danger)";
                    default:     return "var(--ca-warning)";
                }
            }
        }
    }

    // ─── Submission Result Enum ───────────────────────────────
    public enum FlagSubmissionResult
    {
        Correct,
        Incorrect,
        AlreadySolved,
        RateLimited
    }

    // ─── Shared Mock Data ─────────────────────────────────────
    public static class ChallengeMockData
    {
        public static List<ChallengeListItem> GetAll()
        {
            return new List<ChallengeListItem>
            {
                // Web Security
                new ChallengeListItem { Id=1,  Title="SQL Injection Basics",       Category=ChallengeCategories.Web,        Difficulty="Easy",   Points=100,  IsSolved=true,  SolverCount=184, ShortDescription="Find the hidden flag by exploiting a classic SQL injection vulnerability in the login form." },
                new ChallengeListItem { Id=2,  Title="XSS Reflected Attack",       Category=ChallengeCategories.Web,        Difficulty="Easy",   Points=150,  IsSolved=true,  SolverCount=142, ShortDescription="Craft a reflected XSS payload to steal the admin cookie from the vulnerable search endpoint." },
                new ChallengeListItem { Id=3,  Title="CSRF Token Bypass",          Category=ChallengeCategories.Web,        Difficulty="Medium", Points=250,  IsSolved=false, SolverCount=67,  ShortDescription="Bypass the CSRF protection mechanism and perform an unauthorized state-changing action." },
                new ChallengeListItem { Id=4,  Title="JWT Secret Cracking",        Category=ChallengeCategories.Web,        Difficulty="Medium", Points=300,  IsSolved=false, SolverCount=53,  ShortDescription="The application uses a weak HS256 JWT secret. Crack it and forge an admin token.", HasAttachment=true },
                new ChallengeListItem { Id=5,  Title="GraphQL Introspection Leak", Category=ChallengeCategories.Web,        Difficulty="Hard",   Points=500,  IsSolved=false, SolverCount=21,  ShortDescription="The GraphQL endpoint has misconfigured introspection. Enumerate the schema and retrieve the hidden flag." },
                // Cryptography
                new ChallengeListItem { Id=6,  Title="Caesar's Secret",            Category=ChallengeCategories.Crypto,     Difficulty="Easy",   Points=75,   IsSolved=true,  SolverCount=213, ShortDescription="A simple Caesar cipher stands between you and the flag. Brute force or calculate the shift." },
                new ChallengeListItem { Id=7,  Title="RSA Weak Key",               Category=ChallengeCategories.Crypto,     Difficulty="Medium", Points=300,  IsSolved=true,  SolverCount=58,  ShortDescription="The RSA public key has a small prime factor. Factor the modulus and decrypt the message.", HasAttachment=true },
                new ChallengeListItem { Id=8,  Title="AES ECB Penguin",            Category=ChallengeCategories.Crypto,     Difficulty="Medium", Points=250,  IsSolved=false, SolverCount=72,  ShortDescription="AES in ECB mode reveals patterns. Exploit the deterministic block cipher to recover the plaintext." },
                new ChallengeListItem { Id=9,  Title="Elliptic Curve Discrete Log",Category=ChallengeCategories.Crypto,     Difficulty="Hard",   Points=600,  IsSolved=false, SolverCount=12,  ShortDescription="Solve the ECDLP on a small non-secure curve using Pohlig-Hellman or baby-step giant-step." },
                // Reverse Engineering
                new ChallengeListItem { Id=10, Title="Crackme Level 1",            Category=ChallengeCategories.Reversing,  Difficulty="Easy",   Points=100,  IsSolved=false, SolverCount=134, ShortDescription="A simple license key checker binary. Analyze the control flow and find the valid key.", HasAttachment=true },
                new ChallengeListItem { Id=11, Title="Anti-Debug Bypass",          Category=ChallengeCategories.Reversing,  Difficulty="Medium", Points=350,  IsSolved=false, SolverCount=41,  ShortDescription="The binary detects debuggers and exits. Patch the anti-debug checks and extract the flag.", HasAttachment=true },
                new ChallengeListItem { Id=12, Title="Obfuscated Python",          Category=ChallengeCategories.Reversing,  Difficulty="Medium", Points=275,  IsSolved=true,  SolverCount=63,  ShortDescription="A heavily obfuscated Python script hides the flag validation logic. Deobfuscate and extract it.", HasAttachment=true },
                new ChallengeListItem { Id=13, Title="LLVM Bitcode Maze",          Category=ChallengeCategories.Reversing,  Difficulty="Hard",   Points=550,  IsSolved=false, SolverCount=9,   ShortDescription="The flag checker is compiled to LLVM bitcode. Lift it to readable IR and solve the constraints." },
                // Forensics
                new ChallengeListItem { Id=14, Title="Wireshark Hunt",             Category=ChallengeCategories.Forensics,  Difficulty="Easy",   Points=150,  IsSolved=true,  SolverCount=165, ShortDescription="A suspicious .pcap file contains a flag exfiltrated over DNS. Find it.", HasAttachment=true },
                new ChallengeListItem { Id=15, Title="Memory Dump Analysis",       Category=ChallengeCategories.Forensics,  Difficulty="Medium", Points=325,  IsSolved=false, SolverCount=47,  ShortDescription="Analyze the Windows memory dump with Volatility to recover the attacker's credentials.", HasAttachment=true },
                new ChallengeListItem { Id=16, Title="Deleted File Recovery",      Category=ChallengeCategories.Forensics,  Difficulty="Medium", Points=200,  IsSolved=false, SolverCount=81,  ShortDescription="A disk image contains a recently deleted file with the flag. Recover it using file carving.", HasAttachment=true },
                // Networking
                new ChallengeListItem { Id=17, Title="TCP Handshake Hijack",       Category=ChallengeCategories.Networking, Difficulty="Easy",   Points=125,  IsSolved=false, SolverCount=109, ShortDescription="Intercept the TCP stream and identify the flag hidden in the application-layer payload." },
                new ChallengeListItem { Id=18, Title="BGP Route Poisoning",        Category=ChallengeCategories.Networking, Difficulty="Hard",   Points=500,  IsSolved=false, SolverCount=18,  ShortDescription="A simulated BGP environment has a misconfigured router. Exploit it and capture the flag." },
                // Steganography
                new ChallengeListItem { Id=19, Title="Hidden in Plain Sight",      Category=ChallengeCategories.Stego,      Difficulty="Easy",   Points=100,  IsSolved=false, SolverCount=152, ShortDescription="A JPEG image hides a flag using LSB steganography. Extract it with steghide or zsteg.", HasAttachment=true },
                new ChallengeListItem { Id=20, Title="Audio Spectrogram Secret",   Category=ChallengeCategories.Stego,      Difficulty="Medium", Points=225,  IsSolved=false, SolverCount=74,  ShortDescription="Load the WAV file in Audacity or SoX and examine the spectrogram for hidden text.", HasAttachment=true },
                // OSINT
                new ChallengeListItem { Id=21, Title="The Mysterious Developer",   Category=ChallengeCategories.Osint,      Difficulty="Easy",   Points=100,  IsSolved=false, SolverCount=193, ShortDescription="A developer accidentally committed secrets to a public GitHub repo. Find the flag in the history." },
                new ChallengeListItem { Id=22, Title="GeoGuessr Intelligence",     Category=ChallengeCategories.Osint,      Difficulty="Medium", Points=200,  IsSolved=false, SolverCount=88,  ShortDescription="Given only a blurry street-level image, identify the exact city and street to retrieve the flag." },
                // Pwn
                new ChallengeListItem { Id=23, Title="Buffer Overflow 101",        Category=ChallengeCategories.Pwn,        Difficulty="Medium", Points=250,  IsSolved=true,  SolverCount=76,  ShortDescription="Overflow the stack buffer to overwrite the return address and jump to the win() function.", HasAttachment=true },
                new ChallengeListItem { Id=24, Title="Kernel Exploit 0-day",       Category=ChallengeCategories.Pwn,        Difficulty="Hard",   Points=1000, IsSolved=false, SolverCount=5,   ShortDescription="Exploit a use-after-free in the simulated kernel module to escalate privileges and read /flag.", HasAttachment=true },
            };
        }

        public static ChallengeDetailViewModel GetDetail(int id)
        {
            var all  = GetAll();
            var item = all.FirstOrDefault(c => c.Id == id);
            if (item == null) return null;

            var hints = new List<ChallengeHint>();
            if (id % 3 == 0 || id % 5 == 0)
                hints.Add(new ChallengeHint { Number = 1, Text = "Try using common tools like Burp Suite, Wireshark, or strings utility on the binary.", PointPenalty = 25 });
            if (id % 2 == 0)
                hints.Add(new ChallengeHint { Number = 2, Text = "The flag format is CTF{...}. Pay close attention to the error messages returned by the server.", PointPenalty = 50 });

            var attachments = new List<ChallengeAttachment>();
            if (item.HasAttachment)
                attachments.Add(new ChallengeAttachment
                {
                    FileName    = string.Format("challenge_{0}_files.zip", id),
                    FileSize    = string.Format("{0}.{1} MB", new Random(id).Next(1, 15), new Random(id * 2).Next(1, 9)),
                    DownloadUrl = "#",
                    FileIcon    = "fa-file-zipper"
                });

            var allIds = all.Select(c => c.Id).OrderBy(x => x).ToList();
            int idx    = allIds.IndexOf(id);
            int? prev  = idx > 0               ? allIds[idx - 1] : (int?)null;
            int? next  = idx < allIds.Count - 1 ? allIds[idx + 1] : (int?)null;

            return new ChallengeDetailViewModel
            {
                Id                = item.Id,
                Title             = item.Title,
                Category          = item.Category,
                Difficulty        = item.Difficulty,
                Points            = item.Points,
                Description       = item.ShortDescription
                    + "\n\nConnect to the challenge environment using the provided credentials. "
                    + "The flag is in the standard format: CTF{...}\n\nGood luck, and remember — enumerate everything!",
                IsSolved          = item.IsSolved,
                SolverCount       = item.SolverCount,
                TotalParticipants = 214,
                SolvedAt          = item.IsSolved ? DateTime.UtcNow.AddHours(-new Random(id).Next(1, 72)) : (DateTime?)null,
                SolvedRank        = item.IsSolved ? new Random(id).Next(1, 30) : (int?)null,
                Hints             = hints,
                Attachments       = attachments,
                PrevChallengeId   = prev,
                NextChallengeId   = next,
            };
        }
    }
}
