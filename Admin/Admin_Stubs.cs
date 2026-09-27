using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberArenaWebForms.Admin
{
    /// <summary>
    /// Stub definitions for missing controls
    /// </summary>
    public partial class Reports
    {
        protected Repeater rptMostSolved;
        protected Repeater rptLeastSolved;
        protected Repeater rptTeamPerformance;
    }

    public partial class Announcements
    {
        protected Repeater rptAnnouncements;
    }

    public partial class AnnouncementForm
    {
        protected TextBox txtTitle;
        protected TextBox txtMessage;
    }

    public partial class Challenges
    {
        protected Repeater rptChallenges;
    }

    public partial class ChallengeForm
    {
        protected TextBox txtTitle;
        protected TextBox txtDescription;
        protected DropDownList ddlStatus;
    }

    public partial class ChallengeDetails
    {
        protected Literal litTitle;
    }

    public partial class Admin
    {
        protected Literal litPageTitle;
    }
}
