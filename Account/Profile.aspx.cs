using System;
using System.Web.UI;
using CyberArenaWebForms.Models;
using CyberArenaWebForms.Helpers;

namespace CyberArenaWebForms.Account
{
    public partial class ProfilePage : Page
    {
        public ProfileViewModel VM { get; private set; } = new ProfileViewModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as SiteMaster;
            if (master != null)
            {
                master.PageTitle = "My Profile";
                master.ActiveRoute = "Profile";
            }
            Page.Title = "My Profile â€” CyberArena";

            string successFlash = SessionHelper.GetFlash("ProfileSuccess");
            if (!string.IsNullOrEmpty(successFlash))
            {
                pnlSuccess.Visible = true;
                litSuccessMsg.Text = System.Web.HttpUtility.HtmlEncode(successFlash);
            }

            string errorFlash = SessionHelper.GetFlash("ProfileError");
            if (!string.IsNullOrEmpty(errorFlash))
            {
                pnlError.Visible = true;
                litErrorMsg.Text = System.Web.HttpUtility.HtmlEncode(errorFlash);
            }

            // In a real app we'd load the current user's profile
            LoadMockProfile();

            if (!IsPostBack)
            {
                txtFullName.Text = VM.FullName;
                txtBio.Text = VM.Bio;
                txtLocation.Text = VM.Location;
                txtWebsite.Text = VM.Website;
                txtGitHub.Text = VM.GitHubHandle;
            }
        }

        private void LoadMockProfile()
        {
            VM.Username = "cyber_ninja";
            VM.FullName = "Alex Chen";
            VM.Email = "alex.chen@example.com";
            VM.AvatarInitials = "CN";
            VM.JoinDate = new DateTime(2023, 5, 12);
            VM.LastActiveDate = DateTime.Now.AddHours(-2);
            VM.TeamName = "ByteForce";
            VM.TeamRole = "Captain";
            
            VM.ChallengesSolved = 42;
            VM.TotalChallenges = 120;
            VM.TotalScore = 14500;
            VM.CurrentRank = 8;
            VM.TotalParticipants = 312;
            VM.CompetitionsEntered = 4;
            VM.CompetitionsWon = 1;

            VM.Bio = "Full-stack developer by day, CTF enthusiast by night. Passionate about web security and cryptography.";
            VM.Location = "San Francisco, CA";
            VM.Website = "https://alexchen.dev";
            VM.GitHubHandle = "alexc-sec";
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            // TODO: EF Core save profile

            SessionHelper.SetFlash("ProfileSuccess", "Your profile has been updated successfully.");
            Response.Redirect("~/Account/Profile.aspx");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            // Mock success
            SessionHelper.SetFlash("ProfileSuccess", "Your password has been changed successfully.");
            Response.Redirect("~/Account/Profile.aspx");
        }
    }
}

