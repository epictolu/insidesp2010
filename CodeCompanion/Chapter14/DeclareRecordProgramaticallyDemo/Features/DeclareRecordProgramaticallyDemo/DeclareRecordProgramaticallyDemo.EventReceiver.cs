using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace DeclareRecordProgramaticallyDemo.Features.DeclareRecordProgramaticallyDemo {
  /// <summary>
  /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
  /// </summary>
  /// <remarks>
  /// The GUID attached to this class may be used during packaging and should not be modified.
  /// </remarks>

  [Guid("77d3e413-d9ec-48e7-a279-61124f0eb3ed")]
  public class DeclareRecordProgramaticallyDemoEventReceiver : SPFeatureReceiver {
    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPWeb site = properties.Feature.Parent as SPWeb;
      SPList list = site.Lists["Declare Records Demo"];

      list.EventReceivers.Add(
        SPEventReceiverType.ItemAdded,
        System.Reflection.Assembly.GetExecutingAssembly().FullName,
        "DeclareRecordProgramaticallyDemo.DeclareRecordReceiver.DeclareRecordReceiver"
      );
      list.EventReceivers.Add(
        SPEventReceiverType.ItemUpdated,
        System.Reflection.Assembly.GetExecutingAssembly().FullName,
        "DeclareRecordProgramaticallyDemo.DeclareRecordReceiver.DeclareRecordReceiver"
      );

      list.Update();
    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
      SPWeb site = properties.Feature.Parent as SPWeb;
      SPList list = site.Lists["Declare Records Demo"];
      list.Delete();
      list.Update();
    }
  }
}
