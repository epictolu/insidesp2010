using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Taxonomy;

namespace ContactLocationAutoTagReceiver.Features.ContactLocationAutoTagReceiverDemo {
  /// <summary>
  /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
  /// </summary>
  /// <remarks>
  /// The GUID attached to this class may be used during packaging and should not be modified.
  /// </remarks>

  [Guid("c7bb229a-2f8a-4aaf-93cc-72d274615e5a")]
  public class ContactLocationAutoTagReceiverDemoEventReceiver : SPFeatureReceiver {
    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
      SPWeb site = properties.Feature.Parent as SPWeb;
      SPList list = site.Lists["Contact List with Automatic Tags"];

      CreateManagedMetadataField(site.Site, list);
      AttachContactListAutoTaggerReceiver(list);
    }


    public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
    {
      SPWeb site = properties.Feature.Parent as SPWeb;
      SPList list = site.Lists["Contact List with Automatic Tags"];
      list.Delete();
      list.Update();
    }

    private void CreateManagedMetadataField(SPSite siteCollection, SPList list) {
      TaxonomySession taxonomySession = new TaxonomySession(siteCollection);
      TermStore termStore = taxonomySession.TermStores[0];
      Group termGroup = termStore.Groups["Locations"];
      TermSet termSet = termGroup.TermSets["United States Geography"];

      TaxonomyField taxonomyField = list.Fields.CreateNewField("TaxonomyFieldType", "Location Tag") as TaxonomyField;
      taxonomyField.SspId = termStore.Id;
      taxonomyField.TermSetId = termSet.Id;
      taxonomyField.AllowMultipleValues = true;
      list.Fields.Add(taxonomyField);
      list.Update();
    }

    private void AttachContactListAutoTaggerReceiver(SPList list) {
      SPEventReceiverDefinition itemAddedEvent = list.EventReceivers.Add();
      itemAddedEvent.Type = SPEventReceiverType.ItemAdded;
      itemAddedEvent.Synchronization = SPEventReceiverSynchronization.Synchronous;
      itemAddedEvent.Assembly = System.Reflection.Assembly.GetExecutingAssembly().FullName;
      itemAddedEvent.Class = "ContactLocationAutoTagReceiver.ContactAutoTaggerReceiver.ContactAutoTaggerReceiver";
      itemAddedEvent.Update();

      SPEventReceiverDefinition itemUpdatedEvent = list.EventReceivers.Add();
      itemUpdatedEvent.Type = SPEventReceiverType.ItemUpdated;
      itemUpdatedEvent.Synchronization = SPEventReceiverSynchronization.Synchronous;
      itemUpdatedEvent.Assembly = System.Reflection.Assembly.GetExecutingAssembly().FullName;
      itemUpdatedEvent.Class = "ContactLocationAutoTagReceiver.ContactAutoTaggerReceiver.ContactAutoTaggerReceiver";
      itemUpdatedEvent.Update();
    }

  }
}
