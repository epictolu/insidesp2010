using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace WingtipEmployeeTypes.Features.MainSite {

  [Guid("139253ce-301f-45fa-b075-9fc4d5071bdc")]
  public class MainSiteEventReceiver : SPFeatureReceiver {

    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPSite sc = (SPSite)properties.Feature.Parent;
      SPWeb site = sc.RootWeb;
      SPList list = site.Lists["Employees"];

      SPField fldTitle = list.Fields.GetFieldByInternalName("Title");
      fldTitle.Title = "Last Name";
      fldTitle.Update();

      SPField fldFirstName = site.Fields.GetFieldByInternalName("FirstName");
      fldFirstName.Required = true;
      list.Fields.Add(fldFirstName);


      SPField fldStartDate = site.Fields.GetFieldByInternalName("EmployeeStartDate");
      list.Fields.Add(fldStartDate);

      SPField fldSSN = site.Fields.GetFieldByInternalName("SocialSecurityNumber");
      list.Fields.Add(fldSSN);

      SPView view = list.DefaultView;
      view.ViewFields.Add(fldFirstName);
      view.ViewFields.Add(fldStartDate);
      view.ViewFields.Add(fldSSN);
      view.Update();

    }


  }
}
