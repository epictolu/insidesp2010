using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace WingtipDevProject2.Features.ProductDevelopment {


  [Guid("bc740d09-badc-4a7b-8472-898a9358b6c9")]
  public class ProductDevelopmentEventReceiver : SPFeatureReceiver {

    public override void FeatureActivated(SPFeatureReceiverProperties props) {
      SPWeb site = props.Feature.Parent as SPWeb;
      if (site != null) {
        site.SiteLogoUrl = @"_layouts/images/WingtipDevProject2/SiteIcon.gif";
        site.Update();
      }
    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties props) {
      SPWeb site = props.Feature.Parent as SPWeb;
      if (site != null) {
        site.SiteLogoUrl = "";
        site.Update();
        SPList list = site.Lists.TryGetList("Scientists");
        if (list != null) {
          list.Delete();
        }
      }
    }

  }
}
