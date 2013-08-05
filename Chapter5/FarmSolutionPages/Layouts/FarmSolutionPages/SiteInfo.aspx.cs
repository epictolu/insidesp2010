using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace FarmSolutionPages.Layouts.FarmSolutionPages
{
    public partial class SiteInfo : LayoutsPageBase
    {
        protected override void OnInit(EventArgs e)
        {
            cmdReturn.Click += new EventHandler(cmdReturn_Click);
        }

        void cmdReturn_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect("settings.aspx", SPRedirectFlags.RelativeToLayoutsPage, Context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
