using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Web.UI.WebControls.WebParts;

namespace WingtipWebParts.Features.WebPartsPreconnected
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("2b4f3128-b634-40ad-ad78-cf6040f0b36e")]
    public class WebPartsPreconnectedEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var site = properties.Feature.Parent as SPWeb;
            var page = site.GetFile("WebPartsPreconnected/WebPartsPreconnected.aspx");
            var webPartManager = page.GetLimitedWebPartManager(PersonalizationScope.Shared);

            var providerWebPart = new FontConnectionProvider.cs.FontConnectionProvider();
            providerWebPart.Title = "Font Provider 1";
            providerWebPart.TextFontSize = 25;
            providerWebPart.TextFontColor = "Green";
            webPartManager.AddWebPart(providerWebPart, "Main", 0);

            var consumerWebPart = new FontConnectionConsumer.cs.FontConnectionConsumer();
            consumerWebPart.Title = "Font Consumer 1";
            webPartManager.AddWebPart(consumerWebPart, "TopRight", 0);

            webPartManager.SPConnectWebParts(providerWebPart, webPartManager.GetProviderConnectionPoints(providerWebPart).Default, consumerWebPart, webPartManager.GetConsumerConnectionPoints(consumerWebPart).Default);
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
