using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.Office.Server.Search.Query;
using Microsoft.Office.Server.Search.WebControls;

namespace CustomSearchParts.AssignedToMeResults
{
    [ToolboxItemAttribute(false)]
    public class AssignedToMeResults : CoreResultsWebPart
    {
        protected override System.Xml.XPath.XPathNavigator GetXPathNavigator(string viewPath)
        {
            QueryManager queryManager = SharedQueryManager.GetInstance(this.Page).QueryManager;
            queryManager.UserQuery += 
                " AssignedTo:" + SPContext.Current.Web.CurrentUser.LoginName +
                " AssignedTo:" + SPContext.Current.Web.CurrentUser.Name;
            return base.GetXPathNavigator(viewPath);
        }
    }
}
