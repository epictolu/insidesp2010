using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipControls.WingtipControls
{
    public class WingtipMenuControl : Control
    {
        string subMenuIconUrl = @"/_layouts/images/WingtipControls/SubMenuIcon.gif";
        string menuIconUrl = @"/_layouts/images/WingtipControls/MenuIcon.gif";
        string appPage1Url = @"/_layouts/WingtipControls/CustomControlDemo.aspx";
        string appPage2Url = @"/_layouts/WingtipControls/UserControlDemo.aspx";

        protected override void CreateChildControls()
        {
            var site = SPContext.Current.Web;

            var subMenuTemplate = new SubMenuTemplate 
                { 
                    ID = "CustomSubMenu", 
                    Text = "Wingtip Menu Control", 
                    Description = "Demo of custom fly out menu", 
                    MenuGroupId = 1, 
                    Sequence = 1, 
                    ImageUrl = site.Url + subMenuIconUrl 
                };

            var menuItemTemplate1 = new MenuItemTemplate
                {
                    ID = "FlyoutMenu1",
                    Text = "Custom Control Demo",
                    Description = "hosted on an application page",
                    Sequence = 1,
                    ClientOnClickNavigateUrl = site.Url + appPage1Url,
                    ImageUrl = site.Url + menuIconUrl
                };

            var menuItemTemplate2 = new MenuItemTemplate
                {
                    ID = "FlyoutMenu2",
                    Text = "User Control Demo",
                    Description = "hosted on an application page",
                    Sequence = 2,
                    ClientOnClickNavigateUrl = site.Url + appPage2Url,
                    ImageUrl = site.Url + menuIconUrl
                };

            subMenuTemplate.Controls.Add(menuItemTemplate1);
            subMenuTemplate.Controls.Add(menuItemTemplate2);

            Controls.Add(subMenuTemplate);
        }
    }
}
