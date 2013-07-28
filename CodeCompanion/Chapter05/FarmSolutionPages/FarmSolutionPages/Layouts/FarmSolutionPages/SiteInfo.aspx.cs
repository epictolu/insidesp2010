using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

using Microsoft.SharePoint.WebControls;
//using Microsoft.SharePoint.WebPartPages;


namespace FarmSolutionPages {

  public partial class SiteInfo : LayoutsPageBase {

    protected override void OnInit(EventArgs e) {
      cmdReturn.Click += new EventHandler(cmdReturn_Click);
    }

    void cmdReturn_Click(object sender, EventArgs e) {
      SPUtility.Redirect("settings.aspx",
                         SPRedirectFlags.RelativeToLayoutsPage,
                         this.Context);

    }

    class ListInformationListItem {
      public string Title { get; set; }
      public string ID { get; set; }
      public string ItemCount { get; set; }
      public string Versioning { get; set; }
    }


   
    protected override void OnPreRender(EventArgs e) {


      var Lists = new List<ListInformationListItem>();
      foreach (SPList list in this.Web.Lists) {
        if (!list.Hidden) {
          Lists.Add(new ListInformationListItem {
            ID = list.ID.ToString(),
            Title = list.Title,
            ItemCount = list.Items.Count.ToString(),
            Versioning = list.EnableVersioning.ToString()
          });
        }        
      }
      grdLists.DataSource = Lists;
      grdLists.DataBind();

      var SystemLists = new List<ListInformationListItem>();
      foreach (SPList list in this.Web.Lists) {
        if (list.Hidden) {
          SystemLists.Add(new ListInformationListItem {
            ID = list.ID.ToString(),
            Title = list.Title,
            ItemCount = list.Items.Count.ToString(),
            Versioning = list.EnableVersioning.ToString()
          });
        }
      }
      grdSystemLists.DataSource = SystemLists;
      grdSystemLists.DataBind();

    }


  }
}
