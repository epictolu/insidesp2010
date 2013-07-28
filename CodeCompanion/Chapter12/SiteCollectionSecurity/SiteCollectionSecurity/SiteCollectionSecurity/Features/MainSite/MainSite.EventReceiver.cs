using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;
using Microsoft.SharePoint.Security;

namespace SiteCollectionSecurity.Features.MainSite {

  [Guid("7011d279-efc5-4dcb-b381-b049edc5ab56")]
  public class MainSiteEventReceiver : SPFeatureReceiver {
    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = properties.Feature.Parent as SPSite;
      if (siteCollection != null) {
        SPWeb site = siteCollection.RootWeb;
        // create dropdown menu for custom site pages
        SPNavigationNodeCollection topNav = site.Navigation.TopNavigationBar;
        SPNavigationNode node1 = new SPNavigationNode("User Information List", "_layouts/SiteCollectionSecurity/UserInformationList.aspx");
        SPNavigationNode node2 = new SPNavigationNode("Current User", "_layouts/SiteCollectionSecurity/CurrentUser.aspx");
        SPNavigationNode node3 = new SPNavigationNode("Elevated User", "_layouts/SiteCollectionSecurity/ElevatedUserInfo.aspx");                
        site.Navigation.TopNavigationBar.AddAsLast(node1);
        site.Navigation.TopNavigationBar.AddAsLast(node2);
        site.Navigation.TopNavigationBar.AddAsLast(node3);
      }
    }


    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = properties.Feature.Parent as SPSite;
      if (siteCollection != null) {
        SPWeb site = siteCollection.RootWeb;
        SPNavigationNodeCollection topNav = site.Navigation.TopNavigationBar;
        for (int i = topNav.Count - 1; i >= 0; i--) {
          if (topNav[i].Url.Contains("SiteCollectionSecurity")) {
            topNav[i].Delete();
          }
        }
      }

    }

  }
}
