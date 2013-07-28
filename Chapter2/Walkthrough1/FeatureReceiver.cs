using Microsoft.SharePoint;

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
    }
}
