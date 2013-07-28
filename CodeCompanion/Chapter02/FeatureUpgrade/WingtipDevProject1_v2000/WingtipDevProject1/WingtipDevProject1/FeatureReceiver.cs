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
        list = site.Lists.TryGetList("Customers");
        if (list != null) {
          list.Delete();
        }

      }
    }

    public override void FeatureUpgrading(SPFeatureReceiverProperties props, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters) {
      SPWeb site = props.Feature.Parent as SPWeb;
      if (site != null) {
        // determine which custom upgrade action is executing
        switch (upgradeActionName) {
          case "UpdateSiteTitle":
            // begin custom upgrade action 
            string NewTitle = parameters["NewSiteTitle"];
            site.Title = NewTitle;
            site.Update();
            break;
            // end custom upgrade action 
          default:
            // unexpected feature upgrade action
            break;
        }
      }
    }
  }
}
