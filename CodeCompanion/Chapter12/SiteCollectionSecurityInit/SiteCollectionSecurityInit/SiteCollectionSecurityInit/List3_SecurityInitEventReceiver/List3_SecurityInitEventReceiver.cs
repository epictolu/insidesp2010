using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace SiteCollectionSecurityInit.List3_SecurityInitEventReceiver {

  public class List3_SecurityInitEventReceiver : SPItemEventReceiver {
    public override void ItemAdded(SPItemEventProperties properties) {
      SPWeb site = properties.OpenWeb();
      SPListItem item = properties.ListItem;
      item.BreakRoleInheritance(false);
      // create role assignmentand assign perm level
      SPRoleAssignment list3ItemRoleAssignment;
      string ContributorGroupName = "Contributor Dudes";
      list3ItemRoleAssignment = new SPRoleAssignment(site.SiteGroups[ContributorGroupName]);
      SPRoleDefinition permLevel = site.RoleDefinitions["Read"];
      list3ItemRoleAssignment.RoleDefinitionBindings.Add(permLevel);
      item.RoleAssignments.Add(list3ItemRoleAssignment);
  
    }
  }
}
