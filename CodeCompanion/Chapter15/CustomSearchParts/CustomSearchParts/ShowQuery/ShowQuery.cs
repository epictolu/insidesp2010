using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.Office.Server.Search.Query;
using Microsoft.Office.Server.Search.WebControls;

namespace CustomSearchParts.ShowQuery
{
    [ToolboxItemAttribute(false)]
    public class ShowQuery : CoreResultsWebPart
    {

        Label queryText;

        protected override void CreateChildControls()
        {
            queryText = new Label();
            Controls.Add(queryText);
            base.CreateChildControls();

        }

        protected override System.Xml.XPath.XPathNavigator GetXPathNavigator(string viewPath)
        {

            QueryManager queryManager = SharedQueryManager.GetInstance(this.Page).QueryManager;
            queryText.Text = queryManager.UserQuery;
            return base.GetXPathNavigator(viewPath);
        }
    }
}
