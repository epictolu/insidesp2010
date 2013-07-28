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

namespace CustomSearchParts.RankingModelIdResults
{
    [ToolboxItemAttribute(false)]
    public class RankingModelIdResults : CoreResultsWebPart
    {
        //Use default ranking model to start
        private string rankingModelId = "8f6fd0bc-06f9-43cf-bbab-08c377e083f4";

        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true),
        WebDescription("The ID of the Ranking Model to use"),WebDisplayName("Ranking Model ID"),
        Category("Configuration")]
        public string RankingModelID
        {
            get { return rankingModelId; }
            set { rankingModelId = value; }
        }

        protected override System.Xml.XPath.XPathNavigator GetXPathNavigator(string viewPath)
        {
            try
            {
                QueryManager queryManager = SharedQueryManager.GetInstance(this.Page).QueryManager;
                foreach (LocationList locList in queryManager)
                {
                    foreach (Location loc in locList)
                        try { loc.RankingModelID = RankingModelID; }
                        catch { }
                }
            }
            catch { }

            return base.GetXPathNavigator(viewPath);
        }
    }
}
