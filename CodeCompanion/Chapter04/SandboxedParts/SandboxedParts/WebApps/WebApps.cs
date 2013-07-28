using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.UserCode;

namespace SandboxedParts.WebApps {

  public class WebApps : WebPart {
    protected override void RenderContents(HtmlTextWriter writer) {
      string operationAssemblyName = 
        "WingtipTrustedBase, Version=1.0.0.0, " + 
        "Culture=neutral, PublicKeyToken=3b76db908ec0d4d4";
      string operationTypeName = "WingtipTrustedBase.GetWingtipWebApps";
      List<string> WebApplications = 
        SPUtility.ExecuteRegisteredProxyOperation(operationAssemblyName,
                                                  operationTypeName,
                                                  null) as List<string>;
      
      // use the data returned from full trsut proxy operation
      foreach (var WebApplication in WebApplications) {
        writer.Write(WebApplication + "<br />");
      }
    }
  }
}
