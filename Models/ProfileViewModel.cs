using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CyberArenaWebForms.Models
{
    public class ProfileViewModel
    {
        // Identity info
        public string Username { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }
        public string AvatarInitials
        {
            get
            {
                return string.IsNullOrWhiteSpace(FullName)
                    ? (!string.IsNullOrEmpty(Username) ? Username[0].ToString().ToUpper() : "?")
                    : string.Join("", FullName.Split(' ').Take(2).Select(w => w[0])).ToUpper();
            }
        }

        public ProfileViewModel()
        {
            Username = string.Empty;
            Email = string.Empty;
        }

        // CTF stats
        public int TotalScore { get; set; }
        public int CurrentRank { get; set; }
        public int TotalParticipants { get; set; }
        public int ChallengesSolved { get; set; }
        public int TotalChallenges { get; set; }
        public int CompetitionsEntered { get; set; }
        public int CompetitionsWon { get; set; }

        // Team
        public string TeamName { get; set; }
        public string TeamRole { get; set; } // Captain | Member

        // Dates
        public DateTime JoinDate { get; set; }
        public DateTime? LastActiveDate { get; set; }

        // Bio / Social
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string GitHubHandle { get; set; }
    }
}
