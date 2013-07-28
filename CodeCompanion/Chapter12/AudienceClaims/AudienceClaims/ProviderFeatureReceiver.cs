using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Diagnostics;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.WebControls;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Claims;

namespace AudienceClaims {
  public class ProviderFeatureReceiver : SPClaimProviderFeatureReceiver {
    private void ExecBaseFeatureActivated(SPFeatureReceiverProperties properties) {
      base.FeatureActivated(properties);
    }

    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      ExecBaseFeatureActivated(properties);
    }

    public override string ClaimProviderAssembly {
      get { return typeof(Provider).Assembly.FullName; }
    }

    public override string ClaimProviderDescription {
      get { return "Audience Claim Provider"; }
    }

    public override string ClaimProviderDisplayName {
      get { return "Audience Claim Provider"; }
    }

    public override string ClaimProviderType {
      get { return typeof(Provider).FullName; }
    }
  }
}
