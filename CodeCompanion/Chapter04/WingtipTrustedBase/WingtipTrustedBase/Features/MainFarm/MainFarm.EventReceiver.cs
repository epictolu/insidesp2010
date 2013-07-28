using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.UserCode;
using Microsoft.SharePoint.Administration;


namespace WingtipTrustedBase.Features.MainFarm {

  [Guid("ac52a0b6-7d95-400a-a911-25a103fa3ba7")]
  public class MainFarmEventReceiver : SPFeatureReceiver {

    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      // register trusted operations
      SPUserCodeService userCodeService = SPUserCodeService.Local;
      if (userCodeService != null) {
        string assemblyName = this.GetType().Assembly.FullName;

        SPProxyOperationType GetWingtipWebAppsOperation =
            new SPProxyOperationType(assemblyName, typeof(GetWingtipWebApps).FullName);

        SPProxyOperationType GetWingtipSiteCollectionsOperation =
            new SPProxyOperationType(assemblyName, typeof(GetWingtipSiteCollections).FullName);

        SPProxyOperationType GetWingtipSalesLeadsOperation =
            new SPProxyOperationType(assemblyName, typeof(GetWingtipSalesLeads).FullName);


        userCodeService.ProxyOperationTypes.Add(GetWingtipWebAppsOperation);
        userCodeService.ProxyOperationTypes.Add(GetWingtipSiteCollectionsOperation);
        userCodeService.ProxyOperationTypes.Add(GetWingtipSalesLeadsOperation);

        userCodeService.Update();

      }
      else {
       throw new ApplicationException("User Code Service is not running.");
      }
    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
      // unregister trusted operations
      SPUserCodeService userCodeService = SPUserCodeService.Local;
      if (userCodeService != null) {
        string assemblyName = this.GetType().Assembly.FullName;

        SPProxyOperationType GetWingtipWebAppsOperation =
            new SPProxyOperationType(assemblyName, typeof(GetWingtipWebApps).FullName);

        SPProxyOperationType GetWingtipSiteCollectionsOperation =
            new SPProxyOperationType(assemblyName, typeof(GetWingtipSiteCollections).FullName);

        SPProxyOperationType GetWingtipSalesLeadsOperation =
            new SPProxyOperationType(assemblyName, typeof(GetWingtipSalesLeads).FullName);


        userCodeService.ProxyOperationTypes.Remove(GetWingtipWebAppsOperation);
        userCodeService.ProxyOperationTypes.Remove(GetWingtipSiteCollectionsOperation);
        userCodeService.ProxyOperationTypes.Remove(GetWingtipSalesLeadsOperation);

        userCodeService.Update();

      }
      else {
        throw new ApplicationException("User Code Service is not running.");
      }
    }
  
  }
}
