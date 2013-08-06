using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace Branding101.Features.Main
{
    [Guid("2cfcf7ce-34b9-417c-8785-bde2d55c1266")]
    public class MainEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var siteCollection = properties.Feature.Parent as SPSite;

            if (siteCollection == null)
                return;

            var topLevelSite = siteCollection.RootWeb;
            var webAppRelativePath = topLevelSite.ServerRelativeUrl.EndsWith("/") ?
                topLevelSite.ServerRelativeUrl : topLevelSite.ServerRelativeUrl + "/";

            foreach (SPWeb site in siteCollection.AllWebs)
            {
                site.MasterUrl = webAppRelativePath + "_catalogs/masterpage/branding101.master";
                site.CustomMasterUrl = webAppRelativePath + "_catalogs/masterpage/branding101.master";
                site.AlternateCssUrl = webAppRelativePath + "Style Library/Branding101/styles.css";
                site.SiteLogoUrl = webAppRelativePath + "Style Library/Branding101/images/logo.gif";
                site.Update();
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var siteCollection = properties.Feature.Parent as SPSite;

            if (siteCollection == null)
                return;

            var topLevelSite = siteCollection.RootWeb;
            var webAppRelativePath = topLevelSite.ServerRelativeUrl.EndsWith("/") ?
                topLevelSite.ServerRelativeUrl : topLevelSite.ServerRelativeUrl + "/";

            foreach (SPWeb site in siteCollection.AllWebs)
            {
                site.MasterUrl = webAppRelativePath + "_catalogs/masterpage/v4.master";
                site.CustomMasterUrl = webAppRelativePath + "_catalogs/masterpage/v4.master";
                site.AlternateCssUrl = "";
                site.SiteLogoUrl = "";
                site.Update();
            }
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
