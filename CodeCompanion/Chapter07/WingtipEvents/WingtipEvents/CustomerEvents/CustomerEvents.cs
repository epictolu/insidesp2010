using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace WingtipEvents.CustomerEvents {

  public class CustomerEvents : SPItemEventReceiver {

    protected string ValidationErrorMessage;
    protected bool CustomerIsValid(string WorkPhone, string HomePhone) {
      if ((string.IsNullOrEmpty(WorkPhone)) &&
      (string.IsNullOrEmpty(HomePhone))) {
        ValidationErrorMessage = "Valid customer must have either work phone or home phone.";
        return false;
      }
      else {
        return true;
      }
    }

    public override void ItemAdding(SPItemEventProperties properties) {
      string WorkPhone = properties.AfterProperties["WorkPhone"].ToString();
      string HomePhone = properties.AfterProperties["HomePhone"].ToString();
      if (!CustomerIsValid(WorkPhone, HomePhone)) {
        properties.Cancel = true;
        properties.ErrorMessage = this.ValidationErrorMessage;
      }
    }

    public override void ItemUpdating(SPItemEventProperties properties) {
      string WorkPhone = properties.AfterProperties["WorkPhone"].ToString();
      string HomePhone = properties.AfterProperties["HomePhone"].ToString();
      if (!CustomerIsValid(WorkPhone, HomePhone)) {
        properties.Cancel = true;
        properties.ErrorMessage = this.ValidationErrorMessage;
      }
    }

    public override void ItemAdded(SPItemEventProperties properties) {
      this.EventFiringEnabled = false;
      properties.ListItem["Title"] = properties.ListItem["Title"].ToString().ToUpper();
      properties.ListItem.UpdateOverwriteVersion();
      this.EventFiringEnabled = true;
    }

    public override void ItemUpdated(SPItemEventProperties properties) {
      this.EventFiringEnabled = false;
      properties.ListItem["Title"] = properties.ListItem["Title"].ToString().ToUpper();
      properties.ListItem.UpdateOverwriteVersion();
      this.EventFiringEnabled = true;
    }


  }
}
