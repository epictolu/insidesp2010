using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace WingtipEvents.ListEvents {

  public class ListEvents : SPListEventReceiver {

    public override void ListAdded(SPListEventProperties properties) {
      // get reference to list
      SPList list = properties.List;
      // determine if list was created from document library template
      if (list.BaseTemplate == SPListTemplateType.DocumentLibrary) {
        // convert SPList reference to SPDocumentLibrary
        SPDocumentLibrary doclib = (SPDocumentLibrary)list;
        // configure document library with standard Wingtip settings
        doclib.ForceCheckout = true;
        doclib.EnableVersioning = true;
        doclib.MajorVersionLimit = 3;
        doclib.EnableMinorVersions = true;
        doclib.MajorWithMinorVersionsLimit = 5;
        doclib.Update();
      }
    }

    public override void ListDeleting(SPListEventProperties properties) {
      if (!properties.Web.UserIsSiteAdmin) {
        properties.Cancel = true;
        properties.ErrorMessage = "Only site collection owners can delete lists";
      }
    }


  }
}
