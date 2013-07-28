using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.UserCode;

namespace WingtipSandboxManagement.Features.MainFarm {

  [Guid("ebea5b5a-196b-4ec5-ab9f-f32337d11ead")]
  public class MainFarmEventReceiver : SPFeatureReceiver {

    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPUserCodeService userCodeService = SPUserCodeService.Local;
      SPSolutionValidator validator = new WingtipSolutionValidator(userCodeService);
      userCodeService.SolutionValidators.Add(validator);      
    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
      SPUserCodeService userCodeService = SPUserCodeService.Local;
      SPSolutionValidator validator = new WingtipSolutionValidator(userCodeService);
      userCodeService.SolutionValidators.Remove(validator.Id);
    }
  }
}
