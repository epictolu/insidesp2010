using System;
using System.Linq;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Linq;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.Taxonomy;
using System.Collections.Generic;

namespace ContactLocationAutoTagReceiver.ContactAutoTaggerReceiver {
  /// <summary>
  /// List Item Events
  /// </summary>
  public class ContactAutoTaggerReceiver : SPItemEventReceiver {

    public override void ItemAdded(SPItemEventProperties properties) {
      base.ItemAdded(properties);

      this.EventFiringEnabled = false;
      UpdateLocationTagsField(properties.ListItem);
      this.EventFiringEnabled = true;
    }

    public override void ItemUpdated(SPItemEventProperties properties) {
      base.ItemUpdated(properties);

      this.EventFiringEnabled = false;
      UpdateLocationTagsField(properties.ListItem);
      this.EventFiringEnabled = true;
    }

    private void UpdateLocationTagsField(SPListItem listItem) {
      using (ContactListAutoTagsEntitiesDataContext dc = new ContactListAutoTagsEntitiesDataContext(listItem.Web.Url)) {
        var query = from contact in dc.ContactListWithAutomaticTags
                    where contact.Id == listItem.ID
                    select contact;
        var contactItem = query.Single();

        TaxonomySession taxonomySession = new TaxonomySession(listItem.Web.Site);
        TermStore termStore = taxonomySession.TermStores[0];
        Group termGroup = termStore.Groups["Locations"];
        TermSet termSet = termGroup.TermSets["United States Geography"];

        List<Term> terms = new List<Term>();
        // check city
        if (!string.IsNullOrEmpty(contactItem.City))
        terms.AddRange(termSet.GetTerms(contactItem.City, true).ToList());
        // check state
        if (!string.IsNullOrEmpty(contactItem.StateProvince))
          terms.AddRange(termSet.GetTerms(contactItem.StateProvince, true).ToList());
        // check zip
        if (!string.IsNullOrEmpty(contactItem.ZIPPostalCode))
          terms.AddRange(termSet.GetTerms(contactItem.ZIPPostalCode, true).ToList());

        if (terms.Count > 0) {
          TaxonomyField locationTags = listItem.Fields["Location Tag"] as TaxonomyField;
          locationTags.SetFieldValue(listItem, terms);
          listItem.Update();
        }
      }
    }
  }
}