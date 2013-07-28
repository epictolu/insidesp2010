using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.UserCode;

namespace WingtipTrustedBase {

  [Serializable]
  public class GetWingtipSiteCollectionsArgs : SPProxyOperationArgs {

    public string TargetWebApplicationUrl { get; set; }
    
    public GetWingtipSiteCollectionsArgs(string TargetWebApplicationUrl) {
      this.TargetWebApplicationUrl = TargetWebApplicationUrl;
    }

  }
}
