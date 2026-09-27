using System;
using System.Web.UI;

namespace CyberArenaWebForms
{
    /// <summary>
    /// Root redirect page â€” equivalent to HomeController.Index() which redirected to Login.
    /// </summary>
    public partial class DefaultPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx", true);
        }
    }
}