using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ThrottleListQuery.ThrottleQueryPart {
  
  [ToolboxItemAttribute(false)]
  public class ThrottleQueryPart : WebPart {
    private bool overrideThrottling = false;

    [Personalizable(PersonalizationScope.Shared), WebBrowsable(true),
    WebDisplayName("Override Throttling"),
    WebDescription("Overrides list throttle settings")]
    public bool OverrideThrottling {
      get { return overrideThrottling; }
      set { overrideThrottling = value; }
    }

    protected override void RenderContents(HtmlTextWriter writer) {
      try {
        //Get list
        SPWeb site = SPContext.Current.Web;
        SPList list = site.Lists["Large List"];
        SPUser user = SPContext.Current.Web.CurrentUser;

        //Create query
        SPQuery query = new SPQuery();

        //Overriding allows auditors and administrators to return more than
        //the list limit all the way to the max limit specified by the WebApplication
        //Server admins have no limits on queries
        if (overrideThrottling)
          query.QueryThrottleMode = SPQueryThrottleOption.Override;
        else
          query.QueryThrottleMode = SPQueryThrottleOption.Strict;
        query.Query = "</OrderBy>";
        query.ViewFields = "<FieldRef Name=\"Title\" />";

        SPListItemCollection items = list.GetItems(query);

        //Show current role
        if (user.IsSiteAdmin || user.IsSiteAuditor)
          writer.Write("<p>You are a 'Super User'</p>");
        else
          writer.Write("<p>You are a regular user</p>");

        //Is throttling enabled?
        if (list.EnableThrottling)
          writer.Write("<p>Throttling is enabled</p>");
        else
          writer.Write("<p>Throttling is not enabled</p>");

        //Show count of items returned
        writer.Write("<p>" + items.Count + " items returned.</p>");

      }
      catch (Exception x) {
        writer.Write("<p>" + x.Message + "</p>");
      }
    }
  }
}
