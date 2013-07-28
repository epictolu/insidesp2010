using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace LeadTracker.Features.MainSite {

  [Guid("65444c8e-6575-4af5-b1a6-a769070319ed")]
  public class MainSiteEventReceiver : SPFeatureReceiver {

    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = properties.Feature.Parent as SPSite;
      if (siteCollection != null) {
        SPWeb site = siteCollection.RootWeb;
        // configure Lead Tracker lists
        SPList listSalesLeads = site.Lists["Sales Leads"];
        SPView viewSalesLeads = listSalesLeads.Views["All contacts"];
        viewSalesLeads.TabularView = false;
        viewSalesLeads.ViewFields.Delete("HomePhone");
        viewSalesLeads.ViewFields.Delete("Email");
        viewSalesLeads.Update();
        listSalesLeads.EnableAttachments = false;
        listSalesLeads.Update();

        // initialize quick launch
        SPNavigationNodeCollection quickLaunch = site.Navigation.QuickLaunch;
        // delete all existing nodes
        for (int i = quickLaunch.Count - 1; i >= 0; i--) {
          quickLaunch[i].Delete();
        }
        // create lead tracker heading node
        SPNavigationNode heading = quickLaunch.AddAsFirst(new SPNavigationNode("Lead Tracker", ""));
        // create nodes for lead tracker lists
        string urlSite = site.ServerRelativeUrl;
        if (!urlSite.EndsWith(@"/")) { urlSite += @"/"; }
        string urlListSalesLeads = urlSite + "Lists/SalesLeads";
        heading.Children.AddAsLast(new SPNavigationNode("Sales Leads", "Lists/SalesLeads"));
        string urlListCustomers = urlSite + "Lists/Customers";
        heading.Children.AddAsLast(new SPNavigationNode("Customers", "Lists/Customers"));

        heading = quickLaunch.AddAsLast(new SPNavigationNode("Other Sites", ""));
        heading.Children.AddAsLast(new SPNavigationNode("Bing", "http://www.bing.com", true));
        heading.Children.AddAsLast(new SPNavigationNode("MSDN", "http://msdn.microsoft.com", true));

      }

    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = properties.Feature.Parent as SPSite;
      if (siteCollection != null) {
        SPWeb site = siteCollection.RootWeb;
        // delete lists
        SPList list;
        list = site.Lists.TryGetList("Sales Leads");
        if (list != null) list.Delete();
        list = site.Lists.TryGetList("Customers");
        if (list != null) list.Delete();
        // delete top-nav links
        SPNavigationNodeCollection topNav = site.Navigation.TopNavigationBar;
        for (int i = topNav.Count - 1; i >= 0; i--) {
          if (topNav[i].Url.Contains("SalesLeads") ||
              topNav[i].Url.Contains("Customers")  ||
              topNav[i].Title.Contains("Lead")) {
            topNav[i].Delete();
          }
        }

      }
    }


  }
}
