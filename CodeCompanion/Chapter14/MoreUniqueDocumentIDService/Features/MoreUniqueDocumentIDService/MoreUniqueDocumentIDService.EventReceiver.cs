using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.Office.DocumentManagement;

namespace MoreUniqueDocumentIDService.Features.MoreUniqueDocumentIDService {
  /// <summary>
  /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
  /// </summary>
  /// <remarks>
  /// The GUID attached to this class may be used during packaging and should not be modified.
  /// </remarks>

  [Guid("6cdbfdb5-a7be-457b-aae1-8748c3691629")]
  public class MoreUniqueDocumentIDServiceEventReceiver : SPFeatureReceiver {
    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = properties.Feature.Parent as SPSite;
      MoreUniqueDocumentIDProvider docIDProvider = new MoreUniqueDocumentIDProvider();

      DocumentId.SetProvider(siteCollection, docIDProvider);
    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
    {
      SPSite siteCollection = properties.Feature.Parent as SPSite;

      // set the provider back to the original provider
      DocumentIdProvider docIDProvider = new Microsoft.Office.DocumentManagement.Internal.OobProvider();
      DocumentId.SetProvider(siteCollection, docIDProvider);
    }
  }
}
