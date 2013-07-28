using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace FarmSolutionPages.Layouts.FarmSolutionPages {
  public partial class SiteConfig : LayoutsPageBase {

    protected override void OnInit(EventArgs e) {
      cmdUpdateSite.Click += new EventHandler(cmdUpdateSite_Click);
      cmdCancel.Click += new EventHandler(cmdCancel_Click);
    }

    void cmdUpdateSite_Click(object sender, EventArgs e) {
      
      this.Web.Title = txtSiteTitle.Text;
      if (!string.IsNullOrEmpty(txtSiteDescription.Text))
        this.Web.Description = txtSiteDescription.Text;
      this.Web.Update();

      SPUtility.Redirect("settings.aspx", SPRedirectFlags.RelativeToLayoutsPage,
                this.Context);
    }

    void cmdCancel_Click(object sender, EventArgs e) {
      SPUtility.Redirect("settings.aspx", SPRedirectFlags.RelativeToLayoutsPage,
                this.Context);
    }
    
    protected override void OnPreRender(EventArgs e) {
      txtSiteTitle.Text = this.Web.Title;
      txtSiteDescription.Text = this.Web.Description;
    }
  }
}
