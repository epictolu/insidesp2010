using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.UserCode;

namespace FarmParts.TrustedProxyOperations {
  public partial class TrustedProxyOperationsUserControl : UserControl {
    protected override void OnPreRender(EventArgs e) {
      lstTrustedProxyOperations.Items.Clear();

      SPUserCodeService userCodeService = SPUserCodeService.Local;
      foreach (var op in userCodeService.ProxyOperationTypes) {
        lstTrustedProxyOperations.Items.Add(op.TypeName);
      }
   
    }
  }
}
