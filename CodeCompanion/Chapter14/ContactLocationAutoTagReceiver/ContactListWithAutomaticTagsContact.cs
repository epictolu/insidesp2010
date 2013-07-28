using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Taxonomy;

namespace ContactLocationAutoTagReceiver {
  public partial class ContactListWithAutomaticTagsContact : Contact, ICustomMapping {
    private const string LOCATION_TAG_FIELD = "Location_x0020_Tag";

    [CustomMapping(Columns = new string[] { LOCATION_TAG_FIELD })]
    public void MapFrom(object listItem) {
      SPListItem item = listItem as SPListItem;
      this.LocationTag = item[LOCATION_TAG_FIELD] as TaxonomyField;
    }

    public void MapTo(object listItem) {
      SPListItem item = listItem as SPListItem;
      item[LOCATION_TAG_FIELD] = this.LocationTag;
    }

    public void Resolve(RefreshMode mode, object originalListItem, object databaseListItem) {
      SPListItem originalItem = originalListItem as SPListItem;
      SPListItem databaseItem = databaseListItem as SPListItem;

      TaxonomyField originalValue = originalItem[LOCATION_TAG_FIELD] as TaxonomyField;
      TaxonomyField databaseValue = databaseItem[LOCATION_TAG_FIELD] as TaxonomyField;

      switch (mode) {
        case RefreshMode.OverwriteCurrentValues:
          this.LocationTag = databaseValue;
          break;
        case RefreshMode.KeepCurrentValues:
          databaseItem[LOCATION_TAG_FIELD] = this.LocationTag;
          break;
        case RefreshMode.KeepChanges:
          if (this.LocationTag != originalValue)
            databaseItem[LOCATION_TAG_FIELD] = this.LocationTag;
          else if (this.LocationTag == originalValue && this.LocationTag != databaseValue)
            this.LocationTag = databaseValue;
          break;
      }
    }

    private TaxonomyField _taxonomyFields;
    public TaxonomyField LocationTag {
      get {
        return _taxonomyFields;
      }
      set {
        if (value != _taxonomyFields) {
          this.OnPropertyChanging("LocationTag", _taxonomyFields);
          _taxonomyFields = value;
          this.OnPropertyChanged("LocationTag");
        }
      }
    }

  }
}
