using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using WingtipLists;

namespace WingtipLists.Features.MainSite {

  [Guid("e4e5552f-94fd-4cec-9b31-6d548db3119a")]
  public class MainSiteEventReceiver : SPFeatureReceiver {

    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = (SPSite)properties.Feature.Parent;
      SPWeb site = siteCollection.RootWeb;

      WingtipListFactory.CreateMarketingTasksList(site);
      WingtipListFactory.CreateWingtipSientistsList(site);
      WingtipListFactory.CreateProductRelatedLists(site);
      WingtipListFactory.CreateSalesLeadsList(site);
      WingtipListFactory.CreateEmployeeStatusSiteColumn(site);
      WingtipListFactory.CreateEmployeesList(site);
      WingtipListFactory.CreatePersonContentType(site);
      WingtipListFactory.CreatePeopleList(site);
      WingtipListFactory.CreateWikePageLibrary(site);

      WingtipListFactory.UpdateEmployeeStatusSiteColumn(site);
      WingtipListFactory.UpdateContactContentType(site);
    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {

      SPSite siteCollection = (SPSite)properties.Feature.Parent;
      SPWeb site = siteCollection.RootWeb;

      string[] ListsToDelete = {"Customers", 
                                "Marketing Tasks", 
                                "Wingtip Scientists", 
                                "Sales Leads", 
                                "Employees",
                                "People"};

      foreach (string list in ListsToDelete) {
        try { site.Lists[list].Delete(); }
        catch { /* ignore error */ }
      }

      WingtipListFactory.DeleteProductRelatedLists(site);
      WingtipListFactory.DeleteEmployeeStatusSiteColumn(site);
      WingtipListFactory.DeletePersonContentType(site);

    }
  }
}
