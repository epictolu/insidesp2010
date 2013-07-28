using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Administration;
using System.Web.UI;

namespace WingtipControls {
  class WingtipMenuControl : Control {

    protected override void CreateChildControls() {
      SPWeb site = SPContext.Current.Web;

      SubMenuTemplate smt = new SubMenuTemplate();
      smt.ID = "CustomSubMenu";
      smt.Text = "Wingtip Menu Control";
      smt.Description = "Demo of custom flyout menu";
      smt.MenuGroupId = 1;
      smt.Sequence = 1;
      smt.ImageUrl = site.Url + @"/_layouts/images/WingtipControls/MenuIcon.gif";

      MenuItemTemplate mit1 = new MenuItemTemplate();
      mit1.ID = "submenu1";
      mit1.Text = "Custom Control Demo";
      mit1.Description = "hosted on an application page";
      mit1.Sequence = 1;
      mit1.ClientOnClickNavigateUrl = site.Url + "/_layouts/WingtipControls/CustomControlDemo.aspx";
      mit1.ImageUrl = site.Url + @"/_layouts/images/WingtipControls/SubMenuIcon.gif";
      smt.Controls.Add(mit1);

      MenuItemTemplate mit2 = new MenuItemTemplate();
      mit2.ID = "submenu2";
      mit2.Text = "User Control Demo";
      mit2.Description = "hosted on an application page";
      mit2.Sequence = 2;
      mit2.ClientOnClickNavigateUrl = site.Url + "/_layouts/WingtipControls/UserControlDemo.aspx";
      mit2.ImageUrl = site.Url + @"/_layouts/images/WingtipControls/SubMenuIcon.gif";
      smt.Controls.Add(mit2);

      this.Controls.Add(smt);
    }
  }
}