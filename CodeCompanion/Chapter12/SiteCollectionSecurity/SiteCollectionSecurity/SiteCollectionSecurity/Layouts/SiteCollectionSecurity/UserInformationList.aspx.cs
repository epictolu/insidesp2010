using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SiteCollectionSecurity.Layouts.SiteCollectionSecurity {
  class UserInformationListItem {
    public string ID { get; set; }
    public string ContentType { get; set; }
    public string User { get; set; } // Title
    public string Account { get; set; } // Name
    public string ImnName { get; set; }
    public string EMail { get; set; }
    public string SipAddress { get; set; }
    public string IsSiteAdmin { get; set; }
  }

  public partial class UserInformationList : LayoutsPageBase {

    protected override void OnPreRender(EventArgs e) {

      SPSecurity.RunWithElevatedPrivileges(delegate() {
        using (SPSite siteCollection = new SPSite(this.Site.ID)) {
          SPWeb topLevelSite = siteCollection.RootWeb;
          var Users = new List<UserInformationListItem>();

          foreach (SPListItem user in topLevelSite.Lists["User Information List"].Items) {
            Users.Add(new UserInformationListItem {
              ID = user["ID"].ToString(),
              ContentType = user["ContentType"].ToString(),
              User = user["Title"] != null ? user["Title"].ToString() : "null",
              Account = user["Name"] != null ? user["Name"].ToString() : "null",
              ImnName = user["ImnName"] != null ? user["ImnName"].ToString() : "null",
              EMail = user["EMail"] != null ? user["EMail"].ToString() : "null",
              SipAddress = user["SipAddress"] != null ? user["SipAddress"].ToString() : "null",
              IsSiteAdmin = user["IsSiteAdmin"] != null ? user["IsSiteAdmin"].ToString() : "null"
            });
          }

          grdUserInformationList.DataSource = Users;
          grdUserInformationList.DataBind();
        }

      });


    }
  }
}
