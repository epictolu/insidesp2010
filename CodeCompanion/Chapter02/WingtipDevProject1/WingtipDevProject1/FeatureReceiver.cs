using System;
using Microsoft.SharePoint;

namespace WingtipDevProject1 {

  public class FeatureReceiver : SPFeatureReceiver {

    public override void FeatureActivated(SPFeatureReceiverProperties props) {
      SPWeb site = props.Feature.Parent as SPWeb;
      if (site != null) {
        site.Title = "Feature Activated";
        site.SiteLogoUrl = @"_layouts/images/WingtipDevProject1/SiteIcon.gif";
        site.Update();
      }
    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties props) {
      SPWeb site = props.Feature.Parent as SPWeb;
      if (site != null) {
        site.Title = "Feature Deactivated";
        site.SiteLogoUrl = "";
        site.Update();
        SPList list = site.Lists.TryGetList("Sales Leads");
        if (list != null) {
          list.Delete();
        }
      }
    }
  }
}
