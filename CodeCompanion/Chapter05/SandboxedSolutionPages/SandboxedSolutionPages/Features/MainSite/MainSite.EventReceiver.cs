using System;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using Microsoft.SharePoint.Navigation;

namespace SandboxedSolutionPages.Features.MainSite {

  [Guid("6c255910-5ce4-43ee-a920-1236cb0429b7")]
  public class MainSiteEventReceiver : SPFeatureReceiver {

public override void FeatureActivated(SPFeatureReceiverProperties properties) {

  SPSite siteCollection = (SPSite)properties.Feature.Parent;
  SPWeb site = siteCollection.RootWeb;

  // create dropdown menu for custom site pages
  SPNavigationNodeCollection topNav = site.Navigation.TopNavigationBar;
  topNav.AddAsLast(new SPNavigationNode("Page 1", "WingtipSitePages/SitePage1.aspx"));
  topNav.AddAsLast(new SPNavigationNode("Page 2", "WingtipSitePages/SitePage2.aspx"));
  topNav.AddAsLast(new SPNavigationNode("Page 3", "WingtipSitePages/SitePage3.aspx"));
  topNav.AddAsLast(new SPNavigationNode("Page 4", "WingtipSitePages/SitePage4.aspx"));

}

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {

      SPSite siteCollection = (SPSite)properties.Feature.Parent;
      SPWeb site = siteCollection.RootWeb;

      SPFolder WingtipSitePages = site.GetFolder("WingtipSitePages");
      if (WingtipSitePages.Exists) {
        WingtipSitePages.Delete();
      }

      SPNavigationNodeCollection topNav = site.Navigation.TopNavigationBar;
      for (int i = (topNav.Count - 1); i >= 0; i--) {
        if (topNav[i].Url.Contains("WingtipSitePages")) {
          topNav[i].Delete();
        }
      }
    }
  }
}
