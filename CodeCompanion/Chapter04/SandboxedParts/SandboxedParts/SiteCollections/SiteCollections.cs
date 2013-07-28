using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Xsl;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.UserCode;
using WingtipTrustedBase;

namespace SandboxedParts.WebApps {

  public class SiteCollections : WebPart {
    protected override void RenderContents(HtmlTextWriter writer) {

      string operationAssemblyName = "WingtipTrustedBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b76db908ec0d4d4";
      string operationTypeName = "WingtipTrustedBase.GetWingtipSiteCollections";
      //string urlWebApp = "http://intranet.wingtip.com";
      //GetWingtipSiteCollectionsArgs args = 
      //  new GetWingtipSiteCollectionsArgs(urlWebApp);
      List<string> SiteCollections =
        SPUtility.ExecuteRegisteredProxyOperation(operationAssemblyName,
                                                  operationTypeName,
                                                  null) as List<string>;

      foreach (var SiteCollection in SiteCollections) {
        writer.Write(SiteCollection + "<br />");
      }
    }
  }
}

    
    