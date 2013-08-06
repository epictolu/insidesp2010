using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FarmSolutionPages.Features.FarmSolutionPages
{
    [Guid("36ecb965-ce05-4b95-97f1-51e997883d42")]
    public class FarmSolutionPagesEventReceiver : SPFeatureReceiver
    {
        //public override void FeatureActivated(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
            var webService = SPFarm.Local.Services.GetValue<SPWebService>();
            webService.ApplyApplicationContentToLocalServer();
        }


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
