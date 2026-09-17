using System.ComponentModel.DataAnnotations;

namespace cyber_arena.ViewModels
{
    // ─── Challenge Category Constants ─────────────────────────
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

        public static readonly string[] All =
        [
            Web, Crypto, Reversing, Forensics, Networking, Stego, Osint, Pwn
        ];

        // Returns Font Awesome icon class for a given category
        public static string Icon(string category) => category switch
        {
            Web        => "fa-globe",
            Crypto     => "fa-key",
            Reversing  => "fa-code",
            Forensics  => "fa-magnifying-glass",
            Networking => "fa-network-wired",
            Stego      => "fa-image",
            Osint      => "fa-satellite-dish",
            Pwn        => "fa-terminal",
            _          => "fa-flag"
        };

        // Returns CSS class for category icon color
        public static string CssClass(string category) => category switch
        {
            Web        => "ch-cat-web",
            Crypto     => "ch-cat-crypto",
            Reversing  => "ch-cat-rev",
            Forensics  => "ch-cat-forensics",
            Networking => "ch-cat-network",
            Stego      => "ch-cat-stego",
            Osint      => "ch-cat-osint",
            Pwn        => "ch-cat-pwn",
            _          => "ch-cat-web"
        };
    }

    // ─── Challenge Difficulty Constants ───────────────────────
    public static class ChallengeDifficulty
    {
        public const string Easy   = "Easy";
        public const string Medium = "Medium";
        public const string Hard   = "Hard";
        public static readonly string[] All = [Easy, Medium, Hard];
    }

    // ─── Challenge Status Constants (Admin) ───────────────────
    public static class ChallengeStatus
    {
        public const string Active   = "Active";
        public const string Draft    = "Draft";
        public const string Archived = "Archived";
        public static readonly string[] All = [Active, Draft, Archived];
    }

    // ─── Challenge Attachment Item ────────────────────────────
    public class ChallengeAttachment
    {
        public string FileName { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;    // e.g. "2.4 MB"
        public string DownloadUrl { get; set; } = "#";
        public string FileIcon { get; set; } = "fa-file";       // FA icon class
    }

    // ─── Challenge Hint ───────────────────────────────────────
    public class ChallengeHint
    {
        public int Number { get; set; }
        public string Text { get; set; } = string.Empty;
        public int PointPenalty { get; set; }    // penalty for revealing hint
    }

    // ─── Challenge List Item (card / table row) ───────────────
    public class ChallengeListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = ChallengeDifficulty.Medium;
        public int Points { get; set; }
        public bool IsSolved { get; set; }
        public int SolverCount { get; set; }
        public string? ShortDescription { get; set; }
        public bool HasAttachment { get; set; }

        // Derived helpers
        public string CategoryIcon    => ChallengeCategories.Icon(Category);
        public string CategoryCssClass => ChallengeCategories.CssClass(Category);
        public string DifficultyLower  => Difficulty.ToLower();
    }

    // ─── Challenge List Page ViewModel ────────────────────────
    public class ChallengeListViewModel
    {
        // All challenges (before client-side filtering)
        public List<ChallengeListItemViewModel> Challenges { get; set; } = new();

        // Current filter state (for rendering active pill)
        public string? ActiveCategory   { get; set; }
        public string? ActiveDifficulty { get; set; }
        public string? SearchQuery      { get; set; }
        public string  SortBy          { get; set; } = "points-desc";

        // Counts per category (for tabs)
        public Dictionary<string, int> CategoryCounts { get; set; } = new();

        // Stats
        public int TotalCount    => Challenges.Count;
        public int SolvedCount   => Challenges.Count(c => c.IsSolved);
        public int TotalPoints   => Challenges.Sum(c => c.Points);
        public int EarnedPoints  => Challenges.Where(c => c.IsSolved).Sum(c => c.Points);
    }

    // ─── Challenge Detail ViewModel ───────────────────────────
    public class ChallengeDetailViewModel
    {
        public int    Id          { get; set; }
        public string Title       { get; set; } = string.Empty;
        public string Category    { get; set; } = string.Empty;
        public string Difficulty  { get; set; } = ChallengeDifficulty.Medium;
        public int    Points      { get; set; }
        public string Description { get; set; } = string.Empty;

        public bool   IsSolved    { get; set; }
        public int    SolverCount { get; set; }
        public int    TotalParticipants { get; set; }
        public DateTime? SolvedAt { get; set; }
        public int?   SolvedRank  { get; set; }   // e.g. "5th to solve"

        public List<ChallengeHint>       Hints       { get; set; } = new();
        public List<ChallengeAttachment> Attachments { get; set; } = new();

        // Breadcrumb navigation
        public int? PrevChallengeId { get; set; }
        public int? NextChallengeId { get; set; }

        // Derived
        public string CategoryIcon     => ChallengeCategories.Icon(Category);
        public string CategoryCssClass => ChallengeCategories.CssClass(Category);
        public string DifficultyLower  => Difficulty.ToLower();
        public int    SolvePercent     => TotalParticipants > 0
            ? (int)((double)SolverCount / TotalParticipants * 100) : 0;

        // Flag submission result (set after POST)
        public FlagSubmissionResult? SubmissionResult { get; set; }
    }

    // ─── Flag Submission ──────────────────────────────────────
    public class FlagSubmissionViewModel
    {
        public int ChallengeId { get; set; }

        [Required(ErrorMessage = "Please enter a flag.")]
        [StringLength(500, ErrorMessage = "Flag is too long.")]
        [Display(Name = "Flag")]
        public string Flag { get; set; } = string.Empty;
    }

    public enum FlagSubmissionResult
    {
        Correct,
        Incorrect,
        AlreadySolved,
        RateLimited
    }

    // ─── Admin Challenge List Item ────────────────────────────
    public class AdminChallengeListItemViewModel
    {
        public int    Id          { get; set; }
        public string Title       { get; set; } = string.Empty;
        public string Category    { get; set; } = string.Empty;
        public string Difficulty  { get; set; } = ChallengeDifficulty.Medium;
        public int    Points      { get; set; }
        public string Status      { get; set; } = ChallengeStatus.Active;
        public int    SolverCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool   HasAttachment { get; set; }

        public string DifficultyLower => Difficulty.ToLower();
        public string StatusLower     => Status.ToLower();
    }

    // ─── Admin Challenge List ViewModel ───────────────────────
    public class AdminChallengeListViewModel
    {
        public List<AdminChallengeListItemViewModel> Challenges { get; set; } = new();
        public string? SearchQuery { get; set; }
        public string? FilterCategory { get; set; }
        public string? FilterStatus { get; set; }

        public int TotalCount    => Challenges.Count;
        public int ActiveCount   => Challenges.Count(c => c.Status == ChallengeStatus.Active);
        public int DraftCount    => Challenges.Count(c => c.Status == ChallengeStatus.Draft);
        public int ArchivedCount => Challenges.Count(c => c.Status == ChallengeStatus.Archived);
    }

    // ─── Challenge Create / Edit Form ViewModel ───────────────
    public class ChallengeFormViewModel
    {
        public int Id { get; set; }   // 0 = new challenge

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be 3–200 characters.")]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Category")]
        public string Category { get; set; } = ChallengeCategories.Web;

        [Required(ErrorMessage = "Difficulty is required.")]
        [Display(Name = "Difficulty")]
        public string Difficulty { get; set; } = ChallengeDifficulty.Medium;

        [Required(ErrorMessage = "Points value is required.")]
        [Range(1, 10000, ErrorMessage = "Points must be between 1 and 10,000.")]
        [Display(Name = "Points")]
        public int Points { get; set; } = 100;

        [Required(ErrorMessage = "Flag is required.")]
        [StringLength(500, ErrorMessage = "Flag cannot exceed 500 characters.")]
        [Display(Name = "Flag")]
        public string Flag { get; set; } = string.Empty;

        [Display(Name = "Hint")]
        [StringLength(1000, ErrorMessage = "Hint cannot exceed 1000 characters.")]
        public string? Hint { get; set; }

        [Display(Name = "Hint Point Penalty")]
        [Range(0, 10000)]
        public int HintPenalty { get; set; } = 0;

        [Display(Name = "Status")]
        public string Status { get; set; } = ChallengeStatus.Active;

        // File attachment (handled by backend — this is just the UI hook)
        [Display(Name = "Attachment File")]
        public IFormFile? AttachmentFile { get; set; }

        // Existing attachment info (for edit mode)
        public string? ExistingAttachmentName { get; set; }
        public bool    HasExistingAttachment  => !string.IsNullOrEmpty(ExistingAttachmentName);

        // Helpers for select lists
        public bool IsEdit => Id > 0;
        public string PageTitle => IsEdit ? "Edit Challenge" : "Create Challenge";
        public string SubmitLabel => IsEdit ? "Save Changes" : "Create Challenge";
    }
}
