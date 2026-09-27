using System;
using System.Web.UI;
using System.Linq;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;

namespace CyberArenaWebForms.Admin
{
    public partial class AnnouncementFormPage : Page
    {
        public AnnouncementFormViewModel VM { get; private set; } = new AnnouncementFormViewModel();

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
                master.PageTitle = VM.PageTitle;
            }
            Page.Title = VM.PageTitle + " â€” CyberArena";

            if (!IsPostBack)
            {
                BindForm();
            }
        }

        private void BindForm()
        {
            if (VM.IsEdit)
            {
                var ann = AdminMockData.GetAnnouncements().FirstOrDefault(a => a.Id == VM.Id);
                if (ann != null)
                {
                    txtTitle.Text = ann.Title;
                    txtBody.Text = ann.Body;
                    chkIsPinned.Checked = ann.IsPinned;
                    VM.IsPinned = ann.IsPinned;
                }
                else
                {
                    Response.Redirect("~/Admin/Announcements.aspx");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            VM.Title = txtTitle.Text.Trim();
            VM.Body = txtBody.Text.Trim();
            VM.IsPinned = chkIsPinned.Checked;

            if (VM.IsEdit)
            {
                // TODO: EF Core update
                SessionHelper.SetFlash("AdminSuccess", $"Announcement \"{VM.Title}\" updated successfully.");
            }
            else
            {
                // TODO: EF Core insert
                SessionHelper.SetFlash("AdminSuccess", $"Announcement \"{VM.Title}\" published successfully.");
            }

            Response.Redirect("~/Admin/Announcements.aspx");
        }
    }
}

