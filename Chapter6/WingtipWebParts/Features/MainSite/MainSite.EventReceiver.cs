using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls.WebParts;

namespace WingtipWebParts.Features.MainSite
{
    [Guid("006bfae0-e42d-42d3-b7ec-5c748418ccdb")]
    public class MainSiteEventReceiver : SPFeatureReceiver
    {
/*
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var siteCollection = properties.Feature.Parent as SPSite;
            var site = siteCollection.RootWeb;
            var homePage = site.GetFile("default.aspx");
            var wpm = homePage.GetLimitedWebPartManager(PersonalizationScope.Shared);

            while (wpm.WebParts.Count > 0)
                wpm.DeleteWebPart(wpm.WebParts[0]);

            var webPart = new ImageWebPart();
            webPart.ChromeType = PartChromeType.None;
            webPart.ImageLink = "_layouts/images/IPVW.GIF";
            wpm.AddWebPart(webPart, "Right", 0);
        }
*/
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var siteCollection = properties.Feature.Parent as SPSite;
            var site = siteCollection.RootWeb;

            var filesToDelete = new List<SPFile>();
            var webPartGallery = site.Lists["Web Part Gallery"];
            var webPartGalleryItems = webPartGallery.Items;

            foreach (SPListItem item in webPartGalleryItems)
                if (item.File.Name.Contains("WingtipWebParts"))
                    filesToDelete.Add(item.File);

            foreach (var file in filesToDelete)
                file.Delete();
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
