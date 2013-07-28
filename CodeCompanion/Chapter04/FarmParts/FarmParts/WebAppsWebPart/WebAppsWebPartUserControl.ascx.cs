using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FarmParts.WebAppsWebPart {
  public partial class WebAppsWebPartUserControl : UserControl {
    protected override void OnPreRender(EventArgs e) {
      lstWebApps.Items.Clear();
      SPWebService webService = SPFarm.Local.Servers.GetValue<SPWebService>();
      foreach (SPWebApplication webApp in webService.WebApplications) {
       lstWebApps.Items.Add(webApp.DisplayName);
      }
      
    }
  }
}
