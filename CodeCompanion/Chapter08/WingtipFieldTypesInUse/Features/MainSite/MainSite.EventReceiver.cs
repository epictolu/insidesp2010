using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace WingtipFieldTypesInUse.Features.MainSite {
  
  [Guid("c1534ce3-3030-4cf4-a837-25a18a7b6a9a")]
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

      SPField fldAddress = site.Fields.GetFieldByInternalName("UnitedStatesAddress");
      list.Fields.Add(fldAddress);

      SPField fldEmployeeStatus = site.Fields.GetFieldByInternalName("EmployeeStatus");
      list.Fields.Add(fldEmployeeStatus);


      SPView view = list.DefaultView;
      view.ViewFields.Add(fldFirstName);
      view.ViewFields.Add(fldStartDate);      
      view.ViewFields.Add(fldSSN);
      view.ViewFields.Add(fldAddress);
      view.ViewFields.Add(fldEmployeeStatus);      
      view.Update();

    }

  }
}
