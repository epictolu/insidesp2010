using System;
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

namespace SandboxedParts.SalesLeads {

  public class SalesLeads : WebPart {


      //string operationAssemblyName = "WingtipTrustedBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b76db908ec0d4d4";
      //string operationTypeName = "WingtipTrustedBase.GetWingtipSalesLeads";
      //string xml = SPUtility.ExecuteRegisteredProxyOperation(operationAssemblyName,
      //                                                       operationTypeName,
      //                                                       null) as string;

      //return new System.Text.StringBuilder(xml).ToString();

    protected override void RenderContents(HtmlTextWriter writer) {

      string operationAssemblyName = "WingtipTrustedBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b76db908ec0d4d4";
      string operationTypeName = "WingtipTrustedBase.GetWingtipSalesLeads";
      string xml = SPUtility.ExecuteRegisteredProxyOperation(operationAssemblyName,
                                                             operationTypeName,
                                                             null) as string;

      writer.Write(xml);

      //writer.Write("xxx");
    }
  }
}
