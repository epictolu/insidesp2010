using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace WingtipTrustedBase {
  public class GetWingtipWebApps : SPProxyOperation {

    public override object Execute(SPProxyOperationArgs args) {
      List<string> webApps = new List<string>();
      SPWebService webService = SPFarm.Local.Servers.GetValue<SPWebService>();
      foreach (SPWebApplication webApp in webService.WebApplications) {
        webApps.Add(webApp.DisplayName);
      }
      return webApps;
    }
  }
}
