using Microsoft.SharePoint;
using System.Collections.Generic;

namespace Walkthrough1
{
    public class FeatureReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var site = properties.Feature.Parent as SPWeb;
            
            if (site == null)
                return;

            site.Title = "Feature Activated";
            site.SiteLogoUrl = @"_layouts/images/Walkthrough1/SiteIcon.gif";
            site.Update();
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var site = properties.Feature.Parent as SPWeb;

            if (site == null)
                return;

            site.Title = "Feature Deactivated";
            site.SiteLogoUrl = "";
            site.Update();

            var list = site.Lists.TryGetList("Sales Leads");

            if (list == null)
                return;

            list.Delete();
        }

        public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, IDictionary<string, string> parameters)
        {
            var site = properties.Feature.Parent as SPWeb;

            if (site == null)
                return;

            switch (upgradeActionName)
            {
                case "UpdateSiteTitle":
                    var newTitle = parameters["NewSiteTitle"] as string;
                    
                    site.Title = newTitle;
                    site.Update();
                    break;

                default:
                    break;
            }
        }
    }
}
