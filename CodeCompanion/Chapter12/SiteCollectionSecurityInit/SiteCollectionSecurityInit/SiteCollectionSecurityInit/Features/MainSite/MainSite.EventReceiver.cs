using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace SiteCollectionSecurityInit.Features.MainSite {

  [Guid("deb13339-69de-41c5-b090-14c375e7e937")]
  public class MainSiteEventReceiver : SPFeatureReceiver {

    public bool GroupDoesNotExist(SPWeb site, string GroupName) {
      foreach (SPGroup group in site.SiteGroups) {
        if (group.Name.Equals(GroupName)) {
          return false;
        }        
      }
      return true;
    }

    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = properties.Feature.Parent as SPSite;
      if (siteCollection != null) {
        SPWeb site = siteCollection.RootWeb;

        string AdminGroupName = "Admin Guys";
        if (GroupDoesNotExist(site, AdminGroupName)) {
          site.SiteGroups.Add(AdminGroupName, site.CurrentUser, site.CurrentUser, "Site Admins");
          SPGroup AdminGroup = site.SiteGroups[AdminGroupName];
          SPRoleAssignment adminRoleAssignment = new SPRoleAssignment(AdminGroup);
          SPRoleDefinition permLevel1 = site.RoleDefinitions["Full Control"];
          adminRoleAssignment.RoleDefinitionBindings.Add(permLevel1);
          site.RoleAssignments.Add(adminRoleAssignment);
          // add user
          //AdminGroup.AddUser(@"WINGTIP\AC", "ac@CallMeCrazy.net", "Andrew Connell", "don't ask");
        }


        string ContributorGroupName = "Contributor Dudes";
        if (GroupDoesNotExist(site, ContributorGroupName)) {
          site.SiteGroups.Add(ContributorGroupName, site.CurrentUser, site.CurrentUser, "Site Admins");
          SPGroup ContributorGroup = site.SiteGroups[ContributorGroupName];
          SPRoleAssignment ContributorRoleAssignment = new SPRoleAssignment(ContributorGroup);
          SPRoleDefinition permLevel2 = site.RoleDefinitions["Contribute"];
          ContributorRoleAssignment.RoleDefinitionBindings.Add(permLevel2);
          site.RoleAssignments.Add(ContributorRoleAssignment);
          // add AD group
          //ContributorGroup.AddUser(@"WINGTIP\WingtipScientists", "", "", "");
        }


        string ReaderGroupName = "Reader Weenies";
        if (GroupDoesNotExist(site, ReaderGroupName)) {
          site.SiteGroups.Add(ReaderGroupName, site.CurrentUser, site.CurrentUser, "Site Admins");
          SPGroup ReaderGroup = site.SiteGroups[ReaderGroupName];
          SPRoleAssignment ReaderRoleAssignment = new SPRoleAssignment(ReaderGroup);
          SPRoleDefinition permLevel3 = site.RoleDefinitions["Read"];
          ReaderRoleAssignment.RoleDefinitionBindings.Add(permLevel3);
          site.RoleAssignments.Add(ReaderRoleAssignment);
          // add AD group
          //ReaderGroup.AddUser(@"WINGTIP\Domain Users", "", "","");
        }

        SPList list2 = site.Lists["List2"];
        SPSecurableObject secObj = list2;
        secObj.BreakRoleInheritance(false);
    
        // create role assignmentand assign perm level
        SPRoleAssignment list2RoleAssignment;
        list2RoleAssignment = new SPRoleAssignment(site.SiteGroups[ContributorGroupName]);
        SPRoleDefinition permLevel4 = site.RoleDefinitions["Contribute"];
        list2RoleAssignment.RoleDefinitionBindings.Add(permLevel4);
        // add role assignment to securable object
        secObj.RoleAssignments.Add(list2RoleAssignment);


      }
    }


    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = properties.Feature.Parent as SPSite;
      if (siteCollection != null) {
        SPWeb site = siteCollection.RootWeb;
        
      }

    }

  }
}
