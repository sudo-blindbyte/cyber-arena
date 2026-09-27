using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CyberArenaWebForms.Helpers;
using CyberArenaWebForms.Models;

namespace CyberArenaWebForms.Competitions
{
    public partial class CompetitionsPage : Page
    {
        public CompetitionListViewModel VM { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.RequireUserSession(this.Page);
            Page.Title = "Competitions â€” CyberArena";

            var master = Master as SiteMaster;
            if (master != null) master.PageTitle = "Competitions";

            if (!IsPostBack)
            {
                VM = BuildViewModel();
                BindPage();
            }
        }

        private CompetitionListViewModel BuildViewModel()
        {
            string status = Request.QueryString["status"] ?? "all";
            
            return new CompetitionListViewModel
            {
                Competitions = CompetitionMockData.GetAll(),
                FilterStatus = status
            };
        }

        private void BindPage()
        {
            litCompCount.Text = VM.Competitions.Count.ToString();

            if (VM.Active.Count > 0)
            {
                pnlActiveBanner.Visible = true;
                litActiveCount.Text = VM.Active.Count.ToString();
                litActivePlural.Text = VM.Active.Count != 1 ? "s" : "";
            }

            rptCompetitions.DataSource = VM.Competitions;
            rptCompetitions.DataBind();
        }

        protected void rptCompetitions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Join")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                // TODO: EF Core â€” register current user in competition
                SessionHelper.SetFlash("AdminSuccess", "You have successfully joined the competition!");
                Response.Redirect($"~/Competitions/CompetitionDetails.aspx?id={id}");
            }
        }
    }
}

