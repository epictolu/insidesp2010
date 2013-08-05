using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace SandboxedSolutionPages.Features.Feature1
{
    [Guid("0e7b0fe3-46c6-4dff-aba8-66c7639295c9")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var siteCollection = properties.Feature.Parent as SPSite;
            var site = siteCollection.RootWeb;

            var topNavigation = site.Navigation.TopNavigationBar;
            topNavigation.AddAsLast(new SPNavigationNode("Site Page 1", "WingtipSitePages/SitePage1.aspx"));
            topNavigation.AddAsLast(new SPNavigationNode("Site Page 2", "WingtipSitePages/SitePage2.aspx"));
            topNavigation.AddAsLast(new SPNavigationNode("Site Page 3", "WingtipSitePages/SitePage3.aspx"));
            topNavigation.AddAsLast(new SPNavigationNode("Site Page 4", "WingtipSitePages/SitePage4.aspx"));
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
