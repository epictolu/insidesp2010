using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Microsoft.Office.RecordsManagement.RecordsRepository;

namespace DeclareRecordProgramaticallyDemo.DeclareRecordReceiver {
  /// <summary>
  /// List Item Events
  /// </summary>
  public class DeclareRecordReceiver : SPItemEventReceiver {
    public override void ItemAdded(SPItemEventProperties properties) {
      base.ItemAdded(properties);

      if (properties.ListItem.Name.ToLower().Contains("declare")) {
        this.EventFiringEnabled = false;
        Records.DeclareItemAsRecord(properties.ListItem);
        this.EventFiringEnabled = true;
      }
    }

    public override void ItemUpdated(SPItemEventProperties properties) {
      base.ItemUpdated(properties);

      if (properties.ListItem.Name.ToLower().Contains("declare")) {
        this.EventFiringEnabled = false;
        Records.DeclareItemAsRecord(properties.ListItem);
        this.EventFiringEnabled = true;
      }
    }
  }
}
