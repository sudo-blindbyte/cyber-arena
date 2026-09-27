using System;
using System.Web.UI;
using System.Linq;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;

namespace CyberArenaWebForms.Admin
{
    public partial class ChallengeFormPage : Page
    {
        public ChallengeFormViewModel VM { get; private set; } = new ChallengeFormViewModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireAdminSession(this.Page);
            
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int id))
            {
                VM.Id = id;
            }

            var master = Master as AdminMaster;
            if (master != null)
            {
                master.PageTitle = VM.IsEdit ? "Edit Challenge" : "Create Challenge";
            }
            Page.Title = (VM.IsEdit ? "Edit Challenge" : "Create Challenge") + " â€” CyberArena";

            if (!IsPostBack)
            {
                PopulateDropdowns();
                BindForm();
            }
        }

        /* private void PopulateDropdowns()
        {
            ddlCategory.DataSource = ChallengeCategories.All;
            ddlCategory.DataBind();

            ddlDifficulty.DataSource = ChallengeDifficulty.All;
            ddlDifficulty.DataBind();

            ddlStatus.DataSource = ChallengeStatus.All;
            ddlStatus.DataBind();
        } */

        private void BindForm()
        {
            if (VM.IsEdit)
            {
                var ch = AdminMockData.GetAdminById(VM.Id);
                if (ch != null)
                {
                    txtTitle.Text = ch.Title;
                    txtDescription.Text = "Example description loaded from database.";
                    ddlCategory.SelectedValue = ch.Category;
                    ddlDifficulty.SelectedValue = ch.Difficulty;
                    txtPoints.Text = ch.Points.ToString();
                    txtFlag.Attributes["value"] = "CTF{placeholder_flag}";
                    txtHint.Text = "Example hint text loaded from database.";
                    txtHintPenalty.Text = "0";
                    ddlStatus.SelectedValue = ch.Status;

                    VM.ExistingAttachmentName = ch.HasAttachment ? $"challenge_{VM.Id}_files.zip" : null;
                }
                else
                {
                    Response.Redirect("~/Admin/Challenges.aspx");
                }
            }
            else
            {
                txtPoints.Text = "100";
                txtHintPenalty.Text = "0";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            VM.Title = txtTitle.Text.Trim();
            VM.Description = txtDescription.Text.Trim();
            VM.Category = ddlCategory.SelectedValue;
            VM.Difficulty = ddlDifficulty.SelectedValue;
            int.TryParse(txtPoints.Text, out int pts);
            VM.Points = pts;
            VM.Flag = txtFlag.Text;
            VM.Hint = txtHint.Text.Trim();
            int.TryParse(txtHintPenalty.Text, out int hp);
            VM.HintPenalty = hp;
            VM.Status = ddlStatus.SelectedValue;

            if (VM.IsEdit)
            {
                // TODO: EF Core update
                SessionHelper.SetFlash("AdminSuccess", $"Challenge \"{VM.Title}\" updated successfully.");
            }
            else
            {
                // TODO: EF Core insert
                SessionHelper.SetFlash("AdminSuccess", $"Challenge \"{VM.Title}\" created successfully.");
            }

            Response.Redirect("~/Admin/Challenges.aspx");
        }
    }
}

