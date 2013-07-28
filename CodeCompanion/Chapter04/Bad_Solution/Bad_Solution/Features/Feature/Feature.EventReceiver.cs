using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace Bad_Solution.Features.Feature
{
    [Guid("369f5a55-50e7-42af-950b-0357663facdb")]
    public class FeatureEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite siteCollection = (SPSite)properties.Feature.Parent;
            SPWeb site = siteCollection.RootWeb;
            site.Title = "Bad Feature Activated!";
            site.Update();
        }


        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPSite siteCollection = (SPSite)properties.Feature.Parent;
            SPWeb site = siteCollection.RootWeb;
            site.Title = "Root Site";
            site.Update();
        }


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
