using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Principal;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace SiteCollectionSecurity.Layouts {

  class PropertySetting {
    public string Property { get; set; }
    public string Setting { get; set; }
  }

  public partial class CurrentUser : LayoutsPageBase {

    protected override void OnPreRender(EventArgs e) {


      // Windows Security Context
      List<PropertySetting> windowsSecurityInfo = new List<PropertySetting>();
      WindowsIdentity windowsUserIdentity = WindowsIdentity.GetCurrent();
      windowsSecurityInfo.Add(new PropertySetting {
        Property = "User Logon Name",
        Setting = windowsUserIdentity.Name
      });
      windowsSecurityInfo.Add(new PropertySetting {
        Property = "Is User Authenticated",
        Setting = windowsUserIdentity.IsAuthenticated.ToString()
      });
      windowsSecurityInfo.Add(new PropertySetting {
        Property = "Authentication Type",
        Setting = windowsUserIdentity.AuthenticationType
      });
      windowsSecurityInfo.Add(new PropertySetting {
        Property = "ImpersonationLevel",
        Setting = windowsUserIdentity.ImpersonationLevel.ToString()
      });
      grdCurrentUserInfoWindows.DataSource = windowsSecurityInfo;
      grdCurrentUserInfoWindows.DataBind();

      // SharePoint Security Context
      List<PropertySetting> aspnetSecurityInfo = new List<PropertySetting>();
      IIdentity aspnetUserIdentity = this.Page.User.Identity;
      aspnetSecurityInfo.Add(new PropertySetting { Property = "User Name", Setting = aspnetUserIdentity.Name });
      aspnetSecurityInfo.Add(new PropertySetting { Property = "Is Authenticated", Setting = aspnetUserIdentity.IsAuthenticated.ToString() });
      aspnetSecurityInfo.Add(new PropertySetting { Property = "Authentication Type", Setting = aspnetUserIdentity.AuthenticationType.ToString() });
      aspnetSecurityInfo.Add(new PropertySetting { Property = "User Identity Type", Setting = aspnetUserIdentity.GetType().ToString() });


      grdCurrentUserInfoAspnet.DataSource = aspnetSecurityInfo;
      grdCurrentUserInfoAspnet.DataBind();


      // SharePoint Security Context    
      SPSite siteCollection = this.Site;
      SPWeb site = this.Web;
      SPUser currentUser = site.CurrentUser;
      SPPrincipalInfo currentPrincipal =
        SPUtility.ResolveWindowsPrincipal(
          SPWebApplication.Lookup(new Uri(this.Site.Url)),
          "",
          SPPrincipalType.All,
          false);


      List<PropertySetting> spSecurityInfo = new List<PropertySetting>();
      spSecurityInfo.Add(new PropertySetting { Property = "ID", Setting = currentUser.ID.ToString() });
      spSecurityInfo.Add(new PropertySetting { Property = "Name", Setting = currentUser.Name });
      spSecurityInfo.Add(new PropertySetting { Property = "EMail", Setting = currentUser.Email });
      spSecurityInfo.Add(new PropertySetting { Property = "Login Name", Setting = currentUser.LoginName });
      spSecurityInfo.Add(new PropertySetting { Property = "SID", Setting = currentUser.Sid });
      spSecurityInfo.Add(new PropertySetting { Property = "AllowBrowseUserInfo", Setting = currentUser.AllowBrowseUserInfo.ToString() });

      foreach (SPGroup group in currentUser.Groups) {
        spSecurityInfo.Add(new PropertySetting { Property = "Group Membership", Setting = group.Name });
      }

      grdCurrentUserInfoSharePoint.DataSource = spSecurityInfo;
      grdCurrentUserInfoSharePoint.DataBind();   
    }

  }
}
