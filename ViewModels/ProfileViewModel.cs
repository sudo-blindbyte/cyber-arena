using System.ComponentModel.DataAnnotations;

namespace cyber_arena.ViewModels
{
    public class ProfileViewModel
    {
        // Identity info
        public string Username { get; set; } = string.Empty;
        public string? FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }
        public string AvatarInitials => string.IsNullOrWhiteSpace(FullName)
            ? (Username.Length > 0 ? Username[0].ToString().ToUpper() : "?")
            : string.Join("", FullName.Split(' ').Take(2).Select(w => w[0])).ToUpper();

        // CTF stats
        public int TotalScore { get; set; }
        public int CurrentRank { get; set; }
        public int TotalParticipants { get; set; }
        public int ChallengesSolved { get; set; }
        public int TotalChallenges { get; set; }
        public int CompetitionsEntered { get; set; }
        public int CompetitionsWon { get; set; }

        // Team
        public string? TeamName { get; set; }
        public string? TeamRole { get; set; } // Captain | Member

        // Dates
        public DateTime JoinDate { get; set; }
        public DateTime? LastActiveDate { get; set; }

        // Bio / Social
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public string? Website { get; set; }
        public string? GitHubHandle { get; set; }

        // Edit mode flag
        public bool IsEditMode { get; set; }

        // Edit form fields (used in POST)
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string? EditFullName { get; set; }

        [Display(Name = "Bio")]
        [StringLength(300)]
        public string? EditBio { get; set; }

        [Display(Name = "Location")]
        [StringLength(100)]
        public string? EditLocation { get; set; }

        [Display(Name = "Website")]
        [Url]
        [StringLength(200)]
        public string? EditWebsite { get; set; }

        [Display(Name = "GitHub Username")]
        [StringLength(50)]
        public string? EditGitHubHandle { get; set; }

        // Change password sub-model
        public ChangePasswordViewModel? ChangePassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your new password.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
